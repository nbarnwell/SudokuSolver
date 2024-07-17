using System.Text.Json;

var http = new HttpClient();
var response = await http.GetAsync("https://sudoku-api.vercel.app/api/dosuku");
var json = await response.Content.ReadAsStringAsync();
var puzzle = JsonSerializer.Deserialize<Puzzle>(json);


System.Console.WriteLine("Output:");
System.Console.WriteLine(json);
Console.WriteLine(puzzle.newboard.grids[0].value[0][0]);