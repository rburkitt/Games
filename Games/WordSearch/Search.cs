namespace Games.WordSearch
{
    public class Search
    {
        public string[,] Puzzle;
        public List<string> Found;
        public string[,] Solution;

        public bool HidePuzzle { get; set; } = false;
        public bool HideSolution { get; set; } = true;
        public string[,] BgColor = new string[10, 10];
        public string[] Finds;
        public int Find = 0;

        public Search(List<string> theWords, int height = 10, int width = 10)
        {
            Random rnd = new();
            Found = new List<string>();

            string fill = "abcdefghijklmnopqrstuvwxyz";
            Puzzle = new string[height, width];
            Solution = new string[height, width];

            for (int r = 0; r < Puzzle.GetLength(0); r++)
            {
                for (int c = 0; c < Puzzle.GetLength(1); c++)
                {
                    Puzzle[r, c] = "*";
                    Solution[r, c] = "*";
                }
            }

            for (int k = 0; k < theWords.Count; k++)
            {
                int row = rnd.Next(0, height);
                int col = rnd.Next(0, width);
                int dir = rnd.Next(0, 3);
                int fwd = rnd.Next(0, 2);

                while (fwd == 0 && (row + theWords[k].Length > Puzzle.GetLength(0) || col + theWords[k].Length > Puzzle.GetLength(1)) || fwd == 1 && (row - theWords[k].Length < 0 || col - theWords[k].Length < 0))//check for out of bounds
                {
                    row = rnd.Next(0, height);
                    col = rnd.Next(0, width);
                }

                if (Check(fwd, dir, row, col, theWords[k]))
                {
                    Found.Add(theWords[k]);
                    for (int m = 0; m < theWords[k].Length; m++)//add each word's characters
                    {
                        Puzzle[row, col] = theWords[k].Substring(m, 1);
                        Solution[row, col] = theWords[k].Substring(m, 1);

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

            for (int r = 0; r < height; r++)//fill empty spots
            {
                for (int c = 0; c < width; c++)
                {
                    if (Puzzle[r, c].Equals("*"))
                    {
                        int spot = rnd.Next(0, 26);
                        Puzzle[r, c] = fill.Substring(spot, 1);
                    }
                }
            }

            HidePuzzle = false;
            HideSolution = true;

            for (int r = 0; r < BgColor.GetLength(0); r++)
            {
                for (int c = 0; c < BgColor.GetLength(1); c++)
                {
                    BgColor[r, c] = "00f";
                }
            }

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
                    for (int r = row; r < row + word.Length; r++)
                    {
                        if (!Puzzle[r, col].Equals("*"))
                        {
                            return false;
                        }
                    }
                }
                else if (dir == 1)
                {
                    for (int c = col; c < col + word.Length; c++)
                    {
                        if (!Puzzle[row, c].Equals("*"))
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    for (int r = row; r < row + word.Length; r++)
                    {
                        for (int c = col; c < col + word.Length; c++)
                        {
                            if (!Puzzle[r, c].Equals("*"))
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            else
            {
                if (dir == 0)
                {
                    for (int r = row; r >= row - word.Length; r--)
                    {
                        if (!Puzzle[r, col].Equals("*"))
                        {
                            return false;
                        }
                    }
                }
                else if (dir == 1)
                {
                    for (int c = col; c >= col - word.Length; c--)
                    {
                        if (!Puzzle[row, c].Equals("*"))
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    for (int r = row; r >= row - word.Length; r--)
                    {
                        for (int c = col; c >= col - word.Length; c--)
                        {
                            if (!Puzzle[r, c].Equals("*"))
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }
    }
}
