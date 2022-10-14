using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace BRY
{
	public class Pipe
	{
		public bool _execution = true;
		public string _Result = "";

		public void MessagePipeServer(string pipeName)
		{
			Task.Run(() =>
			{ //Taskを使ってクライアント待ち
				while (_execution)
				{
					//複数作ることもできるが、今回はwhileで1つずつ処理する
					using (NamedPipeServerStream pipeServer = new NamedPipeServerStream(pipeName, PipeDirection.InOut, 1))
					{
						// クライアントの接続待ち
						pipeServer.WaitForConnection();

						StreamString ssSv = new StreamString(pipeServer);

						while (true)
						{ //データがなくなるまで                       
							string read = ssSv.ReadString(); //クライアントの引数を受信 
							if (string.IsNullOrEmpty(read))
							{
								break;
							}
							else
							{
								_Result = read;
								_execution = false;
							}
							if (!_execution)
								break; //起動停止？
						}
						ssSv = null;
					}
				}
			});
		}
		// ******************************************************************************
		public Task PipeClient(string pipeName, string js)
		{
			return Task.Run(() =>
			{ //Taskを使ってサーバに送信waitで処理が終わるまで待つ
				using (NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", pipeName, PipeDirection.InOut, PipeOptions.None, System.Security.Principal.TokenImpersonationLevel.Impersonation))
				{
					StreamString ssCl;
					string writeData;
					pipeClient.Connect();

					ssCl = new StreamString(pipeClient);
					writeData = js; //送信する引数
					ssCl.WriteString(writeData);
					ssCl = null;
				}
			});
		}
	}
}
