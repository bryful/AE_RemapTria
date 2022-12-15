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
	public class TR_Frame :TR_Control
	{

		private Rectangle Dot1 = new Rectangle(5, 5, 12, 10);
		private Rectangle Dot2 = new Rectangle(5, 8, 8, 4);
		private Rectangle Dot3 = new Rectangle(5, 9, 4, 2);

		public TR_Frame()
		{
			FontSize = 9;
			FontIndex = 5;
		}
		// ************************************************************************
		public override void SetTRForm(TR_Form fm,bool isI=true)
		{
			base.SetTRForm(fm,false);
			if (m_form != null)
			{
				ChkOffScr();
			}
			Invalidate();
		}


		private void CellData_CountChanged(object? sender, EventArgs e)
		{
			ChkDot();
			this.Invalidate();
		}

		private void Sizes_ChangeDisp(object? sender, EventArgs e)
		{
			this.Invalidate();
		}


		//--------------------------------------------
		private void ChangedEvent(object? sender, EventArgs e)
		{
			this.Invalidate();
		}

		private void Sizes_ChangeDispMax(object? sender, EventArgs e)
		{
			ChkDot();
			this.Invalidate();
		}

		private void Sizes_ChangeGridSize(object? sender, EventArgs e)
		{
			ChkDot();
			this.Invalidate();
		}
		// ********************************************************************
		public override void SetLocSize()
		{
			if(m_form==null) return;
			int x = 0;
			int y = m_form.Sizes.MenuHeight + m_form.Sizes.InterHeight
				+ m_form.Sizes.CaptionHeight + m_form.Sizes.CaptionHeight2 + m_form.Sizes.InterHeight;
			Point p = new Point(x, y);
			if(m_Location != p) m_Location = p;


			int ww = x + m_form.Sizes.InterWidth + TR_Size.VScrolWidth;
			int hh = y + m_form.Sizes.InterHeight + TR_Size.HScrolHeight;
			Size sz = new Size(
				m_form.Width - ww,
				m_form.Height - hh
				);
			if (m_Size != sz) m_Size = sz;
			ChkOffScr();
		}

		private void ChkDot()
		{
			if (m_form != null)
			{
				int w = m_form.Sizes.FrameWidth2 * 2 / 3;
				int h = m_form.Sizes.CellHeight / 2;
				int l = (m_form.Sizes.FrameWidth2 - w) / 2;
				int t = -h / 2;
				int l2 = l + w;
				Dot1 = new Rectangle(l, t, w, h);
				w = w * 2 / 3;
				//h = h/2;
				t = -h / 2;
				l = l2 - w;
				Dot2 = new Rectangle(l, t, w, h);
				w = w / 2;
				h = h / 2;
				t = -h / 2;
				l = l2 - w;
				Dot3 = new Rectangle(l, t, w, h);
			}
		}

 		public override bool ChkMouseDown(MouseEventArgs e)
		{
			bool ret = false;
			ret = base.ChkMouseDown(e);
			if (m_inMouse == false) return ret;
			if ((m_form == null) || (Sizes == null)) return ret;
			if (e.Button == MouseButtons.Left)
			{
				int y = (m_MDownPoint.Y + Sizes.DispY)/ Sizes.CellHeight;

				m_form.SetCellStart(y);
				ret = true;
			}
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
				sb.Color = m_form.Colors.Gray;
				Fill(g, sb, rct);
			}
			//フレーム番号を描く
			if(isNoEnabled) sb.Color = m_form.Colors.GrayMoji;
			else sb.Color = m_form.Colors.Moji;
			Rectangle rct2 = new Rectangle(rct.X, rct.Y, rct.Width - 1, rct.Height);
			m_form.Alignment = StringAlignment.Far;
			DrawStr(g, m_form.CellData.FrameStr(f), sb, rct2);

			// 下の横線を描く
			//p.Color = m_grid.Colors.LineB;
			//p.Width = 1;
			//DrawHorLine(g, p, m_grid.Sizes.FrameWidth2, rct.Right - 1, y);

			p.Width = 1;

			sb.Color = m_form.Colors.Kagi;
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
				Rectangle r = new Rectangle(Dot1.Location, Dot1.Size);
				r.Y += y;
				Fill(g, sb, r);
				p.Color = m_form.Colors.Line;
				DrawHorLine(g, p, rct.Left, rct.Right, y +1);
				DrawHorLine(g, p, rct.Left, rct.Right, y);
				DrawHorLine(g, p, rct.Left, rct.Right, y-1);
			}
			else if (f % HSecBar == 0)
			{
				Rectangle r = new Rectangle(Dot2.Location, Dot2.Size);
				r.Y += y;
				Fill(g, sb, r);
				p.Color = m_form.Colors.Line;
				DrawHorLine(g, p, rct.Left, rct.Right, y );
				DrawHorLine(g, p, rct.Left, rct.Right, y-1);

			}
			else
			{
				Rectangle r = new Rectangle(Dot3.Location, Dot3.Size);
				r.Y += y;
				Fill(g, sb, r);
				if (f % HHSecBar == 0)
				{
					p.Color = m_form.Colors.LineDark;
					int y2 = y - 1;
					DrawHorLine(g, p, rct.Left, rct.Right, y2);
				}
				else
				{
					p.Color = m_form.Colors.LineB;
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
					p.Color = m_form.Colors.Line;
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
