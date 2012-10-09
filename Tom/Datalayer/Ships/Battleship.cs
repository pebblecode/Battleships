using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datalayer.Ships
{
    public class Battleship : Ship
    {
        public Battleship(BoardCell startPosition, Enums.Orientation orientation)
        {
            if (orientation == Enums.Orientation.Horizontal)
            {
                this.Cells.Add(startPosition);           

                //Battleships have 5 cells  
                var next = startPosition.NextDown();
                while (this.Cells.Count < 5)
                {
                    this.Cells.Add(next);
                    next = next.NextDown();
                }
  
            }
            else if (orientation == Enums.Orientation.Vertical)
            {
                this.Cells.Add(startPosition);                

                //Battleships have 5 cells
                var next = startPosition.NextAlong();
                while (this.Cells.Count < 5)
                {
                    this.Cells.Add(next);
                    next = next.NextAlong();
                }
            }
        }
    }
}
