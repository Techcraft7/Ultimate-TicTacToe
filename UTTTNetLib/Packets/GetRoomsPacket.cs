using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UTTTNetLib.Packets
{
	public class GetRoomsPacket : Packet
	{
		private static List<uint> ROOM_IDS => Server.Rooms.Select(kv => kv.Key).ToList();

		public override byte GetID() => 2;
		public override void Handle(Socket s)
		{
			ROOM_IDS.Clear();
			int size = BitConverter.ToInt32(NetUtils.Read(s, sizeof(int)), 0);
			for (int i = 0; i < size; i++)
			{
				ROOM_IDS.Add(BitConverter.ToUInt32(NetUtils.Read(s, sizeof(uint)), 0));
			}
		}

		public override void Write(Socket s)
		{
			throw new NotImplementedException();
		}
	}
}