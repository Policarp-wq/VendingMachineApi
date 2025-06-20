using VendingMachineApi.Models;

namespace VendingMachineApi.ApiContracts
{
    public class CoinQuantity
    {
        public CoinValue ValueName { get; set; }
        public int Quantity { get; set; }
    }
}
