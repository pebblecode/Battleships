namespace Battleships
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PossibleShipPosition
    { 
        public int ShipLength { get; private set; }

        // todo should this be a list?
        public IList<Position> RequiredPositions { get; private set; }

        public PossibleShipPosition(Position origin, int length, Direction direction)
        {
            if (length <= 0)
            {
                throw new ArgumentOutOfRangeException("length", "Ships must be 1 or more units in length.");
            }

            this.ShipLength = length;

            this.RequiredPositions = new List<Position>(length);
            if (direction == Direction.Y)
            {
                for (int y = origin.PosY; y < (length + origin.PosY); y++)
                {
                    this.RequiredPositions.Add(new Position(origin.PosX, y));
                }
            }
            else
            {

                for (int x = origin.PosX; x < (length + origin.PosX); x++)
                {
                    this.RequiredPositions.Add(new Position(x, origin.PosY));
                }
            }
        }

        public bool IsUsingAnyOfPositions(IEnumerable<Position> positions)
        {
            if (positions == null)
            {
                return false;
            }

            return this.RequiredPositions.Intersect(positions).Any();
        }
    }
}