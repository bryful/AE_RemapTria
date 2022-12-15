using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE_RemapTria
{
	public class TR_VScrol : TR_Control
	{
		// ************************************************************************
		public TR_VScrol()//:base()
		{
			m_Size = new Size(20, 100);
		}
		// ************************************************************************
		// ************************************************************************
		public override void SetLocSize()
		{
			if ((m_form != null)&&(Sizes!=null))
			{
				int x = m_form.Width - m_Size.Width;
				int y = Sizes.MenuHeight + Sizes.InterHeight
					+ Sizes.CaptionHeight + Sizes.CaptionHeight2 + Sizes.InterHeight;
				Point p = new Point(x, y);
				if (m_Location != p) m_Location = p;
				int w = TR_Size.VScrolWidth;

				int h = m_form.Height
					- y
					- Sizes.InterHeight
					- TR_Size.HScrolHeight;
				Size sz = new Size(w, h);
				if(m_Size != sz) m_Size= sz;
				ChkOffScr();
			}
		}
		private PointF[] Arrow(int y, bool IsTop = true)
		{
			PointF[] ret = new PointF[4];

			if (IsTop)
			{
				ret[0] = new PointF(1, y + 5);
				ret[1] = new PointF(1, y);
				ret[2] = new PointF(13, y);
				ret[3] = new PointF(13, y + 5);
			}
			else
			{
				ret[0] = new PointF(1, y - 5);
				ret[1] = new PointF(1, y);
				ret[2] = new PointF(13, y);
				ret[3] = new PointF(13, y - 5);
			}
			return ret;
		}
		private PointF[] Cursor(int y)
		{
			PointF[] ret = new PointF[3];

			ret[0] = new PointF(10, y - 7);
			ret[1] = new PointF(1, y);
			ret[2] = new PointF(10, y+7);
			return ret;
		}
		private int DispYTo()
		{
			int ret = -1;
			if (m_form == null) return ret;
			int h = m_Size.Height - 20;
			if ((m_form.Sizes.DispMax.Y > 0)
				&& (m_form.Sizes.DispY >=0)
				)
			{
				ret = m_form.Sizes.DispY * h / m_form.Sizes.DispMax.Y;
				ret += 10;
			}
			
			return ret;
		}
		public override void Draw(Graphics g)
		{
			if (m_form == null) return;
			Pen p = new Pen(m_form.Colors.Line);
			SolidBrush sb = new SolidBrush(m_form.Colors.Line);
			try
			{
				g.Clear(Color.Transparent);
				p.Width = 2;
				g.DrawLines(p, Arrow(0, true));
				g.DrawLines(p, Arrow(Height-1, false));

				int v = DispYTo();
				if (v > 0)
				{
					p.Width = 1;
					p.Color = m_form.Colors.LineB;
					g.DrawLine(p, 7, 10, 7, Height - 11);
					g.DrawLine(p, 1, 10, 14, 10);
					g.DrawLine(p, 1, Height - 11, 14, Height - 11);
					Rectangle rr = new Rectangle(0, v - 10, 20,20);
					g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
					sb.Color = Color.Transparent;
					g.FillRectangle(sb, rr);
					g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
					p.Width = 3;
					p.Color = m_form.Colors.Line;
					g.DrawLines(p, Cursor(v));
				}
				/*
			Rectangle r = new Rectangle(0, 0, ArrowW, 5);
			g.FillRectangle(sb,r);
			r = new Rectangle(0,Height-6, ArrowW, 5);
			g.FillRectangle(sb, r);
			PointF [] ps = new PointF[]
			{
				new PointF(0, 20),
				new PointF(ArrowW/2, 10),
				new PointF(ArrowW, 20),
			};
			p.Width = 2;
			g.DrawLines(p, ps);
			ps = new PointF[]
			{
				new PointF(0, Height -20),
				new PointF(ArrowW/2, Height-10),
				new PointF(ArrowW, Height - 20),
			};
			g.DrawLines(p, ps);
			*/
			}
			finally
			{
				p.Dispose();
				sb.Dispose();
			}

		}
	}
}
