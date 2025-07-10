using JvEstoque.Core.Handlers;
using JvEstoque.Core.Requests.Produtos;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace JvEstoque.Web.Components.Produtos;

public partial class EditProdutosComponentBase : ComponentBase
{
    #region Properties

    public bool IsBusy { get; set; } = false;
    public UpdateProdutoRequest InputModel { get; set; } = new();

    #endregion

    #region Parameters

    [Parameter] public int Id { get; set; }
    [Inject] public ISnackbar Snackbar { get; set; } = null!;
    [Inject] public IProdutoHandler Handler { get; set; } = null!;
    [CascadingParameter] public IMudDialogInstance DialogInstance { get; set; } = null!;
    
    
    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        var request = new GetProdutoByIdRequest
        {
            Id = Id
        };

        IsBusy = true;
        try
        {
            var response = await Handler.GetByIdAsync(request);
            if (response is { IsSucess: true, Data: not null })
                InputModel = new UpdateProdutoRequest
                {
                    Id = response.Data.Id,
                    Nome = response.Data.Nome,
                    Descricao = response.Data.Descricao,
                    Preco = response.Data.Preco,
                    Peca = response.Data.Peca
                };
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