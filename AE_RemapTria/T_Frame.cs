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
#pragma warning disable CS8600 // Null リテラルまたは Null の可能性がある値を Null 非許容型に変換しています。
#pragma warning disable CS8603 // Null 参照戻り値である可能性があります。
	public class T_Frame :T_BaseControl
	{
		private T_Grid? m_grid = null;

		private Rectangle Dot1 = new Rectangle(5, 5, 12, 10);
		private Rectangle Dot2 = new Rectangle(5, 8, 8, 4);
		private Rectangle Dot3 = new Rectangle(5, 9, 4, 2);

		public T_Frame()
		{
			Init();
			Alignment = StringAlignment.Far;
			MyFontSize = 9;

		}
		protected override void InitLayout()
		{
			base.InitLayout();
			ChkGrid();
			MyFontSize = 9;
			Alignment = StringAlignment.Far;
		}
		//-------------------------------------------
		public T_Grid Grid
		{
			get { return m_grid; }

			set
			{
				m_grid = value;
				ChkGrid();
			}
		}
		//------------------------------------------
		private void ChkGrid()
		{
			if (m_grid != null)
			{

				ChkMinMax();
				SetLocSize();
				m_grid.Sizes.ChangeGridSize += Sizes_ChangeGridSize;
				m_grid.Sizes.ChangeDispMax += Sizes_ChangeDispMax;
				m_grid.Sizes.ChangeDisp += Sizes_ChangeDisp;

				m_grid.CellData.SelChanged += ChangedEvent; ;
				m_grid.CellData.ValueChanged += ChangedEvent;
				m_grid.Colors.ColorChangedEvent += ChangedEvent;

				m_grid.LocationChanged += M_grid_LocationChanged;
				m_grid.SizeChanged+= M_grid_LocationChanged;
			}
		}

		private void Sizes_ChangeDisp(object? sender, EventArgs e)
		{
			this.Invalidate();
		}

		private void M_grid_LocationChanged(object? sender, EventArgs e)
		{
			SetLocSize();
		}

		//--------------------------------------------
		private void ChangedEvent(object? sender, EventArgs e)
		{
			this.Invalidate();
		}

		private void Sizes_ChangeDispMax(object? sender, EventArgs e)
		{
			this.Invalidate();
		}

		private void Sizes_ChangeGridSize(object? sender, EventArgs e)
		{

			this.Invalidate();
		}
		// ********************************************************************
		private void SetLocSize()
		{
			if (m_grid == null) return;
			int x = m_grid.Left - (m_grid.Sizes.FrameWidth + m_grid.Sizes.InterWidth);

			Point p = new Point(x, m_grid.Top);
			if(this.Location != p) this.Location = p;
			if(this.Height != m_grid.Height) this.Height = m_grid.Height;
		}
		//-------------------------------------------------
		private void ChkMinMax()
		{
			if (m_grid != null) {
				this.MinimumSize = new Size(m_grid.Sizes.FrameWidth, m_grid.Sizes.CellHeight * 6);
				this.MaximumSize = new Size(m_grid.Sizes.FrameWidth, m_grid.Sizes.CellHeight * m_grid.CellData.FrameCount);
				ChkDot();
			}
		}
		private void ChkDot()
		{
			if (m_grid != null)
			{
				int w = m_grid.Sizes.FrameWidth2 * 2 / 3;
				int h = m_grid.Sizes.CellHeight / 2;
				int l = (m_grid.Sizes.FrameWidth2 - w) / 2;
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
		//-------------------------------------------------
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			if (m_grid != null)
			{
				m_grid.SizeSetting();
				SetLocSize();
				this.Invalidate();
			}
		}
		protected override void OnLocationChanged(EventArgs e)
		{
			SetLocSize();
			base.OnLocationChanged(e);
		}
		private Point m_MD = new Point(0, 0);
		protected override void OnMouseDown(MouseEventArgs e)
		{
			/*
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				m_MD= new Point(e.X, e.Y);
			}
			*/
			base.OnMouseDown(e);
		}
		//-------------------------------------------------
		private void DrawFrame(Graphics g, SolidBrush sb, Pen p, int f, Rectangle rct)
		{
			if (m_grid == null) return;	
			int y = f * m_grid.Sizes.CellHeight - m_grid.Sizes.Disp.Y;
			rct.Y = y;

			bool isNoEnabled = ! m_grid.CellData.EnabledFrame(f);
			bool isSelected = m_grid.CellData.IsSelectedFrame(f);
			//フレームの背景
			if (isSelected == true)
			{
				sb.Color = m_grid.Colors.Selection;
				Fill(g, sb, rct);
			}else if (isNoEnabled)
			{
				sb.Color = m_grid.Colors.Gray;
				Fill(g, sb, rct);
			}
			//フレーム番号を描く
			if(isNoEnabled) sb.Color = m_grid.Colors.GrayMoji;
			else sb.Color = m_grid.Colors.Moji;
			Rectangle rct2 = new Rectangle(rct.X, rct.Y, rct.Width - 5, rct.Height);
			DrawStr(g, (f + m_grid.CellData.StartDispFrame).ToString(), sb, rct2);

			// 下の横線を描く
			//p.Color = m_grid.Colors.LineB;
			//p.Width = 1;
			//DrawHorLine(g, p, m_grid.Sizes.FrameWidth2, rct.Right - 1, y);

			p.Width = 1;

			sb.Color = m_grid.Colors.Kagi;
			int SecBar = 24;
			int HSecBar = 12;
			int HHSecBar = 6;
			switch (m_grid.CellData.FrameRate)
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
				p.Color = m_grid.Colors.Line;
				int y2 = y - 1;
				DrawHorLine(g, p, rct.Left, rct.Right, y2);
				y2--;
				DrawHorLine(g, p, rct.Left, rct.Right, y2);
			}
			else if (f % HSecBar == 0)
			{
				Rectangle r = new Rectangle(Dot2.Location, Dot2.Size);
				r.Y += y;
				Fill(g, sb, r);
				p.Color = m_grid.Colors.Line;
				int y2 = y - 1;
				DrawHorLine(g, p, rct.Left, rct.Right, y2);

			}
			else
			{
				Rectangle r = new Rectangle(Dot3.Location, Dot3.Size);
				r.Y += y;
				Fill(g, sb, r);
				if (f % HHSecBar == 0)
				{
					p.Color = m_grid.Colors.LineA;
					int y2 = y - 1;
					DrawHorLine(g, p, rct.Left, rct.Right, y2);
				}
				else
				{
					p.Color = m_grid.Colors.LineB;
					DrawHorLine(g, p, m_grid.Sizes.FrameWidth2, rct.Right - 1, y);

				}
			}
		}
		//-------------------------------------------------
		protected override void OnPaint(PaintEventArgs e)
		{
			Pen p = new Pen(Color.Black);
			SolidBrush sb = new SolidBrush(Color.Transparent);
			try
			{
				Graphics g = e.Graphics;
				g.SmoothingMode = SmoothingMode.AntiAlias;
				g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
				//とりあえず塗りつぶし
				sb.Color = Color.Transparent;
				Fill(g, sb);

				if (m_grid != null)
				{
					Rectangle rct = new Rectangle(
						m_grid.Sizes.FrameWidth2,
						0,
						m_grid.Sizes.FrameWidth - m_grid.Sizes.FrameWidth2,
						m_grid.Sizes.CellHeight);

					Rectangle r = m_grid.Sizes.DispCell;
					for (int i = r.Top; i <= r.Bottom; i++)
					{
						DrawFrame(g, sb, p, i, rct);
					}
					rct.Height = this.Height;
					p.Color = m_grid.Colors.Line;
					DrawFrame(g, p, rct);
				}
			}
			finally
			{
				p.Dispose();
				sb.Dispose();
			}

		}
		//-------------------------------------------------
	}
#pragma warning restore CS8600 // Null リテラルまたは Null の可能性がある値を Null 非許容型に変換しています。
#pragma warning restore CS8603 // Null 参照戻り値である可能性があります。

}
