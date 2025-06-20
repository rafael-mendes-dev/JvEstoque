using JvEstoque.Api.Common.Api;
using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.Pedidos;
using JvEstoque.Core.Responses;

namespace JvEstoque.Api.Endpoints.Pedidos;

public class UpdatePedidoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id:int}", HandleAsync)
            .WithName("Pedidos : Update")
            .WithSummary("Atualiza um pedido")
            .WithDescription("Atualiza os dados de um pedido existente")
            .WithOrder(5)
            .Produces<Response<Pedido?>>();
    
    private static async Task<IResult> HandleAsync(
        IPedidoHandler handler,
        int id,
        UpdatePedidoRequest request)
    {
        request.Id = id;
        var result = await handler.UpdateAsync(request);
        return result.IsSucess ? TypedResults.Ok(result) : TypedResults.BadRequest(result.Data);
    }
}