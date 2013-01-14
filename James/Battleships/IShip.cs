namespace Battleships
{
    public interface IShip
    {
        int Length { get; }

        bool IsSunk { get; }

        string LoseLife();

    }
}
