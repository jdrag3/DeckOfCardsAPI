using DeckOfCards.API.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DeckOfCards.API.Contexts
{
    public class DeckOfCardsContext : DbContext
    {
        public DeckOfCardsContext(DbContextOptions<DeckOfCardsContext> options) : base(options)
        { }

        public DbSet<Models.DeckOfCards> Decks { get; set; }

        public long SaveDeck(Deck deck)
        {
            string strDeck = JsonConvert.SerializeObject(deck);
            Models.DeckOfCards deckOfCards = new Models.DeckOfCards
            {
                SerializedDeck = strDeck
            };

            this.Decks.Add(deckOfCards);
            this.SaveChanges();
            return deckOfCards.Id;
        }

        public long UpdateDeck(Deck deck, long deckId)
        {
            string strDeck = JsonConvert.SerializeObject(deck);
            Models.DeckOfCards deckOfCards = new Models.DeckOfCards
            {
                Id = deckId,
                SerializedDeck = strDeck
            };

            this.Decks.Update(deckOfCards);
            this.SaveChanges();
            return deckOfCards.Id;
        }
    }
}
