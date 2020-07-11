using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTTTNetLib.WebInterface
{
	public static class HTTPParser
	{
		public static string GetResponse(string request)
		{
			string path = request.Split(' ')[1].Substring(1);
			string content = WebPages.INDEX;
			bool stop = false;
			switch (path)
			{
				case "stop":
					content = WebPages.STOP;
					stop = true;
					break;
			}
			content = Encoding.ASCII.GetString(Convert.FromBase64String(content));
			string res = $"HTTP/1.1 200 OK\r\nContent-Type: text/html\r\nConnection: close\r\nContent-Length: {Encoding.ASCII.GetByteCount(content)}\r\n\r\n{content}";
			if (stop)
			{
				NetUtils.Log("Stopping server in 5 seconds!", ConsoleColor.Cyan);
				Task.Delay(5000).ContinueWith((t) =>
				{
					NetUtils.Log("Server is stopping!", ConsoleColor.Magenta);
					Server.INSTANCE.Stop();
					Environment.Exit(0);
				});
			}
			return res;
		}
	}
}