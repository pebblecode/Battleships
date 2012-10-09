namespace PebbleCode.Interview.Battleships
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Ship
    {
        protected IEnumerable<Square> Squares { get; set; }

        public bool IsSunk 
        { 
            get
            {
                return !this.AnySquaresMissed();
            } 
        }

        protected Ship(IEnumerable<Square> squares)
        {
            EnsureHorizontalOrVerticalLine(squares);

            Squares = squares.ToArray();

            foreach (var square in squares)
            {
                square.Ship = this;
            }
        }

        private static void EnsureHorizontalOrVerticalLine(IEnumerable<Square> squares)
        {
            var numberOfRows = squares.Select(square => square.Row)
                                      .Distinct()
                                      .Count();

            var numberOfCols = squares.Select(square => square.Column)
                                      .Distinct()
                                      .Count();

            if (numberOfRows != 1 && numberOfCols != 1)
            {
                throw new ArgumentException("The squares for a Ship must be in a horizontal or vertical line.");
            }

            //TODO: validate that they are in a continuous line
        }

        public MoveResult Hit()
        {
            return AnySquaresMissed() ? MoveResult.Hit : MoveResult.Sink;
        }

        private bool AnySquaresMissed()
        {
            return Squares.Any(square => !square.IsHit);
        }
    }
}