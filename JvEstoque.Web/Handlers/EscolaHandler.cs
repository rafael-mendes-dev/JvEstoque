using System.Net.Http.Json;
using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.Escolas;
using JvEstoque.Core.Responses;

namespace JvEstoque.Web.Handlers;

public class EscolaHandler(IHttpClientFactory httpClientFactory) : IEscolaHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
    public async Task<Response<Escola?>> CreateAsync(CreateEscolaRequest request)
    {
        var result = await _client.PostAsJsonAsync("v1/escolas", request);
        return await result.Content.ReadFromJsonAsync<Response<Escola?>>() ?? new Response<Escola?>(null, 400, "Erro ao criar escola.");
    }

    public async Task<Response<Escola?>> UpdateAsync(UpdateEscolaRequest request)
    {
        var result = await _client.PutAsJsonAsync($"v1/escolas/{request.Id}", request);
        return await result.Content.ReadFromJsonAsync<Response<Escola?>>() ?? new Response<Escola?>(null, 400, "Erro ao atualizar escola.");
    }

    public async Task<Response<Escola?>> DeleteAsync(DeleteEscolaRequest request)
    {
        var result = await _client.DeleteAsync($"v1/escolas/{request.Id}");
        return await result.Content.ReadFromJsonAsync<Response<Escola?>>() ?? new Response<Escola?>(null, 400, "Erro ao excluir escola.");
    }

    public async Task<Response<Escola?>> GetByIdAsync(GetEscolaByIdRequest request) =>
        await _client.GetFromJsonAsync<Response<Escola?>>($"v1/escolas/{request.Id}") ?? new Response<Escola?>(null, 400, "Erro ao obter escola pelo ID.");
    

    public async Task<PagedResponse<List<Escola>>> GetAllAsync(GetAllEscolasRequest request) =>
        await _client.GetFromJsonAsync<PagedResponse<List<Escola>>>("v1/escolas") ?? new PagedResponse<List<Escola>>(null, 400, "Erro ao obter todas as escolas.");
}