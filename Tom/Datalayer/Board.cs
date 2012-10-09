using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datalayer
{
    public class Board
    {
        private IDisplay display;
        public List<Ships.Ship> Ships = new List<Ships.Ship>();
        private List<BoardCell> board = new List<BoardCell>();
        private List<BoardCell> shots = new List<BoardCell>();

        public Board(IDisplay display)
        {
            this.display = display;
            //Set up a new 10 by 10 board
            for (int i = 1; i < 11; i++)
            {
                for (int j = 1; j < 11; j++)
                {
                    var cell = new BoardCell { Row = i, Column = (Enums.BoardColumn)j };
                    this.board.Add(cell);
                }
            }
        }

        public void Shoot(BoardCell shotPosition)
        {
            if (!this.board.Contains(shotPosition))
            {
                display.Write("Not a valid board position. Try again");
                return;
            }

            if (this.shots.Contains(shotPosition))
            {
                display.Write("You have already shot there. Try again");
                return;
            }

            this.shots.Add(shotPosition);
            var shotShip = Ships.SingleOrDefault(z => z.Cells.Contains(shotPosition));

            if (shotShip != null)
            {
                display.Write(shotPosition.Column.ToString() + shotPosition.Row + " : Hit!");
                shotShip.Hits.Add(shotPosition);

                if (shotShip.IsSunk())
                {
                    display.Write("Ship sunk!");
                }
            }
            else
            {
                display.Write(shotPosition.Column.ToString() + shotPosition.Row + " : Miss!");
            }
        }
    }
}
