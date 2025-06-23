using System.ComponentModel.DataAnnotations;

namespace VendingMachineApi.ApiContracts
{
    public class OrderInfo : IValidatableObject
    {
        public List<ProductQuantity> Products { get; set; } = [];
        public List<CoinQuantity> Coins { get; set; } = [];
        public int Total { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Products.Count == 0)
                yield return new ValidationResult("There must be at least one product in order");
        }
    }
}
