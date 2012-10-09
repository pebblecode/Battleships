using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datalayer
{
    public class BoardCell
    {
        public Enums.BoardColumn Column { get; set; }
        public int Row { get; set; }

        public BoardCell NextDown()
        {
            var next = new BoardCell();
            next.Column = this.Column + 1;
            next.Row = this.Row;
            return next;
        }

        public BoardCell NextAlong()
        {
            var next = new BoardCell();
            next.Column = this.Column;
            next.Row = this.Row + 1;
            return next;
        }

        /// <summary>
        /// Override the default comparer so it checks if the cells are in the same position rather than the same object
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(BoardCell))
            {
                BoardCell cell = (BoardCell)obj;
                return (cell.Column == this.Column) && (cell.Row == this.Row);
            }
            else
            {
                return false;
            }
        }
    }
}
