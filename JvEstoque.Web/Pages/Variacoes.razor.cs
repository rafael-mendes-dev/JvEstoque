using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.VariacoesProdutos;
using JvEstoque.Web.Components.VariacoesProdutos;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace JvEstoque.Web.Pages;

public partial class VariacoesBase : ComponentBase
{
     #region Properties

    public bool IsBusy { get; set; } = false;
    public List<VariacaoProduto> VariacaoProdutos { get; set; } = [];
    public MudDataGrid<VariacaoProduto> Grid = null!;
    

    #endregion
    
    #region Services
    
    [Inject] public ISnackbar Snackbar { get; set; } = null!;
    [Inject] public IDialogService DialogService { get; set; } = null!;
    [Inject] public IVariacaoProdutoHandler Handler { get; set; } = null!;
    [Inject] public NavigationManager NavigationManager { get; set; } = null!;
    
    #endregion
    
    #region Methods
    
    public async Task OnDeleteButtonClickedAsync(int id, string title)
    {
        try
        {
            var result = await DialogService.ShowMessageBox(
                "Confirmação",
                $"Você tem certeza que deseja excluir a variação {title}?",
                yesText: "Sim",
                cancelText: "Não"
            );

            if (result is true)
            {
                await OnDeleteAsync(id, title);
                await Grid.ReloadServerData();
            }

            StateHasChanged();
        }
        catch (Exception e)
        {
            Snackbar.Add(e.Message, Severity.Error);
        }
    }
    
    private async Task OnDeleteAsync(int id, string title)
    {
        IsBusy = true;

        try
        {
            var request = new DeleteVariacaoProdutoRequest{ Id = id};
            var result = await Handler.DeleteAsync(request);
            Snackbar.Add(result.Message!, result.IsSucess ? Severity.Info : Severity.Error);
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

    public async Task OnEditButtonClickedAsync(int id)
    {
        IsBusy = true;

        try
        {
            var dialogReference = await DialogService.ShowAsync<EditVariacaoProdutoComponent>(
                "Editar Variação",
                new DialogParameters { { "Id", id } },
                new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraLarge }
            );
            
            var result = await dialogReference.Result;
            
            if (!result!.Canceled)
            {
                Snackbar.Add("Variação atualizada com sucesso!", Severity.Success);
                await Grid.ReloadServerData();
                
                StateHasChanged();
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
    
    public async Task OnCreateButtonClickedAsync()
    {
        IsBusy = true;

        try
        {
            var dialogReference = await DialogService.ShowAsync<CreateVariacaoProdutoComponent>(
                "Criar Variação",
                new DialogParameters(),
                new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraLarge }
            );
            
            var result = await dialogReference.Result;
            
            if (result is not null)
            {
                Snackbar.Add("Variacao criada com sucesso!", Severity.Success);
                await Grid.ReloadServerData();
                
                StateHasChanged();
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
    
    public async Task<GridData<VariacaoProduto>> LoadServerData(GridState<VariacaoProduto> state)
    {
        IsBusy = true;
        StateHasChanged();

        var request = new GetAllVariacoesProdutosRequest()
        {
            PageNumber = state.Page + 1,
            PageSize = state.PageSize,
        };

        try
        {
            var result = await Handler.GetAllAsync(request);
            if (result.IsSucess)
            {
                return new GridData<VariacaoProduto>
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

        return new GridData<VariacaoProduto> { Items = [], TotalItems = 0 };
    }
    
    
    
    #endregion
}