using BRY;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Drawing2D;
namespace AE_RemapTria
{
	public partial class TR_NavBar : TR_BaseDialog
	{
		bool _refFlag = true;
		public override void SetTRForm(TR_Form fm)
		{
			base.SetTRForm(fm);
			if(base.Form != null)
			{
				SetLocSize();
				SetIsFront(m_IsFront);
				base.Form.LocationChanged += M_form_LocationChanged;
				base.Form.SizeChanged += M_form_LocationChanged;
				base.Form.TextChanged += M_form_TextChanged;
			}
		}
		private void M_form_TextChanged(object? sender, EventArgs e)
		{
			if (base.Form != null)
			{
				this.Text = base.Form.Text;
				this.Invalidate();
			}
		}

		private void M_form_LocationChanged(object? sender, EventArgs e)
		{
			SetLocSize();
		}

		private bool m_IsFront = false;
		[Category("_AE_Remap")]
		public bool IsFront
		{
			get { return m_IsFront; }
			set { SetIsFront(value); }
		}

		[Category("_AE_Remap")]
		public string Caption
		{
			get { return this.Text; }
		}

		
		public TR_NavBar()
		{
			this.Size = new Size(160, 20);
			InitializeComponent();
			IsFront = true;
			if(DesignMode==true)
			{
				this.Visible = false;
			}
			this.TopMost = true;
			this.SetStyle(
				ControlStyles.DoubleBuffer |
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint|
				ControlStyles.SupportsTransparentBackColor,
				true);
			this.UpdateStyles();
			if(DesignMode==true)
			{
				this.Visible = false;
			}
		}

		// *****************************************************************
		public void SetLocSize()
		{
			if (base.Form == null) return;
			if (_refFlag == false) return;
			_refFlag = false;
			Size sz = new Size(base.Form.Width, this.Height);
			if (this.Size != sz) this.Size = sz;

			Point p = new Point(base.Form.Left, base.Form.Top - this.Height+1);
			if(this.Location!=p) this.Location = p;

			_refFlag = true;
		}
		public void SetLocToTRForm()
		{
			if (base.Form == null) return;
			if (_refFlag == false) return;
			_refFlag = false;
			Point p = new Point(this.Left, this.Top + this.Height-1);
			if (base.Form.Location != p) base.Form.Location = p;

			_refFlag = true;
		}
		private bool m_md = false;
		private Point mousePoint;
		private Point LocPoint;
		private bool m_CB = false;
		protected override void OnMouseEnter(EventArgs e)
		{
			Debug.WriteLine("Enter");
			base.OnMouseEnter(e);
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			formActive();
			Debug.WriteLine("Down");
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				if(e.X<40)
				{
					IsFront = !IsFront;
				}else if(e.X >this.Width-30)
				{
					base.Form.Quit();
				}
				else
				{
					m_md = true;
					mousePoint = new Point(e.X, e.Y);
					LocPoint = this.Location;
				}
			}
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			Debug.WriteLine("Move");
			if (m_md)
			{
				if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
				{
					Debug.WriteLine("Move");
					int ax = e.X - mousePoint.X;
					int ay = e.Y - mousePoint.Y;
					Point n = new Point(this.Location.X + ax, this.Location.Y + ay);
					if(this.Location != n)
					{
						if (_refFlag == false) return;
						this.Location = n;
						SetLocToTRForm();
					}
				}
			}
			else
			{
				if(e.X>this.Width-30)
				{
					m_CB = true;
					this.Invalidate();
				}
				else
				{
					if(m_CB==true)
					{
						m_CB = false;
						this.Invalidate();
					}
				}
			}
			base.OnMouseMove(e);
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			Debug.WriteLine("Up");
			m_md = false;
			base.OnMouseUp(e);
		}
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			if (m_CB == true)
			{
				m_CB = false;
				this.Invalidate();
			}
		}
		protected override void OnResize(EventArgs e)
		{
			Point[] points = WPolygon(-1);
			GraphicsPath path = new GraphicsPath();
			path.AddPolygon(points);
			this.Region = new Region(path);
			this.Refresh();
			base.OnResize(e);
		}
		// *****************************************************************
		public void SetIsFront(bool b)
		{
			if (base.Form == null) return;
			m_IsFront = b;
			base.Form.TopMost = b;
			if (m_IsFront == false)
			{
				formActive();
			}
			this.TopMost = true;
			this.Invalidate();
		}
		// *****************************************************************
		private void formActive()
		{
			if (base.Form == null) return;
			base.Form.ForegroundWindow();
		}
		// *****************************************************************
		
		/*
		private void button1_Click(object sender, EventArgs e)
		{
			if(base.Form != null)
			{
				((TR_Form)base.Form).Quit();
			}
			else
			{
				Application.Exit();
			}

		}
		*/
		// *****************************************************************
		private Point[] SR(Point[]p,Point c)
		{
			Point[] ret = new Point[p.Length];
			if(p.Length>0)
			{
				for(int i=0; i<p.Length;i++)
				{
					ret[i] = new Point(0, 0);
					if (p[i].X<=c.X)
					{
						ret[i].X = p[i].X+1;
					}
					else
					{
						ret[i].X = p[i].X - 1;
					}
					if (p[i].Y <= c.Y)
					{
						ret[i].Y = p[i].Y + 1;
					}
					else
					{
						ret[i].Y = p[i].Y - 1;
					}

				}
			}
			return ret;
		}
		// *****************************************************************
		private Point[] WPolygon(int h=0)
		{
			Point[] ps = new Point[]
				{
				new Point(h,h),
				new Point(this.Width-1-h,h),
				new Point(this.Width-1-h,this.Height-2-h),
				new Point(10+h,this.Height-2-h),
				new Point(10+h,this.Height-2-10-h),
				new Point(h,this.Height-2-10-h)
				};
			return ps;
		}
		// *****************************************************************
		protected override void OnPaint(PaintEventArgs e)
		{
			if ((Form == null) || (Colors == null)) return;
			Color MenuMoji = Colors.MenuMoji;
			Color MenuWaku = Colors.MenuWaku;
			Color MenuWaku1 = Colors.MenuWaku1;
			Color MenuWaku2 = Colors.MenuWaku2;
			Color MenuBack = Colors.MenuBack;
			Color MenuSdw = Colors.MenuSdw;
			Graphics g = e.Graphics;
			Pen p = new Pen(MenuWaku);
			SolidBrush sb = new SolidBrush(MenuWaku);
			try
			{
				//背景
				p.Width = 1;
				g.Clear(MenuBack);

				//左ボタン
				int w = 12;
				Rectangle r = new Rectangle(20, (this.Height - w) / 2, w, w);
				if (m_IsFront)
				{
					sb.Color = MenuMoji;
				}
				else
				{
					sb.Color = MenuWaku1;
				}
				g.FillRectangle(sb, r);
				p.Color = MenuWaku2;
				p.Width = 4;
				g.DrawRectangle(p, r);
				p.Color = MenuMoji;
				p.Width = 2;
				g.DrawRectangle(p, r);

				//文字を描く
				if(this.Text!="")
				{
					StringFormat sf = new StringFormat();
					sf.Alignment = StringAlignment.Center;
					sf.LineAlignment = StringAlignment.Center;
					sb.Color = MenuMoji;
					SizeF sz = g.MeasureString(this.Text, this.Font, 1000, sf);
					int ww = (int)sz.Width+5;
					if (ww > this.Width - 90) ww = this.Width - 90;
					r = new Rectangle(90, 0, ww, this.Height);
					g.DrawString(this.Text, this.Font, sb, r, sf);
					int www = this.Width - 90 - ww;
					if(www>5)
					{
						p.Color = MenuMoji;
						p.Width = 1;
						int cy = r.Top + r.Height / 2;
						int x0 = r.Right;
						int x1 = this.Width - 75;
						g.DrawLine(p, x0, cy, x1, cy);
						g.DrawLine(p, x0, cy-7, x0, cy+7);
						x0 = 80;
						x1 = r.Left;
						g.DrawLine(p, x0, cy, x1, cy);
						g.DrawLine(p, x1, cy - 7, x1, cy + 7);
					}

				}


				//外枠を描く
				Point c = new Point(Width / 2, Height / 2);
				p.Width = 1;
				p.Color = MenuWaku1;
				g.DrawPolygon(p, WPolygon(2));
				p.Color = MenuWaku2;
				g.DrawPolygon(p, WPolygon(3));
				g.DrawPolygon(p, WPolygon(4));

				// SDW
				r = new Rectangle(40, 0, 30, this.Height);
				sb.Color = MenuSdw;
				g.FillRectangle(sb, r);
				r = new Rectangle(this.Width - 60, 0, 20, this.Height);
				g.FillRectangle(sb, r);

				// 横の線
				p.Width = 6;
				p.Color = MenuWaku2;
				int xx = this.Width - 30;
				g.DrawLine(p, xx, 2, xx, this.Height-4);
				p.Width = 2;
				p.Color = MenuWaku;
				g.DrawLine(p, xx, 0, xx, this.Height);

				p.Color = MenuWaku;
				p.Width = 1;
				g.DrawPolygon(p, WPolygon(0));
				g.DrawPolygon(p, WPolygon(1));

				int k = 12;
				r = new Rectangle(this.Width - 20, (this.Height - k) / 2, k, k);
				if (m_CB)
				{
					sb.Color = MenuMoji;
				}
				else
				{
					sb.Color = MenuWaku2;
				}
				g.FillRectangle(sb,r);

			}
			finally
			{
				p.Dispose();
			}
		}
	}
}
