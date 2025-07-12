using JvEstoque.Api.Common.Api;
using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models.Reports;
using JvEstoque.Core.Requests.Reports;
using JvEstoque.Core.Responses;

namespace JvEstoque.Api.Endpoints.Reports;

public class GetPedidosConcluidosEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/concluidos", HandleAsync)
            .Produces<Response<PedidosConcluidosReport>>();
    
    private static async Task<IResult> HandleAsync(
        IReportHandler handler)
    {
        var result = await handler.GetPedidosConcluidosAsync(new GetPedidosConcluidosRequest());
        return result.IsSucess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
    }
}