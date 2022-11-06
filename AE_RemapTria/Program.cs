using BRY;

namespace AE_RemapTria
{

	internal static class Program
	{
		public const string MyAppId = "SkeltonWinForm"; // GUIDなどユニークなもの
		public const string MyCallBackId = "SkeltonWinFormCallBack"; // GUIDなどユニークなもの
		// *******************************************************************************************
		private static System.Threading.Mutex _mutex = new System.Threading.Mutex(false, MyAppId);
		// *******************************************************************************************
		[STAThread]
		static void Main(string[] args)
		{
			// 通常の起動
			//ApplicationConfiguration.Initialize();
			//Application.Run(new Form1());

			bool IsRunning = (_mutex.WaitOne(0, false))==false;
			//起動だけを調べる
			F_Args cmd = new F_Args(args);
			if (cmd.IsOptions(new string[] { "exenow", "isrun", "isrunning", "execnow" }))
			{
				string re = "false";
				if (IsRunning==true)
				{
					re = "true";
				}
				F_W.SettupConsole();
				Console.WriteLine(re);
				F_W.EndConsole();
				return;　//終わる
			}
			F_Pipe cbserver = new F_Pipe();

			
			bool IsMultExecute = cmd.IsOptions(new string[] {"m","mult"});
	
			// 二重起動を許可
			if (IsMultExecute)
			{
				ApplicationConfiguration.Initialize();
				Form1 mf = new Form1();
				mf.IsMultExecute = false;
				Application.Run(mf);
			}
			else if (IsRunning==false)
			{//起動していない
				string[] calls = new string[] { "call", "exec","start" };
				//さらに自分自身を呼び出す。
				if (cmd.IsOptions(calls))
				{
					_mutex.ReleaseMutex();
					_mutex.Dispose();
					string fullName = Application.ExecutablePath;
					if (fullName != null)
					{
						F_W.ProcessStart(fullName, cmd.ArgsString(calls));
					}
				}
				else
				{
					//　通常起動
					ApplicationConfiguration.Initialize();
					Form1 mf = new Form1();
					mf.StartServer(MyAppId);
					Application.Run(mf);
					mf.StopServer();
				}
			}
			else
			{ //起動している
			  //MessageBox.Show("すでに起動しています",
			  //				ApplicationId,
			  //				MessageBoxButtons.OK, MessageBoxIcon.Hand);

				bool Resp = (cmd.FindOption("export") >= 0);
				if (Resp)
				{
					cbserver.Server(MyCallBackId);
					cbserver.Reception += Cbserver_Reception;
				}
				PipeData pd = new PipeData(cmd.ToStringArray(), PIPECALL.DoubleExec);
				F_Pipe.Client(MyAppId, pd.ToJson()).Wait();
				if (Resp)
				{
					cbserver.Wait(1500);
				}
			}
			void Cbserver_Reception(object sender, ReceptionArg e)
			{
				F_W.SettupConsole();
				Console.WriteLine(e.Text);
				cbserver.StopServer();
				F_W.EndConsole();
				//Application.Exit();
			}
		}
	}
}