using JvEstoque.Core.Handlers;
using JvEstoque.Core.Requests.Reports;
using JvEstoque.Web.Common;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace JvEstoque.Web.Components.Reports;

public partial class PedidosPorStatusChartComponent : ComponentBase
{
    #region Properties

    public bool IsBusy { get; set; }
    public string[] Labels { get; set; } = [];
    public double[] Data { get; set; } = [];
    public int Index = -1;

    #endregion

    #region Services

    [Inject] private IReportHandler Handler { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;

    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;

        try
        {
            var response = await Handler.GetQuantidadeDePedidosPorStatusAsync(new GetQuantidadeDePedidosPorStatusRequest());
            if (response.IsSucess && response.Data != null)
            {
                Labels = response.Data.Select(item => item.Status.ToString().AdicionarEspacoAntesMaiuscula()).ToArray();
                Data = response.Data.Select(item => (double)item.Quantidade).ToArray();
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
