using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text;
using System.Text.Json;

namespace GameOfLife.API.IntegrationTests.Features.GenerateNextGrid;

public class GameOfLifeControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public GameOfLifeControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task NextGeneration_ReturnsExpectedResult()
    {
        // Arrange
        var client = _factory.CreateClient();
        var initialGrid = new bool[][] { new[] { true, true }, new[] { true, false } };
        var content = new StringContent(JsonSerializer.Serialize(initialGrid), Encoding.UTF8, "application/json");

        // Act
        var response = await client.PostAsync("/gameoflife/nextgeneration", content);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseGrid = JsonSerializer.Deserialize<bool[][]>(await response.Content.ReadAsStringAsync());
        Assert.NotNull(responseGrid);
        Assert.True(responseGrid[1][1]);
    }

    [Fact]
    public async Task NextGeneration_OnGridEdge_ReturnsExpectedResult()
    {
        // Arrange
        var client = _factory.CreateClient();
        var initialGrid = new bool[][] { new[] { true, true }, new[] { true, false } };
        var content = new StringContent(JsonSerializer.Serialize(initialGrid), Encoding.UTF8, "application/json");

        // Act
        var response = await client.PostAsync("/gameoflife/nextgeneration", content);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseGrid = JsonSerializer.Deserialize<bool[][]>(await response.Content.ReadAsStringAsync());
        Assert.NotNull(responseGrid);
        Assert.True(responseGrid[1][1]); // Cell on grid edge becomes alive
    }
}