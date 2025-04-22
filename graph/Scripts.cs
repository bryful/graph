using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Reflection;

using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.CSharp.Scripting.Hosting;
using Microsoft.CodeAnalysis.Scripting;
using System.IO;
using PdfSharp;
using PdfSharp.Drawing;


namespace graph
{
	public class Scripts
	{
		public Scripts() 
		{
		}
		// *********************************************************
		public string ExecuteFile(string code)
		{
			string result = "error!";
			if (string.IsNullOrEmpty(code))
			{
				return "empty code!";
			}
			try
			{
				string script = File.ReadAllText(code);
				result = ExecuteCode(script);
			}
			catch (Exception ex)
			{
				result = "Error2!\n" + ex.Message;
			}
			return result;
		}


		public string ExecuteCode(string code)
		{
			Root root= new Root();

			string result = "error!";
			if(string.IsNullOrEmpty(code))
			{
				return "empty code";
			}
			List<Assembly> assembly = new List<Assembly>()
			{
				Assembly.Load("System"),
				Assembly.Load("System.IO"),
				Assembly.Load("System.Drawing"),

				Assembly.GetAssembly(typeof(System.Dynamic.DynamicObject)),  // System.Code
				Assembly.GetAssembly(typeof(Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo)),  // Microsoft.CSharp
				Assembly.GetAssembly(typeof(System.Dynamic.ExpandoObject)),
				Assembly.GetAssembly(typeof(System.Data.DataTable)),
				Assembly.GetAssembly(typeof(System.Object)),
				Assembly.GetAssembly(typeof(PDF)),
				Assembly.GetAssembly(typeof(Root)),
				Assembly.GetAssembly(typeof(DXF)),
				Assembly.Load("PDFSharp"),
				Assembly.Load("netDxf"),
				Assembly.GetExecutingAssembly()


			};

			List<string> import = new List<string>()
			{
				"System",
				"System.Dynamic",
				"System.Linq", 
				"System.Text",
				"System.IO",
				"System.Collections.Generic", 
				"System.Data",
				"System.Drawing",
				"PdfSharp",
				"PdfSharp.Drawing",
				"netDxf",
				"graph.DXF",
				"graph.PDF",
				"graph",

			};

			var opt = ScriptOptions.Default.AddReferences(assembly).AddImports(import);

			try
			{
				var script = CSharpScript.Create(code, opt,typeof(Root));
				script.RunAsync(root);
				result = "";
			}
			catch (CompilationErrorException ex)
			{
				result = "compile Error!\n" + ex.Message;
			}
			catch (Exception ex)
			{
				result = "Error1!\n" + ex.Message;
			}

			return result;
		}
	}
}
