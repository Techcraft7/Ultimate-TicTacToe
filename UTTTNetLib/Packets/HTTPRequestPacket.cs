using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UTTTNetLib.WebInterface;

namespace UTTTNetLib.Packets
{
	public class HTTPRequestPacket : Packet
	{
		public override byte GetID() => (byte)'G';

		public override void HandleClientSide(Socket s) => _ = "Not used";

		public override void HandleServerSide(Socket s)
		{
			byte[] buf = new byte[1] { GetID() };
			while (buf.Last() != (byte)'\r')
			{
				buf = buf.Concat(NetUtils.Read(s, 1)).ToArray();
			}
			buf = buf.Where(c => c != (byte)'\r').ToArray();
			string req = Encoding.ASCII.GetString(buf);
			if (!req.StartsWith("GET /"))
			{
				s.Disconnect(false);
				return;
			}

			string res = HTTPParser.GetResponse(req);
			_ = s.Send(Encoding.ASCII.GetBytes(res), SocketFlags.None);
			s.Disconnect(false);
		}
	}
}
