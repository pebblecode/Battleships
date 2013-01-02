using Battleships;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleshipTests
{
    [TestClass]
    public class PointTests
    {
        [TestMethod]
        public void CollidesWithNonCollisionX()
        {
            var point = new Point(1, 1);

            var result = point.ColidesWith(new Point(1, 2));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CollidesWithNonCollisionY()
        {
            var point = new Point(1,1);

            var result = point.ColidesWith(new Point(2, 1));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CollidesWithCollision()
        {
            var point = new Point(1, 1);

            var result = point.ColidesWith(new Point(1, 1));

            Assert.IsTrue(result);
        }
    }
}
