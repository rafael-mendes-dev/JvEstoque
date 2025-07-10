using JvEstoque.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JvEstoque.Api.Data.Mappings;

public class ProdutoMapping : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable("Produto");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Nome).IsRequired().HasMaxLength(50).HasColumnType("VARCHAR(50)");
        builder.Property(p => p.Descricao).IsRequired(false).HasMaxLength(250).HasColumnType("VARCHAR(250)");
        builder.Property(p => p.Preco).IsRequired().HasColumnType("MONEY");
        builder.Property(p => p.Peca).IsRequired().HasColumnType("SMALLINT");
    }
}