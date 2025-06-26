using JvEstoque.Api.Common.Api;
using JvEstoque.Api.Endpoints.Escolas;
using JvEstoque.Api.Endpoints.Identity;
using JvEstoque.Api.Endpoints.Pedidos;
using JvEstoque.Api.Endpoints.Produtos;
using JvEstoque.Api.Endpoints.VariacaoProdutos;
using JvEstoque.Api.Models;

namespace JvEstoque.Api.Endpoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app.MapGroup("");
        
        endpoints.MapGroup("/").WithTags("Health Check").MapGet("/", () => new {message = "OK"});

        endpoints.MapGroup("v1/identity").WithTags("Identity")
            .MapIdentityApi<User>();

        endpoints.MapGroup("v1/identity").WithTags("Identity")
            .MapEndpoint<LogoutEndpoint>()
            .MapEndpoint<GetRolesEndpoint>();

        
        endpoints.MapGroup("v1/escolas").WithTags("Escolas")
            .RequireAuthorization()
            .MapEndpoint<CreateEscolaEndpoint>()
            .MapEndpoint<DeleteEscolaEndpoint>()
            .MapEndpoint<GetAllEscolasEndpoint>()
            .MapEndpoint<GetEscolaByIdEndpoint>()
            .MapEndpoint<UpdateEscolaEndpoint>();
        
        endpoints.MapGroup("v1/pedidos").WithTags("Pedidos")
            .RequireAuthorization()
            .MapEndpoint<CreatePedidoEndpoint>()
            .MapEndpoint<UpdatePedidoEndpoint>()
            .MapEndpoint<GetPedidoByIdEndpoint>()
            .MapEndpoint<GetAllPedidosEndpoint>()
            .MapEndpoint<DeletePedidoEndpoint>();
        
        endpoints.MapGroup("v1/produtos").WithTags("Produtos")
            .RequireAuthorization()
            .MapEndpoint<CreateProdutoEndpoint>()
            .MapEndpoint<UpdateProdutoEndpoint>()
            .MapEndpoint<GetAllProdutosEndpoint>()
            .MapEndpoint<GetProdutoByIdEndpoint>()
            .MapEndpoint<DeleteProdutoEndpoint>();

        endpoints.MapGroup("v1/variacaoProdutos").WithTags("VariacaoProdutos")
            .RequireAuthorization()
            .MapEndpoint<CreateVariacaoProdutoEndpoint>()
            .MapEndpoint<DeleteVariacaoProdutoEndpoint>()
            .MapEndpoint<GetAllVariacoesProdutosEndpoint>()
            .MapEndpoint<GetAllVariacoesProdutosByEscolaIdEndpoint>()
            .MapEndpoint<GetAllVariacoesProdutosByProdutoIdEndpoint>()
            .MapEndpoint<GetVariacaoProdutoByIdEndpoint>()
            .MapEndpoint<UpdateVariacaoProdutoEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
        where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;
    }
}