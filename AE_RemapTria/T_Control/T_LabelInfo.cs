using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE_RemapTria
{
	public class T_LabelInfo : T_Label
	{
		private T_Grid? m_grid = null;
		[Category("_AE_Remap")]
		public T_Grid? Grid
		{
			get { return m_grid; }
			set
			{
				m_grid = value;
				if (m_grid != null)
				{
					this.Text = m_grid.CellData.Info;
					m_grid.CellData.CountChanged += CellData_CountChanged;
					this.Invalidate();
				}
			}
		}

		private void CellData_CountChanged(object? sender, EventArgs e)
		{
			if (m_grid != null)
			{
				this.Text = m_grid.CellData.Info;
				this.Invalidate();
			}
		}
	}
}
