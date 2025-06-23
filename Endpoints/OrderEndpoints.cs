using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using VendingMachineApi.ApiContracts;
using VendingMachineApi.Models;
using VendingMachineApi.Services;

namespace VendingMachineApi.Endpoints
{
    public static class OrderEndpoints
    {
        public static IEndpointRouteBuilder UseOrderEndpoints(this IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("order");
            group.MapGet("/", GetOrders);
            group.MapPost("/", CreateOrder);
            return builder;
        }
        public static async Task<Results<Ok<List<CoinQuantity>>, BadRequest>> CreateOrder(IOrderService service, [FromBody] OrderInfo info)
        {
            return TypedResults.Ok(await service.CreateOrder(info));
        }
        public static async Task<Ok<List<Order>>> GetOrders(IOrderService service)
        {
            return TypedResults.Ok(await service.GetAll());
        }
    }
}
