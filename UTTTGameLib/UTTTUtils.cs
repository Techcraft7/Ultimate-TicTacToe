using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTTTGameLib
{
	public static class UTTTUtils
	{
		public static void DrawCircle(Graphics g, Color c, int x, int y, int radius) => g.FillEllipse(new SolidBrush(c), new Rectangle(x - (radius / 2), y - (radius / 2), radius * 2, radius * 2));
	}
}
