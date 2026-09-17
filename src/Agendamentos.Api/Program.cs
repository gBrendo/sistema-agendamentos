using Agendamentos.Api.DTOs;
using Agendamentos.Application.Interfaces;
using Agendamentos.Application.Services;
using Agendamentos.Infrastructure.Data;
using Agendamentos.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddDbContext<AgendamentosDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IClienteRepository, ClienteRepositorio>();
builder.Services.AddScoped<IProfissionalRepository, ProfissionalRepositorio>();
builder.Services.AddScoped<IAgendamentoRepository, AgendamentoRepositorio>();

builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<ProfissionalService>();
builder.Services.AddScoped<AgendamentoService>();


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapPost("/clientes", async (CriarClienteRequest request, ClienteService clienteService) =>
{
    try
    {
        await clienteService.CriarClienteAsync(request.Nome, request.Email);
        return Results.Created();
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
