using System;

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

class GameManager
{
    private GameManager() { }

    private static readonly GameManager _instance = new GameManager();
    public static GameManager Instance => _instance;
    public Deck Deck;
    private Player _currentTurnPlayer { get; set; }
    private List<Player> _players;
    public bool IsGameEnd;

    public void GameStart(Player player1, Player player2)
    {
        _currentTurnPlayer = player1;
        _players = new List<Player>
        {
            player1,
            player2
        };
        Deck = new Deck();
        Deck.Shuffle();

        for (int i = 0; i < 2; i++)
        {
            player1.Cards.Add(Deck.Draw());
            player2.Cards.Add(Deck.Draw());
        }

        player1.ShowPlayerCards();
        player2.ShowPlayerCards();

        while (!IsGameEnd)
        {
            Console.WriteLine($"{_currentTurnPlayer.Name} 턴.");

            if (GameManager.Instance._currentTurnPlayer.Name == "User")
            {
                ConsoleKey behavior = Console.ReadKey(true).Key;

                switch (behavior)
                {
                    case ConsoleKey.H:
                        _currentTurnPlayer.Hit();
                        break;
                    case ConsoleKey.S:
                        _currentTurnPlayer.Stand();
                        break;
                }
            }
            else if (GameManager.Instance._currentTurnPlayer.Name == "Dealer")
            {
                Player dealer = _players.Find(p => p.Name == "Dealer")!;

                int num = 0;
                foreach (var card in dealer.Cards)
                {
                    num += card.Value;
                }

                if (num >= 17)
                {
                    dealer.Stand();
                    GameEnd();
                }
                else if (num <= 16)
                {
                    dealer.Hit();
                }
            }
        }
    }

    public void GameEnd()
    {
        int userScore = 0, dealerScore = 0;

        foreach (var card in _players[0].Cards)
        {
            userScore += card.Value;
        }

        foreach (var card in _players[1].Cards)
        {
            dealerScore += card.Value;
        }

        if (userScore > 21)
        {
            Console.WriteLine($"{_players[0].Name} Bust... {userScore}");
        }
        else if (dealerScore > 21)
        {
            Console.WriteLine($"{_players[1].Name} Bust... {dealerScore}");
        }
        else
        {
            if (userScore > dealerScore)
            {
                Console.WriteLine($"☆☆☆ {_players[0].Name} 승리! ☆☆☆");
            }
            else if (dealerScore > userScore)
            {
                Console.WriteLine($"☆☆☆ {_players[1].Name} 승리! ☆☆☆");
            }
            else
            {
                Console.WriteLine("☆☆☆ 무승부! ☆☆☆");
            }
        }

        PrintPlayerCards(_players[0]);
        PrintPlayerCards(_players[1]);

        IsGameEnd = true;
    }

    private void PrintPlayerCards(Player player)
    {
        Console.WriteLine(player.Name + "의 카드: ");
        foreach (var card in player.Cards)
        {
            Console.WriteLine($"{card.Suit} {card.Rank}");
        }
    }

    public void ChangeTurnOfPlayer(Player current)
    {
        foreach (var player in _players)
        {
            if (player != current)
            {
                _currentTurnPlayer = player;
                return;
            }
        }
    }
}

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

class Program
{
    static void Main()
    {
        while (true)
        {
            GameManager.Instance.IsGameEnd = false;

            Player user = new Player { Name = "User" };
            Player dealer = new Player { Name = "Dealer" };

            GameManager.Instance.GameStart(user, dealer);

            Console.WriteLine("다시 시작하려면 Y를 누르세요. 종료하려면 아무 키나 누르세요.");
            if (Console.ReadKey(true).Key != ConsoleKey.Y)
            {
                return;
            }

            Console.Clear();
        }
    }
}