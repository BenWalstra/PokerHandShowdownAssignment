using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerHandShowdown
{
    /// <summary>
    /// Game - A class for representing the game of 5 card poker.
    /// 
    /// Date Created:   December. 16th, 2019
    /// 
    /// @author: Benjamin Walstra
    /// @version 1.0
    /// </summary>\
    public class FiveCardPokerGame : ICheckCards
    {
        private bool _wonWithHighCard = false;
        public List<Player> Players { get; set; }
        public List<Player> TiedPlayers { get; set; }
        public Player Winner { get; set; }
        public Deck Deck { get; set; }

        public FiveCardPokerGame(List<Player> players)
        {
            Players = players;
            TiedPlayers = new List<Player>();
        }

        /// <summary>
        /// Creates new Deck and assigns 5 random cards to all players
        /// </summary>
        public void BeginRound()
        {
            Deck = new Deck();
            foreach (var player in Players)
            {
                player.DealHand(Deck.CompleteDeck);
            }
        }

        /// <summary>
        /// Returns all users hands.
        /// </summary>
        public string ShowHands()
        {
            string playerHands = string.Empty;
            foreach (var p in Players)
            {
                playerHands += $"{p.ToString()}\n\n";
            }
            return playerHands;
        }

        #region Check Round Winner

        /// <summary>
        /// Determins the round winner/winners and returns their name/names and how they won
        /// as a formated message.
        /// </summary>
        public string GetRoundWinner()
        {
            string winningMessage = string.Empty;
            if (CheckFlush())
            {
                winningMessage = FindWinningMessage("Flush");
            }
            else if (CheckThreeOfKind())
            {
                winningMessage = FindWinningMessage("Three of a Kind");
            }
            else if (CheckOnePair())
            {
                winningMessage = FindWinningMessage("One Pair");
            }
            else
            {
                List<Player> winners = CheckHighCardPlayer(Players);
                if(winners.Count == 1)
                {
                    Winner = winners.SingleOrDefault();
                } else
                {
                    TiedPlayers = winners;
                }
                winningMessage = FindWinningMessage("High Card");
            }

            // Checks to see if the user tied with a flush, three of a kind or one pair
            // but had the higher card.
            if (_wonWithHighCard)
            {
                winningMessage += " and High Card";
            }
            return winningMessage;
        }
        public bool CheckFlush()
        {
            List<Player> playersWithFlush = Players.Where(x => x.CheckFlush() == true).ToList();

            if (playersWithFlush.Count == 0) return false;

            if (playersWithFlush.Count == 1)
            {
                Winner = playersWithFlush.SingleOrDefault();
                return true;
            }
            DetermineWinnersWithHighCard(CheckHighCardPlayer(playersWithFlush));
            return true;
        }
        public bool CheckThreeOfKind()
        {
            List<Player> playersWithThreeOfaKind= Players.Where(x => x.CheckThreeOfKind() == true).ToList();

            if (playersWithThreeOfaKind.Count == 0) return false;
            if (playersWithThreeOfaKind.Count == 1)
            {
                Winner = playersWithThreeOfaKind.SingleOrDefault();
                return true;
            }

            List<Player> playersWithHighestThreeOfaKind = new List<Player>();
            int sum = 0;
            foreach (var player in playersWithThreeOfaKind)
            {
                if(player.ThreeOfaKindSum >= sum)
                {
                    if(player.ThreeOfaKindSum > sum)
                    {
                        playersWithHighestThreeOfaKind.Clear();
                    }
                    playersWithHighestThreeOfaKind.Add(player);
                    sum = player.ThreeOfaKindSum;
                } 
            }

            // If theres only 1 player left then their 3 of a kind is higher so they win.
            // This is always the case the check is done to meet assignmnet requirements.
            if(playersWithHighestThreeOfaKind.Count == 1)
            {
                Winner = playersWithHighestThreeOfaKind.SingleOrDefault();
            } else
            {
                DetermineWinnersWithHighCard(CheckHighCardPlayer(playersWithHighestThreeOfaKind));
            }
            
            return true;
        }
        public bool CheckOnePair()
        {
            List<Player> playersWithOnePair = Players.Where(x => x.CheckOnePair() == true).ToList();

            if (playersWithOnePair.Count == 0) return false;
            if (playersWithOnePair.Count == 1)
            {
                Winner = playersWithOnePair.SingleOrDefault();
                return true;
            }

            List<Player> playersWithHighestOnePair = new List<Player>();
            int sum = 0;
            foreach (var player in playersWithOnePair)
            {
                if (player.OnePairSum >= sum)
                {
                    if (player.OnePairSum > sum)
                    {
                        playersWithHighestOnePair.Clear();
                    }
                    playersWithHighestOnePair.Add(player);
                    sum = player.OnePairSum;
                }
            }

            // If theres only 1 player left then their One pair is higher so they win.
            if (playersWithHighestOnePair.Count == 1)
            {
                Winner = playersWithHighestOnePair.SingleOrDefault();
            }
            else
            {
                DetermineWinnersWithHighCard(CheckHighCardPlayer(playersWithHighestOnePair));
            }
            return true;
        }

        /// <summary>
        /// Recursive function to get the highest card out of a list of players and assign the winner/winners
        /// </summary>
        private List<Player> CheckHighCardPlayer(List<Player> players)
        {
            if (players.Count == 1) return players;
            if (players.Count(x => !x.Hand.Any()) == players.Count) return players;

            var max = players.Max(x => x.HighCard.CardValue);
            players = players.Where(x => x.HighCard.CardValue == max).ToList();
            if (players.Count == 1) return CheckHighCardPlayer(players);

            foreach (var item in players)
            {
                var lastValue = item.Hand.LastOrDefault();
                item.Hand.Remove(lastValue);
            }
            return CheckHighCardPlayer(players);
        }

        private void DetermineWinnersWithHighCard(List<Player> winners)
        {
            if (winners.Count == 1)
            {
                Winner = winners.SingleOrDefault();
                _wonWithHighCard = true;
            }
            else
            {
                TiedPlayers = winners;
            }

        }

        #endregion

        #region Helpers
        public string FindWinningMessage(string winType)
        {
            string msg = string.Empty;
            if (Winner != null)
            {
                msg = BuildWinningMessage(Winner, winType);
            }
            else
            {
                msg = BuildWinningMessage(TiedPlayers, winType);
            }
            return msg;
        }
        public string BuildWinningMessage(Player winner, string winType)
        {
            return $"Congratulations! The winner this round is: {winner.Name} with {winType}";
        }
        public string BuildWinningMessage(List<Player> winners, string winType)
        {
            string msg = "Looks like we have a tie that round between: ";
            string winnerNames = string.Empty;
            foreach (var winner in winners)
            {
                winnerNames += $"{winner.Name} ";
            }
            winnerNames.Trim();
            return $"{msg} {winnerNames}with a {winType}";
        }
        #endregion
    }
}
