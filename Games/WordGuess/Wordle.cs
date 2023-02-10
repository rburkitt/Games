namespace Games.WordGuess
{
    public class Wordle
    {
        public string[] Words { get; set; }
        public string Word { get; set; }
        public int Turn { get; set; }
        public string GuessWord { get; set; }
        public bool Finished { get; set; }

        public List<Round> Rounds { get; set; }

        public string Msg { get; set; }
        public int Clicks { get; set; }
        public List<string> Guesses { get; set; }
        public bool Update { get; set; }

        public Wordle()
        {
            Word = "";
            Turn = 0;
            GuessWord = "";
            Finished = false;
            Rounds = new() { new Round(), new Round(), new Round(), new Round(), new Round(), new Round() };
            Msg = "";
            Clicks = 0;
            Guesses = new();
            Update = false;
        }

        public Wordle(string word, int turn, string guessWord, bool finished, List<Round> rounds, string msg, int clicks, List<string> guesses, bool update)
        {
            Word = word;
            Turn = turn;
            GuessWord = guessWord;
            Finished = finished;
            Rounds = rounds;
            Msg = msg;
            Clicks = clicks;
            Guesses = guesses;
            Update = update;
        }

        public void Setup(string[] words)
        {
            Words = words;
            Random rnd = new();
            Word = words[rnd.Next(words.Length)].Trim().ToUpper();
            Turn = 0;
            Msg = "";
            Clicks = 0;
            Guesses = new List<string>();
            Update = false;
        }

        public void Guess()
        {
            if (Turn >= Rounds.Count) return;

            var test = Word.ToCharArray().Select(c => c.ToString()).ToArray();

            var currentRound = Rounds.Skip(Turn).First();

            if (currentRound.Letters.Any(o => o.Text == ""))
                throw new Exception("Your guess must be 5 letters.");

            string myGuess = string.Join("", currentRound.Letters.Select(o => o.Text).ToArray());

            if(!Words.Contains(myGuess.ToLower()))
                throw new Exception("Invalid Word, not in word list.");

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
