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
			string msg = "Usage: graph.exe  <filename1> <filename2> ..\n";
			Console.WriteLine(msg);
		}
		[STAThread]
		static int Main(string[] args)
		{
			int re = -1;
			List<string> fns = new List<string>();
			if (args.Length > 0)
			{
				foreach (string s in args)
				{
					string fn = s;
					string e = Path.GetExtension(fn).ToLower();
					if (e=="")
					{
						fn += ".cs";
					}
					if((e== ".cs") || (e == ".csx")|| (e == ".csinc"))
					{
						if (File.Exists(fn))
						{
							fns.Add(fn);
						}
					}
				}
			}
			if (fns.Count >0)
			{
				try
				{
					Scripts scripts = new Scripts();
					string result = scripts.ExecuteFile(fns.ToArray());
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
