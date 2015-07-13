using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WordKnown
{
	/// <summary>
	/// Класс, содержащий упорядоченный набор строк
	/// </summary>
	class OrderedStrings
	{
		List<string> Content = new List<string>();
		string path;
		int Position = 0;

		public OrderedStrings(string path)  { this.path = path; }//func

		public IEnumerable<string> sequenceAll
		{	
			get 
			{
				for (int i = 0; i < Content.Count; i++)
				{
					yield return  Content.ElementAt(i);
				}//for
			}//get
		}//prop

        public void Load()
        {
					if (File.Exists(path))
					{
						var lines = File.ReadAllLines(path, WordTranslate.encoding).OrderBy(s => s).Select(s => s.ToUpper()).ToArray();
						Content.AddRange(lines);
					}//if
        }//func

        public void AddRange(IEnumerable<string> ss)
        {
            Content.AddRange(ss);
        }//func

        public void Save()
        {
					string[] ss = Content
                .Select(s => s.ToUpper())
								.OrderBy(s => s)
								.ToArray();
					File.WriteAllLines(path, ss);
        }//func

        public bool Move(string s)
        {
            bool Ret = false;
            int cmp;
            for (int i = Position; i < Content.Count; i++)
            {
                cmp = string.Compare(s, Content[i]);
                if (cmp > 0)
                    continue;

                if (cmp == 0)
                    Ret = true;

                Position = i;
                break;
            }//func
            return Ret;
        }//func
    }//class
}//ns
