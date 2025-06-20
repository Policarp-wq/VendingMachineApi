using System.Runtime.CompilerServices;
using VendingMachineApi.ApiContracts;
using VendingMachineApi.Exceptions;
using VendingMachineApi.Models;
using VendingMachineApi.Repositories;
using VendingMachineApi.Utility;

namespace VendingMachineApi.Services
{
    public class OrderService
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
            int total = await _productRepository.GetTotal(orderInfo.Products);
            if (orderInfo.Total != total)
                throw new ServerException($"User total is different than actual: {total}", StatusCodes.Status400BadRequest);
            var availableCoins = await _coinRepository.GetAll();
            var customerCoins = orderInfo.Coins;
            bool isChangePossible = CoinsManager.TryGetChange(new Wallet(availableCoins), new Wallet(customerCoins), out Wallet change);
            if (!isChangePossible)
                throw new InvalidOperation("Cannot give change for customers coins");
            using(var transaction = _appDbContext.Database.BeginTransaction())
            {
                List<OrderItem> items = [];
                orderInfo.Products.ForEach(async p =>
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
                change.Coins.ForEach(async c =>
                {
                    await _coinRepository.ReduceCoin((int)c.ValueName, c.Quantity);
                });
                await _orderRepository.CreateOrder(items, total);
                await transaction.CommitAsync();
            }
            return change.Coins;
        }
        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _orderRepository.GetAll();
        }
    }
}
