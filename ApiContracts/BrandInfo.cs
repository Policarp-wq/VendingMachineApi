using VendingMachineApi.Models;

namespace VendingMachineApi.ApiContracts
{
    public record BrandInfo(string Name)
    {
        public static BrandInfo ToDto(Brand brand) => new BrandInfo(brand.Name);
    }
}
