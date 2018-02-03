using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DeckOfCards.API
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
