using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE_RemapTria
{
    public class T_Button : T_BaseControl
    {
		#region Event
		public event EventHandler? CheckedChanged=null;
		protected virtual void OnCheckedChanged(EventArgs e)
		{
			if (CheckedChanged != null)
			{
				CheckedChanged(this, e);
			}
		}
		#endregion

		#region Prop
		private bool m_IsDrawFrame = true;
		[Category("_Button")]
		public bool IsDrawFrame
		{
			get { return m_IsDrawFrame; }
			set { m_IsDrawFrame = value; this.Invalidate(); }
		}

		public int m_Id = 0;
		[Category("_Button")]
		public int Id
		{
			get { return m_Id; }
			set { m_Id = value; }
		}
		private bool m_IsCheckMode = false;
		[Category("_Button")]
		public bool IsCheckMode
		{
			get { return m_IsCheckMode; }
			set {
				if (m_IsCheckMode != value)
				{
					m_IsCheckMode = value;
					//if (m_Checked) m_Checked = false;
					this.Invalidate(); ;
				}
			}
		}
		private bool m_Checked = false;
		[Category("_Button")]
		public bool Checked
		{
			get { return m_Checked; }
			set {
				if (m_Checked != value)
				{
					m_Checked = value;
					if (m_IsCheckMode)
					{
						OnCheckedChanged(new EventArgs());
					}
					this.Invalidate();
				}
			}
		}
		#endregion
		#region Layout
		private int m_LeftBar = 4;
		[Category("_Button_Size")]
		public int LeftBar
		{
			get { return m_LeftBar; }
			set { m_LeftBar = value; this.Invalidate(); }
		}
		private int m_RightBar = 4;
		[Category("_Button_Size")]
		public int RightBar
		{
			get { return m_RightBar; }
			set { m_RightBar = value; this.Invalidate(); }
		}
		private int m_TopBar = 0;
		[Category("_Button_Size")]
		public int TopBar
		{
			get { return m_TopBar; }
			set { m_TopBar = value; this.Invalidate(); }
		}
		private int m_BottomBar = 0;
		[Category("_Button_Size")]
		public int BottomBar
		{
			get { return m_BottomBar; }
			set { m_BottomBar = value; this.Invalidate(); }
		}
		private Color m_Color_line = Color.FromArgb(255, 150, 150, 200);
		[Category("_Button_Color")]
		public Color Color_line
        {
            get { return m_Color_line; }
            set { m_Color_line = value; Invalidate(); }
        }
        private Color m_Color_Enter = Color.FromArgb(64, 150, 150, 200);
		[Category("_Button_Color")]
		public Color Color_Enter
		{
			get { return m_Color_Enter; }
			set { m_Color_Enter = value; Invalidate(); }
		}
		private Color m_Color_Down = Color.FromArgb(200, 220, 220, 255);
		[Category("_Button_Color")]
		public Color Color_Down
		{
			get { return m_Color_Down; }
			set { m_Color_Down = value; Invalidate(); }
		}
		#endregion
		// ********************************************************* 
		public T_Button()
        {
            Size = new Size(100, 25);
            MyFontSize = 9;
            Alignment = StringAlignment.Center;
            LineAlignment = StringAlignment.Center;
            ForeColor = Color.FromArgb(255, 200, 200, 250);
			this.SetStyle(
			   ControlStyles.DoubleBuffer |
			   ControlStyles.UserPaint |
			   ControlStyles.AllPaintingInWmPaint|
			   ControlStyles.SupportsTransparentBackColor,
			   true);
			BackColor = Color.Transparent;
		}
        // *********************************************************
        protected override void OnPaint(PaintEventArgs pe)
        {
            //base.OnPaint(pe);
            Graphics g = pe.Graphics;
			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
			SolidBrush sb = new SolidBrush(Color.Transparent);
            Pen p = new Pen(m_Color_line);
            try
            {
                sb.Color = Color.Transparent;
				Rectangle r = new Rectangle(0, 0, Width, Height);
				Fill(g, sb);

                sb.Color = EnabledColor( m_Color_line ,Enabled);
                if(m_LeftBar>0)
                {
                    r = new Rectangle(0,0, m_LeftBar, Height);
					Fill(g, sb, r);
				}
				if (m_RightBar > 0)
				{
					r = new Rectangle(Width -m_RightBar, 0, m_RightBar, Height);
					Fill(g, sb, r);
				}
				if (m_TopBar > 0)
				{
					r = new Rectangle(0, 0, Width, m_TopBar);
					Fill(g, sb, r);
				}
				if (m_BottomBar > 0)
				{
					r = new Rectangle(0, Height - m_BottomBar, Width,m_BottomBar );
					Fill(g, sb, r);
				}

				int l = 0;
				if (m_LeftBar > 0) l += m_LeftBar + 2;
				int t = 0;
				if (m_TopBar > 0) t += m_TopBar + 2;
				int w = this.Width;
				if (m_LeftBar > 0) w = w - (m_LeftBar + 2);
				if (m_RightBar > 0) w = w - (m_RightBar + 2);
				int h = this.Height;
				if (m_TopBar > 0) h = h - (m_TopBar + 2);
				if (m_BottomBar > 0) h = h - (m_BottomBar + 2);
				r = new Rectangle(l,t,w,h);
                
				if(m_IsCheckMode && m_Checked)
				{
					sb.Color = EnabledColor(ForeColor,Enabled);
					Fill(g, sb, r);

				}
				else if (m_MouseDown)
                {
					sb.Color = EnabledColor(m_Color_Down, Enabled);
					Fill(g, sb, r);
				}
				else if (m_MouseEnter)
                {
					sb.Color = EnabledColor(m_Color_Enter, Enabled);
					Fill(g, sb, r);
                }

				if ((m_Checked)&&(m_IsCheckMode))
				{
					sb.Color = Color.Black;
				}
				else
				{
					sb.Color = EnabledColor(ForeColor, Enabled);
				}
				DrawStr(g, Text, sb, r);

				if (m_IsDrawFrame)
				{
					p.Color = EnabledColor(m_Color_line, Enabled);
					DrawFrame(g, p, r);
				}
            }
            finally
            {
                sb.Dispose();
                p.Dispose();
            }
        }

		#region Mouse Event
		private bool m_MouseEnter = false;
		private bool m_MouseDown = false;
		// *********************************************************
		protected override void OnMouseEnter(EventArgs e)
        {
            if (m_MouseEnter == false)
            {
                m_MouseEnter = true;
                Invalidate();
            }
            base.OnMouseEnter(e);
        }
		// *********************************************************
		protected override void OnMouseLeave(EventArgs e)
        {
            if (m_MouseEnter == true)
            {
                m_MouseEnter = false;
                Invalidate();
            }
            base.OnMouseLeave(e);
        }
		// *********************************************************
		public bool IsMouseDown
		{
			get { return m_MouseDown; }
			set
			{
				if(m_MouseDown != value)
				{
					m_MouseDown = value;
					this.Refresh();
				}
			}
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (m_IsCheckMode==true)
			{
				m_Checked = !m_Checked;
				OnCheckedChanged(new EventArgs());
				this.Invalidate();
			}
			else 
			if (m_MouseDown == false)
			{
				m_MouseDown = true;
				Invalidate();
			}
			base.OnMouseDown(e);
		}
        // *********************************************************
        protected override void OnMouseUp(MouseEventArgs e)
        {
			if (m_MouseDown == true)
			{
				m_MouseDown = false;
				Invalidate();
			}
			base.OnMouseUp(e);
        }
		#endregion

		protected override void OnTextChanged(EventArgs e)
        {
            Invalidate();
            base.OnTextChanged(e);
        }
		protected override void OnEnabledChanged(EventArgs e)
		{
			this.Invalidate();
			base.OnEnabledChanged(e);
		}
	}
}
