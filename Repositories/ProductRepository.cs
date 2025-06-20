using Microsoft.EntityFrameworkCore;
using VendingMachineApi.ApiContracts;
using VendingMachineApi.Exceptions;
using VendingMachineApi.Filters;
using VendingMachineApi.Models;

namespace VendingMachineApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        private DbSet<Product> _products => _context.Products;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _products
                .AsNoTracking()
                .Include(p => p.Brand)
                .ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetFiltered(ProductFilter filter)
        {
            return await _products
                .AsNoTracking()
                .ApplyFilters(filter)
                .ToListAsync();
        }
        public async Task<Product> CreateProduct(ProductCreateInfo createInfo)
        {
            if (!await _context.Brands.AnyAsync(b => b.Id == createInfo.BrandId))
                throw new InvalidOperation("Brand id was incorrect");
            var res = _products.Add(new Product()
            {
                Name = createInfo.Name,
                Amount = createInfo.Amount,
                BrandId = createInfo.BrandId,
                Price = createInfo.Price,
            });
            await _context.SaveChangesAsync();
            return res.Entity;
        }
        public async Task<bool> ReduceAmount(int productId, int delta)
        {
            var product = await _products.SingleOrDefaultAsync(p => p.Id == productId);
            if (product == null)
                return false;
            if (product.Amount < delta)
                throw new InvalidOperation($"Amount of product was lowers than tried to reduce: {product.Amount} < {delta}");
            product.Amount -= delta;
            return await _context.SaveChangesAsync() > 0;
        }
        //maybe use dapper to write custom sql with higher perfomance
        public async Task<int> GetTotal(List<ProductQuantity> products)
        {
            int[] ids = products.Select(p => p.ProductId).ToArray();
            var info = await _products
                .AsNoTracking()
                .Where(p => ids.Contains(p.Id))
                .Select(p =>
                new
                {
                    p.Id,
                    p.Price,
                    p.Amount
                })
                .ToListAsync();
            var productInfoWithQuantityAndPrice = info.Join(products, i => i.Id, p => p.ProductId, (inf, pr) =>
                new
                {
                    inf.Amount,
                    inf.Price,
                    pr.Quantity
                });
            return productInfoWithQuantityAndPrice.Sum(inf => inf.Price * inf.Quantity);
        }

        public async Task<Product?> GetById(int id)
        {
            return await _products
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == id);
        }
    }
}
