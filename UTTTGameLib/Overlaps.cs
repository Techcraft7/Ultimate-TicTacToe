using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTTTGameLib
{
	public static class Overlaps
	{
		public static readonly List<bool[,]> WinConditions = new List<bool[,]>()
		{
			new bool[4, 1]
			{
				{ true },
				{ true },
				{ true },
				{ true }
			},
			new bool[1, 4]
			{
				{ true, true, true, true }
			},
			new bool[4, 4]
			{
				{  true, false, false, false },
				{ false,  true, false, false },
				{ false, false,  true, false },
				{ false, false, false,  true }
			},
			new bool[4, 4]
			{
				{ false, false, false,  true },
				{ false, false,  true, false },
				{ false,  true, false, false },
				{  true, false, false, false }
			}
		};

		public static bool DoesOverlap(bool[,] grid)
		{
			foreach (bool[,] wc in WinConditions)
			{
				if (FindPattern(grid, wc))
				{
					return true;
				}
			}
			return false;
		}

		public static bool FindPattern(bool[,] grid, bool[,] pattern, bool includeEmpty = false)
		{
			//False if grid is smaller than pattern
			for (int i = 0; i <= 1; i++)
			{
				if (grid.GetLength(i) < pattern.GetLength(i))
				{
					return false;
				}
			}
			//Subtract pattern's width and height so we don't go off the grid
			for (int yo = 0; yo < grid.GetLength(0) - pattern.GetLength(0); yo++)
			{
				for (int xo = 0; xo < grid.GetLength(1) - pattern.GetLength(1); xo++)
				{
					bool fail = false;
					for (int y = 0; y < pattern.GetLength(0); y++)
					{
						for (int x = 0; x < pattern.GetLength(1); x++)
						{
							//Only check if true
							if (pattern[y, x] || includeEmpty)
							{
								if (!grid[y + yo, x + xo])
								{
									fail = true;
									x = y = int.MaxValue;
									break;
								}
							}
						}
					}
					if (!fail)
					{
						return true;
					}
				}
			}
			return false;
		}

		private static void PrintBoolGrid(bool[,] grid)
		{
			Console.WriteLine($"\nPATTERN {grid.GetLength(1)}x{grid.GetLength(0)}:");
			for (int y = 0; y < grid.GetLength(0); y++)
			{
				for (int x = 0; x < grid.GetLength(1); x++)
				{
					Console.Write(grid[y, x] ? "1" : "0");
				}
				Console.WriteLine();
			}
			Console.WriteLine();
		}
	}
}