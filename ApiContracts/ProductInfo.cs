using VendingMachineApi.Models;

namespace VendingMachineApi.ApiContracts
{
    public record ProductInfo(int Id, string Name, string Brand, int Price, int Amount, string? Image)
    {
        public static ProductInfo ToDto(Product product)
            => new ProductInfo(product.Id, product.Name, product.Brand.Name, product.Price, product.Amount, product.Image);
    }
}
