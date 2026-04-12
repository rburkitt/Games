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
            var emptyCell = Cells.FirstOrDefault(x => x.Value == null);
            if (emptyCell == null)
                return;

            var emptyIndex = Cells.IndexOf(emptyCell);
            if (emptyIndex == index)
                return;

            int clickedRow = index / 4;
            int clickedCol = index % 4;
            int emptyRow = emptyIndex / 4;
            int emptyCol = emptyIndex % 4;

            if (clickedRow == emptyRow)
            {
                // Same row — slide all tiles between clicked and empty horizontally
                int step = emptyIndex > index ? 1 : -1;
                for (int i = emptyIndex; i != index; i -= step)
                {
                    Cells[i] = Cells[i - step];
                }
                Cells[index] = emptyCell;
            }
            else if (clickedCol == emptyCol)
            {
                // Same column — slide all tiles between clicked and empty vertically
                int step = emptyIndex > index ? 4 : -4;
                for (int i = emptyIndex; i != index; i -= step)
                {
                    Cells[i] = Cells[i - step];
                }
                Cells[index] = emptyCell;
            }
        }

        // a function to check if the puzzle is solved
        public bool IsSolved()
        {
            // if the list of cells is equal to 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,null then the puzzle is solved
            bool retVal = Cells.Select(o => o.Value).SequenceEqual(new List<int?> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, null });
            if (retVal)
            {
                Cells.ForEach(x => x.Disabled = true);
            }
            return retVal;
        }
    }

    public class Cell
    {
        public bool Disabled { get; set; } = false;
        public int? Value { get; set; }
    }
}
