using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.Pedidos;
using JvEstoque.Web.Components.Pedidos;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace JvEstoque.Web.Pages;

public partial class PedidosBase : ComponentBase
{
    #region Properties
    public bool IsBusy { get; set; } = false;
    public MudDataGrid<Pedido> Grid = null!;
    #endregion

    #region Services
    [Inject] public ISnackbar Snackbar { get; set; } = null!;
    [Inject] public IDialogService DialogService { get; set; } = null!;
    [Inject] public IPedidoHandler Handler { get; set; } = null!;
    #endregion

    #region Methods
    public async Task<GridData<Pedido>> LoadServerData(GridState<Pedido> state)
    {
        IsBusy = true;
        StateHasChanged();

        var request = new GetAllPedidosRequest
        {
            PageNumber = state.Page + 1,
            PageSize = state.PageSize,
        };

        try
        {
            var result = await Handler.GetAllAsync(request);
            if (result.IsSucess)
            {
                return new GridData<Pedido>
                {
                    Items = result.Data ?? [],
                    TotalItems = result.TotalCount
                };
            }
            Snackbar.Add(result.Message!, Severity.Error);
        }
        catch (Exception e)
        {
            Snackbar.Add(e.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
            StateHasChanged();
        }

        return new GridData<Pedido> { Items = [], TotalItems = 0 };
    }

    public async Task OnCreateButtonClickedAsync()
    {
        var dialog = await DialogService.ShowAsync<CreatePedidoComponent>("Criar Pedido", new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium });
        var result = await dialog.Result;
        if (!result.Canceled)
        {
            await Grid.ReloadServerData();
        }
    }

    public async Task OnEditButtonClickedAsync(long id)
    {
        var parameters = new DialogParameters { ["Id"] = id };
        var dialog = await DialogService.ShowAsync<EditPedidoComponent>($"Editar Pedido #{id}", parameters, new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium });
        var result = await dialog.Result;
        if (!result.Canceled)
        {
            await Grid.ReloadServerData();
        }
    }

    public async Task OnDeleteButtonClickedAsync(long id)
    {
        var result = await DialogService.ShowMessageBox(
            "Atenção",
            $"Deseja realmente excluir o pedido #{id}?",
            yesText: "Excluir",
            cancelText: "Cancelar");

        if (result is true)
        {
            var deleteResult = await Handler.DeleteAsync(new DeletePedidoRequest { Id = id });
            if (deleteResult.IsSucess)
            {
                Snackbar.Add("Pedido excluído com sucesso!", Severity.Success);
                await Grid.ReloadServerData();
            }
            else
            {
                Snackbar.Add(deleteResult.Message, Severity.Error);
            }
        }
    }
    #endregion
}
