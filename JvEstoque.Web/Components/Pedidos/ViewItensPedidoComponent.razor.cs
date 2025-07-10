using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.Pedidos;
using JvEstoque.Core.Requests.VariacoesProdutos;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace JvEstoque.Web.Components.Pedidos;

public partial class ViewItensPedidoComponentBase : ComponentBase
{
    #region Properties

    public bool IsBusy { get; set; }
    public IList<ItemPedido> Itens { get; set; } = new List<ItemPedido>();
    public const string VariacoesRote = "/variacoes";
    [Parameter] public int Id { get; set; }

    #endregion

    #region Services

    [Inject] public IPedidoHandler Handler { get; set; } = null!;
    [Inject] public ISnackbar Snackbar { get; set; } = null!;
    [Inject] public NavigationManager NavigationManager { get; set; } = null!;
    [Inject] public IDialogService DialogService { get; set; } = null!;
    [CascadingParameter] public IMudDialogInstance DialogInstance { get; set; } = null!;

    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;
        try
        {
            var response = await Handler.GetByIdAsync(new GetPedidoByIdRequest{ Id = Id});
            if (response.IsSucess && response.Data != null)
            {
                Itens = response.Data.Itens ?? new List<ItemPedido>();
            }
            else
            {
                Snackbar.Add(response.Message ?? "Erro ao carregar itens do pedido.", Severity.Error);
            }
        }
        catch (Exception e)
        {
            Snackbar.Add(e.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }

    #endregion
}