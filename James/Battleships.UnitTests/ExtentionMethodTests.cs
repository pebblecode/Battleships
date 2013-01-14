namespace Battleships.UnitTests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class ExtentionMethodTests
    {
        public GridSquare[,] GetTestGridSquares()
        {
            
            GridSquare[,] gridSquares = new GridSquare[2,2];

            gridSquares[0, 0] = new GridSquare();
            gridSquares[0, 1] = new GridSquare(ShipFactory.CreateBattleship());

            return gridSquares;

        }

        [Test]
        public void CanGetExisitng()
        {
            GridSquare square;
            var testGridSquares = this.GetTestGridSquares();
            var testPosition = new Position(0, 1);
            Assert.AreEqual(true, testGridSquares.TryGetSquare(testPosition, out square));
            Assert.NotNull(square);
        }

        [Test]
        public void CannotGetIfNotInGrid()
        {
            GridSquare square;
            var testGridSquares = this.GetTestGridSquares();
            var testPosition = new Position(0, 10);
            Assert.AreEqual(false, testGridSquares.TryGetSquare(testPosition, out square));
            Assert.IsNull(square);
        }

        [Test]
        public void CanCreateIfNull()
        {
            GridSquare square;
            var testGridSquares = this.GetTestGridSquares();
            var testPosition = new Position(1, 1);

            Assert.IsNull(testGridSquares[1, 1]);


            Assert.AreEqual(true, testGridSquares.TryGetSquare(testPosition, out square));
            Assert.NotNull(square);
        }
    }
}
