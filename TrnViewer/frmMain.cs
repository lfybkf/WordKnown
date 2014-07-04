using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TrnViewer
{
	public partial class frmMain : Form
	{
		string[] Content;
		public frmMain()
		{
			InitializeComponent();
		}//func

		private void frmMain_Load(object sender, EventArgs e)
		{
			Left = 0;
			Top = 0;
			Height = Screen.PrimaryScreen.WorkingArea.Height;

			string[] args = Environment.GetCommandLineArgs();
			if (args.Count() > 1)
				LoadTrn(args[1]);
			else
				LoadTrn("test.trn");

			ctlFlt.Focus();
		}//func

		public void LoadTrn(string file)
		{
			if (!File.Exists(file))
			{
				MessageBox.Show("NoArgs"); 
				Close();
			}//if
			Content = File.ReadAllLines(file, Encoding.GetEncoding(1251));
			lstMain.DataSource = Content;
		}//func

		private void lstMain_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Space)
			{
				int iNext = lstMain.SelectedIndex + 20;
				if (lstMain.Items.Count > iNext)
					lstMain.SelectedIndex = iNext;
			}//if
			else
				e.Handled = false;
		}

		private void lstMain_SelectedIndexChanged(object sender, EventArgs e)
		{
			lstMain.TopIndex = lstMain.SelectedIndex;
		}

		private void ctlFlt_TextChanged(object sender, EventArgs e)
		{
			if (ctlFlt.Text.EndsWith(" "))
			{
				ctlFlt.Text = string.Empty;
				lstMain.SelectedIndex = 0;
			}//if
			else
			{
				int i = lstMain.FindString(ctlFlt.Text);
				if (i >= 0)
					lstMain.SelectedIndex = i;
			}//else
		}//func
	}//class
}//ns
