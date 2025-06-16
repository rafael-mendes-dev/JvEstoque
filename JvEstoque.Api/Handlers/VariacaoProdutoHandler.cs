using JvEstoque.Api.Data;
using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.VariacoesProdutos;
using JvEstoque.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace JvEstoque.Api.Handlers;

public class VariacaoProdutoHandler(AppDbContext context) : IVariacaoProdutoHandler
{
    public async Task<Response<VariacaoProduto?>> CreateAsync(CreateVariacaoProdutoRequest request)
    {
        try
        {
            var variacaoProduto = new VariacaoProduto
            {
                Sku = request.Sku,
                ProdutoId = request.ProdutoId,
                EscolaId = request.EscolaId,
                Tamanho = request.Tamanho,
                Cor = request.Cor,
                Tecido = request.Tecido,
                Genero = request.Genero,
            };
            
            await context.VariacoesProdutos.AddAsync(variacaoProduto);
            await context.SaveChangesAsync();
            
            return new Response<VariacaoProduto?>(variacaoProduto, message: "Variação de produto criada com sucesso.");
        }
        catch
        {
            return new Response<VariacaoProduto?>(null, 500, "Erro ao criar a variação de produto. Por favor, tente novamente mais tarde.");
        }
    }

    public async Task<Response<VariacaoProduto?>> UpdateAsync(UpdateVariacaoProdutoRequest request)
    {
        try
        {
            var variacaoProduto = await context.VariacoesProdutos.FindAsync(request.Id);
            
            if (variacaoProduto == null)
                return new Response<VariacaoProduto?>(null, 404, "Variação de produto não encontrada.");
            
            variacaoProduto.Sku = request.Sku;
            variacaoProduto.ProdutoId = request.ProdutoId;
            variacaoProduto.EscolaId = request.EscolaId;
            variacaoProduto.Tamanho = request.Tamanho;
            variacaoProduto.Cor = request.Cor;
            variacaoProduto.Tecido = request.Tecido;
            variacaoProduto.Genero = request.Genero;

            context.VariacoesProdutos.Update(variacaoProduto);
            await context.SaveChangesAsync();

            return new Response<VariacaoProduto?>(variacaoProduto, message: "Variação de produto atualizada com sucesso.");
        }
        catch
        {
            return new Response<VariacaoProduto?>(null, 500, "Erro ao atualizar a variação de produto. Por favor, tente novamente mais tarde.");
        }
    }

    public async Task<Response<VariacaoProduto?>> DeleteAsync(DeleteVariacaoProdutoRequest request)
    {
        try
        {
            var variacaoProduto = await context.VariacoesProdutos.FindAsync(request.Id);
            
            if (variacaoProduto == null)
                return new Response<VariacaoProduto?>(null, 404, "Variação de produto não encontrada.");
            
            context.VariacoesProdutos.Remove(variacaoProduto);
            await context.SaveChangesAsync();
            
            return new Response<VariacaoProduto?>(variacaoProduto, message: "Variação de produto excluída com sucesso.");
        }
        catch 
        {
            return new Response<VariacaoProduto?>(null, 500, "Erro ao excluir a variação de produto. Por favor, tente novamente mais tarde.");
        }
    }

    public async Task<Response<VariacaoProduto?>> GetByIdAsync(GetVariacaoProdutoByIdRequest request)
    {
        try
        {
            var variacaoProduto = await context.VariacoesProdutos.FindAsync(request.Id);
            
            return variacaoProduto is null
                ? new Response<VariacaoProduto?>(null, 404, "Variação de produto não encontrada.")
                : new Response<VariacaoProduto?>(variacaoProduto, message: "Variação de produto encontrada.");
        }
        catch
        {
            return new Response<VariacaoProduto?>(null, 500, "Erro ao buscar a variação de produto. Por favor, tente novamente mais tarde.");
        }
    }

    public async Task<PagedResponse<List<VariacaoProduto>>> GetAllAsync(GetAllVariacoesProdutosRequest request)
    {
        try
        {
            var query = context.VariacoesProdutos.AsNoTracking().OrderBy(e => e.Id);
                
            var variacaoProdutos = await query.Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
                
            var totalCount = await query.CountAsync();
                
            return new PagedResponse<List<VariacaoProduto>>(variacaoProdutos, totalCount, request.PageNumber, request.PageSize);
        }
        catch 
        {
            return new PagedResponse<List<VariacaoProduto>>(null, 500, "Erro ao buscar variações de produto. Por favor, tente novamente mais tarde.");
        }
    }

    public async Task<PagedResponse<List<VariacaoProduto>>> GetAllByProdutoIdAsync(GetAllVariacoesProdutosByProdutoIdRequest request)
    {
        try
        {
            var query = context.VariacoesProdutos
                .Where(v => v.ProdutoId == request.ProdutoId)
                .AsNoTracking();
            
            var totalCount = await query.CountAsync();
            
            var variacaoProdutos = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            
            return new PagedResponse<List<VariacaoProduto>>(variacaoProdutos, totalCount, request.PageNumber, request.PageSize);
        }
        catch
        {
            return new PagedResponse<List<VariacaoProduto>>(null, 500, "Erro ao buscar variações de produto por ID do produto. Por favor, tente novamente mais tarde.");
        }
    }

    public async Task<PagedResponse<List<VariacaoProduto>>> GetAllByEscolaIdAsync(GetAllVariacoesProdutosByEscolaIdRequest request)
    {
        try
        {
            var query = context.VariacoesProdutos
                .Where(v => v.EscolaId == request.EscolaId)
                .AsNoTracking();
            
            var totalCount = await query.CountAsync();
            
            var variacaoProdutos = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            
            return new PagedResponse<List<VariacaoProduto>>(variacaoProdutos, totalCount, request.PageNumber, request.PageSize);
        }
        catch
        {
            return new PagedResponse<List<VariacaoProduto>>(null, 500, "Erro ao buscar variações de produto por ID do produto. Por favor, tente novamente mais tarde.");
        }
    }
}