using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTicTacToe
{
	internal static class Utils
	{
		public static void IfHasFlags<T>(Dictionary<T, Action> flags, T v) where T : Enum
		{
			foreach (T flag in flags.Keys)
			{
				if (v.HasFlag(flag))
				{
					flags[flag].Invoke();
				}
			}
		}
	}
}
