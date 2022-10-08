using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Configuration.Internal;
using System.Numerics;
namespace AE_RemapTria
{
	public partial class T_CellData
	{

		// ******************************************************
		public int GetCellData(int c, int f)
		{
			int ret = -1;
			if ((c >= 0) && (c < CellCount))
			{
				ret = m_cells[c].Value(f);
			}
			return ret;
		}
		// ******************************************************
		public void SetCellData(int c, int f, int v)
		{
			if ((c >= 0) && (c < CellCount) && (f >= 0) && (f < FrameCountTrue))
			{
				m_cells[c].SetValue(f, v);
				OnValueChanged(new EventArgs());
			}
		}
		public int[][] ToArray(int c)
		{
			return m_cells[c].ToArray();
		}
		public int[][] ToArrayFromTarget()
		{
			return m_cells[Selection.Target].ToArray();
		}
		// ******************************************************
		public int[][][] Cell
		{
			get
			{
				int[][][] ret = new int[CellCount][][];
				for (int i = 0; i < CellCount; i++)
				{
					ret[i] = m_cells[i].ToArray(m_FrameEnabled);
				}
				return ret;
			}
			set
			{
				SetCell(value);
			}
		}
		public void SetCell(int[][][] v)
		{
			if (v == null) return;
			m_FrameEnabled.Init();
			int c = v.Length;
			if (c <= 0) return;
			if (c > CellCount) c = CellCount;

			try
			{
				for (int i = 0; i < c; i++)
				{
					int[][] vv = v[i];
					m_cells[i].FromArray(vv);
				}
			}
			catch( Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}
		// ******************************************************
		public int[][][] CellWithEnabled
		{
			get
			{
				int[][][] ret = new int[CellCount+1][][];
				ret[0] = m_FrameEnabled.ToArray();
				for (int i = 0; i < CellCount; i++)
				{
					ret[i+1] = m_cells[i].ToArray();
				}
				return ret;
			}
			set
			{
				int c = value.Length-1;
				if(c <= 0) return;
				if(c> CellCount) c = CellCount;
				m_FrameEnabled.FromArray(value[0]);
				for (int i = 0; i < c; i++)
				{
					m_cells[i].FromArray(value[i+1]);
				}
			}
		}
		// ******************************************************
		/// <summary>
		/// セル名の配列
		/// </summary>
		public string[] Caption
		{
			get
			{
				string[] ret = new string[CellCount];
				for (int i = 0; i < CellCount; i++)
					ret[i] = m_cells[i].Caption;
				return ret;
			}
			set
			{
				int c = value.Length;
				if (c > CellCount) c = CellCount;
				for (int i = 0; i < c; i++)
					m_cells[i].Caption = value[i];
			}
		}
		public bool SaveToArdj(string p)
		{
			T_Ardj t_Ardj = new T_Ardj(this);
			return t_Ardj.Save(p);
		}
		public string ToArdj()
		{
			T_Ardj t_Ardj = new T_Ardj(this);
			return t_Ardj.ToArdj();
		}
		public bool LoadFromArdj(string p)
		{
			T_Ardj t_Ardj = new T_Ardj(this);
			return t_Ardj.Load(p);
		}
		public bool Save(string p)
		{
			bool ret = false;
			if(p!="")
			{
				ret= SaveToArdj(p);
			}
			return ret;
		}
		public bool Load(string p)
		{
			bool ret = false;
			if (p != "")
			{
				ret =  LoadFromArdj(p);
			}
			return ret;
		}
		public const string ClipHeader = "#AF_Remap.exe clip";
		public bool Copy()
		{
			bool ret = false;
			int f = FrameCountTrue;
			string s = "";
			for (int i= 0;i<Selection.Length;i++)
			{
				int idx = i+ Selection.Start;
				if((idx>=0)&&(idx<f))
				{
					s+= m_cells[Selection.Target].Value(f).ToString()+"\r\n";
				}
			}
			if (s.Length > 0)
			{
				s = ClipHeader + "\r\n" + s;
				Clipboard.SetText(s);
				ret = true;
			}
			return ret;
		}
		public bool Cut()
		{
			bool ret = Copy();
			if (ret)
			{
				SetCellNum(0, false);
			}
			return ret;
		}
		public bool Paste()
		{
			bool ret = false;
			if (Clipboard.ContainsText() == false) return ret;
			string s = Clipboard.GetText();
			if(s.Length > 0)
			{
				try
				{
					string[] sa = s.Split("\r\n");
					if (sa.Length <= 1) return ret;
					if (sa[0].Trim() != ClipHeader) return ret;
					int[] ints = new int[sa.Length - 1];
					for (int i = 0; i < sa.Length - 1; i++)
					{
						ints[i] = int.Parse(sa[i + 1]);
					}
					PushUndo(BackupSratus.NumberInput);
					Selection.Length = ints.Length;
					for (int i = 0; i < Selection.Length; i++)
					{
						m_cells[Selection.Target].SetValue(Selection.Start + i, ints[i]);
					}
					ret = true;
				}
				catch
				{
					ret = false;
				}
			}
			return ret;
		}
	}

}
