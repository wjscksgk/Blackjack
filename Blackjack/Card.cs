using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class Card
    {
        public CardSuitEnum Suit { get; set; }
        public string Rank { get; set; }
        public int Value { get; set; }
    }

    public enum CardSuitEnum
    {
        Clubs = 0,
        Hearts = 1,
        Diamonds = 2,
        Spades = 3,
    }
}
