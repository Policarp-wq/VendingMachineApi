using VendingMachineApi.ApiContracts;

namespace VendingMachineApi.Services
{
    public interface IBrandService
    {
        Task Create(string name);
        Task<IEnumerable<BrandInfo>> GetAll();
    }
}