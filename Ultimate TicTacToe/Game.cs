using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UTTTGameLib;

namespace UltimateTicTacToe
{
	internal partial class Game : Form
	{
		public UTTTGame gameState = new UTTTGame();

		public Game(GameOptions gameOptions)
		{
			InitializeComponent();
		}

		private void GameDisplay_Paint(object sender, PaintEventArgs e) => gameState.DrawOnPanel(ref GameDisplay);

		private void GameDisplay_Resize(object sender, EventArgs e) => GameDisplay_Paint(null, null);

		private void GameDisplay_Click(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left || gameState.TurnIndex != 0)
			{
				return;
			}
			Tuple<int, int> coords = gameState.GetClickedPoint(e.Location, GameDisplay.Width, GameDisplay.Height);
			if (coords == null)
			{
				return;
			}
			if (!gameState.CanPlacePiece(coords.Item1, coords.Item2))
			{
				SystemSounds.Asterisk.Play();
				_ = MessageBox.Show("You cannot place a piece there!");
				return;
			}
			//Play
			foreach (Func<int> f in new Func<int>[]
			{
				() =>
				{
					gameState.PlayPiece(coords.Item1, coords.Item2, GameDisplay.Handle, out int w);
					return w;
				},
				() =>
				{
					Bot.Play(gameState, GameDisplay.Handle, out int w);
					return w;
				}
			})
			{
				int winner = f.Invoke();
				gameState.DrawOnPanel(ref GameDisplay);
				if (winner == -1)
				{
					return;
				}
				if (winner != 0)
				{
					Application.DoEvents();
					_ = MessageBox.Show($"Player {winner} wins!");
					Close();
					return;
				}
			}
		}
	}
}
