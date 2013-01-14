namespace Battleships
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Grid
    {
        public GridSquare[,] GridPositions { get; private set; }

        private readonly IEnumerable<IShip> ships;

        public Grid(int gridSizeX, int gridSizeY, params IShip[] shipsToAdd)
        {
            if (gridSizeX < 1)
            {
                throw new ArgumentOutOfRangeException("gridSizeX", "The Grid cannot have a negitive or Zero X axis Size.");
            }
            if (gridSizeY < 1)
            {
                throw new ArgumentOutOfRangeException("gridSizeY", "The Grid cannot have a negitive or Zero Y axis Size.");
            }
            var maxShipSize = shipsToAdd.Max(ship => ship.Length);
            if (maxShipSize > gridSizeX && maxShipSize > gridSizeY)
            {
                throw new ArgumentException("The Grid must be at least the size of the biggest ship in 1 dimention.");
            }

            this.GridPositions = new GridSquare[gridSizeX, gridSizeY];

            this.ships = this.AddShipsToGrid(shipsToAdd);

        }

        public bool AllShipsSunk()
        {
            return this.ships.All(ship => ship.IsSunk);
        }

        public string Attack(Position position)
        {
            GridSquare square;

            if (this.GridPositions.TryGetSquare(position, out square))
            {
                return square.Attack();
            }

            return string.Format("Position is not within the game grid.");
        }

        private IEnumerable<IShip> AddShipsToGrid(params IShip[] shipsToAdd)
        {
            Random rand = new Random();
            var possibleShipPositions = this.GetPosiblePositions(shipsToAdd);
            foreach (var ship in shipsToAdd)
            {
                //pick random from list
                var shipPositions = possibleShipPositions.Where(possibleShipPosition => possibleShipPosition.ShipLength == ship.Length).ToList();
                var shipPosition = shipPositions[rand.Next(shipPositions.Count)];

                //add to grid
                foreach (var position in shipPosition.RequiredPositions)
                {
                    this.GridPositions[position.PosX, position.PosY] = new GridSquare(ship);
                }

                //remove from list
                //update possibles
                possibleShipPositions =
                    possibleShipPositions.Where(
                        possibleShipPosition => !possibleShipPosition.IsUsingAnyOfPositions(shipPosition.RequiredPositions)).ToArray();
                
            }

            return shipsToAdd.ToList();
        }

        private IEnumerable<PossibleShipPosition> GetPosiblePositions(IEnumerable<IShip> candidateShips)
        {
            int maxY = this.GridPositions.GetLength(0);
            int maxX = this.GridPositions.GetLength(1);

            var possiblePositions = new List<PossibleShipPosition>();
            foreach (var length in candidateShips.Select(ship => ship.Length).Distinct())
            {
                for (int x = 0; x < maxX; x++)
                {
                    for (int y = 0; y < maxY; y++)
                    {
                        if ((y + length) < maxY)
                        {
                            possiblePositions.Add(new PossibleShipPosition(new Position(x, y), length, Direction.Y));
                        }

                        if ((x + length) < maxX)
                        {
                            possiblePositions.Add(new PossibleShipPosition(new Position(x, y), length, Direction.X));
                        }
                    }
                }
            }

            return possiblePositions;
        }
    }
}
