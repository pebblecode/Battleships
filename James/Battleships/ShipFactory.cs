namespace Battleships
{
    public class ShipFactory
    {
        public static IShip CreateBattleship()
        {
            return new Ship("BattleShip", 5);
        }

        public static IShip CreateDestroyer()
        {
            return new Ship("Destroyer", 4);
        }
    }
}