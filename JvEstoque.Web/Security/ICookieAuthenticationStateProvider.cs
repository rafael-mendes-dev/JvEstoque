using Microsoft.AspNetCore.Components.Authorization;

namespace JvEstoque.Web.Security;

public interface ICookieAuthenticationStateProvider
{
    Task<bool> CheckAuthenticationAsync();
    Task<AuthenticationState> GetAuthenticationStateAsync();
    void NotifyAuthenticationStateChanged();
}