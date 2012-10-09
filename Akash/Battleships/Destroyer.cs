namespace PebbleCode.Interview.Battleships
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Destroyer : Ship
    {
        public Destroyer(IEnumerable<Square> squares) : base(squares)
        {
            if (squares.Count() != 4)
            {
                throw new ArgumentException("A Destroyer must consist of 4 squares", "squares");
            }
        }
    }
}