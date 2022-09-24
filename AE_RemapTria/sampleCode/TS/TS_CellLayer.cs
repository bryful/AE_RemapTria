using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TS
{
	public class TS_CellLayer
	{
		//----------------------------------------------------------
		private int[] m_cell = new int[72];
		private int m_frameCount;
		private string m_caption;
		//----------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		/// <param name="fc">フレーム数</param>
		/// <param name="cap">セルの名前</param>
		public TS_CellLayer(int fc = 6, string cap = "")
		{
			Init(fc, cap);
		}
		//---------------------------------------------------
		public void Init(int fc = 6, string c = "")
		{
			if (fc < 6) fc = 6;
			Array.Resize(ref m_cell, fc);
			for (int i = 0; i < fc; i++) m_cell[i] = 0;
			m_caption = c;
			m_frameCount = m_cell.Length;
		}
		//---------------------------------------------------
		public void SetFrameCount(int fc)
		{
			if (fc < 6) fc = 6;
			int l = m_frameCount;
			int v = fc - l;
			Array.Resize(ref m_cell, fc);
			if (v > 0)
			{
				for (int i = l; i < fc; i++)
				{
					m_cell[i] = 0;
				}
			}
			m_frameCount = m_cell.Length;
		}
		//---------------------------------------------------
		public void InsertFrame(int f, int c)
		{
			if (c<=0)
			{
				return;
			}
			if (f < 0)
			{
				f = 0;
			}
			else if (f >= m_frameCount)
			{
				SetFrameCount(m_frameCount + c);
				return;
			}
			int sz = m_frameCount + c;
		}
		//---------------------------------------------------
		public string Caption
		{
			get { return m_caption; }
			set { m_caption = value; }
		}
        //---------------------------------------------------
        public int FrameCount
        {
            get { return m_frameCount; }
			set { SetFrameCount(value); }
        }
		//---------------------------------------------------
		public  void SetCell(List<List<int>> s)
		{
			int c  = s.Count;
			if (c <= 0) return;
			int fc = s[c - 1][0];

			int[] r = new int[fc];
			for (int i = 0; i < fc; i++) r[i] = -1;
			for ( int i=0; i<c;i++)
			{
				r[s[i][0]] = s[i][1];
			}
			for (int i = 1; i < fc; i++)
			{
				if (r[i] == -1)
				{
					r[i] = r[i - 1];
				}
			}
			SetFrameCount(fc);
			for (int i = 1; i < fc; i++)
			{
				m_cell[i] = r[i];
			}


		}
		//---------------------------------------------------
		public List<List<int>> GetCell()
		{
			List<List<int>> ret = new List<List<int>>();

			int[] r = new int[m_frameCount];
			int lc = m_frameCount - 1;
			r[0] = m_cell[0];
			r[lc] = m_cell[lc];
			for (int i = 1; i < lc; i++)
			{
				if (m_cell[i - 1] == m_cell[i])
				{
					r[i] = -1;
				}
				else
				{
					r[i] = m_cell[i];
				}
			}
			for (int i = 0; i < m_frameCount; i++)
			{
				if (r[i]!=-1)
				{
					List<int> rr = new List<int>();
					rr.Add(i);
					rr.Add(r[i]);
					ret.Add(rr);
				}
			}

			return ret;

		}
		//---------------------------------------------------
		public void SetCell(int idx,int v)
		{
			if ((idx >= 0) && (idx < m_frameCount))
			{
				if (v < 0) v = 0;
				m_cell[idx] = v;
			}
		}
		//---------------------------------------------------
		public int GetCell(int idx)
		{
			int ret = -1;
			if ((idx >= 0) && (idx < m_frameCount))
			{
				ret = m_cell[idx];
			}
			return ret;
		}
		//---------------------------------------------------
		public int this[int idx]
		{
			get
			{
				return GetCell(idx);
			}
			set
			{
				SetCell(idx, value);
			}
		}
		//---------------------------------------------------

	}
}
