namespace VendingMachineApi.Models
{
    public class Coin
    {
        public static int[] AvailableValues = [1, 2, 5, 10];
        public int Id { get; set; }
        public int Value { get; set; }
        public int Amount { get; set; }
    }
}
