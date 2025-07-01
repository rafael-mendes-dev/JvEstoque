using JvEstoque.Api.Data;
using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.Produtos;
using JvEstoque.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace JvEstoque.Api.Handlers;

public class ProdutoHandler(AppDbContext context) : IProdutoHandler
{
    public async Task<Response<Produto?>> CreateAsync(CreateProdutoRequest request)
    {
        try
        {
            var produto = new Produto
            {
                Nome = request.Nome,
                Descricao = request.Descricao,
                Preco = request.Preco,
                Peca = request.Peca
            };

            await context.Produtos.AddAsync(produto);
            await context.SaveChangesAsync();
            return new Response<Produto?>(produto, message: "Produto cadastrado com sucesso");
        }
        catch 
        {
            return new Response<Produto?>(null, 500, "Erro ao cadastrar produto, por favor, tente novamente mais tarde.");
        }
    }

    public async Task<Response<Produto?>> UpdateAsync(UpdateProdutoRequest request)
    {
        try
        {
            var produto = await context.Produtos.FindAsync(request.Id);
            if (produto == null)
                return new Response<Produto?>(null, 404, "Produto não encontrado.");

            produto.Descricao = request.Descricao;
            produto.Nome = request.Nome;
            produto.Preco = request.Preco;
            produto.Peca = request.Peca;

            context.Produtos.Update(produto);
            await context.SaveChangesAsync();

            return new Response<Produto?>(produto, message: "Produto atualizado com sucesso.");
        }
        catch 
        {
            return new Response<Produto?>(null, 500, "Erro ao atualizar produto, por favor, tente novamente mais tarde.");
        }
    }

    public async Task<Response<Produto?>> DeleteAsync(DeleteProdutoRequest request)
    {
        try
        {
            var produto = await context.Produtos.FindAsync(request.Id);
            if (produto == null)
                return new Response<Produto?>(null, 404, "Produto não encontrado.");
            
            context.Produtos.Remove(produto);
            await context.SaveChangesAsync();
            
            return new Response<Produto?>(produto, message: "Produto excluído com sucesso.");
        }
        catch 
        {
            return new Response<Produto?>(null, 500, "Erro ao excluir produto, por favor, tente novamente mais tarde.");
        }
    }

    public async Task<Response<Produto?>> GetByIdAsync(GetProdutoByIdRequest request)
    {
        try
        {
            var produto = await context.Produtos.FindAsync(request.Id);
            
            return produto is null
                ? new Response<Produto?>(null, 404, "Produto não encontrado.")
                : new Response<Produto?>(produto, message: "Produto encontrado com sucesso.");
        }
        catch 
        {
            return new Response<Produto?>(null, 500, "Erro ao buscar produto, por favor, tente novamente mais tarde.");
        }
    }

    public async Task<PagedResponse<List<Produto>>> GetAllAsync(GetAllProdutosRequest request)
    {
        try
        {
            var query = context.Produtos.AsNoTracking().OrderBy(e => e.Id);
                
            var produtos = await query.Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
                
            var totalCount = await query.CountAsync();
                
            return new PagedResponse<List<Produto>>(produtos, totalCount, request.PageNumber, request.PageSize);
        }
        catch 
        {
            return new PagedResponse<List<Produto>>(null, 500, "Erro ao buscar produtos, por favor, tente novamente mais tarde.");
        }
    }
}