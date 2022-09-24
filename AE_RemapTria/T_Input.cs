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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ScrollBar;

namespace AE_RemapTria
{
	public partial class T_Input : T_ContolBase
	{
		private T_Grid m_grid = null;

		private int m_value = 0;

		public T_Input()
		{
			Init();
			Alignment = StringAlignment.Far;
			//FontBold = true;

			SizeFix();
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
				MyFonts = m_grid.MyFonts;
				SetMyFont(MFontIndex,MFontSize);
				m_grid.Sizes.ChangeGridSize += M_Size_ChangeGridSize;
				m_grid.Colors.ColorChangedEvent += M_Colors_ColorChangedEvent;
				SizeFix();
			}
		}
		//------------------------------------------
		public void SizeFix()
		{
			if (m_grid != null)
			{
				this.Size = new Size(m_grid.Sizes.FrameWidth, m_grid.Sizes.CaptionHeight);
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
					Rectangle rct = this.Rect();
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

}
