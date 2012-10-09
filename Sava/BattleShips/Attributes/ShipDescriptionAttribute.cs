namespace BattleShips.Attributes
{
	using System;

	public class ShipDescriptionAttribute : Attribute
	{
		public string Description { get; set; }

		public ShipDescriptionAttribute(string length)
		{
			this.Description = length;
		}
	}
}