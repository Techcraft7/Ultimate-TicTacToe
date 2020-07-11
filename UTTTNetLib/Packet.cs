using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UTTTNetLib
{
	public abstract class Packet
	{
		public abstract byte GetID();
		public abstract void Handle(Socket s);
		public abstract void Write(Socket s);
    }
}
