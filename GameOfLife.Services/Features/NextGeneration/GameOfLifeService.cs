namespace GameOfLife.Services.Features.GenerateNextGrid;

public class GameOfLifeService
{
    public bool[][] CalculateNextGeneration(bool[][] initialGrid)
    {
        var nextGeneration = new bool[initialGrid.Length][];

        for (int i = 0; i < initialGrid.Length; i++)
        {
            nextGeneration[i] = new bool[initialGrid[i].Length];
            for (int j = 0; j < initialGrid[i].Length; j++)
            {
                var liveNeighbours = CountLiveNeighbours(initialGrid, i, j);
                if (initialGrid[i][j])
                {
                    nextGeneration[i][j] = liveNeighbours == 2 || liveNeighbours == 3;
                }
                else
                {
                    nextGeneration[i][j] = liveNeighbours == 3;
                }
            }
        }

        return nextGeneration;
    }

    private int CountLiveNeighbours(bool[][] grid, int x, int y)
    {
        int count = 0;
        for (int i = Math.Max(0, x - 1); i <= Math.Min(grid.Length - 1, x + 1); i++)
        {
            for (int j = Math.Max(0, y - 1); j <= Math.Min(grid[i].Length - 1, y + 1); j++)
            {
                if (i != x || j != y)
                {
                    count += grid[i][j] ? 1 : 0;
                }
            }
        }
        return count;
    }
}