using JvEstoque.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JvEstoque.Api.Data.Mappings;

public class VariacaoProdutoMapping : IEntityTypeConfiguration<VariacaoProduto>
{
    public void Configure(EntityTypeBuilder<VariacaoProduto> builder)
    {
        builder.ToTable("VariacaoProduto");
        builder.HasKey(vp => vp.Id);
        builder.Property(vp => vp.Sku).IsRequired().HasMaxLength(50).HasColumnType("NVARCHAR(50)");
        builder.Property(vp => vp.Tamanho).IsRequired().HasColumnType("SMALLINT");
        builder.Property(vp => vp.Cor).IsRequired().HasMaxLength(30).HasColumnType("NVARCHAR(30)");
        builder.Property(vp => vp.Tecido).IsRequired(false).HasMaxLength(50).HasColumnType("NVARCHAR(50)");
        builder.Property(vp => vp.Genero).IsRequired().HasColumnType("SMALLINT");
        
        builder.HasOne(vp => vp.Produto)
            .WithMany(p => p.Variacoes)
            .HasForeignKey(vp => vp.ProdutoId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(vp => vp.Escola)
            .WithMany(e => e.VariacoesProdutos)
            .HasForeignKey(vp => vp.EscolaId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(vp => vp.Estoque)
            .WithOne(e => e.Variacao)
            .HasForeignKey<Estoque>(e => e.VariacaoProdutoId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(vp => vp.ItensPedidos)
            .WithOne(ip => ip.VariacaoProduto)
            .HasForeignKey(ip => ip.VariacaoProdutoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}