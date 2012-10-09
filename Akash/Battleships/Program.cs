namespace PebbleCode.Interview.Battleships
{
    using System;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            MoveResult result;

            do
            {
                var legalMoves = game.LegalTargets;

                Console.WriteLine("Choose one of these moves: {0}", legalMoves);
                var userEntry = Console.ReadLine();

                var chosenMove = legalMoves.SingleOrDefault(legalMove => legalMove.Coordinates.Equals(userEntry, StringComparison.CurrentCultureIgnoreCase));

                result = game.Hit(chosenMove);
                Console.WriteLine(result);
            }
            while (result != MoveResult.GameOver);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
