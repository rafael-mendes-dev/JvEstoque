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
        if (!result!.Canceled)
        {
            await Grid.ReloadServerData();
        }
    }

    public async Task OnEditButtonClickedAsync(int id)
    {
        var parameters = new DialogParameters { ["Id"] = id };
        var dialog = await DialogService.ShowAsync<EditPedidoComponent>($"Editar Pedido #{id}", parameters, new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium });
        var result = await dialog.Result;
        if (!result!.Canceled)
        {
            await Grid.ReloadServerData();
        }
    }

    public async Task OnDeleteButtonClickedAsync(int id)
    {
        try
        {
            var result = await DialogService.ShowMessageBox(
                "Confirmação",
                $"Você tem certeza que deseja excluir o pedido #{id}?",
                yesText: "Sim",
                cancelText: "Não"
            );

            if (result is true)
            {
                await OnDeleteAsync(id);
                await Grid.ReloadServerData();
            }

            StateHasChanged();
        }
        catch (Exception e)
        {
            Snackbar.Add(e.Message, Severity.Error);
        }
    }
    
    private async Task OnDeleteAsync(int id)
    {
        IsBusy = true;

        try
        {
            var request = new DeletePedidoRequest{ Id = id};
            var result = await Handler.DeleteAsync(request);
            if (result.IsSucess)
            {
                Snackbar.Add($"Pedido #{id} excluído com sucesso!", Severity.Success);
            }
            else
            {
                Snackbar.Add(result.Message!, Severity.Error);
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

    public async Task OnViewItemsButtonClickedAsync(int id)
    {
        var parameters = new DialogParameters { ["Id"] = id };
        await DialogService.ShowAsync<ViewItensPedidoComponent>($"Itens do Pedido #{id}", parameters, new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium });
    }
    #endregion
}
