using System.ComponentModel.DataAnnotations;

namespace VendingMachineApi.ApiContracts
{
    public class ProductCreateInfo : IValidatableObject
    {
        public string Name { get; }
        public int BrandId { get; }
        public int Amount { get; }
        public int Price { get; }
        public string Image { get; }

        public ProductCreateInfo(string name, int brandId, int amount, int price, string image)
        {
            Name = name;
            BrandId = brandId;
            Amount = amount;
            Price = price;
            Image = image;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Name)) yield return new ValidationResult("Product name must be a non-zero string");
            if (Amount <= 0) yield return new ValidationResult("Product amount must be natural");
            if (Price <= 0) yield return new ValidationResult("Product price must be natural");
        }
    }
}
