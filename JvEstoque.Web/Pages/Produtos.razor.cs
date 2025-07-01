using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.Produtos;
using JvEstoque.Web.Components.Produtos;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace JvEstoque.Web.Pages;

public partial class ProdutosBase : ComponentBase
{
    #region Properties

    public bool IsBusy { get; set; } = false;
    public List<Produto> Produtos { get; set; } = [];
    public string SearchTerm { get; set; } = string.Empty;
    public MudDataGrid<Produto> _grid = null!;
    

    #endregion
    
    #region Services
    
    [Inject] public ISnackbar Snackbar { get; set; } = null!;
    [Inject] public IDialogService DialogService { get; set; } = null!;
    [Inject] public IProdutoHandler Handler { get; set; } = null!;
    [Inject] public NavigationManager NavigationManager { get; set; } = null!;
    
    #endregion
    
    // #region Overrides
    //
    // protected override async Task OnInitializedAsync()
    // {
    //     IsBusy = true;
    //
    //     try
    //     {
    //         var request = new GetAllProdutosRequest();
    //         var result = await Handler.GetAllAsync(request);
    //         if (result.IsSucess)
    //             Produtos = result.Data ?? [];
    //         else
    //         {
    //             Snackbar.Add(result.Message!, Severity.Error);
    //         }
    //     }
    //     catch (Exception e)
    //     {
    //         Snackbar.Add(e.Message, Severity.Error);
    //     }
    //     finally
    //     {
    //         IsBusy = false;
    //     }
    // }
    //
    // #endregion
    
    #region Methods
    
    public async Task OnDeleteButtonClickedAsync(int id, string title)
    {
        try
        {
            var result = await DialogService.ShowMessageBox(
                "Confirmação",
                $"Você tem certeza que deseja excluir o produto {title}?",
                yesText: "Sim",
                cancelText: "Não"
            );

            if (result is true)
            {
                await OnDeleteAsync(id, title);
                await _grid.ReloadServerData();
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
            var request = new DeleteProdutoRequest{ Id = id};
            var result = await Handler.DeleteAsync(request);
            if (result.IsSucess)
            {
                Snackbar.Add($"Produto {title} excluído com sucesso!", Severity.Success);
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

    public async Task OnEditButtonClickedAsync(int id)
    {
        IsBusy = true;

        try
        {
            var dialogReference = await DialogService.ShowAsync<EditProdutosComponent>(
                "Editar Produto",
                new DialogParameters { { "Id", id } },
                new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraLarge }
            );
            
            var result = await dialogReference.Result;
            
            if (result is not null)
            {
                Snackbar.Add("Produto atualizado com sucesso!", Severity.Success);
                await _grid.ReloadServerData();
                
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
            var dialogReference = await DialogService.ShowAsync<CreateProdutoComponent>(
                "Criar Produto",
                new DialogParameters(),
                new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraLarge }
            );
            
            var result = await dialogReference.Result;
            
            if (result is not null)
            {
                Snackbar.Add("Produto criado com sucesso!", Severity.Success);
                await _grid.ReloadServerData();
                
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
    
    public async Task<GridData<Produto>> LoadServerData(GridState<Produto> state)
    {
        IsBusy = true;
        StateHasChanged();

        var request = new GetAllProdutosRequest
        {
            PageNumber = state.Page,
            PageSize = state.PageSize,
        };

        try
        {
            var result = await Handler.GetAllAsync(request);
            if (result.IsSucess)
            {
                return new GridData<Produto>
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

        return new GridData<Produto> { Items = [], TotalItems = 0 };
    }
    
    
    
    #endregion
}