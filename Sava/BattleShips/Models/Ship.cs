namespace BattleShips.Models
{
	using System.Collections.Generic;

	using BattleShips.Attributes;
	using BattleShips.Enums;

	public class Ship
	{
		public Ship()
		{
			this.Cells = new List<string>();
		}

		private int _hits;

		public IList<string> Cells { get; set; }

		public int Length
		{
			get
			{
				return this.ShipType.GetShipLength();
			}
		}

		public ShipTypeEnum ShipType { get; set; }

		public bool IsHorizontal { get; set; }

		public bool Sunk
		{
			get
			{
				return _hits == this.Length;
			}
		}

		public virtual void Hit()
		{
			_hits++;
		}
	}
}