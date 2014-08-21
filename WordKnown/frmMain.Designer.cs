namespace WordKnown
{
	partial class frmMain
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
			this.lstMain = new System.Windows.Forms.ListBox();
			this.lstKnown = new System.Windows.Forms.ListBox();
			this.lstCrap = new System.Windows.Forms.ListBox();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.miSave = new System.Windows.Forms.ToolStripMenuItem();
			this.miCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.miPaste = new System.Windows.Forms.ToolStripMenuItem();
			this.miKnown = new System.Windows.Forms.ToolStripMenuItem();
			this.miSelectCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lstMain
			// 
			this.lstMain.ColumnWidth = 400;
			this.lstMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstMain.FormattingEnabled = true;
			this.lstMain.ItemHeight = 25;
			this.lstMain.Location = new System.Drawing.Point(208, 31);
			this.lstMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.lstMain.MultiColumn = true;
			this.lstMain.Name = "lstMain";
			this.lstMain.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.lstMain.Size = new System.Drawing.Size(404, 300);
			this.lstMain.TabIndex = 0;
			this.lstMain.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstMain_KeyUp);
			// 
			// lstKnown
			// 
			this.lstKnown.Dock = System.Windows.Forms.DockStyle.Left;
			this.lstKnown.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lstKnown.FormattingEnabled = true;
			this.lstKnown.ItemHeight = 25;
			this.lstKnown.Location = new System.Drawing.Point(0, 31);
			this.lstKnown.MultiColumn = true;
			this.lstKnown.Name = "lstKnown";
			this.lstKnown.Size = new System.Drawing.Size(208, 300);
			this.lstKnown.TabIndex = 2;
			this.lstKnown.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstKnown_KeyUp);
			// 
			// lstCrap
			// 
			this.lstCrap.Dock = System.Windows.Forms.DockStyle.Right;
			this.lstCrap.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lstCrap.FormattingEnabled = true;
			this.lstCrap.ItemHeight = 25;
			this.lstCrap.Location = new System.Drawing.Point(612, 31);
			this.lstCrap.MultiColumn = true;
			this.lstCrap.Name = "lstCrap";
			this.lstCrap.Size = new System.Drawing.Size(208, 300);
			this.lstCrap.TabIndex = 3;
			this.lstCrap.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstCrap_KeyUp);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miSave,
            this.miCopy,
            this.miPaste,
            this.miKnown,
            this.miSelectCopy});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(820, 31);
			this.menuStrip1.TabIndex = 4;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// miSave
			// 
			this.miSave.Name = "miSave";
			this.miSave.Size = new System.Drawing.Size(86, 27);
			this.miSave.Text = "miSave";
			// 
			// miCopy
			// 
			this.miCopy.Name = "miCopy";
			this.miCopy.Size = new System.Drawing.Size(88, 27);
			this.miCopy.Text = "miCopy";
			// 
			// miPaste
			// 
			this.miPaste.Name = "miPaste";
			this.miPaste.Size = new System.Drawing.Size(93, 27);
			this.miPaste.Text = "miPaste";
			// 
			// miKnown
			// 
			this.miKnown.Name = "miKnown";
			this.miKnown.Size = new System.Drawing.Size(101, 27);
			this.miKnown.Text = "miKnown";
			// 
			// miSelectCopy
			// 
			this.miSelectCopy.Name = "miSelectCopy";
			this.miSelectCopy.Size = new System.Drawing.Size(123, 27);
			this.miSelectCopy.Text = "SelectCopy";
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(820, 331);
			this.Controls.Add(this.lstMain);
			this.Controls.Add(this.lstCrap);
			this.Controls.Add(this.lstKnown);
			this.Controls.Add(this.menuStrip1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "frmMain";
			this.Text = "Form1";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox lstMain;
        private System.Windows.Forms.ListBox lstKnown;
		private System.Windows.Forms.ListBox lstCrap;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem miSave;
		private System.Windows.Forms.ToolStripMenuItem miCopy;
		private System.Windows.Forms.ToolStripMenuItem miPaste;
		private System.Windows.Forms.ToolStripMenuItem miKnown;
		private System.Windows.Forms.ToolStripMenuItem miSelectCopy;
	}
}

