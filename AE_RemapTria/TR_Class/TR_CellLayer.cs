using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE_RemapTria
{
	public class TR_CellLayer
	{
		private bool m_IsEnabled=false;
		public string Caption = "";
		/// <summary>
		/// 実際のフレーム数
		/// </summary>
		public int FrameCountTrue { get { return m_cells.Length; } }
		private int m_FrameCount = 0;
		/// <summary>
		/// 抜きコマを考慮したフレーム数
		/// </summary>
		public int FrameCount { get { return m_FrameCount; } }
		private int m_UnEnabledFrameCount = 0;
		public int UnEnabledFrameCount { get { return m_UnEnabledFrameCount; } }

		private int[] m_cells = new int[0];
		// *********************************************************************
		public TR_CellLayer(int frm, bool isEna)
		{
			m_IsEnabled = true;
			Caption = "Enabled";
			SetFrameCountTrue(frm);
		}
		// ***********************************************************
		public void Copy(TR_CellLayer cl)
		{
			m_IsEnabled = cl.m_IsEnabled;
			Caption = cl.Caption;
			m_FrameCount = cl.m_FrameCount;
			m_UnEnabledFrameCount = cl.m_UnEnabledFrameCount;

			if(m_cells.Length!= cl.m_cells.Length)
			{
				Array.Resize(ref m_cells, cl.m_cells.Length);
			}
			for(int i=0; i<m_cells.Length;i++)
			{
				m_cells[i] = cl.m_cells[i];
			}
			CalcEnableFrame();
		}
		public bool Swap(ref TR_CellLayer cl)
		{
			if (m_cells.Length != cl.m_cells.Length)
			{
				return false;
			}
			bool b = m_IsEnabled;
			m_IsEnabled = cl.m_IsEnabled;
			cl.m_IsEnabled = b;
			string s = Caption;
			Caption = cl.Caption;
			cl.Caption = s;
			int v = m_FrameCount;
			m_FrameCount = cl.m_FrameCount;
			cl.m_FrameCount = v;
			v = m_UnEnabledFrameCount;
			m_UnEnabledFrameCount = cl.m_UnEnabledFrameCount;
			cl.m_UnEnabledFrameCount = v;

			for (int i = 0; i < m_cells.Length; i++)
			{
				int v2 = m_cells[i];
				m_cells[i] = cl.m_cells[i];
				cl.m_cells[i] = v2;
			}
			return true;
 		}
		// ***********************************************************
		public bool IsEmpty(TR_CellLayer cl)
		{
			bool ret = true;
			for(int i=0; i<m_cells.Length;i++)
			{
				if ((m_cells[i] > 0) && (cl.m_cells[i]==1))
				{
					ret = false;
					break;
				}
			}
			return ret;
		}
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
		public int[] RawData(TR_CellLayer cl)
		{
			int f = cl.FrameCount;
			int[] ret = new int[f];
			int cnt = 0;
			for (int i = 0; i < m_cells.Length; i++)
			{
				if(cl.Value(i)==1)
				{
					ret[cnt] = m_cells[i];
					cnt++;
					if (cnt >= f) break;
				}
			}
			return ret;
		}
		public int[] RawData()
		{
			int[] ret = new int[m_cells.Length];
			for(int i = 0; i < ret.Length; i++) ret[i] = m_cells[i];
			return ret;
		}
		public void SetRawData(int[] a)
		{
			int l = a.Length;
			if (l > m_cells.Length) l = m_cells.Length;
			if(l< m_cells.Length)
				for (int i = l; i < m_cells.Length; i++) m_cells[i] = 0;
			for (int i = 0; i < l; i++) m_cells[i] = a[i];
		}
		public int[] Values(TR_Selection sel)
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
		public void SetValues(TR_Selection sel,int[] a)
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
		public void SetValues(TR_Selection sel, int a)
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
		public void SetAllValues(int[] v)
		{
			int cnt = v.Length;
			if (cnt > m_cells.Length) cnt = m_cells.Length;
			for (int i = 0; i <cnt; i++)
			{
				m_cells[i] = v[i];
			}
		}
		// *********************************************************************
		public TR_CellLayer(int frm,string cap)
		{
			m_IsEnabled = false;
			Caption = cap.Trim();
			SetFrameCountTrue(frm);
		}

		// *********************************************************************
		public void Clear()
		{
			m_FrameCount = m_cells.Length;
			m_UnEnabledFrameCount = 0;
			int v = 0;
			if (m_IsEnabled) v = 1;
			for (int i = 0; i < m_FrameCount; i++) m_cells[i] = v;

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
		// *********************************************************************
		public bool SetFrameCount(int f)
		{
			bool ret = false;
			int nf = f;
			if(m_IsEnabled == true) nf += m_UnEnabledFrameCount;
			ret = SetFrameCountTrue(nf);
			return ret;
		}
		// *********************************************************************
		public bool SetFrameCountTrue(int f)
		{
			bool ret = false;
			int fc = m_cells.Length;
			if (fc != f)
			{
				int vl = 0;
				if (m_IsEnabled == true) vl = 1;

				Array.Resize(ref m_cells, f);
				if (f > fc)
				{
					for (int i = fc; i < f; i++)
						m_cells[i] = vl;
				}
				ret = true;
				CalcEnableFrame();
			}

			return ret;
		}
		// *********************************************************************
		public void CalcEnableFrame()
		{
			int fc = m_cells.Length;
			if (m_IsEnabled)
			{
				int cnt = 0;
				for (int i = 0; i < fc; i++)
				{
					if (m_cells[i] > 0) cnt++;
				}
				m_FrameCount = cnt;
				m_UnEnabledFrameCount = fc - cnt;

			}
			else
			{
				m_FrameCount = fc;
				m_UnEnabledFrameCount = 0;
			}
		}


		// *********************************************************************
		/// <summary>
		/// [frame,value]を要素とした配列をにする。
		/// 全部
		/// </summary>
		/// <returns></returns>
		public int[][] ToArray()
		{
			return ToArraySub(m_cells);
		}
		// *********************************************************************
		/// <summary>
		/// 除外フレームを考慮して[frame,value]を要素とした配列をにする
		/// </summary>
		/// <param name="ec">除外フレーム</param>
		/// <returns></returns>
		public int[][] ToArray(TR_CellLayer ec)
		{
			if(ec.FrameCountTrue == ec.FrameCount)
			{
				return ToArray();
			}
			int[] buf = new int[ec.FrameCount];
			int cnt = 0;
			for( int i=0;i<ec.FrameCountTrue;i++)
			{
				if(ec.Enable(i))
				{
					buf[cnt] = m_cells[i];
					cnt++;
				}
			}
			return ToArraySub(buf);
		}
		// *********************************************************************
		static private int[][] ToArraySub(int[] buf)
		{
			List<int[]> list = new List<int[]>();
			list.Add(new int[] { 0, buf[0] });
			for (int i = 1; i < buf.Length; i++)
			{
				if (buf[i - 1] != buf[i])
				{
					list.Add(new int[] { i, buf[i] });
				}
			}
			int[][] ret = new int[list.Count][];
			for (int i = 0; i < list.Count; i++)
			{
				ret[i] = list[i];
			}
			return ret;
		}
		// *********************************************************************
		public void FromArray(int[][] buf)
		{
			m_cells = FromArraySub(buf);
		}
		// *********************************************************************
		private int[] FromArraySub(int[][] buf)
		{
			int f = m_cells.Length;
			int[] ret = new int[f];
			// とりあえず-1
			for (int i = 0; i < f; i++) ret[i] = -1;
			//スタートは念のため０を入れておく
			ret[0] = 0;
			for (int i=0; i<buf.Length;i++)
			{
				if (buf[i].Length>=2)
				{
					if ( (buf[i][0]>=0)&& (buf[i][0] <f))
					{
						ret[buf[i][0]] = buf[i][1];
					}
				}
			}
			for (int i = 1; i < f; i++)
			{
				if (ret[i] == -1)
				{
					ret[i] = ret[i - 1];
				}
			}
			return ret;
		}
		// *********************************************************************
		public bool InsertFrame(int start,int length)
		{
			if(start<0)
			{
				length = length+start;
				start = 0;
			}else if(start+length>m_cells.Length)
			{
				length = (start + length) - m_cells.Length;
			}

			if ((start < 0) || (start >= m_cells.Length) || (length <= 0)) return false;
			SetFrameCountTrue(m_cells.Length + length);

			for(int i=m_cells.Length-1; i>=start+length; i--)
			{
				m_cells[i] = m_cells[i-length];
			}
			int v = 0;
			if (m_IsEnabled) v = 1;
			for(int i=0; i<length; i++)
			{
				m_cells[start + i] = v;
			}
			CalcEnableFrame();
			return true;
		}
		public bool RemoveFrame(int start, int length)
		{
			if (start < 0)
			{
				length = length + start;
				start = 0;
			}
			else if (start + length > m_cells.Length)
			{
				length = (start + length) - m_cells.Length;
			}
			if ((start < 0) || (start >= m_cells.Length) || (length <= 0)) return false;

			if (start + length < m_cells.Length)
			{
				for (int i = start; i < m_cells.Length - length; i++)
				{
					m_cells[i] = m_cells[i + length];
				}
			}
			SetFrameCountTrue(m_cells.Length - length);
			CalcEnableFrame();
			return true;
		}
		// *********************************************************************
		public void AutoInput(TR_Selection sel,int st,int lt,int koma)
		{
			int[] ary = new int[sel.Length];

			int cnt = sel.Length;
			int idx = 0;
			int v = st;
			int ad = 1;
			if (lt < st) ad = -1;
			while(idx<cnt)
			{
				for(int i = 0; i < koma; i++)
				{
					if (v <= 0)
					{
						ary[idx] = 0;
					}
					else
					{
						ary[idx] = v;
					}
					idx++;
					if (idx >= cnt) break;
				}
				v+=ad;
				if (ad > 0)
				{
					if (v > lt) v = st;
				}
				else
				{
					if (v < lt) v = st;
				}
			}
			SetValues(sel,ary);
		}
	}
}
