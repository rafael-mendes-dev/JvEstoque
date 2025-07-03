using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.Escolas;
using JvEstoque.Core.Requests.Produtos;
using JvEstoque.Core.Requests.VariacoesProdutos;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JvEstoque.Web.Components.VariacoesProdutos;

public partial class CreateVariacaoProdutoComponent : ComponentBase
{
    #region Properties
    public bool IsBusy { get; set; } = true;
    public CreateVariacaoProdutoRequest InputModel { get; set; } = new();
    public IEnumerable<Produto> Produtos { get; set; } = Enumerable.Empty<Produto>();
    public IEnumerable<Escola> Escolas { get; set; } = Enumerable.Empty<Escola>();
    #endregion

    #region Parameters
    [Inject] public ISnackbar Snackbar { get; set; } = null!;
    [Inject] public IVariacaoProdutoHandler Handler { get; set; } = null!;
    [Inject] public IProdutoHandler ProdutoHandler { get; set; } = null!;
    [Inject] public IEscolaHandler EscolaHandler { get; set; } = null!;
    [CascadingParameter] IMudDialogInstance DialogInstance { get; set; } = null!;
    #endregion

    #region Overrides
    protected override async Task OnInitializedAsync()
    {
        try
        {
            await Task.WhenAll(
                LoadProdutosAsync(),
                LoadEscolasAsync()
            );
        }
        catch (System.Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }
    #endregion

    #region Methods
    private async Task LoadProdutosAsync()
    {
        var response = await ProdutoHandler.GetAllAsync(new GetAllProdutosRequest());
        if (response.IsSucess)
            Produtos = response.Data ?? Enumerable.Empty<Produto>();
    }

    private async Task LoadEscolasAsync()
    {
        var response = await EscolaHandler.GetAllAsync(new GetAllEscolasRequest());
        if (response.IsSucess)
            Escolas = response.Data ?? Enumerable.Empty<Escola>();
    }

    public async Task OnValidSubmitAsync()
    {
        IsBusy = true;
        try
        {
            var response = await Handler.CreateAsync(InputModel);
            if (response is { IsSucess: false, Message: not null })
            {
                Snackbar.Add(response.Message, Severity.Error);
                return;
            }
            DialogInstance.Close(DialogResult.Ok(true));
        }
        catch (System.Exception e)
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