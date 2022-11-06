using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BRY
{
	public class F_W
	{
		// *************************************************************************
		/// <summary>
		/// アプリケーションを全面に
		/// </summary>
		/// <param name="hWnd"></param>
		/// <returns></returns>
		// *************************************************************************

		#region Process
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
			sInfoEx.StartupInfo = new STARTUPINFO();

			var pSec = new SECURITY_ATTRIBUTES();
			var tSec = new SECURITY_ATTRIBUTES();
			pSec.nLength = Marshal.SizeOf(pSec);
			tSec.nLength = Marshal.SizeOf(tSec);

			bool bResult = CreateProcess(
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
			sInfoEx.StartupInfo = new STARTUPINFO();

			var pSec = new SECURITY_ATTRIBUTES();
			var tSec = new SECURITY_ATTRIBUTES();
			pSec.nLength = Marshal.SizeOf(pSec);
			tSec.nLength = Marshal.SizeOf(tSec);

			bool bResult = CreateProcess(
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
		#endregion

		#region DllImport
		// トップレベルウィンドウを列挙する
		[DllImport("user32.dll")]
		private static extern bool EnumWindows(EnumWindowsDelegate lpEnumFunc, IntPtr lParam);

		// ウィンドウの表示状態を調べる(WS_VISIBLEスタイルを持つかを調べる)
		[DllImport("user32.dll")]
		private static extern bool IsWindowVisible(IntPtr hWnd);

		//ウィンドウのタイトルの長さを取得する
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int GetWindowTextLength(IntPtr hWnd);

		// ウィンドウのタイトルバーのテキストを取得
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

		// ウィンドウを作成したプロセスIDを取得
		//[DllImport("user32")]// 「.dll」なくても動いてた
		[DllImport("user32.dll")]
		private static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

		// EnumWindowsから呼び出されるコールバック関数のデリゲート
		private delegate bool EnumWindowsDelegate(IntPtr hWnd, IntPtr lParam);

		[DllImport("user32.dll", SetLastError = true)]
		static extern int GetWindowLong(IntPtr hWnd, int nIndex);
		[DllImport("user32.dll", SetLastError = true)]
		static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool SetForegroundWindow(IntPtr hWnd);
		#endregion

		// **********************************************************************************************************
		static public Process[] GetAEProcess()
		{
			Process[] ret = new Process[0];
			List<Process> lst = new List<Process>();
			foreach (Process p in Process.GetProcesses())
			{
				if (p.ProcessName == "AfterFX")
				{
					lst.Add(p);
				}
			}
			if (lst.Count > 0)
			{
				ret = lst.ToArray();
			}
			return ret;
		}
		static private bool WaitForInputIdle(Process Proc)
		{
			bool ret = false;
			if (Proc != null)
			{
				ret = Proc.WaitForInputIdle();
			}
			return ret;
		}
		static public bool SetForegroundWindow(Process Proc)
		{
			bool ret = false;
			if (Proc != null)
			{
				ret = SetForegroundWindow(Proc.MainWindowHandle);
			}
			return ret;

		}
		static public bool SetWindow(Process Proc, int p)
		{
			bool ret = false;
			if (Proc != null)
			{
				if (Proc.WaitForInputIdle())
				{
					ret = ShowWindow(Proc.MainWindowHandle, p);
				}
			}
			return ret;

		}

		static public void SetAEWindowAll(int p)
		{
			Process[] lst = GetAEProcess();
			if (lst.Length > 0)
			{
				for (int i = lst.Length - 1; i >= 0; i--)
				{
					SetWindow(lst[i], p);
					SetForegroundWindow(lst[i]);
				}
			}
		}
		static public void AEWindowMax()
		{
			SetAEWindowAll(3);
		}
		static public void AEWindowMin()
		{
			SetAEWindowAll(2);
		}
		static public void AEWindowNormal()
		{
			SetAEWindowAll(1);
		}
		static private string ProcInfo(Process ps)
		{
			string ret = "";
			ret += String.Format("id:{0}", ps.Id);
			ret += String.Format(",mainWindowTitle :\"{0}\"", ps.MainWindowTitle);
			ret += String.Format(",processName  :\"{0}\"", ps.ProcessName);
			ret += String.Format(",fileName  :\"{0}\"", ps.MainModule.FileName);

			ret = "({" + ret + "})";
			return ret;

		}
		static public string AEProcessList()
		{
			string ret = "";
			Process[] lst = GetAEProcess();

			if (lst.Length > 0)
			{
				for (int i = 0; i < lst.Length; i++)
				{
					if (ret != "") ret += ",";
					ret += ProcInfo(lst[i]);

				}
			}
			ret = "[" + ret + "]";
			return ret;
		}
		// ***************************************************************************************************
		static string GetOsType()
		{
			const string Path = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion";
			var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(Path, false);
			return (string)key.GetValue("ProductName", "", Microsoft.Win32.RegistryValueOptions.DoNotExpandEnvironmentNames);
		}
		static public string PCInfo()
		{
			string ret = "";
			ret += "({";
			ret += "OSName:\"" + GetOsType() + "\"";
			ret += ",";
			ret += "OSVersion:\"" + Environment.OSVersion.ToString() + "\"";
			ret += ",";
			ret += "PCName:\"" + Environment.MachineName + "\"";
			ret += ",";
			ret += "UserName:\"" + Environment.UserName + "\"";
			ret += "})";
			return ret;
		}
		 // *****************************************************************************
		[System.Runtime.InteropServices.DllImport("Kernel32.dll")]
		static extern bool AttachConsole(int processId);
		const int ATTACH_PARENT_PROCESS = -1;

		[System.Runtime.InteropServices.DllImport("Kernel32.dll")]
		static extern bool AllocConsole();

		[System.Runtime.InteropServices.DllImport("Kernel32.dll")]
		static extern bool FreeConsole();
		static public void SettupConsole()
		{
			var attachConsole = AttachConsole(ATTACH_PARENT_PROCESS);
			if (attachConsole == false)
			{
				if (AllocConsole() == false)
				{
					return;
				}

				// 標準出力のストリームを取得
				var stream = Console.OpenStandardOutput();
				var stdout = new StreamWriter(stream)
				{
					// Write メソッド実行時に即出力するためには AutoFlush プロパティを True にする必要があるらしい
					AutoFlush = true,
				};
				// 出力先を標準出力に設定
				Console.SetOut(stdout);
			}



		}
		static public void EndConsole()
		{
			var attachConsole = AttachConsole(ATTACH_PARENT_PROCESS);
			if (attachConsole == false)
			{
				FreeConsole();
			}

		}
	}
}
