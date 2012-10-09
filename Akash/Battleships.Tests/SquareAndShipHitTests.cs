namespace PebbleCode.Interview.Battleships.Tests
{
    using NUnit.Framework;

    /// <summary>
    /// This tests the Hit method of both Square and Ship. Not ideal - maybe better to define an IShip interface that Square uses
    /// and mock out the IShip instance for the Square tests.
    /// </summary>
    [TestFixture]
    public class SquareAndShipHitTests
    {
        [Test]
        public void Hit_NotPartOfShip_ReturnsMiss()
        {
            var square = new Square('A', 1);

            var result = square.Hit();
            Assert.AreEqual(MoveResult.Miss, result);
        }

        [Test]
        public void Hit_NotLastPartOfShipToBeHit_ReturnsHit()
        {
            var battleShipSquares = GetBattleshipSquares();
            var ship = new Battleship(battleShipSquares);

            var result = battleShipSquares[0].Hit();

            Assert.AreEqual(MoveResult.Hit, result);
        }

        [Test]
        public void Hit_LastPartOfShipToBeHit_ReturnsSink()
        {
            var battleShipSquares = GetBattleshipSquares();
            var ship = new Battleship(battleShipSquares);
            battleShipSquares[0].Hit();
            battleShipSquares[1].Hit();
            battleShipSquares[2].Hit();
            battleShipSquares[3].Hit();

            var result = battleShipSquares[4].Hit();

            Assert.AreEqual(MoveResult.Sink, result);
        }

        public static Square[] GetBattleshipSquares()
        {
            return new[] { new Square('A', 1), new Square('A', 2), new Square('A', 3), new Square('A', 4), new Square('A', 5) };            
        }
    }
}
