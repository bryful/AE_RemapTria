using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AE_RemapTria
{
	public partial class TR_KeyPad : TR_DialogControl
	{
		private int m_KeyWidth = 25;
		private int m_KeyHeight = 18;
		private int m_KeyInterWidth = 2;
		private int m_KeyInterHeight = 2;
		private int m_RoundMargin = 3;
		private Color m_Color_Down = Color.FromArgb(200, 220, 220, 255);
		[Category("_AE_Reamp")]
		public Color Color_Down
		{
			get { return m_Color_Down; }
			set { m_Color_Down = value; Invalidate(); }
		}
		public TR_KeyPad()
		{
			MyFontSize = 8;
			MyFontIndex = 5;
			SetSize();
			InitializeComponent();
		}
		private void SetSize()
		{
			m_FrameColor = Color.FromArgb(100, 100, 150);
			Size sz = new Size(
				m_KeyWidth * 4 + m_KeyInterWidth * 3+ m_RoundMargin*2,
				m_KeyHeight * 4 + m_KeyInterHeight * 3+ m_RoundMargin*2);
			this.MaximumSize = sz;
			this.MinimumSize = sz;
			this.Size = sz;
		}
		private Rectangle keyRect(int x,int y, int xc=1,int yc=1)
		{
			int l = m_RoundMargin + x * (m_KeyWidth + m_KeyInterWidth);
			int t = m_RoundMargin + y * (m_KeyHeight + m_KeyInterHeight);
			int w = m_KeyWidth * xc + m_KeyInterWidth * (xc - 1);
			int h = m_KeyHeight * yc + m_KeyInterHeight * (yc - 1);
			return new Rectangle(l, t, w, h);
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			//base.OnPaint(pe);
			SolidBrush sb = new SolidBrush(m_FrameColor);
			Pen p = new Pen(m_FrameColor);
			Graphics g = pe.Graphics;
			try
			{
				Alignment = StringAlignment.Center;
				//BG
				sb.Color = BackColor;
				g.FillRectangle(sb, this.ClientRectangle);

				sb.Color = m_FrameColor;
				//07
				Padding pd = new Padding(0, 0, 2, 1);
				Rectangle r = keyRect(0, 0, 1, 1);
				TRc.DrawFrame(g, sb,r , pd);
				sb.Color = ForeColor;
				DrawStr(g, "7",sb, r);

				//08
				pd = new Padding(0, 0, 2, 1);
				r = keyRect(1, 0, 1, 1);
				sb.Color = m_FrameColor;
				TRc.DrawFrame(g, sb,r , pd);
				sb.Color = ForeColor;
				DrawStr(g, "8", sb, r);

				//09
				pd = new Padding(0, 0, 2, 1);
				r = keyRect(2, 0, 1, 1);
				sb.Color = m_FrameColor;
				TRc.DrawFrame(g, sb,r , pd);
				sb.Color = ForeColor;
				DrawStr(g, "9", sb, r);

				//BS
				pd = new Padding(0, 0, 2, 1);
				r = keyRect(3, 0, 1, 1);
				sb.Color = m_FrameColor;
				TRc.DrawFrame(g, sb,r , pd);
				sb.Color = ForeColor;
				DrawStr(g, "BS", sb, r);
				//04
				pd = new Padding(0, 0, 2, 1);
				r = keyRect(0, 1, 1, 1);
				sb.Color = m_FrameColor;
				TRc.DrawFrame(g, sb, r, pd);
				sb.Color = ForeColor;
				DrawStr(g, "4", sb, r);
				//05
				pd = new Padding(0, 0, 2, 1);
				r = keyRect(1, 1, 1, 1);
				sb.Color = m_FrameColor;
				TRc.DrawFrame(g, sb, r, pd);
				sb.Color = ForeColor;
				DrawStr(g, "5", sb, r);
				//06
				pd = new Padding(0, 0, 2, 1);
				r = keyRect(2, 1, 1, 1);
				sb.Color = m_FrameColor;
				TRc.DrawFrame(g, sb, r, pd);
				sb.Color = ForeColor;
				DrawStr(g, "6", sb, r);

				//CLR
				pd = new Padding(0, 0, 2, 1);
				r = keyRect(3, 1, 1, 1);
				sb.Color = m_FrameColor;
				TRc.DrawFrame(g, sb, r, pd);
				sb.Color = ForeColor;
				DrawStr(g, "CL", sb, r);
				//01
				pd = new Padding(0, 0, 2, 1);
				r = keyRect(0, 2, 1, 1);
				sb.Color = m_FrameColor;
				TRc.DrawFrame(g, sb, r, pd);
				sb.Color = ForeColor;
				DrawStr(g, "1", sb, r);
				//02
				pd = new Padding(0, 0, 2, 1);
				r = keyRect(1, 2, 1, 1);
				sb.Color = m_FrameColor;
				TRc.DrawFrame(g, sb, r, pd);
				sb.Color = ForeColor;
				DrawStr(g, "2", sb, r);
				//03
				pd = new Padding(0, 0, 2, 1);
				r = keyRect(2, 2, 1, 1);
				sb.Color = m_FrameColor;
				TRc.DrawFrame(g, sb, r, pd);
				sb.Color = ForeColor;
				DrawStr(g, "3", sb, r);
				//Sec
				pd = new Padding(0, 0, 2, 2);
				r = keyRect(3, 2, 1, 2);
				sb.Color = m_FrameColor;
				TRc.DrawFrame(g, sb, r, pd);
				sb.Color = ForeColor;
				DrawStr(g, "+", sb, r);
				//0
				pd = new Padding(0, 0, 2, 2);
				r = keyRect(0, 3, 3, 1);
				sb.Color = m_FrameColor;
				TRc.DrawFrame(g, sb, r, pd);
				sb.Color = ForeColor;
				DrawStr(g, "0", sb, r);

			}
			finally
			{
				sb.Dispose();
				p.Dispose();
			}
		}
	}
}
