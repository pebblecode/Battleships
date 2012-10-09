namespace BattleShips.Test
{
	using System.Collections.Generic;
	using System.Linq;

	using BattleShips.Enums;

	using FluentAssertions;

	using NUnit.Framework;

	[TestFixture]
	public class RandomSeaGeneratorFixture
	{
		// single integration test to check the generator as a whole
		[Test]
		public void CreateSeaWithShips()
		{
			// Arrange
			var list = new List<ShipTypeEnum>()
				{
					ShipTypeEnum.Destroyer, ShipTypeEnum.Battleship, ShipTypeEnum.Battleship
				};
			var generator = new RandomSeaGenerator();
			
			// Act
			var result = generator.CreateSeaWithShips(list);

			// Assert
			
			// check what kind of ships are created
			result.Ships.Count(ship => ship.ShipType == ShipTypeEnum.Battleship).Should().Be(2);
			result.Ships.Count(ship => ship.ShipType == ShipTypeEnum.Destroyer).Should().Be(1);

			// check if all are correct length
			result.Ships.Where(ship => ship.ShipType == ShipTypeEnum.Destroyer).Select(ship => ship.Length).Should().OnlyContain(x => x == 4);
			result.Ships.Where(ship => ship.ShipType == ShipTypeEnum.Battleship).Select(ship => ship.Length).Should().OnlyContain(x => x == 5);
		}
	}
}
