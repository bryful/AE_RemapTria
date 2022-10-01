using AE_RemapTria;
using System.Configuration.Internal;
using System.Numerics;

namespace AE_RemapTria
{
	public enum T_Fps
	{
		FPS24 = 24,
		FPS30 = 30
	}
	//**************************************************************
	//１ページのフレーム数
	public enum T_PageSec
	{
		sec3 = 3,
		sec6 = 6
	}
	public enum T_FrameDisp
	{
		frame = 0,
		pageFrame,
		paseSecFrame,
		SecFrame,
		Count
	}

	public enum BackupSratus
	{
		None,
		NumberInput,
		SelectionChange,
		FrameEnabled,
		All
	}

	public class BackupCellData
	{
		public T_Selection sel = new();
		public int[] ints = Array.Empty<int>();
		public int[][] aints = new int [0][];
		public string[] caps = new string[0];
		public bool[] enableds = new bool[0];
		public BackupSratus stat = BackupSratus.None;
		public BackupCellData(T_CellData cd,BackupSratus bs)
		{
			stat = bs;
			switch (bs)
			{
				case BackupSratus.All:
					this.sel = new T_Selection(sel);
					aints = cd.BackupCellData();
					caps = cd.BackupCaption();
					enableds = cd.GetFrameEnabled();
					break;
				case BackupSratus.NumberInput:
					this.sel = new T_Selection(cd.Selection);
					this.ints = cd.GetCellNum();
					break;
				case BackupSratus.SelectionChange:
					this.sel = new T_Selection(cd.Selection);
					break;
				case BackupSratus.FrameEnabled:
					this.sel = new T_Selection(cd.Selection);
					this.enableds = cd.GetFrameEnabled();
					break;
			}

		}
		public void Restore(T_CellData cd)
		{
			switch (stat)
			{
				case BackupSratus.All:
					cd.RestoreCellData(aints);
					cd.RestoreCaption(caps);
					cd.Selection.Copy(sel);
					cd.SetFrameEnabled(enableds);
					break;
				case BackupSratus.NumberInput:
					cd.Selection.Copy(sel);
					cd.SetCellNum(ints);
					break;
				case BackupSratus.SelectionChange:
					cd.Selection.Copy(sel);
					break;
				case BackupSratus.FrameEnabled:
					cd.Selection.Copy(sel);
					cd.SetFrameEnabled(enableds);
					break;
			}
		}
	}
	public enum CellType
	{
		None,
		Normal,
		SameAsBefore,
		EmptyStart
	}

	public struct CellSatus
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

	public partial class T_CellData 
	{
		public bool _eventFlag = true;
		public bool _undePushFlag = true;

		public event EventHandler? ValueChanged = null;
		public event EventHandler? SelChanged = null;

		private List<BackupCellData> m_BackupCells = new List<BackupCellData>();
		private T_Selection m_sel = new();
		public T_Selection Selection { get { return m_sel; } }
		public bool IsTargetCell(int idx) { return  m_sel.IsTargerCell(idx); }
		public bool IsSelectedFrame(int f) { return m_sel.IsSelectedFrame(f); }
		public bool IsSelected(int c,int f) { return m_sel.IsSelected(c,f); }

		public bool EnabledFrame(int f) 
		{
			bool ret = true;
			if((f>=0)&&(f<m_FrameEnabled.Length))
			{
				ret = m_FrameEnabled[f];
			}
			return ret;
		}
		private int[][] m_data = new int[1][];
		private bool[] m_FrameEnabled = new bool[1];
		private string[] m_Caption = new string[1];
		public int CellCount { get { return m_data.Length; } set { SetCellCount(value); } }
		public int FrameCount { get { return m_data[0].Length; } set { SetFrameCount(value); } }
		private int m_FrameCountTrue = 1;
		public int FrameCountTrue { get { return m_FrameCountTrue; } }
		public int[] Cell(int c) { return m_data[c]; }
		public string[] Captions { get { return m_Caption; } }
		public int[][] BackupCellData()
		{
			int[][] ret = new int[CellCount][];
			for (int i = 0; i< CellCount; i++)
			{
				ret[i] = new int[FrameCount];
				for (int j = 0; j < FrameCount; j++)
				{
					ret[i][j] = m_data[i][j];
				}
			}
			return ret;
		}
		public void RestoreCellData(int[][] d)
		{
			if (d.Length <= 0) return;
			if (d[0].Length <= 0) return;
			bool b = _undePushFlag;
			_undePushFlag = false;
			SetCellCount(d.Length);
			SetFrameCount(d[0].Length);
			for(int i = 0; i < d.Length; i++)
			{
				for (int j = 0; j < d[i].Length; j++)
				{
					m_data[i][j] = d[i][j];
				}
			}

			_undePushFlag = b;
		}
		public string[] BackupCaption()
		{
			string[] ret = new string[CellCount];
			for (int i = 0; i < CellCount; i++)
			{
				ret[i] = m_Caption[i];
			}
			return ret;
		}
		public void RestoreCaption(string[] c)
		{
			int cl = c.Length;
			if (cl <= 0) return;
			if(CellCount!=cl)
			{
				bool b= _undePushFlag;
				_undePushFlag = false;
				SetCellCount(cl);
				_undePushFlag = b;
			}
			for(int i=0; i<cl;i++)
			{
				m_Caption[i] = c[i];
			}
		}
		public string Caption(int c) 
		{
			string ret = "";
			if((c>=0)&&(c<m_Caption.Length))
			{
				ret = m_Caption[c];
			}
			return ret;
		}
		public int TargetIndex
		{
			get { return m_sel.Target; }
			set 
			{
				int v = value;
				if (v < 0) v = 0;
				else if (v >= CellCount) v = CellCount - 1;

				if (m_sel.Target != v)
				{
					PushUndo(BackupSratus.SelectionChange);
					m_sel.Target = v;
					OnSelChanged(new EventArgs());
				}
			}
		}
		public int[] TargetCell 
		{
			get { return m_data[m_sel.Target]; }
			set 
			{
				int cnt = value.Length;
				if (cnt > m_data[0].Length) cnt = m_data[0].Length;
				PushUndo(BackupSratus.NumberInput);
				for(int i=0;i<cnt;i++)
				{
					m_data[m_sel.Target][i] = value[i];
				}
				m_sel.Start = 0;
				m_sel.Length= cnt;
				OnValueChanged(new EventArgs());
			}
		}
		// ******************************************************
		private T_Fps m_FrameRate = T_Fps.FPS24;
		public T_Fps FrameRate
		{
			get { return m_FrameRate; }
			set { m_FrameRate = value; OnValueChanged(new EventArgs()); }
		}

		private T_PageSec m_PageSec = T_PageSec.sec6;
		private T_FrameDisp m_FrameDisp = T_FrameDisp.pageFrame;

		private int m_StartDispFrame = 1;
		public int StartDispFrame 
		{ 
			get { return m_StartDispFrame; } 
			set { m_StartDispFrame = value;OnValueChanged(new EventArgs()); }
		}

		public string SheetName = "";
		public string FileName = "";

		public DateTime CREATE_TIME = new DateTime(1963, 9, 9);
		public DateTime UPDATE_TIME = new DateTime(1963, 9, 9);
		// ******************************************************
		public T_CellData()
		{
			_undePushFlag = false;
			m_sel.SetCellData(this);
			InitSize(12, 72);
			_undePushFlag = true;
		}
		// ******************************************************
		protected virtual void OnValueChanged(EventArgs e)
		{
			if (_eventFlag == false) return;
			if (ValueChanged != null)
			{
				ValueChanged(this, e);
			}
		}
		// ******************************************************
		protected virtual void OnSelChanged(EventArgs e)
		{
			if (_eventFlag == false) return;
			if (SelChanged != null)
			{
				SelChanged(this, e);
			}
		}
		// ******************************************************
		public void InitSize(int cc,int fc)
		{
			if (cc < 10) cc = 10;
			if (fc < 6) fc = 6;
			m_Caption = new string[cc];
			for(int i=0; i<cc;i++)
			{
				m_Caption[i] = Char.ConvertFromUtf32('A' + i);
			}
			m_FrameEnabled = new bool[fc];
			for (int i = 0; i < fc; i++)
			{
				m_FrameEnabled[i] = true;
			}
			m_FrameCountTrue = fc;
			m_data = new int[cc][];
			for (int i = 0; i < cc; i++)
			{
				m_data[i] = new int[fc];
				for(int j = 0; j < fc; j++)
				{
					m_data[i][j] = 0;
				}
			}
			CalcFrameEnabled();
		}
		// ******************************************************
		public void SetFrameCount(int fc)
		{
			if (fc <= 6) fc = 6;
			if (m_data[0].Length != fc)
			{
				PushUndo(BackupSratus.All);
				int cc = CellCount;
				int oldfc = FrameCount;

				for (int i = 0; i < cc; i++)
				{
					Array.Resize(ref m_data[i], fc);
					if (fc > oldfc)
					{
						for (int j = oldfc; j < fc; j++)
						{
							m_data[i][j] = 0;
						}
					}
				}
				Array.Resize(ref m_FrameEnabled, fc);
				if (fc > oldfc)
				{
					for (int j = oldfc; j < fc; j++)
					{
						m_FrameEnabled[j] = true;
					}
				}
				CalcFrameEnabled();
				OnValueChanged(new EventArgs());
			}
		}
		// ******************************************************
		public void SetCellCount(int cc)
		{
			if (cc <= 10) cc = 10;
			if (m_data.Length != cc)
			{
				PushUndo(BackupSratus.All);
				int fc = FrameCount;
				int oldcc = CellCount;
				Array.Resize(ref m_data, cc);
				Array.Resize(ref m_Caption, cc);

				if (cc > oldcc)
				{
					char c = m_Caption[oldcc - 1][0];
					for (int i = oldcc; i < cc; i++)
					{
						for (int j = 0; j < fc; j++)
						{
							m_data[i][j] = 0;
						}
						m_Caption[i] = Char.ConvertFromUtf32(c + i + 1);
					}
				}
				OnValueChanged(new EventArgs());
			}
		}
		// ******************************************************
		public void SetCellFrame(int c,int f)
		{
			bool b = _eventFlag;
			_eventFlag = false;
			bool b2 = _undePushFlag;
			_undePushFlag = false;
			PushUndo(BackupSratus.All);
			SetCellCount(c);
			SetFrameCount(f);
			_eventFlag = b;
			_undePushFlag = b2;
		}
		// ******************************************************
		public bool[] GetFrameEnabled()
		{
			bool[] ret = new bool[Selection.Length];
			for(int i = 0; i < Selection.Length; i++)
			{
				int f = Selection.Start + i;
				if (f < 0) f = 0;
				else if (f < m_FrameEnabled.Length) f = m_FrameEnabled.Length - 1;
				ret[i] = m_FrameEnabled[f];
			}
			return ret;
		}
		public void SetFrameEnabled(bool[]bb)
		{
			int l = bb.Length;
			if (l > Selection.Length) l = Selection.Length;
			for (int i = 0; i < l; i++)
			{
				int f = Selection.Start + i;
				if ((f >= 0) && (f < m_FrameEnabled.Length))
				{
					m_FrameEnabled[f] = bb[i];
				}
			}
		}
		// ******************************************************
		private void CalcFrameEnabled()
		{
			int ret = 0;
			for(int i=0; i<m_FrameEnabled.Length;i++)
			{
				if (m_FrameEnabled[i]==true)
				{
					ret++;
				}
			}
			m_FrameCountTrue = ret;

		}
		// ******************************************************
		public bool InitFrameEnabled()
		{
			PushUndo(BackupSratus.FrameEnabled);
			for (int i = 0; i < m_FrameEnabled.Length; i++)
			{
				m_FrameEnabled[i] = true;
			}
			m_FrameCountTrue = m_FrameEnabled.Length;
			return true;
		}
		// ******************************************************
		public bool SetFrameEnabled(bool b)
		{
			PushUndo(BackupSratus.FrameEnabled);
			for (int i=0; i<Selection.Length;i++)
			{
				int f = Selection.Start + i;
				if((f>=0) && (f<m_FrameEnabled.Length))
				{
					m_FrameEnabled[f] = b;
				}
			}
			CalcFrameEnabled();
			OnValueChanged(new EventArgs());
			return true;
		}
		// ******************************************************
		public bool ToggleFrameEnabled()
		{
			PushUndo(BackupSratus.FrameEnabled);
			for (int i = 0; i < Selection.Length; i++)
			{
				int f = Selection.Start + i;
				if ((f >= 0) && (f < m_FrameEnabled.Length))
				{
					m_FrameEnabled[f] = !m_FrameEnabled[f];
				}
			}
			CalcFrameEnabled();
			OnValueChanged(new EventArgs());
			return true;
		}
		// ******************************************************
		public int GetCellData(int c, int f)
		{
			int ret = -1;
			if((c>=0)&&(c< CellCount)&& (f >= 0) && (f < FrameCount))
			{
				ret = m_data[c][f];
			}
			return ret;
		}
		// ******************************************************
		public CellSatus GetCellStatus(int c, int f)
		{
			CellSatus cs = new CellSatus();
			cs.Init();
			if ((c >= 0) && (c < CellCount) && (f >= 0) && (f < FrameCount))
			{
				int v = m_data[c][f];
				cs.Cell = c;
				cs.Frame = f;
				cs.Value = v;
				if(f== 0)
				{
					if (v == 0)
					{
						cs.Status = CellType.EmptyStart;
					}
					else
					{
						cs.Status = CellType.Normal;
					}
				}
				else
				{
					if(v == m_data[c][f - 1])
					{
						if (v == 0)
						{
							cs.Status = CellType.None;
						}
						else
						{
							cs.Status = CellType.SameAsBefore;
						}
					}
					else
					{
						if (v == 0)
						{
							cs.Status = CellType.EmptyStart;
						}
						else
						{
							cs.Status = CellType.Normal;
						}

					}
				}
			}
			return cs;
		}
		// ******************************************************
		public void SetCellData(int c, int f, int v)
		{
			if ((c >= 0) && (c < CellCount) && (f >= 0) && (f < FrameCount))
			{
				m_data[c][f] = v;
				OnValueChanged(new EventArgs());
			}
		}
		// ******************************************************
		public void SetTarget(int c)
		{
			int cc = c;
			if (cc < 0) cc = 0;
			else if (cc >= m_data.Length) cc = m_data.Length;
			if( cc != m_sel.Target)
			{
				PushUndo(BackupSratus.SelectionChange);
				m_sel.SetTarget(cc);
				OnSelChanged(new EventArgs());
			}
		}
		// ******************************************************
		public void ClearAll()
		{
			PushUndo(BackupSratus.All);
			int cc = m_data.Length;
			int fc = m_data[0].Length;
			for (int c = 0; c < cc; c++)
				for (int f =0; f < fc; f++)
					m_data[c][f] = 0;
			OnValueChanged(new EventArgs());

		}
		// ******************************************************
		public void Clear(int c)
		{
			if((c>=0) && (c < CellCount))
			{
				int fc = m_data[0].Length;
				for (int f = 0; f < fc; f++) m_data[c][f] = 0;
				OnValueChanged(new EventArgs());
			}

		}
		// ******************************************************
		public int[] CopyData()
		{
			int st = m_sel.StartTrue;
			int ed = m_sel.LastIndex;
			int[] ret = new int[m_sel.LengthTrue];
			for (int i=0; i< ret.Length;i++)
			{
				ret[i] = m_data[m_sel.Target][st + i];
			}
			return ret;
		}
		// ******************************************************
		public void PasteData(int[] d)
		{
			int st = m_sel.StartTrue;
			int ed = m_sel.LastIndex;
			int len = ed - st + 1;
			if (len > d.Length) len = d.Length;
			for (int i = 0; i < d.Length; i++)
			{
				m_data[m_sel.Target][st + i] = d[i];
			}
			OnValueChanged(new EventArgs());
		}
		// ******************************************************
		public int[] GetCellNum()
		{

			int[] ret = new int[m_sel.Length];

			int fc = m_data[0].Length;
			for(int i=0; i< m_sel.Length;i++)
			{
				int idx = m_sel.Start + i;
				if (idx < 0) idx = 0;
				else if(idx>=fc) idx = fc-1;
				ret[i] = m_data[m_sel.Target][idx];
			}
			return ret;

		}
		// ******************************************************
		/// <summary>
		/// 選択範囲に選択範囲の前のフレームと同じ数値を入れる
		/// </summary>
		public void SetCellNumSame()
		{
			if (_undePushFlag == true) PushUndo(BackupSratus.NumberInput);


			int fc = m_data[0].Length;
			int sv = m_sel.Start-1;
			if (sv < 0) sv = 0;
			int v = m_data[m_sel.Target][sv];
			if ((sv == 0) && (v == 0)) v = 1;
			for (int i = 0; i < m_sel.Length; i++)
			{
				int idx = m_sel.Start + i;
				if ((idx >= 0) && (idx < fc))
				{
					m_data[m_sel.Target][idx] = v;
				}
			}
			m_sel.MoveDown();
			OnValueChanged(new EventArgs());

		}
		public void SetCellNumInc()
		{
			if (_undePushFlag == true) PushUndo(BackupSratus.NumberInput);
			int fc = m_data[0].Length;
			int sv = m_sel.Start - 1;
			if (sv < 0) sv = 0;
			int v = m_data[m_sel.Target][sv]+1;
			for (int i = 0; i < m_sel.Length; i++)
			{
				int idx = m_sel.Start + i;
				if ((idx >= 0) && (idx < fc))
				{
					m_data[m_sel.Target][idx] = v;
				}
			}
			m_sel.MoveDown();
			OnValueChanged(new EventArgs());

		}

		public void SetCellNumDec()
		{
			if (_undePushFlag == true) PushUndo(BackupSratus.NumberInput);
			int fc = m_data[0].Length;
			int sv = m_sel.Start - 1;
			if (sv < 0) sv = 0;
			int v = m_data[m_sel.Target][sv] - 1;
			if (v < 0) v = 0;
			for (int i = 0; i < m_sel.Length; i++)
			{
				int idx = m_sel.Start + i;
				if ((idx >= 0) && (idx < fc))
				{
					m_data[m_sel.Target][idx] = v;
				}
			}
			m_sel.MoveDown();
			OnValueChanged(new EventArgs());

		}
		// ******************************************************
		/// <summary>
		/// 選択範囲にセル番号を入力
		/// </summary>
		/// <param name="v"></param>
		public void SetCellNum(int v,bool IsMove =true)
		{
			if (v < 0) v = 0;
			if (_undePushFlag == true) PushUndo(BackupSratus.NumberInput);
			int fc = m_data[0].Length;
			for (int i = 0; i < m_sel.Length; i++)
			{
				int idx = m_sel.Start + i;
				if ((idx >= 0) && (idx < fc))
				{
					m_data[m_sel.Target][idx] = v;
				}
			}
			if (IsMove)
			{
				m_sel.MoveDown();
			}
			OnValueChanged(new EventArgs());

		}
		// ******************************************************
		public void SetCellNumEmpty(bool IsMove=true)
		{
			bool b = true;
			if(IsMove==false)
			{
				b = false;
				int[] v = GetCellNum();
				for (int i = 0; i < v.Length; i++)
				{
					if(v[i] != 0)
					{
						b = true;
						break;
					}
				}
			}
			if (b) SetCellNum(0,IsMove);
		}
		// ******************************************************
		public void SetCellNumBS()
		{
			if (Selection.MoveUp())
			{
				SetCellNumEmpty(false);
			}
		}
		/// <summary>
		/// 選択範囲にセル番号配列を入力
		/// 要素数が元の選択範囲より多ければ選択範囲を拡張
		/// </summary>
		/// <param name="ints"></param>
		public void SetCellNum(int [] ints)
		{
			if(_undePushFlag==true) PushUndo(BackupSratus.NumberInput);
			int len = m_sel.Length;
			if (len > ints.Length)
			{
				m_sel.Length = len;
				len = ints.Length;
			}
			int fc = m_data[0].Length;
			for (int i = 0; i < len; i++)
			{
				int idx = m_sel.Start + i;
				if ((idx >= 0) && (idx < fc))
				{
					m_data[m_sel.Target][idx] = ints[i];
				}
			}
			OnValueChanged(new EventArgs());

		}
		// ******************************************************
		public void PushUndo(BackupSratus bs)
		{
			if (_undePushFlag == false) return;
			m_BackupCells.Add(new BackupCellData(this, bs));
			if(m_BackupCells.Count>2000)
			{
				m_BackupCells.RemoveAt(0);
			}
		}
		// ******************************************************
		public void PopUndo()
		{
			int idx = m_BackupCells.Count - 1;
			if ( idx < 0) return;
			bool b = _undePushFlag;
			bool b2 = _eventFlag;
			_undePushFlag = false;
			_eventFlag = false;
			m_BackupCells[idx].Restore(this);
			m_BackupCells.RemoveAt(idx);
			_undePushFlag = b;
			_eventFlag = b2;
			OnValueChanged(new EventArgs());
		}
		// ******************************************************
		public bool SelectionAdd(int v)
		{
			bool ret = false;
			int len = m_sel.Length + v;
			if(len>0)
			{
				if (m_sel.Start + len <= FrameCount)
				{
					PushUndo(BackupSratus.SelectionChange);
					m_sel.Length = len;
					ret = true;
					OnSelChanged(new EventArgs());
				}

			}
			return ret;
		}
		public bool SelectionAll(int c)
		{
			bool ret = false;
			if (c < 0) c = 0;
			else if (c >= CellCount) c = CellCount - 1;
			PushUndo(BackupSratus.SelectionChange);
			m_sel.Target = c;
			m_sel.Start = 0;
			m_sel.Length = FrameCount;
			OnSelChanged(new EventArgs());
			return ret;
		}
		public bool SelectionAll()
		{
			return SelectionAll(m_sel.Target);
		}
	}
}
