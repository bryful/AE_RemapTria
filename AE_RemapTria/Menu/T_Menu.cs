using BRY;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AE_RemapTria
{
#pragma warning disable CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。
    public class T_TopMenuItem
    {
        public string Caption { get; set; }
        public int Width { get; set; }
		public int Height { get; set; }
		public int PosLeft { get; set; }
        public int PosRight { get { return PosLeft + Width; } }
        public Rectangle Rect
        {
            get
            {
                return new Rectangle(PosLeft,0,Width,Height);
            }
        }
        public T_TopMenuItem(string cap, int w = 100, int h = 20)
        {
            Caption = cap;
            Width = w;
            Height = h;

	}
    }
    public class T_SubMenuItem
    {
        private int m_Id = -1;
        public int Id { get { return m_Id; } }
        public string EngName
        {
            get
            {
                if (m_funcItem != null)
                {
                    return m_funcItem.EngName;
                }
                else
                {
                    return "";
                }

            }
        }
        public string Caption
        {
            get
            {
                if (m_funcItem != null)
                {
                    return m_funcItem.Caption;
                }
                else
                {
                    return "";
                }

            }
        }
        public Keys Key
        {
            get 
            {
                if(m_funcItem != null)
                {
					return m_funcItem.KeysFirst;
                }
                else
                {
                    return Keys.None;
                }
			}
        }
        public string Shrtcut
        {
			get
			{
                string ret = "";
				if (m_funcItem != null)
				{
                    Keys k = m_funcItem.KeysFirst;
                    if(k != Keys.None)
                    {
                        ret = T_G.KeyInfo(k);
					}
				}
                return ret;
			}

		}
		private FuncItem? m_funcItem;
        public T_SubMenuItem(FuncItem? fnc, int id)
        {
            m_funcItem = fnc;
            m_Id = id;
        }
        public bool Exec()
        {
            if (m_funcItem.Func != null)
            {
                return m_funcItem.Func();
            }
            else
            {
                return false;
            }
        }
    }
#pragma warning restore CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。

    public class T_Menu : T_BaseControl
    {
        public const int MyHeight = 20;
        private T_Grid? m_grid = null;
        private T_Funcs? m_Funcs = null;
        private T_Form m_Form = null;
        public T_Form Form
        {
            get { return m_Form; }
            set
            {
                m_Form = value;
            }
        }
             
        private T_TopMenuItem[] m_TopMenuItems = new T_TopMenuItem[0];
        //private ContextMenuStrip[] m_Submenu = new ContextMenuStrip[0];
        //private T_SubMenuItem[] m_SubMenuItems = new T_SubMenuItem[0];
		private T_SubMenuItem[][] m_SubMenus = new T_SubMenuItem[0][];

		private bool m_IsActive = true;
        [Category("_AE_Remap")]
        public bool IsActive
        {
            get { return m_IsActive; }
            set { m_IsActive = value; Invalidate(); }
        }
        public Rectangle CaptionRect(int idx)
        {
            if ((idx < 0) || (idx >= m_TopMenuItems.Length)) return new Rectangle(0, 0, 0, 0);
            return m_TopMenuItems[idx].Rect;
        }
		public string Caption(int idx)
		{
			if ((idx < 0) || (idx >= m_TopMenuItems.Length)) return "";
			return m_TopMenuItems[idx].Caption;
		}
		public Point GLoc
        {
            get
            {
				if (m_Form==null) return this.Location;

                return new Point(m_Form.Left+this.Left,m_Form.Top+this.Top);
			}
		}
		public T_SubMenuItem[] SubMenus(int idx)
        {
			if ((idx < 0) || (idx >= m_TopMenuItems.Length)) return new T_SubMenuItem[0];
            return m_SubMenus[idx];
		}
		//
		// ****************************************************************
		public T_Menu()
        {
            Size = new Size(1600, MyHeight);
            ChkGrid();
        }
        // ****************************************************************
        public void AddTopMenu(string cap, int w)
        {
            Array.Resize(ref m_TopMenuItems, m_TopMenuItems.Length + 1);
            Array.Resize(ref m_SubMenus, m_SubMenus.Length + 1);

            int cnt = m_TopMenuItems.Length - 1;
            m_TopMenuItems[cnt] = new T_TopMenuItem(cap, w,MyHeight);
            //m_Submenu[cnt] = new ContextMenuStrip();
            //m_Submenu[cnt].BackColor = Color.FromArgb(25, 25, 50);
            //m_Submenu[cnt].ForeColor = Color.FromArgb(200, 200, 250);
            MenuWidthAll();
            //this.Size = new Size(MenuWidthAll(), MyHeight);
        }
        // ****************************************************************
        public void AddSubMenu(int idx, FuncItem fi)
        {
            if ((idx < 0) && (idx >= m_TopMenuItems.Length)) return;
            Array.Resize(ref m_SubMenus[idx], m_SubMenus[idx].Length + 1);
            int cnt = m_SubMenus[idx].Length - 1;
            m_SubMenus[idx][cnt] = new T_SubMenuItem(fi, cnt);
            /*
            ToolStripMenuItem a = new ToolStripMenuItem(fi.Caption);
            a.Click += A_Click;
            a.Tag = cnt;
            m_Submenu[idx].Items.Add(a);
            */
        }
        // ****************************************************************
        public void AddSubMenu(int idx, string EngN)
        {
            if (m_Funcs == null) return;
            FuncItem? ft = m_Funcs.FindFunc(EngN);
            if (ft == null) return;
            if (m_SubMenus[idx] == null) m_SubMenus[idx] = new T_SubMenuItem[0];
			Array.Resize(ref m_SubMenus[idx], m_SubMenus[idx].Length + 1);
			int cnt = m_SubMenus[idx].Length - 1;
			m_SubMenus[idx][cnt] = new T_SubMenuItem(ft, cnt);
            /*
            ToolStripMenuItem a = new ToolStripMenuItem(ft.Caption);
            a.ShortcutKeys = ft.KeyArray[0];
            a.Click += A_Click;
            a.Tag = cnt;
            m_Submenu[idx].Items.Add(a);
            */
        }
        // ****************************************************************
        public void ClearMenu()
        {
            m_TopMenuItems = new T_TopMenuItem[0];
			m_SubMenus = new T_SubMenuItem[0][];
        }
        // ****************************************************************
        /*
        public void UpdateMenu()
        {
            if (m_SubMenuItems.Length > 0)
            {
                for (int i = 0; i < m_Submenu.Length; i++)
                {
                    if (m_Submenu[i].Items.Count > 0)
                    {
                        for (int j = 0; j < m_Submenu[i].Items.Count; j++)
                        {
                            int idx = (int)m_Submenu[i].Items[j].Tag;
                            if (idx >= 0)
                            {

                                m_Submenu[i].Items[j].Text = m_SubMenuItems[idx].Caption;
                                ((ToolStripMenuItem)m_Submenu[i].Items[j]).ShortcutKeys = m_SubMenuItems[idx].Key;
                            }
                        }

                    }
                }
            }
        }
        */
        // ****************************************************************
        public void AddSubMenuSepa(int idx)
        {
            if((idx>=0)&&(idx< m_SubMenus.Length))
            {
                if (m_SubMenus[idx] == null) m_SubMenus[idx] = new T_SubMenuItem[0];
                if(m_SubMenus[idx] ==null) m_SubMenus[idx] = new T_SubMenuItem[0];
				Array.Resize(ref m_SubMenus[idx], m_SubMenus[idx].Length + 1);
				m_SubMenus[idx][m_SubMenus[idx].Length - 1] = new T_SubMenuItem(null, -1);
			}
		}

        // ****************************************************************
        /*
        private void A_Click(object? sender, EventArgs e)
        {
#pragma warning disable CS8600 // Null リテラルまたは Null の可能性がある値を Null 非許容型に変換しています。
            ToolStripMenuItem m = (ToolStripMenuItem)sender;
#pragma warning restore CS8600 // Null リテラルまたは Null の可能性がある値を Null 非許容型に変換しています。
            if (m != null)
            {
                int idx = (int)m.Tag;
                if (idx >= 0 && idx < m_SubMenuItems.Length)
                {
                    m_SubMenuItems[idx].Exec();
                }
            }
        }
        */
        // ****************************************************************
        public void Clear()
        {
            m_TopMenuItems = new T_TopMenuItem[0];
            m_SubMenus = new T_SubMenuItem[0][];

        }
        // ****************************************************************
        private int MenuWidthAll()
        {
            int ret = 20;
            if (m_TopMenuItems.Length > 0)
            {
                int xp = 20;
                for (int i = 0; i < m_TopMenuItems.Length; i++)
                {
                    ret += m_TopMenuItems[i].Width;
                    m_TopMenuItems[i].PosLeft = xp;
                    xp += m_TopMenuItems[i].Width;
                }
            }
            return ret;
        }
        // ****************************************************************
        protected override void InitLayout()
        {
            base.InitLayout();
            ChkGrid();
            MenuWidthAll();
            //this.Size = new Size(MenuWidthAll(), MyHeight);

        }
        // ****************************************************************
        [Category("_AE_Remap")]
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
			m_Funcs = m_grid.Funcs;
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
                m_grid.Top - (m_grid.Sizes.CaptionHeight + m_grid.Sizes.CaptionHeight2 + m_grid.Sizes.InterHeight + Height)
                );
            if (Location != p) Location = p;
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
            for (int i = 0; i < m_TopMenuItems.Length; i++)
            {
                if (x > m_TopMenuItems[i].PosLeft && x < m_TopMenuItems[i].PosLeft + m_TopMenuItems[i].Width)
                {
                    xx = i;
                    break;
                }
            }
            if (m_mm != xx && xx >= 0)
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


                Invalidate();
            }
            base.OnMouseMove(e);
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            if (m_mm >= 0)
            {
                m_mm = -1;
                Invalidate();
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
                if (m_grid != null)
                {
                    if (m_IsActive)
                    {
                        sb.Color = m_grid.Colors.MenuBack;
                    }
                    else
                    {
                        sb.Color = m_grid.Colors.MenuBackNoActive;
                    }
                    Fill(g, sb);
                    // 四角
                    if (m_IsActive)
                    {
                        sb.Color = m_grid.Colors.MenuMoji;
                    }
                    else
                    {
                        sb.Color = m_grid.Colors.MenuMojiNoActive;
                    }
                    Rectangle r = new Rectangle(4, 4, 12, 12);
                    Fill(g, sb, r);
                    //項目
                    for (int i = 0; i < m_TopMenuItems.Length; i++)
                    {
                        r = new Rectangle(m_TopMenuItems[i].PosLeft, 0, m_TopMenuItems[i].Width, MyHeight);
                        /*
						if(m_mm==i)
						{
							sb.Color = m_grid.Colors.Selection;
							Fill(g, sb, r);
							sb.Color = m_grid.Colors.Moji;
						}
						*/
                        Alignment = StringAlignment.Center;
                        DrawStr(g, m_TopMenuItems[i].Caption, sb, r);
                    }
                    if (Text != "")
                    {
                        int x = m_TopMenuItems[m_TopMenuItems.Length - 1].PosLeft + m_TopMenuItems[m_TopMenuItems.Length - 1].Width;
                        int w = Width - x;
                        r = new Rectangle(x, 0, w, Height);
                        Alignment = StringAlignment.Near;
                        string s = " ]";
                        if (m_grid != null) if (m_grid.IsModif) s = "* ]";

                        DrawStr(g, "[ " + Text + s, sb, r);
                    }
                }
                else
                {
                    Fill(g, sb);
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
            if (m_mm >= 0)
            {
                if (m_SubMenus[m_mm].Length>0)
                {
					T_MenuPlate dlg = new T_MenuPlate();
					dlg.SetSubMenuItems(m_mm,this);
                    if( dlg.ShowDialog()== DialogResult.OK)
                    {
						m_SubMenus[dlg.Index][dlg.SubIndex].Exec();
					}
                    dlg.Dispose();
				}
				//m_Submenu[m_mm].Show(this, new Point(m_TopMenuItems[m_mm].PosLeft, 20));
			}
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
			MakeSubMenu();
		}
		protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
        }
    }
}
