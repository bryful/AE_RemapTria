using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Pipes;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace BRY
{
	/// <summary>
	/// どこから送られたか
	/// </summary>
	public enum PIPECALL
	{
		/// <summary>
		/// 最初の起動コマンド
		/// </summary>
		StartupExec,
		/// <summary>
		/// 起動中に送られてきた
		/// </summary>
		DoubleExec,
		/// <summary>
		/// Pipeで送られてきた
		/// </summary>
		PipeExec,
		None
	}
	/// <summary>
	/// Pipeで送受信する時のJsonデータを扱うクラス
	/// </summary>
	public class PipeData
	{
		private JsonObject obj = new JsonObject();

		public string[] Args { get { return GetArgs(); } set { SetArgs(value); } }
		public PIPECALL PIPECALL { get { return GetPIPECALL(); } set { SetPIPECALL(value); } }
		public PipeData(string[] args, PIPECALL pc)
		{
			SetArgs(args);
			SetPIPECALL(pc);
		}
		// --------------------------------
		public PipeData(string js)
		{
			FromJson(js);
		}
		// --------------------------------
		/// <summary>
		/// Jsonからデータを得る
		/// </summary>
		/// <param name="js"></param>
		public void FromJson(string js)
		{
			try
			{
				if (js != "")
				{
					var doc = JsonNode.Parse(js);
					if (doc != null)
					{
						obj = (JsonObject)doc;
					}
				}
			}
			catch
			{
				obj = new JsonObject();
			}
		}
		// --------------------------------
		/// <summary>
		/// Jsonへ
		/// </summary>
		/// <returns></returns>
		public string ToJson()
		{
			if (obj != null)
			{
				return obj.ToJsonString();
			}
			else
			{
				return "";
			}
		}
		// --------------------------------
		public void SetPIPECALL(PIPECALL pc)
		{
			obj.Add("PIPECALL", (int)pc);
		}
		// --------------------------------
		public void SetArgs(string[] args)
		{
			JsonArray ja = new JsonArray();
			if (args.Length > 0)
			{
				foreach (string arg in args)
				{
					ja.Add(arg);
				}
			}
			obj.Add("Args", ja);
		}
		// --------------------------------
		public string[] GetArgs()
		{
			string[] ret = new string[0];
			try
			{
				var ja = obj["Args"].AsArray();
				if ((ja!=null)&&(ja.Count > 0))
				{
					ret = new string[ja.Count];
					int i = 0;
					foreach (var item in ja)
					{
						ret[i] = item.GetValue<string>();
						i++;
					}
				}
			}
			catch
			{
				ret = new string[0];
			}
			return ret;
		}
		// --------------------------------
		// ****************************************************
		public PIPECALL GetPIPECALL()
		{
			PIPECALL ret = PIPECALL.None;
			try
			{
				int i = obj["PIPECALL"].GetValue<int>();
				ret = (PIPECALL)i;
			}
			catch
			{
				ret = PIPECALL.None;
			}
			return ret;
		}
	}
	public class ReceptionArg : EventArgs
	{
		public string Text="";
		public ReceptionArg(string v)
		{
			Text = v;
		}
	}
	public class F_Pipe
	{
		private bool _execution = true;
		public delegate void ReceptionHandler(object sender, ReceptionArg e);
		public event ReceptionHandler? Reception;
		// ************************************************************
		protected virtual void OnReception(ReceptionArg e)
		{
			if (Reception != null)
			{
				Reception(this, e);
			}
		}
		public bool IsRunning { get { return _execution; } }
		// ************************************************************
		public void StopServer()
		{
			if (_execution)
			{
				_execution = false;
			}
		}
		// ************************************************************
		public void Wait(int ms)
		{
			int cnt = 0;
			int cntMax = ms / 50;
			while (_execution)
			{
				Thread.Sleep(50);
				cnt++;
				if (cnt >= cntMax) { StopServer(); }
			}

		}
		// ************************************************************
		public F_Pipe()
		{
			_execution = true;
		}
		public F_Pipe(string pipeName)
		{
			_execution = true;
			Server(pipeName);
		}
		// ************************************************************
		public void Server(string pipeName)
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

						StreamString? ssSv = new StreamString(pipeServer);

						while (true)
						{ //データがなくなるまで                       
							string read = ssSv.ReadString(); //クライアントの引数を受信 
							if (string.IsNullOrEmpty(read))
								break;

							//引数が受信できたら、Applicationに登録されているだろうForm1に引数を送る
							/*
							FormCollection apcl = Application.OpenForms;

							if (apcl.Count > 0)
							{
								PipeData pd = new PipeData(read);
								((MainForm)apcl[0]).Command(pd.GetArgs(), pd.GetPIPECALL()); //取得した引数を送る
							}*/

							ReceptionArg rcp = new ReceptionArg(read);
							OnReception(rcp);

							if (!_execution)
								break; //起動停止？
						}
						ssSv = null;
					}
				}
			});
		}       
		// ******************************************************************************
		static public Task Client(string pipeName, string js)
		{
			return Task.Run(() =>
			{ //Taskを使ってサーバに送信waitで処理が終わるまで待つ
				using (NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", pipeName, PipeDirection.InOut, PipeOptions.None, System.Security.Principal.TokenImpersonationLevel.Impersonation))
				{
					StreamString? ssCl;
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
	// **************************
	public class StreamString
	{
		private System.IO.Stream ioStream;
		private System.Text.UnicodeEncoding streamEncoding;
		public StreamString(System.IO.Stream ioStream)
		{
			this.ioStream = ioStream;
			streamEncoding = new System.Text.UnicodeEncoding();
		}

		// ********************************************************************
		public string ReadString()
		{
			int len = 0;
			len = ioStream.ReadByte() * 256; //テキスト長
			len += ioStream.ReadByte(); //テキスト長余り
			if (len > 0)
			{ //テキストが格納されている
				byte[] inBuffer = new byte[len];
				ioStream.Read(inBuffer, 0, len); //テキスト取得
				return streamEncoding.GetString(inBuffer);
			}
			else //テキストなし
				return "";
		}
		// ********************************************************************
		public int WriteString(string outString)
		{
			if (string.IsNullOrEmpty(outString))
				return 0;
			byte[] outBuffer = streamEncoding.GetBytes(outString);
			int len = outBuffer.Length; //テキストの長さ
			if (len > UInt16.MaxValue)
				len = (int)UInt16.MaxValue; //65535文字
			ioStream.WriteByte((byte)(len / 256)); //テキスト長
			ioStream.WriteByte((byte)(len & 255)); //テキスト長余り
			ioStream.Write(outBuffer, 0, len); //テキストを格納
			ioStream.Flush();
			return outBuffer.Length + 2; //テキスト＋２(テキスト長)
		}
	}
}
