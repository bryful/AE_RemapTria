using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE_RemapTria
{
	public class T_Selection
	{
		private T_CellData? m_cellDate;
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
		private int m_LastIndex = 1;
		public int LastIndex { get { return m_LastIndex; } }

		// ******************************************************************
		public T_Selection()
		{
			m_cellDate = null;
		}
		public bool IsTargerCell(int c)
		{
			return ((m_target == c) && (m_target >= 0));
		}
		// ******************************************************************
		public T_Selection(T_CellData cellDate)
		{
			m_cellDate = cellDate;
		}
		// ******************************************************************
		public T_Selection(T_Selection sel)
		{
			Copy(sel);
		}
		// ******************************************************************
		public void Copy(T_Selection sel)
		{
			m_cellDate = sel.m_cellDate;
			m_target = sel.m_target;
			m_start = sel.m_start;
			m_length = sel.m_length;
			m_lengthTrue = sel.m_lengthTrue;
			m_LastIndex = sel.m_LastIndex;
		}
		// ******************************************************************
		public void SetCellData(T_CellData cd)
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
			m_LastIndex = m_start + m_length - 1;
			if (m_cellDate != null)
			{
				if (m_LastIndex >= m_cellDate.FrameCount) m_LastIndex = m_cellDate.FrameCount - 1;
			}
			m_lengthTrue = m_LastIndex - m_startTrue + 1;

		}
		// ******************************************************************
		public bool MoveUp()
		{
			bool ret = false;
			int st = m_start - m_length;
			int ed = st + m_length - 1;
			if(ed>=0)
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
	}
}
