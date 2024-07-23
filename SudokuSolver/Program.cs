using System.Net;
using System.Text.Json;

namespace SudokuSolver;

public static class Program
{
    readonly static HttpClient http = new HttpClient();

    public static async Task Main()
    {
        var cancel = new CancellationToken();
        Puzzle puzzle;

        var exampleFilename = "Example1.json";
        if (!File.Exists(exampleFilename))
        {
            puzzle = await GetNewPuzzle(cancel);
            await SavePuzzle(puzzle, exampleFilename);
        }
        else
        {
            puzzle = await LoadPuzzle(exampleFilename, cancel);
        }

        var solver = new Solver();
        solver.Solve(puzzle.newboard.grids[0]);

        PrintGrid(puzzle.newboard.grids[0]);
    }

    private static void PrintGrid(Grid grid)
    {
        foreach (var row in grid.value)
        {
            Console.WriteLine(string.Join(" | ", row));
        }
    }

    private static async Task SavePuzzle(Puzzle puzzle, string filename)
    {
        var json = JsonSerializer.Serialize<Puzzle>(puzzle, new JsonSerializerOptions() { WriteIndented = true });
        await File.WriteAllTextAsync(filename, json); 
    }

    private static async Task<Puzzle> LoadPuzzle(string filename, CancellationToken cancel)
    {
        using var json = new StreamReader(filename);
        var puzzle = await JsonSerializer.DeserializeAsync<Puzzle>(json.BaseStream, cancellationToken: cancel);
        return puzzle;
    }

    private static async Task<Puzzle> GetNewPuzzle(CancellationToken cancel)
    {
        var response = await http.GetAsync("https://sudoku-api.vercel.app/api/dosuku", cancel);
        var json = await response.Content.ReadAsStreamAsync(cancel);
        var puzzle = await JsonSerializer.DeserializeAsync<Puzzle>(json, cancellationToken: cancel);
        return puzzle;
    }
}

