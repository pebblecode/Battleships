using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datalayer.Ships
{
    public class Destroyer : Ship
    {
        public Destroyer(BoardCell startPosition, Enums.Orientation orientation)
        {
            if (orientation == Enums.Orientation.Horizontal)
            {
                this.Cells.Add(startPosition);

                //Destroyers have 4 cells  
                var next = startPosition.NextDown();
                while (this.Cells.Count < 4)
                {
                    this.Cells.Add(next);
                    next = next.NextDown();
                }

            }
            else if (orientation == Enums.Orientation.Vertical)
            {
                this.Cells.Add(startPosition);

                //Destroyers have 4 cells  
                var next = startPosition.NextAlong();
                while (this.Cells.Count < 4)
                {
                    this.Cells.Add(next);
                    next = next.NextAlong();
                }
            }
        }
    }
}
