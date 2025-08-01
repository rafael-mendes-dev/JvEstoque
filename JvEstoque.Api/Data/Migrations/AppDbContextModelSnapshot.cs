﻿// <auto-generated />
using System;
using JvEstoque.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JvEstoque.Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("JvEstoque.Api.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(180)
                        .HasColumnType("nvarchar(180)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(180)
                        .HasColumnType("nvarchar(180)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(180)
                        .HasColumnType("nvarchar(180)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(180)
                        .HasColumnType("nvarchar(180)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .IsUnique()
                        .HasFilter("[NormalizedEmail] IS NOT NULL");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("IdentityUser", (string)null);
                });

            modelBuilder.Entity("JvEstoque.Core.Models.Escola", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Endereco")
                        .HasMaxLength(120)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("Telefone")
                        .HasMaxLength(15)
                        .HasColumnType("NVARCHAR");

                    b.HasKey("Id");

                    b.ToTable("Escola", (string)null);
                });

            modelBuilder.Entity("JvEstoque.Core.Models.Estoque", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<int>("VariacaoProdutoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VariacaoProdutoId")
                        .IsUnique();

                    b.ToTable("Estoque", (string)null);
                });

            modelBuilder.Entity("JvEstoque.Core.Models.ItemPedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PedidoId")
                        .HasColumnType("int");

                    b.Property<int>("Quantidade")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT")
                        .HasDefaultValue(1);

                    b.Property<decimal>("ValorUnitario")
                        .HasColumnType("MONEY");

                    b.Property<int>("VariacaoProdutoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PedidoId");

                    b.HasIndex("VariacaoProdutoId");

                    b.ToTable("ItemPedido", (string)null);
                });

            modelBuilder.Entity("JvEstoque.Core.Models.Pedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataPedido")
                        .HasColumnType("datetime2");

                    b.Property<string>("NomeCliente")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("NVARCHAR(120)");

                    b.Property<short>("Status")
                        .HasColumnType("SMALLINT");

                    b.Property<string>("TelefoneCliente")
                        .HasMaxLength(15)
                        .HasColumnType("NVARCHAR(15)");

                    b.Property<decimal>("ValorTotal")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("MONEY")
                        .HasDefaultValue(0m);

                    b.HasKey("Id");

                    b.ToTable("Pedido", (string)null);
                });

            modelBuilder.Entity("JvEstoque.Core.Models.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descricao")
                        .HasMaxLength(250)
                        .HasColumnType("VARCHAR(250)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR(50)");

                    b.Property<short>("Peca")
                        .HasColumnType("SMALLINT");

                    b.Property<decimal>("Preco")
                        .HasColumnType("MONEY");

                    b.HasKey("Id");

                    b.ToTable("Produto", (string)null);
                });

            modelBuilder.Entity("JvEstoque.Core.Models.Reports.FaturamentoReports", b =>
                {
                    b.Property<decimal>("Entradas")
                        .HasColumnType("decimal(18,2)");

                    b.ToTable((string)null);

                    b.ToView("vwFaturamentoReport", (string)null);
                });

            modelBuilder.Entity("JvEstoque.Core.Models.Reports.ItensEmBaixoEstoqueReport", b =>
                {
                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.ToTable((string)null);

                    b.ToView("vwItensEmBaixoEstoqueReport", (string)null);
                });

            modelBuilder.Entity("JvEstoque.Core.Models.Reports.PedidosConcluidosReport", b =>
                {
                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.ToTable((string)null);

                    b.ToView("vwPedidosConcluidosReport", (string)null);
                });

            modelBuilder.Entity("JvEstoque.Core.Models.Reports.QuantidadeDePedidosPorStatusReport", b =>
                {
                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<short>("Status")
                        .HasColumnType("smallint");

                    b.ToTable((string)null);

                    b.ToView("vwQuantidadeDePedidosPorStatusReport", (string)null);
                });

            modelBuilder.Entity("JvEstoque.Core.Models.VariacaoProduto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Cor")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("NVARCHAR(30)");

                    b.Property<int>("EscolaId")
                        .HasColumnType("int");

                    b.Property<int>("EstoqueId")
                        .HasColumnType("int");

                    b.Property<short>("Genero")
                        .HasColumnType("SMALLINT");

                    b.Property<int>("ProdutoId")
                        .HasColumnType("int");

                    b.Property<string>("Sku")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR(50)");

                    b.Property<short>("Tamanho")
                        .HasColumnType("SMALLINT");

                    b.Property<string>("Tecido")
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR(50)");

                    b.HasKey("Id");

                    b.HasIndex("EscolaId");

                    b.HasIndex("ProdutoId");

                    b.ToTable("VariacaoProduto", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.HasIndex("UserId");

                    b.ToTable("IdentityRole", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ClaimValue")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("IdentityRoleClaim", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ClaimValue")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("IdentityClaim", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("IdentityUserLogin", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.ToTable("IdentityUserRole", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("Name")
                        .HasMaxLength(180)
                        .HasColumnType("nvarchar(180)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("IdentityUserToken", (string)null);
                });

            modelBuilder.Entity("JvEstoque.Core.Models.Estoque", b =>
                {
                    b.HasOne("JvEstoque.Core.Models.VariacaoProduto", "Variacao")
                        .WithOne("Estoque")
                        .HasForeignKey("JvEstoque.Core.Models.Estoque", "VariacaoProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Variacao");
                });

            modelBuilder.Entity("JvEstoque.Core.Models.ItemPedido", b =>
                {
                    b.HasOne("JvEstoque.Core.Models.Pedido", "Pedido")
                        .WithMany("Itens")
                        .HasForeignKey("PedidoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JvEstoque.Core.Models.VariacaoProduto", "VariacaoProduto")
                        .WithMany("ItensPedidos")
                        .HasForeignKey("VariacaoProdutoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Pedido");

                    b.Navigation("VariacaoProduto");
                });

            modelBuilder.Entity("JvEstoque.Core.Models.VariacaoProduto", b =>
                {
                    b.HasOne("JvEstoque.Core.Models.Escola", "Escola")
                        .WithMany("VariacoesProdutos")
                        .HasForeignKey("EscolaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("JvEstoque.Core.Models.Produto", "Produto")
                        .WithMany("Variacoes")
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Escola");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<int>", b =>
                {
                    b.HasOne("JvEstoque.Api.Models.User", null)
                        .WithMany("Roles")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("JvEstoque.Api.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("JvEstoque.Api.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("JvEstoque.Api.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("JvEstoque.Api.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JvEstoque.Api.Models.User", b =>
                {
                    b.Navigation("Roles");
                });

            modelBuilder.Entity("JvEstoque.Core.Models.Escola", b =>
                {
                    b.Navigation("VariacoesProdutos");
                });

            modelBuilder.Entity("JvEstoque.Core.Models.Pedido", b =>
                {
                    b.Navigation("Itens");
                });

            modelBuilder.Entity("JvEstoque.Core.Models.Produto", b =>
                {
                    b.Navigation("Variacoes");
                });

            modelBuilder.Entity("JvEstoque.Core.Models.VariacaoProduto", b =>
                {
                    b.Navigation("Estoque")
                        .IsRequired();

                    b.Navigation("ItensPedidos");
                });
#pragma warning restore 612, 618
        }
    }
}
