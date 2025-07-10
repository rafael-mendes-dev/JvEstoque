using JvEstoque.Api.Data;
using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.Pedidos;
using JvEstoque.Core.Responses;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

namespace JvEstoque.Api.Handlers;
[EnableRateLimiting("fixed")]
public class PedidoHandler(AppDbContext context) : IPedidoHandler
{
    public async Task<Response<Pedido?>> CreateAsync(CreatePedidoRequest request)
    {
        try
        {
            var variacaoProdutos = request.Itens.Select(i => i.VariacaoProdutoId).ToList();
            
            var precosVariacoes = await context.VariacoesProdutos
                .AsNoTracking()
                .Include(v => v.Produto)
                .Where(v => variacaoProdutos.Contains(v.Id))
                .ToDictionaryAsync(v => v.Id, v => v.Produto.Preco);
            
            var itensPedido = request.Itens.Select(i => new ItemPedido
            {
                VariacaoProdutoId = i.VariacaoProdutoId,
                Quantidade = i.Quantidade,
                ValorUnitario = precosVariacoes.GetValueOrDefault(i.VariacaoProdutoId)
            }).ToList();
            
            var pedido = new Pedido
            {
                DataPedido = request.DataPedido ?? DateTime.Now,
                NomeCliente = request.NomeCliente,
                TelefoneCliente = request.TelefoneCliente,
                Status = request.Status,
                Itens = itensPedido,
                ValorTotal = itensPedido.Sum(i => i.SubTotal),
            };
            
            await context.Pedidos.AddAsync(pedido);
            await context.SaveChangesAsync();
            
            return new Response<Pedido?>(pedido, 201, "Pedido criado com sucesso.");
        }
        catch
        {
            return new Response<Pedido?>(null, 500, "Erro ao criar o pedido. Por favor, tente novamente mais tarde.");
        }
    }

    public async Task<Response<Pedido?>> UpdateAsync(UpdatePedidoRequest request)
    {
        try
        {
            var pedido = await context.Pedidos.Include(p => p.Itens)
                .FirstOrDefaultAsync(p => p.Id == request.Id);
            
            if (pedido == null)
                return new Response<Pedido?>(null, 404, "Pedido não encontrado.");
            
            var variacaoIds = request.Itens.Select(i => i.VariacaoProdutoId).ToList();
            var precosVariacoes = await context.VariacoesProdutos
                .AsNoTracking()
                .Include(v => v.Produto)
                .Where(v => variacaoIds.Contains(v.Id))
                .ToDictionaryAsync(v => v.Id, v => v.Produto.Preco);

            context.ItensPedidos.RemoveRange(pedido.Itens);
            
            var itens = request.Itens.Select(i => new ItemPedido
            {
                VariacaoProdutoId = i.VariacaoProdutoId,
                Quantidade = i.Quantidade,
                ValorUnitario = precosVariacoes.GetValueOrDefault(i.VariacaoProdutoId)
            }).ToList();
            
            pedido.DataPedido = request.DataPedido ?? pedido.DataPedido;
            pedido.NomeCliente = request.NomeCliente;
            pedido.TelefoneCliente = request.TelefoneCliente;
            pedido.Status = request.Status;
            pedido.Itens = itens;
            pedido.ValorTotal = itens.Sum(i => i.SubTotal);
            
            context.Pedidos.Update(pedido);
            await context.SaveChangesAsync();
            
            return new Response<Pedido?>(pedido, message: "Pedido atualizado com sucesso.");
        }
        catch 
        {
            return new Response<Pedido?>(null, 500, "Erro ao atualizar o pedido. Por favor, tente novamente mais tarde.");
        }
    }

    public async Task<Response<Pedido?>> DeleteAsync(DeletePedidoRequest request)
    {
        try
        {
            var pedido = await context.Pedidos.FindAsync(request.Id);
            if (pedido == null)
                return new Response<Pedido?>(null, 404, "Pedido não encontrado.");

            context.Pedidos.Remove(pedido);
            await context.SaveChangesAsync();
            return new Response<Pedido?>(pedido, message: "Pedido excluído com sucesso.");
        }
        catch 
        {
            return new Response<Pedido?>(null, 500, "Erro ao excluir o pedido. Por favor, tente novamente mais tarde.");
        }
    }

    public async Task<Response<Pedido?>> GetByIdAsync(GetPedidoByIdRequest request)
    {
        try
        {
            var pedido = await context.Pedidos
                .Include(p => p.Itens)
                    .ThenInclude(i => i.VariacaoProduto)
                        .ThenInclude(vp => vp.Produto)
                .Include(p => p.Itens)
                    .ThenInclude(i => i.VariacaoProduto)
                        .ThenInclude(vp => vp.Escola)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == request.Id);

            return pedido is null
                ? new Response<Pedido?>(null, 404, "Pedido não encontrado.")
                : new Response<Pedido?>(pedido, message: "Pedido encontrado com sucesso.");
        }
        catch
        {
            return new Response<Pedido?>(null, 500, "Erro ao buscar o pedido. Por favor, tente novamente mais tarde.");
        }
    }

    public async Task<PagedResponse<List<Pedido>>> GetAllAsync(GetAllPedidosRequest request)
    {
        try
        {
            var query = context.Pedidos.AsNoTracking().OrderBy(e => e.Id);
                
            var pedidos = await query.Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
                
            var totalCount = await query.CountAsync();
                
            return new PagedResponse<List<Pedido>>(pedidos, totalCount, request.PageNumber, request.PageSize);
        }
        catch 
        {
            return new PagedResponse<List<Pedido>>(null, 500, "Erro ao buscar as escolas. Por favor, tente novamente mais tarde.");
        }
    }
}