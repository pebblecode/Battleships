using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTests
{
    internal class FakeDisplay : Datalayer.IDisplay
    {
        public string LastMessage { get; private set; }

        public void Write(string message)
        {
            LastMessage = message;
        }

    }
}
