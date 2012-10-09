using Datalayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
{
    

    [TestClass()]
    public class GameTest
    {

        FakeDisplay display;
        Game game;
        [TestInitialize()]
        public void SetUp()
        {
            display = new FakeDisplay();
            game = new Game(display);
        }

        [TestMethod()]
        public void TypoShouldFail()
        {
            game.AddInput("A10A");
            Assert.AreEqual("Invalid Input. Should be like this : A10", display.LastMessage);
        }

        [TestMethod()]
        public void TooShortShouldFail()
        {
            game.AddInput("A");
            Assert.AreEqual("Invalid Input. Should be like this : A10", display.LastMessage);
        }

        [TestMethod()]
        public void ValidShouldWork()
        {
            game.AddInput("b10");
            Assert.AreNotEqual("Invalid Input. Should be like this : A10", display.LastMessage);
        }
    }
}
