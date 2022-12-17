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

namespace AE_RemapTria
{
	//**************************************************************
	/// <summary>
	/// セルのコマ打ちデータ
	/// </summary>
	public partial class TR_CellData 
	{
		/// <summary>
		/// イベント発生フラグ。falseでイベント発生しない。
		/// </summary>
		public bool _eventFlag = true;

		protected TR_Form m_form;
		public TR_Selection sel;
		public void SetTRForm(TR_Form fm)
		{
			m_form = fm;
			if(m_form!=null)
			{
				sel = m_form.Selection;
			}
		}
		// ******************************************************************************
		public bool IsTargetCell(int idx) { return  sel.IsTargerCell(idx); }
		public bool IsSelectedFrame(int f) { return sel.IsSelectedFrame(f); }
		public bool IsSelected(int c,int f) { return sel.IsSelected(c,f); }
		// ******************************************************************************
		/// <summary>
		/// 抜きセル用のフラグ配列
		/// </summary>
		private TR_CellLayer m_FrameEnabled = new TR_CellLayer(1,true);
		public bool EnableFrame(int f) { return m_FrameEnabled.Enable(f); }
		/// <summary>
		/// セルデータ
		/// </summary>
		private TR_CellLayer[] m_cells = new TR_CellLayer[1];
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
		public int FrameCount { get { return m_FrameEnabled.FrameCount; } set { SetFrameCount(value); } }
		/// <summary>
		/// EnabledFrameを考慮したフレーム数
		/// </summary>
		public int FrameCountTrue { get { return m_FrameEnabled.FrameCountTrue; } }
		public int UnEnabledFrameCount { get { return m_FrameEnabled.UnEnabledFrameCount; } }
		public TR_CellLayer CellLayer(int c) { return m_cells[c]; }
		/// <summary>
		/// セル名をインデックスで獲得
		/// </summary>
		/// <param name="c">セルのインデックス</param>
		/// <returns>セルの名前</returns>
		public string CaptionFromIndex(int c) 
		{
			string ret = "";
			if((c>=0)&&(c<CellCount))
			{
				ret = m_cells[c].Caption;
			}
			return ret;
		}
		public string CaptionTarget()
		{
			return CaptionFromIndex(sel.Target);
		}
		public void SetCaptionTarget(string s)
		{
			s = s.Trim();
			if (s == "") return;
			int c = sel.Target;
			if ((c >= 0) && (c < CellCount))
			{
				m_cells[c].Caption = s;
			}

		}
		public int TargetIndex
		{
			get { return sel.Target; }
			set 
			{
				int v = value;
				if (v < 0) v = 0;
				else if (v >= CellCount) v = CellCount - 1;

				if (sel.Target != v)
				{
					sel.Target = v;
				}
			}
		}
		// ******************************************************
		private T_Fps m_FrameRate = T_Fps.FPS24;
		public T_Fps FrameRate
		{
			get { return m_FrameRate; }
			set 
			{ 
				if(m_FrameRate != value)
				{
					m_FrameRate = value;
				};  
			}
		}
		// ******************************************************
		private T_PageSec m_PageSec = T_PageSec.sec6;
		public T_PageSec PageSec { 
			get { return m_PageSec; }
			set { m_PageSec = value;}
		}
		private T_FrameDisp m_FrameDisp = T_FrameDisp.pageFrame;
		public T_FrameDisp FrameDisp { get { return m_FrameDisp; } }
		public void ToggleFrameDisp()
		{
			int v = (int)m_FrameDisp + 1;
			if (v >= (int)T_FrameDisp.Count) v = (int)T_FrameDisp.frame;
			m_FrameDisp = (T_FrameDisp)v;
		}

		// ******************************************************
		private int m_StartKoma = 1;
		/// <summary>
		/// 表示フレーム数のスタート番号
		/// </summary>
		public int StartKoma 
		{ 
			get { return m_StartKoma; } 
			set {
				if (value >= 1)
				{
					m_StartKoma = 1;
				}
				else
				{
					m_StartKoma = 0;
				}
			}
		}
		private int m_OffSetFrame = 0;
		public int OffSetFrame
		{
			get { return m_OffSetFrame; }
			set
			{
				m_OffSetFrame=value;
			}
		}


		// ******************************************************
		public string SheetName = "";
		public string TITLE = "";
		public string SUB_TITLE = "";
		public string OPUS = "";
		public string SCECNE = "";
		public string CUT = "";
		public string CAMPANY_NAME = "";

		public string CREATE_USER = "";
		public string UPDATE_USER = "";

		public DateTime CREATE_TIME = new DateTime(1963, 9, 9);
		public DateTime UPDATE_TIME = new DateTime(1963, 9, 9);

		// ******************************************************
		public TR_CellData()
		{
			InitSize(12, 72);
		}


		// ******************************************************
		public void InitSize(int cc,int fc)
		{
			if (cc < 10) cc = 10;
			if (fc < 6) fc = 6;
			m_FrameEnabled = new TR_CellLayer(fc,true);
			m_cells = new TR_CellLayer[cc];
			for (int i = 0; i < cc; i++)
			{
				m_cells[i] = new TR_CellLayer(fc, Char.ConvertFromUtf32('A' + i));
			}
			CalcInfo();
		}
		// ******************************************************
		public void SetFrameCount(int fc)
		{
			if (fc <= 6) fc = 6;
			if (m_FrameEnabled.FrameCount != fc)
			{
				int tfc = fc + m_FrameEnabled.UnEnabledFrameCount;
				PushUndo(BackupSratus.All);
				int cc = CellCount;

				for (int i = 0; i < cc; i++)
				{
					m_cells[i].SetFrameCountTrue(tfc);
				}
				m_FrameEnabled.SetFrameCountTrue(tfc);
				CalcInfo();
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
						m_cells[i] = new TR_CellLayer(fc, Char.ConvertFromUtf32('A'+i)); ;
					}
				}
			}
		}
		// ******************************************************
		public void SetCellFrame(int c,int f)
		{
			bool b = _eventFlag;
			_eventFlag = false;
			bool b2 = _undoPushFlag;
			_undoPushFlag = false;
			SetCellCount(c);
			SetFrameCount(f);
			_eventFlag = b;
			_undoPushFlag = b2;

		}
		// ******************************************************
		public int[] EnableFrames()
		{

			return m_FrameEnabled.Values(sel); 
		}
		public void SetFrameEnabled(int[]bb)
		{
			m_FrameEnabled.SetValues(sel, bb);
		}
		// ******************************************************
		private void CalcInfo()
		{
			m_Info = string.Format("{0}+{1}:{2}",
				FrameCount / (int)m_FrameRate,
				FrameCount % (int)m_FrameRate,
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
		public bool ToggleFrameEnabled()
		{
			PushUndo(BackupSratus.FrameEnabled);
			for (int i = 0; i < sel.Length; i++)
			{
				int f = sel.Start + i;
				if ((f >= 0) && (f < m_FrameEnabled.FrameCountTrue))
				{
					m_FrameEnabled.SetEnable(f, !m_FrameEnabled.Enable(f));
				}
			}
			m_FrameEnabled.CalcEnableFrame();
			CalcInfo ();
			return true;
		}
		// ******************************************************
		public void SetFrameEnabled(bool b)
		{
			PushUndo(BackupSratus.FrameEnabled);
			for (int i = 0; i < sel.Length; i++)
			{
				int f = sel.Start + i;
				if ((f >= 0) && (f < m_FrameEnabled.FrameCountTrue))
				{
					m_FrameEnabled.SetEnable(f,b);
				}
			}
			m_FrameEnabled.CalcEnableFrame();
			CalcInfo();
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
		public void SetTargetCell(int c)
		{
			int cc = c;
			if (cc < 0) cc = 0;
			else if (cc >= m_cells.Length) cc = m_cells.Length-1;
			if( cc != sel.Target)
			{
				sel.SetTarget(cc);
			}
		}
		// ******************************************************
		public void SetSelStart(int f)
		{
			int ff = f;
			if (ff < 0) ff = 0;
			else if (ff >= m_cells[0].FrameCountTrue) ff = m_cells[0].FrameCountTrue;
			if (ff != sel.Start)
			{
				sel.SetStart(ff);
				//OnSelChanged(new EventArgs());
			}
		}
		// ******************************************************
		public void ClearAll()
		{
			PushUndo(BackupSratus.All);
			m_FrameEnabled.Init();
			for (int c = 0; c < m_cells.Length; c++) m_cells[c].Init();
			CalcInfo();

		}
		// ******************************************************
		public void Clear(int c)
		{
			if((c>=0) && (c < CellCount))
			{
				m_cells[c].Init();
				CalcInfo();
			}

		}

		// ******************************************************
		public int[] GetCellNum()
		{
			return m_cells[sel.Target].Values(sel);
		}
		// ******************************************************
		/// <summary>
		/// 選択範囲に選択範囲の前のフレームと同じ数値を入れる
		/// </summary>
		public void SetCellNumSame()
		{
			PushUndo(BackupSratus.NumberInput);

			int sv = sel.Start-1;
			if (sv < 0) sv = 0;
			int v = m_cells[sel.Target].Value(sv);
			if ((sv == 0) && (v <= 0)) v = 1;

			m_cells[sel.Target].SetValues(sel, v);
			sel.MoveDown();

		}
		public void SetCellNumInc()
		{
			PushUndo(BackupSratus.NumberInput);
			//int fc = m_data[0].Length;

			int sv = sel.Start - 1;
			if (sv < 0) sv = 0;
			int v = m_cells[sel.Target].Value(sv);
			v += 1;
			m_cells[sel.Target].SetValues(sel, v);
			sel.MoveDown();

		}

		public void SetCellNumDec()
		{
			PushUndo(BackupSratus.NumberInput);
			int sv = sel.Start - 1;
			if (sv < 0) sv = 0;
			int v = m_cells[sel.Target].Value(sv);
			v -= 1;
			if (v < 0) v = 0;
			m_cells[sel.Target].SetValues(sel, v);
			sel.MoveDown();

		}
		// ******************************************************
		/// <summary>
		/// 選択範囲にセル番号を入力
		/// </summary>
		/// <param name="v"></param>
		public void SetCellNum(int v,bool IsMove =true)
		{
			if (v < 0) v = 0;
			PushUndo(BackupSratus.NumberInput);
			m_cells[sel.Target].SetValues(sel, v);
			if (IsMove)
			{
				sel.MoveDown();
			}

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
			if (sel.MoveUp())
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
			PushUndo(BackupSratus.NumberInput);
			int len = sel.Length;
			if (len > ints.Length)
			{
				sel.Length = len;
				len = ints.Length;
			}
			m_cells[sel.Target].SetValues(sel, ints);

		}

		// ******************************************************
		public bool SelectionAdd(int v)
		{
			bool ret = false;
			int len = sel.Length + v;
			if(len>0)
			{
				if (sel.Start + len <= FrameCount)
				{
					sel.Length = len;
					ret = true;
				}

			}
			return ret;
		}
		public bool SelectionAll(int c)
		{
			bool ret = false;
			if (c < 0) c = 0;
			else if (c >= CellCount) c = CellCount - 1;
			sel.Target = c;
			sel.Start = 0;
			sel.Length = FrameCount;
			ret = true;
			return ret;
		}
		public bool SelectionAll()
		{
			return SelectionAll(sel.Target);
		}
		static private string NV(int v)
		{
			string s = v.ToString();
			if (s == "") s = "0";
			return s;
		}
		static private string SP2(int v)
		{
			string s = NV(v);
			if (v < 10)
			{
				return " " + s;
			}
			else
			{
				return s;
			}
		}
		static private string SP4(int v)
		{
			string s = NV(v);
			if (v < 10)
			{
				return "   " + s;
			}
			else if (v < 100)
			{
				return "  " + s;
			}
			else if (v < 1000)
			{
				return " " + s;
			}
			else
			{
				return s;
			}
		}
		public string FrameStr(int idx)
		{
			string ret = "";
			int frmV = 0;
			int pageV = 0;
			int secV = 0;
			int pageFrm = (int)m_PageSec*(int)m_FrameRate;
			idx -= m_OffSetFrame;
			switch (m_FrameDisp)
			{
				case T_FrameDisp.frame:
					if (idx >= 0)
					{
						frmV = idx + m_StartKoma;
					}
					else
					{
						frmV = idx;
					}
					ret = NV(frmV);
					break;
				case T_FrameDisp.pageFrame:
					//s0 = String.Format("{0,3}", frmV);
					if ((idx % (int)FrameRate) == 0)
					{
						pageV = (idx / (int)pageFrm) + 1;
						ret += NV(pageV)+"p";
					}
					frmV = (idx % pageFrm);
					if (frmV < 0) frmV = pageFrm + frmV;
					frmV += m_StartKoma;
					ret += SP4(frmV);
					break;
				case T_FrameDisp.pageSecFrame:
					if (idx % (int)FrameRate == 0)
					{
						pageV = (idx / (int)pageFrm) + 1;
						ret += NV(pageV) + "p";
						secV = (idx / (int)m_FrameRate);
						ret += SP2(secV) + "+";
					}
					frmV = (idx % (int)FrameRate);
					if (frmV < 0) frmV = (int)FrameRate + frmV;
					frmV += m_StartKoma;
					ret += SP2(frmV);
					break;
				case T_FrameDisp.SecFrame:
					if (idx % (int)FrameRate == 0)
					{
						secV = (idx / (int)FrameRate);
						ret = NV(secV)+"+";
					}
					frmV = (idx % (int)FrameRate);
					if(frmV < 0) frmV = (int)FrameRate + frmV;
					frmV += m_StartKoma;
					ret += SP2(frmV);
					break;
			}
			return ret;

		}
		// **********************************************************************
		public bool SwapCell(int c0,int c1)
		{
			bool ret = false;

			if((c0 != c1)&&(c0>=0) && (c0<CellCount)&&(c1 >= 0) && (c1 < CellCount))
			{
				ret =m_cells[c0].Swap(ref m_cells[c1]);
			}
			return ret;
		}
		public bool CellLeftShift()
		{
			bool ret = false;
			if((sel.Target>0)&&(sel.Target < CellCount))
			{
				ret = SwapCell(sel.Target-1, sel.Target);
				sel.Target -= 1;
			}
			return ret;
		}
		public bool CellRightShift()
		{
			bool ret = false;
			if ((sel.Target >=0) && (sel.Target < CellCount - 1))
			{
				ret = SwapCell(sel.Target, sel.Target+1);
				sel.Target += 1;
			}
			return ret;
		}
		public bool AutoInput(int st,int lt,int koma)
		{
			if ((st==lt)||(koma<=0)) return false;
			PushUndo(BackupSratus.NumberInput);
			m_cells[sel.Target].AutoInput(sel, st,lt, koma);
			return true;
		}
	}
}
