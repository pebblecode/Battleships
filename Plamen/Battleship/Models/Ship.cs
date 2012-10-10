using System.Collections.Generic;
using System.Linq;

namespace Battleships.Models
{
    public class Ship
    {
        public Ship(string shipName, ShipType type)
        {
            ShipName = shipName;
            Type = type;
        }

        public string ShipName { get; private set; }
        public ShipType Type { get; private set; }
        public List<BoardSquare> ShipCoords { get; set; }

        public int Length
        {
            get { return ShipCoords.Count; }
        }

        public bool IsSinked
        {
            get { return ShipCoords.Count(bp => bp.IsHitted) == Length; }
        }

        public bool CheckItsFromTheShip(string square)
        {
            return ShipCoords.Count(c => c.Caption == square) > 0;
        }

        public void MarkAsHit(string square)
        {
            var point = ShipCoords.FirstOrDefault(c => c.Caption == square);

            if (point != null) point.IsHitted = true;
        }
    }
}
