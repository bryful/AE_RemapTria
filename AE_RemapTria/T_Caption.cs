using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AE_RemapTria
{
	public class T_Caption :T_ContolBase
	{
		private Bitmap CellArrow = Properties.Resources.CellArrow;
		private Bitmap CellArrowNone = Properties.Resources.CellArrowNone;
		private T_Grid m_grid = null;

		// ***********************************************
		public T_Caption()
		{
			Init();

			Alignment = StringAlignment.Center;
			Font_Size = 12;
			FontBold = true;
		}
		//--------------------------------------------
		public T_Grid Grid
		{
			get { return m_grid; }
			set
			{
				m_grid = value;
				ChkGrid();
			}
		}
		// ***********************************************
		private void ChkGrid()
		{

			if (m_grid != null)
			{
				MyFonts = m_grid.MyFonts;
				SetMyFont(MFontIndex, MFontSize);
				m_grid.CellData.SelChangedEvent += ChangedEvent;
				m_grid.CellData.ValueChangedEvent += ChangedEvent;
				m_grid.Sizes.ChangeGridSize += Sizes_ChangeGridSize;
				m_grid.Sizes.ChangeDisp += ChangedEvent;
				m_grid.Colors.ColorChangedEvent += ChangedEvent;
				ChkMinMax();

			}
		}

		// ***********************************************
		private void Sizes_ChangeGridSize(object? sender, EventArgs e)
		{
			ChkMinMax();
			this.Invalidate();
		}

		// ***********************************************
		private void ChangedEvent(object? sender, EventArgs e)
		{
			this.Invalidate();
		}
		// ***********************************************
		private void ChkMinMax()
		{
			if (m_grid == null) return;
			int lc = m_grid.CellData.CellCount;
			int h = m_grid.Sizes.CaptionHeight + m_grid.Sizes.CaptionHeight2;
			this.MinimumSize = new Size(m_grid.Sizes.CellWidth * 6, h);
			this.MaximumSize = new Size(m_grid.Sizes.CellWidth * lc, h);
		}
		//-------------------------------------------------
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			if (m_grid != null)
			{
				m_grid.SetSize();
			}
			this.Invalidate();
		}               
		//-------------------------------------------------
		private void DrawCaption(Graphics g, SolidBrush sb, Pen p, int l)
		{
			if (m_grid == null) return;
			int x = l * m_grid.Sizes.CellWidth - m_grid.Sizes.Disp.X;

			Rectangle r;
			r = new Rectangle(x, m_grid.Sizes.CaptionHeight2, m_grid.Sizes.CellWidth, m_grid.Sizes.CaptionHeight);
			if (m_grid.CellData.IsTargetCell(l) == true)
			{
				sb.Color = m_grid.Colors.SelectionCaption;
				g.DrawImage(CellArrow, new Point(x + m_grid.Sizes.CellWidth / 2 - 6, m_grid.Sizes.CaptionHeight2-12));
			}
			else
			{
				sb.Color = m_grid.Colors.Caption;
				g.DrawImage(CellArrowNone, new Point(x + m_grid.Sizes.CellWidth / 2 - 6, m_grid.Sizes.CaptionHeight2 - 12));
			}
			Fill(g, sb, r);
			p.Color = m_grid.Colors.LineA;
			DrawVerLine(g, p, x, m_grid.Sizes.CaptionHeight2, m_grid.Sizes.CaptionHeight + m_grid.Sizes.CaptionHeight2 - 1);
			sb.Color = m_grid.Colors.Moji;
			DrawStr(g, m_grid.CellData.Caption(l), sb, r);
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			if(m_grid == null) return;
			Pen p = new Pen(m_grid.Colors.Line);
			SolidBrush sb = new SolidBrush(Color.Transparent);
			try
			{
				Graphics g = e.Graphics;
				g.SmoothingMode = SmoothingMode.AntiAlias;
				g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;

				//とりあえず塗りつぶし
				Fill(g, sb);
				Rectangle r = m_grid.Sizes.DispCell;
				for (int i = r.Left; i <= r.Right; i++)
				{
					DrawCaption(g, sb, p, i);
				}
				p.Color = m_grid.Colors.Line;
				int y0 = m_grid.Sizes.CaptionHeight2;
				int y1 = m_grid.Sizes.CaptionHeight2 + m_grid.Sizes.CaptionHeight - 1;
				DrawVerLine(g, p, 0, y0, y1);
				DrawVerLine(g, p, this.Width - 1, y0, y1);
				DrawHorLine(g, p, 0, this.Width - 1, y1);
				DrawHorLine(g, p, 0, this.Width - 1, y0);

			}
			finally
			{
				p.Dispose();
				sb.Dispose();
			}

		}
		// *****************************************************************************
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (m_grid == null) return;
			int t = (e.Location.X + m_grid.Sizes.Disp.X)/ m_grid.Sizes.CellWidth;
			m_grid.CellData.SetTarget(t);
		}
		// *****************************************************************************
	}
}
