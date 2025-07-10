using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.Escolas;
using JvEstoque.Core.Requests.Estoques;
using JvEstoque.Core.Requests.Produtos;
using JvEstoque.Core.Requests.VariacoesProdutos;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace JvEstoque.Web.Components.VariacoesProdutos;

public partial class EditVariacaoProdutoComponentBase : ComponentBase
{
    #region Properties

    [Parameter] public int Id { get; set; }
    public bool IsBusy { get; set; } = false;
    public UpdateVariacaoProdutoRequest InputModel { get; set; } = new();
    public List<Produto> Produtos { get; set; } = [];
    public List<Escola> Escolas { get; set; } = [];

    #endregion

    #region Services

    [Inject] public ISnackbar Snackbar { get; set; } = null!;
    [CascadingParameter] public IMudDialogInstance DialogInstance { get; set; } = null!;
    [Inject] public IVariacaoProdutoHandler Handler { get; set; } = null!;
    [Inject] public IProdutoHandler ProdutoHandler { get; set; } = null!;
    [Inject] public IEscolaHandler EscolaHandler { get; set; } = null!;

    #endregion
    
    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;
        
        await GetVariacaoProdutoByIdAsync();
        await GetProdutosAsync();
        await GetEscolasAsync();
        
        IsBusy = false;
    }
    
    #endregion
    
    #region Private Methods

    private async Task GetVariacaoProdutoByIdAsync()
    {
        IsBusy = true;
        try
        {
            var result = await Handler.GetByIdAsync(new GetVariacaoProdutoByIdRequest { Id = Id });
            if (result is { IsSucess: true, Data: not null })
            {
                InputModel = new UpdateVariacaoProdutoRequest
                {
                    Id = result.Data.Id,
                    ProdutoId = result.Data.ProdutoId,
                    EscolaId = result.Data.EscolaId,
                    Cor = result.Data.Cor,
                    Tamanho = result.Data.Tamanho,
                    Tecido = result.Data.Tecido,
                    Sku = result.Data.Sku,
                    Genero = result.Data.Genero,
                    EstoqueId = result.Data.EstoqueId,
                    Estoque = new UpdateEstoqueRequest
                    {
                        VariacaoProdutoId = result.Data.Id,
                        Quantidade = result.Data.Estoque.Quantidade,
                    }
                };
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
    
    private async Task GetProdutosAsync()
    {
        IsBusy = true;
        try
        {
            var result = await ProdutoHandler.GetAllAsync(new GetAllProdutosRequest());
            if (result.IsSucess)
            {
                Produtos = result.Data ?? [];
                InputModel.ProdutoId = Produtos.FirstOrDefault()?.Id ?? 0;
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

    private async Task GetEscolasAsync()
    {
        IsBusy = true;
        try
        {
            var result = await EscolaHandler.GetAllAsync(new GetAllEscolasRequest());
            if (result.IsSucess)
            {
                Escolas = result.Data ?? [];
                InputModel.EscolaId = Escolas.FirstOrDefault()?.Id ?? 0;
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
            StateHasChanged();
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