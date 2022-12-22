using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE_RemapTria
{
	public class TR_Selection
	{
		private TR_CellData? m_cellDate;
		private int m_target = 0;
		public int Target { get { return m_target; } set { SetTarget(value); } }	

		private int m_start = 0;
		public int Start { get { return m_start; } set { SetStart(value); } }
		private int m_length = 1;
		public int Length { get { return m_length; } set { m_length = value; } }

		private int m_startTrue = 0;
		public int StartTrue { get { return m_startTrue; } }
		private int m_lengthTrue = 1;
		public int LengthTrue { get { return m_length; } }
		private int m_LastFrameTrue = 1;
		public int LastIndex { get { return m_LastFrameTrue; } }

		private int m_LastFrame = 0;
		public int LastFrame
		{
			get { return m_LastFrame; }
			set
			{
				int v = value;
				if(v==m_start)
				{
					m_length=1;
				}else if(v>m_start)
				{
					m_length = v - m_start + 1;
				}else if(v < m_start)
				{
					int ms = m_start;
					m_start = v;
					m_length = v - ms + 1;
				}
				CalcTrue();
			}
		}


		// ******************************************************************
		public TR_Selection()
		{
			m_cellDate = null;
			CalcTrue();
		}
		public bool IsTargerCell(int c)
		{
			return (m_target == c);
		}
		public bool IsSelectedFrame(int f)
		{
			int ed = m_start + m_length;
			return ((f>=m_start) && (f<ed));
		}
		public bool IsSelected(int c,int f)
		{
			int ed = m_start + m_length;
			return ((m_target == c)&&(f >= m_start) && (f < ed));
		}
		// ******************************************************************
		public TR_Selection(TR_CellData cellDate)
		{
			SetCellData(cellDate);
		}
		// ******************************************************************
		public TR_Selection(TR_Selection sel)
		{
			Copy(sel);
		}
		// ******************************************************************
		public void Copy(TR_Selection sel)
		{
			m_cellDate = sel.m_cellDate;
			m_target = sel.m_target;
			m_start = sel.m_start;
			m_length = sel.m_length;
			m_lengthTrue = sel.m_lengthTrue;
			m_LastFrameTrue = sel.m_LastFrameTrue;
		}
		// ******************************************************************
		public void SetCellData(TR_CellData cd)
		{
			m_cellDate = cd;
		}
		// ******************************************************************
		public void SetTarget(int t)
		{
			if(m_cellDate !=null)
			{
				if (t < 0) t = 0;
				else if (t >= m_cellDate.CellCount) t = m_cellDate.CellCount - 1;
				m_target = t;
			}
		}
		// ******************************************************************
		public void SetStartLength(int s,int l)
		{
			m_start = s;
			if (l <= 0) l = 1;
			m_length = l;
			CalcTrue();
		}
		// ******************************************************************
		public void SetTargetStartLength(int t,int s, int l)
		{
			m_start = s;
			if (l <= 0) l = 1;
			m_length = l;
			m_target = t;
			CalcTrue();
		}
		// ******************************************************************
		public void Set2Frame(int f0, int f1)
		{
			if(f0==f1)
			{
				m_start = f0;
				m_length = 1;
			}else if (f0 < f1)
			{
				m_start = f0;
				m_length = f1 - f0 + 1;
			}
			else
			{
				m_start = f1;
				m_length = f0 - f1 + 1;
			}
			CalcTrue();

		}
		// ******************************************************************
		public void SetStart(int s)
		{
			m_start = s;
			CalcTrue();
		}
		// ******************************************************************
		public void SetLength(int l)
		{
			if (l <= 0) l = 1;
			m_length = l;
			CalcTrue();
		}
		// ******************************************************************
		private void CalcTrue()
		{
			m_startTrue = m_start;
			if (m_startTrue < 0) m_startTrue = 0;
			
			m_LastFrame = m_start + m_length - 1;
			m_LastFrameTrue = m_LastFrame;
			if (m_cellDate != null)
			{
				if (m_LastFrameTrue >= m_cellDate.FrameCount) m_LastFrameTrue = m_cellDate.FrameCount - 1;
			}
			m_lengthTrue = m_LastFrameTrue - m_startTrue + 1;

		}
		// ******************************************************************
		public bool MoveUp()
		{
			bool ret = false;
			int st = m_start - m_length;
			int ed = st + m_length;
			if(ed>0)
			{
				m_start = st;
				CalcTrue();
				ret = true;
			}
			return ret;
		}
		// ******************************************************************
		public bool MoveDown()
		{
			bool ret = false;
			int st = m_start + m_length;
			int ed = st + m_length - 1;
			if (m_cellDate != null)
			{
				if (st < m_cellDate.FrameCount)
				{
					m_start = st;
					CalcTrue();
					ret = true;
				}
			}
			return ret;
		}
		// ******************************************************************
		public bool MoveLeft()
		{
			bool ret = false;
			int t = m_target - 1;
			if (t >= 0)
			{
				m_target = t;
				ret = true;
			}
			return ret;
		}
		public bool MoveRight()
		{
			bool ret = false;
			int t = m_target + 1;
			if (m_cellDate != null)
			{
				if (t < m_cellDate.CellCount)
				{
					m_target = t;
					ret = true;
				}
			}
			return ret;
		}
		public bool SelToEnd()
		{
			bool ret = false;
			if(m_cellDate!=null)
			{
				m_length = m_cellDate.FrameCount-m_start;
				CalcTrue();
				ret = true;
				/*
				 * 0
				 * 1
				 * 2
				 * 3
				 */
			}
			return ret;
		}
	}
}
