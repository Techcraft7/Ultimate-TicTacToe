using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UTTTNetLib;

namespace UTTTServer
{
	class Program
	{
		private static Server s;

		private static void Main(string[] args)
		{
			Console.CancelKeyPress += Console_CancelKeyPress;
			s = new Server(1, 6969);
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
