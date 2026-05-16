using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class Player
    {
        public string Name { get; set; }
        public List<Card> Cards = new List<Card>();

        public void Hit()
        {
            Console.WriteLine(Name + " Hit!");

            Cards.Add(GameManager.Instance.Deck.Draw());
            ShowPlayerCards();

            int score = 0;
            foreach (var card in Cards)
            {
                score += card.Value;
            }
            if (score > 21)
            {
                GameManager.Instance.GameEnd();
            }
        }

        public void Stand()
        {
            Console.WriteLine(Name + " Stand!");
            GameManager.Instance.ChangeTurnOfPlayer(this);
        }

        public void ShowPlayerCards()
        {
            if (Name == "User")
            {
                Console.WriteLine($"현재 당신의 패: ");

                foreach (var card in Cards)
                {
                    Console.Write($"{card.Suit} {card.Rank}\n");
                }
            }
            else if (Name == "Dealer")
            {
                Console.Write($"현재 딜러의 패: \n{Cards[0].Suit} {Cards[0].Rank}\n");
            }
        }
    }
}
