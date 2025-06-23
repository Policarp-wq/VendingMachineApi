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
        public async Task<Product> CreateProduct(ProductCreateInfo createInfo)
        {
            return await _productRepository.CreateProduct(createInfo);
        }

        public async Task<int> GetTotal(List<ProductQuantity> products)
        {
            return await _productRepository.GetTotal(products);
        }

        public async Task<IEnumerable<ProductInfo>> GetAll()
        {
            return (await _productRepository.GetAll()).Select(ProductInfo.ToDto);
        }

        public async Task<Product?> GetById(int id)
        {
            return await _productRepository.GetById(id);
        }

        public async Task<IEnumerable<ProductInfo>> GetFiltered(ProductFilter filter)
        {
            return (await _productRepository.GetFiltered(filter)).Select(ProductInfo.ToDto);
        }

        public async Task<bool> ReduceAmount(int productId, int delta)
        {
            return await _productRepository.ReduceAmount(productId, delta);
        }
    }
}
