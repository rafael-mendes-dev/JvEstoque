using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;
using System.Threading.Tasks;
using JvEstoque.Api.Data;
using JvEstoque.Api.Handlers;
using JvEstoque.Api.Models;
using JvEstoque.Core;
using JvEstoque.Core.Handlers;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

namespace JvEstoque.Api.Common.Api
{
    public static class BuilderExtension
    {
        public static void AddConfiguration(this WebApplicationBuilder builder)
        {
            Configuration.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
            Configuration.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;
            Configuration.FrontendUrl = builder.Configuration.GetValue<string>("FrontendUrl") ?? string.Empty;
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
            builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme).AddIdentityCookies();
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.SameSite = SameSiteMode.None; // Permite cross-site
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Sempre envia em HTTPS
            });
            builder.Services.AddAuthorization();
        }

        public static void AddDataContexts(this WebApplicationBuilder builder)
        {
            // Adicionar o DbContext e outras configurações de acesso a dados
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.ConnectionString));
            builder.Services.AddIdentityCore<User>().AddRoles<IdentityRole<int>>().AddEntityFrameworkStores<AppDbContext>().AddApiEndpoints();
        }

        public static void AddCrossOrigin(this WebApplicationBuilder builder)
        {
            // Configurar CORS para permitir requisições de diferentes origens
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(ApiConfiguration.CorsPolicyName,builder =>
                {
                    builder.WithOrigins([Configuration.BackendUrl, Configuration.FrontendUrl])
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                });
            });
        }
        
        public static void AddServices(this WebApplicationBuilder builder)
        {
            // Registrar serviços de aplicação, como interfaces e implementações
            builder.Services.AddScoped<IVariacaoProdutoHandler, VariacaoProdutoHandler>();
            builder.Services.AddScoped<IPedidoHandler, PedidoHandler>();
            builder.Services.AddScoped<IProdutoHandler, ProdutoHandler>();
            builder.Services.AddScoped<IEscolaHandler, EscolaHandler>();
            builder.Services.AddScoped<IEstoqueHandler, EstoqueHandler>();
            builder.Services.AddScoped<IReportHandler, ReportHandler>();
        }

        public static void ConfigureRateLimiter(this WebApplicationBuilder builder)
        {
            builder.Services.AddRateLimiter(options =>
            {
                options.AddFixedWindowLimiter(policyName: "fixed", limiterOptions =>
                {
                    limiterOptions.PermitLimit = 100; 
                    limiterOptions.Window = TimeSpan.FromMinutes(1); 
                    limiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    limiterOptions.QueueLimit = 5; 
                });
                
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            });
        }
    }
}