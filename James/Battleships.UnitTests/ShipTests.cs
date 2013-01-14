namespace Battleships.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using NUnit.Framework;

    [TestFixture]
    public class ShipTests
    {
        [Test]
        public void CanCreateShipUsingFactory()
        {
            IShip battleship = ShipFactory.CreateBattleship();
            IShip destroyer = ShipFactory.CreateDestroyer();

            Assert.AreEqual(5, battleship.Length);
            Assert.AreEqual(4, destroyer.Length);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
            ExpectedMessage = "Ships must be 1 or more units in length.", MatchType = MessageMatch.StartsWith)]
        public void MustNotCreateShipWithNegitiveLength()
        {
            IShip testShip = new Ship("battleship", -1);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
           ExpectedMessage = "Ships must be 1 or more units in length.", MatchType = MessageMatch.StartsWith)]

        public void MustNotCreateShipWithZeroLength()
        {
            IShip testShip = new Ship("battleship", 0);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
                   ExpectedMessage = "Ships must have a name.", MatchType = MessageMatch.StartsWith)]

        public void MustNotCreateShipWithBlankName()
        {
            IShip testShip = new Ship("", 5);
        }

        [Test]
        public void MustReportLossOfLife()
        {
            const string ExpectedMessage = "A good Hit! But we're still afloat!";
            IShip testShip = new Ship("Battleship", 5);
            Assert.AreEqual(ExpectedMessage, testShip.LoseLife());
        }

        [Test]
        public void MustReportSinking()
        {
            const string ExpectedMessage = "You sunk my Battleship!";
            IShip testShip = new Ship("Battleship", 1);
            Assert.AreEqual(ExpectedMessage, testShip.LoseLife());
        }

        [Test]
        public void MustReportSunkWhenNoLivesLeft()
        {
            IShip testShip = new Ship("Battleship", 1);
            testShip.LoseLife();
            Assert.AreEqual(true, testShip.IsSunk);
        }

    }
}
