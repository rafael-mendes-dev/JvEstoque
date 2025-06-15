using JvEstoque.Api.Common.Api;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();
builder.AddSecurity();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
