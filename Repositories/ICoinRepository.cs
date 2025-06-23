using VendingMachineApi.Models;

namespace VendingMachineApi.Repositories
{
    public interface ICoinRepository
    {
        //Task<bool> AddCoin(int value, int delta);
        //Task<bool> ReduceCoin(int value, int delta);
        Task<Coin> SetCoinAmount(int value, int amount);
        Task<List<Coin>> GetAll();
        Task<Coin> UpdateCoinAmount(int value, int delta);
    }
}