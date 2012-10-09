namespace PebbleCode.Interview.Battleships
{
    using System.Diagnostics;

    [DebuggerDisplay("{Coordinates}")]
    public class Square : ITarget
    {
        public char Column { get; private set; }

        public int Row { get; private set; }

        public Ship Ship { get; set; }

        public bool IsHit { get; private set; }

        public Square(char column, int row)
        {
            this.Column = column;
            this.Row = row;
        }

        public MoveResult Hit()
        {
            IsHit = true;

            return Ship == null ? MoveResult.Miss : Ship.Hit();
        }

        public string Coordinates
        {
            get
            {
                return Column.ToString() + Row;                
            }
        }
    }
}