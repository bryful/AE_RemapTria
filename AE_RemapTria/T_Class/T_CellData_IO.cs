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
using System.Dynamic;

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
		public void FromArray(int c,int[][] ary)
		{
			m_cells[c].FromArray(ary);
		}
		public void FromArray(int[][] ary)
		{
			m_cells[Selection.Target].FromArray(ary);
		}
		public void SetAllValues(int[][] v)
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
		public JsonObject RawData
		{
			get
			{
				JsonObject ret = new JsonObject();

				bool[] tbl = IsEmpties;
				int cnt = 0;
				for (int i=0; i<tbl.Length;i++) if (!tbl[i]) cnt++;
				for(int i=0; i<tbl.Length;i++)
				{
					if (!tbl[i])
					{
						int[] raw = m_cells[i].RawData(m_FrameEnabled);
						JsonArray arr = new JsonArray();
						for(int j=0; j<raw.Length;j++) arr.Add(raw[j]);
						string key = m_cells[i].Caption;
						ret[key] = arr;
					}
				}
				return ret;
			}
		}
		public string [] RawCaption
		{
			get
			{
				bool[] tbl = IsEmpties;
				int cnt = 0;
				for (int i = 0; i < tbl.Length; i++) if (!tbl[i]) cnt++;
				string[] ret = new string[cnt];
				int idx = 0;
				for (int i = 0; i < tbl.Length; i++)
				{
					if (!tbl[i])
					{
						ret[idx] = m_cells[i].Caption;
						idx++;
					}
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
		// *********************************************************************************
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
		// *********************************************************************************
		public bool[] IsEmpties
		{
			get
			{
				bool[] ret = new bool[CellCount];
				for(int i=0; i<CellCount;i++)
				{
					ret[i] = m_cells[i].IsEmpty(m_FrameEnabled);
				}
				return ret;
			}
		}
		public bool InsertCell(int idx,string cap)
		{
			if((idx<0)&&(idx>=CellCount)) return false;
			cap = cap.Trim();
			if (cap == "") return false;
			bool b = true;
			for(int i=0; i<CellCount; i++)
			{
				if (m_cells[i].Caption==cap)
				{
					b = false;
					break;
				}
			}
			if(b==false) return false;

			PushUndo(BackupSratus.All);
			bool e = _eventFlag;
			bool u = _undoPushFlag;
			SetCellCount(CellCount + 1);

			if (idx != CellCount - 1)
			{
				for (int i = CellCount - 1; i > idx; i--)
				{
					m_cells[i].Copy(m_cells[i - 1]);
				}
			}
			m_cells[idx].Init();
			m_cells[idx].Caption=cap;
			_eventFlag = e;
			_undoPushFlag = u;
			OnCountChanged(new EventArgs());
			return true;

		}
		public bool InsertCell(string cap)
		{
			return InsertCell(Selection.Target, cap);
		}
		public bool RemoveCell(int c)
		{
			if ((c < 0) && (c >= CellCount)) return false;

			PushUndo(BackupSratus.All);
			bool e = _eventFlag;
			bool u = _undoPushFlag;
			_eventFlag =false;
			_undoPushFlag=false;
			if (c< CellCount-1)
			{
				for(int i=c;i< CellCount-1;i++)
				{
					m_cells[i].Copy(m_cells[i + 1]);
				}
			}
			SetCellCount(CellCount-1);
			if(Selection.Target >=CellCount)
			{
				Selection.Target = CellCount - 1;
			}
			_eventFlag = e;
			_undoPushFlag = u;
			OnCountChanged(new EventArgs());
			return true;
		}
		public bool RemoveCell()
		{
			if (CellCount==1) return false;
			return RemoveCell(Selection.Target);
		}
		public bool InsertFrame(int start,int length)
		{
			if (start < 0)
			{
				length = length + start;
				start = 0;
			}
			else if (start + length > FrameCountTrue)
			{
				length = (start + length) - FrameCountTrue;
			}
			if ((start < 0) || (start >= FrameCountTrue) || (length <= 0)) return false;
			PushUndo(BackupSratus.All);
			bool e = _eventFlag;
			bool u = _undoPushFlag;
			_eventFlag = false;
			_undoPushFlag = false;
			m_FrameEnabled.InsertFrame(start, length);
			for (int i=0; i<CellCount; i++)
			{
				m_cells[i].InsertFrame(start, length);
			}
			_eventFlag = e;
			_undoPushFlag = u;
			CalcInfo();
			OnCountChanged(new EventArgs());
			return true;
		}
		public bool InsertFrame()
		{
			return InsertFrame(Selection.Start, Selection.Length);
		}
		public bool RemoveFrame(int start, int length)
		{
			if (start < 0)
			{
				length = length + start;
				start = 0;
			}
			else if (start + length > FrameCountTrue)
			{
				length = (start + length) - FrameCountTrue;
			}
			if ((start < 0) || (start >= FrameCountTrue) || (length <= 0)) return false;
			PushUndo(BackupSratus.All);
			bool e = _eventFlag;
			bool u = _undoPushFlag;
			_eventFlag = false;
			_undoPushFlag = false;
			m_FrameEnabled.RemoveFrame(start, length);
			for (int i = 0; i < CellCount; i++)
			{
				m_cells[i].RemoveFrame(start, length);
			}
			_eventFlag = e;
			_undoPushFlag = u;
			CalcInfo();
			OnCountChanged(new EventArgs());
			return true;
		}
		public bool RemoveFrame()
		{
			return RemoveFrame(Selection.Start, Selection.Length);
		}
	}

}
