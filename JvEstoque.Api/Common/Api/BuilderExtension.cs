using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using JvEstoque.Api.Data;
using JvEstoque.Api.Handlers;
using JvEstoque.Core;
using JvEstoque.Core.Handlers;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;

namespace JvEstoque.Api.Common.Api
{
    public static class BuilderExtension
    {
        public static void AddConfiguration(this WebApplicationBuilder builder)
        {
            Configuration.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }

        public static void AddDocumentation(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(x => { x.CustomSchemaIds(type => type.FullName); });
        }
        
        public static void AddJsonOptions(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<JsonOptions>(options =>
            {
                options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
        }

        public static void AddSecurity(this WebApplicationBuilder builder)
        {
            // Adicionar configuracoes de segurança, como autenticação e autorização
        }

        public static void AddDataContexts(this WebApplicationBuilder builder)
        {
            // Adicionar o DbContext e outras configurações de acesso a dados
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.ConnectionString));
        }

        public static void AddCrossOrigin(this WebApplicationBuilder builder)
        {
            // Configurar CORS para permitir requisições de diferentes origens
        }
        
        public static void AddServices(this WebApplicationBuilder builder)
        {
            // Registrar serviços de aplicação, como interfaces e implementações
            builder.Services.AddScoped<IVariacaoProdutoHandler, VariacaoProdutoHandler>();
            builder.Services.AddScoped<IPedidoHandler, PedidoHandler>();
            builder.Services.AddScoped<IProdutoHandler, ProdutoHandler>();
            builder.Services.AddScoped<IEscolaHandler, EscolaHandler>();
            builder.Services.AddScoped<IEstoqueHandler, EstoqueHandler>();
        }
    }
}