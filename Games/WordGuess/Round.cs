namespace Games.WordGuess
{
    public class Round
    {
        public int Score { get; set; } = 0;
        public Letter[] Letters { get; } = new Letter[5] { new Letter(), new Letter(), new Letter(), new Letter(), new Letter() };
    }
}
