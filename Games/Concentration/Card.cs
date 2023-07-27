namespace Games.Concentration
{
    public class Card
    {
        public string Value { get; set; } = null!;
        public string Text { get; set; } = null!;
        public bool IsEnabled { get; set; }

        public Card() 
        { 
            Value = null!;
            Text = null!;
            IsEnabled = false;
        }

        public Card(string value, string text, bool isEnabled)
        {
            Value = value;
            Text = text;
            IsEnabled = isEnabled;
        }
    }
}
