using Microsoft.EntityFrameworkCore;
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
        public async Task<List<Coin>> GetAll()
        {
            return await _coins
                .AsNoTracking()
                .ToListAsync();
        }
        private async Task<Coin> CreateCoin(int value, int amount)
        {
            var coin = _coins.Add(new Coin()
            {
                Value = value,
                Amount = amount
            });
            await _context.SaveChangesAsync();
            return coin.Entity;
        }
        public async Task<Coin> SetCoinAmount(int value, int amount)
        {
            if (!Coin.AvailableValues.Contains(value))
                throw new InvalidOperation($"Value must be 1, 2, 5 or 10");
            var coin = await _coins.SingleOrDefaultAsync(c => c.Value == value);
            if (coin == null)
            {
                coin = (await _coins.AddAsync(new Coin { Value = value, Amount = amount })).Entity;
            }
            else
            {
                coin.Amount = amount;
            }
            await _context.SaveChangesAsync();
            return coin;
        }

        public async Task<Coin> UpdateCoinAmount(int value, int delta)
        {
            if (!Coin.AvailableValues.Contains(value))
                throw new InvalidOperation($"Value must be 1, 2, 5 or 10"); //change to be not hardcoded
            var coin = await _coins.SingleOrDefaultAsync(c => c.Value == value);
            if (coin == null)
            {
                if (delta < 0)
                    throw new InvalidOperation($"No coin with value {value} in machine");
                return await CreateCoin(value, delta);
            }
            else
            {
                if (delta < 0)
                    ReduceCoin(coin, -delta);
                else AddCoin(coin, delta);
                await _context.SaveChangesAsync();
            }
            return coin;
        }
        private static void ReduceCoin(Coin coin, int delta)
        {
            if (coin.Amount < delta)
                throw new InvalidOperation($"Amount of coin was lowers than tried to reduce: {coin.Amount} < {delta}");
            coin!.Amount -= delta;
        }
        private static void AddCoin(Coin coin, int delta)
        {
            coin.Amount += delta;
        }
    }
}
