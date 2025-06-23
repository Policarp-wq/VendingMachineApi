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

            var availableCoins = await _coinRepository.GetAll();

            var customerWallet = new Wallet(orderInfo.Coins);
            var machineWallet = new Wallet(availableCoins);
            machineWallet = Wallet.Add(machineWallet, customerWallet);

            var change = GetChange(machineWallet, customerWallet, total);
            await CommitOrder(machineWallet, orderInfo.Products, change, total);

            return change.Coins;
        }
        private Wallet GetChange(Wallet machineWallet, Wallet customerWallet, int orderTotal)
        {
            bool isChangePossible = CoinsManager.TryGetChange(machineWallet, customerWallet, orderTotal, out Wallet change);
            if (!isChangePossible)
                throw new InvalidOperation("Cannot give change for customers coins");
            return change;
        }
        private async Task CommitOrder(Wallet machineWallet, List<ProductQuantity> products, Wallet change, int total)
        {
            await using var transaction = await _appDbContext.Database.BeginTransactionAsync();
            var items = new List<OrderItem>();
            foreach (var p in products)
            {
                await _productRepository.ReduceAmount(p.ProductId, p.Quantity);
                var product = (await _productRepository.GetById(p.ProductId))!;
                items.Add(new OrderItem
                {
                    Brand = product.Brand.Name,
                    Name = product.Name,
                    Amount = p.Quantity,
                });
            }
            foreach (var coin in Wallet.CoinValuesDescending)
            {
                int changeAmount = change.GetAmount(coin);
                int newAmount = machineWallet.GetAmount(coin) - changeAmount;
                if (newAmount < 0)
                    throw new InvalidOperation("Attempted to set coin amount to negative value");
                await _coinRepository.SetCoinAmount(coin, newAmount);
            }
            await _orderRepository.CreateOrder(items, total);
            await transaction.CommitAsync();
        }

        public async Task<List<Order>> GetAll()
        {
            return await _orderRepository.GetAll();
        }
    }
}
