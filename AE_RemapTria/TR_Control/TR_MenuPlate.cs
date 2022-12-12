using BRY;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection.Metadata;
using System.Security.Cryptography.Xml;
using SixLabors.Fonts;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Diagnostics;

namespace AE_RemapTria
{
	public partial class TR_MenuPlate : TR_BaseDialog
	{
		 
		private T_SubMenuItem[] m_Items = new T_SubMenuItem[0];
		private TR_Menu? m_menu = null;
		public override void SetTRForm(TR_Form fm)
		{
			base.SetTRForm(fm);
			m_form = fm;
			if (m_form != null)
			{
				m_grid = fm.Grid;
				Colors = fm.Colors;
				m_menu = fm.Menu;
				this.MyFonts = m_form.MyFonts;
				m_MyFontIndex = m_menu.FontIndex;
				m_MyFontSize = m_menu.FontSize;
				this.Font = MyFonts.MyFont(m_MyFontIndex, m_MyFontSize, this.Font.Style);

			}
		}

		private int m_MenuHeight = 20;
		[Category("_AE_Remap")]
		public int MenuHeight
		{
			get { return m_MenuHeight; }
			set 
			{
				if(m_MenuHeight != value)
				{
					m_MenuHeight = value;
					SetRegion();
				}
				this.Invalidate();
			}
		}
		private int m_Width = 200;
		private int m_MenuWidth = 80;
		//private int m_ItemSizeMax = 400;
		[Category("_AE_Remap")]
		public int MenuWidth
		{
			get { return m_MenuWidth; }
			set
			{
				if (m_MenuWidth != value)
				{
					m_MenuWidth = value;
					SetRegion();
				}
				this.Invalidate();
			}
		}
		private int m_RowHeight = 16;
		private int m_LeftSideWidth = 20;

		private Color m_WakuColor = Color.FromArgb(100, 100, 200);
		private Color m_LeftSideColor1 = Color.FromArgb(120, 120, 240);
		private Color m_LeftSideColor2 = Color.FromArgb(60, 60, 140);
		private Color m_CaptionBackColor = Color.FromArgb(95, 108, 176);
		private Color m_CaptionForeColor = Color.FromArgb(194, 202, 243);
		private Color m_MojiColor = Color.FromArgb(194, 202, 243);
		private Color m_SelectedColor = Color.FromArgb(25, 25, 100);
		private Color m_ClickColor = Color.FromArgb(100,100, 200);

		private int m_Index = -1;
		public int Index
		{
			get { return m_Index; }
		}
		private int m_SubIndex = -1;
		public int SubIndex
		{
			get { return m_SubIndex; }
		}


		public TR_MenuPlate()
		{

			InitializeComponent();
			MyFontSize = 9;
			MyFontIndex = 5;
			//this.Font = MyFonts.MyFont(MyFontIndex, MyFontSize, this.Font.Style);

			this.SetStyle(
			ControlStyles.DoubleBuffer |
			ControlStyles.UserPaint |
			ControlStyles.AllPaintingInWmPaint,
			//ControlStyles.SupportsTransparentBackColor,
			true);
			this.UpdateStyles();
			this.FormBorderStyle = FormBorderStyle.None;
			this.TopMost = true;
			this.Size = new Size(m_Width, this.Height);
		}
		private void SetRegion()
		{
			GraphicsPath path = new GraphicsPath();
			path.AddRectangle(this.ClientRectangle);
			path.AddRectangle(new Rectangle(m_MenuWidth,0,this.Width-m_MenuWidth,m_MenuHeight));

			this.Region = new Region(path);
		}
		// ********************************************************
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			SetRegion();
		}
		// ********************************************************
		protected override void OnPaint(PaintEventArgs e)
		{
			//base.OnPaint(e);
			Graphics g = e.Graphics;
			SolidBrush sb = new SolidBrush(this.BackColor);
			Pen p = new Pen(this.ForeColor);
			try
			{
				Rectangle r = ClientRectangle;
				T_G.GradBG(g, r);
				
				//Caption
				r = new Rectangle(0, 0, m_MenuWidth+40, m_MenuHeight);
				sb.Color = Color.Black;
				g.FillRectangle(sb, r);
				r = new Rectangle(0, 0, m_MenuWidth, m_MenuHeight);
				StringFormat sf = new StringFormat();
				sf.Alignment = StringAlignment.Center;
				sf.LineAlignment = StringAlignment.Center;
				sb.Color = m_CaptionForeColor;
				g.DrawString(this.Text, this.Font, sb, r, sf);
				Debug.WriteLine($"MP {this.Font.Name} : {this.Font.Size}");
				// LeftSide
				r = new Rectangle(0, m_MenuHeight, m_LeftSideWidth, this.Height - m_MenuHeight);
				T_G.GradV(g, m_LeftSideColor1, m_LeftSideColor2, r);

				//項目の描画
				if (m_Items.Length > 0)
				{
					for(int i = 0; i < m_Items.Length; i++)
					{
						sb.Color = m_MojiColor;
						if ((m_Items[i]==null)||(m_Items[i].Caption==""))
						{
							sb.Color = m_MojiColor;
							int ls = 1;
							r = new Rectangle(
								m_LeftSideWidth, 
								m_RowHeight * i + (m_RowHeight-ls)/2 + m_MenuHeight + m_RowHeight/2, 
								this.Width - (m_LeftSideWidth),
								ls);
							g.FillRectangle(sb, r);

						}
						else
						{
							int ds = 8;
							if (m_SubIndex==i)
							{
								r = new Rectangle(
								m_LeftSideWidth,
								m_RowHeight * i + m_MenuHeight + m_RowHeight/2,
								this.Width - (m_LeftSideWidth),
								m_RowHeight);
								if(m_IsClick)
								{
									sb.Color = m_ClickColor;
								}
								else
								{
									sb.Color = m_SelectedColor;
								}
								g.FillRectangle(sb, r);
							}
							sb.Color = m_MojiColor;
							// 横のドット
							if (m_SubIndex != i)
							{
								r = new Rectangle(
									m_LeftSideWidth + ds / 2,
									m_RowHeight * i + (m_RowHeight - ds) / 2 + m_MenuHeight + m_RowHeight / 2,
									ds, ds);
							}
							g.FillRectangle(sb, r);

							r = new Rectangle(
								m_LeftSideWidth + ds*2, 
								m_RowHeight * i +  m_MenuHeight + m_RowHeight / 2, 
								this.Width-(m_LeftSideWidth+ds*2+5), 
								m_RowHeight);
							sf.Alignment = StringAlignment.Near;
							g.DrawString(m_Items[i].Caption, this.Font, sb, r, sf);
							sf.Alignment = StringAlignment.Far;
							string k = m_Items[i].Shrtcut;
							if (k != "")
							{
								g.DrawString(k, this.Font, sb, r, sf);
							}
						}
					}
				}
				//枠線
				r = new Rectangle(0,m_MenuHeight,this.Width,this.Height-m_MenuHeight);
				p.Color = m_WakuColor;
				DrawFrame(g, p, r,1);


			}
			finally
			{
				sb.Dispose();
				p.Dispose();
			}
		}
		// ********************************************************
		public void QShow()
		{
			if (m_menu == null) return;
			Rectangle r = m_menu.CaptionRect(m_Index);
			this.Location = new Point(m_menu.GLoc.X + r.Left, m_menu.GLoc.Y);
			this.Show();
		}
		public void AddSubMenuItems(int idx)
		{
			m_Items = new T_SubMenuItem[0];
			if (m_menu == null) return;
			m_Items  = m_menu.SubMenus(idx);
			m_Index = idx;
			Rectangle r = m_menu.CaptionRect(idx);
			m_MenuWidth = r.Width;
			m_MenuHeight = r.Height;
			this.Text = m_menu.Caption(idx);
			if (m_Items.Length <= 0) return;

			Bitmap bmp = new Bitmap(1000, m_RowHeight);
			Graphics g = Graphics.FromImage(bmp);
			StringFormat sf = new StringFormat();
			int w = 0;
			for(int i = 0; i < m_Items.Length; i++)
			{
				string s = m_Items[i].Caption + " " + m_Items[i].Shrtcut;
				SizeF sz = g.MeasureString(s, this.Font, 1000, sf);
				if (w < (int)sz.Width) w = (int)sz.Width;
			}
			bmp.Dispose();
			int h = m_MenuHeight + m_RowHeight * (m_Items.Length) + m_RowHeight;
			this.Size = new Size(w + 40, h);
		}
		protected override void OnDeactivate(EventArgs e)
		{
			if (m_IsClick == false)
			{
				m_SubIndex = -1;
				this.Hide();
			}
			base.OnDeactivate(e);
			//this.Close();
		}
		protected override void OnMouseLeave(EventArgs e)
		{
			if (m_IsClick == false)
			{
				m_SubIndex = -1;
				this.Hide();
			}
		}
		private bool m_IsClick = false;
		private int getMP(MouseEventArgs e)
		{
			int ret = -1;
			int y = (e.Y - m_MenuHeight - m_RowHeight / 2) / m_RowHeight;
			if ((y >= 0) && (y < m_Items.Length))
			{
				if ((m_Items[y]!=null) &&(m_Items[y].Caption!=""))
				{
					ret = y;
					this.Invalidate();
				}
			}
			return ret;
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			m_SubIndex = getMP(e);
			this.Invalidate();
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			m_SubIndex = getMP(e);
			if (m_SubIndex >= 0)
			{
				m_IsClick = true;
				this.Invalidate();
				m_IsClick = false;
				this.Invalidate();
				int idx = m_SubIndex;
				m_SubIndex = -1;
				this.Hide();
				m_Items[idx].Exec();
			}
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			m_IsClick = false;
			m_SubIndex = -1;
			this.Hide();

		}
	}
}
