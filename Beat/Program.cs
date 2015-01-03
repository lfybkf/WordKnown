using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Beat
{
	class Program
	{
		static void Main(string[] args)
		{
			Encoding enc = Encoding.GetEncoding(1251);
			int Lim = 150, NumOfFiles = 0, Index = 0;
			string path = args[0];
			string fileFmt = Path.GetFileNameWithoutExtension(path) + "_{0:D3}.trn";
			string[] Content = File.ReadAllLines(path, enc);
			List<string> New = new List<string>();
			NumOfFiles = Content.Length / Lim;
			//recalc Lim
			Lim = Content.Length / NumOfFiles + 1;
			for (int i = 0; i < NumOfFiles; i++)
			{
				for (int j = 0; j < Lim; j++)
				{
					Index = j * NumOfFiles + i;
					if (Index >= Content.Length)
						continue;

					New.Add(Content[Index]);
				}//for
				

				//this file is full
				File.WriteAllLines(string.Format(fileFmt, i), New, enc);
				New.Clear();
			}//for
		}//function
	}//class
}//ns
