using Carta_Aleatoria_CP6.Controller;
using Carta_Aleatoria_CP6.Database;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Carta Random", Version = "v1" });
});

builder.Services.AddScoped<CartaDbContext>();

//HealthCheck
builder.Services.AddScoped<DatabaseHealthCheck>();
builder.Services.AddHealthChecks().AddCheck<DatabaseHealthCheck>("database");

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Endpoints
app.AddEndpointsCarta();
app.UseHealthChecks("/health");

app.Run();
