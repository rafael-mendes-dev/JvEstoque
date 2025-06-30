using JvEstoque.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JvEstoque.Api.Data.Mappings;

public class EstoqueMapping : IEntityTypeConfiguration<Estoque>
{
    public void Configure(EntityTypeBuilder<Estoque> builder)
    {
        builder.ToTable("Estoque");
        
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Quantidade).IsRequired();
        builder.HasOne(e => e.Variacao)
            .WithOne(vp => vp.Estoque)
            .HasForeignKey<Estoque>(e => e.VariacaoProdutoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}