using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeckOfCards.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CardController : Controller
    {
        private CardContext m_cardContext;

        public CardController(CardContext cardContext)
        {
            m_cardContext = cardContext;

            if (m_cardContext.Cards.Count() == 0)
            {
                m_cardContext.Cards.Add(new Card { Face = "A", Suit = "Hearts", Played = false });
                m_cardContext.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<Card> GetAll()
        {
            return m_cardContext.Cards.AsNoTracking().ToList();
        }

        [HttpGet("{id}", Name = "GetCard")]
        public IActionResult GetById(long id)
        {
            var card = m_cardContext.Cards.FirstOrDefault(c => c.Id == id);

            if (card == null)
            {
                return NotFound(); // returns 404
            }

            return new ObjectResult(card); // returns 200
        }
    }
}