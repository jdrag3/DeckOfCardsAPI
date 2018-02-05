using System;
using System.Linq;
using DeckOfCards.API.Contexts;
using DeckOfCards.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DeckOfCards.API.Controllers
{
    [Produces("application/json")]
    [Route("api/DeckOfCards")]
    public class DeckOfCardsController : Controller
    {
        private const string DECK_DOES_NOT_EXIST_ERROR = "Deck does not exist, try creating a new one.";
        private DeckOfCardsContext m_deckContext;
        private long DeckId
        {
            get
            {
                string strDeckId = HttpContext.Session.GetString("_DeckId");

                if (String.IsNullOrEmpty(strDeckId))
                {
                    return -1L;
                }

                return Convert.ToInt64(strDeckId);

            }
            set { HttpContext.Session.SetString("_DeckId", value.ToString()); }
        }

        public DeckOfCardsController(DeckOfCardsContext deckContext)
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

            return NoContent();
        }

        [HttpPost("shuffle")]
        public IActionResult Shuffle()
        {
            Deck deck = this.ConvertJsonDeck();

            if (deck == null)
            {
                return NotFound(DECK_DOES_NOT_EXIST_ERROR);
            }

            deck = deck.ShuffleDeck();

            if (deck == null)
            {
                return NotFound("No cards left in the deck to shuffle");
            }

            m_deckContext.UpdateDeck(deck, this.DeckId);

            return NoContent();
        }

        [HttpPost("cut")]
        public IActionResult Cut()
        {
            Deck deck = this.ConvertJsonDeck();

            if (deck == null)
            {
                return NotFound(DECK_DOES_NOT_EXIST_ERROR);
            }

            deck = deck.CutDeck();

            if (deck == null)
            {
                return NotFound("No cards left in the deck to cut");
            }

            m_deckContext.UpdateDeck(deck, this.DeckId);

            return NoContent();
        }

        [HttpGet("getcard")]
        public IActionResult GetCard()
        {
            Deck deck = this.ConvertJsonDeck();

            if (deck == null)
            {
                return NotFound(DECK_DOES_NOT_EXIST_ERROR);
            }

            Card card = null;
            deck = deck.GetCard(out card);

            if (deck == null || card == null)
            {
                return NotFound("No cards left in the deck");
            }

            m_deckContext.UpdateDeck(deck, this.DeckId);

            return Ok(card);
        }

        [HttpGet("getdeck")]
        public IActionResult GetDeck()
        {
            Deck deck = this.ConvertJsonDeck();

            if (deck == null)
            {
                return NotFound(DECK_DOES_NOT_EXIST_ERROR);
            }

            return Ok(deck);
        }

        private Deck ConvertJsonDeck()
        {
            Models.DeckOfCards jsonDeck = null;
            Deck deck = null;

            try
            {
                jsonDeck = m_deckContext.Decks.AsNoTracking().First(d => d.Id == this.DeckId);
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