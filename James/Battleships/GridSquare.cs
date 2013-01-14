namespace Battleships
{
    using System;

    public class GridSquare
    {
        private State state;

        private readonly IShip ship;

        public GridSquare(IShip ship)
        {
            this.state = State.Occupied;
            this.ship = ship;
        }

        public GridSquare()
        {
            this.state = State.NotOccupied;
        }

        public string Attack()
        {
            
            string returnMessage;
            switch (this.state)
            {
                case State.Occupied:
                    returnMessage = this.ship.LoseLife();
                    break;
                case State.NotOccupied:
                    returnMessage = "Miss!";
                    break;
                case State.Attacted:
                    returnMessage = "This GridSquare has already been attacked.";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            this.state = State.Attacted;

            return returnMessage;

        }
    }
}