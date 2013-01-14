
namespace Battleships.UnitTests
{
    using System;

    using NUnit.Framework;
    
    [TestFixture]
    public class GridTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "The Grid must be at least the size of the biggest ship in 1 dimention.", MatchType = MessageMatch.Contains)]
        public void CannotAddShipBiggerThanGrid()
        {
            int gridSizeX = 4;
            int gridSizeY = 4;
            var grid = new Grid(gridSizeX, gridSizeY, ShipFactory.CreateBattleship());
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException), ExpectedMessage = "The Grid cannot have a negitive or Zero X axis Size.", MatchType = MessageMatch.Contains)]
        public void CannotGridWithZeroSizedDimentions()
        {
            int gridSizeX = 0;
            int gridSizeY = 4;
            var grid = new Grid(gridSizeX, gridSizeY, ShipFactory.CreateBattleship());
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException), ExpectedMessage = "The Grid cannot have a negitive or Zero Y axis Size.", MatchType = MessageMatch.Contains)]
        public void CannotGridWithNegativeSizedDimentions()
        {
            int gridSizeX = 5;
            int gridSizeY = -4;
            var grid = new Grid(gridSizeX, gridSizeY, ShipFactory.CreateBattleship());
        }
    }
}
