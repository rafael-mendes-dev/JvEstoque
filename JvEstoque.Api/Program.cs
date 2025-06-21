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

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapEndpoints();

app.Run();
