using System;
using System.Linq;
using System.Net.Sockets;

namespace UTTTNetLib
{
	public abstract class RoomboundPacket : Packet
	{
		public override void HandleServerSide(Socket s)
		{
			byte[] data = NetUtils.Read(s, sizeof(uint));
			uint ID = BitConverter.ToUInt32(data, 0);
			HandleRoomboundServer(s, ID);
		}

		public override void HandleClientSide(Socket s)
		{
			byte[] data = NetUtils.Read(s, sizeof(uint));
			uint ID = BitConverter.ToUInt32(data, 0);
			HandleRoomboundClient(s, ID);
		}

		protected abstract void HandleRoomboundServer(Socket s, uint roomID);
		protected abstract void HandleRoomboundClient(Socket s, uint roomID);
		
	}
}
