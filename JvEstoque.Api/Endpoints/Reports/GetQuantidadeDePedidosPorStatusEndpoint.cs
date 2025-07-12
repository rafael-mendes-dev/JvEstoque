using JvEstoque.Api.Common.Api;
using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models.Reports;
using JvEstoque.Core.Requests.Reports;
using JvEstoque.Core.Responses;

namespace JvEstoque.Api.Endpoints.Reports;

public class GetQuantidadeDePedidosPorStatusEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/pedidosPorStatus", HandleAsync)
            .Produces<Response<QuantidadeDePedidosPorStatusReport>>();
    
    private static async Task<IResult> HandleAsync(
        IReportHandler handler)
    {
        var result = await handler.GetQuantidadeDePedidosPorStatusAsync(new GetQuantidadeDePedidosPorStatusRequest());
        return result.IsSucess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
    }
}