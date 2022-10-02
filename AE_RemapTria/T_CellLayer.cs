using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE_RemapTria
{
	public class T_CellLayer
	{
		private bool m_IsEnabled=false;
		public string Caption = "";
		public int FrameCount { get { return m_cells.Length; } }
		private int m_FrameCountTrue = 0;
		public int FrameCountTrue { get { return m_FrameCountTrue; } }

		private int[] m_cells = new int[0];
		public int Value(int idx)
		{
			int ret = -1;
			if((idx >= 0)&&(idx<m_cells.Length))
			{
				ret = m_cells[idx];
			}
			return ret;
		}
		public void SetValue(int idx,int v)
		{
			if (v < 0) v = 0;
			if ((idx >= 0) && (idx < m_cells.Length))
			{
				m_cells[idx] = v;
			}
		}
		public bool Enable(int idx)
		{
			bool ret = true;
			if ((idx >= 0) && (idx < m_cells.Length))
			{
				ret = (m_cells[idx]>0);
			}
			return ret;
		}
		public void SetEnable(int idx, bool b)
		{
			if ((idx >= 0) && (idx < m_cells.Length))
			{
				int v = 1;
				if (b == false) v = 0; 
				m_cells[idx] = v;
			}
		}
		public int[] Arrays()
		{
			int[] ret = new int[m_cells.Length];
			for(int i = 0; i < ret.Length; i++) ret[i] = m_cells[i];
			return ret;
		}
		public void SetArrays(int[] a)
		{
			int l = a.Length;
			if (l > m_cells.Length) l = m_cells.Length;
			if(l< m_cells.Length)
				for (int i = l; i < m_cells.Length; i++) m_cells[i] = 0;
			for (int i = 0; i < l; i++) m_cells[i] = a[i];
		}
		public int[] Values(T_Selection sel)
		{
			int[] ret = new int[sel.Length];
			for( int i = 0; i < sel.Length; i++)
			{
				int idx = sel.Start + i;
				if (idx < 0) idx = 0;
				else if (idx >= m_cells.Length) idx = m_cells.Length - 1;
				ret[i] = m_cells[idx];
			}
			return ret;
		}
		public void SetValues(T_Selection sel,int[] a)
		{
			int l= a.Length;
			if (l > sel.Length) l = sel.Length;
			for (int i = 0; i < l; i++)
			{
				int idx = sel.Start + i;
				if((idx>=0)&&(idx<m_cells.Length))
				{
					m_cells[idx] = a[i];
				}
			}
		}
		// *********************************************************************
		public void SetValues(T_Selection sel, int a)
		{
			for (int i = 0; i < sel.Length; i++)
			{
				int idx = sel.Start + i;
				if ((idx >= 0) && (idx < m_cells.Length))
				{
					m_cells[idx] = a;
				}
			}
		}
		// *********************************************************************
		public T_CellLayer(int frm,string cap)
		{
			m_IsEnabled = false;
			Caption = cap.Trim();
			SetFrameCount(frm);
		}
		// *********************************************************************
		public T_CellLayer(int frm, bool isEna)
		{
			m_IsEnabled = true;
			Caption = "Enabled";
			SetFrameCount(frm);
		}
		// *********************************************************************
		public void Init()
		{
			for(int i=0; i<m_cells.Length; i++)
			{
				int v = 0;
				if (m_IsEnabled == true) v = 1;
				m_cells[i] = v;
			}
			CalcEnableFrame();
		}
		public bool SetFrameCount(int v)
		{
			bool ret = false;
			int fc = m_cells.Length;
			if (fc !=v)
			{
				int vl = 0;
				if(m_IsEnabled == true) vl = 1;

				Array.Resize(ref m_cells, v);
				if(v>fc)
				{
					for(int i=fc; i<v;i++)
						m_cells[i] = vl;
				}
				ret = true;
				CalcEnableFrame();
			}

			return ret;
		}
		public void CalcEnableFrame()
		{
			int fc = FrameCount;
			if (m_IsEnabled)
			{
				int cnt = 0;
				for (int i = 0; i < fc; i++)
				{
					if (m_cells[i] > 0) cnt++;
				}
				m_FrameCountTrue = cnt;
			}
			else
			{
				m_FrameCountTrue = fc;
			}
		}

	}
}
