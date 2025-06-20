using Azure;
using JvEstoque.Api.Common.Api;
using JvEstoque.Core;
using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.Produtos;
using Microsoft.AspNetCore.Mvc;

namespace JvEstoque.Api.Endpoints.Produtos;

public class GetAllProdutosEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .WithName("Produtos : GetAll")
            .WithSummary("Obtém todos os produtos")
            .WithDescription("Obtém todos os produtos do estoque")
            .WithOrder(2)
            .Produces<Response<List<Produto>?>>();
    
    private static async Task<IResult> HandleAsync(
        IProdutoHandler handler,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetAllProdutosRequest
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        
        var result = await handler.GetAllAsync(request);
        return result.IsSucess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
    }
}