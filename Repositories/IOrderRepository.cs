using VendingMachineApi.ApiContracts;
using VendingMachineApi.Models;

namespace VendingMachineApi.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrder(IEnumerable<OrderItem> items, int total);
        Task<IEnumerable<Order>> GetAll();
    }
}
