using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Rabbit.Message.Api.Messaging;

var builder = WebApplication.CreateBuilder(args);

// Registra RabbitPublisher como singleton
builder.Services.AddSingleton<IRabbitPublisher, RabbitPublisher>();

// Outros serviços (EF, MediatR, AutoMapper) podem ser adicionados aqui
// builder.Services.AddDbContext<...>();
// builder.Services.AddMediatR(...);

var app = builder.Build();

// Apenas para teste
app.MapGet("/", () => "API funcionando!");

app.Run();
