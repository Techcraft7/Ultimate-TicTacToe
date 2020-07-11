using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTTTGameLib
{
	public static class Bot
	{
		public static void Play(UTTTGame s, IntPtr hPanel, out int winner)
		{
			Random rng = new Random();
			int x = 0;
			int y = 0;
			do
			{
				x = rng.Next(8);
				y = rng.Next(8);
			}
			while (!s.CanPlacePiece(x, y));
			s.PlayPiece(x, y, hPanel, out winner);
		}
	}
}
