using VendingMachineApi.Models;

namespace VendingMachineApi.Services
{
    public interface IBrandService
    {
        Task<Brand> Create(string name);
        Task<IEnumerable<Brand>> GetAll();
    }
}