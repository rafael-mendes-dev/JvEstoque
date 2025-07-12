using JvEstoque.Api.Common.Api;
using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models.Reports;
using JvEstoque.Core.Requests.Reports;
using JvEstoque.Core.Responses;

namespace JvEstoque.Api.Endpoints.Reports;

public class GetFaturamentoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapGet("/faturamento", HandleAsync)
        .Produces<Response<FaturamentoReports>>();
    
    private static async Task<IResult> HandleAsync(
        IReportHandler handler)
    {
        var result = await handler.GetFaturamentoAsync(new GetFaturamentoRequest());
        return result.IsSucess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
    }
}