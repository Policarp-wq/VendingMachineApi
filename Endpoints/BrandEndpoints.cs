using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using VendingMachineApi.ApiContracts;
using VendingMachineApi.Filters;
using VendingMachineApi.Models;
using VendingMachineApi.Services;

namespace VendingMachineApi.Endpoints
{
    public static class BrandEndpoints
    {
        public static IEndpointRouteBuilder UseBrandEndpoints(this IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("brand");
            group.MapGet("/", GetAll);
            group.MapPost("/", CreateBrand);
            return builder;
        }

        public static async Task<Ok<IEnumerable<Brand>>> GetAll(IBrandService brandService)
        {
            var brands = await brandService.GetAll();
            return TypedResults.Ok(brands);
        }
        public static async Task<Results<Ok, BadRequest<string>>> CreateBrand(IBrandService brandService, [FromQuery] string name)
        {
            if(string.IsNullOrWhiteSpace(name))
                return TypedResults.BadRequest("Brand name must be a valid stirng");
            await brandService.Create(name);
            return TypedResults.Ok();
        }
    }
}
