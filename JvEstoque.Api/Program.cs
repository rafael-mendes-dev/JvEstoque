using JvEstoque.Api;
using JvEstoque.Api.Common.Api;
using JvEstoque.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();
builder.AddSecurity();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddJsonOptions();
builder.AddServices();
builder.ConfigureRateLimiter();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.ConfigureDevEnvironment();

app.UseCors(ApiConfiguration.CorsPolicyName);
app.UseRateLimiter();
app.UseSecurity();
app.MapEndpoints();

app.Run();
