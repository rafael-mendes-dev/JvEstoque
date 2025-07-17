using JvEstoque.Api.Data;
using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models.Reports;
using JvEstoque.Core.Requests.Reports;
using JvEstoque.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace JvEstoque.Api.Handlers;

public class ReportHandler(AppDbContext context) : IReportHandler
{
    public async Task<Response<FaturamentoReports?>> GetFaturamentoAsync(GetFaturamentoRequest request)
    {
        try
        {
            var data = await context.Faturamento.OrderByDescending(x => x.Entradas).AsNoTracking().FirstOrDefaultAsync();
            return new Response<FaturamentoReports?>(data);
        }
        catch
        {
            return new Response<FaturamentoReports?>(null, 500, "Erro ao obter o faturamento. Por favor, tente novamente mais tarde.");
        }
    }

    public async Task<Response<List<QuantidadeDePedidosPorStatusReport>?>> GetQuantidadeDePedidosPorStatusAsync(GetQuantidadeDePedidosPorStatusRequest request)
    {
        try
        {
            var data = await context.QuantidadeDePedidosPorStatus
                .OrderByDescending(x => x.Quantidade)
                .AsNoTracking()
                .ToListAsync();;
            return new Response<List<QuantidadeDePedidosPorStatusReport>?>(data);
        }
        catch
        {
            return new Response<List<QuantidadeDePedidosPorStatusReport>?>(null, 500, "Erro ao obter a quantidade de pedidos por status. Por favor, tente novamente mais tarde.");
        }
    }

    public async Task<Response<PedidosConcluidosReport?>> GetPedidosConcluidosAsync(GetPedidosConcluidosRequest request)
    {
        try
        {
            var data = await context.PedidosConcluidos.OrderByDescending(x => x.Quantidade).AsNoTracking().FirstOrDefaultAsync();
            return new Response<PedidosConcluidosReport?>(data);
        }
        catch
        {
            return new Response<PedidosConcluidosReport?>(null, 500, "Erro ao obter os pedidos concluídos. Por favor, tente novamente mais tarde.");
        }
    }

    public async Task<Response<ItensEmBaixoEstoqueReport?>> GetItensEmBaixoEstoqueAsync(GetItensEmBaixoEstoqueRequest request)
    {
        try
        {
            var data = await context.ItensEmBaixoEstoque.OrderByDescending(x => x.Quantidade).AsNoTracking().FirstOrDefaultAsync();
            return new Response<ItensEmBaixoEstoqueReport?>(data);
        }
        catch
        {
            return new Response<ItensEmBaixoEstoqueReport?>(null, 500, "Erro ao obter os itens em baixo estoque. Por favor, tente novamente mais tarde.");
        }
    }
}