using JvEstoque.Api.Common.Api;
using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.Pedidos;
using JvEstoque.Core.Responses;

namespace JvEstoque.Api.Endpoints.Pedidos;

public class GetPedidoByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id:int}", HandleAsync)
            .WithName("Pedidos : GetById")
            .WithSummary("Obtém um pedido pelo ID")
            .WithDescription("Obtém os detalhes de um pedido específico pelo ID")
            .WithOrder(3)
            .Produces<Response<Pedido?>>();
    
    private static async Task<IResult> HandleAsync(
        IPedidoHandler handler,
        int id)
    {
        var request = new GetPedidoByIdRequest
        {
            Id = id
        };
        
        var result = await handler.GetByIdAsync(request);
        return result.IsSucess ? TypedResults.Ok(result) : TypedResults.BadRequest(result.Data);
    }
}