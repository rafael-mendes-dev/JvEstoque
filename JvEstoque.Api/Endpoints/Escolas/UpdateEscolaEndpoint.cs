using JvEstoque.Api.Common.Api;
using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.Escolas;
using JvEstoque.Core.Responses;

namespace JvEstoque.Api.Endpoints.Escolas;

public class UpdateEscolaEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id:int}", HandleAsync)
            .WithName("Escolas : Update")
            .WithSummary("Atualiza uma escola")
            .WithDescription("Atualiza os dados de uma escola existente")
            .WithOrder(5)
            .Produces<Response<Escola?>>();
    
    private static async Task<IResult> HandleAsync(
        IEscolaHandler handler,
        int id,
        UpdateEscolaRequest request)
    {
        request.Id = id;
        var result = await handler.UpdateAsync(request);
        return result.IsSucess ? TypedResults.Ok(result) : TypedResults.BadRequest(result.Data);
    }
}