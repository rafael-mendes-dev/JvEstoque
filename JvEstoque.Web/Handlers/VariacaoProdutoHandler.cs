using System.Net.Http.Json;
using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.VariacoesProdutos;
using JvEstoque.Core.Responses;

namespace JvEstoque.Web.Handlers;

public class VariacaoProdutoHandler(IHttpClientFactory clientFactory) : IVariacaoProdutoHandler
{
    private readonly HttpClient _client = clientFactory.CreateClient(Configuration.HttpClientName);
    public async Task<Response<VariacaoProduto?>> CreateAsync(CreateVariacaoProdutoRequest request)
    {
        var result = await _client.PostAsJsonAsync("v1/variacaoProdutos", request);
        return await result.Content.ReadFromJsonAsync<Response<VariacaoProduto?>>()
               ?? new Response<VariacaoProduto?>(null, 400, "Erro ao criar variação de produto.");
    }

    public async Task<Response<VariacaoProduto?>> UpdateAsync(UpdateVariacaoProdutoRequest request)
    {
        var result = await _client.PutAsJsonAsync($"v1/variacaoProdutos/{request.Id}", request);
        return await result.Content.ReadFromJsonAsync<Response<VariacaoProduto?>>()
               ?? new Response<VariacaoProduto?>(null, 400, "Erro ao atualizar variação de produto.");
    }

    public async Task<Response<VariacaoProduto?>> DeleteAsync(DeleteVariacaoProdutoRequest request)
    {
        var result = await _client.DeleteAsync($"v1/variacaoProdutos/{request.Id}");
        return await result.Content.ReadFromJsonAsync<Response<VariacaoProduto?>>()
               ?? new Response<VariacaoProduto?>(null, 400, "Erro ao excluir variação de produto.");
    }

    public async Task<Response<VariacaoProduto?>> GetByIdAsync(GetVariacaoProdutoByIdRequest request) =>
        await _client.GetFromJsonAsync<Response<VariacaoProduto?>>($"v1/variacaoProdutos/{request.Id}") 
               ?? new Response<VariacaoProduto?>(null, 400, "Erro ao obter variação de produto pelo ID.");

    public async Task<PagedResponse<List<VariacaoProduto>>> GetAllAsync(GetAllVariacoesProdutosRequest request) =>
        await _client.GetFromJsonAsync<PagedResponse<List<VariacaoProduto>>>("v1/variacaoProdutos") 
               ?? new PagedResponse<List<VariacaoProduto>>(null, 400, "Erro ao obter todas as variações de produtos.");

    public async Task<PagedResponse<List<VariacaoProduto>>> GetAllByProdutoIdAsync(GetAllVariacoesProdutosByProdutoIdRequest request) =>
        await _client.GetFromJsonAsync<PagedResponse<List<VariacaoProduto>>>($"v1/variacaoProdutos/produtos/{request.ProdutoId}")
               ?? new PagedResponse<List<VariacaoProduto>>(null, 400, "Erro ao obter variações de produtos pelo ID do produto.");

    public async Task<PagedResponse<List<VariacaoProduto>>> GetAllByEscolaIdAsync(GetAllVariacoesProdutosByEscolaIdRequest request) =>
        await _client.GetFromJsonAsync<PagedResponse<List<VariacaoProduto>>>($"v1/variacaoProdutos/escolas/{request.EscolaId}")
                ?? new PagedResponse<List<VariacaoProduto>>(null, 400, "Erro ao obter variações de produtos pelo ID da escola.");
}