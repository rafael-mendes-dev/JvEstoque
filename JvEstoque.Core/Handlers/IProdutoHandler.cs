using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.Produtos;
using JvEstoque.Core.Responses;

namespace JvEstoque.Core.Handlers;

public interface IProdutoHandler
{
    Task<Response<Produto?>> CreateAsync(CreateProdutoRequest request);
    Task<Response<Produto?>> UpdateAsync(UpdateProdutoRequest request);
    Task<Response<Produto?>> DeleteAsync(DeleteProdutoRequest request);
    Task<Response<Produto?>> GetByIdAsync(GetProdutoByIdRequest request);
    Task<PagedResponse<List<Produto>>> GetAllAsync(GetAllProdutosRequest request);
}