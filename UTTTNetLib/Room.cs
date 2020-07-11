using System;

namespace UTTTNetLib
{
	public class Room
	{
		public int P1 { get; private set; }
		public int P2 { get; private set; }

		public Room() => Reset();

		public void Reset()
		{
			P1 = P2 = 0;
		}
	}
}