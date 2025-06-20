using JvEstoque.Api.Common.Api;
using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.Produtos;
using JvEstoque.Core.Responses;

namespace JvEstoque.Api.Endpoints.Produtos;

public class UpdateProdutoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id:int}", HandleAsync)
            .WithName("Produtos : Update")
            .WithSummary("Atualiza um produto")
            .WithDescription("Atualiza um produto pelo ID")
            .WithOrder(4)
            .Produces<Response<Produto?>>();
    
    private static async Task<IResult> HandleAsync(
        IProdutoHandler handler,
        int id,
        UpdateProdutoRequest request)
    {
        request.Id = id;
        var result = await handler.UpdateAsync(request);
        return result.IsSucess ? TypedResults.Ok(result) : TypedResults.BadRequest(result.Data);
    }
}