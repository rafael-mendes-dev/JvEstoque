using JvEstoque.Api.Common.Api;
using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.VariacoesProdutos;
using JvEstoque.Core.Responses;

namespace JvEstoque.Api.Endpoints.VariacaoProdutos;

public class UpdateVariacaoProdutoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id:int}", HandleAsync)
            .WithName("VariacaoProdutos : Update")
            .WithSummary("Atualiza uma variação de produto")
            .WithDescription("Atualiza uma variação de produto pelo ID")
            .WithOrder(2)
            .Produces<Response<VariacaoProduto?>>();
    
    private static async Task<IResult> HandleAsync(
        IVariacaoProdutoHandler handler,
        int id,
        UpdateVariacaoProdutoRequest request)
    {
        request.Id = id;
        var result = await handler.UpdateAsync(request);
        return result.IsSucess ? TypedResults.Ok(result) : TypedResults.BadRequest(result.Data);
    }
}