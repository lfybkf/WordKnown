using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BDB;

namespace WordKnown
{
	public partial class frmMain : Form
	{
		Glossary glos = new Glossary();
		List<Kommand> kmds = new List<Kommand>();
		int SelectLim = 200;

		IEnumerable<WordTranslate> GetSelected()
		{
			return lstMain.SelectedItems.Cast<WordTranslate>();
		}//func

		public frmMain()
		{
			InitializeComponent();
		}//func

		public void InitializeKommands()
		{
			kmds.Add(new Kommand(Name: "Copy", Caption: "Копировать", Exec: kmdCopy, Scut: Keys.Control | Keys.Insert));
			kmds.Add(new Kommand(Name: "Paste", Caption: "Вставить", Exec: kmdPaste, Scut: Keys.Shift | Keys.Insert));
			kmds.Add(new Kommand(Name: "Known", Caption: "Убрать известные", Exec: kmdKnown, Scut: Keys.Control | Keys.Z));
			kmds.Add(new Kommand(Name: "Save", Caption: "Сохранить", Exec: kmdSave, Scut: Keys.Control | Keys.S));
			kmds.Add(new Kommand(Name: "SelectCopy", Caption: "Выбрать и копировать", Exec: kmdSelectCopy, Scut: Keys.Control | Keys.Space));

			foreach (var mi in menuStrip1.Items.OfType<ToolStripMenuItem>() )
			{
				kmds.LinkToComponent(mi);
				mi.ToolTipText = ((ToolStripMenuItem)mi).ShortcutKeys.ToString();
				mi.MouseHover += new System.EventHandler(this.mi_MouseHover);
			}//foreach
		}//func


		public void kmdCopy()
		{
			glos.Copy();
		}//func

		public void kmdSelectCopy()
		{
			lstMain.SelectedItems.Clear();
			int iSelected = 0;
			WordTranslate wt = null;

			//выделяем все, у которых нет перевода
			for (int i = 0; i < lstMain.Items.Count; i++ )
			{
				wt = (WordTranslate)lstMain.Items[i];
				if (wt.HasTranslate())
					continue;

				if (iSelected >= SelectLim)
					break;

				lstMain.SelectedIndex = i;
				iSelected++;
			}//for

			//копируем выделенное
			glos.Copy();
		}//func

		public void kmdPaste()
		{
			glos.Paste();
		}//func

		public void kmdSave()
		{
			glos.Save();
		}//func

		public void kmdKnown()
		{
			glos.SetKnown(lstKnown.Items.Cast<WordTranslate>());
			glos.SetCrap(lstCrap.Items.Cast<WordTranslate>());
			glos.Refresh();
			lstKnown.Items.Clear();
			lstCrap.Items.Clear();
		}//func

		private void frmMain_Load(object sender, EventArgs e)
		{
			InitializeKommands();

			glos.ctl = this.lstMain;
			glos.Selected = GetSelected;

			string[] args = Environment.GetCommandLineArgs();
			if (args.Count() > 1)
				glos.LoadText(args[1]);
			else
				glos.LoadText(Path.Combine(Environment.CurrentDirectory, "Input"));
		}//func

		private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (MessageBox.Show("Save?","ask"
				, MessageBoxButtons.OKCancel
				, MessageBoxIcon.Question) == DialogResult.OK)
				glos.Save();
		}

		private void lstMain_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Shift || e.Control || e.Alt)
				return;

			if (e.KeyCode == Keys.Enter)
			{
				lstKnown.Items.AddRange(
					lstMain.SelectedItems.Cast<WordTranslate>()
					.ToArray());
			}//if
			if (e.KeyCode == Keys.Delete)
			{
				lstCrap.Items.AddRange(
					lstMain.SelectedItems.Cast<WordTranslate>()
					.ToArray());
			}//if

		}

		private void lstKnown_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				ListBox lst = (ListBox)sender;
				lst.Items.Remove(lst.SelectedItem);
			}//if
		}

		private void lstCrap_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				ListBox lst = (ListBox)sender;
				lst.Items.Remove(lst.SelectedItem);
			}//if
		}//func

		private void zKmd_Click(object sender, EventArgs e)
		{
			Kommand kmd = Kommand.GetFromTag(sender);
			Kommand.Execute(kmd);
		}

		private void mi_MouseHover(object sender, EventArgs e)
		{
			Text = (sender as ToolStripMenuItem).ShortcutKeys.ToString();
		}//func

	}//class
}//ns
