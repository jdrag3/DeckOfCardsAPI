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

        public static bool operator ==(Card card1, Card card2)
        {
            if ((card1.Face.CompareTo(card2.Face) == 0) &&
                (card1.Suit.CompareTo(card2.Suit) == 0))
            {
                return true;
            }

            return false;
        }

        public static bool operator !=(Card card1, Card card2)
        {
            return !(card1 == card2);
        }

        public override bool Equals(object obj)
        {
            if (obj is Card)
            {
                Card card = obj as Card;
                return (this == card);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}