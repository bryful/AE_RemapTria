using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PdfSharpCore.Drawing;

namespace AE_RemapTria
{
    public partial class TR_Grid : TR_Control
    {

        public TR_Grid() //: base()
		{
			m_FontIndex = 5;
			m_FontSize = 9;
		}
        // *************************************************************************************************
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
			int leftW = Sizes.FrameWidth + Sizes.InterWidth;
			int topW = Sizes.MenuHeight + Sizes.InterHeight
				+ Sizes.CaptionHeight + Sizes.CaptionHeight2 + Sizes.InterHeight;

			Point p = new Point(leftW, topW);
			if (m_Location != p)m_Location = p;

			int ww = leftW + Sizes.InterWidth + TR_Size.VScrolWidth;
			int hh = topW + Sizes.InterHeight + TR_Size.HScrolHeight;
			Size sz = new Size(
				m_form.Width - ww,
				m_form.Height - hh
				);
			if (m_Size != sz)
			{
				m_Size = sz;
				//Sizes.SizeSetting();
			}
			ChkOffScr();
		}

		//------------------------------------------------- 
		private void DrawCell(Graphics g, SolidBrush sb, Pen p, int c, int f)
		{
			int x = c * Sizes.CellWidth - Sizes.DispX;
			int y = f * Sizes.CellHeight - Sizes.DispY;

			Rectangle r = new Rectangle(x, y, Sizes.CellWidth, Sizes.CellHeight);
			//塗りつぶし
			bool IsSel = (CellData.IsSelected(c, f));
			bool IsUnEnabled = !CellData.EnableFrame(f);
			if (IsSel == true)
			{
				sb.Color = Colors.Selection;
				g.FillRectangle(sb, r);
			}
			else if (IsUnEnabled)
			{
				sb.Color = Colors.Gray;
				g.FillRectangle(sb, r);
			}

			//文字を書く
			CellSatus cs = CellData.GetCellStatus(c, f);
			switch (cs.Status)
			{
				case CellType.Normal:
					if (IsUnEnabled) sb.Color = Colors.GrayMoji;
					else sb.Color = Colors.Moji;
					m_form.Alignment = StringAlignment.Center;
					DrawStr(g, CellData.GetCellData(c, f).ToString(), sb, r);
					break;
				case CellType.SameAsBefore:
					if (IsUnEnabled) p.Color = Colors.GrayMoji;
					else p.Color = Colors.LineDark;
					DrawVerLine(g, p, r.Left + r.Width / 2, r.Top + 2, r.Bottom - 2);
					break;
				case CellType.EmptyStart:
					if (IsUnEnabled) p.Color = Colors.GrayMoji;
					else p.Color = Colors.Moji;
					DrawBatsu(g, p, r);
					break;
				case CellType.None:
					break;
			}

		}
		private void DrawFrameLine(Graphics g, Pen p, int f)
		{
			int y = Sizes.CellHeight * f - Sizes.DispY;
			int Sec = 24;
			int HSec = 12;
			int HHSec = 6;
			switch (CellData.FrameRate)
			{
				case T_Fps.FPS24:
					Sec = 24;
					HSec = 12;
					HHSec = 6;
					break;
				case T_Fps.FPS30:
					Sec = 30;
					HSec = 15;
					HHSec = 5;
					break;
			}
			if (f % Sec == 0)
			{
				p.Color = Colors.Line;
				DrawHorLine(g, p, 0, this.Width, y);
				DrawHorLine(g, p, 0, this.Width, y + 1);
			}
			else if (f % HSec == 0)
			{
				p.Color = Colors.Line;
				DrawHorLine(g, p, 0, this.Width, y);

			}
			else
			{
				if (f % HHSec == 0)
				{
					p.Color = Colors.Line;
					DrawHorLine(g, p, 0, this.Width, y);
				}
				else
				{
					p.Color = Colors.LineB;
					DrawHorLine(g, p, 0, this.Width, y);
				}
			}
		}
		private bool IsDrawOffScr = false;
		public override void Draw(Graphics g)
		{
			Debug.WriteLine("Grid");
			if (IsDrawOffScr) return;
			if (m_form == null) return;
			IsDrawOffScr = true;
			Pen p = new Pen(Color.Black);
			SolidBrush sb = new SolidBrush(Color.White);
			try
			{
				//T_G.GradBG(g, this.ClientRectangle);
				int c0 = Sizes.DispX / Sizes.CellWidth - 1;
				if (c0 < 0) c0 = 0;
				int c1 = (Sizes.DispX + this.Width) / Sizes.CellWidth + 1;
				if (c1 >= CellData.CellCount) c1 = CellData.CellCount - 1;

				int f0 = Sizes.DispY / Sizes.CellHeight - 1;
				if (f0 < 0) f0 = 0;
				int f1 = (Sizes.DispY + this.Height) / Sizes.CellHeight + 1;
				if (f1 >= CellData.FrameCount) c1 = CellData.FrameCount - 1;

				Rectangle r = Sizes.DispCell;


				for (int c = r.Left; c <= r.Right; c++)
				{
					int x = c * Sizes.CellWidth - Sizes.DispX;
					int x2 = x + Sizes.CellWidth - 1;
					//偶数のセルを塗る
					if (c == CellData.TargetIndex)
					{
						T_G.GradBGCurrent(g, new Rectangle(x, 0, Sizes.CellWidth, this.Height));
					}
					else if (c % 2 == 0)
					{

						T_G.GradBGEven(g, new Rectangle(x, 0, Sizes.CellWidth, this.Height));
					}


					for (int f = r.Top; f <= r.Bottom; f++)
					{
						DrawCell(g, sb, p, c, f);
						//横線を描く
						DrawFrameLine(g, p, f);
					}
					//縦線を描く
					p.Width = 1;
					p.Color = Colors.LineDark;
					DrawVerLine(g, p, x2, 0, this.Height);
					if (c == CellData.TargetIndex)
					{
						DrawVerLine(g, p, x2 - 1, 0, this.Height);
						DrawVerLine(g, p, x, 0, this.Height);
					}
				}
				p.Color = Colors.Line;
				DrawFrame(g, p, new Rectangle(0, 0, Width, Height));
			}
			catch
			{
				Debug.WriteLine("Grid Print Error");
			}
			finally
			{
				p.Dispose();
				sb.Dispose();
			}

			IsDrawOffScr = false;

		}
		// *****************************************************
		public void DrawOne(Graphics g ,int c)
		{
			if (IsDrawOffScr) return;
			if (m_form == null) return;
			IsDrawOffScr = true;
			Pen p = new Pen(Color.Black);
			SolidBrush sb = new SolidBrush(Color.White);
			try
			{
				int c0 = Sizes.DispX / Sizes.CellWidth - 1;
				if (c0 < 0) c0 = 0;
				int c1 = (Sizes.DispX + this.Width) / Sizes.CellWidth + 1;
				if (c1 >= CellData.CellCount) c1 = CellData.CellCount - 1;

				int f0 = Sizes.DispY / Sizes.CellHeight - 1;
				if (f0 < 0) f0 = 0;
				int f1 = (Sizes.DispY + this.Height) / Sizes.CellHeight + 1;
				if (f1 >= CellData.FrameCount) c1 = CellData.FrameCount - 1;

				Rectangle r = Sizes.DispCell;
				if((c>= r.Left)&&(c<=r.Right))
				{
					int x = c * Sizes.CellWidth - Sizes.DispX;
					int x2 = x + Sizes.CellWidth - 1;
					// 消去
					sb.Color = Color.Transparent;
					g.FillRectangle(sb, new Rectangle(x, 0, Sizes.CellWidth, m_Size.Height));

					//偶数のセルを塗る
					if (c == CellData.TargetIndex)
					{
						T_G.GradBGCurrent(g, new Rectangle(x, 0, Sizes.CellWidth, this.Height));
					}
					else if (c % 2 == 0)
					{

						T_G.GradBGEven(g, new Rectangle(x, 0, Sizes.CellWidth, this.Height));
					}


					for (int f = r.Top; f <= r.Bottom; f++)
					{
						DrawCell(g, sb, p, c, f);
						//横線を描く
						DrawFrameLine(g, p, f);
					}
					//縦線を描く
					p.Width = 1;
					p.Color = Colors.LineDark;
					DrawVerLine(g, p, x2, 0, this.Height);
					if (c == CellData.TargetIndex)
					{
						DrawVerLine(g, p, x2 - 1, 0, this.Height);
						DrawVerLine(g, p, x, 0, this.Height);
					}
				}
				p.Color = Colors.Line;
				DrawFrame(g, p, new Rectangle(0, 0, Width, Height));
			}
			catch
			{
				Debug.WriteLine("Grid Print Error");
			}
			finally
			{
				p.Dispose();
				sb.Dispose();
			}

			IsDrawOffScr = false;

		}
		public void ChkOffScrOne(int idx)
		{
			if (m_form != null)
			{
				Graphics g = Graphics.FromImage(m_OffScr);
				g.SmoothingMode = SmoothingMode.AntiAlias;
				DrawOne(g,idx);
			}
		}
		public void ChkOffScrOne()
		{
			if (m_form != null)
			{
				ChkOffScrOne(m_form.Selection.Target);
			}
		}
		private int m_mdFrame = -1;
		private int m_mdCell = -1;
		private int m_CopyFrame = -1;
		private Point m_md = new Point(-1, -1);
		private Point m_disp = new Point(-1, -1);
		public override bool ChkMouseDown(MouseEventArgs e)
		{
			bool ret = false; 
			base.ChkMouseDown(e);
			if (m_inMouse == false) return ret;
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				ret = true;
				Point cp = Sizes.PosCell(m_MDownPoint.X, m_MDownPoint.Y);
				if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
				{
					int y0 = CellData.sel.Start;
					int y1 = CellData.sel.LastFrame;
					if (cp.Y <= y0)
					{
						y0 = cp.Y;
					}
					else
					{
						y1 = cp.Y;
					}
					CellData.sel.Set2Frame(y0, y1);
				}
				else if ((Control.ModifierKeys & Keys.Alt) == Keys.Alt)
				{
					CellData.sel.SelToEnd();
				}
				else
				{
					m_mdFrame = cp.Y;
					m_mdCell = cp.X;
					CellData.sel.SetTargetStartLength(cp.X, cp.Y, 1);
					if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
					{
						m_CopyFrame = CellData.GetCellData(cp.X, cp.Y);
						CellData.PushUndo(BackupSratus.NumberInput);
					}
					else
					{
						m_CopyFrame = -1;
					}

				}
				ChkOffScr();
				if (m_form != null) m_form.Caption.ChkOffScr();
				if (m_form!=null) m_form.Frame.ChkOffScr();
				Invalidate();
			}else if((e.Button & MouseButtons.Right) == MouseButtons.Right)
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
		public override bool ChkMouseMove(MouseEventArgs e)
		{
			bool ret = false;
			ret = base.ChkMouseDown(e);
			if (m_inMouse == false) return ret;
			if((CellData==null)||(Sizes==null)) return ret;
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				if (m_mdFrame >= 0)
				{
					bool b = CellData._undoPushFlag;
					if (m_CopyFrame >= 0)
					{
						CellData._undoPushFlag = false;
					}
					if (e.Y >= this.Height)
					{
						Sizes.DispY += Sizes.CellHeight;
					}
					else if (e.Y < 0)
					{
						Sizes.DispY -= Sizes.CellHeight;
					}
					Point cp = Sizes.PosCell(m_MDownPoint.X, m_MDownPoint.Y);
					CellData.sel.Set2Frame(m_mdFrame, cp.Y);
					if (m_CopyFrame >= 0)
					{
						CellData.SetCellNum(m_CopyFrame, false);
					}
					CellData._undoPushFlag = b;
					ChkOffScrOne(m_mdCell);
					if (m_form != null) m_form.Frame.ChkOffScr();
					Invalidate();
				}
			}
			return ret;
		}
		public override bool ChkMouseUp(MouseEventArgs e)
		{
			bool ret = false;
			base.ChkMouseDown(e);
			if (m_inMouse == false) return ret; 
			if (m_mdFrame >= 0)
			{
				m_mdFrame = -1;
				m_mdCell = -1;
				ChkOffScr();
				if (m_form != null) m_form.Frame.ChkOffScr();
				if (m_form != null) m_form.Caption.ChkOffScr();
				Invalidate();
			}
			m_md = new Point(-1, -1);
			m_disp = new Point(-1, -1);
			m_CopyFrame = -1;
			return ret;
		}
		public override bool ChkMouseLeave(EventArgs e)
		{
			if (m_mdFrame >= 0)
			{
				m_mdFrame = -1;
			}
			m_mdFrame = -1;
			m_CopyFrame = -1;
			return base.ChkMouseLeave(e);
		}

	}
}
