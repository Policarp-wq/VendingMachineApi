using VendingMachineApi.Models;

namespace VendingMachineApi.Repositories
{
    public interface ICoinRepository
    {
        Task<bool> AddCoin(int value, int delta);
        Task<IEnumerable<Coin>> GetAll();
        Task<bool> ReduceCoin(int value, int delta);
        Task<bool> UpdateCoinAmount(int value, int delta);
    }
}