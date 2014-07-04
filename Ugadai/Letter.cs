using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Ugadai
{
	class Letter
	{
		static int Lim = 2;
		public static char[] Rus = Enumerable.Range(1040, 32)
			.Select(i => (char)i).ToArray();
		public static char[] Eng = Enumerable.Range(65, 26)
			.Select(i => (char)i).ToArray();

		public static void AddButtons(Panel pan, char[] cc)
		{
			Button btn;

			for (int i = 0; i < cc.Length; i += Lim)
			{
				btn = new Button();
				btn.Content = new string(cc.Skip(i).Take(Lim).ToArray());
				btn.Width = Lim * 40;
				pan.Children.Add(btn);
			}//for
		}//func

		public static char[] GetFromButton(Button btn)
		{
			return ((string)btn.Content).ToCharArray();
		}//func

	}
}
