namespace Battleships.Models
{
    public class BoardSquare
    {
        public string Caption { get; private set; }
        public bool IsHitted { get; set; }

        public BoardSquare(string caption)
        {
            Caption = caption;
            IsHitted = false;
        }
    }
}
