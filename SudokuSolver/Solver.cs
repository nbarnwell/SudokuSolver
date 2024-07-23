
public class Solver
{
    private const int GridColumns = 9;
    private const int GridRows = 9;
    private const int RegionSize = 3;

    public void Solve(Grid puzzle)
    {
        bool finished = false;
        while (!finished)
        {
            var emptyCells = GetEmptyCells(puzzle);
            var madeChanges = false;
            foreach (var cell in emptyCells)
            {
                var possiblities =
                    Enumerable.Range(1, 9).Except(
                        GetRowValues(puzzle, cell.Row).Concat(GetColumnValues(puzzle, cell.Column))
                                                      .Concat(GetRegionValues(puzzle, cell)));
                if (possiblities.Count() == 1) 
                {
                    puzzle.value[cell.Row][cell.Column] = possiblities.Single();
                    madeChanges = true;
                }
            }

            if (!madeChanges || !GetEmptyCells(puzzle).Any())
            {
                finished = true;
            }
        }
    }

    private IEnumerable<int> GetRegionValues(Grid grid, Cell cell)
    {
        var topLeftCol = cell.Column / RegionSize;
        var topLeftRow = cell.Row / RegionSize;

        for (int row = topLeftRow; row < topLeftRow + RegionSize; row++)
        {
            for (int column = 0; column < topLeftCol + RegionSize; column++)
            {
                int value = grid.value[row][column];
                if (value != 0) {
                    yield return value;
                }
            }
        }
    }

    private IEnumerable<int> GetRowValues(Grid grid, int row)
    {
        return grid.value[row].Where(x => x != 0);
    }

    private IEnumerable<int> GetColumnValues(Grid grid, int column)
    {
        for (int row = 0; row < GridRows; row++)
        {
            int value = grid.value[column][row];
            if (value != 0)
            {
                yield return value;
            }
        }
    }

    private IEnumerable<Cell> GetEmptyCells(Grid grid)
    {
        for (var column = 0; column < GridColumns; column++)
        {
            for (int row = 0; row < GridRows; row++)
            {
                if (grid.value[row][column] == 0)
                {
                    yield return new Cell(grid.value[row][column], column, row);
                }
            }
        }
    }

    private record Cell(int Value, int Column, int Row);
}