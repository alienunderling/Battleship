using System;
namespace BattleShipApplication.Components.Boards
{
	class Unit
	{
		internal Coordinates coordinates { get; set; }
		bool isOccupied = false;
		string status = "X";

		public Unit(int column, int row)
		{
			coordinates = new Coordinates(column, row);
		}

		public void setIsOccupied()
		{
			isOccupied = true;
			status = "S";
		}

		public bool getIsOccupied()
		{
			return isOccupied;
		}

		public void setShot()
		{
			if (isOccupied)
			{
				status = "H";
			}
			else
			{
				status = "M";
			}
		}

		public string getStatus()
		{
			return status;
		}
	}
}
