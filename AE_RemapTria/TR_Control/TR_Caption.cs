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
using BRY;
using PdfSharpCore.Drawing;

namespace AE_RemapTria
{
	public class TR_Caption :TR_Control
	{
		// ***********************************************
		public TR_Caption()
		{
			m_FontSize = 9;
			m_FontIndex = 5;
		}
		// ***********************************************
		public override void SetTRForm(TR_Form fm,bool isI=true)
		{
			base.SetTRForm(fm,false);
			if (m_form != null)
			{
				SetLocSize();
				ChkOffScr();
			}
			Invalidate();
		}
		public override void SetLocSize()
		{
			if (m_form == null) return;
			int l = m_form.Sizes.FrameWidth + m_form.Sizes.InterWidth;
			int t = m_form.Sizes.MenuHeight + m_form.Sizes.InterHeight;
			Point p = new Point(l, t);
			if (m_Location != p) m_Location = p;
			int w =
				m_form.Width
				- m_form.Sizes.FrameWidth
				- m_form.Sizes.InterWidth
				- m_form.Sizes.InterWidth
				- TR_Size.VScrolWidth;
			int h = m_form.Sizes.CaptionHeight
				+ m_form.Sizes.CaptionHeight2;
			Size sz = new Size(w,h);
			if (m_Size != sz) m_Size = sz;
			ChkOffScr();
		}

		//-------------------------------------------------
		private PointF [] Arrow(Rectangle r)
		{
			float cx = r.X + (float)r.Width / 2;
			PointF [] ret = new PointF[]
			{
				new PointF(cx-5,r.Bottom-10),
				new PointF(cx,r.Bottom-4),
				new PointF(cx+5,r.Bottom-10)
			};
			return ret;

		}
		//-------------------------------------------------
		private PointF[] ArrowS(Rectangle r)
		{
			float cx = r.X + (float)r.Width / 2;
			PointF[] ret = new PointF[]
			{
				new PointF(cx-3,r.Bottom-6),
				new PointF(cx,r.Bottom-2),
				new PointF(cx+3,r.Bottom-6)
			};
			return ret;

		}
		//-------------------------------------------------
		private void DrawCaption(Graphics g, SolidBrush sb, Pen p, int l)
		{
			if (m_form == null) return;

			int x = l * m_form.Sizes.CellWidth - m_form.Sizes.Disp.X;

			Rectangle r;
			Rectangle r2;
			r = new Rectangle(
				x, 
				m_form.Sizes.CaptionHeight2, 
				m_form.Sizes.CellWidth, 
				m_form.Sizes.CaptionHeight);
			r2 = new Rectangle(
				x,
				0,
				m_form.Sizes.CellWidth,
				m_form.Sizes.CaptionHeight2);
			if (m_form.CellData.IsTargetCell(l) == true)
			{
				sb.Color = m_form.Colors.Selection;
				Fill(g, sb, r);
				sb.Color = m_form.Colors.GLine;
				g.FillPolygon(sb, Arrow(r2));
			}
			else
			{
				sb.Color = m_form.Colors.GLineHor;
				g.FillPolygon(sb, ArrowS(r2));
			}
			p.Color = m_form.Colors.GLineInSide;
			DrawVerLine(g, p, 
				x-1, 
				m_form.Sizes.CaptionHeight2, 
				m_form.Sizes.CaptionHeight + m_form.Sizes.CaptionHeight2 - 1);
			sb.Color = m_form.Colors.Moji;
			DrawStr(g, m_form.CellData.CaptionFromIndex(l), sb, r);
		}
		public override void Draw(Graphics g)
		{
			if (m_form == null) return;
			Pen p = new Pen(m_form.Colors.GLine);
			SolidBrush sb = new SolidBrush(Color.Transparent);
			try
			{
				g.Clear(Color.Transparent);

				Rectangle r = m_form.Sizes.DispCell;
				m_form.Alignment = StringAlignment.Center;
				for (int i = r.Left; i <= r.Right; i++)
				{
					DrawCaption(g, sb, p, i);
				}
				p.Color = m_form.Colors.GLine;
				int y0 = m_form.Sizes.CaptionHeight2;
				int y1 = m_form.Sizes.CaptionHeight2 + m_form.Sizes.CaptionHeight - 1;
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
		public override bool ChkMouseDown(MouseEventArgs e)
		{
			bool ret = false;
			if (m_form == null) return false;
			ret = base.ChkMouseDown(e);
			if (m_inMouse == false) return ret;

			int t = (m_MDownPoint.X + m_form.Sizes.Disp.X) / m_form.Sizes.CellWidth;
			m_form.SetCellTarget(t);
			if((e.Button & MouseButtons.Right) == MouseButtons.Right)
			{
				if (m_form != null)
				{
					ContextMenuStrip m = m_form.MakeCMGrid();
					Point p = Cursor.Position;

					m.Show(p);
					ret = true;
				}

			}
			return ret;
		}
		// *****************************************************************************
		public override bool ChkDoubleClick(EventArgs e)
		{
			bool ret = false;
			if (m_MDown)
			{
				if (m_form != null)
				{
					m_form.SelectionAll();
					ret = true;
				}
			}
			return ret;
		}
	}
}
