using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DeckOfCards.API
{
    public class Card
    {
        public long Id { get; set; }
        public string Suit { get; set; }
        public string Face { get; set; }
        public bool Played { get; set; }
    }

    public class CardContext : DbContext
    {
        public CardContext(DbContextOptions<CardContext> options) : base(options)
        {
        }

        public DbSet<Card> Cards { get; set; }
    }
}
