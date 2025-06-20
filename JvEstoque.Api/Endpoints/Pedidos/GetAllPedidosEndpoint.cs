using JvEstoque.Api.Common.Api;
using JvEstoque.Core;
using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.Pedidos;
using JvEstoque.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace JvEstoque.Api.Endpoints.Pedidos;

public class GetAllPedidosEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapGet("/", HandleAsync)
        .WithName("Pedidos : GetAll")
        .WithSummary("Obtém todos os pedidos")
        .WithDescription("Obtém todos os pedidos")
        .WithOrder(2)
        .Produces<Response<List<Pedido>?>>();
    
    private static async Task<IResult> HandleAsync(
        IPedidoHandler handler,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = Configuration.DefaultPageSize)
    {
        
        var request = new GetAllPedidosRequest
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        var result = await handler.GetAllAsync(request);
        return result.IsSucess ? TypedResults.Ok(result) : TypedResults.BadRequest(result.Data);
    }
}