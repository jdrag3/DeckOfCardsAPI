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

        // overriding for unit tests
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj is Card)
            {
                Card card = obj as Card;
                if (card == null)
                {
                    return false;
                }

                return ((this.Face.CompareTo(card.Face) == 0) &&
                    (this.Suit.CompareTo(card.Suit) == 0));
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}