using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerHandShowdown
{
    /// <summary>
    /// ValidateFiveCardPokerGame - A static class for validating the base requirements for Five Card Poker.
    /// 
    /// Date Created:   December. 18th, 2019
    /// 
    /// @author: Benjamin Walstra
    /// @version 1.0
    /// </summary>\
    public static class ValidateFiveCardPokerGame
    {
        public static bool ValidateNumberOfPlayers(string input)
        {
            bool isValid = true;
            int numberOfPlayers = 0;

            int.TryParse(input, out numberOfPlayers);
            if(numberOfPlayers <= 1 || numberOfPlayers > 6) isValid = false;
            return isValid;
        }
        public static bool ValidateUniqueUserNames(string name, List<Player> players)
        {
            bool isValid = true;
            if (players.Count(x => x.Name.ToUpper() == name.ToUpper()) == 1) isValid = false;
            return isValid;
        }
    }
}
