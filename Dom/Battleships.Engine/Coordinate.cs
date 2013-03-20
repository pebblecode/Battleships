using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Battleships.Engine
{
    /// <summary>
    /// Simple type to represent the x,y coordinate pair.
    /// </summary>
    public struct Coordinate : IEquatable<Coordinate>
    {
        /// <summary>
        /// Regular expression that will be used when parsing the user entered coordinate string.
        /// We have a limit on a maximum size of 26 horizontal indeces and 99 vertical indeces.
        /// </summary>
        private const string PARSE_REGEX_STRING = "^[A-Za-z][0-9]{1,2}$";

        /// <summary>
        /// Precompiled regex for performing the matching.
        /// </summary>
        private static readonly Regex ParseRegex = new Regex(PARSE_REGEX_STRING, RegexOptions.Compiled);

        /// <summary>
        /// The X component of the coordinate
        /// </summary>
        private int _x;

        /// <summary>
        /// The Y component of the coordinate
        /// </summary>
        private int _y;

        /// <summary>
        /// Constructs a new immutable coordinate
        /// </summary
        public Coordinate(int x, int y)
        {
            _x = x;
            _y = y;
        }

        /// <summary>
        /// Attempts to parse a coordinate string into a coordinate
        /// </summary>
        public static bool TryParse(string coordString, out Coordinate coord)
        {
            coord = default(Coordinate);

            if (string.IsNullOrWhiteSpace(coordString))
            {
                return false;
            }

            if (!ParseRegex.IsMatch(coordString))
            {
                return false;
            }

            //Convert letter to uppercase, convert to integer value, subtract from A's ascii code to get it's zero based offset.
            //This code is a bit quick and dirty in that the conversion from char to int is implicitly reliant on the UTF8 or ASCII encodings.
            //The alternative is to make the encoding explicit or have a static alphabet lookup table of characters mapping to number (by their position).
            int x = (int)char.ToUpper(coordString[0]) - ((int)'A');

            int y;
            if (!int.TryParse(coordString.Substring(1), out y))
            {
                return false;
            }

            //Coordinate stings are 1-based but the board is 0-based, adjust y accordingly
            coord = new Coordinate(x,(y-1));
            return true;
        }

        /// <summary>
        /// Gets the X coordinate component
        /// </summary>
        public int X { get { return _x; } }

        /// <summary>
        /// Gets the Y coordinate component
        /// </summary>
        public int Y { get { return _y; } }

        public bool Equals(Coordinate other)
        {
            return other._x == this._x && other._y == this._y;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!(obj is Coordinate))
            {
                return false;
            }

            return Equals((Coordinate)obj);
        }

        public static bool operator ==(Coordinate a, Coordinate b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Coordinate a, Coordinate b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            //Borrowed from Jon Skeet!
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + _x.GetHashCode();
                hash = hash * 23 + _y.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return string.Format("({0},{1})", _x, _y);
        }
    }
}

