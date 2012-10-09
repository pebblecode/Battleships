using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datalayer;
namespace Battleships
{
    class Program
    {
        static void Main(string[] args)
        {
            Datalayer.Game game = new Game(new RealDisplay());

            while (!game.IsFinished())
            {
                Console.WriteLine("Enter a shot");
                game.AddInput(Console.ReadLine());
            }
            Console.WriteLine("Game over, you win!");
            Console.ReadLine();
        }
    }
}
