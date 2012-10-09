using Datalayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
{
    
    
    /// <summary>
    ///This is a test class for BoardTest and is intended
    ///to contain all BoardTest Unit Tests
    ///</summary>
    [TestClass()]
    public class BoardTest
    {
        
        FakeDisplay display;
        Board board;
        [TestInitialize()]
        public void SetUp()
        {
            display = new FakeDisplay();
            board = new Board(display);

            var start = new BoardCell { Row = 4, Column = Enums.BoardColumn.E };
            var battleShip = new Datalayer.Ships.Battleship(start,Enums.Orientation.Vertical);
            board.Ships.Add(battleShip);

        }

        [TestMethod()]
        public void ShootOffTheBoardShouldError()
        {
            var shot = new BoardCell { Row = 15, Column = Enums.BoardColumn.E };
            board.Shoot(shot);
            Assert.AreEqual("Not a valid board position. Try again", display.LastMessage);
        }

        [TestMethod()]
        public void MissShouldMiss()
        {
            var shot = new BoardCell { Row = 9, Column = Enums.BoardColumn.F };
            board.Shoot(shot);
            Assert.AreEqual("F9 : Miss!", display.LastMessage);
        }

        [TestMethod()]
        public void HitShouldHit()
        {
            var shot = new BoardCell { Row = 7, Column = Enums.BoardColumn.E };
            board.Shoot(shot);
            Assert.AreEqual("E7 : Hit!", display.LastMessage);
        }
        
        [TestMethod()]
        public void SinkingShouldWork()
        {
            board.Shoot(new BoardCell { Row = 4, Column = Enums.BoardColumn.E });
            board.Shoot(new BoardCell { Row = 5, Column = Enums.BoardColumn.E });
            board.Shoot(new BoardCell { Row = 6, Column = Enums.BoardColumn.E });
            board.Shoot(new BoardCell { Row = 7, Column = Enums.BoardColumn.E });
            board.Shoot(new BoardCell { Row = 8, Column = Enums.BoardColumn.E });
            Assert.AreEqual("Ship sunk!", display.LastMessage);
        }

        [TestMethod()]
        public void ReShootingTheSameCellShouldError()
        {
            var shot = new BoardCell { Row = 7, Column = Enums.BoardColumn.E };
            board.Shoot(shot);
            board.Shoot(shot);
            Assert.AreEqual("You have already shot there. Try again", display.LastMessage);
        }

    }
}
