using System.IO;

namespace BRY
{
	/// <summary>
	/// Argsクラスが扱うデータ
	/// </summary>
	public class Param
	{
		private int m_Index = 0;
		/// <summary>
		/// パラメータのインデックス番号
		/// </summary>
		public int Index { get { return m_Index; } }
		private string m_Arg = "";
		/// <summary>
		/// パラメータ文字
		/// </summary>
		public string Arg { get { return m_Arg; } }
		private bool m_IsOption= false;
		/// <summary>
		/// オプションかどうか?[-|/]で始まるか
		/// </summary>
		public bool IsOption{get{ return m_IsOption; }}
		private bool m_IsPath = false;
		/// <summary>
		/// パス文字列かどうか？余り当てにならない
		/// </summary>
		public bool IsPath { get { return m_IsPath; } }
		/// <summary>
		/// オプションだったら、それを除いた文字列
		/// </summary>
		public string OptionStr
		{
			get
			{
				string ret = "";
				if(m_IsOption)
				{
					ret = m_Arg.Substring(1);
				}
				return ret;
			}
		}
		// *******************************************************
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="s">Arg</param>
		/// <param name="idx">index</param>
		public Param(string s,int idx)
		{
			m_Arg = s;
			m_Index = idx;
			if (m_Arg != "")
			{
				m_IsOption = ((m_Arg[0] == '-') || (m_Arg[0] == '/'));
				// Path文字列か調べる
				if(m_IsOption==false)
				{
					if(s.Length>3)
					{
						//とりあえず/は\に直しておく
						string ps = m_Arg.Replace('/', '\\');
						// js形式か
						string dd = ps.Substring(1, 1).ToUpper();
						if ((ps[0]=='/')&& (ps[2] == '/') && ((dd[0] >='A')&& (dd[0] <= 'Z')))
						{
							m_IsPath = true;
							ps = String.Format("/{0}{1}",dd,ps[2..]);
						}
						if(m_IsPath==false)
						{
							dd = s.Substring(0, 1).ToUpper();
							if ((s[1] == ':') && (s[2] == '\\') && ((dd[0] >= 'A') && (dd[0] <= 'Z')))
							{
								m_IsPath = true;
							}
						}
						if (m_IsPath == false)
						{
							if((ps.Substring(0,2)==".\\")|| (ps.Substring(0, 3) == "..\\"))
							{
								m_IsPath = true;
							}
						}
						if (m_IsPath == false)
						{
							if (ps[0]=='~')
							{
								m_IsPath = true;
							}
						}
						if (m_IsPath == false)
						{
							if (Path.GetExtension(ps)!="")
							{
								m_IsPath = true;
							}
						}
						if(m_IsPath==true)
						{
							m_Arg = ps;
						}
					}
				}
			}

		}
		// *******************************************************

	}

	/// <summary>
	/// コマンド文字配列を解析するクラス
	/// </summary>
	public class Args
	{
		// ********************************************************************
		/// <summary>
		/// Argをクラスにした物
		/// </summary>
		public Param[] Params = new Param[0];
		/// <summary>
		/// オプションのインデックステーブル
		/// </summary>
		private int [] m_IndexTbl = new int[0];
		/// <summary>
		/// オプションの数
		/// </summary>
		public int OptionCount { get { return m_IndexTbl.Length; } }
		/// <summary>
		/// Argsの個数
		/// </summary>
		public int ParamsCount { get { return Params.Length; } }
		public Param Option(int idx)
		{
			Param ret = new Param("",-1);
			if ((idx >= 0) && (idx < m_IndexTbl.Length))
			{
				ret = Params[m_IndexTbl[idx]];
			}
			return ret;
		}
		/// <summary>
		/// Argsそのもの
		/// </summary>
		public string[] ParamStrings
		{
			get
			{
				string[] ret = new string[Params.Length];
				if (Params.Length > 0) {
					for (int i=0; i< Params.Length;i++)
					{
						ret[i] = Params[i].Arg;
					}
				}
				return ret;
			}
		}
		// ********************************************************************
		// ********************************************************************
		public Args(string[] args)
		{
			if(args.Length<=0) return;
			Params = new Param[args.Length];
			List<int> list = new List<int>();
			for(int i=0; i< args.Length;i++)
			{
				Params[i] = new Param(args[i], i);
				if(Params[i].IsOption)
				{
					list.Add(i);
				}
			}
			m_IndexTbl = list.ToArray();
		}
		// ********************************************************************
	}
}
  