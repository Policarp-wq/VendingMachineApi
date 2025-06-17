namespace VendingMachineApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Price { get; set; }
        public int Amount { get; set; }
        public string? Image { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; } = null!;
    }
}
