using Microsoft.OpenApi.Models;
using Saga.Orquestrador.Api.Worker;
using Rabbit.Message.Api.Messaging;    

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Saga Orquestrador", Version = "v1" });
});

// Worker que escuta filas
builder.Services.AddHostedService<PropostaSagaWorker>();
builder.Services.AddSingleton<IRabbitPublisher, RabbitPublisher>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Saga Orquestrador V1"));
}

app.MapControllers();
app.Run();
