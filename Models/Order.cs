namespace VendingMachineApi.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Details { get; set; } = null!; //Store as JSON cuz task requires details to be in the same row
        public int Sum { get; set; }
    }
}
