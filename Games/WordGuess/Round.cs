namespace Games.WordGuess
{
    public class Round
    {
        public int Score { get; set; }
        public Letter[] Letters { get; set; }

        public Round()
        {
            Score = 0;
            Letters = new Letter[5] { new Letter(), new Letter(), new Letter(), new Letter(), new Letter() };
        }
        public Round(int score, Letter[] letters)
        {
            Score = score;
            Letters = letters;
        }
    }
}
