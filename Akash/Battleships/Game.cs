namespace PebbleCode.Interview.Battleships
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Game
    {
        private static readonly char[] columns = new[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
        private static readonly int[] rows = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        private readonly LegalTargets legalTargets;
        private readonly HashSet<Ship> ships = new HashSet<Ship>();

        public IEnumerable<ITarget> LegalTargets 
        { 
            get
            {
                return this.legalTargets;
            }
        }

        public Game()
        {
            var allSquares = from column in columns 
                             from row in rows 
                             select new Square(column, row);

            this.legalTargets = new LegalTargets(new HashSet<Square>(allSquares));

            AddShips();
        }

        public MoveResult Hit(ITarget target)
        {
            var result = this.legalTargets.Hit(target);

            if (result != MoveResult.Sink)
            {
                return result;
            }

            return AnyShipFloating() ? result : MoveResult.GameOver;
        }

        private bool AnyShipFloating()
        {
            return this.ships.Any(ship => !ship.IsSunk);
        }

        private void AddShips()
        {
            AddShip(5, squares => new Battleship(squares));
            AddShip(4, squares => new Destroyer(squares));
            AddShip(4, squares => new Destroyer(squares));
        }

        private void AddShip(int shipLength, Func<IEnumerable<Square>, Ship> createShip)
        {
            var squares = this.GetShipSquares(shipLength);

            ships.Add(createShip(squares));
        }

        private IEnumerable<Square> GetShipSquares(int shipLength)
        {
            var random = new Random();

            IEnumerable<Square> result;
            do
            {
                var startRow = random.Next(1, rows.Count() + 1);
                var startColIndex = random.Next(0, columns.Count());
                var direction = random.Next(0, 4); // define 0 = left; 1 = up; 2 = right; 3 = down

                result = GetUnoccupiedSquares(startRow, startColIndex, direction, shipLength);
            }
            while (result == null);

            return result;
        }

        private IEnumerable<Square> GetUnoccupiedSquares(int startRow, int startColIndex, int direction, int shipLength)
        {
            // returning null implies that either:
            // a) A line of shipLength squares cannot be formed in the required direction from the starting row and column, OR
            // b) One or more of the intended squares in already occupied.

            switch (direction)
            {
                case 0:
                    if (startColIndex + 1 < shipLength)
                    {
                        return null;
                    }

                    return this.GetUnoccupiedSquares(shipLength, offset => columns[startColIndex - offset].ToString() + startRow);
                case 1:
                    if (startRow < shipLength)
                    {
                        return null;
                    }

                    return this.GetUnoccupiedSquares(shipLength, offset => columns[startColIndex].ToString() + (startRow - offset));
                case 2:
                    if (startColIndex + shipLength >= columns.Count())
                    {
                        return null;
                    }

                    return this.GetUnoccupiedSquares(shipLength, offset => columns[startColIndex + offset].ToString() + startRow);
                case 3:
                    if (startRow + shipLength >= rows.Count())
                    {
                        return null;
                    }

                    return this.GetUnoccupiedSquares(shipLength, offset => columns[startColIndex].ToString() + (startRow + offset));
                default:
                    throw new ArgumentOutOfRangeException("direction", direction, "Must be 0, 1, 2 or 3");
            }
        }

        private IEnumerable<Square> GetUnoccupiedSquares(int shipLength, Func<int, string> getCoordinates)
        {
            // returning null implies that one or more of the intended squares is already occupied.

            var squares = new List<Square>();

            for (var i = 0; i < shipLength; i++)
            {
                var offset = i;
                var square = this.legalTargets.Cast<Square>()
                                              .First(target => target.Coordinates == getCoordinates(offset));

                if (square.Ship != null)
                {
                    return null;
                }

                squares.Add(square);
            }

            return squares;
        }
    }
}