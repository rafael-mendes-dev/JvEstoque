using JvEstoque.Api.Common.Api;
using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.VariacoesProdutos;
using JvEstoque.Core.Responses;

namespace JvEstoque.Api.Endpoints.VariacaoProdutos;

public class CreateVariacaoProdutoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("VariacaoProdutos : Create")
            .WithSummary("Cria uma nova variação de produto")
            .WithDescription("Cria uma nova variação de produto")
            .WithOrder(1)
            .Produces<Response<VariacaoProduto?>>();
    
    private static async Task<IResult> HandleAsync(
        IVariacaoProdutoHandler handler,
        CreateVariacaoProdutoRequest request)
    {
        var result = await handler.CreateAsync(request);
        return result.IsSucess ? TypedResults.Created($"/{result.Data?.Id}", result) : TypedResults.BadRequest(result.Data);
    }
}