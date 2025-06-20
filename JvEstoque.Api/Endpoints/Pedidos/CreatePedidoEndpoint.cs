using JvEstoque.Api.Common.Api;
using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.Pedidos;
using JvEstoque.Core.Responses;

namespace JvEstoque.Api.Endpoints.Pedidos;

public class CreatePedidoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("Pedidos : Create")
            .WithSummary("Cria um novo pedido")
            .WithDescription("Cria um novo pedido")
            .WithOrder(1)
            .Produces<Response<Pedido?>>();
    
    private static async Task<IResult> HandleAsync(
        IPedidoHandler handler,
        CreatePedidoRequest request)
    {
        var result = await handler.CreateAsync(request);
        return result.IsSucess ? TypedResults.Created($"/{result.Data?.Id}", result) : TypedResults.BadRequest(result.Data);
    }
}