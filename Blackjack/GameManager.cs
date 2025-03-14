using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class GameManager
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
}
