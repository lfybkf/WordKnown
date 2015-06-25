using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trn2Txt
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length != 2)
			{
				Console.WriteLine("Usage: exe txt trn");
				foreach (var item in args)
				{
					Console.WriteLine(item);
				}//for
				return;
			}//if

			string txtFile = args[0];
			string trnFile = args[1];
			Encoding encoding = Encoding.GetEncoding(1251);

			string line;
			string D = " - ";
			string sRus, sEng;
			string sAll = File.ReadAllText(txtFile, encoding);
			int Didx = 0, Dlen = D.Length;
			Dictionary<string, string> Trn = new Dictionary<string, string>();
			using (StreamReader sr = new StreamReader(trnFile, encoding))
			{
				while ((line = sr.ReadLine()) != null)
				{
					Didx = line.IndexOf(D);
					sRus = line.Substring(Didx + Dlen).ToLower();
					if (sRus.Length <= 15) //длинные переводы не влезут в строку субтитров
					{
						sEng = line.Substring(0, Didx).ToLower();
						Trn.Add(sEng, sRus);
					}//if
				}//while
			}//using

			Console.WriteLine("Starting with TrnCount=" + Trn.Count);

			string withTrn = " {0}({1})";
			string[] ends = { " ", ".", "," };
			int Count = 0;
			foreach (var key in Trn.Keys)
			{
				foreach (var end in ends)
				{
					sEng = string.Format(" {0}{1}", key, end);//чтобы заменять не "word", а " word ". Иначе будут проблемы с "wordish"
					if (sAll.Contains(sEng))
					{
						sRus = string.Format(withTrn, key, Trn[key]) + end;
						sAll = sAll.Replace(sEng, sRus);
					}//if
				}//for
				
				Count++;
				if (Count % 50 == 0)
					Console.Write(" " + Count);
			}//for

			string FileName = Path.GetFileNameWithoutExtension(txtFile) + "_trn" + Path.GetExtension(txtFile);
			File.WriteAllText(FileName , sAll, encoding);

			Console.WriteLine("ALL DONE");
		}//function
	}//class
}
