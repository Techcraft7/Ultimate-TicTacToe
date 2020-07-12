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
		private static byte[] LAST_PING = new byte[4];
		private static Random rng = new Random();

		public override byte GetID() => 0x00;
		public override void HandleServerSide(Socket s)
		{
			Send(s, NetUtils.Read(s, 4));
		}

		public override void HandleClientSide(Socket s)
		{
			if (BitConverter.ToInt32(NetUtils.Read(s, 4), 0) != BitConverter.ToInt32(LAST_PING, 0))
			{
				s.Disconnect(false);
			}
		}

		public void Ping(Socket s)
		{
			rng.NextBytes(LAST_PING);
			Send(s, LAST_PING);
		}
	}
}
