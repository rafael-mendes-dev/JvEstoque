using JvEstoque.Api.Common.Api;
using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.ItensPedidos;
using JvEstoque.Core.Responses;

namespace JvEstoque.Api.Endpoints.ItemPedidos;

public class GetItemPedidoByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id:int}", HandleAsync)
            .WithName("ItemPedidos : GetById")
            .WithSummary("Obtém um item de pedido pelo ID")
            .WithDescription("Obtém um item de pedido pelo ID")
            .WithOrder(2)
            .Produces<Response<ItemPedido?>>();
    
    private static async Task<IResult> HandleAsync(
        IItemPedidoHandler handler,
        int id)
    {
        var request = new GetItemPedidoByIdRequest()
        {
            Id = id
        };
        
        var result = await handler.GetByIdAsync(request);
        return result.IsSucess ? TypedResults.Ok(result) : TypedResults.NotFound(result.Data);
    }
}