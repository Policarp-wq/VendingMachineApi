using VendingMachineApi.ApiContracts;
using VendingMachineApi.Filters;
using VendingMachineApi.Models;

namespace VendingMachineApi.Repositories
{
    public interface IProductRepository
    {
        Task<Product> CreateProduct(ProductCreateInfo createInfo);
        Task<IEnumerable<Product>> GetAll();
        Task<IEnumerable<Product>> GetFiltered(ProductFilter filter);
        Task<bool> ReduceAmount(int productId, int delta);
    }
}