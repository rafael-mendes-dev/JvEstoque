using JvEstoque.Api.Common.Api;
using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.VariacoesProdutos;
using JvEstoque.Core.Responses;

namespace JvEstoque.Api.Endpoints.VariacaoProdutos;

public class DeleteVariacaoProdutoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id:int}", HandleAsync)
            .WithName("VariacaoProdutos : Delete")
            .WithSummary("Deleta uma variação de produto")
            .WithDescription("Deleta uma variação de produto pelo ID")
            .WithOrder(4)
            .Produces<Response<VariacaoProduto?>>();
    
    public static async Task<IResult> HandleAsync(
        IVariacaoProdutoHandler handler,
        int id)
    {
        var request = new DeleteVariacaoProdutoRequest()
        {
            Id = id
        };
        
        var result = await handler.DeleteAsync(request);
        return result.IsSucess ? TypedResults.Ok(result) : TypedResults.BadRequest(result.Data);
    }
}