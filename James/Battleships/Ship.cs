namespace Battleships
{
    using System;
    
    public class Ship : IShip
    {
        public Ship(string shipName, int length)
        {
            if (shipName == string.Empty)
            {
                throw new ArgumentOutOfRangeException("shipName", "Ships must have a name.");
            }

            if (length < 1)
            {
                throw new ArgumentOutOfRangeException("length", "Ships must be 1 or more units in length.");
            }

            this.shipName = shipName;
            this.Length = length;
            this.remainingLives = length;

        }

        private readonly string shipName;

        private int remainingLives;
 
        public int Length{ get; private set; }

        public string LoseLife()
        {
            this.remainingLives--;

            if (this.IsSunk)
            {
                return string.Format("You sunk my {0}!", this.shipName);
            }

            return "A good Hit! But we're still afloat!";

        }

        public bool IsSunk
        {
            get
            {
                return this.remainingLives <= 0;
            }
        }
    }
}