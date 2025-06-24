using System.Net.Http.Json;
using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.Produtos;
using JvEstoque.Core.Responses;

namespace JvEstoque.Web.Handlers;

public class ProdutoHandler(IHttpClientFactory clientFactory) : IProdutoHandler
{
    private readonly HttpClient _client = clientFactory.CreateClient(Configuration.HttpClientName);
    public async Task<Response<Produto?>> CreateAsync(CreateProdutoRequest request)
    {
        var result = await _client.PostAsJsonAsync("v1/produtos", request);
        return await result.Content.ReadFromJsonAsync<Response<Produto?>>()
               ?? new Response<Produto?>(null, 400, "Erro ao criar produto.");
    }

    public async Task<Response<Produto?>> UpdateAsync(UpdateProdutoRequest request)
    {
        var result = await _client.PutAsJsonAsync($"v1/produtos/{request.Id}", request);
        return await result.Content.ReadFromJsonAsync<Response<Produto?>>()
               ?? new Response<Produto?>(null, 400, "Erro ao atualizar produto.");
    }

    public async Task<Response<Produto?>> DeleteAsync(DeleteProdutoRequest request)
    {
        var result = await _client.DeleteAsync($"v1/produtos/{request.Id}");
        return await result.Content.ReadFromJsonAsync<Response<Produto?>>()
               ?? new Response<Produto?>(null, 400, "Erro ao excluir produto.");
    }

    public async Task<Response<Produto?>> GetByIdAsync(GetProdutoByIdRequest request) =>
        await _client.GetFromJsonAsync<Response<Produto?>>($"v1/produtos/{request.Id}")
               ?? new Response<Produto?>(null, 400, "Erro ao obter produto pelo ID.");
    

    public async Task<PagedResponse<List<Produto>>> GetAllAsync(GetAllProdutosRequest request) =>
        await _client.GetFromJsonAsync<PagedResponse<List<Produto>>>("v1/produtos")
               ?? new PagedResponse<List<Produto>>(null, 400, "Erro ao obter todos os produtos.");
}