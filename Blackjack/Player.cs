namespace Blackjack
{
    public class Player
    {
        public string Name { get; set; } = string.Empty;
        public List<Card> Cards = new List<Card>();

        public int Score
        {
            get
            {
                int score = Cards.Sum(card => card.Value);
                int aceCount = Cards.Count(card => card.Rank == "A");

                while (score > 21 && aceCount > 0)
                {
                    score -= 10;
                    aceCount--;
                }

                return score;
            }
        }

        public void Hit()
        {
            Console.WriteLine(Name + " Hit!");

            Cards.Add(GameManager.Instance.Deck.Draw());
            ShowPlayerCards();

            if (Score > 21)
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
