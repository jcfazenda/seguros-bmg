using Microsoft.OpenApi.Models;
using Saga.Orquestrador.Api.Worker;
using Rabbit.Message.Api.Messaging; 

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Saga Orquestrador", Version = "v1" });
});  
 

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
