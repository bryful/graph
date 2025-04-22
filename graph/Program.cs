using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using graph;
using System.IO;

namespace graph_fw
{
	internal class Program
	{
		/*
		static void Main(string[] args)
		{
		}
		*/
		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		static extern int MessageBoxW(int hWnd, [MarshalAs(UnmanagedType.LPWStr)] string lpText, [MarshalAs(UnmanagedType.LPWStr)] string lpCaption, uint uType);

		public static void Usage()
		{
			string msg = "Usage: graph.exe  <filename>\n";
			Console.WriteLine(msg);
		}

		static int Main(string[] args)
		{
			int re = -1;
			string fn = "";
			if (args.Length > 0)
			{
				if (File.Exists(args[0]))
				{
					fn = args[0];
				}
			}
			if (fn != "")
			{
				try
				{
					Scripts scripts = new Scripts();
					string result = scripts.ExecuteFile(fn);
					Console.WriteLine(result);
					re = 0;
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					re = -1;
				}
			}
			else
			{
				Usage();
				re = -1;
			}
			return re;
		}
	}
}
