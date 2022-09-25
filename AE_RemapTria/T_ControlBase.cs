using AE_RemapTria.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AE_RemapTria
{
	public partial class T_ControlBase : Control
	{
		private Font m_font =new Font("Arial",14);
		private int m_MFontIndex = 5;
		public int MFontIndex { get { return m_MFontIndex; } set { SetMFontIndex(value); } }
		private float m_MFontSize = 14;
		public float MFontSize { get { return m_MFontSize; } set { SetMFontSize(value); } }
		public Font MFont { get { return m_font; } }
		private StringFormat m_format = new StringFormat();
		private StringAlignment m_Alignment_Bak;
		private StringAlignment m_LineAlignment_Bak;
		private float m_FontSize_Bak;
		private FontStyle m_FontStyle_Bak;
		[System.Runtime.InteropServices.DllImport("gdi32.dll")]
		public static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);
		public PrivateFontCollection? MyFonts = null;// new PrivateFontCollection();
		// ************************************************************************
		public T_ControlBase()
		{
			this.Size = new Size(100, 100);
			InitializeComponent();
			Init();
		}       
		// ************************************************************************
		public int MyFontsCount
		{ 
			get 
			{
				int ret = 0;
				if(MyFonts!=null)
				{
					ret = MyFonts.Families.Length;
				}
				return ret;
			}
		}
		// ************************************************************************
		public void SetMFontIndex(int idx)
		{
			try
			{
				Font? f = MFontMake(idx, m_MFontSize);
				if (f != null)
				{
					m_font = f;
					this.Invalidate();
				}
			}
			catch
			{

			}
		}
		// ************************************************************************
		public void SetMFontSize(float sz)
		{
			try
			{
				Font? f = MFontMake(m_MFontIndex, sz);
				if (f != null)
				{
					m_font = f;
					this.Invalidate();
				}
			}
			catch
			{

			}
		}
		// ************************************************************************
		public void MFontInit()
		{
			//SourceHanCodeJP
			//MyricaM
			//Myrica
			byte[] fontData = Properties.Resources.SourceHanCodeJP;
			IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
			System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
			uint dummy = 0;
			MyFonts = new PrivateFontCollection();
			MyFonts.AddMemoryFont(fontPtr, Properties.Resources.SourceHanCodeJP.Length);
			AddFontMemResourceEx(fontPtr, (uint)Properties.Resources.SourceHanCodeJP.Length, IntPtr.Zero, ref dummy);
			System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);
			SetMyFont(m_MFontIndex, m_MFontSize);
		}
		// ************************************************************************
		public void SetMyFont(int idx, float sz) 
		{ 
			m_font = MFontMake(idx, sz); 
			this.Font = m_font; 
			this.Invalidate(); 
		}
		// ************************************************************************
		public Font MFontMake(int idx,float sz)
		{
			Font ret = new Font("Arial", m_MFontSize);
			if(MyFonts!=null)
			{
				if((idx>=0)&& (idx < MyFonts.Families.Length))
				{
					try
					{
						ret = new Font(MyFonts.Families[idx], sz);
						m_MFontIndex = idx;
						m_MFontSize = sz;
					}
					catch
					{
						m_MFontIndex = 0;
						ret = new Font(MyFonts.Families[0], m_MFontSize);
					}
				}
			}
			return ret;
		}
		// ************************************************************************
		public StringAlignment Alignment
		{
			get { return m_format.Alignment; }
			set { m_format.Alignment = value; }
		}
		// ************************************************************************
		public StringAlignment LineAlignment
		{
			get { return m_format.LineAlignment; }
			set { m_format.LineAlignment = value; }
		}

		// ************************************************************************
		public void Init()
		{
			
			this.SetStyle(
				ControlStyles.DoubleBuffer|
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.SupportsTransparentBackColor,
				true);
			this.BackColor = Color.Transparent;
			this.UpdateStyles();
			m_Alignment_Bak =
			Alignment = StringAlignment.Far;
			m_LineAlignment_Bak =
			LineAlignment = StringAlignment.Center;
			m_FontSize_Bak = this.Font.Size;
			m_FontStyle_Bak = this.Font.Style;

		}
		// ************************************************************************
		protected override void OnPaint(PaintEventArgs pe)
		{
			//base.OnPaint(pe);

			Graphics g = pe.Graphics;
			Fill(g, this.BackColor);
		}
		// ************************************************************************
		/// <summary>
		/// コントロール全部の塗りつぶし
		/// </summary>
		/// <param name="g">Graphics</param>
		/// <param name="sb">SolidBrush</param>
		public void Fill(Graphics g, SolidBrush sb)
		{
			g.FillRectangle(sb, new Rectangle(0, 0, this.Width, this.Height));
		}
		// ************************************************************************
		public void Fill(Graphics g, Color color)
		{
			SolidBrush sb = new SolidBrush(color);
			try
			{
				g.FillRectangle(sb, new Rectangle(0, 0, this.Width, this.Height));
			}
			finally
			{
				sb.Dispose();
			}
		}
		// ************************************************************************
		public void Fill(Graphics g, SolidBrush sb, Rectangle rct)
		{
			g.FillRectangle(sb, rct);
		}
		// ************************************************************************
		public void Fill(Graphics g, Color color, Rectangle rct)
		{
			SolidBrush sb = new SolidBrush(color);
			try
			{
				g.FillRectangle(sb, rct);
			}
			finally
			{
				sb.Dispose();
			}
		}
		// ************************************************************************
		public void DrawFrame(Graphics g, Pen p, int ps = 1)
		{
			DrawFrame(g, p, new Rectangle(0, 0, this.Width, this.Height), ps);
		}
		// ************************************************************************
		public void DrawFrame(Graphics g, Pen p, Rectangle rct, int ps = 1)
		{
			if (ps < 1) ps = 1;
			float pw = p.Width;
			p.Width = ps;
			rct.Width -= ps;
			rct.Height -= ps;
			g.DrawRectangle(p, rct);
			p.Width = pw;
		}
		// ************************************************************************
		public void DrawStr(Graphics g, string s, SolidBrush sb, Rectangle rct)
		{
			if (m_font != null)
			{
				try
				{
					g.SmoothingMode = SmoothingMode.AntiAlias;
					g.DrawString(s, m_font, sb, rct, m_format);
				}
				catch
				{

				}
			}
		}
		// ************************************************************************
		public Rectangle Rect()
		{
			return new Rectangle(0, 0, this.Width, this.Height);
		}
		// ************************************************************************
		public void DrawHorLine(Graphics g, Pen p, int x0, int x1, int y)
		{
			g.DrawLine(p, x0, y, x1, y);
		}
		// ************************************************************************
		public void DrawVerLine(Graphics g, Pen p, int x, int y0, int y1)
		{
			g.DrawLine(p, x, y0, x, y1);
		}
		// ************************************************************************
		public bool FontBold
		{
			get { return m_font.Bold; }
			set
			{
				if (MyFonts != null)
				{
					int v = (int)m_font.Style;

					if (value)
					{
						v |= (int)FontStyle.Bold;
					}
					else
					{
						v &= 0xE;
					}
					m_font = new Font(MyFonts.Families[m_MFontIndex], m_MFontSize, (FontStyle)v);
					this.Invalidate();
				}
			}
		}
		// ************************************************************************
		public FontStyle Font_Style
		{
			get { return m_font.Style; }
			set
			{
				if (MyFonts != null)
				{
					if (m_font.Style != value)
					{
						m_font = new Font(MyFonts.Families[m_MFontIndex], m_MFontSize, value);
						this.Invalidate();
					}
				}
			}
		}
		// ************************************************************************
		public float Font_Size
		{
			get { return m_MFontSize; }
			set
			{
				if (MyFonts != null)
				{
					if (m_MFontSize != value)
					{
						m_MFontSize = value;
						m_font = new Font(MyFonts.Families[m_MFontIndex], m_MFontSize, m_font.Style);
						this.Invalidate();
					}
				}
			}
		}

		// ************************************************************************
		public void PushFontStatus()
		{
			m_Alignment_Bak = Alignment;
			m_LineAlignment_Bak = LineAlignment;
			m_FontSize_Bak = Font_Size;
			m_FontStyle_Bak = Font_Style;
		}
		// ************************************************************************
		public void PopFontStatus()
		{
			Alignment = m_Alignment_Bak;
			LineAlignment = m_LineAlignment_Bak;
			Font_Size = m_FontSize_Bak;
			Font_Style = m_FontStyle_Bak;
		}
		// ************************************************************************
	}
}
