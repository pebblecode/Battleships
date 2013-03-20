using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Engine.Tests
{
    [TestClass]
    public class CoordinateTests
    {
        [TestMethod]
        public void TryParse_NullString()
        {
            Coordinate coord;
            bool result = Coordinate.TryParse(null, out coord);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TryParse_EmptyString()
        {
            Coordinate coord;
            bool result = Coordinate.TryParse(string.Empty, out coord);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TryParse_StringTooShort()
        {
            Coordinate coord;
            bool result = Coordinate.TryParse("A", out coord);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TryParse_StringTooLong() {

            Coordinate coord;
            bool result = Coordinate.TryParse("A111", out coord);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TryParse_InvalidLetter() {

            Coordinate coord;
            bool result = Coordinate.TryParse("!1", out coord);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TryParse_InvalidNumber()
        {
            Coordinate coord;
            bool result = Coordinate.TryParse("A!", out coord);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TryParse_ValidSingleNumber()
        {
            Coordinate coord;
            bool result = Coordinate.TryParse("B2", out coord);

            Assert.IsTrue(result);
            Assert.AreEqual(1, coord.X);
            Assert.AreEqual(1, coord.Y);
        }

        [TestMethod]
        public void TryParse_ValidDoubleNumber()
        {
            Coordinate coord;
            bool result = Coordinate.TryParse("C45", out coord);

            Assert.IsTrue(result);
            Assert.AreEqual(2, coord.X);
            Assert.AreEqual(44, coord.Y);
        }

        [TestMethod]
        public void Constructor_ValuesSet()
        {
            Coordinate coord = new Coordinate(1, 2);
            
            Assert.AreEqual(1, coord.X);
            Assert.AreEqual(2, coord.Y);
        }

        [TestMethod]
        public void Equality_CoordinatesAreEqual()
        {
            Coordinate coord1 = new Coordinate(1, 2);
            Coordinate coord2 = new Coordinate(1, 2);

            Assert.IsTrue(Object.Equals(coord1, coord2));
            Assert.IsTrue(coord1.Equals(coord2));   //IEquality override
            Assert.IsTrue(coord1.Equals((object)coord2)); //Object override
            Assert.IsTrue(coord1 == coord2);
        }

        [TestMethod]
        public void Equality_CoordinatesAreNotEqual()
        {
            Coordinate coord1 = new Coordinate(1, 2);
            Coordinate coord2 = new Coordinate(3, 4);

            Assert.IsFalse(Object.Equals(coord1, coord2));
            Assert.IsFalse(coord1.Equals(coord2));   //IEquality override
            Assert.IsFalse(coord1.Equals((object)coord2)); //Object override
            Assert.IsTrue(coord1 != coord2);
        }

    }
}
