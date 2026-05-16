namespace Blackjack
{
    public class Card
    {
        public CardSuitEnum Suit { get; set; }
        public string Rank { get; set; } = string.Empty;
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
