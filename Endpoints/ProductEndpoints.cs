using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using VendingMachineApi.ApiContracts;
using VendingMachineApi.Filters;
using VendingMachineApi.Models;
using VendingMachineApi.Services;

namespace VendingMachineApi.Endpoints
{
    public static class ProductEndpoints
    {
        public static IEndpointRouteBuilder UseProductEndpoints(this IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("product");
            group.MapGet("/", GetAll);
            group.MapPost("/", CreateProduct);
            group.MapGet("/filtered", GetFiltered);
            return builder;
        }

        public static async Task<Ok<IEnumerable<Product>>> GetAll(IProductService productService)
        {
            var products = await productService.GetAll();
            return TypedResults.Ok(products);
        }
        public static async Task<Ok<Product>> CreateProduct(IProductService productService, [FromBody] ProductCreateInfo createInfo)
        {
            return TypedResults.Ok(await productService.CreateProduct(createInfo));
        }
        public static async Task<Results<Ok<IEnumerable<Product>>, BadRequest<string>>> GetFiltered(IProductService productService, [FromQuery] string? brand, int? minPrice , int? maxPrice)
        {
            if (minPrice is < 0)
                return TypedResults.BadRequest("Min price is negative");
            if (maxPrice is < 0)
                return TypedResults.BadRequest("Max price is negative");
            if (maxPrice != null && minPrice != null && maxPrice - minPrice < 0)
                return TypedResults.BadRequest("Max price must be higher or equal than min");
            return TypedResults.Ok(await productService.GetFiltered(new ProductFilter(brand, minPrice, maxPrice)));
        }
    }
}
