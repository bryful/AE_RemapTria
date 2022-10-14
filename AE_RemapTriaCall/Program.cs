using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BRY;

namespace AE_RemapTria
{
	internal static class Program
	{
		public static string CallExeName = "AE_RemapTria";
		public static string MyExeName = "AE_RemapTriaCall";
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			CallExe ce = new CallExe(CallExeName, MyExeName);
			ce.Run(args);
			Console.WriteLine(ce.ResultString);
		}
	}
}
