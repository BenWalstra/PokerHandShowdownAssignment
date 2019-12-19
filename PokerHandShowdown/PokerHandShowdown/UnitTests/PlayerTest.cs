using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System;
using PokerHandShowdown;

namespace TestLibrary
{
    /// <summary>
    /// PlayerTest - A class for testing the Player class
    /// Player - A class for representing a player in the game of 5 hand poker.
    /// 
    /// Date Created:   December. 16th, 2019
    /// 
    /// @author: Benjamin Walstra
    /// @version 1.0
    /// </summary>\
    [TestFixture]
    public class PlayerTest
    {
        [Test]
        public static void Player_Constructor()
        {
            string name = "test";
            Player player = new Player(name);

            Assert.AreNotEqual(null, player);
            Assert.AreEqual(name, player.Name);
        }

        [Test]
        public static void Check_Player_Hand_Has_5_Unique_Cards()
        {
            Player player = new Player("Ben");
            List<Player> players = new List<Player>{ player };
            FiveCardPokerGame game = new FiveCardPokerGame(players);
            game.BeginRound();

            // Get Unique Cards
            List<ICard> cards = player.Hand.Select(x => x).Distinct().ToList();

            Assert.AreEqual(5, player.Hand.Count);
            Assert.AreEqual(5, cards.Count);
        }

        [Test]
        public static void Check_Player_Has_Flush()
        {
            Player player = new Player("Ben");
            ICard card;

            card = new Spade(CardValues.Ace);
            player.Hand.Add(card);
            card = new Spade(CardValues.Two);
            player.Hand.Add(card);
            card = new Spade(CardValues.Three);
            player.Hand.Add(card);
            card = new Spade(CardValues.Four);
            player.Hand.Add(card);
            card = new Spade(CardValues.Five);
            player.Hand.Add(card);

            Assert.IsTrue(player.CheckFlush());
        }

        [Test]
        public static void Check_Player_Has_No_Flush()
        {
            Player player = new Player("Ben");
            ICard card;

            card = new Heart(CardValues.Ace);
            player.Hand.Add(card);
            card = new Spade(CardValues.Two);
            player.Hand.Add(card);
            card = new Spade(CardValues.Three);
            player.Hand.Add(card);
            card = new Spade(CardValues.Four);
            player.Hand.Add(card);
            card = new Spade(CardValues.Five);
            player.Hand.Add(card);

            Assert.IsFalse(player.CheckFlush());
        }

        [Test]
        public static void Check_Player_Has_Three_Of_a_Kind()
        {
            Player player = new Player("Ben");
            ICard card;

            card = new Spade(CardValues.Ace);
            player.Hand.Add(card);
            card = new Heart(CardValues.Ace);
            player.Hand.Add(card);
            card = new Club(CardValues.Ace);
            player.Hand.Add(card);
            card = new Spade(CardValues.Four);
            player.Hand.Add(card);
            card = new Spade(CardValues.Five);
            player.Hand.Add(card);

            Assert.IsTrue(player.CheckThreeOfKind());
        }

        [Test]
        public static void Check_Player_Has_No_Three_Of_a_Kind()
        {
            Player player = new Player("Ben");
            ICard card;

            card = new Spade(CardValues.Ace);
            player.Hand.Add(card);
            card = new Heart(CardValues.Ace);
            player.Hand.Add(card);
            card = new Club(CardValues.Three);
            player.Hand.Add(card);
            card = new Spade(CardValues.Four);
            player.Hand.Add(card);
            card = new Spade(CardValues.Five);
            player.Hand.Add(card);

            Assert.IsFalse(player.CheckThreeOfKind());
        }

        [Test]
        public static void Check_Player_Has_One_Pair()
        {
            Player player = new Player("Ben");
            ICard card;

            card = new Spade(CardValues.Ace);
            player.Hand.Add(card);
            card = new Heart(CardValues.Ace);
            player.Hand.Add(card);
            card = new Club(CardValues.Three);
            player.Hand.Add(card);
            card = new Spade(CardValues.Four);
            player.Hand.Add(card);
            card = new Spade(CardValues.Five);
            player.Hand.Add(card);

            Assert.IsTrue(player.CheckOnePair());
        }

        [Test]
        public static void Check_Player_Has_No_One_Pair()
        {
            Player player = new Player("Ben");
            ICard card;

            card = new Spade(CardValues.Ace);
            player.Hand.Add(card);
            card = new Heart(CardValues.King);
            player.Hand.Add(card);
            card = new Club(CardValues.Three);
            player.Hand.Add(card);
            card = new Spade(CardValues.Four);
            player.Hand.Add(card);
            card = new Spade(CardValues.Five);
            player.Hand.Add(card);

            Assert.IsFalse(player.CheckOnePair());
        }

        [Test]
        public static void Check_Player_High_Card()
        {
            Player player = new Player("Ben");
            ICard card;

            card = new Spade(CardValues.Ace);
            player.Hand.Add(card);
            card = new Heart(CardValues.Six);
            player.Hand.Add(card);
            card = new Club(CardValues.Three);
            player.Hand.Add(card);
            card = new Spade(CardValues.Four);
            player.Hand.Add(card);
            card = new Spade(CardValues.Five);
            player.Hand.Add(card);

            player.OrderHand();

            Assert.IsTrue(player.HighCard.CompareTo(new Spade(CardValues.Ace)) == 0);
        }

        [Test]
        public static void Check_Player_Not_High_Card()
        {
            Player player = new Player("Ben");
            ICard card;

            card = new Spade(CardValues.Ace);
            player.Hand.Add(card);
            card = new Heart(CardValues.Six);
            player.Hand.Add(card);
            card = new Club(CardValues.Three);
            player.Hand.Add(card);
            card = new Spade(CardValues.Four);
            player.Hand.Add(card);
            card = new Spade(CardValues.Five);
            player.Hand.Add(card);

            player.OrderHand();

            Assert.IsFalse(player.HighCard.CompareTo(new Spade(CardValues.Six)) == 0);
        }

        [Test]
        public static void Check_Player_Three_of_A_Kind_Sum()
        {
            Player player = new Player("Ben");
            ICard card;

            card = new Spade(CardValues.Ace);
            player.Hand.Add(card);
            card = new Heart(CardValues.Ace);
            player.Hand.Add(card);
            card = new Club(CardValues.Ace);
            player.Hand.Add(card);
            card = new Spade(CardValues.Four);
            player.Hand.Add(card);
            card = new Spade(CardValues.Five);
            player.Hand.Add(card);

            Assert.IsTrue(player.CheckThreeOfKind());
            Assert.AreEqual(13, player.ThreeOfaKindSum);
        }

        [Test]
        public static void Check_Player_One_Pair_Sum()
        {
            Player player = new Player("Ben");
            ICard card;

            card = new Spade(CardValues.Ace);
            player.Hand.Add(card);
            card = new Heart(CardValues.Ace);
            player.Hand.Add(card);
            card = new Club(CardValues.King);
            player.Hand.Add(card);
            card = new Spade(CardValues.Four);
            player.Hand.Add(card);
            card = new Spade(CardValues.Five);
            player.Hand.Add(card);

            Assert.IsTrue(player.CheckOnePair());
            Assert.AreEqual(13, player.OnePairSum);
        }
    }
}
