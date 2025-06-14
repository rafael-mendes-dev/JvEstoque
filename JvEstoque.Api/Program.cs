var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x=> x.CustomSchemaIds(type => type.FullName));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
