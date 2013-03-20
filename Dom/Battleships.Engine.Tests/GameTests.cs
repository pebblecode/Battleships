using Battleships.Engine.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Engine.Tests
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))] //Need to inspect exception for correct arg type here
        public void PlayTurn_InvalidCoordinateThrows()
        {
            Game game = new Game(new StubRandomProvider(), 6, 1, ShipDefinition.Destroyer);

            HitResult result = game.PlayTurn(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))] //Need to inspect exception for correct arg type here
        public void PlayTurn_OutOfBoundsCoordinateThrows()
        {
            Game game = new Game(new StubRandomProvider(), 6, 1, ShipDefinition.Destroyer);

            HitResult result = game.PlayTurn("z9");
        }

        //Ordinarilly done with a Mock
        private class StubRandomProvider : IRandomProvider
        {
            public int Next(int maxValue)
            {
                return 0;
            }

            public bool NextBool()
            {
                return false;
            }
        }

        [TestMethod]
        public void IsEnded_EnsureGameEnds()
        {
            Game game = new Game(new StubRandomProvider(), 6, 1, ShipDefinition.Destroyer);

            Assert.AreEqual(HitResult.Miss, game.PlayTurn("F1"));
            Assert.AreEqual(HitResult.Miss, game.PlayTurn("E1"));
            Assert.AreEqual(HitResult.Hit, game.PlayTurn("D1"));
            Assert.AreEqual(HitResult.Hit, game.PlayTurn("C1"));
            Assert.AreEqual(HitResult.Hit, game.PlayTurn("B1"));
            Assert.AreEqual(HitResult.HitAndSink, game.PlayTurn("A1"));
            Assert.IsTrue(game.IsEnded);
        }
    }
}
