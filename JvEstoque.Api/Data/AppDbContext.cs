using System.Reflection;
using JvEstoque.Api.Models;
using JvEstoque.Core.Models;
using JvEstoque.Core.Models.Reports;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JvEstoque.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<User, 
    IdentityRole<int>, 
    int,  
    IdentityUserClaim<int>, 
    IdentityUserRole<int>, 
    IdentityUserLogin<int>, 
    IdentityRoleClaim<int>, 
    IdentityUserToken<int>> (options)
{
    public DbSet<Escola> Escolas { get; set; } = null!;
    public DbSet<Estoque> Estoques { get; set; } = null!;
    public DbSet<ItemPedido> ItensPedidos { get; set; } = null!;
    public DbSet<Pedido> Pedidos { get; set; } = null!;
    public DbSet<Produto> Produtos { get; set; } = null!;
    public DbSet<VariacaoProduto> VariacoesProdutos { get; set; } = null!;
    public DbSet<FaturamentoReports> Faturamento { get; set; } = null!;
    public DbSet<ItensEmBaixoEstoqueReport> ItensEmBaixoEstoque { get; set; } = null!;
    public DbSet<PedidosConcluidosReport> PedidosConcluidos { get; set; } = null!;
    public DbSet<QuantidadeDePedidosPorStatusReport> QuantidadeDePedidosPorStatus { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<FaturamentoReports>().HasNoKey().ToView("vwFaturamentoReport");
        modelBuilder.Entity<ItensEmBaixoEstoqueReport>().HasNoKey().ToView("vwItensEmBaixoEstoqueReport");
        modelBuilder.Entity<PedidosConcluidosReport>().HasNoKey().ToView("vwPedidosConcluidosReport");
        modelBuilder.Entity<QuantidadeDePedidosPorStatusReport>().HasNoKey().ToView("vwQuantidadeDePedidosPorStatusReport");
    }
}