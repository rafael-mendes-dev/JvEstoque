using System.Net.Http.Json;
using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.Pedidos;
using JvEstoque.Core.Responses;

namespace JvEstoque.Web.Handlers;

public class PedidoHandler(IHttpClientFactory clientFactory) : IPedidoHandler
{
    private readonly HttpClient _client = clientFactory.CreateClient(Configuration.HttpClientName);
    public async Task<Response<Pedido?>> CreateAsync(CreatePedidoRequest request)
    {
        var result = await _client.PostAsJsonAsync("v1/pedidos", request);
        return await result.Content.ReadFromJsonAsync<Response<Pedido?>>()
               ?? new Response<Pedido?>(null, 400, "Erro ao criar pedido.");
    }

    public async Task<Response<Pedido?>> UpdateAsync(UpdatePedidoRequest request)
    {
        var result = await _client.PutAsJsonAsync($"v1/pedidos/{request.Id}", request);
        return await result.Content.ReadFromJsonAsync<Response<Pedido?>>()
               ?? new Response<Pedido?>(null, 400, "Erro ao atualizar pedido.");
    }

    public async Task<Response<Pedido?>> DeleteAsync(DeletePedidoRequest request)
    {
        var result = await _client.DeleteAsync($"v1/pedidos/{request.Id}");
        return await result.Content.ReadFromJsonAsync<Response<Pedido?>>()
               ?? new Response<Pedido?>(null, 400, "Erro ao excluir pedido.");
    }

    public async Task<Response<Pedido?>> GetByIdAsync(GetPedidoByIdRequest request) =>
    await _client.GetFromJsonAsync<Response<Pedido?>>($"v1/pedidos/{request.Id}")
           ?? new Response<Pedido?>(null, 400, "Erro ao obter pedido pelo ID.");

    public async Task<PagedResponse<List<Pedido>>> GetAllAsync(GetAllPedidosRequest request) =>
        await _client.GetFromJsonAsync<PagedResponse<List<Pedido>>>("v1/pedidos")
               ?? new PagedResponse<List<Pedido>>(null, 400, "Erro ao obter todos os pedidos.");
}