// See https://aka.ms/new-console-template for more information
using System.Runtime.InteropServices;
using System.Text.Json.Nodes;
using graph;

internal partial class Program
{
	[DllImport("user32.dll", CharSet = CharSet.Unicode)]
	static extern int MessageBoxW(nint hWnd, [MarshalAs(UnmanagedType.LPWStr)] string lpText, [MarshalAs(UnmanagedType.LPWStr)] string lpCaption, uint uType);

	public static void Usage()
	{
		string msg = "Usage: graph.exe  <filename>\n";
		Console.WriteLine(msg);
	}

	private static int Main(string[] args)
	{
		int re = -1;
		string fn = "";
		if(args.Length > 0)
		{
			if (File.Exists(args[0]))
			{
				fn = args[0];
			}
		}
		if(fn != "")
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