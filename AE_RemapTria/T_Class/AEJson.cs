using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
namespace BRY
{
#pragma warning disable CS8600 // Null リテラルまたは Null の可能性がある値を Null 非許容型に変換しています。
#pragma warning disable CS8602 // null 参照の可能性があるものの逆参照です。
#pragma warning disable CS8603 // Null 参照戻り値である可能性があります。
	/// <summary>
	/// After EffectsとJsonでデータのやりとりするためのクラス
	/// toSource()形式のJsonも扱える
	/// </summary>
	public class AEJson
	{
		private JsonObject? m_data = new JsonObject();
		private string _filePath = "";
		public string FilePath { get { return _filePath; } }
		public void SetFilePath(string p) { _filePath = p; }
		static private bool StringBlock(string s,int idx,out int result)
		{
			bool ret = false;
			result = -1;
			if ((idx < 0) || (idx >= s.Length)) return ret;
			if (s[idx] == '"')
			{
				for (int i = idx + 1; i < s.Length; i++)
				{
					if ((s[i] == '"') && (s[i - 1] != '\\'))
					{
						result = i;
						ret = true;
						break;
					}
				}
			}
			return ret;
		}
		static private bool IsNameTag(string s, int idx)
		{
			bool ret = false;

			for(int i= idx; i < s.Length; i++)
			{
				if ((s[i] == ' ') || (s[i] == '\t') || (s[i] == '\r') || (s[i] == 'n'))
					continue;
				if(s[i] == ':')
				{
					ret = true;
				}
				break;
			}
			return ret;
		}
		/// <summary>
		/// JsonをtoSource()形式のフォーマットに変換
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		static public string ToAEJson(string s)
		{
			string ret = "";

			int idx = 0;
			int len = s.Length;
			string block = "";
			while(idx<len)
			{
				//string ss = s.Substring(idx, 1);
				int j = 0;
				if (StringBlock(s, idx, out j) == true)
				{
					ret += block; block = "";
					string nm = s.Substring(idx, j - idx + 1);
					idx = j + 1;
					if (IsNameTag(s, idx) == true)
					{
						ret += nm.Substring(1, nm.Length - 2);
					}
					else
					{
						ret += nm;
					}
					continue;
				}
				if (s[idx]=='{')
				{
					ret += block; block = "";
					ret += "({";
					idx ++;
					continue;
				}
				if (s[idx] == '}')
				{
					ret += block; block = "";
					ret += "})";
					idx++;
					continue;
				}
				if ((s[idx] ==' ')|| (s[idx] == '\t')||(s[idx] == '\r')||(s[idx] == '\n'))
				{
					idx++;
					continue;
				}
				block += s.Substring(idx, 1);
				idx++;
			}
			if (block != "") ret += block;
			return ret;
		}
		/// <summary>
		/// toSource形式のデータを通常Jsonに変換
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		static public string FromAEJson(string s)
		{
			string ret = "";

			int idx = 0;
			int len = s.Length;
			string block = "";
			while (idx < len)
			{
				int j = 0;
				if (StringBlock(s, idx, out j) == true)
				{
					ret += block; block = "";
					ret += s.Substring(idx, j - idx + 1);
					idx = j + 1;
					continue;
				}
				if ((idx < len - 1) && (s[idx] == '(') && (s[idx + 1] == '{'))
				{
					ret += block; block = "";
					ret += "{";
					idx += 2;
					continue;
				}
				if ((idx < len - 1) && (s[idx] == '}') && (s[idx + 1] == ')'))
				{
					ret += block; block = "";
					ret += "}";
					idx += 2;
					continue;
				}
				if (s[idx] == ':')
				{
					ret += "\"" + block + "\"" + s.Substring(idx, 1);
					block = "";
					idx++;
					continue;
				}
				if ((s[idx] == ' ') || (s[idx] == '\t') || (s[idx] == '\r') || (s[idx] == '\n'))
				{
					idx++;
					continue;
				}
				if ((s[idx] == ',') || (s[idx] == '[') || (s[idx] == ']'))
				{
					ret += block + s.Substring(idx, 1);
					block = "";
					idx++;
					continue;
				}
				block += s.Substring(idx, 1);
				idx++;
			}
			if (block != "") ret += block;
			return ret;
		}

		/// <summary>
		/// 特に何もしていない
		/// </summary>
		public AEJson()
		{

		}
		// ****************************************************
		public void SetPoint(string key, Point p)
		{
			JsonObject pnt = new JsonObject();
			pnt["X"] = p.X;
			pnt["Y"] = p.Y;
			m_data.Add(key, pnt);
		}
		// ****************************************************
		public Point GetPoint(string key, out bool ok)
		{
			Point ret = new Point(0, 0);
			ok = false;
			if ((key is not null) && (m_data is not null))
			{
				try
				{
					if (m_data.ContainsKey(key))
					{
						ret.X = m_data[key]["X"].GetValue<int>();
						ret.Y = m_data[key]["Y"].GetValue<int>();
						ok = true;
					}
				}
				catch
				{
					ok = false;
					ret = new Point(0, 0);
				}
			}
			return ret;
		}
		public bool GetPoint(string key, out Point p)
		{
			bool ret = false;
			p = new Point(0, 0);
			if ((key is not null) && (m_data is not null))
			{
				try
				{
					if (m_data.ContainsKey(key))
					{
						p.X = m_data[key]["X"].GetValue<int>();
						p.Y = m_data[key]["Y"].GetValue<int>();
						ret = true;
					}
				}
				catch
				{
					ret = false;
				}
			}
			return ret;
		}
		// ****************************************************
		public void SetRect(string key, Rectangle p)
		{
			JsonObject pnt = new JsonObject();
			pnt["Left"] = p.Left;
			pnt["Top"] = p.Top;
			pnt["Width"] = p.Width;
			pnt["Height"] = p.Height;
			m_data.Add(key, pnt);
		}
		// ****************************************************
		public Rectangle GetRect(string key, out bool ok)
		{
			Rectangle ret = new Rectangle(0, 0, 0, 0);
			ok = false;
			if ((key is not null) && (m_data is not null))
			{
				try
				{
					if (m_data.ContainsKey(key))
					{
						int L = m_data[key]["Left"].GetValue<int>();
						int T = m_data[key]["Top"].GetValue<int>();
						int W = m_data[key]["Width"].GetValue<int>();
						int H = m_data[key]["Height"].GetValue<int>();
						ret = new Rectangle(L, T, W, H);
						ok = true;
					}
				}
				catch
				{
					ok = false;
					ret = new Rectangle(0, 0, 0, 0);
				}
			}
			return ret;
		}
		public bool GetRect(string key, out Rectangle r)
		{
			r = new Rectangle(0, 0, 0, 0);
			bool ret = false;
			if ((key is not null) && (m_data is not null))
			{
				try
				{
					if (m_data.ContainsKey(key))
					{
						int L = m_data[key]["Left"].GetValue<int>();
						int T = m_data[key]["Top"].GetValue<int>();
						int W = m_data[key]["Width"].GetValue<int>();
						int H = m_data[key]["Height"].GetValue<int>();
						r = new Rectangle(L, T, W, H);
						ret = true;
					}
				}
				catch
				{
					ret = false;
				}
			}
			return ret;
		}
		// ****************************************************
		public void SetValue(string key, string v)
		{
			m_data.Add(key, v);
		}
		// ****************************************************
		public string GetValueString(string key, out bool ok)
		{
			string ret = "";
			ok = false;
			if ((key is not null) && (m_data is not null))
			{
				try
				{
					if (m_data.ContainsKey(key))
					{
						ret = m_data[key].GetValue<string>();
						ok = true;
					}
				}
				catch
				{
					ret = "";
					ok = false;
				}
			}
			return ret;

		}
		// ****************************************************
		public void SetValue(string key, int v)
		{
			m_data.Add(key, v);
		}
		// ****************************************************
		public int GetValueInt(string key, out bool ok)
		{
			int ret = 0;
			ok = false;
			if ((key is not null) && (m_data is not null))
			{
				try
				{
					if (m_data.ContainsKey(key))
					{
						ret = m_data[key].GetValue<int>();
						ok = true;
					}
				}
				catch
				{
					ret = 0;
					ok = false;
				}
			}
			return ret;

		}
		// ****************************************************
		public void SetValue(string key, string[] v)
		{
			JsonArray array = new JsonArray();
			foreach (string s in v)
			{
				array.Add(s);
			}
			m_data.Add(key, array);
		}
		// ****************************************************
		public string[] GetValueStringArray(string key, out bool ok)
		{
			string[] ret = new string[0];
			ok = false;
			if ((key is not null) && (m_data is not null))
			{
				try
				{
					if (m_data.ContainsKey(key))
					{
						var ja = m_data?[key].AsArray();
						if (ja.Count > 0)
						{
							ret = new string[ja.Count];
							int i = 0;
							foreach (var s in ja)
							{
								ret[i] = s.GetValue<string>();
								i++;
							}
							ok = true;
						}
					}
				}
				catch
				{
					ret = new string[0];
					ok = false;
				}
			}
			return ret;

		}
		// ****************************************************
		public void SetValue(string key, int[] v)
		{
			JsonArray array = new JsonArray();
			foreach (int s in v)
			{
				array.Add(s);
			}
			m_data.Add(key, array);
		}
		// ****************************************************
		public int[] GetValueIntArray(string key, out bool ok)
		{
			int[] ret = new int[0];
			ok = false;
			if ((key is not null) && (m_data is not null))
			{
				try
				{
					if (m_data.ContainsKey(key))
					{
						var ja = m_data?[key].AsArray();
						if (ja.Count > 0)
						{
							ret = new int[ja.Count];
							int i = 0;
							foreach (var s in ja)
							{
								ret[i] = s.GetValue<int>();
								i++;

							}
							ok = true;
						}
					}
				}
				catch
				{
					ret = new int[0];
					ok = false;
				}
			}
			return ret;

		}
		// ****************************************************
		public void SetValue(string key, bool v)
		{
			m_data.Add(key, v);
		}
		// ****************************************************
		public bool GetValueBool(string key, out bool ok)
		{
			bool ret = false;
			ok = false;
			if ((key is not null) && (m_data is not null))
			{
				try
				{
					if (m_data.ContainsKey(key))
					{
						ret = m_data[key].GetValue<bool>();
						ok = true;
					}
				}
				catch
				{
					ret = false;
					ok = false;
				}
			}
			return ret;

		}
		// ****************************************************
		public void SetValue(string key, bool[] v)
		{
			JsonArray array = new JsonArray();
			foreach (bool s in v)
			{
				array.Add(s);
			}
			m_data.Add(key, array);
		}
		// ****************************************************
		public bool[] GetValueBoolArray(string key, out bool ok)
		{
			bool[] ret = new bool[0];
			ok = false;
			if ((key is not null) && (m_data is not null))
			{
				try
				{
					if (m_data.ContainsKey(key))
					{
						var ja = m_data?[key].AsArray();
						if (ja.Count > 0)
						{
							ret = new bool[ja.Count];
							int i = 0;
							foreach (var s in ja)
							{
								ret[i] = s.GetValue<bool>();
								i++;

							}
							ok = true;
						}
					}
				}
				catch
				{
					ret = new bool[0];
					ok = false;
				}
			}
			return ret;
		}
		// ****************************************************
		public string ToJson(bool isAE=false)
		{
			string js= m_data.ToJsonString();
			if (isAE)
			{
				js= ToAEJson(js);
			}
			return js;
		}
		// ****************************************************
		// ****************************************************
		public void FromJson(string js)
		{
			try
			{
				js = js.Trim();
				if((js.Length >= 2) && (js[0]=='(')&& (js[1] == '{'))
				{
					js = FromAEJson(js);
				}
				var doc = JsonNode.Parse(js);
				m_data = (JsonObject)doc;
			}
			catch
			{
				m_data = new JsonObject();
			}
		}
		// ****************************************************
		public JsonObject GetKey(string key)
		{
			JsonObject? ret = null;
			if ((key is not null) && (m_data is not null))
			{
				try
				{
					if ((key is not null) && (m_data is not null))
					{
						if (m_data.ContainsKey(key))
						{
							ret = m_data[key].GetValue<JsonObject>();
						}
					}
				}
				catch
				{
					ret = null;
				}
			}
			return ret;
		}
		// ****************************************************
		public bool Save(string p,bool isAE = false)
		{
			bool ret = false;
			try
			{
				string js = ToJson(isAE);
				File.WriteAllText(p, js, Encoding.GetEncoding("utf-8"));
				ret = true;
				_filePath = p;
			}
			catch
			{
				ret = false;
			}
			return ret;
		}
		// ****************************************************
		public bool Save(bool isAE=false)
		{
			return Save(_filePath,isAE);
		}
		// ****************************************************
		public bool Load(string p)
		{
			bool ret = false;

			try
			{
				if (File.Exists(p) == true)
				{
					string str = File.ReadAllText(p, Encoding.GetEncoding("utf-8"));
					if (str != "")
					{
						FromJson(str);
						ret = true;
						_filePath = p;
					}
				}
			}
			catch
			{
				m_data = new JsonObject();
				ret = false;
			}
			return ret;
		}
		// ****************************************************
		/// <summary>
		/// デフォルトのパスを読み込む
		/// </summary>
		/// <returns></returns>
		public bool Load()
		{
			return Load(_filePath);
		}
	}
#pragma warning restore CS8600 // Null リテラルまたは Null の可能性がある値を Null 非許容型に変換しています。
#pragma warning restore CS8602 // null 参照の可能性があるものの逆参照です。
#pragma warning restore CS8603 // Null 参照戻り値である可能性があります。
}
