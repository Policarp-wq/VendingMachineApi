namespace VendingMachineApi.Filters
{
    public class ProductFilter
    {
        public int? BrandId { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
    }
}
