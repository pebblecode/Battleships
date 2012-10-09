namespace BattleShips.Attributes
{
	using System;

	public class ShipLengthAttribute : Attribute
	{
		public int Length { get; set; }

		public ShipLengthAttribute(int length)
		{
			this.Length = length;
		}
	}
}