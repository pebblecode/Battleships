using System;
using Battleships;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleshipTests
{
    [TestClass]
    public class BattleShipGameTests
    {
        [TestMethod]
        public void GameCanBeWonDestoryer()
        {
            var game = new BattleShipGame(4, 4, new Destroyer());

            var hitCount = 0;
            var sankCount = 0;

            game.OnShipSank += s => sankCount++;
            game.OnShipHit += s => hitCount++;

            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    game.FireAtPosition(new Point(i, j));
                }
            }

            Assert.AreEqual(4, hitCount);
            Assert.AreEqual(1, sankCount);
            Assert.IsFalse(game.Running());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CommandParserOutOfRangeX()
        {
            var game = new BattleShipGame(2, 2);

            game.ParseCommand("C0");
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CommandParserOutOfRangeY()
        {
            var game = new BattleShipGame(2, 2);

            game.ParseCommand("A2");
        }

        [TestMethod]
        public void CommandParserCorrectValue()
        {
            var game = new BattleShipGame(10, 10);

            var point = game.ParseCommand("A3");

            Assert.AreEqual(0, point.X);
            Assert.AreEqual(3, point.Y);
        }
    }
}
