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

		private string serv = "0.0.0.0:0";
		public string ServerString
		{
			get => serv;
			set
			{
				serv = value;
				_ = serv.Split('.').Length != 4 ? null : new object() ?? throw new ArgumentNullException();
				_ = serv.Split(':').Length != 2 ? null : new object() ?? throw new ArgumentNullException();
				ServerIP = IPAddress.Parse(serv.Split(':')[0]);
				ServerPort = ushort.Parse(serv.Split(':')[1]);
			}
		}

		public bool Online = false;
		public ushort ServerPort { get; private set; }
		public IPAddress ServerIP { get; private set; }
	}
}