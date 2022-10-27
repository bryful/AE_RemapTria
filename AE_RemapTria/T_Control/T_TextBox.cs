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
	public partial class T_TextBox : TextBox
	{
		private T_MyFonts? m_MyFonts = null;
		public T_MyFonts? MyFonts
		{
			get { return m_MyFonts; }
			set
			{
				m_MyFonts = value;
				if (m_MyFonts != null)
				{
					this.Font = m_MyFonts.MyFont(m_MyFontIndex, this.Font.Size, this.Font.Style);
					this.Invalidate();
				}
			}
		}
		private int m_MyFontIndex = 5;
		public int MyFontIndex
		{
			get { return m_MyFontIndex; }
			set
			{
				m_MyFontIndex = value;
				if (m_MyFontIndex < 0) m_MyFontIndex = 0;
				if (m_MyFonts != null)
				{
					this.Font = m_MyFonts.MyFont(m_MyFontIndex, this.Font.Size, this.Font.Style);
					this.Invalidate();
				}
			}
		}
		public float MyFontSize
		{
			get { return this.Font.Size; }
			set
			{
				SetFontSizeStyle(value, this.Font.Style);
			}
		}
		public FontStyle MyFontStyle
		{
			get { return this.Font.Style; }
			set
			{
				SetFontSizeStyle(this.Font.Size, value);
			}
		}
		public void SetFontSizeStyle(float sz, FontStyle fs)
		{
			if (m_MyFonts != null)
			{
				this.Font = m_MyFonts.MyFont(m_MyFontIndex, sz, fs);
			}
			else
			{
				this.Font = new Font(this.Font.FontFamily, sz, fs);
			}
			this.Invalidate();
		}
		public T_TextBox()
		{
			this.ForeColor = Color.FromArgb(255, 120, 220, 250);
			this.BackColor = Color.FromArgb( 6, 11, 25);
			this.BorderStyle = BorderStyle.FixedSingle;
			InitializeComponent();
			Init();
		}
		public void Init()
		{

			this.SetStyle(
				ControlStyles.DoubleBuffer|
				//ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint,
				//ControlStyles.SupportsTransparentBackColor,
								true);
			this.UpdateStyles();

		}
		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
			Graphics g = pevent.Graphics;
			//base.OnPaintBackground(pevent);
			SolidBrush b = new SolidBrush(this.BackColor);
			try
			{
				Rectangle r = this.ClientRectangle;
				g.FillRectangle(b, r);
			}
			finally
			{
				b.Dispose();
			}

		}
		protected override void OnPaint(PaintEventArgs pe)
		{
			//base.OnPaint(pe);
			Graphics g = pe.Graphics;
			SolidBrush b = new SolidBrush(this.BackColor);
			try
			{
				Rectangle r = this.ClientRectangle;
				b.Color = this.ForeColor;
				g.DrawString(this.Text, this.Font, b,r);

			}
			finally
			{
				b.Dispose();
			}

			//文字列を描画する
		}
		
		
	}
}
