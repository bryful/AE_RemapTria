using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AE_RemapTria
{
	public partial class TR_Fps : TR_DialogControl
	{
		private T_Fps m_fps = T_Fps.FPS24;
		[Category("_AE_Reamp")]
		public T_Fps Fps
		{
			get { return m_fps; }
			set
			{
				m_fps = value;
				this.Invalidate();
			}
		}
		private int m_cbWidth = 14;
		private bool m_Enabled = true;
		[Category("_AE_Reamp")]
		public bool IsEnabled
		{
			get { return m_Enabled; }
			set
			{
				m_Enabled = value;
				this.Invalidate();
			}
		}
		public TR_Fps()
		{
			this.Size = new Size(120, 25);
			MyFontSize = 9;
			Alignment = StringAlignment.Center;
			LineAlignment = StringAlignment.Center;
			ForeColor = Color.FromArgb(255, 200, 200, 250);
			InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			//base.OnPaint(pe);
			Graphics g = pe.Graphics;
			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
			SolidBrush sb = new SolidBrush(Color.Transparent);
			Pen p = new Pen(m_FrameColor);
			try
			{
				sb.Color = Color.Transparent;
				Rectangle r = new Rectangle(0, 0, Width, Height);
				Fill(g, sb);

				// Enabled
				r = new Rectangle(0, (Height - m_cbWidth) / 2, m_cbWidth, m_cbWidth);
				p.Color = m_FrameColor;
				p.Width = 2;
				g.DrawRectangle(p,r);
				if (m_Enabled)
				{
					r = new Rectangle(r.Left + 3, r.Top + 3, r.Width - 6, r.Height - 6);
					sb.Color = m_FrameColor;
					g.FillRectangle(sb, r);
				}

				int x = m_cbWidth + 5;
				int w = Width - (m_cbWidth + 5);

				r = new Rectangle(x, (Height -m_cbWidth/2)/2, w, m_cbWidth / 2);
				sb.Color = Color.FromArgb(50, 50, 75);
				g.FillRectangle(sb,r);

				sb.Color= EnabledColor(ForeColor, m_Enabled);
				r = new Rectangle(x, (Height - m_cbWidth) / 2, 2, m_cbWidth);
				g.FillRectangle(sb, r);
				r = new Rectangle(Width-3, (Height - m_cbWidth) / 2, 2, m_cbWidth);
				g.FillRectangle(sb, r);

				Alignment = StringAlignment.Center;
				int x2 = x+5;
				r = new Rectangle(x2, 0, w / 2-5, Height);
				if (m_fps==T_Fps.FPS24)
				{
					sb.Color = EnabledColor(ForeColor, m_Enabled);
					g.FillRectangle(sb, r);
					sb.Color = Color.Black;
				}
				else
				{
					sb.Color = EnabledColor(ForeColor, m_Enabled);
				}
				DrawStr(g, "24fps", sb, r);
				r = new Rectangle(x2 + w / 2-5, 0, w / 2-5, Height);
				if (m_fps == T_Fps.FPS30)
				{
					sb.Color = EnabledColor(ForeColor, m_Enabled);
					g.FillRectangle(sb, r);
					sb.Color = Color.Black;
				}
				else
				{
					sb.Color = EnabledColor(ForeColor, m_Enabled);
				}
				DrawStr(g, "30fps", sb, r);
			}
			finally
			{
				sb.Dispose();
				p.Dispose();
			}
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if(e.X<m_cbWidth)
			{
				m_Enabled = !m_Enabled;
				this.Invalidate();
			}
		}
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			this.Invalidate();
		}
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			this.Invalidate();
		}
	}
}
