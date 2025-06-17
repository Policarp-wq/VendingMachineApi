using VendingMachineApi.Models;

namespace VendingMachineApi.Filters
{
    public static class QueryableExtensions
    {
        public static IQueryable<Product> ApplyFilters(this IQueryable<Product> q, ProductFilter filter)
        {
            if (filter.BrandId != null)
                q = q.Where(p => p.BrandId == filter.BrandId);
            if(filter.MinPrice != null && filter.MaxPrice != null)
                q = q.Where(p => p.Price >=  filter.MinPrice && p.Price <= filter.MaxPrice);
            return q;
        }
    }
}
