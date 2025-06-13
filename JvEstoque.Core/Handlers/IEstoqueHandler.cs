using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.Estoques;
using JvEstoque.Core.Responses;

namespace JvEstoque.Core.Handlers;

public interface IEstoqueHandler
{
    Task<Response<Estoque?>> CreateAsync(CreateEstoqueRequest request);
    Task<Response<Estoque?>> UpdateAsync(UpdateEstoqueRequest request);
}