using VendingMachineApi.ApiContracts;
using VendingMachineApi.Models;

namespace VendingMachineApi.Utility
{
    public static class CoinsManager
    {
        public static bool TryGetChange(Wallet have, Wallet spend, int orderTotal, out Wallet changeRes)
        {
            int[] availableValues = Wallet.CoinValuesDescending;
            int rest = spend.Total - orderTotal;
            List<CoinQuantity> change = [];
            foreach (var val in availableValues)
            {
                int left = have.GetAmount(val) - rest / val;
                if (left < 0)
                    left = 0;
                int given = (have.GetAmount(val) - left);
                rest -= given * val;
                change.Add(new CoinQuantity { ValueName = Wallet.Caster[val], Quantity = given });
            }
            changeRes = new Wallet(change);
            return rest == 0;
        }
    }
}
