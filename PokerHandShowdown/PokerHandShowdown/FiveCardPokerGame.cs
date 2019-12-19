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
        public List<Player> Players { get; private set; }
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
                Random rnd = new Random();
                List<ICard> hand = new List<ICard>();

                for (int i = 0; i < 5; i++)
                {
                    ICard card = Deck.CompleteDeck[rnd.Next(Deck.CompleteDeck.Count)];
                    hand.Add(card);
                    Deck.CompleteDeck.Remove(card);
                }
                player.DealHand(hand);
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
            if (CheckForFlush())
            {
                winningMessage = FindWinningMessage("Flush");
            }
            else if (CheckForThreeOfaKind())
            {
                winningMessage = FindWinningMessage("Three of a Kind");
            }
            else if (CheckForOnePair())
            {
                winningMessage = FindWinningMessage("One Pair");
            }
            else
            {
                //Determines the player/players with the Highest Single card.
                List<Player> winners = GetHighCardPlayers(Players);
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
            if (_wonWithHighCard) winningMessage += " and High Card";

            return winningMessage;
        }

        /// <summary>
        /// Checks to see if any players have a flush and determines winner/winners.
        /// </summary>
        public bool CheckForFlush()
        {
            List<Player> playersWithFlush = Players.Where(x => x.CheckForFlush() == true).ToList();

            if (playersWithFlush.Count == 0) return false;

            if (playersWithFlush.Count == 1)
            {
                Winner = playersWithFlush.SingleOrDefault();
                return true;
            }
            DetermineWinnersWithHighCard(GetHighCardPlayers(playersWithFlush));
            return true;
        }

        /// <summary>
        /// Checks to see if any players have three of a kind and determines winner/winners.
        /// </summary>
        public bool CheckForThreeOfaKind()
        {
            List<Player> playersWithThreeOfaKind = Players.Where(x => x.CheckForThreeOfaKind() == true).ToList();

            if (playersWithThreeOfaKind.Count == 0) return false;
            if (playersWithThreeOfaKind.Count == 1)
            {
                Winner = playersWithThreeOfaKind.SingleOrDefault();
                return true;
            }

            List<Player> playersWithHighestThreeOfaKind = new List<Player>();
            int sum = 0;

            // Checks to see who has highest card out of their Three of a Kind.
            foreach (var player in playersWithThreeOfaKind)
            {
                if(player.threeOfaKindSum >= sum)
                {
                    if(player.threeOfaKindSum > sum) playersWithHighestThreeOfaKind.Clear();

                    playersWithHighestThreeOfaKind.Add(player);
                    sum = player.threeOfaKindSum;
                } 
            }

            // If theres only 1 player left then their 3 of a kind is higher so they win.
            // This is always the case the check is done to meet assignment requirements.
            if(playersWithHighestThreeOfaKind.Count == 1)
            {
                Winner = playersWithHighestThreeOfaKind.SingleOrDefault();
            } 
            else
            {
                DetermineWinnersWithHighCard(GetHighCardPlayers(playersWithHighestThreeOfaKind));
            }
            
            return true;
        }

        /// <summary>
        /// Checks to see if any players have one pair and determines winner/winners.
        /// </summary>
        public bool CheckForOnePair()
        {
            List<Player> playersWithOnePair = Players.Where(x => x.CheckForOnePair() == true).ToList();

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
                if (player.onePairSum >= sum)
                {
                    if (player.onePairSum > sum)
                    {
                        playersWithHighestOnePair.Clear();
                    }
                    playersWithHighestOnePair.Add(player);
                    sum = player.onePairSum;
                }
            }

            // If theres only 1 player left then their One pair is higher so they win.
            if (playersWithHighestOnePair.Count == 1)
            {
                Winner = playersWithHighestOnePair.SingleOrDefault();
            }
            else
            {
                DetermineWinnersWithHighCard(GetHighCardPlayers(playersWithHighestOnePair));
            }
            return true;
        }

        #endregion


        #region Helpers
        /// <summary>
        /// Recursive function to get the highest card out of a list of players and assign the winner/winners
        /// </summary>
        private List<Player> GetHighCardPlayers(List<Player> players)
        {
            if (players.Count == 1) return players;

            // Checks to see if there are still players left but their hands are empty.
            // If this is the case then we have a tie.
            if (players.Count(x => !x.Hand.Any()) == players.Count) return players;

            // Gets the highest player card in the list of players.
            var max = players.Max(x => x.HighCard.CardValue);

            // Gets all players who also have the current highest card.
            players = players.Where(x => x.HighCard.CardValue == max).ToList();

            // If only one player left they win, return player.
            if (players.Count == 1) return players;

            // Hands are ordered by lowest to highest, we remove every players last card to move
            // onto the next highest card.
            foreach (var item in players)
            {
                var lastValue = item.Hand.LastOrDefault();
                item.Hand.Remove(lastValue);
            }
            return GetHighCardPlayers(players);
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
