using Microsoft.EntityFrameworkCore;
using VendingMachineApi.ApiContracts;
using VendingMachineApi.Exceptions;
using VendingMachineApi.Models;

namespace VendingMachineApi.Repositories
{
    public class CoinRepository : ICoinRepository
    {
        private readonly AppDbContext _context;
        private DbSet<Coin> _coins => _context.Coins;

        public CoinRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Coin>> GetAll()
        {
            return await _coins
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<bool> AddCoin(int value, int delta)
        {
            var coin = await _coins.Where(c => c.Value == value).FirstAsync();
            coin!.Amount += delta;
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateCoinAmount(int value, int delta)
        {
            if (!Coin.AvailableValues.Contains(value))
                throw new InvalidOperation($"Value must be 1, 2, 5 or 10"); //change to be not hardcoded
            if (delta <= 0)
                return await ReduceCoin(value, -delta);
            else return await AddCoin(value, delta);
        }
        public async Task<bool> ReduceCoin(int value, int delta)
        {
            var coin = await _coins.Where(c => c.Value == value).FirstAsync();
            if (coin.Amount < delta)
                throw new InvalidOperation($"Amount of coin was lowers than tried to reduce: {coin.Amount} < {delta}");
            coin!.Amount -= delta;
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
