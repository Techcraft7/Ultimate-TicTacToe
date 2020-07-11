using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UTTTGameLib;

namespace UTTTTests
{
	[TestClass]
	public class Tests
	{
		[TestMethod]
		public void OverLapTests()
		{
			bool[,] grid = new bool[8, 8]
			{
				{ false, false, false, false, false, false, false, false },
				{ false, false, false,  true, false, false, false, false },
				{ false, false,  true, false, false, false, false, false },
				{ false,  true, false, false, false, false, false, false },
				{  true, false, false, false, false, false, false, false },
				{ false, false, false, false, false, false, false, false },
				{ false, false, false, false, false, false, false, false },
				{ false, false, false, false, false, false, false, false }
			};
			if (!Overlaps.DoesOverlap(grid))
			{
				throw new Exception("Failed!");
			}
		}
	}
}
