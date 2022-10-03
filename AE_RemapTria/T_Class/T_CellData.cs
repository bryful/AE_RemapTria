using System.Configuration.Internal;
using System.Numerics;

namespace AE_RemapTria
{
	//**************************************************************
	/// <summary>
	/// セルのコマ打ちデータ
	/// </summary>
	public partial class T_CellData 
	{
		/// <summary>
		/// イベント発生フラグ。falseでイベント発生しない。
		/// </summary>
		public bool _eventFlag = true;

		public event EventHandler? ValueChanged = null;
		public event EventHandler? SelChanged = null;
		public event EventHandler? CountChanged = null;
		// ******************************************************************************
		private T_Selection m_sel = new();
		public T_Selection Selection { get { return m_sel; } }
		public bool IsTargetCell(int idx) { return  m_sel.IsTargerCell(idx); }
		public bool IsSelectedFrame(int f) { return m_sel.IsSelectedFrame(f); }
		public bool IsSelected(int c,int f) { return m_sel.IsSelected(c,f); }
		// ******************************************************************************
		/// <summary>
		/// 抜きセル用のフラグ配列
		/// </summary>
		private T_CellLayer m_FrameEnabled = new T_CellLayer(1,true);
		public bool EnableFrame(int f) { return m_FrameEnabled.Enable(f); }
		/// <summary>
		/// セルデータ
		/// </summary>
		private T_CellLayer[] m_cells = new T_CellLayer[1];
		// ******************************************************************************
		private string m_Info = "";
		public string Info
		{
			get { return m_Info; }
		}
		/// <summary>
		/// セルのレイヤ―数
		/// </summary>
		public int CellCount { get { return m_cells.Length; } set { SetCellCount(value); } }
		/// <summary>
		/// フレーム数（物理的な）
		/// </summary>
		public int FrameCount { get { return m_cells[0].FrameCount; } set { SetFrameCount(value); } }
		/// <summary>
		/// EnabledFrameを考慮したフレーム数
		/// </summary>
		public int FrameCountTrue { get { return m_FrameEnabled.FrameCountTrue; } }
		public T_CellLayer CellLayer(int c) { return m_cells[c]; }
		/// <summary>
		/// セル名の配列
		/// </summary>
		public string[] Captions {
			get
			{
				string[]ret =new string[CellCount];
				for (int i = 0; i < CellCount; i++)
					ret[i] = m_cells[i].Caption;
				return ret; 
			} 
		}
		/// <summary>
		/// セル名をインデックスで獲得
		/// </summary>
		/// <param name="c">セルのインデックス</param>
		/// <returns>セルの名前</returns>
		public string Caption(int c) 
		{
			string ret = "";
			if((c>=0)&&(c<CellCount))
			{
				ret = m_cells[c].Caption;
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
		/*
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
		*/
		// ******************************************************
		private T_Fps m_FrameRate = T_Fps.FPS24;
		public T_Fps FrameRate
		{
			get { return m_FrameRate; }
			set { m_FrameRate = value; OnValueChanged(new EventArgs()); }
		}
		// ******************************************************
		private T_PageSec m_PageSec = T_PageSec.sec6;
		private T_FrameDisp m_FrameDisp = T_FrameDisp.pageFrame;

		public string TITLE { get; set; }
		public string SUB_TITLE { get; set; }
		public string OPUS { get; set; }
		public string SCECNE { get; set; }
		public string CUT { get; set; }
		public string CAMPANY_NAME { get; set; }

		// ******************************************************
		private int m_StartDispFrame = 1;
		/// <summary>
		/// 表示フレーム数のスタート番号
		/// </summary>
		public int StartDispFrame 
		{ 
			get { return m_StartDispFrame; } 
			set { m_StartDispFrame = value;OnValueChanged(new EventArgs()); }
		}

		// ******************************************************
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
		protected virtual void OnCountChanged(EventArgs e)
		{
			if (_eventFlag == false) return;
			if (CountChanged != null)
			{
				CountChanged(this, e);
			}
		}
		// ******************************************************
		public void InitSize(int cc,int fc)
		{
			if (cc < 10) cc = 10;
			if (fc < 6) fc = 6;
			m_FrameEnabled = new T_CellLayer(fc,true);
			m_cells = new T_CellLayer[cc];
			for (int i = 0; i < cc; i++)
			{
				m_cells[i] = new T_CellLayer(fc, Char.ConvertFromUtf32('A' + i));
			}
			CalcInfo();
		}
		// ******************************************************
		public void SetFrameCount(int fc)
		{
			if (fc <= 6) fc = 6;
			if (m_cells[0].FrameCount != fc)
			{
				PushUndo(BackupSratus.All);
				int cc = CellCount;
				int oldfc = FrameCount;

				for (int i = 0; i < cc; i++)
				{
					m_cells[i].SetFrameCount(fc);
				}
				m_FrameEnabled.SetFrameCount(fc);
				CalcInfo();
				OnCountChanged(new EventArgs());
			}
		}
		// ******************************************************
		public void SetCellCount(int cc)
		{
			if (cc <= 10) cc = 10;
			if (m_cells.Length != cc)
			{
				PushUndo(BackupSratus.All);
				int fc = FrameCount;
				int oldcc = CellCount;
				Array.Resize(ref m_cells, cc);

				if (cc > oldcc)
				{
					for (int i = oldcc; i < cc; i++)
					{
						m_cells[i] = new T_CellLayer(fc, Char.ConvertFromUtf32('A'+i)); ;
					}
				}
				OnCountChanged(new EventArgs());
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
			OnValueChanged(new EventArgs());

		}
		// ******************************************************
		public int[] EnableFrames()
		{

			return m_FrameEnabled.Values(Selection); 
		}
		public void SetFrameEnabled(int[]bb)
		{
			m_FrameEnabled.SetValues(Selection, bb);
		}
		// ******************************************************
		/*
		private void CalcFrameEnabled()
		{
			int ret = 0;
			for(int i=0; i<m_FrameEnabled.FrameCount;i++)
			{
				if (m_FrameEnabled.Enable(i)==true)
				{
					ret++;
				}
			}
			m_FrameCountTrue = ret;
			CalcInfo();
		}
		*/
		private void CalcInfo()
		{
			m_Info = string.Format("{0}+{1}:{2}",
				FrameCountTrue / (int)m_FrameRate,
				FrameCountTrue % (int)m_FrameRate,
				(int)m_FrameRate
				);
		}
		// ******************************************************
		public bool InitFrameEnabled()
		{
			PushUndo(BackupSratus.FrameEnabled);
			m_FrameEnabled.Init();
			CalcInfo();
			return true;
		}
		// ******************************************************
		/*
		public bool SetFrameEnabled(bool b)
		{
			PushUndo(BackupSratus.FrameEnabled);
			int v = 0;
			if (b == true) v = 1;
			m_FrameEnabled.SetValues(Selection, v);
			m_FrameEnabled.CalcEnableFrame();
			CalcInfo();
			OnCountChanged(new EventArgs());
			return true;
		}*/
		// ******************************************************
		public bool ToggleFrameEnabled()
		{
			PushUndo(BackupSratus.FrameEnabled);
			for (int i = 0; i < Selection.Length; i++)
			{
				int f = Selection.Start + i;
				if ((f >= 0) && (f < m_FrameEnabled.FrameCount))
				{
					m_FrameEnabled.SetEnable(f, !m_FrameEnabled.Enable(f));
				}
			}
			m_FrameEnabled.CalcEnableFrame();
			CalcInfo ();
			OnCountChanged(new EventArgs());
			return true;
		}
		// ******************************************************
		public void SetFrameEnabled(bool b)
		{
			PushUndo(BackupSratus.FrameEnabled);
			for (int i = 0; i < Selection.Length; i++)
			{
				int f = Selection.Start + i;
				if ((f >= 0) && (f < m_FrameEnabled.FrameCount))
				{
					m_FrameEnabled.SetEnable(f,b);
				}
			}
			m_FrameEnabled.CalcEnableFrame();
			CalcInfo();
			OnCountChanged(new EventArgs());
		}
		// ******************************************************
		public CellSatus GetCellStatus(int c, int f)
		{
			CellSatus cs = new CellSatus();
			cs.Init();
			if ((c >= 0) && (c < CellCount))
			{
				int v = m_cells[c].Value(f);
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
					if(v == m_cells[c].Value(f-1))
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
		public void SetTarget(int c)
		{
			int cc = c;
			if (cc < 0) cc = 0;
			else if (cc >= m_cells.Length) cc = m_cells.Length-1;
			if( cc != m_sel.Target)
			{
				PushUndo(BackupSratus.SelectionChange);
				m_sel.SetTarget(cc);
				OnSelChanged(new EventArgs());
			}
		}
		// ******************************************************
		public void SetStart(int f)
		{
			int ff = f;
			if (ff < 0) ff = 0;
			else if (ff >= m_cells[0].FrameCount) ff = m_cells[0].FrameCount;
			if (ff != m_sel.Start)
			{
				PushUndo(BackupSratus.SelectionChange);
				m_sel.SetStart(ff);
				OnSelChanged(new EventArgs());
			}
		}
		// ******************************************************
		public void ClearAll()
		{
			PushUndo(BackupSratus.All);
			for (int c = 0; c < m_cells.Length; c++) m_cells[c].Init();
			OnValueChanged(new EventArgs());

		}
		// ******************************************************
		public void Clear(int c)
		{
			if((c>=0) && (c < CellCount))
			{
				m_cells[c].Init();
				OnValueChanged(new EventArgs());
			}

		}
		/*
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
		*/
		// ******************************************************
		public int[] GetCellNum()
		{
			return m_cells[Selection.Target].Values(Selection);
		}
		// ******************************************************
		/// <summary>
		/// 選択範囲に選択範囲の前のフレームと同じ数値を入れる
		/// </summary>
		public void SetCellNumSame()
		{
			if (_undePushFlag == true) PushUndo(BackupSratus.NumberInput);

			int sv = m_sel.Start-1;
			if (sv < 0) sv = 0;
			int v = m_cells[m_sel.Target].Value(sv);
			if ((sv == 0) && (v <= 0)) v = 1;

			m_cells[Selection.Target].SetValues(Selection, v);
			m_sel.MoveDown();
			OnValueChanged(new EventArgs());

		}
		public void SetCellNumInc()
		{
			if (_undePushFlag == true) PushUndo(BackupSratus.NumberInput);
			//int fc = m_data[0].Length;

			int sv = m_sel.Start - 1;
			if (sv < 0) sv = 0;
			int v = m_cells[m_sel.Target].Value(sv);
			v += 1;
			m_cells[Selection.Target].SetValues(Selection, v);
			m_sel.MoveDown();
			OnValueChanged(new EventArgs());

		}

		public void SetCellNumDec()
		{
			if (_undePushFlag == true) PushUndo(BackupSratus.NumberInput);
			int sv = m_sel.Start - 1;
			if (sv < 0) sv = 0;
			int v = m_cells[m_sel.Target].Value(sv);
			v -= 1;
			if (v < 0) v = 0;
			m_cells[Selection.Target].SetValues(Selection, v);
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
			m_cells[Selection.Target].SetValues(Selection, v);
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
			m_cells[Selection.Target].SetValues(Selection, ints);
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
