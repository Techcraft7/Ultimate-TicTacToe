using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UTTTNetLib.Packets
{
	public class DisconnectPacket : Packet
	{
		public static byte DISCONNECT_REASON = 0;

		private readonly string reason;

		public DisconnectPacket(string reason) => this.reason = reason ?? "Unknown reason";

		public override byte GetID() => 0xFF;

		public override void HandleServerSide(Socket s) => _ = "Not used";

		public override void HandleClientSide(Socket s)
		{
			switch (NetUtils.Read(s, 1).First())
			{
				case 0:
					_ = MessageBox.Show($"{Encoding.ASCII.GetString(NetUtils.Read(s, BitConverter.ToInt32(NetUtils.Read(s, sizeof(int)), 0)))}");
					break;
				case 2:
				case 1:
					_ = MessageBox.Show($"Player {NetUtils.Read(s, 1).First()} wins! GG!");
					break;
			}
		}

		public override void Send(Socket s, byte[] _ = null)
		{
			byte[] data = Encoding.ASCII.GetBytes(reason);
			NetUtils.Write(s, new byte[] { DISCONNECT_REASON });
			DISCONNECT_REASON = 0;
			NetUtils.Write(s, BitConverter.GetBytes(Encoding.ASCII.GetByteCount(reason)));
			NetUtils.Write(s, data);
			s.Disconnect(false);
		}
	}
}
