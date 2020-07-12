using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UTTTGameLib;

namespace UTTTNetLib
{
	public static class NetUtils
	{
		public static void Log(string s, ConsoleColor c = ConsoleColor.Gray)
		{
			var prev = Console.ForegroundColor;
			Console.ForegroundColor = c;
			Console.WriteLine($"[{Thread.CurrentThread.Name ?? "INFO"}] {s}");
			Console.ForegroundColor = prev;
		}

		public static void Log(Exception e)
		{
			Log($"{e.GetType()}: {e.Message}", ConsoleColor.Red);
		}

		public static byte[] Read(Socket s, int n)
		{
			byte[] data = new byte[n];
			_ = s.Receive(data, data.Length, SocketFlags.None);
			return data;
		}

		public static void Write(Socket s, byte[] data) => _ = s.Send(data, data.Length, SocketFlags.None);

		public static void SendBytes(Socket s, byte[] data) => _ = s.Send(data, SocketFlags.None);

		public static UTTTGame GetGameState(Tuple<ulong, ulong, byte> state)
		{
			UTTTGame s = new UTTTGame
			{
				TurnIndex = state.Item3
			};
			string bin1 = Convert.ToString((long)state.Item1, 2).PadLeft(64, '0');
			string bin2 = Convert.ToString((long)state.Item2, 2).PadLeft(64, '0');
			for (int i = 0; i < bin1.Length; i++)
			{
				switch (bin1[i])
				{
					case '1':
						s.SetPiece(i % 8, i / 8, PieceState.P1);
						break;
				}
				switch (bin2[i])
				{
					case '1':
						s.SetPiece(i % 8, i / 8, PieceState.P2);
						break;
				}
			}
			return s;
		}
	}
}
