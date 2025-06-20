using JvEstoque.Api.Common.Api;
using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.Estoques;
using JvEstoque.Core.Responses;

namespace JvEstoque.Api.Endpoints.Estoques;

public class CreateEstoqueEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("Estoques : Create")
            .WithSummary("Cria um estoque")
            .WithDescription("Cria um novo estoque com as informações fornecidas")
            .WithOrder(1)
            .Produces<Response<Estoque?>>();
    
    public static async Task<IResult> HandleAsync(
        IEstoqueHandler handler,
        CreateEstoqueRequest request)
    {
        var result = await handler.CreateAsync(request);
        return result.IsSucess ? TypedResults.Created("/{result.Data?.Id}", result) : TypedResults.BadRequest(result.Data);
    }
}