using BRY;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.Compression;
using System.IO.Pipes;
using System.Runtime.InteropServices;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;

namespace AE_RemapTria
{
    public class TR_Control
    {
        public const int MenuHeight = 20;

        protected TR_Form? m_form = null;
        protected Font? m_font = null;
        protected int m_FontIndex = 5;
        public int FontIndex
        {
            get { return m_FontIndex; }
            set
            {
                if (m_FontIndex != value)
                {
                    m_FontIndex = value;
                    if (m_form != null)
                    {
                        m_font = m_form.MyFont(m_FontIndex, m_FontSize, m_form.FontStyle);
                        ChkOffScr();
                    }

                }
            }
        }

        protected float m_FontSize = 12;
        public float FontSize
        {
            get { return m_FontSize; }
            set
            {
                if (m_FontSize != value)
                {
                    m_FontSize = value;
                    if (m_form != null)
                    {
                        m_font = m_form.MyFont(m_FontIndex, m_FontSize, m_form.FontStyle);
                        ChkOffScr();
                    }

                }
            }
        }
        // ************************************************************************

        // ************************************************************************
        // ************************************************************************
        private Bitmap m_OffScr = new Bitmap(10, 10, PixelFormat.Format32bppArgb);
        public Bitmap Offscr() { return m_OffScr; }
        protected Size m_Size = new Size(100, 100);
        //protected Size m_MinimumSize = new Size(100, 100);
        //protected Size m_MaximumSize = new Size(100, 100);
        public Size Size
        {
            get { return m_Size; }
            set
            {
                m_Size = value;
                //Size sz = ChkSize(m_Size);
                if (m_Size != value)
                {
                    m_Size = value;
                    ChkOffScr();
                    Invalidate();
                }
            }
        }

        public int Width { get { return m_Size.Width; } }
        public int Height { get { return m_Size.Height; } }
		/*
        public void SetMinMaxSize(Size n, Size m)
        {
            m_MinimumSize = n;
            m_MaximumSize = m;
            ChkMinMaxSize();
            Size s = ChkSize(m_Size);
            if (s != m_Size)
            {
                m_Size = s;
                Invalidate();
            }
        }
        public void ChkMinMaxSize()
        {
            if (m_MinimumSize.Width > m_MaximumSize.Width)
            {
                m_MinimumSize.Width = 0;
                m_MaximumSize.Width = 0;
            }
            if (m_MinimumSize.Height > m_MaximumSize.Height)
            {
                m_MinimumSize.Height = 0;
                m_MaximumSize.Height = 0;
            }
        }
		public Size ChkSize(Size sz)
        {
            Size ret = new Size(sz.Width, sz.Height);
            if (m_MinimumSize.Width > 0)
            {
                if (ret.Width < m_MinimumSize.Width)
                {
                    ret.Width = m_MinimumSize.Width;
                }
            }
            if (m_MinimumSize.Height > 0)
            {
                if (ret.Height < m_MinimumSize.Height)
                {
                    ret.Height = m_MinimumSize.Height;
                }
            }
            if (m_MaximumSize.Width > 0)
            {
                if (ret.Width > m_MaximumSize.Width)
                {
                    ret.Width = m_MaximumSize.Width;
                }
            }
            if (m_MaximumSize.Height > 0)
            {
                if (ret.Height > m_MaximumSize.Height)
                {
                    ret.Height = m_MaximumSize.Height;
                }
            }
            return ret;
        }
        */
        protected Point m_Location = new Point(0, 0);
        public Point Location
        {
            get { return m_Location; }
            set
            {
                if (m_Location != value)
                {
                    m_Location = value;
                    Invalidate();
                }
            }
        }
        public Rectangle Bounds
        {
            get { return new Rectangle(m_Location, m_Size); }
        }
        public Rectangle Rect()
        {
            return new Rectangle(m_Location, m_Size);
        }

        // ************************************************************************
        // ************************************************************************
        public TR_Control()
        {
            ChkOffScr();
        }
        // ************************************************************************
        public virtual void SetTRForm(TR_Form fm)
        {
            m_form = fm;
            if (m_form != null)
            {
                m_font = m_form.MyFont(m_FontIndex, m_FontSize, m_form.FontStyle);
                ChkOffScr();
            }
            Invalidate();
        }
        // ************************************************************************
        public void ChkOffScr()
        {
            if (m_Size != m_OffScr.Size)
            {
                m_OffScr = new Bitmap(m_Size.Width, m_Size.Height, PixelFormat.Format32bppArgb);
            }
            if (m_form != null)
            {
                Graphics g = Graphics.FromImage(m_OffScr);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);
                Draw(g);
            }
        }
        // ************************************************************************
        public void Invalidate()
        {
            if (m_form != null) m_form.Invalidate();
        }
		public void Refresh()
		{
			if (m_form != null) m_form.Refresh();
		}
		// ************************************************************************
		public virtual void Draw(Graphics g)
        {
            if (m_form != null)
            {
                Pen p = new Pen(m_form.Colors.Line);
                DrawFrame(g, p, new Rectangle(0, 0, Width, Height));
                p.Dispose();
            }
        }
        // ************************************************************************
        public virtual void SetLocSize()
        {
            m_Location = new Point(0, 0);
            if (m_form != null)
            {
                m_Size = new Size(m_form.Width, MenuHeight);
                ChkOffScr();
            }
        }
        // ************************************************************************
        static public void DrawFrame(Graphics g, Pen p, Rectangle r, int pw = 1)
        {
            Rectangle r2 = new Rectangle(r.Left, r.Top, r.Width - 1, r.Height - 1);
            if (pw < 1) pw = 1;
            p.Width = 1;
            for (int i = 0; i < pw; i++)
            {
                g.DrawRectangle(p, r2);
                r2 = new Rectangle(r.Left + 1, r.Top + 1, r.Width - 2, r.Height - 2);
            }
        }
        // ************************************************************************
        public void Fill(Graphics g, SolidBrush sb)
        {
            g.FillRectangle(sb, Bounds);
        }
        // ************************************************************************
        public void Fill(Graphics g, Color color)
        {
            SolidBrush sb = new SolidBrush(color);
            try
            {
                g.FillRectangle(sb, Bounds);
            }
            finally
            {
                sb.Dispose();
            }
        }
        // ************************************************************************
        static public void Fill(Graphics g, SolidBrush sb, Rectangle rct)
        {
            g.FillRectangle(sb, rct);
        }
        // ************************************************************************
        static public void Fill(Graphics g, Color color, Rectangle rct)
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
        public void DrawStr(Graphics g, string s, SolidBrush sb, Rectangle rct)
        {
            try
            {
                if (m_form != null)
                {
                    g.DrawString(s, m_font, sb, rct, m_form.StringFormat);
                }
            }
            catch
            {

            }
        }
        // ************************************************************************

        // ************************************************************************
        static public void DrawHorLine(Graphics g, Pen p, int x0, int x1, int y)
        {
            g.DrawLine(p, x0, y, x1, y);
        }
        // ************************************************************************
        static public void DrawVerLine(Graphics g, Pen p, int x, int y0, int y1)
        {
            g.DrawLine(p, x, y0, x, y1);
        }
        // ************************************************************************
        static public void DrawBatsu(Graphics g, Pen p, Rectangle r)
        {
            float cx = r.Left + (float)r.Width / 2;
            float cy = r.Top + (float)r.Height / 2;
            float l = (float)r.Height / 2 - 4;
            g.DrawLine(p, cx - l, cy - l, cx + l, cy + l);
            g.DrawLine(p, cx - l, cy + l, cx + l, cy - l);
        }
        // ************************************************************************
        static public Color EnabledColor(Color col, bool isEnabled)
        {
            if (isEnabled)
            {
                return col;
            }
            else
            {
                return Color.FromArgb(col.A, col.R / 2, col.G / 2, col.B / 2);
            }
        }
        // ************************************************************************
        protected bool m_MDown = false;
        protected Point m_MDownPoint = new Point(0, 0);
        protected Point m_MMovePoint = new Point(0, 0);
        protected Point m_MUpPoint = new Point(0, 0);
        protected bool m_inMouse = false;
        // **********************************************************************


        // **********************************************************************
        protected bool InMouse(int x, int y)
        {
            return ((x >= 0) && (y >= 0) && (x < m_Size.Width) && (y < m_Size.Height));
        }
        // **********************************************************************
        public virtual bool ChkMouseDown(MouseEventArgs e)
        {
            bool ret = false;
            int x = e.X - m_Location.X;
            int y = e.Y - m_Location.Y;
            m_inMouse = InMouse(x, y);
            if (m_inMouse)
            {
                m_MDown = true;
                m_MDownPoint = new Point(x, y);
            }
            return ret;
        }
        // **********************************************************************
        public virtual bool ChkMouseMove(MouseEventArgs e)
        {
            bool ret = false;
            int x = e.X - m_Location.X;
            int y = e.Y - m_Location.Y;

            m_inMouse = InMouse(x, y);
            m_MMovePoint = new Point(x, y);
            return ret;
        }
        // **********************************************************************
        public virtual bool ChkMouseUp(MouseEventArgs e)
        {
            bool ret = false;
            int x = e.X - m_Location.X;
            int y = e.Y - m_Location.Y;
            m_inMouse = InMouse(x, y);
            if (m_MDown)
            {
                m_MDown = false;
            }
            m_MUpPoint = new Point(x, y);
            return ret;
        }
		// **********************************************************************
		protected virtual bool ChkMouseLeave(EventArgs e)
        {
			bool ret = false;
			m_MDown = false;
	        m_inMouse = false;
			ChkOffScr();
			Invalidate();

			return ret;
		}

	}
}
