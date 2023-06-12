# Game of Life API 🚀

This project is an implementation of [Conway's Game of Life](https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life) as a web API. The Game of Life is a cellular automaton devised by the British mathematician John Horton Conway in 1970. It's a zero-player game, meaning that its evolution is determined by its initial state, requiring no further input. 🎲

## Project Structure 🏗️

The project follows a "slice by feature" structure. Each feature of the application is contained within its own directory, which includes all the necessary components for that feature (such as services, controllers, and tests). This structure makes it easy to locate all the code related to a specific feature, and helps to keep the codebase clean and maintainable. 📁

## Test-Driven Development (TDD) 🧪

This project was developed using Test-Driven Development (TDD). For each feature, the development process was as follows:

1. Write a failing unit test for a single piece of functionality. ❌
2. Implement the simplest code that makes the test pass. ✅
3. Refactor the code to improve its structure and readability, while keeping the tests green. 🔄

This approach ensures that all code is covered by tests, and allows for safe refactoring. It also encourages small, incremental updates, which are easier to understand and debug than large changes.

## Game of Life Algorithm 🧠

The Game of Life is played on an infinite two-dimensional grid of square cells, each of which is in one of two possible states, live or dead. Every cell interacts with its eight neighbours, which are the cells that are horizontally, vertically, or diagonally adjacent. At each step in time, the following transitions occur:

1. Any live cell with fewer than two live neighbours dies, as if by underpopulation. 💀
2. Any live cell with two or three live neighbours lives on to the next generation. 🔄
3. Any live cell with more than three live neighbours dies, as if by overpopulation. 💀
4. Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction. 🐣

The initial pattern constitutes the seed of the system. The first generation is created by applying the above rules simultaneously to every cell in the seed; births and deaths occur simultaneously, and the discrete moment at which this happens is sometimes called a tick. Each generation is a pure function of the preceding one.

## Game of Life Algorithm Explained 🧠

The `GameOfLifeService` class contains the core logic for calculating the next generation of the Game of Life grid. Here's a step-by-step explanation of how the algorithm works:

### 1. Initialize the Next Generation Grid 🌱

First, we create a new 2D array `nextGeneration` with the same dimensions as the `initialGrid`. This will hold the state of the grid for the next generation.

```csharp
var nextGeneration = new bool[initialGrid.Length][];
```

### 2. Iterate Over Each Cell in the Grid 🔄

We then iterate over each cell in the `initialGrid`. For each cell, we count its live neighbours and determine whether it will be alive or dead in the next generation based on the rules of the Game of Life.

```csharp
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
```

### 3. Count Live Neighbours 👫

In the Game of Life, each cell in the grid has up to eight neighbours: the cells that are directly above, below, to the left, to the right, and the four cells that are diagonally adjacent. 

The `CountLiveNeighbours` method in the `GameOfLifeService` class is responsible for counting how many of a given cell's neighbours are alive. Here's the code for this method:

```csharp
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
```

This method takes three parameters: the current grid and the coordinates `(x, y)` of the cell for which we want to count live neighbours.

The method initializes a `count` variable to 0. This variable will keep track of the number of live neighbours.

The method then enters a nested loop that iterates over the cells in the grid that are in the vicinity of the cell at `(x, y)`. The outer loop iterates over the rows (i.e., the `x` coordinate), and the inner loop iterates over the columns (i.e., the `y` coordinate). 

The range of the loops is determined by `Math.Max(0, x - 1)` and `Math.Min(grid.Length - 1, x + 1)` for the `x` coordinate, and `Math.Max(0, y - 1)` and `Math.Min(grid[i].Length - 1, y + 1)` for the `y` coordinate. This ensures that the loops only iterate over the cells that are neighbours of the cell at `(x, y)`, including the cell itself. It also ensures that the loops do not attempt to access cells that are outside the bounds of the grid, which would cause an error.

Inside the inner loop, the method checks if the current cell `(i, j)` is not the cell `(x, y)` itself. If it's not, and the cell is alive (i.e., `grid[i][j]` is `true`), the method increments the `count` by 1.

Finally, after the loops have finished, the method returns the `count`, which is the number of live neighbours of the cell at `(x, y)`.

This method effectively counts the number of live neighbours for a given cell, taking into account the boundaries of the grid and ignoring the state of the cell itself.

### 4. Apply the Rules of the Game of Life 🎲

The state of each cell in the next generation is determined by the following rules:

- Any live cell with fewer than two live neighbours dies, as if by underpopulation.
- Any live cell with two or three live neighbours lives on to the next generation.
- Any live cell with more than three live neighbours dies, as if by overpopulation.
- Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.

These rules are applied in the following lines of code:

```csharp
if (initialGrid[i][j])
{
    nextGeneration[i][j] = liveNeighbours == 2 || liveNeighbours == 3;
}
else
{
    nextGeneration[i][j] = liveNeighbours == 3;
}
```

### 5. Return the Next Generation Grid 🎉

Finally, the `CalculateNextGeneration` method returns the `nextGeneration` grid, which represents the state of the Game of Life grid in the next generation.

```csharp
return nextGeneration;
```

And that's it! This algorithm effectively calculates the next generation of the Game of Life grid based on the rules of the game. 🚀

## Running the Project 🏃

To run the project, you will need to have .NET 7.0 or later installed. You can then use the `dotnet run` command from the root directory of the project. To run the tests, use the `dotnet test` command.

## API Usage 🌐

The API has a single endpoint, `/gameoflife/nextgeneration`, which accepts a POST request with a JSON body representing the initial state of the Game of Life grid. The grid should be a 2D array of booleans, where `true` represents a live cell and `false` represents a dead cell. The response from the API is a JSON body representing the next generation of the Game of Life grid, following the rules described above.



