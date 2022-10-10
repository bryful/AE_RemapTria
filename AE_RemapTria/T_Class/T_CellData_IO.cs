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
		public void FromArray(int[][] v)
		{
			int c = v.Length;
			if (c > m_cells.Length) c = m_cells.Length;
			for (int i = 0; i < c; i++)
			{
				m_cells[i].SetAllValues(v[i]);
			}
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
		// ******************************************************
		public int[][] RawData
		{
			get
			{
				int[][] ret = new int[CellCount][];
				for(int i=0; i<CellCount;i++)
				{
					ret[i] = m_cells[i].RawData(m_FrameEnabled);
				}
				return ret;
			}
		}
		// ******************************************************
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
		static public string FileTypeOf(string p)
		{
			string n = Path.GetFileNameWithoutExtension(p);
			string e = Path.GetExtension(p);
			string e2 = Path.GetExtension(n);
			if(e2!="")
			{
				n = n.Substring(0, n.Length - e.Length);
				e = e2 + e;
			}
			return e;
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
					s+= m_cells[Selection.Target].Value(idx).ToString()+"\r\n";
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
					string[] sa = s.Trim().Split("\r\n");
					if (sa.Length <= 1) return ret;
					if (sa[0].Trim() != ClipHeader) return ret;
					int[] ints = new int[sa.Length - 1];
					for (int i = 0; i < sa.Length - 1; i++)
					{
						ints[i] = int.Parse(sa[i + 1]);
					}
					PushUndo(BackupSratus.SelectionChange);
					Selection.Length = ints.Length;
					PushUndo(BackupSratus.NumberInput);
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
		public bool FromCommand(string s)
		{
			bool ret = false;
			string[] sa = s.Split('_');
			if (sa.Length < 3) return ret;
			int f = 0;
			try
			{
				f = int.Parse(sa[0]);
				if (FrameCountTrue != f)
				{
					MessageBox.Show("Missmatch Duration!");
					return ret;
				}
			}
			catch
			{
				return ret;
			}
			try
			{
				int fr = int.Parse(sa[1]);
				if (fr != (int)FrameRate)
				{
					MessageBox.Show("Missmatch FrameRate!");
					return ret;
				}
			}
			catch
			{
				return ret;
			}
			try
			{
				List<int[]> list = new List<int[]>();
				for (int i = 2; i < sa.Length; i++)
				{
					string[] sa2 = sa[i].Split('-');
					if (sa2.Length >= 2)
					{
						int[] v = new int[2];
						v[0] = int.Parse(sa2[0]);
						v[1] = int.Parse(sa2[1]);
						list.Add(v);
					}
				}
				m_cells[Selection.Target].FromArray(list.ToArray());
				ret = true;
			}
			catch
			{
				return ret;
			}
			return ret;
		}
	}

}
