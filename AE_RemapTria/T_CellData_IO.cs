using System.Configuration.Internal;
using System.Numerics;


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
			if ((c >= 0) && (c < CellCount) && (f >= 0) && (f < FrameCount))
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
		/*
		/// <summary>
		/// セルの配列を[フレーム、セル番号]２次元の配列に変換
		/// </summary>
		/// <param name="ai"></param>
		/// <returns></returns>
		public int[][] ToCellArray(int[] ai)
		{
			int[][] ret = new int[0][];
			int fc = ai.Length;
			if (fc<0) return ret;
			int[] buf = new int[fc];

			buf[0] = ai[0];
			//前のフレームと同じ数字なら
			for (int i = 1; i < fc; i++)
			{
				if(ai[i-1] == ai[i]) 
					buf[i] = -1;
				else 
					buf[i] = ai[c][i];
			}
			List<int[]> ints = new List<int[]>();
			for (int i = 0; i < buf.Length; i++)
			{
				if (buf[i]>=0)
				{
					int[] p = new int[2];
					p[0] = i;
					p[1] = buf[i];
					ints.Add(p);
				}
			}
			ret = new int[ints.Count][];
			for(int i=0; i< ints.Count;i++)
			{
				ret[i] = ints[i];
			}
			return ret;
		}
		public int[][] ToCellArray(int c)
		{
			int[][] ret = new int[0][];
			if ((c >= 0) && (c < m_data.Length))
			{
				ret = ToCellArray(m_data[c]);
			}
			return ret;
		}
		public int[] FromCellArray(int fc,int[][] ca)
		{
			int [] ret = new int[fc];
			for (int i = 0; i < fc; i++) ret[i] = -1;
			ret[0] = 0;
			for(int i=0;i<ca.Length;i++)
			{
				int[] p = ca[i];
				if(p.Length >=2)
				{
					if ((p[0] >= 0) && (p[0] < fc))
					{
						ret[p[0]] = p[1];
					}
				}
			}
			for (int i = 1; i < fc; i++)
			{
				if (ret[i]==-1)
				{
					ret[i] = ret[i - 1];
				}
			}
			return ret;
		}
		*/
	}

}
