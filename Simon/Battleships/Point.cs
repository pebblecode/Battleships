using System.Collections.Generic;
using System.Linq;

namespace Battleships
{
    public class Point
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public bool ColidesWith(Point point)
        {
            return point.X == X && point.Y == Y;
        }

        public bool ColidesWith(IEnumerable<Point> points)
        {
            return points.Any(p => p.ColidesWith(this));
        }
    }
}
