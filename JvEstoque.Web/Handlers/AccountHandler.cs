using System.Net.Http.Json;
using System.Text;
using JvEstoque.Core.Handlers;
using JvEstoque.Core.Requests.Account;
using JvEstoque.Core.Responses;

namespace JvEstoque.Web.Handlers;

public class AccountHandler(IHttpClientFactory httpClientFactory) : IAccountHandler
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(Configuration.HttpClientName);
    
    public async Task<Response<string>> LoginAsync(LoginRequest request)
    {
        var result = await _httpClient.PostAsJsonAsync("v1/identity/login?useCookies=true", request);
        return result.IsSuccessStatusCode
            ? new Response<string>("Login realizado com sucesso.", 200, "Login realizado com sucesso.")
            : new Response<string>(null, 400, "Erro ao realizar login.");
    }

    public async Task LogoutAsync()
    {
        var emptyContent = new StringContent("{}", Encoding.UTF8, "application/json");
        await _httpClient.PostAsync("/v1/identity/logout", emptyContent);
    }
}