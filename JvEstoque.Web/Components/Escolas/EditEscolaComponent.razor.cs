using JvEstoque.Core.Handlers;
using JvEstoque.Core.Requests.Escolas;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace JvEstoque.Web.Components.Escolas;

public partial class EditEscolaPageBase : ComponentBase
{
    #region Properties
    [Parameter] public int Id { get; set; }
    public bool IsBusy { get; set; } = false;
    public UpdateEscolaRequest InputModel { get; set; } = new();

    #endregion

    #region Services
    
    [Inject] public ISnackbar Snackbar { get; set; } = null!;
    [Inject] public IEscolaHandler Handler { get; set; } = null!;
    [CascadingParameter] public IMudDialogInstance DialogInstance { get; set; } = null!;
    
    
    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        GetEscolaByIdRequest request = new GetEscolaByIdRequest
        {
            Id = Id
        };

        IsBusy = true;
        try
        {
            var response = await Handler.GetByIdAsync(request);
            if (response is { IsSucess: true, Data: not null })
                InputModel = new UpdateEscolaRequest
                {
                    Id = response.Data.Id,
                    Nome = response.Data.Nome,
                    Endereco = response.Data.Endereco,
                    Telefone = response.Data.Telefone,
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