using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace TS
{
	public class TS_GridBase : Control
    {
        private StringFormat m_format = new StringFormat();
        private Font m_font = new Font("Arial", 12);

        private StringAlignment m_Alignment_Bak;
        private StringAlignment m_LineAlignment_Bak;
        private float m_FontSize_Bak;
        private FontStyle m_FontStyle_Bak;
        /// <summary>
        /// 初期化 継承されたコントロールのコンストラクタで必ず実行
        /// </summary>
        public void Init()
		{
			this.SetStyle(
				ControlStyles.DoubleBuffer |
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.SupportsTransparentBackColor,
				true);
			this.BackColor = Color.Transparent;

            m_Alignment_Bak=
            Alignment = StringAlignment.Far;
            m_LineAlignment_Bak =
            LineAlignment = StringAlignment.Center;
            m_FontSize_Bak = m_font.Size;
            m_FontStyle_Bak = m_font.Style;
		}
        //-------------------------------------------------
        /// <summary>
        /// 表示位置。横。
        /// </summary>
        public StringAlignment Alignment
        {
            get { return m_format.Alignment; }
            set { m_format.Alignment = value; }
        }
        //-------------------------------------------------
        /// <summary>
        /// 表示位置。縦。
        /// </summary>
        public StringAlignment LineAlignment
        {
            get { return m_format.LineAlignment; }
            set { m_format.LineAlignment = value; }
        }
        //------------------------------------------------------
        /// <summary>
        /// コントロール全部の塗りつぶし
        /// </summary>
        /// <param name="g">Graphics</param>
        /// <param name="sb">SolidBrush</param>
        public void Fill(Graphics g, SolidBrush sb)
		{
			g.FillRectangle(sb, new Rectangle(0, 0, this.Width, this.Height));
		}
        //------------------------------------------------------
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
        //------------------------------------------------------
        public void Fill(Graphics g, SolidBrush sb,Rectangle rct)
		{
			g.FillRectangle(sb, rct);
		}
        //------------------------------------------------------
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
        //------------------------------------------------------
        public void DrawFrame(Graphics g, Pen p, int ps = 1)
		{
			DrawFrame(g, p, new Rectangle(0, 0, this.Width, this.Height), ps);
		}
		//------------------------------------------------------
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
		//------------------------------------------------------
		public void DrawStr(Graphics g,string s,SolidBrush sb,Rectangle rct)
		{
			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.DrawString(s, m_font, sb, rct, m_format);
		}
		//------------------------------------------------------
		public Rectangle Rect()
		{
			return new Rectangle(0, 0, this.Width, this.Height);
		}
        //------------------------------------------------------
        public void DrawHorLine(Graphics g,Pen p, int x0, int x1,int y)
        {
            g.DrawLine(p, x0, y, x1, y);
        }
        //------------------------------------------------------
        public void DrawVerLine(Graphics g, Pen p, int x, int y0, int y1)
        {
            g.DrawLine(p, x, y0, x, y1);
        }
        //------------------------------------------------------
        public bool FontBold
		{
			get{ return m_font.Bold; }
			set
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

                m_font = new Font(m_font.FontFamily, m_font.Size, (FontStyle)v);
			}
		}
        //------------------------------------------------------
        public FontStyle Font_Style
        {
            get { return m_font.Style; }
            set
            {
                if (m_font.Style !=value)
                {
                    m_font = new Font(m_font.FontFamily, m_font.Size, value);
                }
            }
        }
		//------------------------------------------------------
		public float Font_Size
		{
			get { return m_font.Size; }
			set
			{
                if (m_font.Size != value)
                {
                    m_font = new Font(m_font.FontFamily, value, m_font.Style);
                }
			}
		}

        //------------------------------------------------------
        public void PushFontStatus()
        {
            m_Alignment_Bak = Alignment;
            m_LineAlignment_Bak = LineAlignment;
            m_FontSize_Bak = Font_Size;
            m_FontStyle_Bak = Font_Style;
        }
        //------------------------------------------------------
        public void PopFontStatus()
        {
            Alignment = m_Alignment_Bak;
            LineAlignment= m_LineAlignment_Bak;
            Font_Size = m_FontSize_Bak;
            Font_Style = m_FontStyle_Bak;
        }
        //------------------------------------------------------
    }
}
