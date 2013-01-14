namespace Battleships
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Represents a position in the game
    /// </summary>
    public class Position : IEquatable<Position>
    {

        #region Constructors and Destructors

        public Position(int x, int y)
        {
            this.PosX = x;
            this.PosY = y;
        }

        public Position(string positionId)
        {
            positionId = positionId.ToUpperInvariant();
            
            // Remove whitespace
            positionId = Regex.Replace(positionId, @"\s+", string.Empty, RegexOptions.Compiled);

            if (positionId == string.Empty)
            {
                throw new ArgumentOutOfRangeException("positionId", "A Position must not be null.");
            }

            if (!char.IsLetter(positionId.First()))
            {
                throw new ArgumentOutOfRangeException(
                    "positionId", "A Position must begin with a letter, positionId=" + positionId);
            }

            if (!char.IsNumber(positionId.Last()))
            {
                throw new ArgumentOutOfRangeException(
                    "positionId", "A Position must end with a number, positionId=" + positionId);
            }

            if (!Regex.Match(positionId, @"^+([A-Z]+)+([0-9]+)$", RegexOptions.Compiled).Success)
            {
                throw new ArgumentOutOfRangeException(
                    "positionId", 
                    "A position must only include the Characters A-Z, a-z and the numbers 0-9 and must be in the format [letters][numbers], positionId=" + positionId);
            }

            positionId = positionId.ToUpper();

            this.PosX = FromLettersToNumbers(GetLettersFromPositionString(positionId));

            this.PosY = GetNumbersFromPositionString(positionId) - 1;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets PosX.
        /// </summary>
        public int PosX { get; private set; }

        /// <summary>
        /// Gets PosY.
        /// </summary>
        public int PosY { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The ==.
        /// </summary>
        /// <param name="left">
        /// The left.
        /// </param>
        /// <param name="right">
        /// The right.
        /// </param>
        /// <returns>
        /// </returns>
        public static bool operator ==(Position left, Position right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// The op_ implicit.
        /// </summary>
        /// <param name="positionString">
        /// The position string.
        /// </param>
        /// <returns>
        /// </returns>
        public static implicit operator Position(string positionString)
        {
            var position = new Position(positionString);
            return position;
        }

        ///// <summary>
        ///// The op_ implicit.
        ///// </summary>
        ///// <param name="position">
        ///// The position.
        ///// </param>
        ///// <returns>
        ///// </returns>
        //public static implicit operator string(Position position)
        //{
        //    return position.PositionId;
        //}

        /// <summary>
        /// The !=.
        /// </summary>
        /// <param name="left">
        /// The left.
        /// </param>
        /// <param name="right">
        /// The right.
        /// </param>
        /// <returns>
        /// </returns>
        public static bool operator !=(Position left, Position right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">
        /// An object to compare with this object.
        /// </param>
        public bool Equals(Position other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return other.PosX == this.PosX && other.PosY == this.PosY;
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <param name="obj">
        /// The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>. 
        /// </param>
        /// <filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != typeof(Position))
            {
                return false;
            }

            return this.Equals((Position)obj);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            unchecked
            {
                int result = this.PosX.GetHashCode();
                result = (result * 397) ^ this.PosY;
                return result;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// From http://stackoverflow.com/questions/1951517/convert-a-to-1-b-to-2-z-to-26-and-then-aa-to-27-ab-to-28-column-indexes-to
        /// </summary>
        /// <param name="letters">
        /// </param>
        /// <returns>
        /// The from letters to numbers.
        /// </returns>
        private static int FromLettersToNumbers(string letters)
        {
            return letters.Aggregate(0, (current, c) => current * 26 + c - 'A');
        }

        /// <summary>
        /// The get letters from position string.
        /// </summary>
        /// <param name="position">
        /// The position.
        /// </param>
        /// <returns>
        /// The get letters from position string.
        /// </returns>
        private static string GetLettersFromPositionString(string position)
        {
            return Regex.Match(position, @"^[a-zA-Z]", RegexOptions.Compiled).Groups[0].Value;
        }

        /// <summary>
        /// The get numbers from position string.
        /// </summary>
        /// <param name="position">
        /// The position.
        /// </param>
        /// <returns>
        /// The get numbers from position string.
        /// </returns>
        private static int GetNumbersFromPositionString(string position)
        {
            return int.Parse(Regex.Match(position, @"[0-9]*$", RegexOptions.Compiled).Groups[0].Value);
        }

        #endregion
    }
}