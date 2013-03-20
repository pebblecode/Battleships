using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Engine.Tests
{
    [TestClass]
    public class ShipTests
    {
        [TestMethod]
        public void Constructor_ValuesSet()
        {
            Ship ship = new Ship(ShipDefinition.Destroyer, new Coordinate(0, 0), false);

            Assert.AreEqual(4, ship.Coordinates.Count);
            Assert.AreEqual(new Coordinate(0, 0), ship.Coordinates[0]);
            Assert.AreEqual(new Coordinate(1, 0), ship.Coordinates[1]);
            Assert.AreEqual(new Coordinate(2, 0), ship.Coordinates[2]);
            Assert.AreEqual(new Coordinate(3, 0), ship.Coordinates[3]);
        }

        [TestMethod]
        public void IsDestroyed_TransitionsOk()
        {
            Ship ship = new Ship(ShipDefinition.Destroyer, new Coordinate(0, 0), false);

            Assert.IsFalse(ship.IsDestroyed);

            ship.TryHit(new Coordinate(0, 0));
            Assert.IsFalse(ship.IsDestroyed);

            ship.TryHit(new Coordinate(1, 0));
            Assert.IsFalse(ship.IsDestroyed);

            ship.TryHit(new Coordinate(2, 0));
            Assert.IsFalse(ship.IsDestroyed);

            ship.TryHit(new Coordinate(3, 0));
            Assert.IsTrue(ship.IsDestroyed);
        }

        [TestMethod]
        public void TryHit_NotHitBefore()
        {
            Ship ship = new Ship(ShipDefinition.Destroyer, new Coordinate(0, 0), false);

            HitResult result = ship.TryHit(new Coordinate(0, 0));

            Assert.AreEqual(HitResult.Hit, result);
        }

        [TestMethod]
        public void TryHit_HitBefore()
        {
            Ship ship = new Ship(ShipDefinition.Destroyer, new Coordinate(0, 0), false);

            HitResult result = ship.TryHit(new Coordinate(0, 0));
            result = ship.TryHit(new Coordinate(0, 0));

            Assert.AreEqual(HitResult.AlreadyHit, result);
        }

        [TestMethod]
        public void TryHit_NotSunkBefore()
        {
            Ship ship = new Ship(ShipDefinition.Destroyer, new Coordinate(0, 0), false);

            ship.TryHit(new Coordinate(0, 0));
            ship.TryHit(new Coordinate(1, 0));
            ship.TryHit(new Coordinate(2, 0));

            HitResult result = ship.TryHit(new Coordinate(3, 0));

            Assert.AreEqual(HitResult.HitAndSink, result);
        }

        [TestMethod]
        public void TryHit_SunkBefore ()
        {
            Ship ship = new Ship(ShipDefinition.Destroyer, new Coordinate(0, 0), false);

            ship.TryHit(new Coordinate(0, 0));
            ship.TryHit(new Coordinate(1, 0));
            ship.TryHit(new Coordinate(2, 0));
            ship.TryHit(new Coordinate(3, 0));

            HitResult result = ship.TryHit(new Coordinate(3, 0));

            Assert.AreEqual(HitResult.AlreadySunk, result);
        }

        [TestMethod]
        public void TryHit_Misses()
        {
            Ship ship = new Ship(ShipDefinition.Destroyer, new Coordinate(0, 0), false);

            HitResult result = ship.TryHit(new Coordinate(10, 10));

            Assert.AreEqual(HitResult.Miss, result);
        }
    }
}
