using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Engine
{
    /// <summary>
    /// Represents all possible results the user attempting to hit a ship
    /// </summary>
    public enum HitResult
    {
        /// <summary>
        /// The user missed the ship
        /// </summary>
        Miss,

        /// <summary>
        /// The user hit the ship
        /// </summary>
        Hit,

        /// <summary>
        /// The user hit this part of the ship but it has already been hit
        /// </summary>
        AlreadyHit,

        /// <summary>
        /// The user hit the and has just sunk it.
        /// </summary>
        HitAndSink,

        /// <summary>
        /// The user hit the ship but the ship has already been destroyed.
        /// </summary>
        AlreadySunk
    }
}
