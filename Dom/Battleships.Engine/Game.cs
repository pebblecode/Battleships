using Battleships.Engine.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Engine
{
    public class Game
    {
        /// <summary>
        /// The game's board on which to play
        /// </summary>
        private Board _board;

        /// <summary>
        /// Whether or not the user has destroyed all the ships and ended the game
        /// </summary>
        private bool _isEnded;

        /// <summary>
        /// The source from which to locate our random numbers
        /// </summary>
        private IRandomProvider _randomProvider;

        /// <summary>
        /// Construct a new game of the given width and height with the specified list of ships
        /// </summary>
        /// <param name="width">The width of the playing surface</param>
        /// <param name="height">The heigh of the playing surface</param>
        /// <param name="shipsToPlace">The list of ships that will participate in this game</param>
        public Game(ushort width, ushort height, params ShipDefinition[] shipsToPlace) : this(
            new RandomProvider(), //Ordinarilly injected with IOC
            width, 
            height, 
            shipsToPlace) { }

        /// <summary>
        /// Construct a new game of the given width and height with the specified list of ships
        /// </summary>
        /// <param name="randomProvider">The source from which to get our random numbers</param>
        /// <param name="width">The width of the playing surface</param>
        /// <param name="height">The heigh of the playing surface</param>
        /// <param name="shipsToPlace">The list of ships that will participate in this game</param>
        public Game(IRandomProvider randomProvider, ushort width, ushort height, params ShipDefinition[] shipsToPlace)
        {
            #region Input validation

            if (randomProvider == null)
            {
                throw new ArgumentNullException("randomProvider");
            }

            if (shipsToPlace == null || shipsToPlace.Length == 0)
            {
                throw new ArgumentException("Must have some ships to play the game", "shipsToPlace");
            }

            #endregion

            _board = new Board(width, height);
            _isEnded = false;
            _randomProvider = randomProvider;

            PlaceShips(shipsToPlace);
        }

        /// <summary>
        /// Randomly positions the specified ships into the game
        /// </summary>
        private void PlaceShips(params ShipDefinition[] shipsToPlace)
        {
            foreach (ShipDefinition shipDef in shipsToPlace)
            {
                //First determine whether to place the ship horizontally or vertically
                bool isVertical = _randomProvider.NextBool(); //(this should be a .NextBool() extension method).
                
                //For a given ship we'll keep generating random coordinates until we've placed it.
                //(This isn't ideal because there are no garuantees that all the ships will fit on the
                //game board.  We really want a validation step in the Game constructor to validate that
                //all ships can be placed inside the game board and include some heuristic to ensure
                //that, say, at least 50% of the game board is empty to make for a good game).
                bool shipPlaced = false;
                while (!shipPlaced)
                {
                    //Next generate a coordinate in which to place the boat, the coordinate is clamped 
                    //such that the boat cannot be placed outside of the game board (depending on the ships
                    //orientation).
                    Coordinate coord = new Coordinate(
                        _randomProvider.Next(_board.Width - (isVertical ? 0 : _board.Width - shipDef.Length)),
                        _randomProvider.Next(_board.Height - (isVertical ? _board.Height - shipDef.Length : 0)));

                    Ship newShip = new Ship(shipDef, coord, isVertical);

                    //Try and register the new ship, this will fail if the new ship overlaps an existing ship
                    shipPlaced = _board.TryRegisterShip(newShip);
                }
            }
        }

        /// <summary>
        /// Allows the user to play a turn using the supplied coordinates
        /// </summary>
        public HitResult PlayTurn(String coordinateString)
        {
            #region Input validation

            if (_isEnded)
            {
                throw new InvalidOperationException("Cannot play a turn when the game is ended");
            }

            Coordinate coordinate;
            if (!Coordinate.TryParse(coordinateString, out coordinate))
            {
                throw new ArgumentException("Coordinate could not be parsed", "coordinateString");
            }

            if (!_board.IsInBounds(coordinate))
            {
                throw new ArgumentException("Coordinate is out of bounds", "coordinateString");
            }

            #endregion

            Ship hitShip = _board.TryGetShip(coordinate);

            if (hitShip == null)
            {
                return HitResult.Miss;
            }
            else
            {
                HitResult result = hitShip.TryHit(coordinate);

                if (result == HitResult.HitAndSink)
                {
                    _isEnded = _board.AllShips.All(s => s.IsDestroyed);
                }

                return result;
            }
        }

        /// <summary>
        /// Gets whether or not the game has ended
        /// </summary>
        public bool IsEnded
        {
            get { return _isEnded; }
        }
    }
}
