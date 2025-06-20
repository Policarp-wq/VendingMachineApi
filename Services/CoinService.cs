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
        public async Task AddCoin(CoinQuantity coinQuantity)
        {
            await _coinRepository.AddCoin((int)coinQuantity.ValueName, coinQuantity.Quantity);
        }
        public async Task ReduceCoin(CoinQuantity coinQuantity)
        {
            await _coinRepository.ReduceCoin((int)coinQuantity.ValueName, coinQuantity.Quantity);
        }
        public async Task<IEnumerable<Coin>> GetAll()
        {
            return await _coinRepository.GetAll();
        }
    }
}
