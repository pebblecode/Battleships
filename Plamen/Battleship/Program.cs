using System;
using Battleships.Models;

namespace Battleships
{
    class Program
    {
        static void Main()
        {
            var battleShipGame = new Game();

            Console.WriteLine("***Battleships***");
            Console.Write("Strating...");
            var startResult = battleShipGame.Start();

            Console.WriteLine(startResult == ResultEnum.ProblemWithStarting
                                  ? "Problem while starting the game. Restart the game and try again."
                                  : "OK");


            while (!battleShipGame.IsOver)
            {
                Console.WriteLine("Enter square to shoot:");
                var point = Console.ReadLine();
                var result = battleShipGame.Shoot(point);

                //Small cheat to reveal the location of the ships in order to inspect is the game 
                //working correclty just enter "SECRET" on the command prompt
                if (result.ShootResult == ResultEnum.Secret)
                {
                    for (var i = 0; i < 3; i++)
                    {
                        if (battleShipGame.Ships[i].Type == ShipType.Battleship)
                        {
                            Console.WriteLine("{0} : {1},{2},{3},{4},{5}",
                              battleShipGame.Ships[i].ShipName,
                              battleShipGame.Ships[i].ShipCoords[0].Caption,
                              battleShipGame.Ships[i].ShipCoords[1].Caption,
                              battleShipGame.Ships[i].ShipCoords[2].Caption,
                              battleShipGame.Ships[i].ShipCoords[3].Caption,
                              battleShipGame.Ships[i].ShipCoords[4].Caption);

                        }
                        else
                        {
                            Console.WriteLine("{0} : {1},{2},{3},{4}", 
                                battleShipGame.Ships[i].ShipName,
                                battleShipGame.Ships[i].ShipCoords[0].Caption,
                                battleShipGame.Ships[i].ShipCoords[1].Caption,
                                battleShipGame.Ships[i].ShipCoords[2].Caption,
                                battleShipGame.Ships[i].ShipCoords[3].Caption);
                        }
                    }
                }

                Console.WriteLine(result.Message);
            }

            Console.ReadLine();
        }
    }
}
