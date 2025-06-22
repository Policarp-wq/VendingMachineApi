using VendingMachineApi.ApiContracts;
using VendingMachineApi.Exceptions;
using VendingMachineApi.Models;
using VendingMachineApi.Repositories;
using VendingMachineApi.Utility;

namespace VendingMachineApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IProductRepository _productRepository;
        private readonly ICoinRepository _coinRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderService(AppDbContext appDbContext, IProductRepository productRepository, ICoinRepository coinRepository, IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _coinRepository = coinRepository;
            _orderRepository = orderRepository;
            _appDbContext = appDbContext;
        }
        public async Task<List<CoinQuantity>> CreateOrder(OrderInfo orderInfo)
        {
            if (orderInfo.Coins.Sum(c => c.Quantity * (int)c.ValueName) < orderInfo.Total)
                throw new InvalidOperation("Customer don't have enough coins");

            int total = await _productRepository.GetTotal(orderInfo.Products);
            if (orderInfo.Total != total)
                throw new ServerException($"Customer total is different than actual: {total}", StatusCodes.Status400BadRequest);

            var change = await GetChange(orderInfo.Coins);
            await CommitOrder(orderInfo.Products, change.Coins, total);
            
            return change.Coins;
        }
        private async Task<Wallet> GetChange(List<CoinQuantity> customerCoins)
        {
            var availableCoins = await _coinRepository.GetAll();
            bool isChangePossible = CoinsManager.TryGetChange(new Wallet(availableCoins), new Wallet(customerCoins), out Wallet change);
            if (!isChangePossible)
                throw new InvalidOperation("Cannot give change for customers coins");
            return change;
        }
        private async Task CommitOrder(List<ProductQuantity> products, List<CoinQuantity> coinsDiff, int total)
        {
            using var transaction = _appDbContext.Database.BeginTransaction();
            List<OrderItem> items = [];
            products.ForEach(async p =>
            {
                await _productRepository.ReduceAmount(p.ProductId, p.Quantity);
                var product = (await _productRepository.GetById(p.ProductId))!;
                items.Add(new OrderItem()
                {
                    Brand = product.Brand.Name,
                    Name = product.Name,
                    Amount = p.Quantity,
                });
            });
            coinsDiff.ForEach(async c =>
            {
                await _coinRepository.ReduceCoin((int)c.ValueName, c.Quantity);
            });
            await _orderRepository.CreateOrder(items, total);
            await transaction.CommitAsync();
        }
        public async Task<List<Order>> GetAll()
        {
            return await _orderRepository.GetAll();
        }
    }
}
