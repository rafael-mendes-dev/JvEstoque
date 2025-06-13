using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.ItensPedidos;
using JvEstoque.Core.Responses;

namespace JvEstoque.Core.Handlers;

public interface IItemPedidoHandler
{
    Task<Response<ItemPedido?>> CreateAsync(CreateItemPedidoRequest request);
    Task<Response<ItemPedido?>> UpdateAsync(UpdateItemPedidoRequest request);
    Task<Response<ItemPedido?>> DeleteAsync(DeleteItemPedidoRequest request);
    Task<Response<ItemPedido?>> GetByIdAsync(GetItemPedidoByIdRequest request);
}