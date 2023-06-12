using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Text;

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
        var initialGrid = new[,] { { true, true }, { true, false } };
        var content = new StringContent(JsonConvert.SerializeObject(initialGrid), Encoding.UTF8, "application/json");

        // Act
        var response = await client.PostAsync("/gameoflife/nextgeneration", content);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseGrid = JsonConvert.DeserializeObject<bool[,]>(await response.Content.ReadAsStringAsync());
        Assert.True(responseGrid[1, 1]);
    }

    [Fact]
    public async Task NextGeneration_OnGridEdge_ReturnsExpectedResult()
    {
        // Arrange
        var client = _factory.CreateClient();
        var initialGrid = new[,] { { true, true }, { true, false } };
        var content = new StringContent(JsonConvert.SerializeObject(initialGrid), Encoding.UTF8, "application/json");

        // Act
        var response = await client.PostAsync("/gameoflife/nextgeneration", content);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseGrid = JsonConvert.DeserializeObject<bool[,]>(await response.Content.ReadAsStringAsync());
        Assert.True(responseGrid[1, 1]); // Cell on grid edge becomes alive
    }

    [Fact]
    public async Task NextGeneration_WithInvalidInput_ReturnsBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();
        var invalidJson = "[[true, true], [true]]"; // Malformed JSON representing an invalid grid

        var content = new StringContent(invalidJson, Encoding.UTF8, "application/json");

        // Act
        var response = await client.PostAsync("/gameoflife/nextgeneration", content);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}