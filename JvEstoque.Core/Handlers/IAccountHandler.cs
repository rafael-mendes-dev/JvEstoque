using JvEstoque.Core.Requests.Account;
using JvEstoque.Core.Responses;

namespace JvEstoque.Core.Handlers;

public interface IAccountHandler
{
    Task<Response<string>> LoginAsync(LoginRequest request);
    Task LogoutAsync();
}