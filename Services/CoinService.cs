using VendingMachineApi.ApiContracts;
using VendingMachineApi.Models;
using VendingMachineApi.Repositories;

namespace VendingMachineApi.Services
{
    public class CoinService : ICoinService
    {
        private readonly ICoinRepository _coinRepository;

        public CoinService(ICoinRepository coinRepository)
        {
            _coinRepository = coinRepository;
        }
        public async Task<List<Coin>> GetAll()
        {
            return await _coinRepository.GetAll();
        }

        public async Task<Coin> ChangeAmount(CoinQuantity coinQuantity)
        {
            return await _coinRepository.UpdateCoinAmount((int)coinQuantity.ValueName, coinQuantity.Quantity);
        }
    }
}
