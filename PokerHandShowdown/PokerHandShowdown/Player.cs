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
        public string Name { get; set; }
        public List<ICard> Hand { get; set; }

        /// <summary>
        /// Holds the card value of Three of a kind. Used to make handiling finding highcard
        /// when three are three of a kind easier.
        /// </summary>
        public int ThreeOfaKindSum { get; set; }

        /// <summary>
        /// Holds the card value of One Pair. Used to make handiling finding highcard
        /// when three is One Pair easier.
        /// </summary>
        public int OnePairSum { get; set; }
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

        public bool CheckFlush()
        {
            Suits suit = Hand.FirstOrDefault().Suits;
            int count = Hand.Count(x => x.Suits == suit);
            if (count == 5) return true;
            return false;
        }

        public bool CheckThreeOfKind()
        {
            // Only ned to check first 3 cards
            foreach (var item in Hand.Take(3))
            {
                int count = Hand.Count(x => x.CardValue == item.CardValue);
                if (count >= 3)
                {
                    ThreeOfaKindSum = (int)item.CardValue;
                    return true;
                }
            }
            return false;
        }

        public bool CheckOnePair()
        {
            // Only ned to check first 4 cards
            foreach (var item in Hand.Take(4))
            {
                int count = Hand.Count(x => x.CardValue == item.CardValue);
                if (count == 2)
                {
                    OnePairSum = (int)item.CardValue;
                    return true;
                }
            }
            return false;
        }

        private ICard CheckHighCard(List<ICard> cards)
            => cards.LastOrDefault();

        public void OrderHand()
        {
            if (Hand.Count <= 0) throw new ApplicationException("Player must have a Hand before it can be ordered");
            Hand = Hand.OrderBy(x => x.CardValue).ToList();
        }

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
