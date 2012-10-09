// Implemented on 13.07.2012 by me (Sava Varadzhakov)
// --------------------------------------------------
// Decision taken to not implement any grid - 
// sea is defined as a collection of ships (all the points that are not part of a ship are considered empty)
// 
// As suggested in the task description, no grid is exposed to the output of the console
// 
// Engine in a separated class for unit testing purposes
// Spring.NET used as a IOC solution
// Unit tests in BattleShip.Test project:
//  - console output messages not tested, just the business logic 
//  - all private methods made "protected internal virtual", so they can be setup in the unit test project
//  - in BattleShip project internals are made visible to the test project in order to test the protected internal methods too
// ---------------------------------------------------------------------------------------------------------------

namespace BattleShips
{
	using System;

	using Spring.Context.Support;

	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Welcome to this game of battleships");

			// get the engine out of the IOC container
			var gameEngine = (GameEngine)ContextRegistry.GetContext().GetObject("GameEngine");
			gameEngine.Start();

			Console.WriteLine(Environment.NewLine);
			Console.WriteLine("Thank you for playing. See ya!");
			Console.WriteLine("Press any key to exit..");
			Console.ReadLine();
		}

	}
}
