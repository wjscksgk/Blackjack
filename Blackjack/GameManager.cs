namespace Blackjack
{
    public class GameManager
    {
        private GameManager() { }

        private static readonly GameManager _instance = new GameManager();
        public static GameManager Instance => _instance;
        public Deck Deck = new Deck();
        private Player _currentTurnPlayer { get; set; } = new Player();
        private List<Player> _players = new List<Player>();
        public bool IsGameEnd;

        public void GameStart(Player player1, Player player2)
        {
            IsGameEnd = false;
            Deck.Reset();
            player1.Cards.Clear();
            player2.Cards.Clear();
            _currentTurnPlayer = player1;
            _players = new List<Player>
        {
            player1,
            player2
        };
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

                if (_currentTurnPlayer.Name == "User")
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
                else if (_currentTurnPlayer.Name == "Dealer")
                {
                    Player dealer = _players.Find(p => p.Name == "Dealer")!;

                    int num = dealer.Score;

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
            int userScore = _players[0].Score, dealerScore = _players[1].Score;

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
}
