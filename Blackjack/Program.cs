namespace Blackjack;

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