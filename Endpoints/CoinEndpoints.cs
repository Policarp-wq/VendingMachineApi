using Microsoft.AspNetCore.Http.HttpResults;
using VendingMachineApi.ApiContracts;
using VendingMachineApi.Models;
using VendingMachineApi.Services;

namespace VendingMachineApi.Endpoints
{
    public static class CoinEndpoints
    {
        public static IEndpointRouteBuilder UseCoinEndpoints(this IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("coin");
            group.MapGet("/", GetAll);
            group.MapPost("/", ChangeCoinAmount);
            return builder;
        }
        public static async Task<Ok<List<Coin>>> GetAll(ICoinService service)
        {
            return TypedResults.Ok(await service.GetAll());
        }
        public static async Task<Results<Ok<Coin>, BadRequest>> ChangeCoinAmount(ICoinService service, CoinQuantity coinQuantity)
        {
            return TypedResults.Ok(await service.ChangeAmount(coinQuantity));
        }
    }
}
