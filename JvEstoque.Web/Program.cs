using System.Globalization;
using JvEstoque.Core.Handlers;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using JvEstoque.Web;
using JvEstoque.Web.Handlers;
using JvEstoque.Web.Security;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

Configuration.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<CookieHandler>();

builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthenticationStateProvider, CookieAuthenticationStateProvider>();
builder.Services.AddScoped(x => (ICookieAuthenticationStateProvider)x.GetRequiredService<AuthenticationStateProvider>());

builder.Services.AddMudServices();

builder.Services.AddHttpClient(Configuration.HttpClientName, client =>
{
    client.BaseAddress = new Uri(Configuration.BackendUrl);
}).AddHttpMessageHandler<CookieHandler>();

builder.Services.AddTransient<IAccountHandler, AccountHandler>();
builder.Services.AddTransient<IEscolaHandler, EscolaHandler>();
builder.Services.AddTransient<IProdutoHandler, ProdutoHandler>();
builder.Services.AddTransient<IPedidoHandler, PedidoHandler>();
builder.Services.AddTransient<IVariacaoProdutoHandler, VariacaoProdutoHandler>();

builder.Services.AddLocalization();

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-br");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("pt-br");

await builder.Build().RunAsync();

