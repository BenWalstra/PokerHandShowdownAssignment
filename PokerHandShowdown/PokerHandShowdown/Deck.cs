using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerHandShowdown
{
    /// <summary>
    /// Deck - A class for representing the card Deck for a card game.
    /// 
    /// Date Created:   December. 16th, 2019
    /// 
    /// @author: Benjamin Walstra
    /// @version 1.0
    /// </summary>\
    public class Deck
    {
        public List<ICard> CompleteDeck { get; set; }

        public Deck()
        {
            CompleteDeck = new List<ICard>();
            BuildDeck();
        }

        /// <summary>
        /// Creates playing card deck with all specified cards inside the Suits and CardValue Enums
        /// </summary>
        private void BuildDeck()
        {
            foreach (var suit in EnumHelpers.GetEnumValues<Suits>())
            {
                foreach (var value in EnumHelpers.GetEnumValues<CardValues>())
                {
                    ICard card;
                    if(suit == Suits.Heart)
                    {
                        card = new Heart(value);
                    }else if(suit == Suits.Spade)
                    {
                        card = new Spade(value);
                    } else if(suit == Suits.Club)
                    {
                        card = new Club(value);
                    } else
                    {
                        card = new Diamond(value);
                    }
                    CompleteDeck.Add(card);
                }
            }
        }
    }
}
