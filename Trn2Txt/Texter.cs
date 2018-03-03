using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using io = System.IO;

namespace Trn2Txt
{
	class Texter
	{
		public string srcFile;
		public string trnFile;

		///<summary>trn</summary> 
		static string extTrn = "trn";
		static string D = " - ";
		static string mark = "_trn";
		static Encoding encWin = Encoding.GetEncoding(1251);
		static string[] ends = { " ", ".", "," };

		internal static List<Texter> jobs(string[] args)
		{
			List<Texter> result = new List<Texter>();
			IEnumerable<string> files;
			string srcFile;
			string trnFile;

			if (args.Any())
			{
				if (args.Length == 1) {
					string baseName = io.Path.GetFileNameWithoutExtension(args[0]);
					files = io.Directory.GetFiles(Environment.CurrentDirectory, $"{baseName}.*");
				}	else {
					files = args;
				}//else
				trnFile = files.FirstOrDefault(z => z.EndsWith(extTrn));
				srcFile = files.FirstOrDefault(z => !z.EndsWith(extTrn));
				result.Add(new Texter { srcFile = srcFile, trnFile = trnFile });
			}	else	{
				files = io.Directory.GetFiles(Environment.CurrentDirectory, "*.trn");
				if (files.Count() != 1) {
					Console.WriteLine("Need only one TRN file");
				}	else {
					trnFile = files.First();
					files = io.Directory.GetFiles(Environment.CurrentDirectory, "*.*")
						.Where(z => !z.EndsWith(extTrn) && !z.EndsWith("exe"));
					foreach (var file in files) {
						result.Add(new Texter { srcFile = file, trnFile = trnFile });
					}//for
				}//else
			}//else
			return result;
		}

		internal void run()
		{
			Console.WriteLine("");
			if (srcFile.Contains(mark))
			{
				Console.WriteLine($"marked {srcFile}");
				return;
			}//if
			
			Console.WriteLine($"{trnFile} = {srcFile}");
			Dictionary<string, string> trn = loadTrn(trnFile);

			string sAll = io.File.ReadAllText(srcFile, encWin);
			string sRus, sEng;
			int Dlen = D.Length;
			int Count = 0;
			foreach (var key in trn.Keys) {
				foreach (var end in ends)	{
					sEng = $" {key}{end}";//чтобы заменять не "word", а " word ". Иначе будут проблемы с "wordish"
					if (sAll.Contains(sEng)) {
						sRus = $" {key} ({trn[key]}){end}";
						sAll = sAll.Replace(sEng, sRus);
					}//if
				}//for

				Count++;
				if (Count % 50 == 0)
					Console.Write($" {Count}");
			}//for

			string outFile = io.Path.GetFileNameWithoutExtension(srcFile) + mark + io.Path.GetExtension(srcFile);
			io.File.WriteAllText(outFile, sAll, Encoding.UTF8);
			Console.WriteLine("");
		}

		private static Dictionary<string, Dictionary<string, string>> cache = new Dictionary<string, Dictionary<string, string>>();
		private static Dictionary<string, string> loadTrn(string trnFile)
		{
			if (cache.ContainsKey(trnFile)) {
				Console.WriteLine($"cached TRN={trnFile}");
				return cache[trnFile];
			}//if

			Dictionary<string, string> result = new Dictionary<string, string>();
			string line;
			string sRus, sEng;
			int Didx = 0, Dlen = D.Length;

			using (var sr = new io.StreamReader(trnFile, encWin))
			{
				while ((line = sr.ReadLine()) != null)
				{
					Didx = line.IndexOf(D);
					sRus = line.Substring(Didx + Dlen).ToLower();
					if (sRus.Length <= 15) { //длинные переводы не влезут в строку субтитров
						sEng = line.Substring(0, Didx).ToLower();
						result[sEng] = sRus;
					}//if
				}//while
			}//using
			Console.WriteLine($"Starting with TrnCount={result.Count}");
			cache[trnFile] = result;
			return result;
		}

		internal bool check()
		{
			if (srcFile == null) {
				Console.WriteLine($"No TXT file");
				return false;
			}//if
			if (trnFile == null) {
				Console.WriteLine($"No TRN file");
				return false;
			}//if
			return true;
		}
	}
}
