using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Battleships.Models
{
    public class Game
    {
        private Board _board;
        private List<string> _shoots;
        private Regex _regex = new Regex("^[A-J]{1}[1-9]0*$");
        public List<Ship> Ships { get; set; }
        public bool IsOver { get { return Ships.All(s => s.IsSinked); } }
        
        public Game()
        {
            _shoots = new List<string>();
            _board = new Board();

            Ships = new List<Ship>();
        }

        public ResultEnum Start()
        {
            //Init ships
            var battleShip = new Ship("Battleship-01", ShipType.Battleship);
            var firstDestroyer = new Ship("Destroyer-01", ShipType.Destroyer);
            var secondDestroyer = new Ship("Destroyer-02", ShipType.Destroyer);
            var i = 0;

            //Place ships and re-create board till no ovelapping is occuring
            do
            {
                _board.ResetBoard();
                
                _board.PlaceShipOnBoard(battleShip);
                _board.PlaceShipOnBoard(firstDestroyer);
                _board.PlaceShipOnBoard(secondDestroyer);

                //Prevent endless cycle if the randomization does not remove 
                //the overlapping after 100 times then stop the start of the game and report
                if (i++ == 100)
                {
                    return ResultEnum.ProblemWithStarting;
                }
            } while (!_board.IsValid);
            
            //Add them to the game, too.
            Ships.Add(battleShip);
            Ships.Add(firstDestroyer);
            Ships.Add(secondDestroyer);

            return ResultEnum.OK;
        }

        public Result Shoot(string point)
        {
            var result = new Result();

            //Cheat detection
            if (point == "SECRET")
            {
                result.ShootResult = ResultEnum.Secret;
                return result;
            }

            //Validate input
            if (!_regex.Match(point).Success)
            {
                result.ShootResult = ResultEnum.InvalidCoordinate;
                return result;
            }

            //Is this square already used
            if (_shoots.Contains(point))
            {
                result.ShootResult = ResultEnum.AlreadyStriked;
                return result;
            }

            var ship = Ships.FirstOrDefault(s => s.CheckItsFromTheShip(point));
          
            //There is ship on that square
            if (ship != null)
            {
                //Mark hitted square and check is sinked at this point
                ship.MarkAsHit(point);
                result.ShootResult = ResultEnum.SuccessfulHit;
                result.Ship = ship;
                
                if (ship.IsSinked)
                {
                    result.ShootResult = ResultEnum.ShipSinked;
                }
            }

            //Is the game over
            if (IsOver)
            {
                result.ShootResult = ResultEnum.YouWon;
            }

            _shoots.Add(point);
            return result;
        }
    }
}
