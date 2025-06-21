using JvEstoque.Api.Common.Api;
using JvEstoque.Core;
using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.VariacoesProdutos;
using JvEstoque.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace JvEstoque.Api.Endpoints.VariacaoProdutos;

public class GetAllVariacoesProdutosByEscolaIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapGet("/escolas/{escolaId:int}", HandleAsync)
        .WithName("VariacaoProdutos : GetAllByEscolaId")
        .WithSummary("Obtém todas as variações de produtos por ID da escola")
        .WithDescription("Obtém todas as variações de produtos associadas a uma escola pelo ID")
        .WithOrder(3)
        .Produces<Response<List<VariacaoProduto>?>>();
    
    private static async Task<IResult> HandleAsync(
        IVariacaoProdutoHandler handler,
        int escolaId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetAllVariacoesProdutosByEscolaIdRequest
        {
            EscolaId = escolaId,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        
        var result = await handler.GetAllByEscolaIdAsync(request);
        return result.IsSucess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
    }
}