using System;
using System.Linq;
using System.Net.Sockets;

namespace UTTTNetLib
{
	public abstract class RoomboundPacket : Packet
	{
		public override void Handle(Socket s)
		{
			byte[] data = NetUtils.Read(s, sizeof(uint));
			uint ID = BitConverter.ToUInt32(data, 0);
			HandleRoombound(s, ID, data.Skip(sizeof(uint)).ToArray());
		}

		protected abstract void HandleRoombound(Socket s, uint roomID, byte[] data);
	}
}
