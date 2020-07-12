using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTTTNetLib
{
	public class ByteBuffer
	{
		private List<byte> buf = new List<byte>();

		public byte[] ToArray() => buf.ToArray();

		public void WriteByte(byte s) => buf.Add(s);
		public void WriteBytes(byte[] data) => data.ToList().ForEach(b => buf.Add(b));
		public void WriteLong(long v) => buf = buf.Concat(BitConverter.GetBytes(v)).ToList();
		public void WriteULong(ulong v) => buf = buf.Concat(BitConverter.GetBytes(v)).ToList();
		public void WriteInt(int v) => buf = buf.Concat(BitConverter.GetBytes(v)).ToList();
		public void WriteUInt(uint v) => buf = buf.Concat(BitConverter.GetBytes(v)).ToList();
		public void WriteShort(short v) => buf = buf.Concat(BitConverter.GetBytes(v)).ToList();
		public void WriteUShort(ushort v) => buf = buf.Concat(BitConverter.GetBytes(v)).ToList();
	}
}
