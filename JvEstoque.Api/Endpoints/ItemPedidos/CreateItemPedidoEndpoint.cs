using JvEstoque.Api.Common.Api;
using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.ItensPedidos;
using JvEstoque.Core.Responses;

namespace JvEstoque.Api.Endpoints.ItemPedidos;

public class CreateItemPedidoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("ItemPedidos : Create")
            .WithSummary("Cria um novo item de pedido")
            .WithDescription("Cria um novo item de pedido")
            .WithOrder(1)
            .Produces<Response<ItemPedido?>>();
    
    private static async Task<IResult> HandleAsync(
        IItemPedidoHandler handler,
        CreateItemPedidoRequest request)
    {
        var result = await handler.CreateAsync(request);
        return result.IsSucess ? TypedResults.Created($"/{result.Data?.Id}", result) : TypedResults.BadRequest(result.Data);
    }
}