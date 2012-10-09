namespace PebbleCode.Interview.Battleships
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Battleship : Ship
    {
        public Battleship(IEnumerable<Square> squares) : base(squares)
        {
            if (squares.Count() != 5)
            {
                throw new ArgumentException("A Battleship must consist of 5 squares", "squares");
            }
        }
    }
}