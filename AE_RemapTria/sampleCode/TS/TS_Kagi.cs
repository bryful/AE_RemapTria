using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TS
{
	public enum KAGI
	{
		TOP_RIGHT =0,
		BOTTOM_RIGNT,
		BOTTOM_LEFT,
		TOP_LEFT
	}
	public class TS_Kagi : TS_GridBase
	{
		private TS_Grid m_grid = null;
		private TS_GridColor m_GridColor = null;
		private KAGI m_mode = KAGI.TOP_RIGHT;
		/// <summary>
		/// 
		/// </summary>
		public TS_Kagi()
		{
			Init();
			ChkGridColor();
			int sz = 22;
			this.MinimumSize = new Size(sz, sz);
			this.MaximumSize = new Size(sz, sz);
			this.Invalidate();
		}
		//-------------------------------------------------
		public KAGI Dir
		{
			get { return m_mode; }
			set
			{
				bool b = (m_mode != value);
				m_mode = value;
				if (b) this.Invalidate();
			}
		}
		//-------------------------------------------
		public TS_Grid Grid
		{
			get { return m_grid; }

			set
			{
				m_grid = value;
				ChkGrid();
			}
		}
		//------------------------------------------
		private void ChkGrid()
		{
			if (m_grid == null)
			{
				m_GridColor = null;
			}
			else
			{
				m_GridColor = m_grid.GridColor;
			}
			ChkGridColor();
		}//------------------------------------------
		private void ChkGridColor()
		{
			if (m_GridColor == null) m_GridColor = new TS_GridColor();
		}

		//-------------------------------------------------
		protected override void OnPaint(PaintEventArgs e)
		{
			//base.OnPaint(e);
			Pen p = new Pen(m_GridColor.Kagi);
			try
			{
				Graphics g = e.Graphics;
				p.Width = 5;
				int sz = this.Width;
				int lp = (int)p.Width / 2;
				Point[] points = new Point[3];
				switch (m_mode)
				{
					case KAGI.TOP_RIGHT:
						points[0] = new Point(lp, lp);
						points[1] = new Point(sz - lp, lp);
						points[2] = new Point(sz - lp, sz - lp);
						break;
					case KAGI.BOTTOM_RIGNT:
						points[0] = new Point(sz - lp, lp);
						points[1] = new Point(sz - lp, sz - lp);
						points[2] = new Point(lp, sz - lp);
						break;
					case KAGI.BOTTOM_LEFT:
						points[0] = new Point(sz - lp, sz - lp);
						points[1] = new Point(lp, sz - lp);
						points[2] = new Point(lp, lp);
						break;
					case KAGI.TOP_LEFT:
						points[0] = new Point(lp, sz - lp);
						points[1] = new Point(lp, lp);
						points[2] = new Point(sz, lp);
						break;
				}
				g.DrawLines(p, points);
			}
			finally
			{
				p.Dispose();
			}
		}
		//-------------------------------------------------
	}
}
