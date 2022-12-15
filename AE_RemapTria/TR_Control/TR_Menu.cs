using BRY;
using PdfSharpCore.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace AE_RemapTria
{
	// ***********************************************************************************
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
				return new Rectangle(PosLeft, 0, Width, Height);
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
				if (m_funcItem != null)
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
					if (k != Keys.None)
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
	// ***********************************************************************************
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
        public TR_Menu() //: base()
		{
            m_FontSize = 9;
            m_FontIndex = 5;
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
        public override void SetTRForm(TR_Form fm,bool IsI = true)
        {
            base.SetTRForm(fm,false);
            if (m_form != null)
            {
                ClearMenu();
                m_form.MakeMenu();
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
        public void AddTopMenu(string cap)
        {
            Array.Resize(ref m_TopMenuItems, m_TopMenuItems.Length + 1);
            Array.Resize(ref m_SubMenus, m_SubMenus.Length + 1);

            int w = 50;
            if ((Sizes != null)&&(m_font!=null)&&(m_form!=null))
            {
				Bitmap bmp = new Bitmap(1000, MenuHeight);
                Graphics g = Graphics.FromImage(bmp);
                SizeF sz = g.MeasureString(cap, m_font, 1000, m_form.StringFormat);
                Debug.WriteLine($"Menu:{sz.Width},{sz.Height}");
                w = (int)(sz.Width+0.5);
				w += 10;
				bmp.Dispose();
            }
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
            if (Grid == null) return;
			if (Funcs == null) return;

			FuncItem? ft = Funcs.FindFunc(EngN);
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
            int ret = 35;
            if (m_TopMenuItems.Length > 0)
            {
                int xp = 35;
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
			Debug.WriteLine("Menu");
			if ((m_form == null )||(Colors==null)) return;
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

                Rectangle r = new Rectangle(24-5, 4, 12, 12);
                Fill(g, sb, r);
                ////
                for (int i = 0; i < m_TopMenuItems.Length; i++)
                {
                    r = new Rectangle(
                        m_TopMenuItems[i].PosLeft,
                        0,
                        m_TopMenuItems[i].Width,
                        MenuHeight);

					if (m_mm==i)
                    {
						sb.Color = Colors.MenuBackSelected;
						Fill(g, sb, r);
                    }
					sb.Color = m_form.Colors.MenuMoji;

					m_form.StringFormat.Alignment = StringAlignment.Center;
                    DrawStr(g, m_TopMenuItems[i].Caption, sb, r);
				}


				Pen p = new Pen(m_form.Colors.MenuWaku);
                p.Width = 2;
                Point[] pp = new Point[]
                {
					new Point(11,1),
					new Point(11,11),
					new Point(1,11),
                    new Point(1,Height-2),
                    new Point(Width-2,Height-2),
                    new Point(Width-2,11),
					new Point(Width-13,11),
					new Point(Width-13,1),
				};
                g.DrawLines(p, pp); 
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
			
        }
        private int m_mm = -1;
        // **********************************************************************
        private int mmPos(int x)
        {
            int ret = -1;
            for (int i = 0; i < m_TopMenuItems.Length; i++)
            {
                if (x > m_TopMenuItems[i].PosLeft && x < m_TopMenuItems[i].PosLeft + m_TopMenuItems[i].Width)
                {
                    ret = i;
                    break;
                }
            }

            return ret;
        }

		// **********************************************************************
        public virtual void OnMenuDown(int idx)
        {

			if (idx >= 0)
			{
                if (m_form != null)
                {
                    m_form.MenuPlates[idx].QShow();
                }
			}
		}
		// **********************************************************************
        public virtual void OnMenuEnter(int idx)
		{
            ChkOffScr();
            Refresh();
		}
		// **********************************************************************
		public virtual void OnMenuLeave()
		{
			ChkOffScr();
			Invalidate();
		}
		// **********************************************************************
		public override bool ChkMouseDown(MouseEventArgs e)
        {
			bool ret = false;
			ret = base.ChkMouseDown(e);
            if (m_inMouse)
            {
                m_mm = mmPos(m_MDownPoint.X);
				if (m_mm >=0)
                {
                    m_MDown = true;
					OnMenuDown(m_mm);
                    ret = true;
                }
                else
                {
                    ret = false;
                }
            }
            return ret;
        }
        // **********************************************************************
        private int m_MoveMM = -1;
        public override bool ChkMouseMove(MouseEventArgs e)
        {
            bool ret = false;
            bool inM = m_inMouse;
			base.ChkMouseMove(e);

			if (m_inMouse)
            {
				m_mm = mmPos(m_MMovePoint.X);
				if (m_mm!= m_MoveMM)
                {
					OnMenuEnter(m_mm);
					m_MoveMM = m_mm;

				}
            }
            else
            {
				m_mm = -1;
				m_MoveMM = -1;
				if (inM == true)
                {
				    m_MDown=false;
					OnMenuLeave();
                }
			}
			return ret;
        }
        public override bool ChkMouseUp(MouseEventArgs e)
        {
            bool ret = false;
			base.ChkMouseUp(e);
			if (m_MDown)
            {
                m_MDown = false;
				m_mm = -1;
                ChkOffScr();
				Invalidate();
			}
			return ret;
        }
		public override bool ChkMouseLeave(EventArgs e)
		{
			m_MDown = false;
			m_mm = -1;
			return base.ChkMouseLeave(e);
		}
	}
}
