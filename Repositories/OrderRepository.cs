using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using VendingMachineApi.Models;

namespace VendingMachineApi.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private AppDbContext _context;
        private DbSet<Order> _orders => _context.Orders;

        public OrderRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<Order> CreateOrder(IEnumerable<OrderItem> items, int total)
        {
            var res = _orders.Add(new Order()
            {
                Date = DateTime.UtcNow,
                Details = JsonSerializer.Serialize(items),
                Sum = total,
            });
            await _context.SaveChangesAsync();
            return res.Entity;
        }
        //can be added pagination
        public async Task<List<Order>> GetAll()
        {
            return await _orders
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
