using Battleships.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(10, 10, ShipDefinition.Battleship, ShipDefinition.Destroyer, ShipDefinition.Destroyer);

            Console.WriteLine("Game ready, (coordintes are one-based so 'A1' represents the origin square");

            do{

                Console.WriteLine("Enter coordinates");

                try
                {
                    HitResult result = game.PlayTurn(Console.ReadLine());

                    OutputResult(result);
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }
                catch (InvalidOperationException ioe)
                {
                    Console.WriteLine(ioe.Message);
                }

            }while(!game.IsEnded);
        }

        private static void OutputResult(HitResult result)
        {
            switch (result)
            {
                case HitResult.Hit:
                    Console.WriteLine("You hit a ship");
                    break;
                case HitResult.HitAndSink:
                    Console.WriteLine("You hit and sunk a ship");
                    break;
                case HitResult.AlreadyHit:
                    Console.WriteLine("You've already hit that part of the ship");
                    break;
                case HitResult.AlreadySunk:
                    Console.WriteLine("You've already sunk that ship");
                    break;
                case HitResult.Miss:
                    Console.WriteLine("You missed");
                    break;
                default:
                    throw new InvalidOperationException("Inexhaustive TurnResult processing");
            }
        }
    }
}
