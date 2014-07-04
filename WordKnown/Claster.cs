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
        string Path;
        int Position = 0;

        public OrderedStrings(string Path)
        {
            this.Path = Path;
        }//func

        public void Load()
        {
            Content.AddRange(
				File.ReadAllLines(Path)
				.OrderBy(s => s)
                .Select(s => s.ToUpper())
				.ToArray());
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
			File.WriteAllLines(Path, ss);
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
