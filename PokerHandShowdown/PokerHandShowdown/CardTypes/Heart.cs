using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerHandShowdown
{
    /// <summary>
    /// Heart - A class for representing all cards that are hearts.
    /// 
    /// Date Created:   December. 16th, 2019
    /// 
    /// @author: Benjamin Walstra
    /// @version 1.0
    /// </summary>\
    public class Heart : ICard
    {
        public Suits Suits { get; set; }
        public CardValues CardValue { get; set; }

        public Heart(CardValues cardValue)
        {
            Suits = Suits.Heart;
            CardValue = cardValue;
        }

        public override string ToString()
            => $"{CardValue} {('\u2665').ToString()}";

        public int CompareTo(ICard other)
            => (this.Suits.CompareTo(other.Suits) == 0 &&
            this.CardValue.CompareTo(other.CardValue) == 0) ? 0 : -1;
    }
}
