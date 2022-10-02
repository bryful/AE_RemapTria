using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AE_RemapTria
{
#pragma warning disable CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。
	public class T_MenuItem
	{
		public string Caption { get; set; }
		public int Width { get; set; }
		public int PosLeft { get; set; }
		public T_MenuItem(String cap,int w=100)
		{
			Caption = cap;
			Width = w;
		}
	}
	public class T_SubMenuItem
	{
		private int m_Id = -1;
		public int Id { get { return m_Id; } }
		public string Caption { get; set; }
		private FuncType m_func;

		public T_SubMenuItem(FuncType fnc, String cap,int id)
		{
			m_func = fnc;
			Caption = cap;
			m_Id = id;
		}
		public bool Exec()
		{
			return m_func();
		}
	}
#pragma warning restore CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。

	public class T_Menu :T_BaseControl
	{
		public const int MyHeight = 20;
		private T_Grid? m_grid = null;
		private T_MenuItem[] m_MenuItems = new T_MenuItem[0];
		private ContextMenuStrip[] m_Submenu = new ContextMenuStrip[0];
		private T_SubMenuItem[] m_SubMenuItems = new T_SubMenuItem[0];
		//
		// ****************************************************************
		public T_Menu()
		{
			this.Size = new Size(100, MyHeight);
			ChkGrid();
		}
		// ****************************************************************
		public void AddMenu(string cap,int w)
		{
			Array.Resize(ref m_MenuItems, m_MenuItems.Length + 1);
			Array.Resize(ref m_Submenu, m_Submenu.Length + 1);
			int cnt = m_MenuItems.Length - 1;
			m_MenuItems[cnt] = new T_MenuItem(cap, w);
			m_Submenu[cnt] = new ContextMenuStrip();
			m_Submenu[cnt].BackColor = Color.FromArgb(25,25,50);
			m_Submenu[cnt].ForeColor = Color.FromArgb(200, 200, 250);
			this.Size = new Size(MenuWidthAll(), MyHeight);
		}
		// ****************************************************************
		public void AddSubMenu(int idx, string cap, FuncType ft)
		{
			Array.Resize(ref m_SubMenuItems, m_SubMenuItems.Length + 1);
			int cnt = m_SubMenuItems.Length - 1;
			m_SubMenuItems[cnt ] = new T_SubMenuItem(ft, cap, cnt);
			ToolStripMenuItem a = new ToolStripMenuItem(cap);
			a.Click += A_Click;
			a.Tag = cnt;
			m_Submenu[idx].Items.Add(a);

		}
		// ****************************************************************
		private void A_Click(object? sender, EventArgs e)
		{
			ToolStripMenuItem m = (ToolStripMenuItem)sender;
			if(m!=null)
			{
				int idx = (int)(m.Tag);
				if((idx>=0)&&(idx< m_SubMenuItems.Length))
				{
					m_SubMenuItems[idx].Exec();
				}
			}
		}
		// ****************************************************************
		public void Clear()
		{
			m_MenuItems = new T_MenuItem[0];
			m_Submenu = new ContextMenuStrip[0];
			m_SubMenuItems = new T_SubMenuItem[0];

		}
		// ****************************************************************
		private int MenuWidthAll()
		{
			int ret = 20;
			if(m_MenuItems.Length>0)
			{
				int xp = 20;
				for(int i=0; i< m_MenuItems.Length; i++)
				{
					ret += m_MenuItems[i].Width;
					m_MenuItems[i].PosLeft = xp;
					xp += m_MenuItems[i].Width;
				}
			}
			return ret;
		}
		// ****************************************************************
		protected override void InitLayout()
		{
			base.InitLayout();
			ChkGrid();
			this.Size = new Size(MenuWidthAll(), MyHeight);

		}
		// ****************************************************************
		public T_Grid? Grid
		{
			get { return m_grid; }
			set
			{
				m_grid = value;

				ChkGrid();
			}
		}
		// ****************************************************************
		private void ChkGrid()
		{
			if (m_grid == null) return;
			Clear();
			m_grid.SetT_Menu(this);
			m_grid.MakeMenu();
			Alignment = StringAlignment.Center;
			LineAlignment = StringAlignment.Center;
			MyFontSize = 8;
			SetLoc();
			m_grid.LocationChanged += M_grid_LocationChanged;

		}


		private void SetLoc()
		{
			if (m_grid == null) return;
			Point p = new Point(
				m_grid.Left - (m_grid.Sizes.FrameWidth + m_grid.Sizes.InterWidth),
				m_grid.Top - (m_grid.Sizes.CaptionHeight + m_grid.Sizes.CaptionHeight2 + m_grid.Sizes.InterHeight + this.Height)
				);
			if (this.Location != p) this.Location = p;
		}
		// ******************************************************************
		protected override void OnLocationChanged(EventArgs e)
		{
			//base.OnLocationChanged(e);
			SetLoc();
		}
		// ******************************************************************
		private void M_grid_LocationChanged(object? sender, EventArgs e)
		{
			SetLoc();
		}
		// ******************************************************************
		private int m_mm = -1;
		private bool mmPos(int x)
		{
			bool ret = false;
			int xx = -1;
			for(int i=0;i<m_MenuItems.Length;i++)
			{
				if ((x>m_MenuItems[i].PosLeft)&& (x<m_MenuItems[i].PosLeft+ m_MenuItems[i].Width))
				{
					xx = i;
					break;
				}
			}
			if((m_mm!=xx)&&(xx>=0))
			{
				m_mm = xx;
				ret = true;
			}

			return ret;
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (mmPos(e.X))
			{


				this.Invalidate();
			}
			base.OnMouseMove(e);
		}
		protected override void OnMouseLeave(EventArgs e)
		{
			if(m_mm>=0)
			{
				m_mm = -1;
				this.Invalidate();
			}
			base.OnMouseLeave(e);
		}
		// ****************************************************************
		protected override void OnPaint(PaintEventArgs pe)
		{
			Graphics g = pe.Graphics;
			SolidBrush sb = new SolidBrush(Color.Transparent);
			try
			{
				Fill(g, sb);
				if(m_grid != null)
				{
					// 四角
					sb.Color = m_grid.Colors.Moji;
					Rectangle r = new Rectangle(4, 4, 12, 12);
					Fill(g, sb, r);
					//項目
					for (int i=0; i < m_MenuItems.Length; i++)
					{
						r = new Rectangle(m_MenuItems[i].PosLeft, 0, m_MenuItems[i].Width, MyHeight);
						if(m_mm==i)
						{
							sb.Color = m_grid.Colors.Selection;
							Fill(g, sb, r);
							sb.Color = m_grid.Colors.Moji;
						}
						DrawStr(g, m_MenuItems[i].Caption, sb, r);
					}
				}

			}
			finally
			{
				sb.Dispose();
			}

			//base.OnPaint(pe);
		}
		// ****************************************************************
		private void MakeSubMenu()
		{
			if(m_mm>=0)
			{
				m_Submenu[m_mm].Show(this, new Point(m_MenuItems[m_mm].PosLeft,20));
			}
		}
		protected override void OnMouseClick(MouseEventArgs e)
		{
			base.OnMouseClick(e);
			MakeSubMenu();
		}
	}
}
