using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
		private JsonObject? obj = new JsonObject();

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
				var doc = JsonNode.Parse(js);
				obj = (JsonObject?)doc;
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
			if (obj == null)
			{
				return "";
			}
			else
			{
				return obj.ToJsonString();

			}
		}
		// --------------------------------
		public void SetPIPECALL(PIPECALL pc)
		{
			int vv = (int)pc;
			if (obj != null)
			{
				obj.Add("PIPECALL", vv);
			}
		}
		// --------------------------------
		public void SetArgs(string[] args)
		{
			if (obj != null)
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
		}
		// --------------------------------
		public string[] GetArgs()
		{
			string[] ret = new string[0];
			if (obj != null)
			{
				try
				{
#pragma warning disable CS8602 // null 参照の可能性があるものの逆参照です。
					var ja = obj["Args"].AsArray();
#pragma warning restore CS8602 // null 参照の可能性があるものの逆参照です。
					if (ja.Count > 0)
					{
						ret = new string[ja.Count];
						int i = 0;
						foreach (var item in ja)
						{
							if (item != null)
							{
								ret[i] = item.GetValue<string>();
							}
							i++;
						}
					}
				}
				catch
				{
					ret = new string[0];
				}
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
				int i=(int)PIPECALL.None;
				if(obj!=null)
				{
#pragma warning disable CS8602 // null 参照の可能性があるものの逆参照です。
					i = obj["PIPECALL"].GetValue<int>();
#pragma warning restore CS8602 // null 参照の可能性があるものの逆参照です。
				}
				ret = (PIPECALL)i;
			}
			catch
			{
				ret = PIPECALL.None;
			}
			return ret;
		}
	}

}
