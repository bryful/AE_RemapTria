using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE_RemapTria
{
	public class TR_Label : TR_DialogControl
	{
		
		private Color m_UnderColor = Color.FromArgb(64, 0, 0, 0);

		public TR_Label()
		{
			TabStop = false;
			this.Size = new Size(100, 24);
			this.ForeColor = Color.FromArgb(255, 150, 150, 200);
			MyFontSize = 8;
			Alignment = StringAlignment.Near;
			LineAlignment = StringAlignment.Center;
		}
		//--------------------------------------------------------
		protected override void OnPaint(PaintEventArgs pe)
		{
			//base.OnPaint(pe);
			Graphics g = pe.Graphics;
			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
			SolidBrush sb = new SolidBrush(Color.Transparent);
			try
			{
				Rectangle r = new Rectangle(0, 0, this.Width, this.Height);
				sb.Color = BackColor;
				Fill(g,sb,r);

				int w = 0;
				sb.Color = m_FrameColor;
				if (m_FrameWeight.Left>0)
				{
					w = m_FrameWeight.Left;
					if (w > Height - 2) w = Height - 2;
					r = new Rectangle(0, (Height - w) / 2, w, w);
					g.FillRectangle(sb, r);
				}
				if (m_FrameWeight.Right > 0)
				{
					w = m_FrameWeight.Right;
					if (w > Height - 2) w = Height - 2;
					r = new Rectangle(Width-w, (Height - w) / 2, w, w);
					g.FillRectangle(sb, r);
				}
				if (m_FrameWeight.Top > 0)
				{
					w = Width - m_FrameWeight.Left - m_FrameWeight.Right;
					r = new Rectangle
						(
						(Width - w) / 2,
						0,
						w,
						m_FrameWeight.Top
						);
					g.FillRectangle(sb, r);
				}
				if (m_FrameWeight.Bottom > 0)
				{
					w = Width - m_FrameWeight.Left - m_FrameWeight.Right;
					r = new Rectangle
						(
						m_FrameWeight.Left,
						Height - m_FrameWeight.Bottom,
						w,
						m_FrameWeight.Bottom
						);
					g.FillRectangle(sb, r);
				}


				sb.Color = this.ForeColor;
				DrawStr(g,this.Text,sb,DrawArea);


			}
			finally
			{
				sb.Dispose();
			}
		}
		protected override void OnTextChanged(EventArgs e)
		{
			this.Invalidate();
			base.OnTextChanged(e);
		}

	}
}
