using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerHandShowdown
{
    /// <summary>
    /// ICheckCards - A Interce used for representing objects that need to check for hand types.
    /// 
    /// Date Created:   December. 16th, 2019
    /// 
    /// @author: Benjamin Walstra
    /// @version 1.0
    /// </summary>\
    interface ICheckCards
    {
        bool CheckOnePair();
        bool CheckThreeOfKind();
        bool CheckFlush();
    }
}
