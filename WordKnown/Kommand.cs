using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WordKnown
{
	public class Kommand
	{
		string Name;
		Action Exec;
		Shortcut Scut = Shortcut.None;

		public static Kommand Empty
			= new Kommand("Empty", null, Shortcut.None);

		public Kommand(string Name, Action Exec, Shortcut Scut)
		{
			this.Name = Name;
			this.Exec = Exec;
			this.Scut = Scut;
		}//func

		public void Execute()
		{
			if (Exec != null)
			{
				Exec();
			}//if
		}//func

		public static Kommand Execute(IEnumerable<Kommand> lst, Func<Kommand, bool> Match)
		{
			foreach (Kommand k in lst)
			{
				if (Match(k))
				{
					k.Execute();
					return k;
				}//if
			}//for
			return null;
		}//func

		public static Kommand Execute(IEnumerable<Kommand> lst, Shortcut scut)
		{
			return Execute(lst, (k => k.Scut == scut));
		}//func

		public static Kommand Execute(IEnumerable<Kommand> lst, string Name)
		{
			return Execute(lst, (k => k.Name == Name));
		}//func

		public override string ToString()
		{
			return Name;
		}//func

		#region shortcut
		public static Shortcut GetShortcut(Keys key, Keys mods)
		{
			if (mods.HasFlag(Keys.Control))
			{
				if (key == Keys.C)
					return Shortcut.CtrlC;
				else if (key == Keys.O)
					return Shortcut.CtrlO;
				else if (key == Keys.S)
					return Shortcut.CtrlS;
				else if (key == Keys.V)
					return Shortcut.CtrlV;
				else if (key == Keys.Z)
					return Shortcut.CtrlZ;
				else if (key == Keys.Delete)
					return Shortcut.CtrlDel;
				else if (key == Keys.Insert)
					return Shortcut.CtrlIns;
			}//if
			else if (mods.HasFlag(Keys.Shift))
			{
				if (key == Keys.Insert)
					return Shortcut.ShiftIns;
				else if ( key== Keys.Delete)
					return Shortcut.ShiftDel;
			}//if

			return Shortcut.None;
		}//func
		#endregion

	}//class

}//ns
