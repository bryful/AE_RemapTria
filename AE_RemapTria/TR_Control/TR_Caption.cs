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
		private Bitmap CellArrow = Properties.Resources.CellArrow;
		private Bitmap CellArrowNone = Properties.Resources.CellArrowNone;
		// ***********************************************
		public TR_Caption()
		{
			m_FontSize = 9;
		}
		// ***********************************************
		public override void SetTRForm(TR_Form fm)
		{
			m_form = fm;
			if (m_form != null)
			{
				Colors = m_form.Colors;
				m_font = m_form.MyFont(m_FontIndex, m_FontSize, m_form.FontStyle);
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
				- T_Size.VScrolWidth;
			int h = m_form.Sizes.CaptionHeight
				+ m_form.Sizes.CaptionHeight2;
			Size sz = new Size(w,h);
			if (m_Size != sz) m_Size = sz;
			ChkOffScr();
		}
   
		//-------------------------------------------------
		private void DrawCaption(Graphics g, SolidBrush sb, Pen p, int l)
		{
			if (m_form == null) return;

			int x = l * m_form.Sizes.CellWidth - m_form.Sizes.Disp.X;

			Rectangle r;
			r = new Rectangle(
				x, 
				m_form.Sizes.CaptionHeight2, 
				m_form.Sizes.CellWidth, 
				m_form.Sizes.CaptionHeight);

			if (m_form.CellData.IsTargetCell(l) == true)
			{
				sb.Color = m_form.Colors.SelectionCaption;
				Fill(g, sb, r);
				g.DrawImage(
					CellArrow, 
					new Point(x + m_form.Sizes.CellWidth / 2 - 7, m_form.Sizes.CaptionHeight2-12));
			}
			else
			{
				g.DrawImage(
					CellArrowNone, 
					new Point(x + m_form.Sizes.CellWidth / 2 - 6, m_form.Sizes.CaptionHeight2 - 12));
			}
			p.Color = m_form.Colors.LineDark;
			DrawVerLine(g, p, 
				x, 
				m_form.Sizes.CaptionHeight2, 
				m_form.Sizes.CaptionHeight + m_form.Sizes.CaptionHeight2 - 1);
			sb.Color = m_form.Colors.Moji;
			DrawStr(g, m_form.CellData.CaptionFromIndex(l), sb, r);
		}
		public override void Draw(Graphics g)
		{
			if (m_form == null) return;
			Pen p = new Pen(m_form.Colors.Line);
			SolidBrush sb = new SolidBrush(Color.Transparent);
			try
			{
				g.Clear(Color.Transparent);
				//とりあえず塗りつぶし
				//Fill(g, sb);
				//T_G.GradBG_Top(g,new Rectangle(0,0,,Width,Height);
				Rectangle r = m_form.Sizes.DispCell;
				m_form.Alignment = StringAlignment.Center;
				for (int i = r.Left; i <= r.Right; i++)
				{
					DrawCaption(g, sb, p, i);
				}
				p.Color = m_form.Colors.Line;
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
			base.ChkMouseDown(e);
			if (m_inMouse == false) return ret;

			int t = (m_MDownPoint.X + m_form.Sizes.Disp.X) / m_form.Sizes.CellWidth;
			m_form.SetCellTarget(t);
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
