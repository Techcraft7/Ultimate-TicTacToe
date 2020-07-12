using System.Collections.Generic;
using System.Net.Sockets;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTTTNetLib
{
	public abstract class Packet
	{
		public abstract byte GetID();
		/// <summary>
		/// Called when the Server recieves this packet
		/// </summary>
		/// <param name="s">The client socket</param>
		public abstract void HandleServerSide(Socket s);
		/// <summary>
		/// Called when the Client recieves this packet
		/// </summary>
		/// <param name="s">The server socket</param>
		public abstract void HandleClientSide(Socket s);

		public virtual void Send(Socket s, byte[] data) => NetUtils.SendBytes(s, (new byte[] { GetID() }).Concat(data).ToArray());
	}
}
