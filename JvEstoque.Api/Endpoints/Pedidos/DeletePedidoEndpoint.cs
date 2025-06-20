using JvEstoque.Api.Common.Api;
using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.Pedidos;
using JvEstoque.Core.Responses;

namespace JvEstoque.Api.Endpoints.Pedidos;

public class DeletePedidoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id:int}", HandleAsync)
            .WithName("Pedidos : Delete")
            .WithSummary("Deleta um pedido")
            .WithDescription("Deleta um pedido pelo ID")
            .WithOrder(3)
            .Produces<Response<Pedido?>>();
    
    public static async Task<IResult> HandleAsync(
        IPedidoHandler handler,
        int id)
    {
        var request = new DeletePedidoRequest()
        {
            Id = id
        };
        
        var result = await handler.DeleteAsync(request);
        return result.IsSucess ? TypedResults.Ok(result) : TypedResults.BadRequest(result.Data);
    }
}