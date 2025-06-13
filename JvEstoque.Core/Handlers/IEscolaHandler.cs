using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.Escolas;
using JvEstoque.Core.Responses;

namespace JvEstoque.Core.Handlers;

public interface IEscolaHandler
{
    Task<Response<Escola?>> CreateAsync(CreateEscolaRequest request);
    Task<Response<Escola?>> UpdateAsync(UpdateEscolaRequest request);
    Task<Response<Escola?>> DeleteAsync(DeleteEscolaRequest request);
    Task<Response<Escola?>> GetByIdAsync(GetEscolaByIdRequest request);
    Task<PagedResponse<List<Escola>>> GetAllAsync(GetAllEscolasRequest request);
}