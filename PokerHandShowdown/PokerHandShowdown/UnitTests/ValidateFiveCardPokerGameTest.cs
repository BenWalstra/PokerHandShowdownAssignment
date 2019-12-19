using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System;
using PokerHandShowdown;

namespace TestLibrary
{
    /// <summary>
    /// ValidateFiveCardPokerGameTest - A class for testing the ValidateFiveCardPokerGame class
    /// ValidateFiveCardPokerGame - A static class for validating the start of a five card poker game.
    /// 
    /// Date Created:   December. 18th, 2019
    /// 
    /// @author: Benjamin Walstra
    /// @version 1.0
    /// </summary>\
    [TestFixture]
    public class ValidateFiveCardPokerGameTest
    {
        [Test]
        public static void Test_Validate_Number_Of_Players_Int_Is_True()
        {
            bool result = ValidateFiveCardPokerGame.ValidateNumberOfPlayers(4);
            Assert.IsTrue(result);
        }

        [Test]
        public static void Test_Validate_Number_Of_Players_Int_Is_False()
        {
            bool result = ValidateFiveCardPokerGame.ValidateNumberOfPlayers(0);
            Assert.IsFalse(result);
            result = ValidateFiveCardPokerGame.ValidateNumberOfPlayers(7);
            Assert.IsFalse(result);
        }

        [Test]
        public static void Test_Validate_Number_Of_Players_String_Is_True()
        {
            bool result = ValidateFiveCardPokerGame.ValidateNumberOfPlayers("4");
            Assert.IsTrue(result);
        }

        [Test]
        public static void Test_Validate_Number_Of_Players_String_Is_False()
        {
            bool result = ValidateFiveCardPokerGame.ValidateNumberOfPlayers("0");
            Assert.IsFalse(result);
            result = ValidateFiveCardPokerGame.ValidateNumberOfPlayers("7");
            Assert.IsFalse(result);
        }

        [Test]
        public static void Test_Validate_Uniqe_UserName_Is_True()
        {
            Player player1 = new Player("Ben");
            Player player2 = new Player("Tim");
            Player player3 = new Player("Jim");

            List<Player> players = new List<Player> { player1, player2, player3 };
            bool result = ValidateFiveCardPokerGame.ValidateUniqueUserNames("Scara",players);
            Assert.IsTrue(result);
        }

        [Test]
        public static void Test_Validate_Uniqe_UserName_Is_False()
        {
            Player player1 = new Player("Ben");
            Player player2 = new Player("Tim");
            Player player3 = new Player("Jim");

            List<Player> players = new List<Player> { player1, player2, player3 };
            bool result = ValidateFiveCardPokerGame.ValidateUniqueUserNames("Ben", players);
            Assert.IsFalse(result);
        }
    }
}
