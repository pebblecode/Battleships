namespace Battleships
{
    using System;

    public class Game
    {

        public Game()
        {
            Console.WriteLine("Welcome to battleship. Press any key to start a game!");
            Console.ReadKey();
            this.GameLoop();
        }

        static void Main(string[] args)
        {
            Game game = new Game();
        }

        public void GameLoop()
        {
            var grid = new Grid(
                10,
                10,
                ShipFactory.CreateBattleship(),
                ShipFactory.CreateBattleship(),
                ShipFactory.CreateDestroyer(),
                ShipFactory.CreateDestroyer(),
                ShipFactory.CreateDestroyer(),
                ShipFactory.CreateDestroyer());

            Console.WriteLine("The Game is afoot!");
            while (grid.AllShipsSunk() == false)
            {
                Console.WriteLine("Enter a target: ");
                try
                {
                    var position = new Position(Console.ReadLine());
                    Console.WriteLine(grid.Attack(position));
                }
                catch (ArgumentOutOfRangeException exception)
                {
                    Console.WriteLine(exception.Message);
                }
                
            }

            Console.WriteLine("I surrender you win!");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
