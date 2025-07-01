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
    public string SearchTerm { get; set; }

    #endregion
    
    #region Services
    
    [Inject] public ISnackbar Snackbar { get; set; } = null!;
    [Inject] public IDialogService DialogService { get; set; } = null!;
    [Inject] public IProdutoHandler Handler { get; set; } = null!;
    
    #endregion
    
    #region Overrides
    
    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;

        try
        {
            var request = new GetAllProdutosRequest();
            var result = await Handler.GetAllAsync(request);
            if (result.IsSucess)
                Produtos = result.Data ?? [];
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
    
    #endregion
    
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
                await OnDeleteAsync(id, title);
        
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
                Produtos.RemoveAll(e => e.Id == id);
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
                var request = new GetAllProdutosRequest();
                var response = await Handler.GetAllAsync(request);
                if (response.IsSucess)
                    Produtos = response.Data ?? [];
                
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
                var request = new GetAllProdutosRequest();
                var response = await Handler.GetAllAsync(request);
                if (response.IsSucess)
                    Produtos = response.Data ?? [];
                
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
    
    public Func<Produto, bool> Filter => produto =>
    {
        if (string.IsNullOrWhiteSpace(SearchTerm))
            return true;

        return produto.Nome.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) ||
               produto.Descricao!.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) ||
               produto.Peca.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) ||
               produto.Id.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) ||
               produto.Preco.ToString("C").Contains(SearchTerm, StringComparison.OrdinalIgnoreCase);
    };
    
    #endregion
}