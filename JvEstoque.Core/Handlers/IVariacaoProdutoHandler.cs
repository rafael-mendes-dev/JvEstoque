using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.VariacoesProdutos;
using JvEstoque.Core.Responses;

namespace JvEstoque.Core.Handlers;

public interface IVariacaoProdutoHandler
{
    Task<Response<VariacaoProduto?>> CreateAsync(CreateVariacaoProdutoRequest request);
    Task<Response<VariacaoProduto?>> UpdateAsync(UpdateVariacaoProdutoRequest request);
    Task<Response<VariacaoProduto?>> DeleteAsync(DeleteVariacaoProdutoRequest request);
    Task<Response<VariacaoProduto?>> GetByIdAsync(GetVariacaoProdutoByIdRequest request);
    Task<PagedResponse<List<VariacaoProduto>>> GetAllAsync(GetAllVariacoesProdutosRequest request);
}