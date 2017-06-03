using System;
using System.Collections.Generic;
using System.Linq;
using BattleShipApplication.Components.Boards;

namespace BattleShipApplication.Components.Board
{
	class BattleShipBoard
	{
		public List<Unit> GameBoard { get; set; }

		public BattleShipBoard()
		{
			GameBoard = new List<Unit>();

			for (int i = 1; i <= 8; i++)
			{
				for (int j = 1; j <= 8; j++)
				{
					GameBoard.Add(new Unit(i, j));
				}
			}
		}

		public void PlaceShip(Coordinates start, Coordinates end)
		{
			//Set the Units corresponding to the coord range to occupied.
			//Find the corresponding units and set them to isOccupied

			var occupiedUnits = new List<Unit>();

			occupiedUnits = GameBoard.Where(x => x.coordinates.getRow() >= start.getRow()
											&& x.coordinates.getColumn() >= start.getColumn()
											&& x.coordinates.getRow() <= end.getRow()
											&& x.coordinates.getColumn() <= end.getColumn()).ToList();

			foreach (Unit unit in occupiedUnits)
			{
				unit.setIsOccupied();
			}
		}
	}
}
