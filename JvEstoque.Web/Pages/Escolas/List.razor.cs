using JvEstoque.Core.Handlers;
using JvEstoque.Core.Models;
using JvEstoque.Core.Requests.Escolas;
using JvEstoque.Web.Components.Escolas;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace JvEstoque.Web.Pages.Escolas;

public class ListEscolasPage : ComponentBase
{
    #region Properties

    public bool IsBusy { get; set; }
    protected List<Escola> Escolas { get; set; } = [];

    public string SearchTerm { get; set; } = string.Empty;
    
    #endregion

    #region Services

    [Inject] public ISnackbar Snackbar { get; set; } = null!;
    [Inject] public IDialogService DialogService { get; set; } = null!;
    [Inject] public IEscolaHandler Handler { get; set; } = null!;

    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;

        try
        {
            var request = new GetAllEscolasRequest();
            var result = await Handler.GetAllAsync(request);
            if (result.IsSucess)
                Escolas = result.Data ?? [];
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
                $"Você tem certeza que deseja excluir a escola {title}?",
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
            var request = new DeleteEscolaRequest{ Id = id};
            var result = await Handler.DeleteAsync(request);
            if (result.IsSucess)
            {
                Snackbar.Add($"Escola {title} excluída com sucesso!", Severity.Success);
                Escolas.RemoveAll(e => e.Id == id);
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
            var dialogReference = await DialogService.ShowAsync<EditEscolaComponent>(
                "Editar Escola",
                new DialogParameters { { "Id", id } },
                new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraLarge }
            );
            
            var result = await dialogReference.Result;
            
            if (result is not null)
            {
                Snackbar.Add("Escola atualizada com sucesso!", Severity.Success);
                var request = new GetAllEscolasRequest();
                var response = await Handler.GetAllAsync(request);
                if (response.IsSucess)
                    Escolas = response.Data ?? [];
                
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
            var dialogReference = await DialogService.ShowAsync<CreateEscolaComponent>(
                "Criar Escola",
                new DialogParameters(),
                new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraLarge }
            );

            var result = await dialogReference.Result;
            
            if (result is not null)
            {
                Snackbar.Add("Escola criada com sucesso!", Severity.Success);
                var request = new GetAllEscolasRequest();
                var response = await Handler.GetAllAsync(request);
                if (response.IsSucess)
                    Escolas = response.Data ?? [];
                
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

    #endregion
}