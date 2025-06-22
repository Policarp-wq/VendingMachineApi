namespace VendingMachineApi.Filters
{
    public class ProductFilter
    {
        public string? Brand { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }

        public ProductFilter(string? brandId, int? minPrice, int? maxPrice)
        {
            Brand = brandId;
            MinPrice = minPrice;
            MaxPrice = maxPrice;
        }
    }
}
