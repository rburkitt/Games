namespace Games.SliderPuzzle
{
    public class Game
    {
        public List<Cell> Cells { get; set; } = new List<Cell>();

        public Game()
        {
            Cells = GetNewBoard();
            Shuffle();
        }

        public List<Cell> GetNewBoard()
        {
            Cells = Enumerable.Range(1, 15).Select(x => new Cell { Value = x }).ToList();
            Cells.Add(new Cell());
            return Cells;
        }

        public void Shuffle()
        {
            var random = new Random();
            Cells = Cells.OrderBy(x => random.Next()).ToList();
        }

        public void MoveCell(int index)
        {
            var cell = Cells[index];
            var emptyCell = Cells.FirstOrDefault(x => x.Value == null);
            if (emptyCell == null)
                return;

            var emptyCellIndex = Cells.IndexOf(emptyCell);
            // emptycell must be adjacent to cell
            if (emptyCellIndex != index - 1 && emptyCellIndex != index + 1 && emptyCellIndex != index - 4 && emptyCellIndex != index + 4)
                return;

            Cells[index] = emptyCell;
            Cells[emptyCellIndex] = cell;            
        }
    }

    public class Cell
    {
        public int? Value { get; set; }
    }
}
