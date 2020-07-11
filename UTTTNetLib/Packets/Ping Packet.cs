using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UTTTNetLib.Packets
{
	public class PingPacket : Packet
	{
		private static Random rng = new Random();
		private static Dictionary<Socket, byte[]> pings = new Dictionary<Socket, byte[]>();

		public override byte GetID() => 0;
		public override void Handle(Socket s)
		{
			byte[] data = NetUtils.Read(s, 4);
			if (pings.ContainsKey(s))
			{
				pings.Remove(s);
			}
			pings.Add(s, data);
			Write(s);
		}

		public override void Write(Socket s)
		{
			if (pings.ContainsKey(s))
			{
				NetUtils.SendBytes(s, pings[s]);
				pings.Remove(s);
			}
			else
			{
				byte[] data = new byte[4];
				rng.NextBytes(data);
				NetUtils.SendBytes(s, data);
			}
		}
	}
}
