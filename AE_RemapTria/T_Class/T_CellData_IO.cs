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
		// ******************************************************
		public int[][][] Cell
		{
			get
			{
				int[][][] ret = new int[CellCount][][];
				for (int i = 0; i < CellCount; i++)
				{
					ret[i] = m_cells[i].ToArray();
				}
				return ret;
			}
		}
	}

}
