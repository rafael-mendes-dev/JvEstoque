using JvEstoque.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JvEstoque.Api.Data.Mappings;

public class ItemPedidoMapping : IEntityTypeConfiguration<ItemPedido>
{
    public void Configure(EntityTypeBuilder<ItemPedido> builder)
    {
        builder.ToTable("ItemPedido");
        
        builder.HasKey(ip => ip.Id);
        builder.Property(ip => ip.Quantidade).IsRequired().HasColumnType("INT").HasDefaultValue(1);
        builder.Property(ip => ip.ValorUnitario).IsRequired().HasColumnType("MONEY");
        builder.Property(ip => ip.SubTotal).IsRequired().HasColumnType("MONEY");
        
        builder.HasOne(ip => ip.Pedido)
            .WithMany(p => p.Itens)
            .HasForeignKey(ip => ip.PedidoId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(ip => ip.VariacaoProduto)
            .WithMany(vp => vp.ItensPedidos)
            .HasForeignKey(ip => ip.VariacaoProdutoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}