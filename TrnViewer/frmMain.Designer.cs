namespace TrnViewer
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
			this.ctlFlt = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// lstMain
			// 
			this.lstMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lstMain.FormattingEnabled = true;
			this.lstMain.ItemHeight = 24;
			this.lstMain.Location = new System.Drawing.Point(0, 29);
			this.lstMain.Name = "lstMain";
			this.lstMain.Size = new System.Drawing.Size(370, 558);
			this.lstMain.TabIndex = 1;
			this.lstMain.SelectedIndexChanged += new System.EventHandler(this.lstMain_SelectedIndexChanged);
			this.lstMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstMain_KeyDown);
			// 
			// ctlFlt
			// 
			this.ctlFlt.Dock = System.Windows.Forms.DockStyle.Top;
			this.ctlFlt.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ctlFlt.Location = new System.Drawing.Point(0, 0);
			this.ctlFlt.Name = "ctlFlt";
			this.ctlFlt.Size = new System.Drawing.Size(370, 29);
			this.ctlFlt.TabIndex = 0;
			this.ctlFlt.TextChanged += new System.EventHandler(this.ctlFlt_TextChanged);
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(370, 587);
			this.Controls.Add(this.lstMain);
			this.Controls.Add(this.ctlFlt);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.Name = "frmMain";
			this.Text = "TrnViewer";
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox lstMain;
		private System.Windows.Forms.TextBox ctlFlt;
	}
}

