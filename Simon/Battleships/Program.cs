using System;

namespace Battleships
{
    class Program
    {
        static void Main()
        {
            var game = new BattleShipGame(10, 10, new Battleship(), new Destroyer(), new Destroyer());

            game.OnShipHit += s => Console.WriteLine("Hit!");
            game.OnShipSank += s => Console.WriteLine("You sank my {0}", s);

            Console.WriteLine("Enter command:");

            while (game.Running())
            {
                game.Command(Console.ReadLine());
            }

            Console.WriteLine("You won");
        }
    }
}
