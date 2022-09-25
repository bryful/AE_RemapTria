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
#pragma warning disable CS8603 // Null 参照戻り値である可能性があります。
	public partial class T_Input : T_ControlBase
	{
		private T_Grid? m_grid = null;

		private int m_value = 0;

		public T_Input()
		{
			Init();
			Alignment = StringAlignment.Far;
			//FontBold = true;

			SizeFix();
			ChkGrid();
		}
		protected override void InitLayout()
		{
			base.InitLayout();
			ChkGrid();
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
		private void ChkGrid()
		{
			if (m_grid != null)
			{
				if (MyFonts == null)
				{
					MyFonts = m_grid.MyFonts;
					SetMyFont(MFontIndex, MFontSize);
				}
				SizeFix();
				SetLoc();
				m_grid.Sizes.ChangeGridSize += M_Size_ChangeGridSize;
				m_grid.Colors.ColorChangedEvent += M_Colors_ColorChangedEvent;
				m_grid.LocationChanged += M_grid_LocationChanged; ;
				m_grid.SizeChanged += M_grid_LocationChanged;

			}

		}

		private void M_grid_LocationChanged(object? sender, EventArgs e)
		{
			SetLoc();
		}

		//------------------------------------------
		private void SetLoc()
		{
			if (m_grid == null) return;
			this.Location = new Point(
				m_grid.Left -(m_grid.Sizes.FrameWidth + m_grid.Sizes.InterWidth),
				m_grid.Top -(m_grid.Sizes.InterHeight + m_grid.Sizes.CaptionHeight + m_grid.Sizes.CaptionHeight2)
				);
		}
		//------------------------------------------
		protected override void OnLocationChanged(EventArgs e)
		{
			SetLoc();
			base.OnLocationChanged(e);
		}
		//------------------------------------------
		public void SizeFix()
		{
			if (m_grid != null)
			{
				this.Size = new Size(m_grid.Sizes.FrameWidth, m_grid.Sizes.CaptionHeight+ m_grid.Sizes.CaptionHeight2);
				this.MinimumSize = this.Size;
				this.MaximumSize = this.Size;
				this.Invalidate();
			}
		}       

		private void M_Size_ChangeGridSize(object? sender, EventArgs e)
		{
			SizeFix();
		}
		private void M_Colors_ColorChangedEvent(object? sender, EventArgs e)
		{
			this.Invalidate();
		}

		//------------------------------------------
		protected override void OnPaint(PaintEventArgs e)
		{
			int ps = 1;
			Pen p = new Pen(Color.Black, ps);
			SolidBrush sb = new SolidBrush(Color.Transparent);


			try
			{
				Graphics g = e.Graphics;
				g.SmoothingMode = SmoothingMode.AntiAlias;
				g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
				Fill(g, sb);

				if (m_grid != null)
				{
					Rectangle rct = new Rectangle(m_grid.Sizes.FrameWidth2, m_grid.Sizes.CaptionHeight2,m_grid.Sizes.FrameWidth- m_grid.Sizes.FrameWidth2, m_grid.Sizes.CaptionHeight);
					sb.Color = m_grid.Colors.Moji;
					DrawStr(g, m_value.ToString(), sb, rct);
					p.Color = m_grid.Colors.InputLine;
					DrawFrame(g, p, rct, 2);
				}
			}
			finally
			{
				p.Dispose();
				sb.Dispose();
			}

		}
	}
#pragma warning restore CS8603 // Null 参照戻り値である可能性があります。

}
