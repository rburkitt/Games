namespace Games.Concentration
{
    public class Pexeso
    {
        public List<Card> Cards = new();

        public List<string> faces = new() { "1", "2", "3", "4", "5", "6", "7", "8" };

        public Card? Card1 { get; set; } = null!;
        public Card? Card2 { get; set; } = null!;

        public int Flipped = 0;

        public Pexeso()
        {
            faces.AddRange(faces);
        }

        public void DealCards()
        {
            Card1 = null;
            Card2 = null;
            Cards.Clear();
            faces.OrderBy(a => Guid.NewGuid()).ToList().ForEach(o =>
            {
                Cards.Add(new Card
                {
                    Value = o,
                    Text = "",
                    IsEnabled = false
                });
            });
            Flipped = 0;
        }

        public void flipCard(Card card)
        {
            if (card.Text == "")
            {
                card.Text = card.Value;

                if (Card1 == null)
                {
                    Card1 = card;
                }
                else if (Card2 == null && Card1 != card)
                {
                    Card2 = card;

                }
                else if (Card1 != null && Card2 != null)
                {
                    Card1.Text = "";
                    Card2.Text = "";
                    Card1 = card;
                    Card2 = null;
                }

                if (Card1 != null && Card2 != null)
                {
                    if (Card1.Value == Card2.Value)
                    {
                        Card1 = null;
                        Card2 = null;

                        Cards.Where(o => o.Text != "")
                        .GroupBy(x => x.Text)
                        .Where(o => o.Count() > 1).ToList()
                        .ForEach(o =>
                        {
                            o.ToList().ForEach(c => c.IsEnabled = true);
                        });
                    }
                    Flipped++;
                }
            }
        }
    }
}
