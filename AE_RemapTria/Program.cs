using BRY;

namespace AE_RemapTria
{

	internal static class Program
	{
		private const string ApplicationId = "AE_RemapTria"; // GUIDなどユニークなもの
		private static System.Threading.Mutex _mutex = new System.Threading.Mutex(false, ApplicationId);

		// *******************************************************************************************
		[STAThread]
		static void Main(string[] args)
		{
			// 通常の起動
			//ApplicationConfiguration.Initialize();
			//Application.Run(new Form1());

			if (_mutex.WaitOne(0, false))
			{//起動していない
				MainForm._execution = true;
				MainForm.ArgumentPipeServer(ApplicationId);
				ApplicationConfiguration.Initialize();
				Application.Run(new Form1());
				MainForm._execution = false;
			}
			else
			{ //起動している
			  //MessageBox.Show("すでに起動しています",
			  //				ApplicationId,
			  //				MessageBoxButtons.OK, MessageBoxIcon.Hand);

				PipeData pd = new PipeData(args, PIPECALL.DoubleExec);
				CallExe.PipeClient(ApplicationId, pd.ToJson()).Wait();
			}
		}
	}
}