using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DeckOfCards.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class DeckController : Controller
    {
        private JsonDeckContext m_deckContext;

        public DeckController(JsonDeckContext deckContext)
        {
            m_deckContext = deckContext;
        }

        [HttpPost("newdeck")]
        public IActionResult NewDeck()
        {
            Deck deck = new Deck();
            long deckId = m_deckContext.SaveDeck(deck.NewDeck());

            if (deckId < 0)
            {
                return NotFound("Could not create the deck. Please try again");
            }

            HttpContext.Session.SetString("_DeckId", deckId.ToString());

            return Ok(deckId);
        }

        [HttpPost("shuffle")]
        public IActionResult Shuffle()
        {
            Deck deck = this.ConvertJsonDeck();

            if (deck == null)
            {
                return NotFound("Deck does not exist");
            }

            deck = deck.ShuffleDeck();

            if (deck == null)
            {
                return NotFound("No cards left in the deck to cut");
            }

            m_deckContext.UpdateDeck(deck, this.GetDeckId());

            return Ok();
        }

        [HttpPost("cut")]
        public IActionResult Cut()
        {
            Deck deck = this.ConvertJsonDeck();

            if (deck == null)
            {
                return NotFound("Deck does not exist");
            }

            deck = deck.CutDeck();

            if (deck == null)
            {
                return NotFound("No cards left in the deck to cut");
            }

            m_deckContext.UpdateDeck(deck, this.GetDeckId());

            return Ok();
        }

        [HttpGet("getcard")]
        public IActionResult GetCard()
        {
            Deck deck = this.ConvertJsonDeck();

            if (deck == null)
            {
                return NotFound("Deck does not exist");
            }

            Card card = null;
            deck = deck.GetCard(out card);

            if (deck == null || card == null)
            {
                return NotFound("No cards left in the deck");
            }

            m_deckContext.UpdateDeck(deck, this.GetDeckId());

            return Ok(card);
        }

        [HttpGet("getdeck")]
        public IActionResult GetDeck()
        {
            Deck deck = this.ConvertJsonDeck();

            if (deck == null)
            {
                return NotFound("No decks exist yet try creating one.");
            }

            return Ok(deck);
        }

        private long GetDeckId()
        {
            string strDeckId = HttpContext.Session.GetString("_DeckId");

            if (String.IsNullOrEmpty(strDeckId))
            {
                return -1;
            }

            return Convert.ToInt64(strDeckId);
        }

        private Deck ConvertJsonDeck()
        {
            long deckId = this.GetDeckId();
            JsonDeck jsonDeck = null;
            Deck deck = null;

            try
            {
                jsonDeck = m_deckContext.JsonDecks.AsNoTracking().First(d => d.Id == deckId);
                deck = JsonConvert.DeserializeObject<Deck>(jsonDeck.SerializedDeck);
            }
            catch
            {
                return null;
            }

            return deck;
        }
    }
}