using JvEstoque.Api.Common.Api;
using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.Produtos;
using JvEstoque.Core.Responses;

namespace JvEstoque.Api.Endpoints.Produtos;

public class DeleteProdutoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapDelete("/{id:int}", HandleAsync)
        .WithName("Produtos : Delete")
        .WithSummary("Deleta um produto")
        .WithDescription("Deleta um produto pelo ID")
        .WithOrder(3)
        .Produces<Response<Produto?>>();
    
    public static async Task<IResult> HandleAsync(
        IProdutoHandler handler,
        int id)
    {
        var request = new DeleteProdutoRequest()
        {
            Id = id
        };
        
        var result = await handler.DeleteAsync(request);
        return result.IsSucess ? TypedResults.Ok(result) : TypedResults.BadRequest(result.Data);
    }
}