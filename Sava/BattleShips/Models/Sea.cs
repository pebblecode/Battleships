namespace BattleShips.Models
{
	using System.Collections.Generic;
	using System.Linq;

	public class Sea
	{
		public Sea()
		{
			this.Ships = new List<Ship>();
		}

		public IList<Ship> Ships { get; set; }

		public bool AreAllShipsSunk
		{
			get
			{
				return Ships.All(x => x.Sunk);
			}
		}
	}
}
