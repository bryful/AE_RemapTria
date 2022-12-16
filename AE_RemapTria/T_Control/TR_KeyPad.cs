using SixLabors.ImageSharp.ColorSpaces.Conversion;
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
	public enum KP
	{
		None = -1,
		key0= 0,
		key1,
		key2,
		key3,
		key4,
		key5,
		key6,
		key7,
		key8,
		key9,
		keyBS,
		keyCLS,
		keySEC
	
	}

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
		// ***************************************************
		private TR_DurationBox? m_DBox = null;
		[Category("_AE_Reamp")]
		public TR_DurationBox? DurationBox
		{
			get { return m_DBox; }
			set
			{
				m_DBox = value;
				if(m_DBox != null)
				{

				}
			}
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
		// ******************************************************************************
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

				//07
				Padding pd = new Padding(0, 0, 2, 1);
				Rectangle r = keyRect(0, 0, 1, 1);
				if(m_MDPos==KP.key7)
				{
					sb.Color = m_Color_Down;
					g.FillRectangle(sb, r);
				}
				sb.Color = m_FrameColor;
				TRc.DrawFrame(g, sb,r , pd);
				sb.Color = ForeColor;
				DrawStr(g, "7",sb, r);

				//08
				pd = new Padding(0, 0, 2, 1);
				r = keyRect(1, 0, 1, 1);
				if (m_MDPos == KP.key8)
				{
					sb.Color = m_Color_Down;
					g.FillRectangle(sb, r);
				}
				sb.Color = m_FrameColor;
				TRc.DrawFrame(g, sb,r , pd);
				sb.Color = ForeColor;
				DrawStr(g, "8", sb, r);

				//09
				pd = new Padding(0, 0, 2, 1);
				r = keyRect(2, 0, 1, 1);
				if (m_MDPos == KP.key9)
				{
					sb.Color = m_Color_Down;
					g.FillRectangle(sb, r);
				}
				sb.Color = m_FrameColor;
				TRc.DrawFrame(g, sb,r , pd);
				sb.Color = ForeColor;
				DrawStr(g, "9", sb, r);

				//BS
				pd = new Padding(0, 0, 2, 1);
				r = keyRect(3, 0, 1, 1);
				if (m_MDPos == KP.keyBS)
				{
					sb.Color = m_Color_Down;
					g.FillRectangle(sb, r);
				}
				sb.Color = m_FrameColor;
				TRc.DrawFrame(g, sb,r , pd);
				sb.Color = ForeColor;
				DrawStr(g, "BS", sb, r);
				//04
				pd = new Padding(0, 0, 2, 1);
				r = keyRect(0, 1, 1, 1);
				if (m_MDPos == KP.key4)
				{
					sb.Color = m_Color_Down;
					g.FillRectangle(sb, r);
				}
				sb.Color = m_FrameColor;
				TRc.DrawFrame(g, sb, r, pd);
				sb.Color = ForeColor;
				DrawStr(g, "4", sb, r);
				//05
				pd = new Padding(0, 0, 2, 1);
				r = keyRect(1, 1, 1, 1);
				if (m_MDPos == KP.key5)
				{
					sb.Color = m_Color_Down;
					g.FillRectangle(sb, r);
				}
				sb.Color = m_FrameColor;
				TRc.DrawFrame(g, sb, r, pd);
				sb.Color = ForeColor;
				DrawStr(g, "5", sb, r);
				//06
				pd = new Padding(0, 0, 2, 1);
				r = keyRect(2, 1, 1, 1);
				if (m_MDPos == KP.key6)
				{
					sb.Color = m_Color_Down;
					g.FillRectangle(sb, r);
				}
				sb.Color = m_FrameColor;
				TRc.DrawFrame(g, sb, r, pd);
				sb.Color = ForeColor;
				DrawStr(g, "6", sb, r);

				//CLR
				pd = new Padding(0, 0, 2, 1);
				r = keyRect(3, 1, 1, 1);
				if (m_MDPos == KP.keyCLS)
				{
					sb.Color = m_Color_Down;
					g.FillRectangle(sb, r);
				}
				sb.Color = m_FrameColor;
				TRc.DrawFrame(g, sb, r, pd);
				sb.Color = ForeColor;
				DrawStr(g, "CL", sb, r);
				//01
				pd = new Padding(0, 0, 2, 1);
				r = keyRect(0, 2, 1, 1);
				if (m_MDPos == KP.key1)
				{
					sb.Color = m_Color_Down;
					g.FillRectangle(sb, r);
				}
				sb.Color = m_FrameColor;
				TRc.DrawFrame(g, sb, r, pd);
				sb.Color = ForeColor;
				DrawStr(g, "1", sb, r);
				//02
				pd = new Padding(0, 0, 2, 1);
				r = keyRect(1, 2, 1, 1);
				if (m_MDPos == KP.key2)
				{
					sb.Color = m_Color_Down;
					g.FillRectangle(sb, r);
				}
				sb.Color = m_FrameColor;
				TRc.DrawFrame(g, sb, r, pd);
				sb.Color = ForeColor;
				DrawStr(g, "2", sb, r);
				//03
				pd = new Padding(0, 0, 2, 1);
				r = keyRect(2, 2, 1, 1);
				if (m_MDPos == KP.key3)
				{
					sb.Color = m_Color_Down;
					g.FillRectangle(sb, r);
				}
				sb.Color = m_FrameColor;
				TRc.DrawFrame(g, sb, r, pd);
				sb.Color = ForeColor;
				DrawStr(g, "3", sb, r);
				//Sec
				pd = new Padding(0, 0, 2, 2);
				r = keyRect(3, 2, 1, 2);
				if (m_MDPos == KP.keySEC)
				{
					sb.Color = m_Color_Down;
					g.FillRectangle(sb, r);
				}
				sb.Color = m_FrameColor;
				TRc.DrawFrame(g, sb, r, pd);
				sb.Color = ForeColor;
				DrawStr(g, "+", sb, r);
				//0
				pd = new Padding(0, 0, 2, 2);
				r = keyRect(0, 3, 3, 1);
				if (m_MDPos == KP.key0)
				{
					sb.Color = m_Color_Down;
					g.FillRectangle(sb, r);
				}
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
		// ******************************************************************************
		private KP m_MDPos = KP.None;
		// ******************************************************************************
		private KP GetMDPos(int x,int y)
		{
			KP ret = KP.None;

			int cx = (x - m_RoundMargin) / (m_KeyWidth + m_KeyInterWidth);
			int cy = (y - m_RoundMargin) / (m_KeyHeight + m_KeyInterHeight);
			switch (cy)
			{
				case 3:
					if (cx < 3)
					{
						ret = KP.key0;
					}
					else
					{
						ret = KP.keySEC;
					}
					break;
				case 2:
					switch (cx)
					{
						case 0:
							ret = KP.key1;
							break;
						case 1:
							ret = KP.key2;
							break;
						case 2:
							ret = KP.key3;
							break;
						case 3:
							ret = KP.keySEC;
							break;
					}
					break;
				case 1:
					switch (cx)
					{
						case 0:
							ret = KP.key4;
							break;
						case 1:
							ret = KP.key5;
							break;
						case 2:
							ret = KP.key6;
							break;
						case 3:
							ret = KP.keyCLS;
							break;
					}
					break;
				case 0:
					switch (cx)
					{
						case 0:
							ret = KP.key7;
							break;
						case 1:
							ret = KP.key8;
							break;
						case 2:
							ret = KP.key9;
							break;
						case 3:
							ret = KP.keyBS;
							break;
					}
					break;
			}
			return ret;
		}
		// ******************************************************************************
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (e.Button == MouseButtons.Left)
			{
				KP v = GetMDPos(e.X, e.Y);
				if(v!=KP.None)
				{
					m_MDPos = v;
					this.Invalidate();
				}
			}
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if(m_MDPos!=KP.None)
			{
				KP v = m_MDPos;
				m_MDPos = KP.None;
				this.Invalidate();
				SetDBox(v);
			}
		}
		public void SetDBox(KP v)
		{
			if (m_DBox == null) return;
			switch(v)
			{
				case KP.key0:
					m_DBox.Add(0);
					break;
				case KP.key7:
					m_DBox.Add(7);
					break;
				case KP.key8:
					m_DBox.Add(8);
					break;
				case KP.key9:
					m_DBox.Add(9);
					break;
				case KP.keyBS:
					m_DBox.BS();
					break;
				case KP.key4:
					m_DBox.Add(4);
					break;
				case KP.key5:
					m_DBox.Add(5);
					break;
				case KP.key6:
					m_DBox.Add(6);
					break;
				case KP.keyCLS:
					m_DBox.CLS();
					break;
				case KP.key1:
					m_DBox.Add(1);
					break;
				case KP.key2:
					m_DBox.Add(2);
					break;
				case KP.key3:
					m_DBox.Add(3);
					break;
				case KP.keySEC:
					m_DBox.AddSec();
					break;
			}
		}
		public void SetMDPos(KP v)
		{
			m_MDPos = v;
			this.Invalidate();
		}
	}
}
