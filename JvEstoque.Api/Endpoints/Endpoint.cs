using JvEstoque.Api.Common.Api;
using JvEstoque.Api.Endpoints.Escolas;
using JvEstoque.Api.Endpoints.Estoques;
using JvEstoque.Api.Endpoints.Pedidos;
using JvEstoque.Api.Endpoints.Produtos;
using JvEstoque.Api.Endpoints.VariacaoProdutos;

namespace JvEstoque.Api.Endpoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app.MapGroup("");
        
        endpoints.MapGroup("/").WithTags("Health Check").MapGet("/", () => new {message = "OK"});

        endpoints.MapGroup("v1/escolas").WithTags("Escolas")
            .MapEndpoint<CreateEscolaEndpoint>()
            .MapEndpoint<DeleteEscolaEndpoint>()
            .MapEndpoint<GetAllEscolasEndpoint>()
            .MapEndpoint<GetEscolaByIdEndpoint>()
            .MapEndpoint<UpdateEscolaEndpoint>();
        
        endpoints.MapGroup("v1/estoques").WithTags("Estoques")
            .MapEndpoint<CreateEstoqueEndpoint>()
            .MapEndpoint<UpdateEstoqueEndpoint>();
        
        endpoints.MapGroup("v1/pedidos").WithTags("Pedidos")
            .MapEndpoint<CreatePedidoEndpoint>()
            .MapEndpoint<UpdatePedidoEndpoint>()
            .MapEndpoint<GetPedidoByIdEndpoint>()
            .MapEndpoint<GetAllPedidosEndpoint>()
            .MapEndpoint<DeletePedidoEndpoint>();
        
        endpoints.MapGroup("v1/produtos").WithTags("Produtos")
            .MapEndpoint<CreateProdutoEndpoint>()
            .MapEndpoint<UpdateProdutoEndpoint>()
            .MapEndpoint<GetAllProdutosEndpoint>()
            .MapEndpoint<GetProdutoByIdEndpoint>()
            .MapEndpoint<DeleteProdutoEndpoint>();

        endpoints.MapGroup("v1/variacaoProdutos").WithTags("VariacaoProdutos")
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