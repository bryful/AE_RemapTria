using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace AE_RemapTria
{
    public partial class TR_DialogControl : Control
	{
		protected TR_Form? m_form = null;
		protected TR_BaseDialog? m_dialog=null;
		protected TR_Colors? Colors = null;
		protected TR_Size? Sizes = null;
		protected TR_Grid? Grid = null;
		protected TR_CellData? CellData = null;
		protected TR_MyFonts? m_MyFonts = null;
		public virtual void SetTRDialog(TR_BaseDialog? bd)
		{
			m_dialog = bd;
			if (m_dialog != null)
			{
				m_form = m_dialog.Form;
				m_MyFonts = m_dialog.MyFonts;
				if (m_form != null)
				{
					Grid = m_form.Grid;
					Colors = m_form.Colors;
					Sizes = m_form.Sizes;
					CellData = m_form.CellData;
					m_MyFonts = m_form.MyFonts;

				}
				if (m_MyFonts != null)
				{
					this.Font = m_MyFonts.MyFont(m_MyFontIndex, m_MyFontSize, this.Font.Style);
				}
			}
		}
		protected int m_MyFontIndex = 5;
		[Category("_AE_Remap")]
		public int MyFontIndex
		{
			get { return m_MyFontIndex; }
			set
			{
				m_MyFontIndex = value;
				if (m_MyFontIndex < 0) m_MyFontIndex = 0;
				if (m_MyFonts != null)
				{
					this.Font = this.Font = m_MyFonts.MyFont(m_MyFontIndex, this.Font.Size, this.Font.Style);
				}
			}
		}
		protected float m_MyFontSize = 5;
		[Category("_AE_Remap")]
		public float MyFontSize
		{
			get { return m_MyFontSize; }
			set
			{
				m_MyFontSize = value;
				SetFontSizeStyle(value, this.Font.Style);
			}
		}
		[Category("_AE_Remap")]
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
		protected StringFormat m_format = new StringFormat();
		protected StringAlignment m_LineAlignment_Bak;
		protected StringAlignment m_Alignment_Bak;
		// ************************************************************************
		[Category("_AE_Remap")]
		public StringAlignment Alignment
		{
			get { return m_format.Alignment; }
			set { m_format.Alignment = value; }
		}
		// ************************************************************************
		[Category("_AE_Remap")]
		public StringAlignment LineAlignment
		{
			get { return m_format.LineAlignment; }
			set { m_format.LineAlignment = value; }
		}
		// ************************************************************************
		protected float m_FontSize_Bak;
		protected FontStyle m_FontStyle_Bak;
		// ************************************************************************
		[Category("_AE_Remap")]
		public Color Back
		{
			get { return BackColor; }
			set
			{
				BackColor = value;
				this.Invalidate();

			}
		}
		protected Color m_FrameColor = Color.White;
		[Category("_AE_Remap")]
		public Color Frame
		{
			get { return m_FrameColor; }
			set
			{
				m_FrameColor = value;
				this.Invalidate();

			}
		}
		protected Padding m_FrameWeight = new Padding(0, 0, 0, 0);
		[Category("_AE_Remap")]
		public Padding FrameWeight
		{
			get { return m_FrameWeight; }
			set
			{
				m_FrameWeight = value;
				this.Invalidate();

			}
		}
		protected Padding m_FrameMargin = new Padding(0, 0, 0, 0);
		[Category("_AE_Remap")]
		public Padding FrameMargin
		{
			get { return m_FrameMargin; }
			set
			{
				m_FrameMargin = value;
				this.Invalidate();

			}
		}
		public Rectangle DrawArea
		{
			get
			{
				Rectangle rct = new Rectangle(
					m_FrameWeight.Left + m_FrameMargin.Left,
					m_FrameWeight.Top + m_FrameMargin.Top,
					Width - m_FrameWeight.Left - m_FrameWeight.Right
					- m_FrameMargin.Left - m_FrameMargin.Right,
					Height - m_FrameWeight.Top - m_FrameWeight.Bottom
					- m_FrameMargin.Top - m_FrameMargin.Bottom
					);
				return rct;
			}
		}
		public Rectangle DrawAreaIn
		{
			get
			{
				Rectangle rct = new Rectangle(
					m_FrameWeight.Left + m_FrameMargin.Left+2,
					m_FrameWeight.Top + m_FrameMargin.Top+2,
					Width - m_FrameWeight.Left - m_FrameWeight.Right
					- m_FrameMargin.Left - m_FrameMargin.Right-4,
					Height - m_FrameWeight.Top - m_FrameWeight.Bottom
					- m_FrameMargin.Top - m_FrameMargin.Bottom-4
					);
				return rct;
			}
		}
		public TR_DialogControl()
		{
			this.Size = new Size(100, 100);
			this.Location = new Point(0, 0);
			InitializeComponent();
			Init();

		}
		// ************************************************************************
		
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
				//ControlStyles.Selectable |
				//ControlStyles.UserMouse |
				ControlStyles.DoubleBuffer |
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint|
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
			m_FrameColor = TR_Colors.GetColor(COLS.Line);
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
			Graphics g = pe.Graphics;
			T_G.GradBG(g, this.ClientRectangle);
			SolidBrush sb = new SolidBrush(m_FrameColor);
			sb.Color = m_FrameColor;
			DrawPadding(g, sb);
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
		public void DrawPadding(Graphics g, SolidBrush sb)
		{
			TRc.DrawFrame(g,sb,this.ClientRectangle,m_FrameWeight);
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
			float cx = (float)r.Left + (float)r.Width / 2;
			float cy = (float)r.Top + (float)r.Height / 2;
			float l = (float)r.Height / 2 - 4;
			g.DrawLine(p, cx - l, cy - l, cx + l, cy + l);
			g.DrawLine(p, cx - l, cy + l, cx + l, cy - l);
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
		private bool GetDesignMode(Control control)
		{
			if (control == null) return false;

			bool mode = control.Site == null ? false : control.Site.DesignMode;

			return mode | GetDesignMode(control.Parent);
		}

		// 本来のDesignModeを上書き
		public new bool DesignMode
		{
			get
			{
				return GetDesignMode(this);
			}
		}
	}
}
