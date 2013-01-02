using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleships
{
    public abstract class Ship
    {
        public abstract int Size { get; }
        public bool HasSunk { get; private set; }
        private IList<Point> Points { get; set; }

        public override string ToString()
        {
            return this.GetType().Name;
        }

        public bool Fire(Point target)
        {
            if (target.ColidesWith(Points))
            {
                Points = Points.Where(p => !p.ColidesWith(target)).ToList();
                OnHit(this);
                if (!Points.Any())
                {
                    Destroyed();
                }
                return true;
            }
            return false;
        }

        protected void Destroyed()
        {
            HasSunk = true;
            OnSank(this);
        }

        public event Action<Ship> OnSank;
        public event Action<Ship> OnHit;

        public void GeneratePosition(int width, int height, Ship[] ships)
        {
            if (this.Points != null)
                return;

            IList<Point> points;
            do
            {
                points = GeneratePoints(width, height, Size).ToList();
            } while (ships.Where(s => s.Points != null).SelectMany(s => s.Points).Any(p=>p.ColidesWith(points)));
            this.Points = points;
        }

        public static IEnumerable<Point> GeneratePoints(int width, int height, int count)
        {
            var horizontal = new[] {true, false}.OrderBy(b => Guid.NewGuid()).First();

            var x = Enumerable.Range(0, (width - count) + 1).OrderBy(b => Guid.NewGuid()).First();
            var y = Enumerable.Range(0, (height - count) + 1).OrderBy(b => Guid.NewGuid()).First();

            if (horizontal)
            {
                return Enumerable.Range(x, count).Select(i => new Point(i, y));
            }
            return Enumerable.Range(y, count).Select(i => new Point(x, i));
        }
    }
}
