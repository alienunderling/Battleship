using BattleShipApplication.Components.Game;
using System;

namespace BattleShipApplication
{
    public class BattleShip
	{
		static void Main(string[] args)
		{
			BattleShipGame game = new BattleShipGame();
			/* Battleship Runner */
			Console.WriteLine("Ready to go, Players?  Lets place our ships! Press any key to continue.");
			Console.ReadKey();
			Console.WriteLine("Game Starting...");
			game.startGame();
		}
	}
}