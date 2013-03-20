using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Engine.Tests
{
    [TestClass]
    public class BoardTests
    {
        [TestMethod]
        public void TryGetShip_ShipExists() 
        {
            Board board = new Board(10, 10);

            board.TryRegisterShip(new Ship(ShipDefinition.Battleship, new Coordinate(1, 1), false));

            Assert.IsNotNull(board.TryGetShip(new Coordinate(1, 1)));
            Assert.IsNotNull(board.TryGetShip(new Coordinate(2, 1)));
            Assert.IsNotNull(board.TryGetShip(new Coordinate(3, 1)));
            Assert.IsNotNull(board.TryGetShip(new Coordinate(4, 1))); 
            Assert.IsNotNull(board.TryGetShip(new Coordinate(5, 1)));

        }

        [TestMethod]
        public void TryGetShip_ShipDoesNotExist()
        {
            Board board = new Board(10, 10);

            board.TryRegisterShip(new Ship(ShipDefinition.Battleship, new Coordinate(1, 1), false));

            Assert.IsNull(board.TryGetShip(new Coordinate(0, 1)));
            Assert.IsNull(board.TryGetShip(new Coordinate(6, 1)));
        }

        [TestMethod]
        public void TryRegister_ShipExists()
        {
            Board board = new Board(10, 10);

            board.TryRegisterShip(new Ship(ShipDefinition.Battleship, new Coordinate(1, 1), false));

            Assert.IsFalse(board.TryRegisterShip(new Ship(ShipDefinition.Battleship, new Coordinate(1, 1), false)));
            Assert.IsFalse(board.TryRegisterShip(new Ship(ShipDefinition.Battleship, new Coordinate(2, 1), false)));
            Assert.IsFalse(board.TryRegisterShip(new Ship(ShipDefinition.Battleship, new Coordinate(3, 1), false)));
            Assert.IsFalse(board.TryRegisterShip(new Ship(ShipDefinition.Battleship, new Coordinate(4, 1), false)));
            Assert.IsFalse(board.TryRegisterShip(new Ship(ShipDefinition.Battleship, new Coordinate(5, 1), false)));
        }

        [TestMethod]
        public void TryRegister_ShipDoesNotExist()
        {
            Board board = new Board(10, 10);

            board.TryRegisterShip(new Ship(ShipDefinition.Battleship, new Coordinate(1, 1), false));

            Assert.IsTrue(board.TryRegisterShip(new Ship(ShipDefinition.Battleship, new Coordinate(0, 0), false)));
        }

        [TestMethod]
        public void AllShips_ShipsReturned()
        {
            Board board = new Board(10, 10);

            board.TryRegisterShip(new Ship(ShipDefinition.Battleship, new Coordinate(1, 1), false));
            board.TryRegisterShip(new Ship(ShipDefinition.Battleship, new Coordinate(1, 2), false));

            Assert.AreEqual(2, board.AllShips.Count());
        }

        [TestMethod]
        public void IsInBounds_CoordinateOutOfBounds()
        {
            Board board = new Board(10, 10);
            Assert.IsFalse(board.IsInBounds(new Coordinate(-1, 5)));
            Assert.IsFalse(board.IsInBounds(new Coordinate(10, 5)));
            Assert.IsFalse(board.IsInBounds(new Coordinate(5, -1)));
            Assert.IsFalse(board.IsInBounds(new Coordinate(5, 10)));
            Assert.IsTrue(board.IsInBounds(new Coordinate(5, 5)));
        }
    }
}
