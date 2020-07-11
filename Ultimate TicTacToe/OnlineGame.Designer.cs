namespace UltimateTicTacToe
{
	partial class OnlineGame
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
			this.GameDisplay.Size = new System.Drawing.Size(800, 450);
			this.GameDisplay.TabIndex = 0;
			// 
			// OnlineGame
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.GameDisplay);
			this.Name = "OnlineGame";
			this.Text = "OnlineGame";
			this.Shown += new System.EventHandler(this.OnlineGame_Shown);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel GameDisplay;
	}
}