namespace BattleShips.Enums
{
	using BattleShips.Attributes;

	public enum ShipTypeEnum
	{
		[ShipDescription("Battleship")]
		[ShipLength(5)]
		Battleship = 1,

		[ShipDescription("Destroyer")]
		[ShipLength(4)]
		Destroyer = 2,
	}
}
