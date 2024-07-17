public record Puzzle
{
    public Board newboard { get; set; }
}
public record Board 
{
    public Grid[] grids { get; set; }
}

public record Grid 
{
    public int[][] value { get; set; }
    public int[][] solution { get; set; }
    public string difficulty { get; set; }
}