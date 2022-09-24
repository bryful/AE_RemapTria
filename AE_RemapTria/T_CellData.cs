namespace AE_RemapTria
{
	public enum T_Fps
	{
		fps12 = 12,
		fps15 = 15,
		fps24 = 24,
		fps30 = 30
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

	public class BackupCellData
	{
		public T_Selection sel = new();
		public int[] ints = Array.Empty<int>();
		public BackupCellData(T_Selection sel, int[] ints)
		{
			this.sel = new T_Selection(sel);
			this.ints = ints;
		}
	}

	public partial class T_CellData 
	{
		public bool _eventFlag = true;
		public bool _undePushFlag = true;

		public event EventHandler? ValueChangedEvent = null;
		public event EventHandler? SelChangedEvent = null;

		private List<BackupCellData> m_BackupCells = new List<BackupCellData>();
		private T_Selection m_sel = new();
		public T_Selection Selection { get { return m_sel; } }
		public bool IsTargetCell(int idx) { return  m_sel.IsTargerCell(idx); }
		private int[][] m_data = new int[1][];
		private string[] m_Caption = new string[1];
		public int CellCount { get { return m_data.Length; } set { SetCellCount(value); } }
		public int FrameCount { get { return m_data[0].Length; } set { SetFrameCount(value); } }
		public int[] Cell(int c) { return m_data[c]; }
		public string[] Captions { get { return m_Caption; } }
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
			set { m_sel.Target = value; }
		}
		public int[] TargetCell 
		{
			get { return m_data[m_sel.Target]; }
			set 
			{
				int cnt = value.Length;
				if (cnt > m_data[0].Length) cnt = m_data[0].Length;
				for(int i=0;i<cnt;i++)
				{
					m_data[m_sel.Target][i] = value[i];
				}
			}
		}
		// ******************************************************
		private T_Fps m_FrameRate = T_Fps.fps24;
		private T_PageSec m_PageSec = T_PageSec.sec6;
		private T_FrameDisp m_FrameDisp = T_FrameDisp.pageFrame;

		public string SheetName = "";
		public string FileName = "";

		public DateTime CREATE_TIME = new DateTime(1963, 9, 9);
		public DateTime UPDATE_TIME = new DateTime(1963, 9, 9);
		// ******************************************************
		public T_CellData()
		{
			m_sel.SetCellData(this);
			InitSize(10, 24);
		}
		// ******************************************************
		protected virtual void OnValueChangedEvent(EventArgs e)
		{
			if (_eventFlag == false) return;
			if (ValueChangedEvent != null)
			{
				ValueChangedEvent(this, e);
			}
		}
		// ******************************************************
		protected virtual void OnSelChangedEvent(EventArgs e)
		{
			if (_eventFlag == false) return;
			if (SelChangedEvent != null)
			{
				SelChangedEvent(this, e);
			}
		}
		// ******************************************************
		public void InitSize(int cc,int fc)
		{
			if (cc < 10) cc = 10;
			m_Caption = new string[cc];
			for(int i=0; i<cc;i++)
			{
				m_Caption[i] = Char.ConvertFromUtf32('A' + i);
			}
			m_data = new int[cc][];
			for (int i = 0; i < cc; i++)
			{
				m_data[i] = new int[fc];
				for(int j = 0; j < fc; j++)
				{
					m_data[i][j] = 0;
				}
			}
		}
		// ******************************************************
		public void SetFrameCount(int fc)
		{
			if (fc <= 6) fc = 6;
			if (m_data[0].Length != fc)
			{
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
				OnValueChangedEvent(new EventArgs());
			}
		}
		// ******************************************************
		public void SetCellCount(int cc)
		{
			if (cc <= 10) cc = 10;
			if (m_data.Length != cc)
			{
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
				OnValueChangedEvent(new EventArgs());
			}
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
		public void SetCellData(int c, int f, int v)
		{
			if ((c >= 0) && (c < CellCount) && (f >= 0) && (f < FrameCount))
			{
				m_data[c][f] = v;
				OnValueChangedEvent(new EventArgs());
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
				m_sel.SetTarget(cc);
				OnSelChangedEvent(new EventArgs());
			}
		}
		// ******************************************************
		public void ClearAll()
		{
			int cc = m_data.Length;
			int fc = m_data[0].Length;
			for (int c = 0; c < cc; c++)
				for (int f =0; f < fc; f++)
					m_data[c][f] = 0;
			OnValueChangedEvent(new EventArgs());

		}
		// ******************************************************
		public void Clear(int c)
		{
			if((c>=0) && (c < CellCount))
			{
				int fc = m_data[0].Length;
				for (int f = 0; f < fc; f++) m_data[c][f] = 0;
				OnValueChangedEvent(new EventArgs());
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
			OnValueChangedEvent(new EventArgs());
		}
		// ******************************************************
		public int[] GetData()
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
		public void SetData(int v)
		{
			if (v < 0) v = 0;
			if (_undePushFlag == true) PushUndo();
			int fc = m_data[0].Length;
			for (int i = 0; i < m_sel.Length; i++)
			{
				int idx = m_sel.Start + i;
				if ((idx >= 0) && (idx < fc))
				{
					m_data[m_sel.Target][idx] = v;
				}
			}
			m_sel.MoveDown();
			OnValueChangedEvent(new EventArgs());

		}
		// ******************************************************
		public void SetData(int [] ints)
		{
			if(_undePushFlag==true) PushUndo();
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
			OnValueChangedEvent(new EventArgs());

		}
		// ******************************************************
		public void PushUndo()
		{
			if (_undePushFlag == false) return;
			BackupCellData bc = new BackupCellData(m_sel, GetData());
			m_BackupCells.Add(bc);
		}
		// ******************************************************
		public void PopUndo()
		{
			int idx = m_BackupCells.Count - 1;
			if ( idx < 0) return;
			bool b = _undePushFlag;
			_undePushFlag = false;
			BackupCellData bc = m_BackupCells[idx];
			m_sel.Copy(bc.sel);
			SetData(bc.ints);
			m_BackupCells.RemoveAt(idx);
			_undePushFlag = b;
			OnValueChangedEvent(new EventArgs());
		}
		// ******************************************************
	}
}
