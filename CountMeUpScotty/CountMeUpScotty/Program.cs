using System;

using CountMeUpScottyLibrary;

namespace CountMeUpScotty
{
    class Program
    {
        static void Welcome()
        {
          Console.WriteLine("Welcome to Count Me Up Scotty");
          Console.WriteLine("This game challenges kids to learn how to do basic Math");
        }

        static Player CreatePlayer() {
          Console.Write("Please enter your name: ");
          string name = Console.ReadLine();
          return new Player(name);
        }

        static void Main(string[] args)
        {
          Welcome();
          Player player = CreatePlayer();
          Game game = new Game(player);

          while (!game.IsFinished()) {
            SumChallenge challenge = game.NextChallenge();
            Console.WriteLine(challenge);

            int playerAttempt = Convert.ToInt32(Console.ReadLine());
            challenge.Solve(playerAttempt);
          }

          Console.Write(game.Overview());
        }
    }
}
