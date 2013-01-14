
namespace Battleships.UnitTests
{
    using NUnit.Framework;

    [TestFixture]
    public class GridSquareTests
    {
        [Test]
        public void CanAttackRefrencedShip()
        {
            var testShip = ShipFactory.CreateBattleship();

            var gridSquare = new GridSquare(testShip);

            var expectedMessage = "A good Hit! But we're still afloat!";

            Assert.AreEqual(expectedMessage, gridSquare.Attack());
        }

        [Test]
        public void AttackReportsMissIfUnoccupied()
        {
            var gridSquare = new GridSquare();

            var expectedMessage = "Miss!";

            Assert.AreEqual(expectedMessage, gridSquare.Attack());
        }

        [Test]
        public void AttackReportsAlreadyAttackedIfAttackedTwice()
        {

            var gridSquare = new GridSquare();

            var expectedMessage = "This GridSquare has already been attacked.";

            gridSquare.Attack();

            Assert.AreEqual(expectedMessage, gridSquare.Attack());
        }
    }
}
