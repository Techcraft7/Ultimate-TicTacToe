namespace UltimateTicTacToe
{
	partial class Game
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.GameDisplay = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// GameDisplay
			// 
			this.GameDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
			this.GameDisplay.Location = new System.Drawing.Point(0, 0);
			this.GameDisplay.Name = "GameDisplay";
			this.GameDisplay.Size = new System.Drawing.Size(438, 375);
			this.GameDisplay.TabIndex = 0;
			this.GameDisplay.Paint += new System.Windows.Forms.PaintEventHandler(this.GameDisplay_Paint);
			this.GameDisplay.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GameDisplay_Click);
			this.GameDisplay.Resize += new System.EventHandler(this.GameDisplay_Resize);
			// 
			// Game
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(438, 375);
			this.Controls.Add(this.GameDisplay);
			this.Name = "Game";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Game";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel GameDisplay;
	}
}