using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDB;

namespace WordKnown
{
	class WordTranslate
	{
		public const string D = " - ";
		static string[] dlm = new string[] {D};
		public static readonly Encoding encoding = Encoding.GetEncoding(1251);

		public string Word { get; set; }
		public string Translate { get; set; }
		public bool Selected = false;

		public WordTranslate()
		{
			Translate = string.Empty;
		}//ctor

		public override string ToString()	{	return HasTranslate ? "{0} - {1}".fmt(Word, Translate) : Word; 	}
		public bool BadTranslate { get { return (Word == Translate); } }
		public bool HasTranslate { get { return (Translate != string.Empty); } }

		public bool Parse(string input)
		{
			string[] ss = input.Split(dlm, StringSplitOptions.RemoveEmptyEntries);
			if (ss.Length != 2)
				return false;

			Word = ss[0];
			Translate = ss[1];
			return true;
		}//func

	}//class
}
