using DeckOfCards.API.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DeckOfCardsUnitTest
{
    [TestClass]
    public class DeckUnitTests
    {
        [TestMethod]
        public void TestDecksAreTheSame()
        {
            Deck deck = new Deck();
            deck.NewDeck();

            Card card1 = null;
            deck.GetCard(out card1);

            deck = new Deck();
            deck.NewDeck();

            Card card2 = null;
            deck.GetCard(out card2);

            // if the decks were properly created then
            // the card should be the same
            Assert.AreEqual(card1, card2);


        }

        [TestMethod]
        public void TestShuffle()
        {
            Deck deck = new Deck();
            deck.NewDeck();

            Card card1 = null;
            deck.GetCard(out card1);

            deck = new Deck();
            deck.NewDeck();
            deck.ShuffleDeck();

            Card card2 = null;
            deck.GetCard(out card2);

            // if the deck was properly shuffled then
            // the card should be different
            Assert.AreNotEqual(card1, card2);

        }

        [TestMethod]
        public void TestCut()
        {
            Deck deck = new Deck();
            deck.NewDeck();

            Card card1 = null;
            deck.GetCard(out card1);

            deck = new Deck();
            deck.NewDeck();
            deck.CutDeck();

            Card card2 = null;
            deck.GetCard(out card2);

            // if the deck was properly cut the
            // cards should be different
            Assert.AreNotEqual(card1, card2);
        }

        [TestMethod]
        public void TestDealtCards()
        {
            Deck deck = new Deck();
            deck.NewDeck();

            Card card1 = null;
            deck.GetCard(out card1);

            Deck dealtDeck = deck.GetDeck();

            // the dealt "deck" should have one card
            // and it should be the same as the first card
            // from an unshuffled "deck"
            Assert.AreEqual(1, dealtDeck.DealtCards.Count);
            Assert.AreEqual(card1, dealtDeck.DealtCards[0]);

            Card card2 = null;
            deck.GetCard(out card2);

            // taking a second card from the deck 
            // should not be the same card as the first.
            Assert.AreNotEqual(card2, dealtDeck.DealtCards[0]);

        }
    }
}