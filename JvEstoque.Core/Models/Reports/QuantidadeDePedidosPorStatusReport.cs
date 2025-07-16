using JvEstoque.Core.Enums;

namespace JvEstoque.Core.Models.Reports;

public record QuantidadeDePedidosPorStatusReport(EStatusPedido Status, int Quantidade);