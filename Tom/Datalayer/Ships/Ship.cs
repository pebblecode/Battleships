using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datalayer.Ships
{
    public abstract class Ship
    {
        public Ship() { }

        public List<BoardCell> Cells = new List<BoardCell>();

        public List<BoardCell> Hits = new List<BoardCell>();

        public bool IsSunk()
        {
            //Has no cells that are not hit
            var notHit = Cells.Where(z => !Hits.Contains(z));
            return !notHit.Any();
        }

        public bool IsColliding(Ships.Ship input)
        {
            return this.Cells.Any(z => input.Cells.Contains(z));
        }
    }
}
