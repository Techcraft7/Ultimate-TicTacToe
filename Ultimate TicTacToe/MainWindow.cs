using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UltimateTicTacToe
{
	public partial class MainWindow : Form
	{
		private GameOptionsDialog gameOptions = new GameOptionsDialog();

		public MainWindow()
		{
			InitializeComponent();
		}

		private void PlayButton_Click(object sender, EventArgs e)
		{
			if (gameOptions.ShowDialog() == DialogResult.OK)
			{
				StartGame();
			}
			gameOptions = new GameOptionsDialog();
		}

		private void StartGame()
		{
			if (gameOptions.GameOptions.Online)
			{
				_ = new OnlineGame(new IPEndPoint(gameOptions.GameOptions.ServerIP, gameOptions.GameOptions.ServerPort)).ShowDialog();
			}
			else
			{
				_ = new Game(gameOptions.GameOptions).ShowDialog();
			}
		}
	}
}