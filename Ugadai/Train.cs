using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media;

using KeyValue = System.Collections.Generic.KeyValuePair<string, string>;

namespace Ugadai
{
	class Train: INotifyPropertyChanged
	{
		static readonly string propMask = "Mask";
		static readonly string propBrushDone = "BrushDone";

		static readonly string Delim = " - ";
		static readonly char[] ccDelim = 
			Delim.AsEnumerable<char>().Distinct().ToArray();

		static char emp = '_';
		static List<string> lstUsed = new List<string>();
		static string pathUsed = "used.txt";
		static readonly Encoding Enc = Encoding.GetEncoding(1251);

		public Lang lang = Lang.None;
		public int iPod = 2;
		string[] Content;
		string Line;
		List<char> Existed = new List<char>();
		
		public string Mask { get {
			return MaskedLine();
		} }
		Brush mBrushDone = Brushes.Bisque;
		public Brush BrushDone { get { return mBrushDone;  }
			private set { mBrushDone = value; NotifyPropertyChanged(propBrushDone); }
		}

		public void Load(string path)
		{
			Content = File.ReadAllLines(path, Enc);
			lstUsed.AddRange(File.ReadAllLines(pathUsed, Enc));
		}//func

		public bool Begin()
		{
			Use(Line);
			Existed.Clear();
			Existed.AddRange(ccDelim);

			var qry = from s in Content
								where lstUsed.Contains(s) == false
								select s;

			string[] ss = qry.ToArray();

			if (ss.Length == 0)
				return false;

			Line = ss[DateTime.Now.Millisecond % ss.Length];

			if (lang == Lang.Eng)
				Existed.AddRange(Letter.Eng);
			else if (lang == Lang.Rus)
				Existed.AddRange(Letter.Rus);

			int iStart = DateTime.Now.Millisecond % Line.Length;
			int iCnt = 0;
			char ch;
			for (int i = 0; i < 1000; i++)
			{
				ch = Line[(iStart + i * 3) % Line.Length];
				if (Existed.Contains(ch) == false)
				{
					Existed.Add(ch);
					iCnt++;
				}//if

				if (iCnt >= iPod)
					break;
			}//for

			BrushDone = Brushes.Bisque;

			NotifyPropertyChanged(propMask);
			return true;
		}//func

		public void Set(string s)
		{
			Existed.AddRange(s.ToCharArray());
			NotifyPropertyChanged(propMask);
			if (IsDone())
				BrushDone = Brushes.Red;
		}//func

		public bool IsDone()
		{
			return Line.AsEnumerable<char>().All(c => Existed.Contains(c));
		}//func

		public string MaskedLine()
		{
			if (string.IsNullOrWhiteSpace(Line))
			{
				return "No Value";
			}//if
			else
			{
				var qry = from c in Line.AsEnumerable<char>()
									select (Existed.Contains(c) ? c : emp);

				return new string(qry.ToArray());
			}//else
		}//func

		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged(string prop)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
			}//if
		}//func

		public void Save()
		{
			File.WriteAllLines(pathUsed, lstUsed.OrderBy(s=>s), Enc);
		}//func

		void Use(string s)
		{
			if (string.IsNullOrWhiteSpace(s))
				return;

			lstUsed.Add(s);
		}//func
	}//class
}//ns
