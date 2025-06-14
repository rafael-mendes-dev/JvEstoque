using JvEstoque.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JvEstoque.Api.Data.Mappings;

public class PedidoMapping : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.ToTable("Pedido");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.NomeCliente).IsRequired().HasMaxLength(120).HasColumnType("NVARCHAR(120)");
        builder.Property(p => p.TelefoneCliente).HasMaxLength(15).HasColumnType("NVARCHAR(15)");
        builder.Property(p => p.Status).IsRequired().HasColumnType("SMALLINT");
        builder.Property(p => p.DataPedido).IsRequired();
        builder.Property(p => p.ValorTotal).IsRequired().HasColumnType("MONEY").HasDefaultValue(0);
        
        builder.HasMany(p => p.Itens).WithOne(p => p.Pedido).HasForeignKey(p => p.PedidoId);
    }
}