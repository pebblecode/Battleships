using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datalayer
{
    public class Game
    {

        private IDisplay display;
        private Board board;

        public Game(IDisplay display)
        {
            this.display = display;
            Datalayer.RandomShipFactory factory = new RandomShipFactory();
            this.board = new Board(display);
            board.Ships.Add(factory.GetBattleship());

            Ships.Ship destroyer;

            while (board.Ships.Count < 3)
            {
                destroyer = factory.GetDestroyer();

                //Cant have two ships overlapping
                if (!board.Ships.Any(z => z.IsColliding(destroyer)))
                {
                    board.Ships.Add(destroyer);
                }
            }
        }

        public void AddInput(string input)
        {
            BoardCell cell;
            if (!tryParseInput(input, out cell))
            {
                display.Write("Invalid Input. Should be like this : A10");
            }
            else
            {
                board.Shoot(cell);
            }
        }

        public bool IsFinished()
        {
            return !board.Ships.Any(z => !z.IsSunk());
        }

        private bool tryParseInput(string input, out Datalayer.BoardCell cell)
        {
            cell = null;
            //Should be long enough
            if (string.IsNullOrEmpty(input) || input.Length < 2)
            {
                return false;
            }

            //Should start with a column
            string inputColumn = input.Substring(0, 1);
            Datalayer.Enums.BoardColumn column;
            if (!Enum.TryParse(inputColumn.ToUpper(), out column))
            {
                return false;
            }

            //Should finish with a number less than 10
            string inputRow = input.Substring(1, input.Length - 1);
            int row;
            if (!int.TryParse(inputRow, out row))
            {
                return false;
            }

            //All fine so return the cell
            cell = new BoardCell { Row = row, Column = column };
            return true;
        }

    }


}
