using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JvEstoque.Api.Data;
using JvEstoque.Core;
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
        }
    }
}