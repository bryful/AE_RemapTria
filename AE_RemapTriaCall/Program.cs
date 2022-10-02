using System;

using BRY;

namespace AE_RemapTria // Note: actual namespace depends on the project name.
{
	internal class Program
	{
		public static string CallExeName = "AE_RemapTria";
		public static string MyExeName = "AE_RemapTriaCall";
		// ************************************************************************
		static int Main(string[] args)
		{
			CallExe ce = new CallExe(CallExeName,MyExeName);
			ce.Run(args);

			Console.WriteLine(ce.ResultString);
			return ce.Result;
		}
	}
	// ************************************************************************
}
