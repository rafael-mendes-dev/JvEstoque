using JvEstoque.Api.Common.Api;
using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.Escolas;
using JvEstoque.Core.Responses;

namespace JvEstoque.Api.Endpoints.Escolas;

public class GetEscolaByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id:int}", HandleAsync)
            .WithName("Escolas : GetById")
            .WithSummary("Obtém uma escola pelo ID")
            .WithDescription("Obtém uma escola específica pelo ID")
            .WithOrder(4)
            .Produces<Response<Escola?>>();
    
    private static async Task<IResult> HandleAsync(
        IEscolaHandler handler,
        int id)
    {
        var request = new GetEscolaByIdRequest
        {
            Id = id
        };
        
        var result = await handler.GetByIdAsync(request);
        return result.IsSucess ? TypedResults.Ok(result) : TypedResults.BadRequest(result.Data);
    }
}