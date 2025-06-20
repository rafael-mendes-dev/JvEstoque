using JvEstoque.Api.Common.Api;
using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.VariacoesProdutos;
using JvEstoque.Core.Responses;

namespace JvEstoque.Api.Endpoints.VariacaoProdutos;

public class GetVariacaoProdutoByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id:int}", HandleAsync)
            .WithName("VariacaoProdutos : GetById")
            .WithSummary("Obtém uma variação de produto pelo ID")
            .WithDescription("Obtém uma variação de produto pelo ID")
            .WithOrder(2)
            .Produces<Response<VariacaoProduto?>>();
    
    private static async Task<IResult> HandleAsync(
        IVariacaoProdutoHandler handler,
        int id)
    {
        var request = new GetVariacaoProdutoByIdRequest
        {
            Id = id
        };
        
        var result = await handler.GetByIdAsync(request);
        return result.IsSucess ? TypedResults.Ok(result) : TypedResults.BadRequest(result.Data);
    }
}