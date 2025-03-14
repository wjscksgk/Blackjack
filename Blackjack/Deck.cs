using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class Deck
    {
        private List<Card> _cards;
        private static Random _random = new Random();

        public Deck() => Init();

        private void Init()
        {
            _cards = new List<Card>();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    Card card = new Card
                    {
                        Suit = (CardSuitEnum)i,
                        Rank = (j + 2).ToString(),
                        Value = j + 2
                    };

                    switch (j)
                    {
                        case 9:
                            card.Rank = "J";
                            card.Value = 10;
                            break;
                        case 10:
                            card.Rank = "Q";
                            card.Value = 10;
                            break;
                        case 11:
                            card.Rank = "K";
                            card.Value = 10;
                            break;
                        case 12:
                            card.Rank = "A";
                            card.Value = 11;
                            break;
                    }

                    _cards.Add(card);
                }
            }
        }

        public void Shuffle()
        {
            for (int i = _cards.Count - 1; i > 0; i--)
            {
                int j = _random.Next(i + 1);
                (_cards[i], _cards[j]) = (_cards[j], _cards[i]);
            }
        }

        public Card Draw()
        {
            Card card = _cards[0];
            _cards.RemoveAt(0);
            return card;
        }
    }
}
