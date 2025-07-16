using System.Net.Http.Json;
using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models.Reports;
using JvEstoque.Core.Requests.Reports;
using JvEstoque.Core.Responses;

namespace JvEstoque.Web.Handlers;

public class ReportHandler(IHttpClientFactory httpClientFactory) : IReportHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
    public async Task<Response<FaturamentoReports?>> GetFaturamentoAsync(GetFaturamentoRequest request) 
        => await _client.GetFromJsonAsync<Response<FaturamentoReports?>>($"v1/reports/faturamento") 
           ?? new Response<FaturamentoReports?>(null, 400, "Erro ao obter o faturamento. Por favor, tente novamente mais tarde.");
    

    public async Task<Response<List<QuantidadeDePedidosPorStatusReport>?>> GetQuantidadeDePedidosPorStatusAsync(GetQuantidadeDePedidosPorStatusRequest request)
        => await _client.GetFromJsonAsync<Response<List<QuantidadeDePedidosPorStatusReport>?>>($"v1/reports/pedidosPorStatus") 
           ?? new Response<List<QuantidadeDePedidosPorStatusReport>?>(null, 400, "Erro ao obter o faturamento. Por favor, tente novamente mais tarde.");


    public async Task<Response<PedidosConcluidosReport?>> GetPedidosConcluidosAsync(GetPedidosConcluidosRequest request)
        => await _client.GetFromJsonAsync<Response<PedidosConcluidosReport?>>($"v1/reports/concluidos") 
           ?? new Response<PedidosConcluidosReport?>(null, 400, "Erro ao obter o faturamento. Por favor, tente novamente mais tarde.");


    public async Task<Response<ItensEmBaixoEstoqueReport?>> GetItensEmBaixoEstoqueAsync(GetItensEmBaixoEstoqueRequest request)
        => await _client.GetFromJsonAsync<Response<ItensEmBaixoEstoqueReport?>>($"v1/reports/baixoEstoque") 
           ?? new Response<ItensEmBaixoEstoqueReport?>(null, 400, "Erro ao obter o faturamento. Por favor, tente novamente mais tarde.");

}