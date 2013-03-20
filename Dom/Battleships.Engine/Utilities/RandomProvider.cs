using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Engine.Utilities
{
    public class RandomProvider : IRandomProvider
    {
        private Random _random = new Random();

        public int Next(int maxValue)
        {
            return _random.Next(maxValue);
        }

        public bool NextBool()
        {
            return _random.Next(2) == 0;
        }
    }
}
