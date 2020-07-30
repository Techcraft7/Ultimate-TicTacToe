using System;
using System.Linq;
using System.Net;

namespace UltimateTicTacToe
{
	internal class GameOptions
	{
		private int players;
		public int Players
		{
			get => players;
			set
			{
				if (!new int[] { 1, 2 }.Contains(value))
				{
					throw new ArgumentException();
				}
				players = value;
			}
		}
	}
}