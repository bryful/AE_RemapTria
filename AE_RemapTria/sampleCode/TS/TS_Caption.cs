using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace TS

{
	public class TS_Caption : TS_GridBase
	{
		private TS_Grid m_grid = null;
		private TS_CellData m_CellData = null;

		private TS_Selection m_Selection = null;

		private TS_GridSize m_GridSize = null;
		private TS_GridColor m_GridColor = null;

		private bool UseParm = false;
		//--------------------------------------------
		public TS_Caption()
		{
			Init();

			ChkCellData();
			ChkGridColor();
			ChkGridSize();
			ChkSelection();

            Alignment = StringAlignment.Center;
            Font_Size = 12;
			FontBold = true;
		}
		//--------------------------------------------
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
		private void ChkCellData()
		{
			if (m_CellData == null) m_CellData = new TS_CellData();
		}
		//------------------------------------------
		private void ChkSelection()
		{
			if (m_Selection == null) m_Selection = new TS_Selection();
			m_Selection.ChangeSelectionEvent += ChangeStatus;
		}
		//------------------------------------------
		private void ChkGridSize()
		{
			if (m_GridSize == null) m_GridSize = new TS_GridSize();
			ChkMinMax();
			m_GridSize.ChangeGridSize += ChangeStatus;
			m_GridSize.ChangeDisp += ChangeStatus;
		}
		//------------------------------------------
		private void ChkGridColor()
		{
			if (m_GridColor == null) m_GridColor = new TS_GridColor();
			m_GridColor.ChangeColorEvent += ChangeStatus;
		}
		//--------------------------------------------
		private void ChkGrid()
		{

			if (m_grid == null)
			{
				m_CellData = null;
				m_GridSize = null;
				m_GridColor = null;
				m_Selection = null;
				UseParm = false;
			}
			else
			{
				m_CellData = m_grid.CellData;
				m_GridSize = m_grid.GridSize;
				m_GridColor = m_grid.GridColor;
				m_Selection = m_grid.Selection;
				UseParm = true;
			}
			ChkCellData();
			ChkSelection();
			ChkGridColor();
			ChkGridSize();
		}
		//--------------------------------------------
		private void ChangeStatus(object sender, EventArgs e)
		{
			this.Invalidate();
		}

		//-------------------------------------------------
		private void ChkMinMax()
		{
			int lc =  m_CellData.LayerCount;
			int h = m_GridSize.CaptionHeight + m_GridSize.CaptionHeight2;
			this.MinimumSize = new Size(m_GridSize.CellWidth * 6, h);
			this.MaximumSize = new Size(m_GridSize.CellWidth * lc, h);
		}
		//-------------------------------------------------
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			if(m_grid != null)
			{
				m_grid.SetSize();
			}
			this.Invalidate();
		}       
		//-------------------------------------------------
		private void DrawCaption(Graphics g, SolidBrush sb, Pen p, int l)
		{
			int x = l * m_GridSize.CellWidth - m_GridSize.Disp.X;

			Rectangle r;
			if (m_Selection.IsTargetLayer(l) == true)
			{
				r = new Rectangle(x, 0, m_GridSize.CellWidth, m_GridSize.CaptionHeight+ m_GridSize.CaptionHeight2);
				sb.Color = m_GridColor.SelectionCaption;
				Fill(g, sb, r);
			}
			else
			{
				r = new Rectangle(x, 0, m_GridSize.CellWidth, m_GridSize.CaptionHeight2);
				sb.Color = m_GridColor.Caption2;
				Fill(g, sb, r);
				r = new Rectangle(x, m_GridSize.CaptionHeight2, m_GridSize.CellWidth, m_GridSize.CaptionHeight);
				sb.Color = m_GridColor.Caption;
				Fill(g, sb, r);
				p.Color = m_GridColor.LineA;
                DrawHorLine(g, p, x, x + m_GridSize.CellWidth - 1, m_GridSize.CaptionHeight2);
 			}
            DrawVerLine(g, p, x, m_GridSize.CaptionHeight2, m_GridSize.CaptionHeight + m_GridSize.CaptionHeight2 - 1);

            sb.Color = m_GridColor.Moji;
			r = new Rectangle(x, m_GridSize.CaptionHeight2, m_GridSize.CellWidth, m_GridSize.CaptionHeight);
			DrawStr(g, m_CellData.Caption(l), sb, r);

		}
		//-------------------------------------------------
		protected override void OnPaint(PaintEventArgs e)
		{
			Pen p = new Pen(m_GridColor.Line);
			SolidBrush sb = new SolidBrush(Color.Transparent);
			try
			{
				Graphics g = e.Graphics;
				g.SmoothingMode = SmoothingMode.AntiAlias;
                g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;

                //とりあえず塗りつぶし
                Fill(g, sb);
				Rectangle r = m_GridSize.DispCell;
				for (int i = r.Left; i <= r.Right; i++)
				{
					DrawCaption(g, sb, p, i);
				}
                p.Color = m_GridColor.Line;
                int y0 = m_GridSize.CaptionHeight2;
                int y1 = m_GridSize.CaptionHeight + m_GridSize.CaptionHeight2-1;
                DrawVerLine(g, p, 0, y0, y1);
                DrawVerLine(g, p, this.Width-1, y0, y1);
                DrawHorLine(g, p, 0, this.Width - 1, y1);

            }
            finally
			{
				p.Dispose();
				sb.Dispose();
			}

		}
	}
}
