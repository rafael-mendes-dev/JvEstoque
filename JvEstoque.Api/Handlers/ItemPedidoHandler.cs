using JvEstoque.Api.Data;
using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.ItensPedidos;
using JvEstoque.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace JvEstoque.Api.Handlers;

public class ItemPedidoHandler(AppDbContext context) : IItemPedidoHandler
{
    public async Task<Response<ItemPedido?>> CreateAsync(CreateItemPedidoRequest request)
    {
        try
        {
            var itemPedido = new ItemPedido
            {
                PedidoId = request.PedidoId,
                VariacaoProdutoId = request.VariacaoProdutoId,
                Quantidade = request.Quantidade,
                ValorUnitario = request.ValorUnitario
            };

            await context.ItensPedidos.AddAsync(itemPedido);
            await context.SaveChangesAsync();
            return new Response<ItemPedido?>(itemPedido, 201, "Item do pedido criado com sucesso.");
        }
        catch
        {
            return new Response<ItemPedido?>(null, 500, "Erro ao criar o item do pedido. Por favor, tente novamente mais tarde.");
        }
    }

    public async Task<Response<ItemPedido?>> UpdateAsync(UpdateItemPedidoRequest request)
    {
        try
        {
            var itemPedido = await context.ItensPedidos.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (itemPedido is null)
                return new Response<ItemPedido?>(null, 404, "Item do pedido não encontrado.");

            itemPedido.Quantidade = request.Quantidade;
            itemPedido.ValorUnitario = request.ValorUnitario;
            itemPedido.SubTotal = itemPedido.Quantidade * itemPedido.ValorUnitario;

            context.ItensPedidos.Update(itemPedido);
            await context.SaveChangesAsync();

            return new Response<ItemPedido?>(itemPedido, message: "Item do pedido atualizado com sucesso.");
        }
        catch
        {
            return new Response<ItemPedido?>(null, 500, "Erro ao atualizar o item do pedido. Por favor, tente novamente mais tarde.");
        }
    }

    public async Task<Response<ItemPedido?>> DeleteAsync(DeleteItemPedidoRequest request)
    {
        try
        {
            var itemPedido = await context.ItensPedidos.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (itemPedido is null)
                return new Response<ItemPedido?>(null, 404, "Item do pedido não encontrado.");
            
            context.ItensPedidos.Remove(itemPedido);
            await context.SaveChangesAsync();
            
            return new Response<ItemPedido?>(itemPedido, message: "Item do pedido excluído com sucesso.");
        }
        catch
        {
            return new Response<ItemPedido?>(null, 500, "Erro ao excluir o item do pedido. Por favor, tente novamente mais tarde.");
        }
    }

    public async Task<Response<ItemPedido?>> GetByIdAsync(GetItemPedidoByIdRequest request)
    {
        try
        {
            var itemPedido = await context.ItensPedidos.FirstOrDefaultAsync(x => x.Id == request.Id); 
            
            return itemPedido is null
                ? new Response<ItemPedido?>(null, 404, "Item do pedido não encontrado.")
                : new Response<ItemPedido?>(itemPedido, message: "Item do pedido encontrado com sucesso.");
        }
        catch 
        {
            return new Response<ItemPedido?>(null, 500, "Erro ao buscar o item do pedido. Por favor, tente novamente mais tarde.");
        }
    }
}