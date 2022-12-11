using PdfSharpCore.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace AE_RemapTria
{
    public class TR_Menu : TR_Control
    {
        private T_TopMenuItem[] m_TopMenuItems = new T_TopMenuItem[0];
        private T_SubMenuItem[][] m_SubMenus = new T_SubMenuItem[0][];

        private bool m_IsActive = true;
        public bool IsActive
        {
            get { return m_IsActive; }
            set { m_IsActive = value; ; }
        }
        public Rectangle CaptionRect(int idx)
        {
            if (idx < 0 || idx >= m_TopMenuItems.Length) return new Rectangle(0, 0, 0, 0);
            return m_TopMenuItems[idx].Rect;
        }
        public string Caption(int idx)
        {
            if (idx < 0 || idx >= m_TopMenuItems.Length) return "";
            return m_TopMenuItems[idx].Caption;
        }
        public T_SubMenuItem[] SubMenus(int idx)
        {
            if (idx < 0 || idx >= m_TopMenuItems.Length) return new T_SubMenuItem[0];
            return m_SubMenus[idx];
        }
        // *************************************************************************************************
        public TR_Menu()
        {
            m_FontSize = 7;
            m_Size = new Size(200, MenuHeight);
        }

        private void M_form_Deactivate(object? sender, EventArgs e)
        {
            m_IsActive = false;
            Invalidate();
        }

        private void M_form_Activated(object? sender, EventArgs e)
        {
            m_IsActive = true;
            Invalidate();
        }

        // *************************************************************************************************
        public override void SetTRForm(TR_Form fm)
        {
            m_form = fm;
            if (m_form != null)
            {
                m_font = m_form.MyFont(m_FontIndex, m_FontSize,m_form.FontStyle);
                ClearMenu();
                m_form.Grid.MakeMenu();
                MenuWidthAll();
                SetLocSize();
            }
            Invalidate();
        }

        // *************************************************************************************************
        public override void SetLocSize()
        {
            m_Location = new Point(0, 0);
            if (m_form != null)
            {
                Size sz = new Size(m_form.Width, MenuHeight);

				if (m_Size!=sz)
                {
					m_Size = sz;
                    ChkOffScr();
					Invalidate();
				}
			}
        }
        // *************************************************************************************************
        // ****************************************************************
        public void AddTopMenu(string cap, int w)
        {
            Array.Resize(ref m_TopMenuItems, m_TopMenuItems.Length + 1);
            Array.Resize(ref m_SubMenus, m_SubMenus.Length + 1);

            int cnt = m_TopMenuItems.Length - 1;
            m_TopMenuItems[cnt] = new T_TopMenuItem(cap, w, MenuHeight);
            MenuWidthAll();
        }
        // ****************************************************************
        public void AddSubMenu(int idx, FuncItem fi)
        {
            if (idx < 0 && idx >= m_TopMenuItems.Length) return;
            Array.Resize(ref m_SubMenus[idx], m_SubMenus[idx].Length + 1);
            int cnt = m_SubMenus[idx].Length - 1;
            m_SubMenus[idx][cnt] = new T_SubMenuItem(fi, cnt);
            Invalidate();
        }
        // ****************************************************************
        public void AddSubMenu(int idx, string EngN)
        {
            if (m_form == null) return;
            if (m_form.Grid == null) return;

            FuncItem? ft = m_form.Grid.Funcs.FindFunc(EngN);
            if (ft == null) return;
            if (m_SubMenus[idx] == null) m_SubMenus[idx] = new T_SubMenuItem[0];
            Array.Resize(ref m_SubMenus[idx], m_SubMenus[idx].Length + 1);
            int cnt = m_SubMenus[idx].Length - 1;
            m_SubMenus[idx][cnt] = new T_SubMenuItem(ft, cnt);
            Invalidate();
        }
        // ****************************************************************
        public void ClearMenu()
        {
            m_TopMenuItems = new T_TopMenuItem[0];
            m_SubMenus = new T_SubMenuItem[0][];
            Invalidate();
        }
        // ****************************************************************
        public void AddSubMenuSepa(int idx)
        {
            if (idx >= 0 && idx < m_SubMenus.Length)
            {
                if (m_SubMenus[idx] == null) m_SubMenus[idx] = new T_SubMenuItem[0];
                if (m_SubMenus[idx] == null) m_SubMenus[idx] = new T_SubMenuItem[0];
                Array.Resize(ref m_SubMenus[idx], m_SubMenus[idx].Length + 1);
                m_SubMenus[idx][m_SubMenus[idx].Length - 1] = new T_SubMenuItem(null, -1);
            }
            Invalidate();
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
            Invalidate();
            return ret;
        }
        // ****************************************************************
        // ******************************************************************


        // ****************************************************************
        public override void Draw(Graphics g)
        {
            if (m_form == null ) return;
            SolidBrush sb = new SolidBrush(Color.Transparent);
            try
            {
                if (m_IsActive)
                {
                    sb.Color = m_form.Colors.MenuBack;
                }
                else
                {
                    sb.Color = m_form.Colors.MenuBackNoActive;
                }
                Fill(g, sb);

                // 四角
                if (m_IsActive)
                {
                    sb.Color = m_form.Colors.MenuMoji;
                }
                else
                {
                    sb.Color = m_form.Colors.MenuMojiNoActive;
                }

                Rectangle r = new Rectangle(4, 4, 12, 12);
                Fill(g, sb, r);
                for (int i = 0; i < m_TopMenuItems.Length; i++)
                {
                    r = new Rectangle(
                        m_TopMenuItems[i].PosLeft,
                        0,
                        m_TopMenuItems[i].Width,
                        MenuHeight);

                    if(m_mm==i)
                    {
                        sb.Color = m_form.Colors.Selection;
                        Fill(g, sb, r);
                        sb.Color = m_form.Colors.Moji;
                    }

                    m_form.StringFormat.Alignment = StringAlignment.Center;
                    DrawStr(g, m_TopMenuItems[i].Caption, sb, r);
                }
                if (m_form.Text != "")
                {
                    int x = m_TopMenuItems[m_TopMenuItems.Length - 1].PosLeft + m_TopMenuItems[m_TopMenuItems.Length - 1].Width;
                    int w = Width - x;
                    r = new Rectangle(x, 0, w, m_Size.Height);
                    m_form.StringFormat.Alignment = StringAlignment.Near;
                    string s = " ]";
                    if (m_form.Grid.IsModif) s = "* ]";
                    DrawStr(g, "[ " + m_form.Text + s, sb, r);
                }
				Pen p = new Pen(m_form.Colors.Line);
				DrawFrame(g,p, new Rectangle(0,0,m_Size.Width,m_Size.Height));
            }
            finally
            {
                sb.Dispose();
            }

        }
		// ****************************************************************
		public Point GLoc
        {
            get
            {
                if (m_form == null) return m_Location;
                return new Point(
					m_form.Left + m_Location.X,
                    m_form.Top + m_Location.Y);
            }
        }
        // ****************************************************************
		private void MakeSubMenu()
        {
			if (m_mm >= 0)
			{
				if (m_SubMenus[m_mm].Length > 0)
				{
					TR_MenuPlate dlg = new TR_MenuPlate();
					dlg.SetSubMenuItems(m_mm, this);
					if (dlg.ShowDialog() == DialogResult.OK)
					{
						m_SubMenus[dlg.Index][dlg.SubIndex].Exec();
					}
					dlg.Dispose();
				}
			}
        }
        private int m_mm = -1;
        // **********************************************************************
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
            if (m_MDown && xx >= 0)
            {
                m_mm = xx;
                ret = true;
            }

            return ret;
        }

		// **********************************************************************
		public override bool ChkMouseDown(MouseEventArgs e)
        {
			bool ret = false;
			base.ChkMouseDown(e);
            if (m_inMouse)
            {
                if (mmPos(m_MDownPoint.X))
                {
					MakeSubMenu();
                    ret = true;
                }
            }
            return ret;
        }
		// **********************************************************************
		public override bool ChkMouseMove(MouseEventArgs e)
        {
            bool ret = false;
			base.ChkMouseMove(e);
            if(m_inMouse)
            {
                int oldmm = m_mm;
				if (mmPos(m_MMovePoint.X))
				{
                    if(m_mm!= oldmm)
                    {
                        ChkOffScr();
                    }
				}
			}
			return ret;
        }
        public override bool ChkMouseUp(MouseEventArgs e)
        {
            bool ret = false;
			base.ChkMouseUp(e);
			if (m_mm >= 0)
            {
                m_mm = -1;
				ChkOffScr();
			}
            return ret;
        }
    }
}
