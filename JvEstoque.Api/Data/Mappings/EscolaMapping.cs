using JvEstoque.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JvEstoque.Api.Data.Mappings;

public class EscolaMapping : IEntityTypeConfiguration<Escola>
{
    public void Configure(EntityTypeBuilder<Escola> builder)
    {
        builder.ToTable("Escola");
        
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Nome).IsRequired().HasColumnType("NVARCHAR").HasMaxLength(100);
        builder.Property(e => e.Endereco).IsRequired(false).HasColumnType("NVARCHAR").HasMaxLength(120);
        builder.Property(e => e.Telefone).IsRequired(false).HasColumnType("NVARCHAR").HasMaxLength(15);
        builder.HasMany(e => e.VariacoesProdutos)
            .WithOne(vp => vp.Escola)
            .HasForeignKey(vp => vp.EscolaId);
    }
}
