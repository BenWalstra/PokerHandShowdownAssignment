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
        public static void Deck_Constructor()
        {
            Deck deck = new Deck();

            Assert.AreNotEqual(null, deck);
            Assert.IsTrue(deck.CompleteDeck.Count > 0);
        }

        [Test]
        public static void Check_Deck_Has_52_Cards()
        {
            Deck deck = new Deck();

            Assert.IsTrue(deck.CompleteDeck.Count == 52);
        }

        [Test]
        public static void Check_Deck_Has_No_Dupblicate_Cards()
        {
            Deck deck = new Deck();
            List<ICard> uniqueCards = deck.CompleteDeck.Select(x => x).Distinct().ToList();
            Assert.IsTrue(uniqueCards.Count == 52);
        }

        [Test]
        public static void Check_Deck_Removes_Cards_Once_Dealt_To_Players()
        {
            Player player = new Player("Player1");
            List<Player> players = new List<Player>();
            players.Add(player);

            FiveCardPokerGame game = new FiveCardPokerGame(players);

            game.BeginRound();
            Assert.AreEqual(47, game.Deck.CompleteDeck.Count);
        }

        [Test]
        public static void Check_Deck_Removed_Correct_Cards_Once_Dealt_To_Players()
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
    }
}
