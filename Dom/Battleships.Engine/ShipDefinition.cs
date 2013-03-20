using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Engine
{
    /// <summary>
    /// Simple type that holds all the definitions of the ship type that the game supports 
    /// </summary>
    public struct ShipDefinition
    {
        /// <summary>
        /// The definition of a battleship
        /// </summary>
        public static readonly ShipDefinition Battleship = new ShipDefinition("Battleship", 5);

        //The definition of a destroyer
        public static readonly ShipDefinition Destroyer = new ShipDefinition("Destroyer", 4);

        /// <summary>
        /// The name of the ship type
        /// </summary>
        private string _name;

        /// <summary>
        /// The length of the ship 
        /// </summary>
        private ushort _length;

        /// <summary>
        /// Constructor marked as private to prevent adhoc creation of new ships
        /// </summary>
        private ShipDefinition(string name, ushort length)
        {
            _name = name;
            _length = length;
        }

        /// <summary>
        /// Gets the name of the ship
        /// </summary>
        public string Name { get { return _name; } }

        /// <summary>
        /// Gets the length of the ship
        /// </summary>
        public ushort Length { get { return _length; } }
    }
}
