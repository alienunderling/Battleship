using NUnit.Framework;
using System;
using BattleShipApplication.Components;

namespace BattleShipApplication.Tests
{
    [TestFixture()]
    public class TestCheckCoord
    {
		Player testPlayer;
        bool result;

		[TestFixtureSetUp]
		public void TestSetup()
		{
			testPlayer = new Player();
		}

        [Test()]
        public void coordsRangeIsGood()
        {
            string[] testCoords = { "A3", "A5" };

            result = testPlayer.checkCoord(testCoords);

            Assert.IsTrue(result);
		}

		[Test()]
		public void coordsRangeIsTooLong()
		{
			string[] testCoords = { "A3", "A6" };

			result = testPlayer.checkCoord(testCoords);

            Assert.IsFalse(result);
		}

		[Test()]
		public void coordsRangeIsOffBoardAlpha()
		{
			string[] testCoords = { "Z3", "Z5" };

			result = testPlayer.checkCoord(testCoords);
			Assert.IsFalse(result);
		}

		[Test()]
		public void coordsRangeIsOffBoardNumeric()
		{
			string[] testCoords = { "A7", "A9" };

			result = testPlayer.checkCoord(testCoords);
			Assert.IsFalse(result);
		}

		[Test()]
		public void coordsUsingIncorrectFormat()
		{
			string[] testCoords = { "37", "39" };

			result = testPlayer.checkCoord(testCoords);

			Assert.IsFalse(result);
		}

		[TestFixtureTearDown]
		public void TestTearDown()
		{
			testPlayer = null;
		}
    }

	[TestFixture()]
	public class TestParseLocation
	{

		Player testPlayer;
		string[] result;

		[TestFixtureSetUp]
		public void TestSetup()
		{
			testPlayer = new Player();
		}

		[Test()]
		public void FirstLocationValueShouldBeFirstInResultArray()
		{
			string location = "A3 A5";

            result = testPlayer.parseLocation(location);

            Assert.AreEqual("A3", result[0]);
		}

		[Test()]
		public void SecondLocationValueShouldBeSecondInResultArray()
		{
			string location = "A3 A5";

			result = testPlayer.parseLocation(location);

            Assert.AreEqual("A5", result[1]);
		}

		[Test()]
		public void ValuesOutOfRangeCase1ResultNull()
		{
			string location = "A7 A9";

			result = testPlayer.parseLocation(location);

			Assert.IsNull(result[0]);
		}

		[Test()]
		public void ValuesOutOfRangeCase2()
		{
			string location = "H3 J5";

			result = testPlayer.parseLocation(location);

			Assert.IsNull(result[1]);
		}

		[TestFixtureTearDown]
		public void TestTearDown()
		{
			testPlayer = null;
		}
	}
}
