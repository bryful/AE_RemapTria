using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace BRY
{
	public class PrefFile
	{
#pragma warning disable CS8602 // null 参照の可能性があるものの逆参照です。
		// *********************************
		private string _filePath = "";
		public string FilePath { get { return _filePath; } }
		private string _m_AppName = "";
		public string AppName { get { return _m_AppName; } }

		// *********************************
		private JsonObject? m_data = new JsonObject();
		// *********************************
		public PrefFile(string aName = "")
		{
			if (aName == "")
			{
				_m_AppName = Path.GetFileNameWithoutExtension(Application.ExecutablePath);
			}
			else
			{
				_m_AppName = aName;
			}
			_filePath = Path.Combine(
				GetFileSystemPath(Environment.SpecialFolder.ApplicationData),
				_m_AppName + ".json");
		}
		// ****************************************************
		private static string GetFileSystemPath(Environment.SpecialFolder folder)
		{
			// パスを取得
			string path = String.Format(@"{0}\{1}",//\{2}
			  Environment.GetFolderPath(folder),  // ベース・パス
			  //Application.CompanyName,            // 会社名
			  Application.ProductName
			  );           // 製品名

			// パスのフォルダを作成
			lock (typeof(Application))
			{
				if (!Directory.Exists(path))
				{
					Directory.CreateDirectory(path);
				}
			}
			return path;
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
			Point ret = new Point(0,0);
			ok = false;
			if ((key is not null) && (m_data is not null))
			{
				try
				{
					ret.X = m_data[key]["X"].GetValue<int>();
					ret.Y = m_data[key]["Y"].GetValue<int>();
					ok = true;
				}
				catch
				{
					ok = false;
					ret = new Point(0, 0);
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
			Rectangle ret = new Rectangle(0, 0,0,0);
			ok = false;
			if ((key is not null) && (m_data is not null))
			{
				try
				{
					int L = m_data[key]["Left"].GetValue<int>();
					int T = m_data[key]["Top"].GetValue<int>();
					int W = m_data[key]["Width"].GetValue<int>();
					int H = m_data[key]["Height"].GetValue<int>();
					ret = new Rectangle(L, T, W, H);
					ok = true;
				}
				catch
				{
					ok = false;
					ret = new Rectangle(0, 0, 0, 0);
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
		public string GetValueString(string key ,out bool ok)
		{
			string ret = "";
			ok = false;
			if ((key is not null) && (m_data is not null))
			{
				try
				{
					ret = m_data[key].GetValue<string>();
					ok = true;
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
					ret = m_data[key].GetValue<int>();
					ok = true;
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
			foreach(string s in v)
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
					var ja = m_data?[key].AsArray();
					if(ja.Count>0)
					{
						ret = new string[ja.Count];
						int i = 0;
						foreach(var s in ja)
						{
							ret[i] = s.GetValue<string>();
							i++;
						}
						ok = true;
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
					ret = m_data[key].GetValue<bool>();
					ok = true;
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
				catch
				{
					ret = new bool[0];
					ok = false;
				}
			}
			return ret;
		}
		// ****************************************************
		public string ToJson()
		{
			return m_data.ToJsonString();
			//return JsonSerializer.Serialize(m_data);
		}
		// ****************************************************
		public void FromJson(string js)
		{
			try
			{
				var doc = JsonNode.Parse(js);
#pragma warning disable CS8600 // Null リテラルまたは Null の可能性がある値を Null 非許容型に変換しています。
				m_data = (JsonObject)doc;
#pragma warning restore CS8600 // Null リテラルまたは Null の可能性がある値を Null 非許容型に変換しています。
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
						ret =  m_data[key].GetValue<JsonObject>();
					}
				}
				catch
				{
					ret = null;
				}
			}
#pragma warning disable CS8603 // Null 参照戻り値である可能性があります。
			return ret;
#pragma warning restore CS8603 // Null 参照戻り値である可能性があります。
		}
		// ****************************************************
		public bool Save(string p)
		{
			bool ret = false;
			try
			{
				string js = ToJson();
				File.WriteAllText(p, js, Encoding.GetEncoding("utf-8"));
				ret = true;
			}
			catch
			{
				ret = false;
			}
			return ret;
		}
		// ****************************************************
		public bool Save()
		{
			return Save(_filePath);
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
		// ****************************************************
		static public bool IsInRect(Rectangle a, Rectangle b)
		{
			bool ret = true;

			if ((a.Left > b.Left + b.Width) || (a.Left + a.Width < b.Left))
			{
				ret = false;
			}
			if ((a.Top > b.Top + b.Height) || (a.Top + a.Height < b.Top))
			{
				ret = false;
			}
			return ret;
		}
		// ****************************************************
		static public bool ScreenIn(Rectangle rct)
		{
			bool ret = false;
			foreach (Screen s in Screen.AllScreens)
			{
				Rectangle r = s.Bounds;
				if (IsInRect(r, rct))
				{
					ret = true;
					break;
				}
			}
			return ret;
		}
		static public bool ScreenIn(Point p,Size sz)
		{
			return ScreenIn(new Rectangle(p, sz));
		}
#pragma warning restore CS8602 // null 参照の可能性があるものの逆参照です。
	}
}
