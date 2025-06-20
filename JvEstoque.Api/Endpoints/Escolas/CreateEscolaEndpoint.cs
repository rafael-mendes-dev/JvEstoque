using Azure;
using JvEstoque.Api.Common.Api;
using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.Escolas;

namespace JvEstoque.Api.Endpoints.Escolas;

public class CreateEscolaEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("Escolas : Create")
            .WithSummary("Cria uma nova escola")
            .WithDescription("Cria uma nova escola")
            .WithOrder(1)
            .Produces<Response<Escola?>>();

    private static async Task<IResult> HandleAsync(
        IEscolaHandler handler,
        CreateEscolaRequest request)
    {
        var result = await handler.CreateAsync(request);
        return result.IsSucess ? TypedResults.Created($"/{result.Data?.Id}", result) : TypedResults.BadRequest(result.Data);
    }
    
}