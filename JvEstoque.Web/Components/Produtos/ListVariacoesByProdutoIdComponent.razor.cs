using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.VariacoesProdutos;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace JvEstoque.Web.Components.Produtos;

public partial class ListVariacoesByProdutoIdComponentBase : ComponentBase
{
    #region Properties

    public bool IsBusy { get; set; }
    public List<VariacaoProduto> Variacoes { get; set; } = new();
    public const string VariacoesRote = "/variacoes";
    [Parameter] public int ProdutoId { get; set; }

    #endregion

    #region Services

    [Inject] public IVariacaoProdutoHandler VariacaoProdutoHandler { get; set; } = null!;
    [Inject] public ISnackbar Snackbar { get; set; } = null!;
    [Inject] public NavigationManager NavigationManager { get; set; } = null!;
    [Inject] public IDialogService DialogService { get; set; } = null!;

    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;
        try
        {
            var response = await VariacaoProdutoHandler.GetAllByProdutoIdAsync(new GetAllVariacoesProdutosByProdutoIdRequest{ ProdutoId = ProdutoId});
            if (response.IsSucess)
            {
                Variacoes = response.Data ?? new List<VariacaoProduto>();
                Snackbar.Add("Variações carregadas com sucesso!", Severity.Success);
            }
            else
            {
                Snackbar.Add(response.Message!, Severity.Error);
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
                $"Você tem certeza que deseja excluir a variação {title}?",
                yesText: "Sim",
                cancelText: "Não"
            );

            if (result is true)
            {
                await OnDeleteAsync(id, title);
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
            var result = await VariacaoProdutoHandler.DeleteAsync(request);
            if (result.IsSucess)
            {
                Snackbar.Add($"Variação {title} excluída com sucesso!", Severity.Success);
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

    #endregion
}