using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerHandShowdown
{
    /// <summary>
    /// EnumHelpers - A static class used for helper methods regarding enums.
    /// 
    /// Date Created:   December. 16th, 2019
    /// 
    /// @author: Benjamin Walstra
    /// @version 1.0
    /// </summary>\
    static class EnumHelpers
    {
        /// <summary>
        /// Returns list of all Enum values for a specific Enum
        /// </summary>
        /// <typeparam name="T"> Type of Enum</typeparam>
        /// <returns> an Enumerable of the specified Enum</returns>
        public static IEnumerable<T> GetEnumValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}
