using VendingMachineApi.ApiContracts;
using VendingMachineApi.Models;

namespace VendingMachineApi.Services
{
    public interface IOrderService
    {
        Task<List<CoinQuantity>> CreateOrder(OrderInfo orderInfo);
        Task<List<Order>> GetAll();
    }
}