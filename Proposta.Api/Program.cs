using Microsoft.EntityFrameworkCore;
using MediatR;
using AutoMapper;
using Infraestructure.Domain.Mapping;
using Infraestructure.Domain.Services.Commands;

using Infraestructure.Domain.Services.Handles;
using Infraestructure.Context;
using Infraestructure.Domain.Repository.Interface;
using Infrastructure.Domain.Repository.Queryable;
using Infraestructure.Context.Tenant;
using Microsoft.OpenApi.Models;
using Rabbit.Message.Api.Messaging;  

var builder = WebApplication.CreateBuilder(args);

// HttpContextAccessor -> obrigatório pro multi-tenant
builder.Services.AddHttpContextAccessor();

// Controllers + JSON configurado
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.WriteIndented = true;
    });

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Proposta API",
        Version = "v1"
    });
});

// Customer DbContext dinâmico (multi-tenant)
builder.Services.AddCustomerDbContext();

// Repository
builder.Services.AddScoped<IPropostaRepository, PropostaRepository>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(PropostaMap).Assembly);

// RabbitMQ publisher
builder.Services.AddSingleton<IRabbitPublisher, RabbitPublisher>();

// MediatR - registra todos os handlers do assembly
builder.Services.AddMediatR(typeof(CreatePropostaHandler).Assembly);


var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Proposta API V1");
    });
}

app.UseHttpsRedirection();

// Necessário usar routing antes de controllers
app.UseRouting();

app.UseAuthorization();

// Map Controllers
app.MapControllers();

app.Run();
