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
		public static uint? CURRENT_ROOM;

		public override byte GetID() => 0x01;

		protected override void HandleRoomboundClient(Socket s, uint roomID)
		{
			CURRENT_ROOM = null;
			byte status = NetUtils.Read(s, 1).First();
			switch (status)
			{
				case 1:
					NetUtils.Log($"JOINED ROOM!");
					CURRENT_ROOM = roomID;
					break;
			}
		}

		protected override void HandleRoomboundServer(Socket s, uint roomID)
		{
			Room r = Server.INSTANCE.GetRoom(roomID);
			byte status = 0;
			if (r != null)
			{
				if (r.Player1 == null)
				{
					r.Player1 = s.RemoteEndPoint;
					status = 1;
				}
				else if (r.Player2 == null)
				{
					r.Player2 = s.RemoteEndPoint;
					status = 1;
				}
				if (status == 1)
				{
					NetUtils.Log($"Client joined room {roomID:X8}");
				}
			}
			SendRoomBound(s, roomID, new byte[] { status });
		}
	}
}