using System.Text.Json.Serialization;

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

        [JsonIgnore]
        public Dictionary<(int, int), Coordinate> PuzzleIndex { get; private set; } = [];
        [JsonIgnore]
        public Dictionary<(int, int), Coordinate> SolutionIndex { get; private set; } = [];
        [JsonIgnore]
        public Dictionary<(int, int), Coordinate> BgColorIndex { get; private set; } = [];

        public void BuildIndex()
        {
            PuzzleIndex = Puzzle.ToDictionary(o => (o.X, o.Y));
            SolutionIndex = Solution.ToDictionary(o => (o.X, o.Y));
            BgColorIndex = BgColor.ToDictionary(o => (o.X, o.Y));
        }

        public bool HidePuzzle { get; set; } = false;
        public bool HideSolution { get; set; } = true;
        public List<Coordinate> BgColor { get; set; } = [];
        public string[] Finds { get; set; }
        public int Find { get; set; } = 0;
        public List<string> TheWords { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public Search()
        {
            Found = [];
            Finds = new string[Found.Count];
            Puzzle = [];
            Solution = [];
            TheWords = [];
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
            Found = [];

            string fill = "abcdefghijklmnopqrstuvwxyz";
            Puzzle = [];
            Solution = [];

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
                int row = 0, col = 0, dir = 0, fwd = 0;
                bool placed = false;

                for (int attempt = 0; attempt < 100; attempt++)
                {
                    row = rnd.Next(0, Height);
                    col = rnd.Next(0, Width);
                    dir = rnd.Next(0, 3);
                    fwd = rnd.Next(0, 2);

                    bool inBounds = dir switch
                    {
                        0 => fwd == 0 ? row + theWords[k].Length <= Height : row - theWords[k].Length >= 0,
                        1 => fwd == 0 ? col + theWords[k].Length <= Width  : col - theWords[k].Length >= 0,
                        _ => fwd == 0 ? row + theWords[k].Length <= Height && col + theWords[k].Length <= Width
                                      : row - theWords[k].Length >= 0      && col - theWords[k].Length >= 0
                    };

                    if (inBounds && Check(fwd, dir, row, col, TheWords[k]))
                    {
                        placed = true;
                        break;
                    }
                }

                if (placed)
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

            _ = Puzzle.Where(o => o.Value == "*").Select(o => o.Value = fill.Substring(rnd.Next(0, 26), 1)).ToList();

            HidePuzzle = false;
            HideSolution = true;

            Finds = new string[Found.Count];
            for (int i = 0; i < Found.Count; i++)
            {
                Finds[i] = "";
            }

            BuildIndex();
        }

        public static void Increment(int fwd, ref int val)
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
                        if (item != null && !item.Value.Equals("*") && !item.Value.Equals(letter.ToString()))
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
                        if (item != null && !item.Value.Equals("*") && !item.Value.Equals(letter.ToString()))
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
                        if (item != null && !item.Value.Equals("*") && !item.Value.Equals(letter.ToString()))
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
                        if (item != null && !item.Value.Equals("*") && !item.Value.Equals(letter.ToString()))
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
                        if (item != null && !item.Value.Equals("*") && !item.Value.Equals(letter.ToString()))
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
                        if (item != null && !item.Value.Equals("*") && !item.Value.Equals(letter.ToString()))
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
