using VendingMachineApi.ApiContracts;
using VendingMachineApi.Filters;
using VendingMachineApi.Models;
using VendingMachineApi.Repositories;

namespace VendingMachineApi.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public Task<Product> CreateProduct(ProductCreateInfo createInfo)
        {
            return _productRepository.CreateProduct(createInfo);
        }

        public Task<int> GetTotal(List<ProductQuantity> products)
        {
            return _productRepository.GetTotal(products);
        }

        public Task<IEnumerable<Product>> GetAll()
        {
            return _productRepository.GetAll();
        }

        public Task<Product?> GetById(int id)
        {
            return _productRepository.GetById(id);
        }

        public Task<IEnumerable<Product>> GetFiltered(ProductFilter filter)
        {
            return _productRepository.GetFiltered(filter);
        }

        public Task<bool> ReduceAmount(int productId, int delta)
        {
            return _productRepository.ReduceAmount(productId, delta);
        }
    }
}
