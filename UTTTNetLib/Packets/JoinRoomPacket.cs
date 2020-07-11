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
		public override byte GetID() => 1;

		public override void Write(Socket s)
		{
			throw new NotImplementedException();
		}

		protected override void HandleRoombound(Socket s, uint roomID, byte[] data)
		{
			
		}
	}
}
