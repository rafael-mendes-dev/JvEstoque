using JvEstoque.Api.Data;
using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.Estoques;
using JvEstoque.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace JvEstoque.Api.Handlers;

public class EstoqueHandler(AppDbContext context) : IEstoqueHandler
{
    public async Task<Response<Estoque?>> CreateAsync(CreateEstoqueRequest request)
    {
        try
        {
            var estoque = new Estoque
            {
                VariacaoProdutoId = request.VariacaoProdutoId,
                Quantidade = request.Quantidade
            };
            
            await context.Estoques.AddAsync(estoque);
            await context.SaveChangesAsync();
            
            return new Response<Estoque?>(estoque, 201, "Estoque criado com sucesso.");
        }
        catch 
        {
            return new Response<Estoque?>(null, 500, "Erro ao criar o estoque. Por favor, tente novamente mais tarde.");
        }
    }

    public async Task<Response<Estoque?>> UpdateAsync(UpdateEstoqueRequest request)
    {
        try
        {
            var estoque = await context.Estoques.FirstOrDefaultAsync(e => e.Id == request.Id);

            if (estoque is null)
                return new Response<Estoque?>(null, 404, "Estoque não encontrado.");
            
            estoque.Quantidade = request.Quantidade;
            
            context.Estoques.Update(estoque);
            await context.SaveChangesAsync();
            
            return new Response<Estoque?>(estoque, message: "Estoque atualizado com sucesso.");
        }
        catch 
        {
            return new Response<Estoque?>(null, 500, "Erro ao atualizar o estoque. Por favor, tente novamente mais tarde.");
        }
    }
}