using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System;
using PokerHandShowdown;

namespace TestLibrary
{
    /// <summary>
    /// GameTest - A class for testing the Game class
    /// Game - A class for representing the game of poker.
    /// 
    /// Date Created:   December. 16th, 2019
    /// 
    /// @author: Benjamin Walstra
    /// @version 1.0
    /// </summary>\
    [TestFixture]
    public class FiveCardPokerGameTest
    {
        #region Test Flush

        [Test]
        public static void Test_User_With_Flush_Test()
        {
            Player player = new Player("Ben");
            List<Player> players = new List<Player>();

            var cards = new List<ICard>();
            ICard c;

            c = new Club(CardValues.Two);
            cards.Add(c);
            c = new Club(CardValues.Three);
            cards.Add(c);
            c = new Club(CardValues.Four);
            cards.Add(c);
            c = new Club(CardValues.Five);
            cards.Add(c);
            c = new Club(CardValues.Ace);
            cards.Add(c);

            players.Add(player);

            FiveCardPokerGame game = new FiveCardPokerGame(players);
            
            player.Hand = cards;

            Assert.That(game.CheckForFlush(), Is.True);
        }

        [Test]
        public static void Test_User_Without_Flush_Test()
        {
            Player player = new Player("Ben");

            List<Player> players = new List<Player>();

            var cards = new List<ICard>();
            ICard c;

            c = new Spade(CardValues.Two);
            cards.Add(c);
            c = new Club(CardValues.Three);
            cards.Add(c);
            c = new Club(CardValues.Four);
            cards.Add(c);
            c = new Club(CardValues.Five);
            cards.Add(c);
            c = new Club(CardValues.Ace);
            cards.Add(c);

            players.Add(player);
            FiveCardPokerGame game = new FiveCardPokerGame(players);
            
            player.Hand = cards;

            Assert.That(game.CheckForFlush(), Is.False);
        }

        [Test]
        public static void Test_Multiple_Users_With_One_Winner_Flush_Test()
        {
            Player player1 = new Player("Ben");
            Player player2 = new Player("Neb");
            List<Player> players = new List<Player>();

            var cards1 = new List<ICard>();
            var cards2 = new List<ICard>();
            ICard c;

            // Cards for player 1
            c = new Club(CardValues.Two);
            cards1.Add(c);
            c = new Club(CardValues.Three);
            cards1.Add(c);
            c = new Club(CardValues.Four);
            cards1.Add(c);
            c = new Club(CardValues.Five);
            cards1.Add(c);
            c = new Club(CardValues.Ace);
            cards1.Add(c);

            // Cards for player 2
            c = new Heart(CardValues.Six);
            cards2.Add(c);
            c = new Heart(CardValues.Three);
            cards2.Add(c);
            c = new Heart(CardValues.Four);
            cards2.Add(c);
            c = new Heart(CardValues.Five);
            cards2.Add(c);
            c = new Heart(CardValues.Ace);
            cards2.Add(c);

            players.Add(player1);
            players.Add(player2);

            FiveCardPokerGame game = new FiveCardPokerGame(players);
            
            player1.Hand = cards1;
            player2.Hand = cards2;
            game.GetRoundWinner();

            Assert.That(game.Winner, Is.EqualTo(player2));
        }

        [Test]
        public static void Test_Multiple_Users_With_Flush_And_Same_Values_Test()
        {
            Player player1 = new Player("Ben");
            Player player2 = new Player("Neb");
            List<Player> players = new List<Player>();

            var cards1 = new List<ICard>();
            var cards2 = new List<ICard>();
            ICard c;

            // Cards for player 1
            c = new Club(CardValues.Two);
            cards1.Add(c);
            c = new Club(CardValues.Three);
            cards1.Add(c);
            c = new Club(CardValues.Four);
            cards1.Add(c);
            c = new Club(CardValues.Five);
            cards1.Add(c);
            c = new Club(CardValues.Ace);
            cards1.Add(c);

            // Cards for player 2
            c = new Heart(CardValues.Two);
            cards2.Add(c);
            c = new Heart(CardValues.Three);
            cards2.Add(c);
            c = new Heart(CardValues.Four);
            cards2.Add(c);
            c = new Heart(CardValues.Five);
            cards2.Add(c);
            c = new Heart(CardValues.Ace);
            cards2.Add(c);

            players.Add(player1);
            players.Add(player2);
            FiveCardPokerGame game = new FiveCardPokerGame(players);
            
            player1.Hand = cards1;
            player2.Hand = cards2;


            Assert.That(game.CheckForFlush(), Is.True);
            Assert.That(game.TiedPlayers.Contains(player1) && 
                game.TiedPlayers.Contains(player2) &&
                game.TiedPlayers.Count ==2 , Is.True);
        }

        [Test]
        public static void Test_Single_Winner_From_Flush()
        {
            Player player1 = new Player("Ben");
            Player player2 = new Player("Neb");
            List<Player> players = new List<Player>();
            string winningMsg = "Congratulations! The winner this round is: Neb with Flush";

            var cards1 = new List<ICard>();
            var cards2 = new List<ICard>();
            ICard c;

            // Cards for player 1
            c = new Spade(CardValues.Two);
            cards1.Add(c);
            c = new Club(CardValues.Three);
            cards1.Add(c);
            c = new Club(CardValues.Four);
            cards1.Add(c);
            c = new Club(CardValues.Five);
            cards1.Add(c);
            c = new Club(CardValues.Ace);
            cards1.Add(c);

            // Cards for player 2
            c = new Heart(CardValues.Six);
            cards2.Add(c);
            c = new Heart(CardValues.Three);
            cards2.Add(c);
            c = new Heart(CardValues.Four);
            cards2.Add(c);
            c = new Heart(CardValues.Five);
            cards2.Add(c);
            c = new Heart(CardValues.Ace);
            cards2.Add(c);

            players.Add(player1);
            players.Add(player2);

            FiveCardPokerGame game = new FiveCardPokerGame(players);
            
            player1.Hand = cards1;
            player2.Hand = cards2;

            Assert.That(game.GetRoundWinner, Is.EqualTo(winningMsg));
            Assert.That(game.Winner, Is.EqualTo(player2));
        }

        [Test]
        public static void Test_Single_Winner_With_High_Card_From_Flush()
        {
            Player player1 = new Player("Ben");
            Player player2 = new Player("Neb");
            List<Player> players = new List<Player>();
            string winningMsg = "Congratulations! The winner this round is: Neb with Flush and High Card";

            var cards1 = new List<ICard>();
            var cards2 = new List<ICard>();
            ICard c;

            // Cards for player 1
            c = new Club(CardValues.Two);
            cards1.Add(c);
            c = new Club(CardValues.Three);
            cards1.Add(c);
            c = new Club(CardValues.Four);
            cards1.Add(c);
            c = new Club(CardValues.Five);
            cards1.Add(c);
            c = new Club(CardValues.Ace);
            cards1.Add(c);

            // Cards for player 2
            c = new Heart(CardValues.Six);
            cards2.Add(c);
            c = new Heart(CardValues.Three);
            cards2.Add(c);
            c = new Heart(CardValues.Four);
            cards2.Add(c);
            c = new Heart(CardValues.Five);
            cards2.Add(c);
            c = new Heart(CardValues.Ace);
            cards2.Add(c);

            players.Add(player1);
            players.Add(player2);

            FiveCardPokerGame game = new FiveCardPokerGame(players);
            
            player1.Hand = cards1;
            player2.Hand = cards2;

            Assert.That(game.GetRoundWinner(), Is.EqualTo(winningMsg));
            Assert.That(game.Winner, Is.EqualTo(player2));
        }

        [Test]
        public static void Test_Multiple_Winner_From_Flush_Tie()
        {
            Player player1 = new Player("Ben");
            Player player2 = new Player("Neb");
            List<Player> players = new List<Player>();
            string winningMsg = "Looks like we have a tie that round between:  Ben Neb with a Flush";

            var cards1 = new List<ICard>();
            var cards2 = new List<ICard>();
            ICard c;

            // Cards for player 1
            c = new Club(CardValues.Two);
            cards1.Add(c);
            c = new Club(CardValues.Three);
            cards1.Add(c);
            c = new Club(CardValues.Four);
            cards1.Add(c);
            c = new Club(CardValues.Five);
            cards1.Add(c);
            c = new Club(CardValues.Ace);
            cards1.Add(c);

            // Cards for player 2
            c = new Heart(CardValues.Two);
            cards2.Add(c);
            c = new Heart(CardValues.Three);
            cards2.Add(c);
            c = new Heart(CardValues.Four);
            cards2.Add(c);
            c = new Heart(CardValues.Five);
            cards2.Add(c);
            c = new Heart(CardValues.Ace);
            cards2.Add(c);

            players.Add(player1);
            players.Add(player2);

            FiveCardPokerGame game = new FiveCardPokerGame(players);
            
            player1.Hand = cards1;
            player2.Hand = cards2;
            Assert.That(game.GetRoundWinner(), Is.EqualTo(winningMsg));
            Assert.That(game.TiedPlayers.Contains(player1) &&
                game.TiedPlayers.Contains(player2) &&
                game.TiedPlayers.Count == 2, Is.True);
        }

        [Test]
        public static void Test_Three_Winner_From_Flush()
        {
            Player player1 = new Player("Ben");
            Player player2 = new Player("Neb");
            Player player3 = new Player("Bill");

            List<Player> players = new List<Player>();
            string winningMsg = "Looks like we have a tie that round between:  Ben Neb Bill with a Flush";

            var cards1 = new List<ICard>();
            var cards2 = new List<ICard>();
            var cards3 = new List<ICard>();
            ICard c;

            // Cards for player 1
            c = new Club(CardValues.Two);
            cards1.Add(c);
            c = new Club(CardValues.Three);
            cards1.Add(c);
            c = new Club(CardValues.Four);
            cards1.Add(c);
            c = new Club(CardValues.Five);
            cards1.Add(c);
            c = new Club(CardValues.Ace);
            cards1.Add(c);

            // Cards for player 2
            c = new Heart(CardValues.Two);
            cards2.Add(c);
            c = new Heart(CardValues.Three);
            cards2.Add(c);
            c = new Heart(CardValues.Four);
            cards2.Add(c);
            c = new Heart(CardValues.Five);
            cards2.Add(c);
            c = new Heart(CardValues.Ace);
            cards2.Add(c);

            // Cards for player 3
            c = new Spade(CardValues.Two);
            cards3.Add(c);
            c = new Spade(CardValues.Three);
            cards3.Add(c);
            c = new Spade(CardValues.Four);
            cards3.Add(c);
            c = new Spade(CardValues.Five);
            cards3.Add(c);
            c = new Spade(CardValues.Ace);
            cards3.Add(c);

            players.Add(player1);
            players.Add(player2);
            players.Add(player3);

            FiveCardPokerGame game = new FiveCardPokerGame(players);
            
            player1.Hand = cards1;
            player2.Hand = cards2;
            player3.Hand = cards3;

            Assert.That(game.GetRoundWinner(), Is.EqualTo(winningMsg));
            Assert.That(game.TiedPlayers.Contains(player1) &&
                game.TiedPlayers.Contains(player2) &&
                game.TiedPlayers.Contains(player3) &&
                game.TiedPlayers.Count == 3, Is.True);
        }

        #endregion

        #region Test Three of a Kind

        [Test]
        public static void Test_Winner_From_Three_Of_A_Kind()
        {
            Player player1 = new Player("Ben");
            Player player2 = new Player("Neb");
            List<Player> players = new List<Player>();
            string winningMsg = "Congratulations! The winner this round is: Neb with Three of a Kind";

            var cards1 = new List<ICard>();
            var cards2 = new List<ICard>();
            ICard c;

            // Cards for player 1
            c = new Heart(CardValues.Two);
            cards1.Add(c);
            c = new Club(CardValues.Three);
            cards1.Add(c);
            c = new Club(CardValues.Four);
            cards1.Add(c);
            c = new Club(CardValues.Five);
            cards1.Add(c);
            c = new Club(CardValues.Ace);
            cards1.Add(c);

            // Cards for player 2
            c = new Club(CardValues.Six);
            cards2.Add(c);
            c = new Heart(CardValues.Six);
            cards2.Add(c);
            c = new Spade(CardValues.Six);
            cards2.Add(c);
            c = new Heart(CardValues.Five);
            cards2.Add(c);
            c = new Heart(CardValues.Ace);
            cards2.Add(c);

            players.Add(player1);
            players.Add(player2);

            FiveCardPokerGame game = new FiveCardPokerGame(players);
            
            player1.Hand = cards1;
            player2.Hand = cards2;
            Assert.That(game.GetRoundWinner(), Is.EqualTo(winningMsg));
            Assert.That(game.Winner, Is.EqualTo(player2));
        }

        [Test]
        public static void Test_Winner_From_Three_Of_A_Kind_With_Higher_Three_Cards()
        {
            Player player1 = new Player("Ben");
            Player player2 = new Player("Neb");
            List<Player> players = new List<Player>();
            string winningMsg = "Congratulations! The winner this round is: Neb with Three of a Kind";

            var cards1 = new List<ICard>();
            var cards2 = new List<ICard>();
            ICard c;

            // Cards for player 1
            c = new Heart(CardValues.Two);
            cards1.Add(c);
            c = new Spade(CardValues.Two);
            cards1.Add(c);
            c = new Club(CardValues.Two);
            cards1.Add(c);
            c = new Club(CardValues.Five);
            cards1.Add(c);
            c = new Club(CardValues.Ace);
            cards1.Add(c);

            // Cards for player 2
            c = new Club(CardValues.Six);
            cards2.Add(c);
            c = new Heart(CardValues.Six);
            cards2.Add(c);
            c = new Spade(CardValues.Six);
            cards2.Add(c);
            c = new Heart(CardValues.Five);
            cards2.Add(c);
            c = new Heart(CardValues.Ace);
            cards2.Add(c);

            players.Add(player1);
            players.Add(player2);

            FiveCardPokerGame game = new FiveCardPokerGame(players);
            
            player1.Hand = cards1;
            player2.Hand = cards2;
            Assert.That(game.GetRoundWinner(), Is.EqualTo(winningMsg));
            Assert.That(game.Winner, Is.EqualTo(player2));
        }

        [Test]
        public static void Test_Winner_From_Three_Of_A_Kind_With_High_Card()
        {
            Player player1 = new Player("Ben");
            Player player2 = new Player("Neb");
            List<Player> players = new List<Player>();
            string winningMsg = "Congratulations! The winner this round is: Neb with Three of a Kind and High Card";

            var cards1 = new List<ICard>();
            var cards2 = new List<ICard>();
            ICard c;

            // Cards for player 1
            c = new Heart(CardValues.Two);
            cards1.Add(c);
            c = new Spade(CardValues.Two);
            cards1.Add(c);
            c = new Club(CardValues.Two);
            cards1.Add(c);
            c = new Club(CardValues.Five);
            cards1.Add(c);
            c = new Club(CardValues.Ace);
            cards1.Add(c);

            // Cards for player 2
            c = new Heart(CardValues.Two);
            cards2.Add(c);
            c = new Spade(CardValues.Two);
            cards2.Add(c);
            c = new Club(CardValues.Two);
            cards2.Add(c);
            c = new Heart(CardValues.Ten);
            cards2.Add(c);
            c = new Heart(CardValues.Ace);
            cards2.Add(c);

            players.Add(player1);
            players.Add(player2);

            FiveCardPokerGame game = new FiveCardPokerGame(players);
            
            player1.Hand = cards1;
            player2.Hand = cards2;
            Assert.That(game.GetRoundWinner(), Is.EqualTo(winningMsg));
            Assert.That(game.Winner, Is.EqualTo(player2));
        }

        //Not possible but testing anyway
        [Test]
        public static void Test_Winners_From_Three_of_a_Kind_Tie()
        {
            Player player1 = new Player("Ben");
            Player player2 = new Player("Neb");
            List<Player> players = new List<Player>();
            string winningMsg = "Looks like we have a tie that round between:  Ben Neb with a Three of a Kind";

            var cards1 = new List<ICard>();
            var cards2 = new List<ICard>();
            ICard c;

            // Cards for player 1
            c = new Heart(CardValues.Two);
            cards1.Add(c);
            c = new Club(CardValues.Two);
            cards1.Add(c);
            c = new Diamond(CardValues.Two);
            cards1.Add(c);
            c = new Club(CardValues.Five);
            cards1.Add(c);
            c = new Club(CardValues.Ace);
            cards1.Add(c);

            // Cards for player 2
            c = new Heart(CardValues.Two);
            cards2.Add(c);
            c = new Club(CardValues.Two);
            cards2.Add(c);
            c = new Diamond(CardValues.Two);
            cards2.Add(c);
            c = new Club(CardValues.Five);
            cards2.Add(c);
            c = new Club(CardValues.Ace);
            cards2.Add(c);

            players.Add(player1);
            players.Add(player2);

            FiveCardPokerGame game = new FiveCardPokerGame(players);

            player1.Hand = cards1;
            player2.Hand = cards2;

            Assert.That(game.GetRoundWinner(), Is.EqualTo(winningMsg));
            Assert.That(game.TiedPlayers.Contains(player1) &&
                game.TiedPlayers.Contains(player2) &&
                game.TiedPlayers.Count == 2, Is.True);
        }

        #endregion

        #region Test One Pair

        [Test]
        public static void Test_Winner_From_Pair()
        {
            Player player1 = new Player("Ben");
            Player player2 = new Player("Neb");
            List<Player> players = new List<Player>();
            string winningMsg = "Congratulations! The winner this round is: Neb with One Pair";

            var cards1 = new List<ICard>();
            var cards2 = new List<ICard>();
            ICard c;

            // Cards for player 1
            c = new Heart(CardValues.Two);
            cards1.Add(c);
            c = new Club(CardValues.Three);
            cards1.Add(c);
            c = new Club(CardValues.Four);
            cards1.Add(c);
            c = new Club(CardValues.Five);
            cards1.Add(c);
            c = new Club(CardValues.Ace);
            cards1.Add(c);

            // Cards for player 2
            c = new Club(CardValues.Six);
            cards2.Add(c);
            c = new Heart(CardValues.Six);
            cards2.Add(c);
            c = new Spade(CardValues.King);
            cards2.Add(c);
            c = new Heart(CardValues.Five);
            cards2.Add(c);
            c = new Heart(CardValues.Ace);
            cards2.Add(c);

            players.Add(player1);
            players.Add(player2);

            FiveCardPokerGame game = new FiveCardPokerGame(players);
            
            player1.Hand = cards1;
            player2.Hand = cards2;

            Assert.That(game.GetRoundWinner(), Is.EqualTo(winningMsg));
            Assert.That(game.Winner, Is.EqualTo(player2));
        }

        [Test]
        public static void Test_Winner_From_Pair_With_Higher_Pair()
        {
            Player player1 = new Player("Ben");
            Player player2 = new Player("Neb");
            List<Player> players = new List<Player>();
            string winningMsg = "Congratulations! The winner this round is: Neb with One Pair";

            var cards1 = new List<ICard>();
            var cards2 = new List<ICard>();
            ICard c;

            // Cards for player 1
            c = new Heart(CardValues.Two);
            cards1.Add(c);
            c = new Club(CardValues.Two);
            cards1.Add(c);
            c = new Club(CardValues.Four);
            cards1.Add(c);
            c = new Club(CardValues.Five);
            cards1.Add(c);
            c = new Club(CardValues.Ace);
            cards1.Add(c);

            // Cards for player 2
            c = new Spade(CardValues.Three);
            cards2.Add(c);
            c = new Diamond(CardValues.Three);
            cards2.Add(c);
            c = new Spade(CardValues.King);
            cards2.Add(c);
            c = new Heart(CardValues.Five);
            cards2.Add(c);
            c = new Heart(CardValues.Ace);
            cards2.Add(c);

            players.Add(player1);
            players.Add(player2);

            FiveCardPokerGame game = new FiveCardPokerGame(players);

            player1.Hand = cards1;
            player2.Hand = cards2;

            Assert.That(game.GetRoundWinner(), Is.EqualTo(winningMsg));
            Assert.That(game.Winner, Is.EqualTo(player2));
        }

        [Test]
        public static void Test_Winner_From_Pair_With_High_Card()
        {
            Player player1 = new Player("Ben");
            Player player2 = new Player("Neb");
            List<Player> players = new List<Player>();
            string winningMsg = "Congratulations! The winner this round is: Neb with One Pair and High Card";

            var cards1 = new List<ICard>();
            var cards2 = new List<ICard>();
            ICard c;

            // Cards for player 1
            c = new Heart(CardValues.Two);
            cards1.Add(c);
            c = new Club(CardValues.Two);
            cards1.Add(c);
            c = new Club(CardValues.Four);
            cards1.Add(c);
            c = new Club(CardValues.Five);
            cards1.Add(c);
            c = new Club(CardValues.Ace);
            cards1.Add(c);

            // Cards for player 2
            c = new Spade(CardValues.Two);
            cards2.Add(c);
            c = new Diamond(CardValues.Two);
            cards2.Add(c);
            c = new Spade(CardValues.King);
            cards2.Add(c);
            c = new Heart(CardValues.Five);
            cards2.Add(c);
            c = new Heart(CardValues.Ace);
            cards2.Add(c);

            players.Add(player1);
            players.Add(player2);

            FiveCardPokerGame game = new FiveCardPokerGame(players);
            
            player1.Hand = cards1;
            player2.Hand = cards2;

            Assert.That(game.GetRoundWinner(), Is.EqualTo(winningMsg));
            Assert.That(game.Winner, Is.EqualTo(player2));
        }

        [Test]
        public static void Test_Winners_From_Pair_Tie()
        {
            Player player1 = new Player("Ben");
            Player player2 = new Player("Neb");
            List<Player> players = new List<Player>();
            string winningMsg = "Looks like we have a tie that round between:  Ben Neb with a One Pair";

            var cards1 = new List<ICard>();
            var cards2 = new List<ICard>();
            ICard c;

            // Cards for player 1
            c = new Heart(CardValues.Two);
            cards1.Add(c);
            c = new Club(CardValues.Two);
            cards1.Add(c);
            c = new Club(CardValues.Four);
            cards1.Add(c);
            c = new Club(CardValues.Five);
            cards1.Add(c);
            c = new Club(CardValues.Ace);
            cards1.Add(c);

            // Cards for player 2
            c = new Spade(CardValues.Two);
            cards2.Add(c);
            c = new Diamond(CardValues.Two);
            cards2.Add(c);
            c = new Spade(CardValues.Four);
            cards2.Add(c);
            c = new Heart(CardValues.Five);
            cards2.Add(c);
            c = new Heart(CardValues.Ace);
            cards2.Add(c);

            players.Add(player1);
            players.Add(player2);

            FiveCardPokerGame game = new FiveCardPokerGame(players);

            player1.Hand = cards1;
            player2.Hand = cards2;

            Assert.That(game.GetRoundWinner(), Is.EqualTo(winningMsg));
            Assert.That(game.TiedPlayers.Contains(player1) &&
                game.TiedPlayers.Contains(player2) &&
                game.TiedPlayers.Count == 2, Is.True);
        }

        #endregion

        #region Test High Card

        [Test]
        public static void Test_Winner_From_High_Card()
        {
            Player player1 = new Player("Ben");
            Player player2 = new Player("Neb");
            List<Player> players = new List<Player>();
            string winningMsg = "Congratulations! The winner this round is: Neb with High Card";

            var cards1 = new List<ICard>();
            var cards2 = new List<ICard>();
            ICard c;

            // Cards for player 1
            c = new Heart(CardValues.Two);
            cards1.Add(c);
            c = new Club(CardValues.Three);
            cards1.Add(c);
            c = new Club(CardValues.Four);
            cards1.Add(c);
            c = new Club(CardValues.Five);
            cards1.Add(c);
            c = new Club(CardValues.Ace);
            cards1.Add(c);

            // Cards for player 2
            c = new Club(CardValues.Six);
            cards2.Add(c);
            c = new Heart(CardValues.Three);
            cards2.Add(c);
            c = new Heart(CardValues.Four);
            cards2.Add(c);
            c = new Heart(CardValues.Five);
            cards2.Add(c);
            c = new Heart(CardValues.Ace);
            cards2.Add(c);

            players.Add(player1);
            players.Add(player2);

            FiveCardPokerGame game = new FiveCardPokerGame(players);

            player1.Hand = cards1;
            player2.Hand = cards2;
            Assert.That(game.GetRoundWinner(), Is.EqualTo(winningMsg));
            Assert.That(game.Winner, Is.EqualTo(player2));
        }

        [Test]
        public static void Test_Winners_From_High_Card_Tie()
        {
            Player player1 = new Player("Ben");
            Player player2 = new Player("Neb");
            List<Player> players = new List<Player>();
            string winningMsg = "Looks like we have a tie that round between:  Ben Neb with a High Card";

            var cards1 = new List<ICard>();
            var cards2 = new List<ICard>();
            ICard c;

            // Cards for player 1
            c = new Heart(CardValues.Two);
            cards1.Add(c);
            c = new Club(CardValues.Three);
            cards1.Add(c);
            c = new Club(CardValues.Four);
            cards1.Add(c);
            c = new Club(CardValues.Five);
            cards1.Add(c);
            c = new Club(CardValues.Ace);
            cards1.Add(c);

            // Cards for player 2
            c = new Spade(CardValues.Two);
            cards2.Add(c);
            c = new Diamond(CardValues.Three);
            cards2.Add(c);
            c = new Spade(CardValues.Four);
            cards2.Add(c);
            c = new Spade(CardValues.Five);
            cards2.Add(c);
            c = new Spade(CardValues.Ace);
            cards2.Add(c);

            players.Add(player1);
            players.Add(player2);

            FiveCardPokerGame game = new FiveCardPokerGame(players);
            
            player1.Hand = cards1;
            player2.Hand = cards2;

            Assert.That(game.GetRoundWinner(), Is.EqualTo(winningMsg));
            Assert.That(game.TiedPlayers.Contains(player1) &&
                game.TiedPlayers.Contains(player2) &&
                game.TiedPlayers.Count == 2, Is.True);
        }

        #endregion

        [Test]
        public static void Test_Show_Hands()
        {
            Player player1 = new Player("Ben");
            List<Player> players = new List<Player>();
            string msg = "Player Ben's hand is:\nFive ♦    Six ♥    Seven ♦    Eight ♠    Ace ♥";

            var cards1 = new List<ICard>();
            ICard c;

            // Cards for player 1
            c = new Diamond(CardValues.Five);
            cards1.Add(c);
            c = new Heart(CardValues.Six);
            cards1.Add(c);
            c = new Diamond(CardValues.Seven);
            cards1.Add(c);
            c = new Spade(CardValues.Eight);
            cards1.Add(c);
            c = new Heart(CardValues.Ace);
            cards1.Add(c);

            players.Add(player1);

            FiveCardPokerGame game = new FiveCardPokerGame(players);

            player1.Hand = cards1;
            Assert.That(game.ShowHands().Trim(), Is.EqualTo(msg.Trim()));
        }
    }
}
