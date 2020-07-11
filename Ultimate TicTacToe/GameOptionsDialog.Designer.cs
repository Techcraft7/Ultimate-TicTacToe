namespace UltimateTicTacToe
{
	partial class GameOptionsDialog
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
		internal void InitializeComponent()
		{
			this.GameTypeSel = new System.Windows.Forms.ComboBox();
			this.OKButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.OnlineBox = new System.Windows.Forms.CheckBox();
			this.ServerBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// GameTypeSel
			// 
			this.GameTypeSel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.GameTypeSel.FormattingEnabled = true;
			this.GameTypeSel.Items.AddRange(new object[] {
            "One Player (against CPU)",
            "Two Player"});
			this.GameTypeSel.Location = new System.Drawing.Point(12, 32);
			this.GameTypeSel.Name = "GameTypeSel";
			this.GameTypeSel.Size = new System.Drawing.Size(236, 21);
			this.GameTypeSel.TabIndex = 0;
			// 
			// OKButton
			// 
			this.OKButton.Location = new System.Drawing.Point(12, 146);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(236, 23);
			this.OKButton.TabIndex = 1;
			this.OKButton.Text = "OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(236, 20);
			this.label1.TabIndex = 2;
			this.label1.Text = "Game Type";
			// 
			// OnlineBox
			// 
			this.OnlineBox.Location = new System.Drawing.Point(12, 59);
			this.OnlineBox.Name = "OnlineBox";
			this.OnlineBox.Size = new System.Drawing.Size(236, 24);
			this.OnlineBox.TabIndex = 3;
			this.OnlineBox.Text = "Online";
			this.OnlineBox.UseVisualStyleBackColor = true;
			this.OnlineBox.CheckedChanged += new System.EventHandler(this.OnlineBox_CheckedChanged);
			// 
			// ServerBox
			// 
			this.ServerBox.Location = new System.Drawing.Point(12, 89);
			this.ServerBox.MaxLength = 25;
			this.ServerBox.Name = "ServerBox";
			this.ServerBox.Size = new System.Drawing.Size(236, 20);
			this.ServerBox.TabIndex = 4;
			this.ServerBox.Text = "IP:Port";
			this.ServerBox.Visible = false;
			// 
			// GameOptionsDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(260, 183);
			this.Controls.Add(this.ServerBox);
			this.Controls.Add(this.OnlineBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.OKButton);
			this.Controls.Add(this.GameTypeSel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "GameOptionsDialog";
			this.Text = "Setup Game Options";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GameOptionsDialog_FormClosed);
			this.Shown += new System.EventHandler(this.GameOptionsDialog_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox GameTypeSel;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox OnlineBox;
		private System.Windows.Forms.TextBox ServerBox;
	}
}