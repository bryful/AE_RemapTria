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
		private JsonObject m_data = new JsonObject();
		// *********************************
		private string _m_AppName = "";
		public string AppName { get { return _m_AppName; } }
		// *********************************
		private string _filePath = "";
		public string FilePath { get { return _filePath; } }
		public void SetFilePath(string p) { _filePath = p; }
		// *********************************
		private Form? m_Form = null;
		private string m_FileDirectory = "";
		public string FileDirectory { get { return m_FileDirectory; } }

		// *********************************
		public PrefFile(Form? fm =null,string aName = "")
		{
			m_Form = fm;
			if (aName == "")
			{
				_m_AppName = Path.GetFileNameWithoutExtension(Application.ExecutablePath);
			}
			else
			{
				_m_AppName = aName;
			}
			m_FileDirectory = GetFileSystemPath(Environment.SpecialFolder.ApplicationData);
			SetFilePath(Path.Combine(m_FileDirectory,_m_AppName + ".json"));
		}
		// ****************************************************
		public static string GetFileSystemPath(Environment.SpecialFolder folder)
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
		public void StoreForm()
		{
			if (m_Form == null) return;
			SetRect("FormBounds", m_Form.Bounds);
		}
		// ****************************************************
		public void RestoreForm()
		{
			if (m_Form == null) return;
			bool ok = false;
			Rectangle r = GetRect("FormBounds", out ok);
			if (ok)
			{
				m_Form.MaximumSize = new Size(65536, 65536);
				m_Form.Bounds = r;
			}
			if ((ok == false) || (ScreenIn(r) == false))
			{
				Rectangle rct = Screen.PrimaryScreen.Bounds;
				Point p = new Point((rct.Width - m_Form.Width) / 2, (rct.Height - m_Form.Height) / 2);
				m_Form.Location = p;
			}

		}
		// ****************************************************
		public void SetPoint(string key, Point p)
		{
			JsonObject pnt = new JsonObject();
			pnt.Add("X", p.X);
			pnt.Add("Y", p.Y);
			m_data.Add(key, pnt);
		}
		// ****************************************************
		public Point GetPoint(string key, out bool ok)
		{
			Point ret = new Point(0,0);
			ok = false;
			if (key == "") return ret;
			try
			{
				if(m_data.ContainsKey(key))
				{
					JsonObject obj = m_data[key].AsObject();
					if(obj != null)
					{
						if((obj.ContainsKey("X"))&& (obj.ContainsKey("Y")))
						{
							ret.X = obj["X"].GetValue<int>();
							ret.X = obj["Y"].GetValue<int>();
							ok = true;
						}
					}

				}
			}
			catch
			{
				ok = false;
				ret = new Point(0, 0);
			}
			return ret;
		}
		// ****************************************************
		public void SetRect(string key, Rectangle p)
		{
			JsonObject pnt = new JsonObject();
			pnt.Add("Left", p.Left);
			pnt.Add("Top", p.Top);
			pnt.Add("Width", p.Width);
			pnt.Add("Height", p.Height);
			m_data.Add(key, pnt);
		}
		// ****************************************************
		public Rectangle GetRect(string key, out bool ok)
		{
			Rectangle ret = new Rectangle(0, 0,0,0);
			ok = false;
			if (key == "") return ret;
			try
			{
				if (m_data.ContainsKey(key))
				{
					JsonObject obj = m_data[key].AsObject();
					if (obj != null)
					{
						if (obj.ContainsKey("Left") && obj.ContainsKey("Top") && obj.ContainsKey("Width") && obj.ContainsKey("Height"))
						{
							int L = obj["Left"].GetValue<int>();
							int T = obj["Top"].GetValue<int>();
							int W = obj["Width"].GetValue<int>();
							int H = obj["Height"].GetValue<int>();
							ret = new Rectangle(L, T, W, H);
							ok = true;
						}
					}
				}
			}
			catch
			{
				ok = false;
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
			if (key == null) return ret;
			try
			{
				if(m_data.ContainsKey(key))
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
			if(key=="") return ret;
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
			if (key == "") return ret;
			try
			{
				if (m_data.ContainsKey(key))
				{
					JsonArray ja = m_data[key].AsArray();
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
			if (key == "") return ret;
			try
			{
				if (m_data.ContainsKey(key))
				{
					JsonArray ja = m_data[key].AsArray();
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
			if (key == "") return ret;
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
			if (key == "") return ret;
			try
			{
				if (m_data.ContainsKey(key))
				{
					JsonArray ja = m_data[key].AsArray();
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
				if(js=="") return;
				var doc = JsonNode.Parse(js);
				if (doc != null)
				{
					m_data = (JsonObject)doc;
				}
			}
			catch
			{
				m_data = new JsonObject();
			}
		}
		// ****************************************************
		public JsonObject? GetKey(string key)
		{
			JsonObject? ret = null;
			if (key == "") return ret;
			try
			{
				if (m_data.ContainsKey(key))
				{
					ret =  m_data[key].AsObject();
				}
			}
			catch
			{
				ret = null;
			}
			return ret;
		}
		// ****************************************************
		public bool Save()
		{
			return Save(_filePath);
		}
		// ****************************************************
		public bool Save(string p)
		{
			bool ret = false;
			try
			{
				string js = ToJson();
				File.WriteAllText(p, js);
				ret = true;
			}
			catch
			{
				ret = false;
			}
			return ret;
		}
		// ****************************************************
		public bool Load()
		{
			return Load(_filePath);
		}
		// ****************************************************
		public bool Load(string p)
		{
			bool ret = false;

			try
			{
				if (File.Exists(p) == true)
				{
					string str = File.ReadAllText(p);
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
				Rectangle r = s.WorkingArea;
				if (IsInRect(r, rct))
				{
					ret = true;
					break;
				}
			}
			return ret;
		}
		// ****************************************************
		static public Rectangle NowScreen(Rectangle rct)
		{
			Rectangle ret = new Rectangle(0,0,0,0);
			foreach (Screen s in Screen.AllScreens)
			{
				Rectangle r = s.WorkingArea;
				if (IsInRect(r, rct))
				{
					ret = r;
					break;
				}
			}
			return ret;
		}
		// ****************************************************
		static public bool ScreenIn(Point p,Size sz)
		{
			return ScreenIn(new Rectangle(p, sz));
		}
	}
}
