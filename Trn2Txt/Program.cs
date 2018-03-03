using System;
using System.Collections.Generic;
using io = System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trn2Txt
{
	class Program
	{
		static void Main(string[] args)
		{
			List<Texter> texters = Texter.jobs(args);
			foreach (var texter in texters)	{
				if (texter.check())	{
					texter.run();
				}//if
			}//for
		}//function
	}//class
}
