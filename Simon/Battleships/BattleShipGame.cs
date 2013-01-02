using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Battleships
{
    public class BattleShipGame
    {
        private readonly IList<Ship> ships;
        private readonly int width;
        private readonly int height;

        public BattleShipGame(int width, int height, params Ship[] ships)
        {
            this.width = width;
            this.height = height;
            this.ships = ships;
            foreach (var ship in ships)
            {
                ship.GeneratePosition(width, height, ships);
            }
        }

        public bool Running()
        {
            return !ships.All(s => s.HasSunk);
        }

        public void Command(string command)
        {
            FireAtPosition(ParseCommand(command.ToUpper()));
        }

        public bool FireAtPosition(Point target)
        {
            return ships.Any(ship => ship.Fire(target));
        }

        public Point ParseCommand(string command)
        {
            var x = command[0] - 65;
            var y = int.Parse(command[1].ToString(CultureInfo.InvariantCulture));

            if (x < 0 || x >= this.width || y < 0 || y >= this.height)
                throw new ArgumentOutOfRangeException("command");

            return new Point(x, y);
        }

        public event Action<Ship> OnShipSank
        {
            add
            {
                foreach (var ship in ships)
                {
                    ship.OnSank += value;
                }
            }
            remove
            {
                foreach (var ship in ships)
                {
                    ship.OnSank -= value;
                }
            }
        }

        public event Action<Ship> OnShipHit
        {
            add
            {
                foreach (var ship in ships)
                {
                    ship.OnHit += value;
                }
            }
            remove
            {
                foreach (var ship in ships)
                {
                    ship.OnHit -= value;
                }
            }
        }
    }
}
