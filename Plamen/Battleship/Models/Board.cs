using System;
using System.Collections.Generic;

namespace Battleships.Models
{
    public class Board
    {
        private string[,] _boardSquares = new[,]
                                        {
                                            {"A1", "A2", "A3", "A4", "A5", "A6", "A7", "A8", "A9", "A10"},
                                            {"B1", "B2", "B3", "B4", "B5", "B6", "B7", "B8", "B9", "B10"},
                                            {"C1", "C2", "C3", "C4", "C5", "C6", "C7", "C8", "C9", "C10"},
                                            {"D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9", "D10"},
                                            {"E1", "E2", "E3", "E4", "E5", "E6", "E7", "E8", "E9", "E10"},
                                            {"F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10"},
                                            {"G1", "G2", "G3", "G4", "G5", "G6", "G7", "G8", "G9", "G10"},
                                            {"H1", "H2", "H3", "H4", "H5", "H6", "H7", "H8", "H9", "H10"},
                                            {"I1", "I2", "I3", "I4", "I5", "I6", "I7", "I8", "I9", "I10"},
                                            {"J1", "J2", "J3", "J4", "J5", "J6", "J7", "J8", "J9", "J10"}
                                        };

        private List<string> _usedSquares;
        private Random _generator;

        public bool IsValid { get; private set; }

        public Board()
        {
            _usedSquares = new List<string>();
            _generator = new Random();
        }

        public void ResetBoard()
        {
            _usedSquares = new List<string>();
            IsValid = true;
        }

        public void PlaceShipOnBoard(Ship ship)
        {
            const int boardLength = 10;
            int shipLength;
            string caption;
            
            var boardPoints = new List<BoardSquare>();

            //What type is the ship that needs to be placed
            switch (ship.Type)
            {
                case ShipType.Battleship:
                    shipLength = 5;
                    break;
                case ShipType.Destroyer:
                    shipLength = 4;
                    break;
                default:
                    throw new Exception("Not known ship type!");
            }

            //Generate starting point + orientaion
            var squareXCoordinate = _generator.Next(0, boardLength - shipLength);
            var squareYCoordinate = _generator.Next(0, boardLength - shipLength);
            var orientation = (Orientation)_generator.Next(0, 2);

            //Compose the ship
            if (orientation == Orientation.Verical)
            {
                squareXCoordinate = _generator.Next(0, boardLength);

                for (var i = 0; i < shipLength; i++)
                {
                    caption = _boardSquares[squareXCoordinate, squareYCoordinate + i];
                    
                    if (_usedSquares.Contains(caption))
                    {
                        IsValid = false;
                    }

                    boardPoints.Add(new BoardSquare(caption));
                    _usedSquares.Add(caption);
                }
            }
            else
            {
                squareYCoordinate = _generator.Next(0, boardLength);

                for (var i = 0; i < shipLength; i++)
                {
                    caption = _boardSquares[squareXCoordinate + i, squareYCoordinate];
                    
                    if (_usedSquares.Contains(caption))
                    {
                        IsValid = false;
                    }

                    boardPoints.Add(new BoardSquare(caption));
                    _usedSquares.Add(caption);
                }
            }


            ship.ShipCoords = boardPoints;
        }
    }
}
