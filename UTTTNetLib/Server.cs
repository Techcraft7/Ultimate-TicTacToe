using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UTTTNetLib
{
	using System.Diagnostics;
	using System.Net;
	using UTTTNetLib.Packets;
	using static NetUtils;
	public class Server
	{
		public static Dictionary<uint, Room> Rooms = new Dictionary<uint, Room>();
		private static Socket[] Clients;
		private static readonly Packet[] PACKETS = new Packet[]
		{
			new PingPacket(),
			new JoinRoomPacket(),
			new GetRoomsPacket(),
			new HTTPRequestPacket()
		};
		public static Server INSTANCE = null;

		private List<Thread> Threads = new List<Thread>();
		private int PORT = -1;
		public bool Running = true;
		private Socket SERVER_SOCKET = new Socket(SocketType.Stream, ProtocolType.Tcp);

		public Server(int maxRooms, int port)
		{
			if (INSTANCE != null)
			{
				throw new InvalidOperationException("Only one server allowed per process!");
			}
			INSTANCE = this;

			PORT = port;
			Clients = new Socket[maxRooms * 2];

			Log($"Starting Server on port {PORT}");

			Thread.CurrentThread.Name = "Main Server Thread";

			SERVER_SOCKET.Bind(new IPEndPoint(IPAddress.Any, PORT));
			SERVER_SOCKET.Listen(100);

			CreateRooms(maxRooms);
			CreateThreads(maxRooms);

			Thread.Sleep(1000);

			Log($"Server Started!");
		}

		public void Stop()
		{
			Log("Stopping!");
			Running = false;
			for (int i = 0; i < Threads.Count; i++)
			{
				Threads[i].Abort();
				Console.WriteLine($"Stopped {i + 1}/{Threads.Count} client threads");
			}
			Log("Done!");
		}

		private void CreateThreads(int maxRooms)
		{
			Threads.Clear();
			Log("Creating threads...");
			for (int i = 0; i < Clients.Length; i++)
			{
				Thread ct = new Thread(new ParameterizedThreadStart(ClientThread))
				{
					Name = $"Client Thread {i + 1}"
				};
				ct.Start(i);
				Thread.Sleep(10);
			}
		}

		private static void ClientThread(object obj)
		{
			int clientIndex = (int)obj;
			INSTANCE.Threads.Add(Thread.CurrentThread);
			Task waitForAbort = new Task(() =>
			{
				try
				{
					while (true) ;
				}
				catch (ThreadAbortException)
				{
					Log("Server stopped!");
					return;
				}
				catch (Exception)
				{
					while (true);
				}
			});
			try
			{
				waitForAbort.Start();
				while (INSTANCE.Running)
				{
					Log("Waiting for a connection...");
					Task<Socket> acceptTask = new Task<Socket>(() => INSTANCE.SERVER_SOCKET.Accept());
					acceptTask.Start();
					Task.WaitAny(acceptTask, waitForAbort);
					if (!acceptTask.IsCompleted)
					{
						break;
					}
					Socket s = acceptTask.Result;
					Log($"Connection recieved at {s.RemoteEndPoint}");
					while (s.Connected)
					{
						try
						{
							byte ID = Read(s, 1)[0];
							bool invalid = true;
							foreach (Packet p in PACKETS)
							{
								if (p.GetID() == ID)
								{
									invalid = false;
									Log($"Got packet {ID:X2}");
									p.HandleServerSide(s);
									break;
								}
							}
							if (invalid)
							{
								new DisconnectPacket($"Invalid packet: {ID:X2}").Write(s);
								s.Disconnect(false);
							}
						}
						catch (ThreadAbortException)
						{
							break;
						}
						catch (Exception e)
						{
							Log(e);
						}
					}
					Log("Client is no longer connected!");
				}
			}
			catch (Exception e)
			{
				if (!(e is ThreadAbortException))
				{
					Log(e);
				}
			}
		}

		private static void CreateRooms(int maxRooms)
		{
			Log("Creating rooms...");
			Random rng = new Random();
			for (int i = 0; i < maxRooms; i++)
			{
				Rooms.Add((uint)rng.Next(), new Room());
			}
			foreach (Room r in Rooms.Values)
			{
				r.Reset();
			}
		}
	}
}
