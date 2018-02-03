using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeckOfCards.API
{
    public class JsonDeck
    {
        public long Id { get; set; }
        public string SerializedDeck { get; set; }
    }

    public class JsonDeckContext: DbContext
    {
        public JsonDeckContext(DbContextOptions<JsonDeckContext> options) : base(options)
        { }

        public DbSet<JsonDeck> JsonDecks { get; set; }

        public long SaveDeck(Deck deck)
        {
            string strDeck = JsonConvert.SerializeObject(deck);
            JsonDeck jsonDeck = new JsonDeck
            {
                SerializedDeck = strDeck
            };

            this.JsonDecks.Add(jsonDeck);
            this.SaveChanges();
            return jsonDeck.Id;
        }

        public long UpdateDeck(Deck deck, long deckId)
        {
            string strDeck = JsonConvert.SerializeObject(deck);
            JsonDeck jsonDeck = new JsonDeck
            {
                Id = deckId,
                SerializedDeck = strDeck
            };

            this.JsonDecks.Update(jsonDeck);
            this.SaveChanges();
            return jsonDeck.Id;
        }
    }
}
