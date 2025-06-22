using Microsoft.EntityFrameworkCore;
using VendingMachineApi.Models;

namespace VendingMachineApi.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly AppDbContext _context;
        private DbSet<Brand> _brands => _context.Brands;

        public BrandRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Brand> CreateBrand(string name)
        {
            var brand = _brands.Add(new Brand { Name = name });
            await _context.SaveChangesAsync();
            return brand.Entity;
        }
        public async Task<List<Brand>> GetAll()
        {
            return await _brands.AsNoTracking().ToListAsync();
        }
    }
}
