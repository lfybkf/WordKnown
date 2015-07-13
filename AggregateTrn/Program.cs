using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDB
{
	class Program
	{
		static void Main(string[] args)
		{
			string path = Path.Combine(Environment.CurrentDirectory, "trn");
			var trnFiles = Directory.EnumerateFiles(path, "*.trn");
			string resultFile = "trn.ini";
			Dictionary<string, string> resultContent = new Dictionary<string, string>(200);
			Encoding encoding = Encoding.GetEncoding(1251);
			string D = " - ";
			string sRus, sEng;
			string[] input;

			foreach (var file in trnFiles)
			{
				Console.WriteLine("inputFile: " + Path.GetFileNameWithoutExtension(file));
				input = File.ReadAllLines(file, encoding);
				foreach (var s in input)
				{
					sEng = s.before(D);
					sRus = s.after(D);
					if (resultContent.ContainsKey(sEng)==false)
					{
						resultContent[sEng] = sRus;
					}//if
				}//for
			}//for
			var resultLines = resultContent.Keys.Select(key => key.add(D, resultContent[key])).OrderBy(s => s).ToArray();
			Console.WriteLine("words count: " + resultLines.Length);
			Console.WriteLine("resultFile: " + resultFile);
			File.WriteAllLines(resultFile, resultLines, encoding);
			Console.ReadLine();

		}//function
	}
}
