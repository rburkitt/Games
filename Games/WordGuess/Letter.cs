namespace Games.WordGuess
{
    public enum Location
    {
        Yes = 0,
        No = 1,
        Contains = 2,
        Empty = 3
    }

    public class Letter
    {
        public string Text { get; set; } = "";
        public Location Location { get; set; } = Location.Empty;
    }
}
