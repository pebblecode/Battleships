using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Engine
{
    /// <summary>
    /// Class to represent an instance of ship in the game
    /// </summary>
    public class Ship
    {
        /// <summary>
        /// The list of coordinates in the game which the ship hasn't been hit on yet
        /// </summary>
        private List<Coordinate> _coordinates;

        /// <summary>
        /// The list of coordinates on which the ship has already been hit
        /// </summary>
        private List<Coordinate> _deadCoordinates;

        /// <summary>
        /// The definition of the ship for this ship instance
        /// </summary>
        private ShipDefinition _shipDef;

        /// <summary>
        /// Constructs a new ship instance
        /// </summary>
        /// <param name="shipDef">The definition on which to base this ship</param>
        /// <param name="originCoordinate">The coordinates at which the ship should be placed</param>
        /// <param name="isVertical">Whether the ship is verically orientated</param>
        public Ship(ShipDefinition shipDef, Coordinate originCoordinate, bool isVertical)
        {
            _shipDef = shipDef;

            //Generate coordinates
            _coordinates = Enumerable.Range(0, shipDef.Length).Select(offset =>
            {
                return new Coordinate(
                    (isVertical ? originCoordinate.X : originCoordinate.X + offset),
                    (isVertical ? originCoordinate.Y + offset : originCoordinate.Y));
            }).ToList();

            _deadCoordinates = new List<Coordinate>();
        }

        /// <summary>
        /// Gets the set of coordinates the ship occupies
        /// </summary>
        public ReadOnlyCollection<Coordinate> Coordinates
        {
            get { return _coordinates.AsReadOnly(); }
        }

        /// <summary>
        /// Whether or not this ship is completely destroyed
        /// </summary>
        public bool IsDestroyed
        {
            get { return _deadCoordinates.Count == _shipDef.Length; }
        }

        /// <summary>
        /// Attempts to hit this ship using the given "missile" coordinates
        /// </summary>
        public HitResult TryHit(Coordinate coordinate)
        {
            if (_coordinates.Contains(coordinate))
            {
                _coordinates.Remove(coordinate);
                _deadCoordinates.Add(coordinate);

                return (IsDestroyed ? HitResult.HitAndSink : HitResult.Hit);
            }
            else if (_deadCoordinates.Contains(coordinate))
            {
                if (IsDestroyed)
                {
                    return HitResult.AlreadySunk;
                }
                else
                {
                    return HitResult.AlreadyHit;
                }
            }

            return HitResult.Miss;
        }
    }
}
