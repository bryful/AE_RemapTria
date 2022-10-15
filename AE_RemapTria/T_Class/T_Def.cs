using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AE_RemapTria
{
	/// <summary>
	/// 実行モード
	/// </summary>
	public enum EXEC_MODE
	{
		NONE = 0,
		EXENOW,
		CALL,
		EXPORT,
		IMPORT_LAYER,
		QUIT
	}
	/// <summary>
	/// フレームレート
	/// </summary>
	public enum T_Fps
	{
		FPS24 = 24,
		FPS30 = 30
	}
	/// <summary>
	/// １ページのフレーム数 ６秒シートと３秒シート
	/// </summary>
	public enum T_PageSec
	{
		sec3 = 3,
		sec6 = 6
	}
	//フレームの表示設定
	public enum T_FrameDisp
	{
		frame = 0,
		pageFrame,
		pageSecFrame,
		SecFrame,
		Count
	}
	/// <summary>
	/// セルフレームの表示タイプ CellSatusで使用
	/// </summary>
	public enum CellType
	{
		/// <summary>
		/// 表示なし
		/// </summary>
		None,
		/// <summary>
		/// 数字表示
		/// </summary>
		Normal,
		/// <summary>
		/// 前のコマと同じ数値
		/// </summary>
		SameAsBefore,
		/// <summary>
		/// 空セルはじめ
		/// </summary>
		EmptyStart
	}
	/// <summary>
	/// セルフレームの表示データ T_Gridで描画しに使用
	/// </summary>
	public class CellSatus
	{
		public int Cell = -1;
		public int Frame = -1;
		public int Value = 0;
		public CellType Status = CellType.None;

		public CellSatus()
		{
			Init();
		}
		public void Init()
		{
			Cell = -1;
			Frame = -1;
			Value = 0;
			Status = CellType.None;
		}
	}

	public class T_Def
	{
		public const string ARDJ_Header = "ardjV2";
		// ********************************************************
		/// <summary>
		/// マルチピリオド対応
		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		public static string GetExt(string p)
		{
			string ret = "";
			int idx = IndexOfMuitPriod(p);
			if(idx >=0)
			{
				ret = p.Substring(idx);
			}
			return ret;
		}
		// ********************************************************
		public static string GetNameNoExt(string p)
		{
			string ret = "";
			ret = GetName(p);

			int idx = IndexOfMuitPriod(ret);
			if (idx >= 0)
			{
				ret = ret.Substring(0,idx);
			}
			return ret;
		}
		// ********************************************************
		public static string GetDir(string p)
		{
			string ret = "";
			if (p == "") return ret;
			int idx = p.LastIndexOf('\\');
			if (idx >= 0)
			{
				ret = p.Substring(0,idx);
			}
			return ret;
		}
		// ********************************************************
		public static string GetName(string p)
		{
			string ret = "";
			if (p == "") return ret;
			int idx0 = p.LastIndexOf('\\');
			if (idx0 >= 0)
			{
				ret = p.Substring(idx0 + 1);
			}
			return ret;
		}
		public static string ChangeName(string p,string newName)
		{
			string d = GetDir(p);
			string n = GetNameNoExt(p);
			string e = GetExt(p);
			return Path.Combine(d, newName + e);
		}
		// ********************************************************
		private static int IndexOfMuitPriod(string p)
		{
			int ret = -1;
			int idx1 = p.LastIndexOf('.');
			if (idx1 > 0)
			{
				ret = p.LastIndexOf('.', idx1 - 1);
				if (ret < 0) ret = idx1;
			}
			return ret;
		}
		// ********************************************************
		public static string ToWindwsPath(string p)
		{
			string ret = p;
			if (ret == "") return ret;
			ret = ret.Replace("/", "\\");
			if(ret.Length>=3)
			{
				if ((ret[0]== '\\')&& (ret[2] == '\\'))
				{
					if  ( ((ret[1]>='A')&& (ret[1] <= 'Z'))|| ((ret[1] >= 'a') && (ret[1] <= 'z')))
					{
						ret = ret.Substring(1,1).ToUpper()+":"+ret.Substring(2);
					}
				}
			}
			return ret;
		}
		// ********************************************************
		public static string ToJSPath(string p)
		{
			string ret = p;
			if (ret == "") return ret;
			ret = ret.Replace("\\", "/");
			if (ret.Length >= 3)
			{
				if ((ret[1] == ':') && (ret[2] == '/'))
				{
					if (((ret[0] >= 'A') && (ret[0] <= 'Z')) || ((ret[0] >= 'a') && (ret[0] <= 'z')))
					{
						ret = "/"+ret.Substring(0, 1).ToLower() + ret.Substring(2);
					}
				}
			}
			return ret;
		}
	}

}
