namespace VendingMachineApi.Models
{
    public class OrderItem
    {
        public string Brand { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int Amount { get; set; }
    }
}
