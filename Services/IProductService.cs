using VendingMachineApi.ApiContracts;
using VendingMachineApi.Filters;
using VendingMachineApi.Models;

namespace VendingMachineApi.Services
{
    public interface IProductService
    {
        Task<Product> CreateProduct(ProductCreateInfo createInfo);
        Task<IEnumerable<Product>> GetAll();
        Task<Product?> GetById(int id);
        Task<IEnumerable<Product>> GetFiltered(ProductFilter filter);
        Task<int> GetTotal(List<ProductQuantity> products);
        Task<bool> ReduceAmount(int productId, int delta);
    }
}