using System;
using System.Net;
using System.Net.Sockets;
using UTTTGameLib;

namespace UTTTNetLib
{
	public class Room
	{
		private UTTTGame state = new UTTTGame();

		public int TurnIndex => state.TurnIndex;
		public PieceState[,] Pieces => state.GameState;

		public EndPoint Player1;
		public EndPoint Player2;

		public Room() => Reset();

		public void Reset()
		{
			Player1 = Player2 = null;
			state = new UTTTGame();
		}

		public bool CanPlayerPlay(Socket s) => TurnIndex == 0 ? Player1.Equals(s.RemoteEndPoint) : Player2.Equals(s.RemoteEndPoint);
	}
}