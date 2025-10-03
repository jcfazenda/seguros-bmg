using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Saga.Orquestrador.Api.Worker;
using Rabbit.Message.Api.Messaging;
using Infraestructure.Domain.Repository.Interface;
using Infraestructure.Domain.Repository.Queryable;
using Infraestructure.Context;
using Infraestructure.Context.Tenant;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Saga Orquestrador", Version = "v1" });
});

// Customer DbContext dinâmico (multi-tenant)
builder.Services.AddCustomerDbContext();

// Registrar Repositórios
builder.Services.AddScoped<IPropostaRepository, PropostaRepository>();

// Registrar Rabbit Publisher
builder.Services.AddSingleton<IRabbitPublisher, RabbitPublisher>();

// Registrar Workers
builder.Services.AddHostedService<PropostaSagaWorker>();

var app = builder.Build();

// Swagger UI no Dev
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Saga Orquestrador V1"));
}

app.MapControllers();
app.Run();
