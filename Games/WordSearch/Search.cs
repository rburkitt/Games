namespace Games.WordSearch
{
    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public string Value { get; set; } = "";
    }
    public class Search
    {
        public List<Coordinate> Puzzle { get; set; }
        public List<string> Found { get; set; }
        public List<Coordinate> Solution { get; set; }

        public bool HidePuzzle { get; set; } = false;
        public bool HideSolution { get; set; } = true;
        public List<Coordinate> BgColor { get; set; } = new();
        public string[] Finds { get; set; }
        public int Find { get; set; } = 0;
        public List<string> TheWords { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public Search()
        {
            Found = new List<string>();
            Finds = new string[Found.Count];
            Puzzle = new List<Coordinate>();
            Solution = new List<Coordinate>();
            TheWords = new List<string>();
        }
        public Search(List<Coordinate> puzzle, List<string> found, List<Coordinate> solution, bool hidePuzzle, bool hideSolution, List<Coordinate> bgColor, string[] finds, int find, List<string> theWords, int height, int width)
        {
            Puzzle = puzzle;
            Found = found;
            Solution = solution;
            HidePuzzle = hidePuzzle;
            HideSolution = hideSolution;
            BgColor = bgColor;
            Finds = finds;
            Find = find;
            TheWords = theWords;
            Height = height;
            Width = width;

            Setup(theWords, height, width);
        }

        public void Setup(List<string> theWords, int height = 10, int width = 10)
        {
            TheWords = theWords;
            Height = height;
            Width = width;

            Random rnd = new();
            Found = new List<string>();

            string fill = "abcdefghijklmnopqrstuvwxyz";
            Puzzle = new List<Coordinate>();
            Solution = new List<Coordinate>();

            for (int r = 0; r < Height; r++)
            {
                for (int c = 0; c < Width; c++)
                {
                    Puzzle.Add(new Coordinate { X = r, Y = c, Value = "*" });
                    Solution.Add(new Coordinate { X = r, Y = c, Value = "*" });
                    BgColor.Add(new Coordinate { X = r, Y = c, Value = "00f" });
                }
            }

            for (int k = 0; k < TheWords.Count; k++)
            {
                int row = rnd.Next(0, Height);
                int col = rnd.Next(0, Width);
                int dir = rnd.Next(0, 3);
                int fwd = rnd.Next(0, 2);

                while (fwd == 0 && (row + theWords[k].Length > Height || col + theWords[k].Length > Height) || fwd == 1 && (row - theWords[k].Length < 0 || col - theWords[k].Length < 0))//check for out of bounds
                {
                    row = rnd.Next(0, Height);
                    col = rnd.Next(0, Width);
                }

                if (Check(fwd, dir, row, col, TheWords[k]))
                {
                    Found.Add(TheWords[k]);
                    for (int m = 0; m < TheWords[k].Length; m++)//add each word's characters
                    {
                        Puzzle.First(o => o.X == row && o.Y == col).Value = TheWords[k].Substring(m, 1);
                        Solution.First(o => o.X == row && o.Y == col).Value = TheWords[k].Substring(m, 1);

                        if (dir == 0)
                            Increment(fwd, ref row);
                        else if (dir == 1)
                            Increment(fwd, ref col);
                        else
                        {
                            Increment(fwd, ref row);
                            Increment(fwd, ref col);
                        }
                    }
                }
            }

            Puzzle.Where(o => o.Value == "*").Select(o => o.Value = fill.Substring(rnd.Next(0, 26), 1)).ToList();

            HidePuzzle = false;
            HideSolution = true;

            Finds = new string[Found.Count];
            for (int i = 0; i < Found.Count; i++)
            {
                Finds[i] = "";
            }
        }

        public void Increment(int fwd, ref int val)
        {
            if (fwd == 0)
                val++;
            else
                val--;
        }

        public bool Check(int fwd, int dir, int row, int col, string word)
        {
            if (fwd == 0)
            {
                if (dir == 0)
                {
                    int r = row;
                    foreach (var letter in word)
                    {
                        var item = Puzzle.FirstOrDefault(o => o.X == r && o.Y == col);
                        if (item != null && !item.Value.Equals("*") && !item.Value.Equals(letter))
                        {
                            return false;
                        }
                        r++;
                    }
                }
                else if (dir == 1)
                {
                    int c = col;
                    foreach (var letter in word)
                    {
                        var item = Puzzle.FirstOrDefault(o => o.X == row && o.Y == c);
                        if (item != null && !item.Value.Equals("*") && !item.Value.Equals(letter))
                        {
                            return false;
                        }
                        c++;
                    }
                }
                else
                {
                    int r = row;
                    int c = col;
                    foreach (var letter in word)
                    {
                        var item = Puzzle.FirstOrDefault(o => o.X == r && o.Y == c);
                        if (item != null && !item.Value.Equals("*") && !item.Value.Equals(letter))
                        {
                            return false;
                        }
                        r++;
                        c++;
                    }
                }
            }
            else
            {
                if (dir == 0)
                {
                    int r = row;
                    foreach (var letter in word)
                    {
                        var item = Puzzle.FirstOrDefault(o => o.X == r && o.Y == col);
                        if (item != null && !item.Value.Equals("*") && !item.Value.Equals(letter))
                        {
                            return false;
                        }
                        r--;
                    }
                }
                else if (dir == 1)
                {
                    int c = col;
                    foreach (var letter in word)
                    {
                        var item = Puzzle.FirstOrDefault(o => o.X == row && o.Y == c);
                        if (item != null && !item.Value.Equals("*") && !item.Value.Equals(letter))
                        {
                            return false;
                        }
                        c--;
                    }
                }
                else
                {
                    int r = row;
                    int c = col;
                    foreach (var letter in word)
                    {
                        var item = Puzzle.FirstOrDefault(o => o.X == r && o.Y == c);
                        if (item != null && !item.Value.Equals("*") && !item.Value.Equals(letter))
                        {
                            return false;
                        }
                        r--;
                        c--;
                    }
                }
            }

            return true;
        }
    }
}
