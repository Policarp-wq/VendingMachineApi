using VendingMachineApi.Models;

namespace VendingMachineApi.Repositories
{
    public interface IBrandRepository
    {
        Task<Brand> CreateBrand(string name);
        Task<IEnumerable<Brand>> GetAll();
    }
}