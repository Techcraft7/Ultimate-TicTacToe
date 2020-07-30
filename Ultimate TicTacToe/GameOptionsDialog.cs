using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UltimateTicTacToe
{
	internal partial class GameOptionsDialog : Form
	{
		public GameOptions GameOptions { get; private set; } = new GameOptions();

		public GameOptionsDialog()
		{
			InitializeComponent();
			foreach (ComboBox cb in Controls.OfType<ComboBox>())
			{
				cb.SelectedIndex = 0;
			}
		}

		private void OKButton_Click(object sender, EventArgs e)
		{
			GameOptions.Players = GameTypeSel.SelectedIndex + 1;
			DialogResult = DialogResult.OK;
			Close();
		}

		private void GameOptionsDialog_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (DialogResult != DialogResult.OK)
			{
				DialogResult = DialogResult.Cancel;
			}
		}

		private void GameOptionsDialog_Shown(object sender, EventArgs e) => GameOptions = new GameOptions();
	}
}
