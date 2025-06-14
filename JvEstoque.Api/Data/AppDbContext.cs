using System.Reflection;
using JvEstoque.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace JvEstoque.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext
{
    public DbSet<Escola> Escolas { get; set; }
    public DbSet<Estoque> Estoques { get; set; }
    public DbSet<ItemPedido> ItensPedidos { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<VariacaoProduto> VariacoesProdutos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}