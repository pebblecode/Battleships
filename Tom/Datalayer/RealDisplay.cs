using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datalayer
{
    class RealDisplay : Datalayer.IDisplay
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }

        public string Read()
        {
            return Console.ReadLine();
        }
    }
}
