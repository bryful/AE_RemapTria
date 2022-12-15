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
using AE_RemapTria;

using BRY;

namespace AE_RemapTria
{
	public partial class TR_BaseDialog : Form
	{
		#region Contol
		public TR_Form? Form = null;
		public TR_Grid? Grid = null;
		public TR_CellData?  CellData = null;
		public TR_Size?  Sizes = null;
		public TR_Colors? Colors = null;
		#endregion
		#region Font
		public T_MyFonts MyFonts = new T_MyFonts();
		protected int m_MyFontIndex = 5;
		[Category("_AE_Remap")]
		public int MyFontIndex
		{
			get { return m_MyFontIndex; }
			set
			{
				m_MyFontIndex = value;
				if (m_MyFontIndex < 0) m_MyFontIndex = 0;
				if (this.MyFonts != null)
				{
					this.Font = MyFonts.MyFont(m_MyFontIndex, m_MyFontSize, m_MyFontStyle);
					SetFontIndex(value);
				}
				else
				{
					this.Font = new Font(this.Font.Name, m_MyFontSize, m_MyFontStyle);
				}
			}
		}
		public void SetFontIndex(int idx)
		{
			if(this.Controls.Count>0)
			{
				foreach(Control c in this.Controls)
				{
					if( c is TR_DialogControl)
					{
						((TR_DialogControl)c).MyFontIndex = idx;
					}
				}
			}

		}
		protected float m_MyFontSize = 8;
		[Category("_AE_Remap")]
		public float MyFontSize
		{
			get { return this.Font.Size; }
			set
			{
				m_MyFontSize = value;
				SetFontSizeStyle(value, this.Font.Style);
			}
		}
		protected FontStyle m_MyFontStyle = FontStyle.Regular; 
		[Category("_AE_Remap")]
		public FontStyle MyFontStyle
		{
			get { return this.Font.Style; }
			set
			{
				m_MyFontStyle = value;
				SetFontSizeStyle(this.Font.Size, value);
			}
		}
		public void SetFontSizeStyle(float sz, FontStyle fs)
		{
			m_MyFontSize = sz;
			m_MyFontStyle = fs;
			if (this.MyFonts != null)
			{
				this.Font = MyFonts.MyFont(m_MyFontIndex, sz, fs);
			}
			else
			{
				this.Font = new Font(this.Font.FontFamily, sz, fs);
			}
		}
		// ************************************************************************
		  
		// ************************************************************************
		private StringFormat m_format = new StringFormat();
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
		#endregion

		#region Kagi
		private int m_KagiWidth= 30;
		private int m_KagiHeight = 10;
		private int m_KagiWeight = 5;
		[Category("_AE_Remap")]
		public int KagiWidth
		{
			get { return this.m_KagiWidth; }
			set { this.m_KagiWidth = value; this.Invalidate(); }
		}
		[Category("_AE_Remap")]
		public int KagiHeight
		{
			get { return this.m_KagiHeight; }
			set { this.m_KagiHeight = value; this.Invalidate(); }
		}
		[Category("_AE_Remap")]
		public int KagiWeight
		{
			get { return this.m_KagiWeight; }
			set { this.m_KagiWeight = value; this.Invalidate(); }
		}
		#endregion


		#region Edge
		private Rectangle m_Edge = new Rectangle(10, 5, 10, 10);
		[Category("_AE_Remap")]
		public Rectangle Edge
		{
			get {return m_Edge; }
			set { m_Edge = value; this.Invalidate(); }
		}
		private Color m_EdgeColor = Color.FromArgb(128, 200, 200, 255);
		[Category("_AE_Remap")]
		public Color EdgeColor
		{
			get { return m_EdgeColor; }
			set { m_EdgeColor = value; this.Invalidate(); }
		}
		#endregion

		#region Scale
		private Color m_ScaleColor = Color.FromArgb(255, 100, 100, 200);
		[Category("_AE_Remap")]
		public Color ScaleColor
		{
			get { return m_ScaleColor; }
			set { m_ScaleColor = value; this.Invalidate(); }
		}
		private DrawSCalePrm m_side = new DrawSCalePrm();
		private DrawSCalePrm m_TB = new DrawSCalePrm();
		[Category("_AE_Remap")]
		public int SideCenterLength
		{
			get { return m_side.CenterLength; }
			set { m_side.CenterLength = value; this.Invalidate(); }
		}
		[Category("_AE_Remap")]
		public int SideCenterWeight
		{
			get { return m_side.CenterWeight; }
			set { m_side.CenterWeight = value; this.Invalidate(); }
		}

		[Category("_AE_Remap")]
		public int[] SideInter
		{
			get { return m_side.Inter; }
			set
			{
				m_side.Inter = value;
				this.Invalidate();
			}
		}
		[Category("_AE_Remap")]
		public int[] SideWeight
		{
			get { return m_side.Weight; }
			set
			{
				m_side.Weight = value;
				this.Invalidate();
			}
		}
		[Category("_AE_Remap")]
		public int[] SideLength
		{
			get { return m_side.Length; }
			set
			{
				m_side.Length = value;
				this.Invalidate();
			}
		}
		[Category("_AE_Remap")]
		public int[] SideCount
		{
			get { return m_side.Count; }
			set
			{
				m_side.Count = value;
				this.Invalidate();
			}
		}
		[Category("_AE_Remap")]
		public int TBCenterLength
		{
			get { return m_TB.CenterLength; }
			set { m_TB.CenterLength = value; this.Invalidate(); }
		}
		[Category("_AE_Remap")]
		public int TBCenterWeight
		{
			get { return m_TB.CenterWeight; }
			set { m_TB.CenterWeight = value; this.Invalidate(); }
		}

		[Category("_AE_Remap")]
		public int[] TBInter
		{
			get { return m_TB.Inter; }
			set
			{
				m_TB.Inter = value;
				this.Invalidate();
			}
		}
		[Category("_AE_Remap")]
		public int[] TBWeight
		{
			get { return m_TB.Weight; }
			set
			{
				m_TB.Weight = value;
				this.Invalidate();
			}
		}
		[Category("_AE_Remap")]
		public int[] TBLength
		{
			get { return m_TB.Length; }
			set
			{
				m_TB.Length = value;
				this.Invalidate();
			}
		}
		[Category("_AE_Remap")]
		public int[] TBCount
		{
			get { return m_TB.Count; }
			set
			{
				m_TB.Count = value;
				this.Invalidate();
			}
		}
		#endregion


		private Color m_FrameColor = Color.FromArgb(255, 80, 80, 100);
		[Category("_AE_Remap")]
		public Color FrameColor
		{
			get { return m_FrameColor; }
			set { m_FrameColor = value; this.Invalidate(); }
		}
		// ************************************************************************
		public TR_BaseDialog()
		{
			AutoScaleMode = AutoScaleMode.None;
			InitializeComponent();
			Init();
		}

		// ************************************************************************
		// ************************************************************************
		private bool m_CanReSize = false;
		[Category("_AE_Remap")]
		public bool CanReSize
		{
			get { return m_CanReSize; }
			set { m_CanReSize = value; }
		}
		// ************************************************************************
		public void Init()
		{

			this.SetStyle(
				ControlStyles.DoubleBuffer |
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint|
				ControlStyles.SupportsTransparentBackColor,
				true);
			//this.BackColor = Color.Transparent;
			//this.TransparencyKey = Color.Transparent;
			this.UpdateStyles();
			Alignment = StringAlignment.Far;
			LineAlignment = StringAlignment.Center;
			this.FormBorderStyle = FormBorderStyle.None;
			this.TopMost = true;
			this.Font = MyFonts.MyFont(m_MyFontIndex, m_MyFontSize, m_MyFontStyle);
			SetTRFormToControl();
		}
		protected override void InitLayout()
		{
			if (MyFonts != null)
			{
				this.Font = MyFonts.MyFont(m_MyFontIndex, this.Font.Size, this.Font.Style);
			}
			base.InitLayout();
		}
		protected override void OnControlAdded(ControlEventArgs e)
		{
			base.OnControlAdded(e);
			SetTRFormToControl();
		}
		// ****************************************************************
		public void ToCenter()
		{
			Rectangle rct = Screen.PrimaryScreen.Bounds;
			Point p = new Point((rct.Width - this.Width) / 2, (rct.Height - this.Height) / 2);
			this.Location = p;
			F_W.SetForegroundWindow(this.Handle);
		}
		// *****************************************************************
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
		}
		// *****************************************************************
		public virtual void SetTRFormToControl()
		{
			if(this.Controls.Count>0)
			{
				foreach(Control c in this.Controls)
				{
					if( c is TR_DialogControl)
					{
						try
						{
							((TR_DialogControl)c).SetTRDialog(this);
						}
						catch
						{

						}
					}else if (c is T_BListBox)
					{
						try
						{
							((T_BListBox)c).SetTRDialog(this);
						}
						catch
						{

						}
					}
				}
			}
		}


		public virtual void SetTRForm(TR_Form fm)
		{
			Form = fm;
			if (Form != null)
			{
				Grid = fm.Grid;
				Colors = fm.Colors;
				Sizes = fm.Sizes;
				CellData = fm.CellData;
				//MyFonts =Form.MyFonts;
				
			}
			this.Font = MyFonts.MyFont(m_MyFontIndex, m_MyFontSize, m_MyFontStyle);
		}
		#region Mouse Event
		protected override void OnDoubleClick(EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			base.OnDoubleClick(e);
		}
		private Point m_MD = new Point(0, 0);
		private Point m_MDF = new Point(0, 0);
		private int m_MD_Mode = 0;
		private Size m_MD_Size = new Size(0, 0);
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if((e.X>this.Width-25)&& (e.Y > this.Height - 25)&&(m_CanReSize==true))
				{
					m_MD_Mode = 2;
					m_MD_Size = new Size(this.Width, this.Height);
					this.Invalidate();
				}
				else
				{
					m_MD_Mode = 1;
				}
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
				if (m_MD_Mode == 1)
				{
					this.Location = new Point(ax + this.Left, ay + this.Top);
				}else if(m_MD_Mode==2)
				{
					this.Size = new Size(ax + m_MD_Size.Width, ay + m_MD_Size.Height);
					this.Invalidate();
				}
				/*if(m_Form != null)
				{
					m_Form.Location = new Point(ax + m_Form.Left, ay + m_Form.Top);
				}*/
			}
			base.OnMouseMove(e);
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if(m_MD_Mode!=0)
			{
				m_MD_Mode = 0;
				this.Invalidate();
			}
			base.OnMouseUp(e);
		}
		protected override void OnMouseLeave(EventArgs e)
		{
			if (m_MD_Mode != 0)
			{
				m_MD_Mode = 0;
				this.Invalidate();
			}
			base.OnMouseLeave(e);
		}
		#endregion

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			this.Invalidate();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			//base.OnPaint(e);
			Graphics g = e.Graphics;
			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
			SolidBrush sb = new SolidBrush(this.BackColor);
			Pen p = new Pen(m_ScaleColor);
			//if(m_grid == null) return;
			try
			{
				Rectangle r = new Rectangle(0, 0, this.Width, this.Height);
				T_G.GradBG(g, this.ClientRectangle);
				if(m_MD_Mode==2)
				{
					sb.Color= Color.FromArgb(64, m_EdgeColor.R, m_EdgeColor.G, m_EdgeColor.B);
					r = new Rectangle(this.Width-25, this.Height-25, 25, 25);
					g.FillRectangle(sb,r);	
				}

				p.Color = this.ForeColor;
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

					p.Color = m_ScaleColor;
					m_side.Style = Scale_Style.Left;
					m_side.Loc = new Point(8, this.Height / 2);
					T_G.DrawScale(g, p, m_side);
					m_side.Style = Scale_Style.Right;
					m_side.Loc = new Point(this.Width-8, this.Height / 2);
					T_G.DrawScale(g, p, m_side);

					m_TB.Style = Scale_Style.Top;
					m_TB.Loc = new Point(this.Width/2, 5);
					T_G.DrawScale(g, p, m_TB);
					m_TB.Style = Scale_Style.Bottom;
					m_TB.Loc = new Point(this.Width/2, this.Height-5);
					T_G.DrawScale(g, p, m_TB);

				}

				sb.Color = m_ScaleColor;
				DrawKagiPrm kp = new DrawKagiPrm();
				kp.HLength = m_KagiWidth;
				kp.HWeight = m_KagiWeight;
				kp.VLength = m_KagiHeight;
				kp.VWeight = m_KagiWeight;
				if ((m_KagiWeight > 0)&&(m_KagiWidth > 0)&&(m_KagiHeight > 0))
				{
					kp.Loc = new Point(0, 0);
					T_G.DrawKagi(g, sb, kp, Kagi_Style.TL);
					kp.Loc = new Point(this.Width, 0);
					T_G.DrawKagi(g, sb, kp, Kagi_Style.TR);
					kp.Loc = new Point(0, this.Height);
					T_G.DrawKagi(g, sb, kp, Kagi_Style.BL);
					kp.Loc = new Point(this.Width, this.Height);
					T_G.DrawKagi(g, sb, kp, Kagi_Style.BR);
				}

				p.Color = m_FrameColor;
				DrawFrame(g, p);
			}
			finally
			{
				sb.Dispose();
				p.Dispose();
			}
		}
		#region Graphics
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
		#endregion
		// ************************************************************************
		/// <summary>
		/// 子コントロールにMouseイベントハンドラを設定(再帰)
		/// </summary>
		public void SetEventHandler(Control objControl)
		{
			// イベントの設定
			// (親フォームにはすでにデザイナでマウスのイベントハンドラが割り当ててあるので除外)
			//if (objControl != this)
			//{
			objControl.MouseDown += (sender, e) => this.OnMouseDown(e);
			objControl.MouseMove += (sender, e) => this.OnMouseMove(e);
			objControl.MouseUp += (sender, e) => this.OnMouseUp(e);
			//}
			/*
			// さらに子コントロールを検出する
			if (objControl.Controls.Count > 0)
			{
				foreach (Control objChildControl in objControl.Controls)
				{
					SetEventHandler(objChildControl);
				}
			}
			*/
		}

	}
}
