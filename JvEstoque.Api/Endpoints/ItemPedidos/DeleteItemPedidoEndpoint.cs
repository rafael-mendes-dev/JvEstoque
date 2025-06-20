using JvEstoque.Api.Common.Api;
using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.ItensPedidos;
using JvEstoque.Core.Responses;

namespace JvEstoque.Api.Endpoints.ItemPedidos;

public class DeleteItemPedidoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id:int}", HandleAsync)
            .WithName("ItemPedidos : Delete")
            .WithSummary("Deleta um item de pedido")
            .WithDescription("Deleta um item de pedido pelo ID")
            .WithOrder(3)
            .Produces<Response<ItemPedido?>>();
    
    public static async Task<IResult> HandleAsync(
        IItemPedidoHandler handler,
        int id)
    {
        var request = new DeleteItemPedidoRequest()
        {
            Id = id
        };
        
        var result = await handler.DeleteAsync(request);
        return result.IsSucess ? TypedResults.Ok(result) : TypedResults.BadRequest(result.Data);
    }
}