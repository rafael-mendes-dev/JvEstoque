using JvEstoque.Api.Common.Api;
using JvEstoque.Core;
using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.VariacoesProdutos;
using JvEstoque.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace JvEstoque.Api.Endpoints.VariacaoProdutos;

public class GetAllVariacoesProdutosByProdutoIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapGet("/produtos/{produtoId:int}", HandleAsync)
        .WithName("VariacaoProdutos : GetAllByProdutoId")
        .WithSummary("Obtém todas as variações de produto por ID do produto")
        .WithDescription("Obtém todas as variações de produto associadas a um produto específico pelo ID")
        .WithOrder(2)
        .Produces<Response<List<VariacaoProduto>?>>();
    
    private static async Task<IResult> HandleAsync(
        IVariacaoProdutoHandler handler,
        int produtoId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetAllVariacoesProdutosByProdutoIdRequest
        {
            ProdutoId = produtoId,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        
        var result = await handler.GetAllByProdutoIdAsync(request);
        return result.IsSucess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
    }
}