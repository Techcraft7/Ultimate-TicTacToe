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
		public static List<uint> ROOM_IDS = new List<uint>();

		public override byte GetID() => 2;

		public override void HandleServerSide(Socket s)
		{
			ByteBuffer buf = new ByteBuffer();
			foreach (uint rID in Server.Rooms.Select(kv => kv.Key))
			{
				buf.WriteUInt(rID);
			}
			Send(s, buf.ToArray());
		}

		public override void HandleClientSide(Socket s)
		{
			ROOM_IDS.Clear();
			int size = BitConverter.ToInt32(NetUtils.Read(s, sizeof(int)), 0);
			for (int i = 0; i < size; i++)
			{
				ROOM_IDS.Add(BitConverter.ToUInt32(NetUtils.Read(s, sizeof(uint)), 0));
			}
		}
	}
}