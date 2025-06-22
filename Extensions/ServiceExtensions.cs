using VendingMachineApi.Repositories;
using VendingMachineApi.Services;

namespace VendingMachineApi.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<ICoinRepository, CoinRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<ICoinService, CoinService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
