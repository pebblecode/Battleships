namespace Battleships.UnitTests
{
    using System;

    using NUnit.Framework;

    [TestFixture]
    class PositionTests
    {

        [Test]
        public void CanCreateValidPosition()
        {
            const string ExpextedPosition = "A1";

            var testPosition = new Position(ExpextedPosition);
            
            Assert.AreEqual(0, testPosition.PosX);
            Assert.AreEqual(0, testPosition.PosY);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException), ExpectedMessage = "A Position must begin with a letter", MatchType = MessageMatch.Contains)]
        public void PositionMustBeginWithLetter()
        {
            const string ExpextedPosition = "11";

            var testPosition = new Position(ExpextedPosition);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException), ExpectedMessage = "A Position must end with a number", MatchType = MessageMatch.Contains)]
        public void PositionMustEndWithNumber()
        {
            const string ExpextedPosition = "AA";

            var testPosition = new Position(ExpextedPosition);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException), ExpectedMessage = "A Position must not be null.", MatchType = MessageMatch.Contains)]
        public void PositionMustNotBeNull()
        {
            const string ExpextedPosition = "";

            var testPosition = new Position(ExpextedPosition);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException), ExpectedMessage = "A position must only include the Characters A-Z, a-z and the numbers 0-9 and must be in the format [letters][numbers], positionId", MatchType = MessageMatch.Contains)]
        public void PositionMustOnlyIncludeLegalCharacters()
        {
            const string BadPosition = "A?!.1";

            var testPosition = new Position(BadPosition);
        }


        [Test]
        public void PositionMustIgnoreWhiteSpace()
        {
            const string positionWithWhitespace = "A \t1";

            var testPosition = new Position(positionWithWhitespace);
            Assert.AreEqual(0, testPosition.PosX);
            Assert.AreEqual(0, testPosition.PosY);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException), ExpectedMessage = "A position must only include the Characters A-Z, a-z and the numbers 0-9 and must be in the format [letters][numbers], positionId", MatchType = MessageMatch.Contains)]
        public void PositionMustBeInLetterNumberFormat()
        {
            const string BadPosition = "A11A1";

            var testPosition = new Position(BadPosition);
        }
    }
}
