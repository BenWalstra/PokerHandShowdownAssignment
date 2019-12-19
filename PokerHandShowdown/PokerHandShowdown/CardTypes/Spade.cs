using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerHandShowdown
{
    /// <summary>
    /// Spade - A class for representing all cards that are spades.
    /// 
    /// Date Created:   December. 16th, 2019
    /// 
    /// @author: Benjamin Walstra
    /// @version 1.0
    /// </summary>\
    public class Spade : ICard, IComparable<ICard>
    {
        public Suits Suits { get; set; }
        public CardValues CardValue { get; set; }

        public Spade(CardValues cardValue)
        {
            Suits = Suits.Spade;
            CardValue = cardValue;
        }

        public override string ToString()
            => $"{CardValue} {('\u2660').ToString()}";

        public int CompareTo(ICard other)
            => (this.Suits.CompareTo(other.Suits) == 0 &&
            this.CardValue.CompareTo(other.CardValue) == 0) ? 0 : -1;
    }
}
