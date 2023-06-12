using GameOfLife.Services.Features.GenerateNextGrid;

namespace GameOfLife.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddScoped<GameOfLifeService>();

        var app = builder.Build();

        app.MapPost("/gameoflife/nextgeneration", (GameOfLifeService gameOfLifeService, bool[,] initialGrid) =>
        {
            try
            {
                var nextGeneration = gameOfLifeService.CalculateNextGeneration(initialGrid);
                return Results.Ok(nextGeneration);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        app.Run();
    }
}