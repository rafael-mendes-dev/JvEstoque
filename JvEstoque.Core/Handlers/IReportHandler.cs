using JvEstoque.Core.Models.Reports;
using JvEstoque.Core.Requests.Reports;
using JvEstoque.Core.Responses;

namespace JvEstoque.Core.Handlers;

public interface IReportHandler
{
    Task<Response<FaturamentoReports?>> GetFaturamentoAsync(GetFaturamentoRequest request);
    Task<Response<List<QuantidadeDePedidosPorStatusReport>?>> GetQuantidadeDePedidosPorStatusAsync(GetQuantidadeDePedidosPorStatusRequest request);
    Task<Response<PedidosConcluidosReport?>> GetPedidosConcluidosAsync(GetPedidosConcluidosRequest request);
    Task<Response<ItensEmBaixoEstoqueReport?>> GetItensEmBaixoEstoqueAsync(GetItensEmBaixoEstoqueRequest request);
}