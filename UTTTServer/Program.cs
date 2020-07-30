using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UTTTNetLib;

namespace UTTTServer
{
	class Program
	{
		private const int PORT = 1234;
		private const int MAX_ROOMS = 5;
		private static Server s;

		private static void Main(string[] args)
		{
			Console.Title = $"UTTTServer | Hosting {MAX_ROOMS} rooms on port {PORT}";
			Console.CancelKeyPress += Console_CancelKeyPress;
			s = new Server(MAX_ROOMS, PORT);
			while (s.Running);
		}

		private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
		{
			e.Cancel = true;
			if (s == null)
			{
				return;
			}
			s.Stop();
			Console.WriteLine("Press escape to exit!");
			while (Console.ReadKey().Key != ConsoleKey.Escape);
			Console.WriteLine("Exiting!");
			Environment.Exit(0);
		}
	}
}
