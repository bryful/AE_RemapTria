using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BRY
{
	public class F_W
	{
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		struct SHFILEINFO
		{
			public IntPtr hIcon;
			public IntPtr iIcon;
			public uint dwAttributes;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string szDisplayName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
			public string szTypeName;
		}
		// *************************************************************************
		/// <summary>
		/// アプリケーションを全面に
		/// </summary>
		/// <param name="hWnd"></param>
		/// <returns></returns>
		// *************************************************************************
		const uint SHGFI_LARGEICON = 0x00000000;
		const uint SHGFI_SMALLICON = 0x00000001;
		const uint SHGFI_USEFILEATTRIBUTES = 0x00000010;
		const uint SHGFI_ICON = 0x00000100;
		[DllImport("shell32.dll", CharSet = CharSet.Auto)]
		static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbFileInfo, uint uFlags);
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool DestroyIcon(IntPtr hIcon);

		/// <summary>
		/// 指定したファイルパスに関連付けされたアイコンイメージを取得する。
		/// </summary>
		/// <remarks>
		/// このメソッドは、ファイルの存在チェックを行ない、指定されなかった第３パラメータの
		/// 値を決定する。
		/// </remarks>
		/// <param name="path">アイコンイメージ取得対象のファイルのパス</param>
		/// <param name="isLarge">大きいアイコンを取得するとき true、小さいアイコンを取得するとき false</param>
		/// <returns>取得されたアイコンのビットマップイメージを返す。</returns>
		public static Image FileAssociatedImage(string path, bool isLarge)
		{
			return FileAssociatedImage(path, isLarge, File.Exists(path));
		}
		/// <summary>
		/// 指定したファイルパスに関連付けされたアイコンイメージを取得する。
		/// </summary>
		/// <param name="path">アイコンイメージ取得対象のファイルのパス</param>
		/// <param name="isLarge">
		/// 大きいアイコンを取得するとき true、小さいアイコンを取得するとき false
		/// </param>
		/// <param name="isExist">
		/// ファイルが実在するときだけ動作させるとき true、実在しなくて動作させるとき false
		/// </param>
		/// <returns>取得されたアイコンのビットマップイメージを返す。</returns>
		public static Image FileAssociatedImage(string path, bool isLarge, bool isExist)
		{
			SHFILEINFO fileInfo = new SHFILEINFO();
			uint flags = SHGFI_ICON;
			if (!isLarge) flags |= SHGFI_SMALLICON;
			if (!isExist) flags |= SHGFI_USEFILEATTRIBUTES;
			try
			{
				SHGetFileInfo(path, 0x10, ref fileInfo, (uint)Marshal.SizeOf(fileInfo), flags);
				if (fileInfo.hIcon == IntPtr.Zero)
					return null;
				else
					return Icon.FromHandle(fileInfo.hIcon).ToBitmap();
			}
			finally
			{
				if (fileInfo.hIcon != IntPtr.Zero)
					DestroyIcon(fileInfo.hIcon);
			}
		}
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
		static public DateTime GetBuildDateTime(string asmPath)
		{
			// ファイルオープン
			using (FileStream fs = new FileStream(asmPath, FileMode.Open, FileAccess.Read))
			using (BinaryReader br = new BinaryReader(fs))
			{
				// まずはシグネチャを探す
				byte[] signature = { 0x50, 0x45, 0x00, 0x00 };// "PE\0\0"
				List<byte> bytes = new List<byte>();
				while (true)
				{
					bytes.Add(br.ReadByte());

					if (bytes.Count < signature.Length)
					{
						continue;
					}

					while (signature.Length < bytes.Count)
					{
						bytes.RemoveAt(0);
					}

					bool isMatch = true;
					for (int i = 0; i < signature.Length; i++)
					{
						if (signature[i] != bytes[i])
						{
							isMatch = false;
							break;
						}
					}
					if (isMatch)
					{
						break;
					}
				}

				// COFFファイルヘッダを読み取る
				var coff = new
				{
					Machine = br.ReadBytes(2),
					NumberOfSections = br.ReadBytes(2),
					TimeDateStamp = br.ReadBytes(4),
					PointerToSymbolTable = br.ReadBytes(4),
					NumberOfSymbols = br.ReadBytes(4),
					SizeOfOptionalHeader = br.ReadBytes(2),
					Characteristics = br.ReadBytes(2),
				};

				// タイムスタンプをDateTimeに変換
				int timestamp = BitConverter.ToInt32(coff.TimeDateStamp, 0);
				DateTime baseDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
				DateTime buildDateTimeUtc = baseDateTime.AddSeconds(timestamp);
				DateTime buildDateTimeLocal = buildDateTimeUtc.ToUniversalTime();
				return buildDateTimeLocal;
			}
		}
	}
}
