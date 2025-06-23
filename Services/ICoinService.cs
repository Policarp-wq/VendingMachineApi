using VendingMachineApi.ApiContracts;
using VendingMachineApi.Models;

namespace VendingMachineApi.Services
{
    public interface ICoinService
    {
        //Task AddCoin(CoinQuantity coinQuantity);
        Task<Coin> ChangeAmount(CoinQuantity coinQuantity);
        Task<List<Coin>> GetAll();
        //Task ReduceCoin(CoinQuantity coinQuantity);
    }
}