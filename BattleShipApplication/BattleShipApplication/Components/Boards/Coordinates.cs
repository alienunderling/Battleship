using System;
namespace BattleShipApplication.Components.Boards
{
	internal class Coordinates
	{
		int row { get; set; }
		int column { get; set; }

		public Coordinates(int x, int y)
		{
			column = x;
			row = y;
		}

		public int getRow()
		{
			return row;
		}

		public int getColumn()
		{
			return column;
		}
	} 
}
