using System.Collections.ObjectModel;
using Microsoft.Extensions.Options;
using Rosier.Yosan.Data;
using Rosier.Yosan.Models;

var builder = WebApplication.CreateBuilder(args);

// Add db configuration
builder.Services.Configure<DataSettings>(builder.Configuration.GetSection("Database"));
builder.Services.AddScoped<BudgetRepository>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/dbsettings", (IOptions<DataSettings> options) =>
{
    return options.Value;
});

app.MapPost("/budget", async (BudgetEntry newEntry, BudgetRepository repository) =>
{
    newEntry = await repository.AddBudgetEntry(newEntry);
    return Results.Created($"/budget/entry/{newEntry.Id}", newEntry);
});

app.MapGet("/budget/{year}/{month}", async (int year, int month, BudgetRepository repository) =>
{
    var entries = await repository.GetBudgetOfMonth(year, month);
    return entries;
});

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
