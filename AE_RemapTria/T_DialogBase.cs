using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BRY;
namespace AE_RemapTria
{
	public partial class T_DialogBase : Form
	{
		private T_Form? m_Form = null;
		private T_Grid? m_grid = null;
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

		public T_DialogBase()
		{
			InitializeComponent();
			Init();
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
			if (m_Form == null) return;
			m_grid = fm.Grid;
			m_MyFonts = fm.Grid.MyFonts;
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Graphics g = e.Graphics;
			//if(m_grid == null) return;
			SolidBrush sb = new SolidBrush(Color.FromArgb(128,50,50,100));
			try
			{
				g.FillRectangle(sb,this.ClientRectangle);
				if (m_grid != null)
				{
					sb.Color = m_grid.Colors.Moji;
				}
				else
				{
					sb.Color = Color.White;
				}
				Rectangle r = new Rectangle(10, 5, 15,15);
				g.FillRectangle(sb, r);
				r = new Rectangle(30, 5, this.Width-30,25);
				m_format.Alignment = StringAlignment.Near;
				m_format.LineAlignment = StringAlignment.Center;
				g.DrawString(this.Text,this.Font,sb,r);

			}
			finally
			{
				sb.Dispose();
			}
		}
	}
}
