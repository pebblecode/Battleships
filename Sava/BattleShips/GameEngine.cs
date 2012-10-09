namespace BattleShips
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using System.Text.RegularExpressions;

	using BattleShips.Attributes;
	using BattleShips.Enums;
	using BattleShips.Models;

	public class GameEngine
	{
		protected internal Sea Sea { get; set; }

		public IRandomSeaGenerator RandomSeaGenerator { get; set; }

		public void Start()
		{
			this.Sea =
				RandomSeaGenerator.CreateSeaWithShips(
					new List<ShipTypeEnum>() { ShipTypeEnum.Battleship, ShipTypeEnum.Destroyer, ShipTypeEnum.Destroyer });

			this.Play();
		}

		protected internal virtual void Attack(string cellKey)
		{
			var shipsHit = this.Sea.Ships.Where(x => x.Cells.Contains(cellKey)).ToList();
			if (shipsHit.Count() == 1) // user hit a ship
			{
				var ship = shipsHit.First();
				ship.Hit();
				Console.WriteLine(string.Format("You hit a {0}!", ship.ShipType.GetShipDescription()));
				if (ship.Sunk)
				{
					Console.WriteLine(string.Format("You've taken down a {0}!", ship.ShipType.GetShipDescription()));
				}
			}
		}
		
		protected internal virtual void Play()
		{
			while (!this.Sea.AreAllShipsSunk)
			{
				// read and validate input
				Console.WriteLine("Enter cell key");
				string input = Console.ReadLine();
				if (!string.IsNullOrEmpty(input)) input = input.ToUpper();
				if (!this.ValidateUserInput(input))
				{
					Console.WriteLine("Bad cell key.. Try again!");
					continue;
				}

				// perform attack
				this.Attack(input);
			}

			Console.WriteLine("You've taken down all the ships! Congratulations!");
		}

		protected internal virtual bool ValidateUserInput(string input)
		{
			Match match = Regex.Match(input, "^[A-J]([1-9]|10)$");

			return match.Success;
		}
	}
}