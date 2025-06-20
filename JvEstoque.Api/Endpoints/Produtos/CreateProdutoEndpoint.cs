using JvEstoque.Api.Common.Api;
using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.Produtos;
using JvEstoque.Core.Responses;

namespace JvEstoque.Api.Endpoints.Produtos;

public class CreateProdutoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("Produtos : Create")
            .WithSummary("Cria um novo produto")
            .WithDescription("Cria um novo produto no estoque")
            .WithOrder(1)
            .Produces<Response<Produto?>>();
    
    private static async Task<IResult> HandleAsync(
        IProdutoHandler handler,
        CreateProdutoRequest request)
    {
        var result = await handler.CreateAsync(request);
        return result.IsSucess ? TypedResults.Created($"/{result.Data?.Id}", result) : TypedResults.BadRequest(result.Data);
    }
}