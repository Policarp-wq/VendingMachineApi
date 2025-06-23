using VendingMachineApi.ApiContracts;

namespace VendingMachineApi.Models
{
    public class Wallet
    {
        public static readonly Dictionary<int, CoinValue> Caster = Enum.GetValues<CoinValue>().ToDictionary(v => (int)v);
        public static readonly int[] CoinValuesDescending = Enum.GetValues<CoinValue>().Cast<int>().OrderByDescending(c => c).ToArray();
        private Dictionary<CoinValue, int> _coins;
        public int GetAmount(CoinValue val) => _coins.GetValueOrDefault(val);
        public int GetAmount(int val) => _coins.GetValueOrDefault(Caster.GetValueOrDefault(val));
        public int Total => _coins.Sum(c => (int)c.Key * c.Value);
        public List<CoinQuantity> Coins => Enum.GetValues<CoinValue>().Select(v => new CoinQuantity() { Quantity = GetAmount(v), ValueName = v }).ToList();
        public Wallet(IEnumerable<CoinQuantity> coins)
        {
            _coins = [];
            foreach (var coin in coins)
            {
                _coins.Add(coin.ValueName, coin.Quantity);
            }
        }
        public Wallet(IEnumerable<Coin> coins)
        {
            _coins = [];
            foreach (var coin in coins)
            {
                _coins.Add(Caster[coin.Value], coin.Amount);
            }
        }
        public Wallet GetSum(Wallet wallet)
        {
            List<CoinQuantity> coins = new List<CoinQuantity>();
            foreach (var value in Enum.GetValues<CoinValue>())
            {
                coins.Add(new CoinQuantity() { ValueName = value, Quantity = GetAmount(value) + wallet.GetAmount(value) });
            }
            return new Wallet(coins);
        }
        public static Wallet Add(Wallet a, Wallet b)
        {
            var coins = new List<CoinQuantity>();
            foreach (var value in Enum.GetValues<CoinValue>())
            {
                coins.Add(new CoinQuantity
                {
                    ValueName = value,
                    Quantity = a.GetAmount(value) + b.GetAmount(value)
                });
            }
            return new Wallet(coins);
        }
    }
}
