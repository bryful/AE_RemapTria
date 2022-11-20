using BRY;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE_RemapTria
{
	public partial class T_Grid
	{
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
					DrawStr(g, CellData.GetCellData(c, f).ToString(), sb, r);
					break;
				case CellType.SameAsBefore:
					if (IsUnEnabled) p.Color = Colors.GrayMoji;
					else p.Color = Colors.LineDark;
					DrawVerLine(g, p, r.Left + r.Width / 2, r.Top+2, r.Bottom - 2);
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
		private void DrawFrameLine(Graphics g, Pen p,int f)
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
				DrawHorLine(g, p, 0, this.Width, y+1);
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
		protected override void OnPaint(PaintEventArgs pe)
		{
			Pen p = new Pen(Color.Black);
			SolidBrush sb = new SolidBrush(Color.White);
			Graphics g = pe.Graphics;
			//g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
			try
			{
				T_G.GradBG(g, this.ClientRectangle);

				int c0 = Sizes.DispX / Sizes.CellWidth - 1;
				if (c0 < 0) c0 = 0;
				int c1 = (Sizes.DispX+this.Width) / Sizes.CellWidth +1;
				if (c1 >= CellData.CellCount) c1 = CellData.CellCount - 1;

				int f0 = Sizes.DispY / Sizes.CellHeight - 1;
				if (f0 < 0) f0 = 0;
				int f1 = (Sizes.DispY + this.Height) / Sizes.CellHeight + 1;
				if (f1 >= CellData.FrameCount) c1 = CellData.FrameCount - 1;

				Rectangle r = Sizes.DispCell;


				for (int c = r.Left; c <= r.Right; c++)
				//for (int c=c0; c<=c1; c++)
				{
					int x = c * Sizes.CellWidth - Sizes.DispX;
					int x2 = x + Sizes.CellWidth - 1;
					//偶数のセルを塗る
					if (c == CellData.TargetIndex) 
					{
						T_G.GradBGCurrent(g, new Rectangle(x, 0, Sizes.CellWidth, this.Height));
					}
					else if (c%2==0)
					{

						T_G.GradBGEven(g, new Rectangle(x, 0, Sizes.CellWidth, this.Height));
					}


					for (int f = r.Top; f <= r.Bottom; f++)
					//for (int f = f0; f <= f1; f++)
					{
						DrawCell(g, sb, p, c, f);
						//横線を描く
						DrawFrameLine(g, p,f);
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

				//g.DrawImage(m_offscr, Sizes.DispX * -1, Sizes.DispY * -1);
				p.Color = Colors.Line;
				DrawFrame(g, p);
			}
			finally
			{
				p.Dispose();
				sb.Dispose();
			}


		}
		//-------------------------------------------------
		public bool WritePDF(string p)
		{
			return T_PDF.ExportPDF(p,this);
		}
		//-------------------------------------------------
		public bool WritePDF()
		{
			bool ret = false;
			SaveFileDialog dlg = new SaveFileDialog();
			dlg.Filter = "*.pdf|*.pdf|*.*|*.*";
			if (FileName != "")
			{
				dlg.InitialDirectory = T_Def.GetDir(FileName);
				dlg.FileName = T_Def.GetNameNoExt(FileName) + ".pdf";
			}
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				ret = WritePDF(T_Def.ChangeExt(dlg.FileName, ".pdf"));
				FileName = T_Def.ChangeExt(dlg.FileName,".ardj.json");
			}
			dlg.Dispose();
			return ret;
		}
		//-------------------------------------------------
		/*
		private void DrawCellOffScr(Graphics g, SolidBrush sb, Pen p, int c, int f)
		{
			int x = c * Sizes.CellWidth;
			int y = f * Sizes.CellHeight;

			Rectangle r = new Rectangle(x, y, Sizes.CellWidth, Sizes.CellHeight);
			//塗りつぶし
			sb.Color = Color.FromArgb(0, 0, 0, 0);
			g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
			g.FillRectangle(sb, new Rectangle(x, y, Sizes.CellWidth - 1, Sizes.CellHeight - 1));
			//g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
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
			else
			{
				if (c % 2 == 0)
				{
					sb.Color = Colors.CellEven;
					g.FillRectangle(sb, r);
				}
			}

			//文字を書く
			CellSatus cs = CellData.GetCellStatus(c, f);
			switch (cs.Status)
			{
				case CellType.Normal:
					if (IsUnEnabled) sb.Color = Colors.GrayMoji;
					else sb.Color = Colors.Moji;
					g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
					DrawStr(g, CellData.GetCellData(c, f).ToString(), sb, r);
					g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
					break;
				case CellType.SameAsBefore:
					if (IsUnEnabled) p.Color = Colors.GrayMoji;
					else p.Color = Colors.LineB;
					DrawVerLine(g, p, r.Left + r.Width / 2, r.Top, r.Bottom - 1);
					break;
				case CellType.EmptyStart:
					if (IsUnEnabled) p.Color = Colors.GrayMoji;
					else p.Color = Colors.Moji;
					DrawBatsu(g, p, r);
					break;
				case CellType.None:
					break;
			}
			//枠線を書く
			p.Width = 1;
			p.Color = Colors.LineA;
			DrawVerLine(g, p, r.Left, r.Top, r.Bottom - 1);
			DrawVerLine(g, p, r.Right, r.Top, r.Bottom - 1);

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
				DrawHorLine(g, p, r.Left, r.Right, r.Top);
				DrawHorLine(g, p, r.Left, r.Right, r.Top - 1);
				DrawHorLine(g, p, r.Left, r.Right, r.Top + 1);
			}
			else if (f % HSec == 0)
			{
				p.Color = Colors.Line;
				DrawHorLine(g, p, r.Left, r.Right, r.Top - 1);
				DrawHorLine(g, p, r.Left, r.Right, r.Top);

			}
			else
			{
				if (f % HHSec == 0)
				{
					p.Color = Colors.Line;
					DrawHorLine(g, p, r.Left, r.Right, r.Top);
				}
				else
				{
					p.Color = Colors.LineB;
					DrawHorLine(g, p, r.Left, r.Right, r.Top);
				}
			}
		}
		public void DrawAllOffScr()
		{
			Graphics g = Graphics.FromImage(m_offscr);
			g.SmoothingMode = SmoothingMode.AntiAlias;
			//g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
			SolidBrush sb = new SolidBrush(this.BackColor);
			Pen p = new Pen(this.ForeColor);

			try
			{
				for (int c = 0; c < CellData.CellCount; c++)
				{
					for (int f = 0; f < CellData.FrameCount; f++)
					{
						DrawCellOffScr(g, sb, p, c, f);
					}

				}
			}
			catch
			{
				sb.Dispose();
				p.Dispose();
			}
		}
		public void DrawCellOffScr()
		{
			DrawCellOffScr(CellData.Selection.Target);
		}
		public void DrawCellOffScr(int c)
		{
			Graphics g = Graphics.FromImage(m_offscr);
			g.SmoothingMode = SmoothingMode.AntiAlias;
			//g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
			SolidBrush sb = new SolidBrush(this.BackColor);
			Pen p = new Pen(this.ForeColor);

			try
			{
				for (int f = 0; f < CellData.FrameCount; f++)
				{
					DrawCellOffScr(g, sb, p, c, f);
				}
			}
			catch
			{
				sb.Dispose();
				p.Dispose();
			}
		}
		public void DrawOffScr()
		{
			Graphics g = Graphics.FromImage(m_offscr);
			g.SmoothingMode = SmoothingMode.AntiAlias;
			SolidBrush sb = new SolidBrush(this.BackColor);
			Pen p = new Pen(this.ForeColor);

			try
			{
				T_Selection bsel = CellData.GetUndoSel();
				for (int f = 0; f < bsel.Length; f++)
				{
					int idx = bsel.Start + f;
					if ((idx >= 0) && (idx < CellData.FrameCount))
					{
						DrawCellOffScr(g, sb, p, bsel.Target, idx);
					}
				}
				for (int f = 0; f < CellData.Selection.Length; f++)
				{
					int idx = CellData.Selection.Start + f;
					if ((idx >= 0) && (idx < CellData.FrameCount))
					{
						DrawCellOffScr(g, sb, p, CellData.Selection.Target, idx);
					}
				}
			}
			finally
			{
				sb.Dispose();
				p.Dispose();
			}


		}
		public void ChkOffScr()
		{
			int w = Sizes.CellWidth * CellData.CellCount;
			int h = Sizes.CellHeight * CellData.FrameCount;
			if((w != m_offscr.Width)||(h!=m_offscr.Height))
			{
				m_offscr = new Bitmap(w, h, PixelFormat.Format32bppArgb);
				//DrawAllOffScr();
			}
		}
	*/
	}
}
