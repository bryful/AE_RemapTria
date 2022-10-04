using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BRY;
namespace AE_RemapTria
{
	public partial class T_BaseDialog : Form
	{
		private T_Form? m_Form = null;
		private T_Grid? m_grid = null;
		private T_MyFonts? m_MyFonts = null;
		public T_MyFonts? MyFonts
		{
			get { return m_MyFonts; }
			set
			{
				SetMyFont(value);
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
				if (this.MyFonts != null)
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
			if (this.MyFonts != null)
			{
				this.Font = m_MyFonts.MyFont(m_MyFontIndex, sz, fs);
			}
			else
			{
				this.Font = new Font(this.Font.FontFamily, sz, fs);
			}
		}
		private StringFormat m_format = new StringFormat();
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
		private Color m_EdgeColor = Color.FromArgb(128, 200, 200, 255);
		public Color EdgeColor
		{
			get { return m_EdgeColor; }
			set { m_EdgeColor=value; this.Invalidate(); }
		}

		private Rectangle m_Edge = new Rectangle(10, 5, 10, 10);
		public Rectangle Edge
		{
			get {return m_Edge; }
			set { m_Edge = value; this.Invalidate(); }
		}
		public T_BaseDialog()
		{
			InitializeComponent();
			Init();
		}
		// ************************************************************************
		public void SetMyFont(T_MyFonts mf)
		{
			m_MyFonts = mf;
			if (m_MyFonts != null)
			{
				this.Font = m_MyFonts.MyFont(m_MyFontIndex, this.Font.Size, this.Font.Style);
	
			}
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
			this.BackColor = Color.Transparent;
			this.TransparencyKey = Color.Transparent;
			this.UpdateStyles();
			Alignment = StringAlignment.Far;
			LineAlignment = StringAlignment.Center;
			this.FormBorderStyle = FormBorderStyle.None;
			this.TopMost = true;


		}
		protected override void InitLayout()
		{
			if (m_MyFonts != null)
			{
				this.Font = m_MyFonts.MyFont(m_MyFontIndex, this.Font.Size, this.Font.Style);
			}
			base.InitLayout();
		}       
		// *****************************************************************
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
		}
		// *****************************************************************
		public void SetForm(T_Form fm)
		{
			m_Form = fm;
			if (m_Form != null)
			{
				m_grid = fm.Grid;
				this.MyFonts =m_grid.MyFonts;
			}
		}
		protected override void OnDoubleClick(EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			base.OnDoubleClick(e);
		}
		private Point m_MD = new Point(0, 0);
		private Point m_MDF = new Point(0, 0);

		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				m_MD = e.Location;
			}
			base.OnMouseDown(e);
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				int ax = e.X - m_MD.X;
				int ay = e.Y - m_MD.Y;
				this.Location = new Point(ax+this.Left, ay+this.Top);
				/*if(m_Form != null)
				{
					m_Form.Location = new Point(ax + m_Form.Left, ay + m_Form.Top);
				}*/
			}
			base.OnMouseMove(e);
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			//base.OnPaint(e);
			Graphics g = e.Graphics;
			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
			SolidBrush sb = new SolidBrush(this.BackColor);
			Pen p = new Pen(Color.FromArgb(255, 50, 50, 100));
			//if(m_grid == null) return;
			try
			{
				Rectangle r = new Rectangle(0, 0, this.Width, this.Height);
				sb.Color = Color.Black;
				Fill(g, sb,r);
				DrawBackImage(g,Properties.Resources.Back);
				DrawFrame(g, p);


				if(!m_Edge.IsEmpty)
				{
					int x1 = this.Width - m_Edge.Width - m_Edge.Left;
					int y1 = this.Height - m_Edge.Top - m_Edge.Height;
					sb.Color = m_EdgeColor;
					r = new Rectangle(m_Edge.Location,m_Edge.Size);
					Fill(g, sb, r);
					r.Location = new Point(
						x1,
						m_Edge.Top);
					Fill(g, sb, r);
					r.Location = new Point(
						m_Edge.Left,
						y1);
					Fill(g, sb, r);
					r.Location = new Point(
						x1,
						y1);
					Fill(g, sb, r);

					int h = 3;
					r = new Rectangle(m_Edge.Left, (Height - h) / 2, m_Edge.Width, h);
					Fill(g, sb, r);
					r = new Rectangle(
						Width-(r.Width+m_Edge.Left), 
						r.Top, 
						r.Width, r.Height);
					Fill(g, sb, r);
				}
			}
			finally
			{
				sb.Dispose();
				p.Dispose();
			}
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
				g.SmoothingMode = SmoothingMode.AntiAlias;
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
			g.DrawLine(p, r.Left, r.Top, r.Right, r.Bottom);
			g.DrawLine(p, r.Left, r.Bottom, r.Right, r.Top);
		}
		// ************************************************************************
		public void DrawBackImage(Graphics g, Bitmap img)
		{
			g.DrawImage(img, new Rectangle(0, 0, this.Width, this.Height));
		}
	}
}
