using VendingMachineApi.ApiContracts;
using VendingMachineApi.Models;

namespace VendingMachineApi.Services
{
    public interface ICoinService
    {
        Task AddCoin(CoinQuantity coinQuantity);
        Task<List<Coin>> GetAll();
        Task ReduceCoin(CoinQuantity coinQuantity);
    }
}