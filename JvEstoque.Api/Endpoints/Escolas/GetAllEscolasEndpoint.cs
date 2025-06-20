using JvEstoque.Api.Common.Api;
using JvEstoque.Core;
using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.Escolas;
using JvEstoque.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace JvEstoque.Api.Endpoints.Escolas;

public class GetAllEscolasEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapGet("/", HandleAsync)
        .WithName("Escolas : GetAll")
        .WithSummary("Obtém todas as escolas")
        .WithDescription("Obtém uma lista de todas as escolas cadastradas")
        .WithOrder(2)
        .Produces<Response<List<Escola?>>>();

    private static async Task<IResult> HandleAsync(
        IEscolaHandler handler,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetAllEscolasRequest
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        var result = await handler.GetAllAsync(request);
        return result.IsSucess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
    }
}