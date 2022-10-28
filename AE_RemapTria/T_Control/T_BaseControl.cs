using System.Drawing.Drawing2D;

namespace AE_RemapTria
{
	public partial class T_BaseControl : Control
	{
		private T_MyFonts? m_MyFonts = null;
		/// <summary>
		/// リソースフォント管理のコンポーネント
		/// </summary>
		public T_MyFonts? MyFonts
		{
			get { return m_MyFonts; }
			set
			{
				m_MyFonts = value;
				if (m_MyFonts != null)
				{
					this.Font = m_MyFonts.MyFont(m_MyFontIndex, this.Font.Size, this.Font.Style);
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
		}
		// ************************************************************************
		private StringFormat m_format = new StringFormat();
		private StringAlignment m_LineAlignment_Bak;
		private StringAlignment m_Alignment_Bak;
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
		private float m_FontSize_Bak;
		private FontStyle m_FontStyle_Bak;
		// ************************************************************************
		public T_BaseControl()
		{
			this.Size = new Size(100, 100);
			this.Location = new Point(0, 0);
			InitializeComponent();
			Init();
		}
		protected override void InitLayout()
		{
			if (m_MyFonts != null)
			{
				this.Font = m_MyFonts.MyFont(m_MyFontIndex, this.Font.Size, this.Font.Style);
			}
			base.InitLayout();
		}
		// ************************************************************************
		public void Init()
		{

			this.SetStyle(
				ControlStyles.DoubleBuffer |
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.SupportsTransparentBackColor,
				true);
			this.BackColor = Color.White;
			this.UpdateStyles();
			m_Alignment_Bak =
			Alignment = StringAlignment.Far;
			m_LineAlignment_Bak =
			LineAlignment = StringAlignment.Center;
			m_FontSize_Bak = this.Font.Size;
			m_FontStyle_Bak = this.Font.Style;

		}
		// ************************************************************************
		public void PushFontStatus()
		{
			m_Alignment_Bak = Alignment;
			m_LineAlignment_Bak = LineAlignment;
			m_FontSize_Bak = MyFontSize;
			m_FontStyle_Bak = MyFontStyle;
		}
		// ************************************************************************
		public void PopFontStatus()
		{
			Alignment = m_Alignment_Bak;
			LineAlignment = m_LineAlignment_Bak;
			SetFontSizeStyle(m_FontSize_Bak, m_FontStyle_Bak);
		}
		// ************************************************************************
		protected override void OnPaint(PaintEventArgs pe)
		{
			//base.OnPaint(pe);
			Graphics g = pe.Graphics;
			Fill(g, this.BackColor);
		}
		// ************************************************************************
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
			try
			{
				//g.SmoothingMode = SmoothingMode.AntiAlias;
				g.DrawString(s, this.Font, sb, rct, m_format);
			}
			catch
			{

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
		public void DrawBatsu(Graphics g, Pen p, Rectangle r)
		{
			g.DrawLine(p, r.Left,r.Top,r.Right,r.Bottom);
			g.DrawLine(p, r.Left, r.Bottom, r.Right, r.Top);
		}
		public Color EnabledColor(Color col,bool isEnabled)
		{
			if(isEnabled)
			{
				return col;
			}
			else
			{
				return Color.FromArgb(col.A,col.R/2, col.G / 2, col.B / 2);
			}
		}
	}
}
