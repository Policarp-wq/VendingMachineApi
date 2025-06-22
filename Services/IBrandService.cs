using VendingMachineApi.Models;

namespace VendingMachineApi.Services
{
    public interface IBrandService
    {
        Task Create(string name);
        Task<IEnumerable<Brand>> GetAll();
    }
}