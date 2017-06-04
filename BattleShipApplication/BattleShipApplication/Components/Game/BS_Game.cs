using System;

namespace BattleShipApplication.Components.Game
{
	public class BattleShipGame
	{
		//member variables
		internal Player player1;
		internal Player player2;

		public BattleShipGame()
		{
			player1 = new Player();
			player2 = new Player();
		}

		public void startGame()
		{
			player1.setName("player1");
			player2.setName("player2");

			player1.setShipLocation();
			player2.setShipLocation();

			player1.outputBoards();
			player2.outputBoards();

			playGame();
		}

		public void playGame()
		{
			while (!player1.hasLost() && !player2.hasLost())
			{
				playRound();
			}

			player1.outputBoards();
			player2.outputBoards();

			if (player1.hasLost())
			{
				Console.WriteLine(player2.name + " has won the game!");
			}
			else if (player2.hasLost())
			{
				Console.WriteLine(player1.name + " has won the game!");
			}
		}

		public void playRound()
		{
			//Player1 Fires First
			Console.WriteLine(player1.name + ": Provide a location to hit {0}'s ship", player2.name);
			var p1Shot = Console.ReadLine();

			player2.processShot(p1Shot);

			//Player2 Fires Second
			Console.WriteLine(player2.name + ": Provide a location to hit {0}'s ship", player1.name);
			var p2Shot = Console.ReadLine();

			player1.processShot(p2Shot);
		}
	} 
}
