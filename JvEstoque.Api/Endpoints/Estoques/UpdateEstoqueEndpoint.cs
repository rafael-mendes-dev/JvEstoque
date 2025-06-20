using JvEstoque.Api.Common.Api;
using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.Estoques;
using JvEstoque.Core.Responses;

namespace JvEstoque.Api.Endpoints.Estoques;

public class UpdateEstoqueEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id:int}", HandleAsync)
            .WithName("Estoques : Update")
            .WithSummary("Atualiza um estoque")
            .WithDescription("Atualiza as informações de um estoque existente pelo ID")
            .WithOrder(2)
            .Produces<Response<Estoque?>>();
    
    public static async Task<IResult> HandleAsync(
        IEstoqueHandler handler,
        int id,
        UpdateEstoqueRequest request)
    {
        request.Id = id;
        var result = await handler.UpdateAsync(request);
        return result.IsSucess ? TypedResults.Ok(result) : TypedResults.BadRequest(result.Data);
    }
}