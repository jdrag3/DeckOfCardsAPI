using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DeckOfCards.API
{
    public class Deck
    {
        public List<Card> DealtCards { get; set; }
        public List<Card> Cards { get; set; }

        private string[] Suits = { "Clubs", "Diamonds", "Hearts", "Spades" };
        private string[] Faces = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
        public Deck()
        {
        }

        public Deck NewDeck()
        {
            this.Cards = new List<Card>();
            this.DealtCards = new List<Card>();

            foreach (string suit in Suits)
            {
                foreach (string face in Faces)
                {
                    this.Cards.Add(new Card(suit, face));
                }
            }

            return this;
        }

        public Deck GetCard(out Card card)
        {
            if (this.Cards.Count < 1)
            {
                card = null;
                return null;
            }

            card = Cards[0];
            this.DealtCards.Add(card);
            this.Cards.Remove(Cards[0]);
            return this;
        }

        public Deck GetDeck()
        {
            List<Card> allCards = this.DealtCards.ToList();
            allCards.AddRange(this.Cards.ToList());
            return this;
        }

        public Deck CutDeck()
        {
            int cardCount = this.Cards.Count;

            if (cardCount < 2)
            {
                return null;
            }

            Random rand = new Random();
            int randInt = rand.Next(cardCount);
            List<Card> tempDeck = this.Cards.GetRange(randInt, cardCount - randInt - 1);
            tempDeck.AddRange(this.Cards.GetRange(0, randInt - 1));
            this.Cards.Clear();
            this.Cards = tempDeck;

            return this;
        }

        public Deck ShuffleDeck()
        {
            if (this.Cards.Count < 2)
            {
                return null;
            }

            Card[] tempCards = this.Cards.ToArray();

            Random rand = new Random();

            for (int index = this.Cards.Count - 1; index > 0; index--)
            {
                int randInt = rand.Next(index + 1);
                this.Swap(ref tempCards[index], ref tempCards[randInt]);
            }

            this.Cards.Clear();
            this.Cards = tempCards.ToList();
            return this;
        }

        private void Swap(ref Card card1, ref Card card2)
        {
            Card tempCard = card1;
            card1 = card2;
            card2 = tempCard;
        }
    }
}
