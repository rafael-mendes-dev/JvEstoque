using JvEstoque.Api.Common.Api;
using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.Escolas;
using JvEstoque.Core.Responses;

namespace JvEstoque.Api.Endpoints.Escolas;

public class DeleteEscolaEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id:int}", HandleAsync)
            .WithName("Escolas : Delete")
            .WithSummary("Deleta uma escola")
            .WithDescription("Deleta uma escola pelo ID")
            .WithOrder(3)
            .Produces<Response<Escola?>>();
    
    public static async Task<IResult> HandleAsync(
        IEscolaHandler handler,
        int id)
    {
        var request = new DeleteEscolaRequest()
        {
            Id = id
        };
        
        var result = await handler.DeleteAsync(request);
        return result.IsSucess ? TypedResults.Ok(result) : TypedResults.BadRequest(result.Data);
    }
}