using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BDB;
using Clipboard = System.Windows.Forms.Clipboard;
using MessageBox = System.Windows.Forms.MessageBox;

namespace WordKnown
{


	class Glossary
	{
		#region static
		static OrderedStrings oKnown = new OrderedStrings(Path.Combine(Environment.CurrentDirectory, "Known.ini"));
		static OrderedStrings oCrap = new OrderedStrings(Path.Combine(Environment.CurrentDirectory, "Crap.ini"));
		static OrderedStrings oTrn = new OrderedStrings(Path.Combine(Environment.CurrentDirectory, "Trn.ini"));
		static char[] ccDelim =	"\\\" ,.!?<>-=:;/|[]{}()~@#$%^&*_—".ToCharArray();
		static char[] ccForbid = "+0123456789\'".ToCharArray();
		static char[] ccEng = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
		static int MinLength = 3;
		#endregion

		string fileText;
		List<WordTranslate> lstUnk = new List<WordTranslate>();
		public Func<IEnumerable<WordTranslate>> Selected;

		internal System.Windows.Forms.ListBox ctl;

		public static bool IsEng(string s) { return s.All(ch => ccEng.Contains(ch)); }//func

		public Glossary()
		{
			oKnown.Load();
			oCrap.Load();
			oTrn.Load();
		}//ctor

		public void LoadUnknownFromTrn(string path)
		{
			string[] ss = File.ReadAllLines(path, WordTranslate.encoding);
			WordTranslate item;
			foreach (string s in ss)
			{
				item = new WordTranslate();
				if (item.Parse(s))
				{
					lstUnk.Add(item);
				}//if
			}//for

			Refresh();
		}//func

		public void LoadText(string path)
		{
			fileText = path;
			List<string> Content = new List<string>();
			string[] SingleContent;
			//работаем с целым каталогом файлов
			if (Directory.Exists(path))
			{
				foreach (var path1 in Directory.EnumerateFiles(path))
				{
					if (!File.Exists(path1))
						continue;

					SingleContent = File.ReadAllLines(path1);
					Content.AddRange(SingleContent);
				}//for
			}//if
			else //работаем с одним файлом
			{
				if (path.EndsWith(".trn")) // возобновляем работу с прошлым файлом
				{
					LoadUnknownFromTrn(path);
					return;
				}//if

				//базовый сценарий
				SingleContent = File.ReadAllLines(path);
				Content.AddRange(SingleContent);
			}//else

			LoadContent(Content);
		}//func

		private void LoadContent(IEnumerable<string> Content)
		{
			List<string> words = new List<string>();
			foreach (string line in Content)
			{
				Add(words, line.Split(ccDelim));
			}//for

			bool bKnown = true;
			bool bCrap = true;
			foreach (string word in words.OrderBy(s=>s))
			{
				bKnown = oKnown.Move(word);
				bCrap = oCrap.Move(word);
				if (!bKnown && !bCrap)
				{
					lstUnk.Add(new WordTranslate {Word = word, Translate = string.Empty});
				}//if
			}//for

			//заполняем перевод из старых трн
			IEnumerable<WordTranslate> wtsTrn = oTrn.sequenceAll
				.Select(s => { var wt = new WordTranslate(); return wt.Parse(s) ? wt : null; })
				.Where(wt => wt != null)
				.ToArray();

			WordTranslate wtTrn;
			foreach (WordTranslate wt in lstUnk)
			{
				wtTrn = wtsTrn.FirstOrDefault(o => o.Word == wt.Word);
				if (wtTrn != null) { wt.Translate = wtTrn.Translate; }
			}//for

			Refresh();
		}//func

		public void Copy()
		{
			string dlm= "#";
			string sClip = string.Join(dlm, Selected().Select(wt=>wt.Word).ToArray());
			sClip = sClip.Replace(dlm, Environment.NewLine);
			try
			{
				Clipboard.SetText(sClip);
			}
			catch (Exception)
			{

				MessageBox.Show("Clipboard.SetText(sClip)", "Error");
			}//catch
			
		}//func

		public void Paste()
		{
			string dlm= "#";
			string sClip = Clipboard.GetText();
			sClip = sClip.Replace(Environment.NewLine, dlm);
			string[] ss = sClip.Split(dlm.ToCharArray());

            // если запрос на перевод и количество переведенного не совпадают
			int iW = Selected().Count(), iT = ss.Count();
			if (iW != iT)
			{
				MessageBox.Show(string.Format("Paste Error. Words={0}. Translate={1}", iW, iT));
				return;
			}//if

            //установить перевод
			for (int i = 0; i < iW; i++)
			{
				Selected().ElementAt(i).Translate = ss[i];
			}//for

            //убрать слова, где перевод плохой
            for (int i = 0; i < iW; i++)
            {
                WordTranslate wt = Selected().ElementAt(i);
                if (wt.BadTranslate)
                    lstUnk.Remove(wt);
            }//for

			Refresh();
		}//func

		static void Add(List<string> lst, string[] words)
		{
			string[] ssUp = 
				words
				.Where(s => s.Length >= MinLength)
				.Select(s => s.ToUpper())
				.Distinct()
				.ToArray();

			var qry =
				from s in ssUp
				where lst.Contains(s) == false && s.All(ch => ccEng.Contains(ch))
				select s;

			lst.AddRange(qry);
		}//func

		public void Save()
		{
			oKnown.Save();
			oCrap.Save();
			var qry = from wt in lstUnk
								where wt.Translate != string.Empty
								orderby wt.Word
								select wt.ToString();

			File.WriteAllLines(
				Path.ChangeExtension(fileText, "trn")
				, qry.ToArray()
				, WordTranslate.encoding
				);
		}//func

		public void Refresh()
		{
			int i = ctl.SelectedIndex;
			i = i < 0 ? 0 : i;

			ctl.Items.Clear();
			ctl.Items.AddRange(lstUnk.ToArray());

			if (i < lstUnk.Count)
				ctl.SelectedIndex = i;
			else
				ctl.SelectedIndex = 0;
		}//func

		public void SetKnown(IEnumerable<WordTranslate> wtS)
		{
				//есть ли новые известные
			if (wtS.Any() == false) { return; }
            
			//пополнить список известных
			oKnown.AddRange(wtS.Select(wt => wt.Word));

			//удалить из списка неизвестных
			foreach (var wt in wtS)
			{
				lstUnk.Remove(wt);
			}//for
		}//func

		public void SetCrap(IEnumerable<WordTranslate> wtS)
		{
			//есть ли новые известные
			if (wtS.Any() == false)
				return;

			//пополнить список известных
			oCrap.AddRange(wtS.Select(wt => wt.Word));

			//удалить из списка неизвестных
			foreach (var wt in wtS)
			{
				lstUnk.Remove(wt);
			}//for
		}//func


	}//class
}//ns
