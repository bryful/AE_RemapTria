using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE_RemapTria
{
	public class T_Label : T_BaseControl
	{
		private Size m_LeftBar = new Size(14, 14);
		[Category("_AE_Remap")]
		public Size LeftBar
		{
			get { return m_LeftBar; }
			set { m_LeftBar = value; this.Invalidate(); }
		}
		private Size m_RightBar = new Size(0, 0);
		[Category("_AE_Remap")]
		public Size RightBar
		{
			get { return m_RightBar; }
			set { m_RightBar = value; this.Invalidate(); }
		}
		private Size m_TopBar = new Size(0, 0);
		[Category("_AE_Remap")]
		public Size TopBar
		{
			get { return m_TopBar; }
			set { m_TopBar = value; this.Invalidate(); }
		}
		private Size m_BottomBar = new Size(0, 0);
		[Category("_AE_Remap")]
		public Size BottomBar
		{
			get { return m_BottomBar; }
			set { m_BottomBar = value; this.Invalidate(); }
		}
		private Color m_UnderColor = Color.FromArgb(64, 0, 0, 0);

		public T_Label()
		{
			this.Size = new Size(100, 24);
			this.ForeColor = Color.FromArgb(255, 150, 150, 200);
			MyFontSize = 8;
			Alignment = StringAlignment.Near;
			LineAlignment = StringAlignment.Center;
			this.SetStyle(
				ControlStyles.DoubleBuffer |
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint|
				ControlStyles.SupportsTransparentBackColor,
				true);
			this.BackColor = Color.Transparent;
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
				Fill(g,sb,r);
				if(!m_LeftBar.IsEmpty)
				{
					r = new Rectangle(
						0,
						(this.Height - m_LeftBar.Height) / 2, 
						m_LeftBar.Width, 
						m_LeftBar.Height);
					sb.Color = this.ForeColor;
					Fill(g, sb, r);
				}
				if (!m_RightBar.IsEmpty)
				{
					r = new Rectangle(
						this.Width - m_RightBar.Width,
						(this.Height - m_RightBar.Height) / 2,
						m_RightBar.Width,
						m_RightBar.Height) ;
					sb.Color = this.ForeColor;
					Fill(g, sb, r);
				}
				if(!m_TopBar.IsEmpty)
				{
					switch(Alignment)
					{
						case StringAlignment.Near:
							r = new Rectangle(
								m_LeftBar.Width+2,
								0,
								m_TopBar.Width-4,
								m_TopBar.Height
								);
							break;
						case StringAlignment.Far:
							r = new Rectangle(
								this.Width-(m_TopBar.Width-m_RightBar.Width-2),
								0,
								m_TopBar.Width - 4,
								m_TopBar.Height
								);
							break;
						case StringAlignment.Center:
							r = new Rectangle(
								this.Width - (m_TopBar.Width),
								0,
								m_TopBar.Width,
								m_TopBar.Height
								);
							break;
					}
					sb.Color = this.ForeColor;
					Fill(g, sb, r);

				}
				if (!m_BottomBar.IsEmpty)
				{
					switch (Alignment)
					{
						case StringAlignment.Near:
							r = new Rectangle(
								m_LeftBar.Width + 2,
								this.Height - m_BottomBar.Height,
								m_BottomBar.Width - 4,
								m_BottomBar.Height
								);
							break;
						case StringAlignment.Far:
							r = new Rectangle(
								this.Width - (m_TopBar.Width - m_RightBar.Width - 2),
								this.Height - m_BottomBar.Height,
								m_BottomBar.Width - 4,
								m_BottomBar.Height
								);
							break;
						case StringAlignment.Center:
							r = new Rectangle(
								this.Width - (m_TopBar.Width),
								this.Height - m_BottomBar.Height,
								m_BottomBar.Width,
								m_BottomBar.Height
								);
							break;
					}
					sb.Color = this.ForeColor;
					Fill(g, sb, r);

				}
				r = new Rectangle(
					m_LeftBar.Width + 2,
					0,
					this.Width - (m_LeftBar.Width + m_RightBar.Width + 4),
					this.Height
					);

				sb.Color = this.ForeColor;
				DrawStr(g,this.Text,sb,r);
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
