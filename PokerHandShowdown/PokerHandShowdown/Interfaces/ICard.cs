using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerHandShowdown
{
    /// <summary>
    /// ICard - A Interce used for representing the diffrent kinds of playing cards ex. Ace of Spaces, Three of Clubs.
    /// 
    /// Date Created:   December. 16th, 2019
    /// 
    /// @author: Benjamin Walstra
    /// @version 1.0
    /// </summary>\
    public interface ICard
    {
        Suits Suits { get; set; }
        CardValues CardValue { get; set; }
        int CompareTo(ICard other);
    }
}
