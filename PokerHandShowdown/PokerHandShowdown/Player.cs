using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerHandShowdown
{
    /// <summary>
    /// Player - A class for representing players in a card game.
    /// 
    /// Date Created:   December. 16th, 2019
    /// 
    /// @author: Benjamin Walstra
    /// @version 1.0
    /// </summary>\
    public class Player : ICheckCards
    {
        // Holds the card value of Three of a kind. Used to make handling finding highest three of a kind easier.
        public int threeOfaKindSum = 0;
        // Holds the card value of One Pair. Used to make handiling finding highest One Pair easier.
        public int onePairSum = 0;

        public string Name { get; private set; }
        public List<ICard> Hand { get; set; }
        public ICard HighCard
            => CheckHighCard(Hand);

        public Player(string name)
        {
            Name = name;
            Hand = new List<ICard>();
        }

        /// <summary>
        /// Assigns player 5 cards from passed in playing cards.
        /// </summary>
        public void DealHand(List<ICard> hand)
        {
            Hand.Clear();
            Hand = hand;
            OrderHand();
        }

        /// <summary>
        /// Orders users hand from lowest value card to highest.
        /// </summary>
        public void OrderHand()
        {
            if (Hand.Count <= 0) throw new ApplicationException("Player must have a Hand before it can be ordered");
            Hand = Hand.OrderBy(x => x.CardValue).ToList();
        }


        #region Check 5 card Poker Hands

        /// <summary>
        /// Check if user has a flush.
        /// </summary>
        public bool CheckForFlush()
        {
            Suits suit = Hand.FirstOrDefault().Suits;
            int count = Hand.Count(x => x.Suits == suit);
            if (count == 5) return true;
            return false;
        }

        /// <summary>
        /// Check if user has three of a kind.
        /// </summary>
        public bool CheckForThreeOfaKind()
        {
            // Only need to check first 3 cards
            foreach (var item in Hand.Take(3))
            {
                int count = Hand.Count(x => x.CardValue == item.CardValue);
                if (count >= 3)
                {
                    threeOfaKindSum = (int)item.CardValue;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Check if user has one pair.
        /// </summary>
        public bool CheckForOnePair()
        {
            // Only need to check first 4 cards
            foreach (var item in Hand.Take(4))
            {
                int count = Hand.Count(x => x.CardValue == item.CardValue);
                if (count == 2)
                {
                    onePairSum = (int)item.CardValue;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Check for users highest card.
        /// </summary>
        private ICard CheckHighCard(List<ICard> cards)
        {
            if (Hand.Count <= 0) throw new ApplicationException("Player must have a Hand before you can get High Card");
            return cards.LastOrDefault();
        }
            
        #endregion


        public override string ToString()
        {
            string cards = string.Empty;
            foreach (var item in Hand)
            {
                cards += $"{item.ToString()}    ";
            }
            string output = $"Player {Name}'s hand is:\n{cards}\n";
            return output;
        }
    }
}
