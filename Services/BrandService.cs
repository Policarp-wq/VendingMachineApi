using VendingMachineApi.Models;
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
        public async Task<IEnumerable<Brand>> GetAll()
        {
            return await _brandRepository.GetAll();
        }
    }
}
