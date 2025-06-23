using VendingMachineApi.Models;

namespace VendingMachineApi.Filters
{
    public static class QueryableExtensions
    {
        public static IQueryable<Product> ApplyFilters(this IQueryable<Product> q, ProductFilter filter)
        {
            if (!string.IsNullOrWhiteSpace(filter.Brand))
                q = q.Where(p => p.Brand.Name.Equals(filter.Brand));
            if (filter.MinPrice != null)
                q = q.Where(p => p.Price >= filter.MinPrice);
            if (filter.MaxPrice != null)
                q = q.Where(p => p.Price <= filter.MaxPrice);
            return q;
        }
    }
}
