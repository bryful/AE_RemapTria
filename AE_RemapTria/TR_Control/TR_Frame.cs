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
namespace AE_RemapTria
{
	public class TR_Frame : TR_Control
	{
		public TR_Frame()
		{
			FontSize = 9;
			FontIndex = 5;
		}
		// ************************************************************************
		public override void SetTRForm(TR_Form fm, bool isI = true)
		{
			base.SetTRForm(fm, false);
			if (m_form != null)
			{
				ChkOffScr();
			}
			Invalidate();
		}

		// ********************************************************************
		public override void SetLocSize()
		{
			if (m_form == null) return;
			int x = 0;
			int y = m_form.Sizes.MenuHeight + m_form.Sizes.InterHeight
				+ m_form.Sizes.CaptionHeight + m_form.Sizes.CaptionHeight2 + m_form.Sizes.InterHeight;
			Point p = new Point(x, y);
			if (m_Location != p) m_Location = p;


			int ww = x + m_form.Sizes.InterWidth + TR_Size.VScrolWidth;
			int hh = y + m_form.Sizes.InterHeight + TR_Size.HScrolHeight;
			Size sz = new Size(
				m_form.Width - ww,
				m_form.Height - hh
				);
			if (m_Size != sz) m_Size = sz;
			ChkOffScr();
		}


		public override bool ChkMouseDown(MouseEventArgs e)
		{
			bool ret = false;
			ret = base.ChkMouseDown(e);
			if (m_inMouse == false) return ret;
			if ((m_form == null) || (Sizes == null)) return ret;
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				int y = (m_MDownPoint.Y + Sizes.DispY) / Sizes.CellHeight;

				m_form.SetCellStart(y);
				ret = true;
			}
			else if((e.Button & MouseButtons.Right) == MouseButtons.Right)
			{
				if (m_form != null)
				{
					ContextMenuStrip m = m_form.MakeCMFrame();
					Point p = Cursor.Position;
					m.Show(p);
					ret = true;
				}
			}
			return ret;
		}
		//-------------------------------------------------
		private PointF[] DotSecV(int y)
		{
			int x = Sizes.FrameWidth2 - 16;
			PointF[] ret = new PointF[]
			{
				new PointF(x,y-5),
				new PointF(x+12,y-5),
				new PointF(x+12,y+5),
				new PointF(x,y+5)
			};
			return ret;
		}
		private PointF[] DotHSecV(int y)
		{
			int x = Sizes.FrameWidth2 - 14;
			PointF[] ret = new PointF[]
			{
				new PointF(x,y-5),
				new PointF(x+10,y),
				new PointF(x,y+5),
			};
			return ret;
		}
		private PointF[] DotHHSecV(int y)
		{
			int x = Sizes.FrameWidth2 - 10;
			PointF[] ret = new PointF[]
			{
				new PointF(x,y-4),
				new PointF(x+8,y),
				new PointF(x,y+4),
			};
			return ret;
		}
		private PointF[] DotHHHSecV(int y)
		{
			int x = Sizes.FrameWidth2 - 5;
			PointF[] ret = new PointF[]
			{
				new PointF(x,y-2),
				new PointF(x+4,y),
				new PointF(x,y+2),
			};
			return ret;
		}
		//-------------------------------------------------
		private void DrawFrame(Graphics g, SolidBrush sb, Pen p, int f, Rectangle rct)
		{
			if (m_form == null) return;	
			int y = f * m_form.Sizes.CellHeight - m_form.Sizes.Disp.Y;
			rct.Y = y;

			bool isNoEnabled = !m_form.CellData.EnableFrame(f);
			bool isSelected = m_form.CellData.IsSelectedFrame(f);
			//フレームの背景
			if (isSelected == true)
			{
				sb.Color = m_form.Colors.Selection;
				Fill(g, sb, rct);
			}else if (isNoEnabled)
			{
				sb.Color = m_form.Colors.UnEnabled;
				Fill(g, sb, rct);
			}
			//フレーム番号を描く
			if(isNoEnabled) sb.Color = m_form.Colors.UnEnabledMoji;
			else sb.Color = m_form.Colors.Moji;
			Rectangle rct2 = new Rectangle(rct.X, rct.Y, rct.Width - 1, rct.Height);
			m_form.Alignment = StringAlignment.Far;
			DrawStr(g, m_form.CellData.FrameStr(f), sb, rct2);


			p.Width = 1;

			sb.Color = m_form.Colors.FrameDot;
			int SecBar = 24;
			int HSecBar = 12;
			int HHSecBar = 6;
			switch (m_form.CellData.FrameRate)
			{
				case T_Fps.FPS24:
					SecBar = 24;
					HSecBar = 12;
					HHSecBar = 6;
					break;
				case T_Fps.FPS30:
					SecBar = 30;
					HSecBar = 15;
					HHSecBar = 5;
					break;
			}
			if (f % SecBar == 0)
			{
				g.FillPolygon(sb, DotSecV(y));
				p.Color = m_form.Colors.GLine;
				DrawHorLine(g, p, rct.Left, rct.Right, y +1);
				DrawHorLine(g, p, rct.Left, rct.Right, y);
			}
			else if (f % HSecBar == 0)
			{
				g.FillPolygon(sb, DotHSecV(y));
				p.Color = m_form.Colors.GLine;
				DrawHorLine(g, p, rct.Left, rct.Right, y );

			}
			else
			{
				if (f % HHSecBar == 0)
				{
					g.FillPolygon(sb, DotHHSecV(y));
					p.Color = m_form.Colors.GLineHor;
					int y2 = y - 1;
					DrawHorLine(g, p, rct.Left, rct.Right, y2);
					DrawHorLine(g, p, rct.Left, rct.Right, y2+1);
				}
				else
				{
					g.FillPolygon(sb, DotHHHSecV(y));
					p.Color = m_form.Colors.GLineHor;
					DrawHorLine(g, p, m_form.Sizes.FrameWidth2, rct.Right - 1, y);

				}
			}
		}
		//-------------------------------------------------
		public override void Draw(Graphics g)
		{
			Pen p = new Pen(Color.Black);
			SolidBrush sb = new SolidBrush(Color.Transparent);
			try
			{
				//とりあえず塗りつぶし
				g.Clear(Color.Transparent);
				//T_G.GradFrame(g, this.ClientRectangle);

				if (m_form != null)
				{
					Rectangle rct = new Rectangle(
						m_form.Sizes.FrameWidth2,
						0,
						m_form.Sizes.FrameWidth - m_form.Sizes.FrameWidth2,
						m_form.Sizes.CellHeight);

					Rectangle r = m_form.Sizes.DispCell;
					for (int i = r.Top; i <= r.Bottom; i++)
					{
						DrawFrame(g, sb, p, i, rct);
					}
					rct.Height = this.Height;
					p.Color = m_form.Colors.GLine;
					DrawFrame(g, p, rct);
				}
			}
			finally
			{
				p.Dispose();
				sb.Dispose();
			}

		}
	}
}
