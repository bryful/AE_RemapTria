using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace BRY
{
	public class Wa
	{
		// *************************************************************************
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool SetForegroundWindow(IntPtr hWnd);
		// *************************************************************************
		[StructLayout(LayoutKind.Sequential)]
		public struct PROCESS_INFORMATION
		{
			public IntPtr hProcess;
			public IntPtr hThread;
			public int dwProcessId;
			public int dwThreadId;
		}
		// *************************************************************************
		[StructLayout(LayoutKind.Sequential)]
		public struct SECURITY_ATTRIBUTES
		{
			public int nLength;
			public IntPtr lpSecurityDescriptor;
			public int bInheritHandle;
		}
		// *************************************************************************
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct STARTUPINFOEX
		{
			public STARTUPINFO StartupInfo;
			public IntPtr lpAttributeList;
		}
		// *************************************************************************
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct STARTUPINFO
		{
			public Int32 cb;
			public string lpReserved;
			public string lpDesktop;
			public string lpTitle;
			public Int32 dwX;
			public Int32 dwY;
			public Int32 dwXSize;
			public Int32 dwYSize;
			public Int32 dwXCountChars;
			public Int32 dwYCountChars;
			public Int32 dwFillAttribute;
			public Int32 dwFlags;
			public Int16 wShowWindow;
			public Int16 cbReserved2;
			public IntPtr lpReserved2;
			public IntPtr hStdInput;
			public IntPtr hStdOutput;
			public IntPtr hStdError;
		}

		// *************************************************************************
		[DllImport("kernel32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CreateProcess(
			string lpApplicationName,
			string lpCommandLine,
			ref SECURITY_ATTRIBUTES lpProcessAttributes,
			ref SECURITY_ATTRIBUTES lpThreadAttributes,
			bool bInheritHandles, uint dwCreationFlags,
			IntPtr lpEnvironment,
			string? lpCurrentDirectory,
			[In] ref STARTUPINFOEX lpStartupInfo,
			out PROCESS_INFORMATION lpProcessInformation);
		// ************************************************************************
		[DllImport("kernel32.dll", SetLastError = true)]
		static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);

		[DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool CloseHandle(IntPtr handle);
		// ************************************************************************
		static public bool ProcessStart(string cmd, string args = "")
		{
			var pInfo = new PROCESS_INFORMATION();
			var sInfoEx = new STARTUPINFOEX();
			sInfoEx.StartupInfo = new Wa.STARTUPINFO();

			var pSec = new SECURITY_ATTRIBUTES();
			var tSec = new SECURITY_ATTRIBUTES();
			pSec.nLength = Marshal.SizeOf(pSec);
			tSec.nLength = Marshal.SizeOf(tSec);

			bool bResult = Wa.CreateProcess(
			  cmd
			, args
			, ref pSec
			, ref tSec
			, false
			, 0
			, IntPtr.Zero
			, null
			, ref sInfoEx
			, out pInfo
			);
			return bResult;
		}
		// ************************************************************************
		static public bool ProcessStartWait(string cmd, string args = "")
		{

			var pInfo = new PROCESS_INFORMATION();
			var sInfoEx = new STARTUPINFOEX();
			sInfoEx.StartupInfo = new Wa.STARTUPINFO();

			var pSec = new SECURITY_ATTRIBUTES();
			var tSec = new SECURITY_ATTRIBUTES();
			pSec.nLength = Marshal.SizeOf(pSec);
			tSec.nLength = Marshal.SizeOf(tSec);

			bool bResult = Wa.CreateProcess(
			  cmd
			, args
			, ref pSec
			, ref tSec
			, false
			, 0
			, IntPtr.Zero
			, null
			, ref sInfoEx
			, out pInfo
			);

			if (!CloseHandle(pInfo.hThread))
			{
				return false;
			}
			uint r = WaitForSingleObject(pInfo.hProcess, 0xFFFFFFFF);

			return bResult;
		}
	}
}
