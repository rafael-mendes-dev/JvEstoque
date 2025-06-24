using System.Net.Http.Json;
using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.Estoques;
using JvEstoque.Core.Responses;

namespace JvEstoque.Web.Handlers;

public class EstoqueHandler(IHttpClientFactory clientFactory) : IEstoqueHandler
{
    private readonly HttpClient _client = clientFactory.CreateClient(Configuration.HttpClientName);
    public async Task<Response<Estoque?>> CreateAsync(CreateEstoqueRequest request)
    {
        var result = await _client.PostAsJsonAsync("v1/estoques", request);
        return await result.Content.ReadFromJsonAsync<Response<Estoque?>>() ?? new Response<Estoque?>(null, 400, "Erro ao criar estoque.");
    }

    public async Task<Response<Estoque?>> UpdateAsync(UpdateEstoqueRequest request)
    {
        var result = await _client.PutAsJsonAsync($"v1/estoques/{request.Id}", request);
        return await result.Content.ReadFromJsonAsync<Response<Estoque?>>() ?? new Response<Estoque?>(null, 400, "Erro ao atualizar estoque.");
    }
}