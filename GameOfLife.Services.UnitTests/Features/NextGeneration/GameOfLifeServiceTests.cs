﻿using GameOfLife.Services.Features.GenerateNextGrid;

namespace GameOfLife.API.UnitTests.Features.GenerateNextGrid;

public class GameOfLifeServiceTests
{
    private GameOfLifeService _gameOfLifeService;

    public GameOfLifeServiceTests()
    {
        _gameOfLifeService = new GameOfLifeService();
    }

    [Fact]
    public void LiveCell_WithFewerThanTwoLiveNeighbours_Dies()
    {
        // Arrange
        var initialGrid = new bool[][] { new[] { true, false }, new[] { false, false } };

        // Act
        var nextGeneration = _gameOfLifeService.CalculateNextGeneration(initialGrid);

        // Assert
        Assert.False(nextGeneration[0][0]);
    }

    [Fact]
    public void LiveCell_WithMoreThanThreeLiveNeighbours_Dies()
    {
        // Arrange
        var initialGrid = new bool[][] { new[] { true, true, true }, new[] { true, true, false }, new[] { false, false, false } };

        // Act
        var nextGeneration = _gameOfLifeService.CalculateNextGeneration(initialGrid);

        // Assert
        Assert.False(nextGeneration[1][1]);
    }

    [Fact]
    public void LiveCell_WithTwoOrThreeLiveNeighbours_Lives()
    {
        // Arrange
        var initialGrid = new bool[][] { new[] { true, true }, new[] { true, false } };

        // Act
        var nextGeneration = _gameOfLifeService.CalculateNextGeneration(initialGrid);

        // Assert
        Assert.True(nextGeneration[0][0]);
    }

    [Fact]
    public void DeadCell_WithExactlyThreeLiveNeighbours_BecomesAlive()
    {
        // Arrange
        var initialGrid = new bool[][] { new[] { true, true }, new[] { true, false } };

        // Act
        var nextGeneration = _gameOfLifeService.CalculateNextGeneration(initialGrid);

        // Assert
        Assert.True(nextGeneration[1][1]);
    }

    // Edge Cases

    [Fact]
    public void LiveCell_OnGridEdgeWithFewerThanTwoLiveNeighbours_Dies()
    {
        // Arrange
        var initialGrid = new bool[][] { new[] { true, false }, new[] { false, false } };

        // Act
        var nextGeneration = _gameOfLifeService.CalculateNextGeneration(initialGrid);

        // Assert
        Assert.False(nextGeneration[0][0]);
    }

    [Fact]
    public void DeadCell_OnGridEdgeWithExactlyThreeLiveNeighbours_BecomesAlive()
    {
        // Arrange
        var initialGrid = new bool[][] { new[] { true, true }, new[] { true, false } };

        // Act
        var nextGeneration = _gameOfLifeService.CalculateNextGeneration(initialGrid);

        // Assert
        Assert.True(nextGeneration[1][1]);
    }
}