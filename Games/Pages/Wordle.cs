namespace Games.Pages
{
    public class Wordle
    {
        public string Word { get; set; } = "";
        public int Turn { get; set; } = 0;
        public string GuessWord { get; set; } = "";
        public bool Finished { get; set; } = false;

        public List<Round> Rounds = new List<Round>() { new Round(), new Round(), new Round(), new Round(), new Round(), new Round() };

        public Wordle()
        {
            Turn = 0;
        }

        public Wordle(string[] words)
        {
            Random rnd = new Random();
            Word = words[rnd.Next(words.Length)].Trim().ToUpper();
            Turn = 0;
        }

        public void Guess()
        {
            if (Turn >= Rounds.Count) return;

            var test = Word.ToCharArray().Select(c => c.ToString()).ToArray();

            var currentRound = Rounds.Skip(Turn).First();

            if (currentRound.Letters.Any(o => o.Text == ""))
                throw new Exception("Your guess must be 5 letters.");

            var wordArray = Word.ToCharArray().Select(c => c.ToString()).ToArray();

            for (int i = 0; i < currentRound.Letters.Length; i++)
            {
                if (Word.Contains(currentRound.Letters[i].Text))
                {
                    if (currentRound.Letters[i].Text == wordArray[i])
                    {
                        currentRound.Letters[i].Location = Location.Yes;
                        currentRound.Score++;
                    }
                    else
                    {
                        currentRound.Letters[i].Location = Location.Contains;
                    }
                }
                else
                {
                    currentRound.Letters[i].Location = Location.No;
                }
            }

            for (int i = 0; i < currentRound.Letters.Length; i++)
            {
                var wordCount = wordArray.Count(o => o == currentRound.Letters[i].Text);
                var guessCount = currentRound.Letters.Count(o => o.Text == currentRound.Letters[i].Text);
                var foundCount = currentRound.Letters.Count(t => t.Text == currentRound.Letters[i].Text && t.Location == Location.Yes);

                if (guessCount > 1)
                {
                    if (Word.Contains(currentRound.Letters[i].Text))
                    {
                        if (currentRound.Letters[i].Text == wordArray[i])
                        {
                            currentRound.Letters[i].Location = Location.Yes;
                        }
                        else
                        {
                            if (wordCount == foundCount)
                            {
                                currentRound.Letters[i].Location = Location.No;
                            }
                            else
                            {
                                currentRound.Letters[i].Location = Location.Contains;
                            }
                        }
                    }
                    else
                    {
                        currentRound.Letters[i].Location = Location.No;
                    }
                }
            }

            GuessWord = "";

            Finished = Turn >= Rounds.Count - 1 || Rounds[Turn].Score == 5;

            // increase turn
            Turn++;
        }

        public void SetLetter(int index, string letter)
        {
            var currentRound = Rounds.Skip(Turn).First();
            currentRound.Letters[index].Text = letter.ToUpper();
        }

        public void DeleteLetter(int index)
        {
            var currentRound = Rounds.Skip(Turn).First();
            currentRound.Letters[index].Text = "";
        }
    }
}
