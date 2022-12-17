//using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AE_RemapTria
{
    public partial class TR_ListBox : TR_DialogControl
    {
        public event EventHandler? SelectedIndexChanged;
        protected virtual void OnSelectedIndexChanged(EventArgs e)
        {
            if (SelectedIndexChanged != null)
            {
                SelectedIndexChanged(this, e);
            }
        }

        private ListBox m_list = new ListBox();
        private Color m_SelectedColor = Color.FromArgb(50, 50, 100);
        [Category("_AE_Remap")]
        public Color SelectedColor
        {
            get { return m_SelectedColor; }
            set { m_SelectedColor = value; this.Invalidate(); }
        }
        private T_Funcs t_Funcs = new T_Funcs();

        [Category("_AE_Remap")]
        public new int MyFontIndex
        {
            get { return base.m_MyFontIndex; }
            set
            {
                base.MyFontIndex = value;
                if (m_MyFonts != null)
                {
                    m_list.Font = m_MyFonts.MyFont(m_MyFontIndex, m_list.Font.Size, m_list.Font.Style);
					this.Invalidate();
				}
            }
        }
        public new float MyFontSize
        {
            get { return base.MyFontSize; }
            set
            {
                base.MyFontSize = value;
                if (m_MyFonts != null)
                {
                    m_list.Font = m_MyFonts.MyFont(m_MyFontIndex, m_list.Font.Size, m_list.Font.Style);
                    this.Invalidate();
                }

            }
        }
        public int SelectedIndex
        {
            get { return m_list.SelectedIndex; }
            set { m_list.SelectedIndex = value; this.Invalidate(); }
        }
        public ListBox.ObjectCollection Items
        {
            get { return m_list.Items; }
        }
        public int ItemHeight
        {
            get { return m_list.ItemHeight; }
            set { m_list.ItemHeight = value; }
        }
        public bool FormattingEnabled
        {
            get { return m_list.FormattingEnabled; }
            set { m_list.FormattingEnabled = value; }
        }
        public DrawMode DrawMode
        {
            get { return m_list.DrawMode; }
            set { m_list.DrawMode = value; }
        }
        public BorderStyle BorderStyle
        {
            get { return m_list.BorderStyle; }
            set { m_list.BorderStyle = value; }
        }
        public TR_ListBox()
        {
			MyFontSize = 9;
			m_list.BorderStyle = BorderStyle.None;
            this.Margin = new Padding(0,0,0,0);
            this.Size = new Size(100,100);
            m_list.DrawMode = DrawMode.OwnerDrawFixed;
            m_list.DrawItem += M_list_DrawItem;
            m_list.ForeColor = Color.FromArgb(120, 220, 250);
            m_list.BackColor = Color.FromArgb(10, 10, 15);
            m_list.SelectedIndexChanged += M_list_SelectedIndexChanged;
            this.Controls.Add(m_list);
			InitializeComponent();
            this.BackColor = Color.Transparent;
			m_list.Location = new Point(0, 0);
			m_list.Size = this.Size;
		}
		protected override void InitLayout()
		{
			base.InitLayout();
			this.Margin = new Padding(0, 0, 0, 0);
			m_list.BorderStyle = BorderStyle.None;
			m_list.Location = new Point(0, 0);
			m_list.Size = this.Size;
		}

		private void M_list_SelectedIndexChanged(object? sender, EventArgs e)
        {
            OnSelectedIndexChanged(e);
        }

        private void M_list_DrawItem(object? sender, DrawItemEventArgs e)
        {
            if (m_list == null) return;
            SolidBrush sb = new SolidBrush(this.BackColor);

            try
            {
                e.DrawBackground();

                if (e.Index > -1 && m_list.Items.Count > 0)
                {

                    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                    {
                        sb.Color = m_SelectedColor;
                        e.Graphics.FillRectangle(sb, e.Bounds);
                    }

                    //文字列の取得
                    string txt = "";
                    if (m_list.Items != null)
                    {
                        txt = m_list.Items[e.Index].ToString();
                    }
                    //文字列の描画
                    sb.Color = this.ForeColor;
                    e.Graphics.DrawString(txt, m_list.Font, sb, e.Bounds);

                }

            }
            finally
            {
                sb.Dispose();
            }
            //背景を描画する
        }

        [Category("_AE_Remap")]
        public string[] Names
        {
            get
            {
                string[] ret = new string[m_list.Items.Count];
                if (m_list.Items.Count > 0)
                {
                    for (int i = 0; i < m_list.Items.Count; i++)
                    {
                        string s = m_list.Items[i].ToString();
                        if (s != null) s = "";
                        ret[i] = s;
                    }
                }
                return ret;
            }
            set
            {
                m_list.Items.Clear();
                m_list.Items.AddRange(value);
            }
        }
        protected override void OnResize(EventArgs e)
        {
			m_list.Location = new Point(0, 0);
			m_list.Size = this.Size;
			base.OnResize(e);
        }
		protected override void OnPaint(PaintEventArgs pe)
		{
			//base.OnPaint(pe);
            SolidBrush sb = new SolidBrush(Color.Transparent);
            Graphics g = pe.Graphics;
            g.FillRectangle(sb,this.ClientRectangle);
            sb.Dispose();
		}
	}
}
