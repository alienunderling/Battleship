using System;

namespace BattleShipApplication.Components.Ships
{
    public class Ship
	{
		int width = 3;
		int hits;

		public bool hasSunk()
		{
			return hits >= width;
		}

		public void isHit()
		{
			++hits;
		}
	}
}
