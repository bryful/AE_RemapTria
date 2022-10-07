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
			if ((c >= 0) && (c < CellCount) && (f >= 0) && (f < FrameCountTrue))
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
					ret[i] = m_cells[i].ToArray(m_FrameEnabled);
				}
				return ret;
			}
			set
			{
				m_FrameEnabled.Init();
				int c = value.Length;
				if (c <= 0) return;
				if (c > CellCount) c = CellCount;
				int f = value[0].Length;
				if (f > FrameCountTrue) f = FrameCountTrue;
				if (f < 6) return;
				for (int i = 0; i < c; i++)
				{
					m_cells[c].FromArray(value[c], f);
				}
			}
		// ******************************************************
		public int[][][] CellWithEnabled
		{
			get
			{
				int[][][] ret = new int[CellCount+1][][];
				ret[0] = m_FrameEnabled.ToArray();
				for (int i = 0; i < CellCount; i++)
				{
					ret[i+1] = m_cells[i].ToArray();
				}
				return ret;
			}
		}
		// ******************************************************
		/// <summary>
		/// セル名の配列
		/// </summary>
		public string[] Captions
		{
			get
			{
				string[] ret = new string[CellCount];
				for (int i = 0; i < CellCount; i++)
					ret[i] = m_cells[i].Caption;
				return ret;
			}
			set
			{
				int c = value.Length;
				if (c > CellCount) c = CellCount;
				for (int i = 0; i < c; i++)
					m_cells[i].Caption = value[i];
			}
		}

	}

}
