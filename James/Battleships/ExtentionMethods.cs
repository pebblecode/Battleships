namespace Battleships
{
    using System;

    public static class ExtentionMethods
    {

        public static bool TryGetSquare(this GridSquare[,] gridSquares, Position position, out GridSquare square)
        {
            if (gridSquares == null)
            {
                throw new ArgumentNullException("gridSquares", "Initalise Array First");
            }

            if (CheckPosisionIsInGameGrid(position, gridSquares))
            {
                square = gridSquares[position.PosX, position.PosY] ?? new GridSquare();
                return true;
            }

            square = null;
            return false;

        }

        private static bool CheckPosisionIsInGameGrid(Position positionToTest, GridSquare[,] gridSquares)
        {
            if (positionToTest.PosX <= gridSquares.GetLength(0) && positionToTest.PosY <= gridSquares.GetLength(1))
            {
                return true;
            }



            return false;
        }
    }
}
