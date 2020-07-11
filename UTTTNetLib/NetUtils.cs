using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
	}
}
