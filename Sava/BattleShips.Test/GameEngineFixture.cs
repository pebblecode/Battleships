namespace BattleShips.Test
{
	using System;
	using System.IO;
	using System.Linq;
	using System.Collections.Generic;
	
	using BattleShips.Enums;
	using BattleShips.Models;

	using FluentAssertions;

	using Moq;

	using NUnit.Framework;

	[TestFixture]
	public class GameEngineFixture
	{
		[Test]
		public void Start()
		{
			// Arrange
			var sea = new Sea();
			var engine = Mock.Of<GameEngine>(e => e.RandomSeaGenerator == Mock.Of<IRandomSeaGenerator>());
			Mock.Get(engine).CallBase = true;
			Mock.Get(engine).Setup(e => e.Play());
			Mock.Get(engine.RandomSeaGenerator).Setup(
				r =>
				r.CreateSeaWithShips(
					It.Is<List<ShipTypeEnum>>(
						l => l.Count(x => x == ShipTypeEnum.Destroyer) == 2 && l.Count(x => x == ShipTypeEnum.Battleship) == 1))).Returns(sea); // we pass collection with 2 destroyers and a battleship

			// Act
			engine.Start();

			// Assert
			Mock.Get(engine).VerifyAll();
			Mock.Get(engine.RandomSeaGenerator).VerifyAll();
			engine.Sea.Should().Be(sea);
		}

		[Test]
		public void Play()
		{
			// Arrange
			var sr = new StringReader("A5");
			Console.SetIn(sr);

			var engine =
				Mock.Of<GameEngine>(
					e => e.Sea == new Sea() { Ships = new List<Ship>() { new Ship() { ShipType = ShipTypeEnum.Destroyer } } });
			Mock.Get(engine).CallBase = true;
			Mock.Get(engine).Setup(e => e.ValidateUserInput("A5")).Returns(true);
			Mock.Get(engine).Setup(e => e.ValidateUserInput("SOME NONSENCE")).Returns(false);
			Mock.Get(engine).Setup(e => e.ValidateUserInput(null)).Returns(true);
			// setup second iteration input to "some nonsence" to check if "Attack" is not called
			Mock.Get(engine).Setup(e => e.Attack("A5")).Callback(
				() =>
					{
						sr = new StringReader("some nonsence");
						Console.SetIn(sr);
					});
			// setup third iteration input to exit the loop and finish the test
			Mock.Get(engine).Setup(e => e.Attack(null)).Callback(
				() =>
				{
					// take down the ship
					engine.Sea.Ships.First().Hit();
					engine.Sea.Ships.First().Hit();
					engine.Sea.Ships.First().Hit();
					engine.Sea.Ships.First().Hit();
				});
			
			// Act
			engine.Play();

			// Assert
			Mock.Get(engine).VerifyAll();
			Mock.Get(engine).Verify(e => e.Attack("SOME NONSENCE"), Times.Never());
		}

		[Test]
		[TestCase("A1", true)]
		[TestCase("J10", true)]
		[TestCase("B11", false)]
		[TestCase("K1", false)]
		[TestCase("whatever", false)]
		public void ValidateUserInput(string input, bool expectedResult)
		{
			// Arrange
			var engine = new GameEngine();

			// Act
			var result = engine.ValidateUserInput(input);

			// Assert
			result.Should().Be(expectedResult);
		}

		[Test]
		[TestCase("A5", 1)]
		[TestCase("A4", 0)]
		public void Attack(string cellKey, int timesShipHit)
		{
			// Arrange
			var ship = Mock.Of<Ship>();
			ship.ShipType = ShipTypeEnum.Destroyer;
			ship.Cells = new List<string>(){ "A5" };
			Mock.Get(ship).Setup(s => s.Hit());

			var engine = Mock.Of<GameEngine>(e => e.Sea == new Sea() { Ships = new List<Ship>() { ship } });
			Mock.Get(engine).CallBase = true;

			// Act
			engine.Attack(cellKey);

			// Assert
			Mock.Get(ship).Verify(s => s.Hit(), Times.Exactly(timesShipHit));
		}
	}
}
