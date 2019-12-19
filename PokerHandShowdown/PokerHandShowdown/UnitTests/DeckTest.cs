using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System;
using PokerHandShowdown;

namespace TestLibrary
{
    /// <summary>
    /// DeckTest - A class for testing the Deck class
    /// Deck - A class for representing a Deck of playing cards.
    /// 
    /// Date Created:   December. 16th, 2019
    /// 
    /// @author: Benjamin Walstra
    /// @version 1.0
    /// </summary>\
    [TestFixture]
    public class DeckTest
    {
        [Test]
        public static void Test_Deck_Constructor()
        {
            Deck deck = new Deck();

            Assert.AreNotEqual(null, deck);
            Assert.IsTrue(deck.CompleteDeck.Count > 0);
        }

        [Test]
        public static void Test_Deck_Has_52_Cards()
        {
            Deck deck = new Deck();

            Assert.IsTrue(deck.CompleteDeck.Count == 52);
        }

        [Test]
        public static void Test_Deck_Has_No_Dupblicate_Cards()
        {
            Deck deck = new Deck();
            List<ICard> uniqueCards = deck.CompleteDeck.Select(x => x).Distinct().ToList();
            Assert.IsTrue(uniqueCards.Count == 52);
        }

        [Test]
        public static void Test_Deck_Removes_Cards_Once_Dealt_To_Players()
        {
            Player player = new Player("Player1");
            List<Player> players = new List<Player>();
            players.Add(player);

            FiveCardPokerGame game = new FiveCardPokerGame(players);

            game.BeginRound();
            Assert.AreEqual(47, game.Deck.CompleteDeck.Count);
        }

        [Test]
        public static void Test_Deck_Removed_Correct_Cards_Once_Dealt_To_Players()
        {
            Player player = new Player("Player1");
            List<Player> players = new List<Player>();
            players.Add(player);

            FiveCardPokerGame game = new FiveCardPokerGame(players);
            game.BeginRound();

            foreach (var playerCard in player.Hand)
            {
                Assert.IsFalse(game.Deck.CompleteDeck.Contains(playerCard));
            }
        }

        [Test]
        public static void Test_That_Deal_Hand_Creates_New_Deck_and_Doesnt_add_To_Old_Deck()
        {
            Player player = new Player("Player1");
            List<Player> players = new List<Player>();
            int deckOneCount = 0;
            int deckTwoCount = 0;
            players.Add(player);

            FiveCardPokerGame game = new FiveCardPokerGame(players);
            game.BeginRound();
            deckOneCount = game.Deck.CompleteDeck.Count;
            game.BeginRound();
            deckTwoCount = game.Deck.CompleteDeck.Count;

            Assert.AreEqual(deckOneCount, deckTwoCount);
        }
    }
}
