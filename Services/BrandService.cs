using VendingMachineApi.ApiContracts;
using VendingMachineApi.Repositories;

namespace VendingMachineApi.Services
{
    public class BrandService : IBrandService
    {
        private IBrandRepository _brandRepository;

        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task Create(string name)
        {
            await _brandRepository.CreateBrand(name);
        }
        public async Task<IEnumerable<BrandInfo>> GetAll()
        {
            return (await _brandRepository.GetAll()).Select(BrandInfo.ToDto);
        }
    }
}
