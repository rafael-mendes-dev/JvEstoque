using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.Pedidos;
using JvEstoque.Core.Responses;

namespace JvEstoque.Core.Handlers;

public interface IPedidoHandler
{
    Task<Response<Pedido?>> CreateAsync(CreatePedidoRequest request);
    Task<Response<Pedido?>> UpdateAsync(UpdatePedidoRequest request);
    Task<Response<Pedido?>> DeleteAsync(DeletePedidoRequest request);
    Task<Response<Pedido?>> GetByIdAsync(GetPedidoByIdRequest request);
    Task<PagedResponse<List<Pedido>>> GetAllAsync(GetAllPedidosRequest request);
}