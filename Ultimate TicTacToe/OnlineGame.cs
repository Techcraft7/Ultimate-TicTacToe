using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UTTTGameLib;
using UTTTNetLib;
using UTTTNetLib.Packets;

namespace UltimateTicTacToe
{
	public partial class OnlineGame : Form
	{
		private static readonly Packet[] PACKETS = new Packet[]
		{
			new DisconnectPacket(null),
			new JoinRoomPacket(),
			new GetRoomsPacket()
		};
		private const int CONNECT_TRIES = 5;
		private readonly IPEndPoint server;
		private Socket socket;

		public OnlineGame(IPEndPoint server)
		{
			InitializeComponent();
			socket = new Socket(SocketType.Stream, ProtocolType.Tcp)
			{
				ReceiveTimeout = 5000,
				SendTimeout = 5000
			};
			server = server ?? throw new ArgumentNullException(nameof(server));
			this.server = server;
		}

		private void OnlineGame_Shown(object sender, EventArgs e)
		{
			Task<bool> conTask = new Task<bool>(() =>
			{
				for (int i = 0; i < CONNECT_TRIES; i++)
				{
					Invoke(new Action(() =>
					{
						Text = $"Connecting: {i + 1}/{CONNECT_TRIES} tries";
						Application.DoEvents();
						Update();
					}));
					try
					{
						socket.Connect(server);
						return true;
					}
					catch
					{
						continue;
					}
				}
				return false;
			});
			conTask.Start();
			Utils.WaitForTask(conTask);
			Text = "Ultimate TicTacToe";
			if (!conTask.Result)
			{
				_ = MessageBox.Show("Could not connect to server!", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
				Close();
				return;
			}
			Task<bool> pingTask = new Task<bool>(() =>
			{
				PingPacket p = new PingPacket();
				p.Ping(socket);
				byte[] buf = new byte[1];
				_ = socket.Receive(buf, SocketFlags.None);
				if (buf.First() != 0)
				{
					return false;
				}
				p.HandleClientSide(socket);
				return true;
			});
			pingTask.Start();
			Utils.WaitForTask(pingTask);
			if (!pingTask.Result)
			{
				_ = MessageBox.Show("Invalid protocol!", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
				Close();
				return;
			}
			Task<bool> roomTasks = new Task<bool>(() =>
			{
				try
				{
					GetRoomsPacket p = new GetRoomsPacket();
					p.Send(socket, new byte[0]);
					p.HandleClientSide(socket);
					uint[] rooms = GetRoomsPacket.ROOM_IDS.ToArray();
					JoinRoomPacket jp = new JoinRoomPacket();
					foreach (uint r in rooms)
					{
						jp.Send(socket, BitConverter.GetBytes(r));
						jp.HandleClientSide(socket);
					}
				}
				catch
				{
					return false;
				}
				return true;
			});
			Utils.WaitForTask(roomTasks);
			if (roomTasks.Result)
			{
				_ = MessageBox.Show("Failed to connect to room!", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
				Close();
				return;
			}
			Task recThread = Task.Run(() =>
			{
				while (socket.Connected)
				{
					try
					{
						byte pID = NetUtils.Read(socket, 1).First();
						foreach (Packet p in PACKETS)
						{
							if (p.GetID() == pID)
							{
								p.HandleClientSide(socket);
								break;
							}
						}
					}
					catch (Exception error)
					{
						socket.Disconnect(false);
						Task.Delay(1000).ContinueWith((t) => Invoke(new Action(() =>
						{
							_ = MessageBox.Show($"Error: {error.GetType()} - {error.Message}");
							Close();
						})));
						return;
					}
				}
			});
			recThread.Start();
			Task updateTask = new Task(() =>
			{
				GetGameStatePacket p = new GetGameStatePacket();
				while (socket.Connected)
				{
					try
					{
						p.Send(socket, BitConverter.GetBytes((uint)JoinRoomPacket.CURRENT_ROOM));
						p.HandleClientSide(socket);
						Invoke(new Action(() =>
						{
							UTTTGame state = NetUtils.GetGameState(p.LAST_STATE);
							state.DrawOnPanel(ref GameDisplay);
						}));
					}
					catch
					{

					}
				}
			});
			updateTask.Start();
		}
	}
}
