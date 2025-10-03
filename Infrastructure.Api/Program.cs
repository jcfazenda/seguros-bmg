using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Rabbit.Message.Api.Messaging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IRabbitPublisher, RabbitPublisher>();

// Outros serviços (EF, MediatR, AutoMapper) podem ser adicionados aqui 

var app = builder.Build();

// Apenas para teste
app.MapGet("/", () => "API funcionando!");

app.Run();
