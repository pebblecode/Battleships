namespace BattleShips
{
	using System;
	using System.Linq;
	using System.Collections.Generic;

	using BattleShips.Attributes;
	using BattleShips.Enums;
	using BattleShips.Models;

	public interface IRandomSeaGenerator
	{
		Sea CreateSeaWithShips(IList<ShipTypeEnum> listOfShips);
	}

	public class RandomSeaGenerator : IRandomSeaGenerator
	{
		private Sea _sea;

		public RandomSeaGenerator()
		{
			_sea = new Sea();
		}

		private char GenerateRandomChar(int indexLimit)
		{
			int i = new Random().Next(0, indexLimit);
			var c = (char)('A' + i);
			return c;
		}

		public Sea CreateSeaWithShips(IList<ShipTypeEnum> listOfShips)
		{
			foreach (var shipType in listOfShips)
			{
				this.CreateRandomShip(shipType);
			}

			return _sea;
		}

		private void CreateRandomShip(ShipTypeEnum shipType)
		{
			// try to place ship on random spot until possible (some cells could be busy)
			while (true)
			{
				var created = this.TryCreateShip(shipType);
				if (created) break;
			}
		}

		private bool TryCreateShip(ShipTypeEnum shipType)
		{
			char startingRow;
			int startingColumn;
			var shipLength = shipType.GetShipLength();
			bool isHorizontal = Convert.ToBoolean(new Random().Next(0, 2));

			if (isHorizontal)
			{
				startingRow = this.GenerateRandomChar(10);
				startingColumn = new Random().Next(1, 12 - shipLength); // ship takes some cells, so starting point can't be the last cell of the row
			}
			else
			{
				startingColumn = new Random().Next(1, 11);
				startingRow = this.GenerateRandomChar(11 - shipLength); // ship takes some cells, so starting point can't be the last cell of the column
			}

			// check if next cells are free
			bool allCellsFree = true;
			for (int i = 0; i < shipLength; i++)
			{
				string cellKey = this.GetNextCellKey(startingRow, startingColumn, isHorizontal, i);
				if (!_sea.Ships.Any()) break;
				if (_sea.Ships.SelectMany(x => x.Cells).Any(x => x == cellKey))
				{
					allCellsFree = false;
					break;
				}
			}

			// if free - initialize the ship and add it to the sea
			if (allCellsFree)
			{
				var ship = new Ship() { ShipType = shipType, IsHorizontal = isHorizontal};
				for (int i = 0; i < shipLength; i++)
				{
					string cellKey = this.GetNextCellKey(startingRow, startingColumn, isHorizontal, i);
					ship.Cells.Add(cellKey);
				}
				_sea.Ships.Add(ship);
				return true;
			}

			return false;
		}

		private string GetNextCellKey(char startingRow, int startingColumn, bool isHorizontal, int index)
		{
			if (isHorizontal)
			{
				return string.Format("{0}{1}", startingRow, startingColumn + index);
			}

			return string.Format("{0}{1}", (char)(startingRow + index), startingColumn);
		}
		
	}
}
