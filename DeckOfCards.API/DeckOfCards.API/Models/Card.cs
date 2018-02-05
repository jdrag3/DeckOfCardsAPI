namespace DeckOfCards.API.Models
{
    public class Card
    {
        public Card(string suit, string face)
        {
            this.Suit = suit;
            this.Face = face;
        }

        public string Suit { get; set; }
        public string Face { get; set; }
    }
}
