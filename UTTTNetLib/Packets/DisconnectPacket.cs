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
		private readonly string reason;

		public DisconnectPacket(string reason)
		{
			this.reason = reason ?? throw new ArgumentNullException(nameof(reason));
		}

		public override byte GetID() => 0xFF;
		public override void Handle(Socket s)
		{
			_ = MessageBox.Show($"{Encoding.ASCII.GetString(NetUtils.Read(s, BitConverter.ToInt32(NetUtils.Read(s, sizeof(int)), 0)))}");
		}

		public override void Write(Socket s)
		{
			byte[] data = Encoding.ASCII.GetBytes(reason);
			NetUtils.Write(s, BitConverter.GetBytes(Encoding.ASCII.GetByteCount(reason)));
			NetUtils.Write(s, data);
			s.Disconnect(false);
		}
	}
}
