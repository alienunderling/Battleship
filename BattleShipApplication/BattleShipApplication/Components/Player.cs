using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using BattleShipApplication.Components.Board;
using BattleShipApplication.Components.Boards;
using BattleShipApplication.Components.Ships;

namespace BattleShipApplication.Components
{
	public class Player
	{
		//member variables
		internal string name;
		BattleShipBoard gameBoard;
		string shipLocation;
		Ship ship;
		Coordinates startRange;
		Coordinates endRange;

		public Player()
		{
			gameBoard = new BattleShipBoard();
			ship = new Ship();
		}

		public void setName(string playerName)
		{
			name = playerName;
		}

		public bool hasLost()
		{
			return ship.hasSunk();
		}

		public void setShipLocation()
		{
			string[] coordinates = new string[2];

			Console.WriteLine("Please enter the ship location {0}. Format: A3 A5", name);

			shipLocation = Convert.ToString(Console.ReadLine());

			coordinates = parseLocation(shipLocation);

			while (!checkCoord(coordinates))
			{
				Console.WriteLine("Sorry Player {0} please re-enter. Something was not right with your coordinates. Format: A3 A6", name);

				shipLocation = Convert.ToString(Console.ReadLine());

				coordinates = parseLocation(shipLocation);
			}

			gameBoard.PlaceShip(startRange, endRange);
		}

		/*
         * Check the validity of the coordinate set.
         * eg 'A3, B7'
         */
		public bool checkCoord(string[] coords)
		{
			bool goodCoords = true;
			//'p' is a placeholder to move to base 1
			char[] coordArray = { 'p', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };

			string c1 = coords[0];//A3
			string c2 = coords[1];//B7

			//Check if the user entered a correct format
			if ((c1 == null) || (c2 == null))
			{
				return false;
			}

			//Get the Alpha character from each coord
			char col1 = c1[0];
			char col2 = c2[0];

			//Get the Numeric character from each coord
			char rnum1 = c1[1];
			char rnum2 = c2[1];
			//We need these to be int's.
			int y1 = (int)Char.GetNumericValue(rnum1);
			int y2 = (int)Char.GetNumericValue(rnum2);

			int x1 = Array.IndexOf(coordArray, col1);
			int x2 = Array.IndexOf(coordArray, col2);

			if ((x1 == x2 && y1 == y2) || (x1 != x2 && y1 != y2))
			{
				Console.WriteLine("First Fail: coord NOT ok");
				goodCoords = false;
			}

            if( (x1 == x2) && (x1 >= 1) && (x1 <= 8) && (y1 >= 1) && (y1 <= 8) && (x2 >= 1) && (x2 <= 8) && (y2 >= 1) && (y2 <= 8))
			{
				if (y1 > y2 && (y1 - y2) == 2)
				{
					Console.WriteLine("coord ok");
				}
				else if (y1 < y2 && (y2 - y1) == 2)
				{
					Console.WriteLine("coord ok");
				}
				else
				{
					Console.WriteLine("Second Fail: coord NOT ok");
					goodCoords = false;
				}
			}
			else
			{
				if (x1 > x2 && (x1 - x2) == 2)
				{
					Console.WriteLine("coord ok");
				}
				else if (x1 < x2 && (x2 - x1) == 2)
				{
					Console.WriteLine("coord ok");
				}
				else
				{
					Console.WriteLine("Third Fail: coord NOT ok");
					goodCoords = false;
				}
			}

			if (goodCoords)
			{
				startRange = new Coordinates(x1, y1);
				endRange = new Coordinates(x2, y2);
			}

			return goodCoords;
		}

		/* 
         * The coordinates are all in one string.  
         * Break them apart so they are separate and can be properly parsed from there.
         * 
         * Does check if they are within the bounds of the board.
         */
		public string[] parseLocation(string location)
		{
			//This reg exp ensures the ship location is on the board.
			string re1 = "((?:[a-h][a-h]*[1-8]+[a-h1-8]*))";  // Coordinate
			string re2 = "(\\s+)";    // white space in between

			Regex r = new Regex(re1 + re2 + re1, RegexOptions.IgnoreCase | RegexOptions.Singleline);
			Match m = r.Match(location);

			string[] coordinates = new string[2];

			if (m.Success)
			{
				String coord1 = m.Groups[1].ToString();
				//String ws = m.Groups[2].ToString(); //NOT USED
				String coord2 = m.Groups[3].ToString();

				coordinates[0] = coord1;
				coordinates[1] = coord2;
			}
			else
			{
				Console.WriteLine("Enter coordinates between [A-H], [1-8])");
			}

			return coordinates;
		}

		//TODO: Refactor - There must be a better way to get the units out of the gameBoard
		public void outputBoards()
		{
			var currentUnitList = new List<Unit>();
			Unit tempUnit;

			Console.WriteLine("{0} board", name);
			Console.WriteLine(Environment.NewLine);

			//First print the Column titles...
			string[] clmns = { "   ", " A ", " B ", " C ", " D ", " E ", " F ", " G ", " H " };

			for (int i = 0; i <= 8; i++)
			{
				Console.Write(clmns[i]);
			}

			Console.WriteLine(Environment.NewLine);

			for (int row = 1; row <= 8; row++)
			{
				Console.Write(" {0} ", row);
				for (int ownColumn = 1; ownColumn <= 8; ownColumn++)
				{
					currentUnitList = gameBoard.GameBoard.Where(x => x.coordinates.getRow() == row && x.coordinates.getColumn() == ownColumn).ToList();
					tempUnit = currentUnitList[0];
					Console.Write(" {0} ", tempUnit.getStatus());
				}

				Console.WriteLine(Environment.NewLine);
			}

			Console.WriteLine(Environment.NewLine);
		}

		public void processShot(string shot)
		{
			Unit hitUnit;
			char c1;
			char c2;
			int xValue;
			int yValue;
			var currentUnitList = new List<Unit>();
			//'p' is a placeholder to move to base 1
			char[] coordArray = { 'p', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };
			bool outOfBounds = true;

			do
			{
				//Check if in bounds
				c1 = shot[0];
				c2 = shot[1];

				xValue = Array.IndexOf(coordArray, c1);
				//convert to int
				yValue = (int)Char.GetNumericValue(c2);

				if (((xValue > 8) || (yValue > 8)) || ((xValue < 1) || (yValue < 1)))
				{
					Console.WriteLine("Shot outside of bounds.  Please re-enter:");
					shot = Console.ReadLine();
				}
				else
				{
					outOfBounds = false;
				}
			}
			while (outOfBounds);

			//find Unit that was fired at
			currentUnitList = gameBoard.GameBoard.Where(x => x.coordinates.getColumn() == xValue && x.coordinates.getRow() == yValue).ToList();
			hitUnit = currentUnitList[0];
			hitUnit.setShot();

			if (hitUnit.getIsOccupied())
			{
				//Call out a hit
				Console.WriteLine(name + " says: \"Hit!\"");

				ship.isHit();
			}
			else
			{
				//Call out a miss
				Console.WriteLine(name + " says: \"Miss!\"");
			}
		}
	}
}
