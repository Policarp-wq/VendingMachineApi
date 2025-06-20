using System.ComponentModel.DataAnnotations;

namespace VendingMachineApi.ApiContracts
{
    public class ProductQuantity : IValidatableObject
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ProductId <= 0)
                yield return new ValidationResult("Product id must be natural");
            if(Quantity <= 0)
                yield return new ValidationResult("Quantity must be natural");
        }
    }
}
