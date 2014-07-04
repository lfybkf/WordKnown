using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using KeyValue = System.Collections.Generic.KeyValuePair<string, string>;

namespace Parer
{
	public class Dwarf
	{
		static string fmt = "{0} - {1}";
		static int iPageLimit = 10;

		string[] Content;
		List<string> Error = new List<string>();
		bool ErrorMode = false;
		int iPageNum = 0;
		public RoutedEventHandler handler;

		public Dwarf(string path)
		{
			string[] hContent;
			hContent = File.ReadAllLines(path, Encoding.GetEncoding(1251));
			Content = hContent.OrderBy(s => s.Substring(2)).ToArray();
		}//func

		public string Contains(string Eng, string Rus)
		{
			if (string.IsNullOrEmpty(Eng) || string.IsNullOrEmpty(Rus))
				return string.Empty;

			string find = string.Format(fmt, Eng, Rus);
			if (Content.Contains(find))
			{
				if (ErrorMode)
					Error.Remove(Eng);

				return find;
			}//if
			else
			{
				//if (!Error.Contains(Eng))
					Error.Add(Eng);

				return string.Empty;
			}
		}//func



		public void FillPage(Panel panEng, Panel panRus)
		{
			IEnumerable<string> ssNext;
			ssNext = Content
				.Skip(iPageLimit * iPageNum)
				.Take(iPageLimit);

			if (!ssNext.Any())
			{
				ErrorMode = true;
				ssNext = Content.Where(s => Error.Contains(s.Eng()));
			}

			FillStream(panEng, panRus, ssNext);
			iPageNum++;
		}//func

		private void FillStream(Panel panEng, Panel panRus, IEnumerable<string> ssNext)
		{
			panEng.Children.Clear();
			panRus.Children.Clear();

			RadioButton btn;
			foreach (string item in ssNext
				.Select(s => s.Eng()))
			{
				btn = new RadioButton() { Content = item };
				btn.Checked += handler;
				panEng.Children.Add(btn);
			}//for

			foreach (string item in ssNext
				.Select(s => s.Rus()).OrderBy(s => s))
			{
				btn = new RadioButton() { Content = item };
				btn.Checked += handler;
				panRus.Children.Add(btn);
			}//for
		}//func
	}//class

	public static class StringExtension
	{
		static string D = " - ";
		public static string Eng(this string s)
		{
			return s.Substring(0, s.IndexOf(D));
		}//func

		public static string Rus(this string s)
		{
			return s.Substring(s.IndexOf(D) + D.Length);
		}//func

	}//class
}//ns
