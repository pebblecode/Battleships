using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Engine
{
    /// <summary>
    /// Representation of the game board
    /// </summary>
    public class Board
    {
        //To represent a game of battleships we don't need to create a
        //2D array the represents the game surface.  The entire state of the
        //game can be inferred from the status of the ships.  
        //When the user plays a turn we can check the list of ships to see if
        //they hit any of them.  If they don't then the turn is classed as a miss.
        //This approach means that we consume less memory and is suitable for very large 
        //playing grids.  
        //The downside is that it's more difficult to track whether or not
        //the user has fired twice on the same square and missed.  We could store a list
        //of the users previous shots to determine whether or not the user has played a given
        //square before.
        //Alternatively, if we wanted to support both scenarios (low memory and ease of detemining 
        //shot history) we could use some kind of sparse array structure, possibly a quadtree.
        //Another alternative could be to modify the type Dictionary<Coordinate, Ship> to be
        //a Dictionary<Coordinate, Option<Ship,Coordinate>> that would represent (for a given coord)
        //either a ship or a previous shot-and-miss coordinate.

        /// <summary>
        /// The width of the playing surface, unsigned so we don't need to check for negatives
        /// </summary>
        private ushort _width;

        /// <summary>
        /// The heigh of the playing surface, unsigned so we don't need to check for negatives
        /// </summary>
        private ushort _height;

        /// <summary>
        /// The set of ships that are participating in this game, indexed by their coordinates.
        /// </summary>
        private Dictionary<Coordinate, Ship> _ships;

        /// <summary>
        /// Build a new game board of the given width and height
        /// </summary>
        public Board(ushort width, ushort height)
        {
            _width = width;
            _height = height;
            _ships = new Dictionary<Coordinate, Ship>();
        }

        /// <summary>
        /// Gets the width of the game board
        /// </summary>
        public ushort Width { get { return _width; } }

        /// <summary>
        /// Gets the height of the game board
        /// </summary>
        public ushort Height { get { return _height; } }

        /// <summary>
        /// Attempts to get a ship for a given coordinate
        /// </summary>
        /// <returns>
        /// Returns the ship if one exists at the supplied coordinate, otherwise null
        /// </returns>
        public Ship TryGetShip(Coordinate coord)
        {
            Ship foundShip = null;
            if (_ships.TryGetValue(coord, out foundShip))
            {
                return foundShip;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Attempts to register a ship on the game board
        /// </summary>
        /// <returns>True if successfully registered, false if the new ship will overlap an existing ship</returns>
        public bool TryRegisterShip(Ship newShip)
        {
            if (newShip == null)
            {
                throw new ArgumentNullException("newShip");
            }

            bool overlaps = newShip.Coordinates.Any(newShipCoord => TryGetShip(newShipCoord) != null);
            if (!overlaps)
            {
                foreach (Coordinate coord in newShip.Coordinates)
                {
                    _ships.Add(coord, newShip);
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a collection of all the ships on the game board
        /// </summary>
        public IEnumerable<Ship> AllShips
        {
            get
            {
                //Default reference equality comparer is fine here
                return _ships.Values.Distinct();
            }
        }

        /// <summary>
        /// Is the coordinate within the bounds of the games playing area
        /// </summary>
        public bool IsInBounds(Coordinate coordinate)
        {
            if (coordinate.X < 0 || coordinate.Y < 0)
            {
                return false;
            }
            if (coordinate.X >= _width || coordinate.Y >= _height)
            {
                return false;
            }

            return true;
        }
    }
}
