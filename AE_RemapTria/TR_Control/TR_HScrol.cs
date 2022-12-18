using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE_RemapTria
{
	public class TR_HScrol :TR_Control
	{
		public TR_HScrol()//:base()
		{
			m_Size = new Size(100, 20);
		}
		// ************************************************************************
		public override void SetLocSize()
		{
			if ((m_form != null) && (Sizes != null))
			{
				int x = Sizes.FrameWidth +Sizes.InterWidth;
				int y = m_form.Height - TR_Size.HScrolHeight;
				Point p = new Point(x, y);
				if (m_Location != p) m_Location = p;
				int h = TR_Size.HScrolHeight;

				int w = m_form.Width
					- x
					- Sizes.InterWidth
					- TR_Size.VScrolWidth;
				Size sz = new Size(w, h);
				if (m_Size != sz) m_Size = sz;
				ChkOffScr();
			}

		}
		private PointF[] Arrow(int x, bool IsLeft = true)
		{
			PointF[] ret = new PointF[4];

			if (IsLeft)
			{
				ret[0] = new PointF(x + 5, 1);
				ret[1] = new PointF(x, 1);
				ret[2] = new PointF(x, 13);
				ret[3] = new PointF(x + 5, 13);
			}
			else
			{
				ret[0] = new PointF(x-5, 1);
				ret[1] = new PointF(x, 1);
				ret[2] = new PointF(x,13);
				ret[3] = new PointF(x-5,13);
			}
			return ret;
		}
		private PointF[] Cursor(int x)
		{
			PointF[] ret = new PointF[3];

			ret[0] = new PointF(x - 7, 10);
			ret[1] = new PointF(x, 1);
			ret[2] = new PointF(x + 7, 10);
			return ret;
		}
		private int DispXTo()
		{
			int ret = -1;
			if (m_form == null) return ret;
			int w = m_Size.Width - 20;
			if ((m_form.Sizes.DispMax.X > 0)
				&& (m_form.Sizes.DispX >= 0)
				)
			{
				ret = m_form.Sizes.DispX * w / m_form.Sizes.DispMax.X;
				ret += 10;
			}

			return ret;
		}
		public override void Draw(Graphics g)
		{
			if (m_form == null) return;
			Pen p = new Pen(m_form.Colors.GLine);
			SolidBrush sb = new SolidBrush(m_form.Colors.GLine);
			try
			{
				g.Clear(Color.Transparent);
				p.Width = 2;
				g.DrawLines(p, Arrow(0, true));
				g.DrawLines(p, Arrow(Width - 1, false));

				int v = DispXTo();
				if (v > 0)
				{
					p.Width = 1;
					p.Color = m_form.Colors.GLineHor;
					g.DrawLine(p, 10, 7, Width - 11, 7);
					g.DrawLine(p, 10, 1, 10, 14);
					g.DrawLine(p, Width - 11, 1, Width - 11, 14);
					Rectangle rr = new Rectangle(v - 10, 0, 20, 20);
					g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
					sb.Color = Color.Transparent;
					g.FillRectangle(sb, rr);
					g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
					p.Width = 3;
					p.Color = m_form.Colors.GLine;
					g.DrawLines(p, Cursor(v));
				}
			}
			finally
			{
				p.Dispose();
				sb.Dispose();
			}

		}
		private enum MDPosType
		{
			None = -1,
			LeftMax,
			Left,
			Cursol,
			Right,
			RightMax,
		};
		private MDPosType m_MDPos = MDPosType.None;

		private MDPosType GetMDPos(int x)
		{
			MDPosType ret = MDPosType.None;
			if (x <= 10)
			{
				ret = MDPosType.LeftMax;
			}
			else if (this.Width - 10 <= x)
			{
				ret = MDPosType.RightMax;
			}
			else
			{
				int v = DispXTo();
				if ((x >= v - 10) && (x <= v + 10))
				{
					ret = MDPosType.Cursol;
				}
				else if (x < v - 10)
				{
					ret = MDPosType.Left;
				}
				else
				{
					ret = MDPosType.Right;
				}
			}

			return ret;
		}
		int m_mdp = -1;
		int m_dispx = -1;
		// ****************************************************************
		public override bool ChkMouseDown(MouseEventArgs e)
		{
			bool ret = false;
			ret = base.ChkMouseDown(e);
			if (m_inMouse == false) return ret;
			if ((m_form == null) || (Sizes == null)) return ret;
			if (e.Button == MouseButtons.Left)
			{
				m_MDPos = GetMDPos(m_MDownPoint.X);
				if (m_MDPos == MDPosType.Cursol)
				{
					m_mdp = m_MDownPoint.X;
					m_dispx = Sizes.DispX;
				}
				ret = true;
			}
			return ret;
		}
		public override bool ChkMouseMove(MouseEventArgs e)
		{
			bool ret = false;
			Debug.WriteLine($"MouseMove");
			ret = base.ChkMouseMove(e);
			if ((m_form == null) || (Sizes == null)) return ret;
			if (m_MDPos == MDPosType.Cursol)
			{
				int ax = m_MMovePoint.X - m_mdp;
				ax = ax * m_form.Sizes.DispMax.X / (Width - 20);
				Sizes.DispX = m_dispx + ax;
				m_form.DrawAll();
				m_form.Refresh();
				ret= true;
			}
			return ret;
		}
		public override bool ChkMouseUp(MouseEventArgs e)
		{
			bool ret = false;
			ret = base.ChkMouseUp(e);
			if ((m_form == null) || (Sizes == null)) return ret;
			if (m_MDPos == MDPosType.None) return ret;
			switch (m_MDPos)
			{
				case MDPosType.LeftMax:
					m_form.PageLeftMax();
					break;
				case MDPosType.RightMax:
					m_form.PageRightMax();
					break;
				case MDPosType.Left:
					m_form.PageLeft();
					break;
				case MDPosType.Right:
					m_form.PageRight();
					break;
			}
			m_MDPos = MDPosType.None;
			ret = true;
			return ret;
		}
	}
}
