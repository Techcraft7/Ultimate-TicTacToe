using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UltimateTicTacToe
{
	public partial class OnlineGame : Form
	{
		private const int CONNECT_TRIES = 5;
		private readonly IPEndPoint server;
		private Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp);

		public OnlineGame(IPEndPoint server)
		{
			InitializeComponent();
			server = server ?? throw new ArgumentNullException(nameof(server));
			this.server = server;
		}

		private void OnlineGame_Shown(object sender, EventArgs e)
		{
			Task<bool> conTask = new Task<bool>(() =>
			{
				for (int i = 0; i < CONNECT_TRIES; i++)
				{
					Invoke(new Action(() => Text = $"Connecting: {i + 1}/{CONNECT_TRIES} tries"));
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
			//Wait for task to complete without freezing form
			while (!conTask.IsCompleted)
			{
				Application.DoEvents();
				Update();
			}
			Text = "Ultimate TicTacToe";
			if (conTask.Result)
			{
				return;
			}
			else
			{
				_ = MessageBox.Show("Could not connect to server!", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
				Close();
			}
		}
	}
}
