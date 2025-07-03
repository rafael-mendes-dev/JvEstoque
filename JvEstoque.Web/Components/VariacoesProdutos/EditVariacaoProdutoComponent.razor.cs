using JvEstoque.Core.Enums;
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

public partial class EditVariacaoProdutoComponent : ComponentBase
{
    #region Properties
    public bool IsBusy { get; set; } = true;
    public UpdateVariacaoProdutoRequest InputModel { get; set; } = new();
    public IEnumerable<Produto> Produtos { get; set; } = Enumerable.Empty<Produto>();
    public IEnumerable<Escola> Escolas { get; set; } = Enumerable.Empty<Escola>();
    #endregion

    #region Parameters
    [Parameter] public int Id { get; set; }
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
                LoadEscolasAsync(),
                LoadVariacaoAsync()
            );
        }
        catch (Exception ex)
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
    private async Task LoadVariacaoAsync()
    {
        var response = await Handler.GetByIdAsync(new GetVariacaoProdutoByIdRequest { Id = Id });
        if (response.IsSucess && response.Data is not null)
        {
            var variacao = response.Data;
            InputModel.Id = variacao.Id;
            InputModel.Sku = variacao.Sku;
            InputModel.ProdutoId = variacao.Produto.Id;
            InputModel.EscolaId = variacao.Escola.Id;
            InputModel.Cor = variacao.Cor;
            InputModel.Tamanho = variacao.Tamanho;
            InputModel.Genero = variacao.Genero;
            InputModel.Tecido = variacao.Tecido;
            InputModel.Estoque.Quantidade = variacao.Estoque.Quantidade;
        }
        else
        {
            Snackbar.Add(response.Message!, Severity.Error);
        }
    }

    private async Task LoadProdutosAsync()
    {
        var response = await ProdutoHandler.GetAllAsync(new GetAllProdutosRequest());
        if (response.IsSucess)
        {
            Produtos = response.Data ?? Enumerable.Empty<Produto>();
            InputModel.ProdutoId = Produtos.FirstOrDefault()?.Id ?? 0;
        }
    }

    private async Task LoadEscolasAsync()
    {
        var response = await EscolaHandler.GetAllAsync(new GetAllEscolasRequest());
        if (response.IsSucess)
        {
            Escolas = response.Data ?? Enumerable.Empty<Escola>();
            InputModel.EscolaId = Escolas.FirstOrDefault()?.Id ?? 0;
        }
    }

    public async Task OnValidSubmitAsync()
    {
        IsBusy = true;
        try
        {
            var response = await Handler.UpdateAsync(InputModel);
            if (response is { IsSucess: false, Message: not null })
            {
                Snackbar.Add(response.Message, Severity.Error);
                return;
            }
            DialogInstance.Close(DialogResult.Ok(true));
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