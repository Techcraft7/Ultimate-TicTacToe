using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UTTTGameLib;

namespace UTTTNetLib.Packets
{
	public class GetGameStatePacket : RoomboundPacket
	{
		public Tuple<ulong, ulong, byte> LAST_STATE { get; private set; }

		public override byte GetID() => 0x03;

		protected override void HandleRoomboundClient(Socket s, uint roomID)
		{
			throw new NotImplementedException();
		}

		protected override void HandleRoomboundServer(Socket s, uint roomID)
		{
			ByteBuffer buf = new ByteBuffer();
			string binP1 = string.Empty;
			string binP2 = string.Empty;
			Room r = Server.INSTANCE.GetRoom(roomID);
			if (r != null)
			{
				return;
			}
			for (int y = 0; y < 8; y++)
			{
				for (int x = 0; x < 8; x++)
				{
					switch (r.Pieces[y, x])
					{
						case PieceState.NONE:
							binP1 += "0";
							binP2 += "0";
							break;
						case PieceState.P1:
							binP1 += "1";
							binP2 += "0";
							break;
						case PieceState.P2:
							binP1 += "0";
							binP2 += "1";
							break;
					}
				}
			}
			buf.WriteULong(Convert.ToUInt64(binP1, 2));
			buf.WriteULong(Convert.ToUInt64(binP2, 2));
			buf.WriteByte((byte)r.TurnIndex);
			Send(s, buf.ToArray());
		}
	}
}
