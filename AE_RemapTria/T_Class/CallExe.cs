using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO.Pipes;
//using System.Text.Json.Nodes;
using System.IO;
using AE_RemapTria;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Security.Permissions;

namespace BRY
{


	public class CallExe
	{

		// ************************************************************************
		private string m_AppID = "AE_RemapTria";
		private string m_MyID = "AE_RemapTriaCall";
		/// <summary>
		/// 実行するアプリの名前
		/// </summary>
		public string AppID { get { return m_AppID; } }
		/// <summary>
		/// 自分自身のアプリの名前
		/// </summary>
		public string MyID { get { return m_MyID; } }

		private string m_ResultString = "false";
		/// <summary>
		/// 実行後に
		/// </summary>
		public string ResultString { get { return m_ResultString; } }

		
		// ************************************************************************
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="ap">呼び出すアプリ名</param>
		/// <param name="my">自分自身のアプリ名</param>
		public CallExe(string ap, string my)
		{
			m_AppID = ap;
			m_MyID = my;
		}
		static private async void SleepAsync()
		{
			await Task.Delay(750);
		}
		private string ArgsString(string[] args)
		{
			string opts = "";
			if (args.Length > 0)
			{
				for (int i = 0; i < args.Length; i++)
				{
					if (opts != "") opts += " ";
					opts += "\"" + args[i] + "\"";
				}
			}
			return opts;
		}
		static private string CallExePath(string nm)
		{
			string ret = "";
#pragma warning disable CS8600 // Null リテラルまたは Null の可能性がある値を Null 非許容型に変換しています。
			string fullName = Environment.ProcessPath;
			string n = "";
			if (fullName != null)
			{
				n = Path.GetDirectoryName(fullName);
#pragma warning restore CS8600 // Null リテラルまたは Null の可能性がある値を Null 非許容型に変換しています。

			}
			if ((n != null) && (n != ""))
			{
				ret = Path.Combine(n, nm + ".exe");
			}
			else
			{
				ret = nm + ".exe";
			}
			return ret;
		}
		static private EXEC_MODE GetOption(ref string[] args)
		{
			EXEC_MODE ret = EXEC_MODE.NONE;
			if (args.Length > 0)
			{
				for (int i = 0; i < args.Length; i++)
				{
					if ((args[i][0] == '-') || (args[i][0] == '/'))
					{
						string p = args[i].Substring(1).ToLower();
						switch (p)
						{
							case "exenow":
							case "isrun":
							case "isrunning":
							case "execnow":
								args[i] = "exenow";
								ret = EXEC_MODE.EXENOW;
								break;
							case "call":
							case "execute":
							case "start":
								args[i] = "call";
								ret = EXEC_MODE.CALL;
								break;
							case "export":
								ret = EXEC_MODE.EXPORT;
								break;
							case "import_layer":
								ret = EXEC_MODE.IMPORT_LAYER;
								break;
						}

					}
				}
			}
			return ret;
		}
		// ************************************************************************
		/// <summary>
		/// 実行部
		/// </summary>
		/// <param name="args">Mainのargs</param>
		/// <returns>実行コード</returns>
		public void Run(string[] args)
		{
			string rets = "false";
#if NET6_0
			Process? proc = null;
#else
			Process proc = null;
#endif
			bool isRunnig = false;
			Process[] ps = Process.GetProcessesByName(AppID);
			if (ps.Length > 0)
			{
				proc = ps[0];
				isRunnig = true;
			}

			//System.Threading.Mutex _mutex = new System.Threading.Mutex(false, m_AppID);

			EXEC_MODE em = GetOption(ref args);
			switch (em)
			{
				case EXEC_MODE.EXENOW:
					if (isRunnig)
					{
						rets = "true";
					}
					break;
				// アプリを実行する
				// 実行してるなら全面に
				case EXEC_MODE.CALL:
					if (isRunnig)
					{
						if (proc != null)
						{
							Wa.SetForegroundWindow(proc.MainWindowHandle);
							rets = "true";
						}
					}
					else
					{
						
						string p = CallExePath(AppID);
						Console.WriteLine(p);
						if (File.Exists(p))
						{
							if (Wa.ProcessStart(p))
							{
								rets = "true";
							}
						}
					}
					break;
				case EXEC_MODE.EXPORT:
					if (isRunnig)
					{
						Pipe pp = new Pipe();
						pp._execution = true;
						pp._Result = "";

						pp.MessagePipeServer(m_MyID);
						PipeData pd = new PipeData(args, PIPECALL.PipeExec);
						string s = pd.ToJson();
						pp.PipeClient(m_AppID, s).Wait();
						int cnt = 0;
						while (pp._execution)
						{
							//SleepAsync();
							Thread.Sleep(250);
							cnt++;
							if (cnt >= 50) { pp._execution = false; }
						}
						rets = pp._Result;
					}
					else
					{
						rets = "false";
					}
					break;
				case EXEC_MODE.IMPORT_LAYER:
					if (isRunnig)
					{
						Pipe pp = new Pipe();
						PipeData pd = new PipeData(args, PIPECALL.PipeExec);
						string s = pd.ToJson();
						pp.PipeClient(m_AppID, s).Wait();
						rets = "true";
					}
					else
					{
						rets = "false";
					}
					break;
				case EXEC_MODE.NONE:
				default:
					em = EXEC_MODE.NONE;
					break;
			}
			if (em == EXEC_MODE.NONE)
			{
				if (isRunnig)
				{
					PipeData pd2 = new PipeData(args, PIPECALL.PipeExec);
					string s2 = pd2.ToJson();
					Pipe pp2 = new Pipe();
					pp2._execution = true;
					pp2._Result = "";
					pp2.PipeClient(m_AppID, s2).Wait();
					rets = "true";
				}
				else
				{
					string exename = CallExePath(AppID);
					if (File.Exists(exename) == true)
					{
						if(Wa.ProcessStart(exename, ArgsString(args)))
						{
							rets = "true";
						}

					}
					else
					{
						rets = "false";
					}
				}
			}
			m_ResultString = rets;
		}

		// ******************************************************************************
		static public string OpenDialog(string p = "")
		{
			string ret = "";
#if NET6_0
			OpenFileDialog dlg = new OpenFileDialog();
			if(p!="")
			{
				dlg.InitialDirectory = T_Def.GetDir(p);
				dlg.FileName = T_Def.GetNameNoExt(p);
			}
			dlg.Filter = "*.ardj.jsx|*.ardj.jsx|*.jsx|*.jsx|*.*|*.*";
			if (dlg.ShowDialog()== DialogResult.OK)
			{
				ret = dlg.FileName;
			}
#endif
			return ret;
		}
		static public string SaveDialog(string p = "")
		{
			string ret = "";
#if NET6_0
			SaveFileDialog dlg = new SaveFileDialog();
			if (p != "")
			{
				dlg.InitialDirectory = T_Def.GetDir(p);
				dlg.FileName = T_Def.GetNameNoExt(p);
			}
			dlg.Filter = "*.ardj.jsx|*.ardj.jsx|*.jsx|*.jsx|*.*|*.*";
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				ret = dlg.FileName;
			}
#endif
			return ret;
		}
	}

}
