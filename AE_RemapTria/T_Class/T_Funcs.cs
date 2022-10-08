using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace AE_RemapTria
{
	/// <summary>
	/// 関数ポインタ代わりのデリゲート
	/// </summary>
	/// <returns></returns>
	public delegate bool FuncType();
	// ***************************************************************************
	/// <summary>
	/// 機能をカプセル化したクラス
	/// </summary>
	public class FuncItem
	{
		private string m_EngName = "";
		/// <summary>
		/// 機能の英語名、関数の名前から獲得
		/// </summary>
		public string EngName { get { return m_EngName; }  }
		/// <summary>
		/// 機能の日本語名
		/// </summary>
		public string JapName { get; set; }

		/// <summary>
		/// 機能の名前。OSによって英語名・日本名を切り替える
		/// </summary>
		public string Caption
		{
			get
			{
				string ret = m_EngName;
				if ((JapName!="")&& (System.Globalization.CultureInfo.CurrentUICulture.Name == "ja-JP"))
				{
					ret = JapName;
				}
				return ret;
			}
		}

		/// <summary>
		/// 実際の機能。関数ポインタ
		/// </summary>
		public FuncType? Func { get; set; }
		/// <summary>
		/// ショートカットキー
		/// </summary>
		private Keys[] m_Keys = new Keys[2];

		/// <summary>
		/// ショートカットキー
		/// </summary>
		public Keys[] Keys
		{
			get { return m_Keys; }
			set { m_Keys = value; }
		}
		public FuncItem(FuncType fnc, Keys[] keys)
		{
			Func = fnc;
			Func = fnc;
			m_EngName = fnc.Method.Name;
			JapName = "";
			m_Keys = keys;
		}
		public FuncItem(FuncType fnc, Keys key, string JapN="")
		{
			Func = fnc;
			m_EngName = fnc.Method.Name;
			JapName = JapN;
			m_Keys = new Keys[] { key };
		}
		public FuncItem(FuncType fnc, Keys key0, Keys key1, string japN = "")
		{
			Func = fnc;
			m_EngName = fnc.Method.Name;
			JapName = japN;
			m_Keys = new Keys[] { key0,key1 };
		}
		/// <summary>
		/// 同じショートカットキーがあるか
		/// </summary>
		/// <param name="k">探すKeys</param>
		/// <returns></returns>
		public bool IsKey(Keys k)
		{
			bool ret = false;
			if(m_Keys.Length>0)
			{
				foreach(Keys k2 in m_Keys)
				{
					if(k2==k)
					{
						ret = true;
						break;
					}
				}
			}
			return ret;
		}
	}

	// ***************************************************************************
	/// <summary>
	/// 機能を管理するクラス
	/// </summary>
	public class T_Funcs
	{
		private  FuncItem[] m_FuncItems = new FuncItem[0];
		public int Count
		{
			get
			{
				return m_FuncItems.Length;
			}
		}
		// ********************************************************************
		public T_Funcs()
		{
			m_FuncItems = new FuncItem[0]; ;
		}
		// ********************************************************************
		/// <summary>
		/// ショートカットキーを探す
		/// </summary>
		/// <param name="k">FunctionItem自体</param>
		/// <returns></returns>
		public FuncItem? FindKeys(Keys k)
		{
			FuncItem? ret =null;
			if(m_FuncItems.Length>0)
			{
				foreach(FuncItem item in m_FuncItems)
				{
					if (item.IsKey(k) == true)
					{
						ret = item;
						break;
					}
				}
			}
			return ret;
		}
		/// <summary>
		/// 初期化
		/// </summary>
		private void Init()
		{
			m_FuncItems = new FuncItem[0]; ;
		}
		/// <summary>
		/// 関数を探す
		/// </summary>
		/// <param name="name">インデックス</param>
		/// <returns></returns>
		public int IndexOfFunc(string name)
		{
			int ret = -1;
			if(m_FuncItems.Length > 0)
			{
				for(int i= 0; i < m_FuncItems.Length; i++)
				{
					if (string.Compare(m_FuncItems[i].EngName, name, true) == 0)
					{
						ret=i;
						break;
					}
				}
			}
			return ret;
		}
		/// <summary>
		/// 関数を探す
		/// </summary>
		/// <param name="Engn"></param>
		/// <returns></returns>
		public FuncItem? FindFunc(string name)
		{
			FuncItem? ret = null;
			int idx = IndexOfFunc(name);
			if (idx >= 0) ret = m_FuncItems[idx];
			return ret;
		}
		/// <summary>
		/// 関数配列を設定
		/// </summary>
		/// <param name="fs"></param>
		public void SetFuncItems(FuncItem[] fs)
		{
			m_FuncItems = fs;
		}
		/// <summary>
		/// 関数配列をJsonに
		/// </summary>
		/// <returns></returns>
		public string ToJson()
		{
			JsonObject jo = new JsonObject();
			jo.Add("header", "AE_RemapTria");
			JsonArray ja = new JsonArray();

			foreach(FuncItem item in m_FuncItems)
			{
				JsonObject jo2 = new JsonObject();
				jo2.Add("name", item.EngName);
				jo2.Add("jap", item.JapName);
				JsonArray ja2 = new JsonArray();
				if(item.Keys.Length>0)
				{
					foreach(Keys k in item.Keys)
					{
						ja2.Add(k);
					}
				}
				jo2.Add("keys", ja2);
				ja.Add(jo2);
			}
			jo.Add("funcList", ja);
			//JsonSerializerOptions options = new() { WriteIndented = true };
			return jo.ToJsonString();
		}
#pragma warning disable CS8602 // null 参照の可能性があるものの逆参照です。
		public bool FromJson(string json)
		{
			bool ret = false;
			try
			{
				if ((json == null) || (json == "")) return ret;
				var doc = JsonNode.Parse(json);
				if (doc == null) return ret;
				JsonObject jo = (JsonObject)doc;

				string key = "header";
				string s = "";
				if(jo.ContainsKey(key)==false) return ret;
				s = jo[key].GetValue<string>();
				if (s!= "AE_RemapTria") return ret;

				key = "funcList";
				if (jo.ContainsKey(key) == false) return ret;
				JsonArray ja = jo[key].AsArray();
				List<string> names = new List<string>();
				List<string> jnames = new List<string>();
				List<Keys[]> ks = new List<Keys[]>();

				if (ja.Count>0)
				{
					foreach(var item in ja)
					{
						JsonObject jn = (JsonObject)item;
						key = "name";
						if (jn.ContainsKey(key) == false) continue;
						string nm = jn[key].GetValue<string>();
						key = "jap";
						string jnm = "";
						if (jn.ContainsKey(key) == true)
						{
							jnm= jn[key].GetValue<string>();
						}
						key = "keys";
						Keys[] kk = new Keys[0];
						if(jn.ContainsKey(key))
						{
							int[] a = jn[key].GetValue<int[]>();
							if(a.Length>0)
							{
								kk = new Keys[a.Length];
								for(int i=0; i<a.Length; i++)
								{
									kk[i] = (Keys)a[i];
								}
							}
						}
						names.Add(nm);
						jnames.Add(jnm);
						ks.Add(kk);
					}
					if(names.Count > 0)
					{
						for (int i=0; i< names.Count;i++)
						{
							int idx = IndexOfFunc(names[i]);
							if(idx>=0)
							{
								m_FuncItems[idx].JapName = jnames[i];
								m_FuncItems[idx].Keys = ks[i];
							}
						}
					}
					ret = true;
				}
			}
			catch
			{
				ret = false;
			}
			return ret;
		}
#pragma warning restore CS8602 // null 参照の可能性があるものの逆参照です。
	}
}
