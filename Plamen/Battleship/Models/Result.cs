using System;

namespace Battleships.Models
{
    public class Result
    {
        public ResultEnum ShootResult { get; set; }
        public Ship Ship { get; set; }

        public Result()
        {
            
        }

        public string Message
        {
            get { return ComposeMessage(); }
        }

        private string ComposeMessage()
        {
            var message = string.Empty;

            switch (ShootResult)
            {
                case ResultEnum.NoHit:
                    message = "Sorry, no score!";
                    break;
                case ResultEnum.InvalidCoordinate:
                    message = "Sorry, the given square caption is invalid!";
                    break;
                case ResultEnum.SuccessfulHit:
                    message = String.Format("You have just hitted {0}!", Ship.ShipName);
                    break;
                case ResultEnum.ShipSinked:
                    message = String.Format("Ship {0} is sinked!", Ship.ShipName);
                    break;
                case ResultEnum.AlreadyStriked:
                    message = "This square is already used!";
                    break;
                case ResultEnum.YouWon:
                    message = "Hooray all ships are sinked! You won the game!";
                    break;
                    case ResultEnum.Secret:
                    message = "Ships locations is revealed!";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return message;
        }
    }
}
