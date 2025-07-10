using JvEstoque.Api.Data;
using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.Escolas;
using JvEstoque.Core.Responses;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

namespace JvEstoque.Api.Handlers
{
    [EnableRateLimiting("fixed")]
    public class EscolaHandler (AppDbContext context) : IEscolaHandler
    {
        public async Task<Response<Escola?>> CreateAsync(CreateEscolaRequest request)
        {
            try
            {
                var escola = new Escola
                {
                    Nome = request.Nome,
                    Endereco = request.Endereco,
                    Telefone = request.Telefone
                };

                await context.Escolas.AddAsync(escola);
                await context.SaveChangesAsync();

                return new Response<Escola?>(escola, 201, "Escola criada com sucesso.");
            }
            catch 
            {
                return new Response<Escola?>(null,500, "Erro ao criar a escola. Por favor, tente novamente mais tarde.");
            }
        }

        public async Task<Response<Escola?>> UpdateAsync(UpdateEscolaRequest request){
            try
            {
                var escola = await context.Escolas.FirstOrDefaultAsync(e => e.Id == request.Id);

                if (escola == null)
                    return new Response<Escola?>(null, 404, "Escola não encontrada.");
                
                escola.Nome = request.Nome;
                escola.Endereco = request.Endereco;
                escola.Telefone = request.Telefone;

                context.Escolas.Update(escola);
                await context.SaveChangesAsync();

                return new Response<Escola?>(escola, message: "Escola atualizada com sucesso.");
            }
            catch 
            {
                
                return new Response<Escola?>(null, 500, "Erro ao atualizar a escola. Por favor, tente novamente mais tarde.");
            }
        }
    
        public async Task<Response<Escola?>> DeleteAsync(DeleteEscolaRequest request)
        {
            try
            {
                var escola = await context.Escolas.Include(e => e.VariacoesProdutos).FirstOrDefaultAsync(e => e.Id == request.Id);

                if (escola == null)
                    return new Response<Escola?>(null, 404, "Escola não encontrada.");

                if (escola.VariacoesProdutos != null && escola.VariacoesProdutos.Any())
                    return new Response<Escola?>(null, 400, "Não é possível excluir a escola porque ela possui variações de produtos associadas.");

                context.Escolas.Remove(escola);
                await context.SaveChangesAsync();

                return new Response<Escola?>(escola, message: "Escola excluída com sucesso.");
            }
            catch 
            {
                return new Response<Escola?>(null, 500, "Erro ao excluir a escola. Por favor, tente novamente mais tarde.");
            }
        }
   
        public async Task<Response<Escola?>> GetByIdAsync(GetEscolaByIdRequest request)
        {
            try
            {
                var escola = await context.Escolas.AsNoTracking().FirstOrDefaultAsync(e => e.Id == request.Id);

                return escola is null 
                    ? new Response<Escola?>(null, 404, "Escola não encontrada.")
                    : new Response<Escola?>(escola, message: "Escola encontrada com sucesso.");
            }
            catch 
            {
                return new Response<Escola?>(null, 500, "Erro ao buscar a escola. Por favor, tente novamente mais tarde.");
            }
        }

        public async Task<PagedResponse<List<Escola>>> GetAllAsync(GetAllEscolasRequest request)
        {
            try
            {
                var query = context.Escolas.AsNoTracking().OrderBy(e => e.Nome);
                
                var escolas = await query.Skip((request.PageNumber - 1) * request.PageSize)
                                          .Take(request.PageSize)
                                          .ToListAsync();
                
                var totalCount = await query.CountAsync();
                
                return new PagedResponse<List<Escola>>(escolas, totalCount, request.PageNumber, request.PageSize);
            }
            catch 
            {
                return new PagedResponse<List<Escola>>(null, 500, "Erro ao buscar as escolas. Por favor, tente novamente mais tarde.");
            }
        }
    }
}