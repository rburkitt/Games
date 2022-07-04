using System;
using System.Collections.Generic;

namespace Games.Pages
{
    public class Word
    {
        public string Text { get; set; }

    }
    public class wordSearch
    {
        public string[,] puzzle;
        public List<string> found;
        public string[,] solution;

        public wordSearch(List<string> theWords, int height = 10, int width = 10)
        {
            Random rnd = new Random();
            found = new List<string>();

            string fill = "abcdefghijklmnopqrstuvwxyz";
            puzzle = new string[height, width];
            solution = new string[height, width];

            for (int r = 0; r < puzzle.GetLength(0); r++)
            {
                for (int c = 0; c < puzzle.GetLength(1); c++)
                {
                    puzzle[r, c] = "*";
                    solution[r, c] = "*";
                }
            }

            for (int k = 0; k < theWords.Count; k++)
            {
                int row = rnd.Next(0, height);
                int col = rnd.Next(0, width);
                int dir = rnd.Next(0, 3);
                int fwd = rnd.Next(0, 2);

                while (fwd == 0 && (row + theWords[k].Length > puzzle.GetLength(0) || col + theWords[k].Length > puzzle.GetLength(1)) || fwd == 1 && (row - theWords[k].Length < 0 || col - theWords[k].Length < 0))//check for out of bounds
                {
                    row = rnd.Next(0, height);
                    col = rnd.Next(0, width);
                }

                if (Check(fwd, dir, row, col, theWords[k]))
                {
                    found.Add(theWords[k]);
                    for (int m = 0; m < theWords[k].Length; m++)//add each word's characters
                    {
                        puzzle[row, col] = theWords[k].Substring(m, 1);
                        solution[row, col] = theWords[k].Substring(m, 1);

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
                    if (puzzle[r, c].Equals("*"))
                    {
                        int spot = rnd.Next(0, 26);
                        puzzle[r, c] = fill.Substring(spot, 1);
                    }
                }
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
                        if (!puzzle[r, col].Equals("*"))
                        {
                            return false;
                        }
                    }
                }
                else if (dir == 1)
                {
                    for (int c = col; c < col + word.Length; c++)
                    {
                        if (!puzzle[row, c].Equals("*"))
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
                            if (!puzzle[r, c].Equals("*"))
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
                        if (!puzzle[r, col].Equals("*"))
                        {
                            return false;
                        }
                    }
                }
                else if (dir == 1)
                {
                    for (int c = col; c >= col - word.Length; c--)
                    {
                        if (!puzzle[row, c].Equals("*"))
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
                            if (!puzzle[r, c].Equals("*"))
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
