using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UTTTNetLib.Packets
{
	public class JoinRoomPacket : RoomboundPacket
	{
		public static uint CURRENT_ROOM;

		public override byte GetID() => 1;

		protected override void HandleRoomboundClient(Socket s, uint roomID, byte[] data)
		{
			
		}

		protected override void HandleRoomboundServer(Socket s, uint roomID, byte[] data)
		{
			
		}
	}
}
