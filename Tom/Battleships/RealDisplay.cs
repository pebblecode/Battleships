using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Battleships
{
    class RealDisplay : Datalayer.IDisplay
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }

    }
}
