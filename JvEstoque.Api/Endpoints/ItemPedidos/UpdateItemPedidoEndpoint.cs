using JvEstoque.Api.Common.Api;
using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.ItensPedidos;
using JvEstoque.Core.Responses;

namespace JvEstoque.Api.Endpoints.ItemPedidos;

public class UpdateItemPedidoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id:int}", HandleAsync)
            .WithName("ItemPedidos : Update")
            .WithSummary("Atualiza um item de pedido")
            .WithDescription("Atualiza um item de pedido pelo ID")
            .WithOrder(3)
            .Produces<Response<ItemPedido?>>();
    
    private static async Task<IResult> HandleAsync(
        IItemPedidoHandler handler,
        int id,
        UpdateItemPedidoRequest request)
    {
        request.Id = id;
        var result = await handler.UpdateAsync(request);
        return result.IsSucess ? TypedResults.Ok(result) : TypedResults.BadRequest(result.Data);
    }
}