using BRY;

namespace SkeltonWinForm
{
	internal static class Program
	{
		// *******************************************************************************************
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

			F_Pipe cbserver = new F_Pipe();
			F_Args cmd = new F_Args(args);

			if (_mutex.WaitOne(0, false))
			{//起動していない
				// 呼び出して終了する
				string[] calls = new string[] { "call", "exec" }; 
				if (cmd.IsOptions(calls))
				{
					_mutex.ReleaseMutex();
					_mutex.Dispose();
					string fullName = Environment.ProcessPath;
					if (fullName != null)
					{
						F_W.ProcessStart(fullName, cmd.ArgsString(calls));
					}
				}
				else
				{
					//　通常起動
					ApplicationConfiguration.Initialize();
					MainForm mf = new MainForm();
					mf.StartServer(MyAppId);
					Application.Run(mf);
					mf.StopServer();
				}
			}
			else
			{ 
				//起動している
				//MessageBox.Show("すでに起動しています",
				//				ApplicationId,
				//				MessageBoxButtons.OK, MessageBoxIcon.Hand);
				bool Resp = (cmd.FindOption("callback")>=0);
				if (Resp)
				{
					cbserver.Server(MyCallBackId);
					cbserver.Reception += Cbserver_Reception;
				}
				PipeData pd = new PipeData(cmd.ToStringArray(), PIPECALL.DoubleExec);
				F_Pipe.Client(MyAppId, pd.ToJson()).Wait();
				if(Resp)
				{
					cbserver.Wait(1500);
				}
			}
			void Cbserver_Reception(object sender, ReceptionArg e)
			{
				F_W.SettupConsole();
				Console.WriteLine("aaa");
				Console.WriteLine(e.Text);
				cbserver.StopServer();
				F_W.EndConsole();
				//Application.Exit();
			}
		}

	}
}