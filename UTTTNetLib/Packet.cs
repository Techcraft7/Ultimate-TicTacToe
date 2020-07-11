using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UTTTNetLib
{
	public abstract class Packet
	{
		public abstract byte GetID();
		public abstract void HandleServerSide(Socket s);
		public abstract void HandleClientSide(Socket s);
    }
}
