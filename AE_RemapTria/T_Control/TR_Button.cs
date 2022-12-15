using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE_RemapTria
{
    public class TR_Button : TR_DialogControl
    {

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
		private int m_Group = 0;
		[Category("_Button")]
		public int Group
		{
			get { return m_Group; }
			set
			{ 
				m_Group = value; 
				if(m_IsCheckMode && m_Checked)
				{
					ChkRadioButton();
				}
			}
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
					ChkRadioButton();
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
						ChkRadioButton();
					}
					this.Invalidate();
				}
			}
		}
		#endregion
		#region Layout
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
		public TR_Button()
        {
            Size = new Size(100, 25);
            MyFontSize = 9;
            Alignment = StringAlignment.Center;
            LineAlignment = StringAlignment.Center;
            ForeColor = Color.FromArgb(255, 200, 200, 250);
			this.SetStyle(
				ControlStyles.Selectable |
				ControlStyles.UserMouse |
				ControlStyles.DoubleBuffer |
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.SupportsTransparentBackColor,
				true);
			this.BackColor = Color.Transparent;
			this.UpdateStyles();
		}
        // *********************************************************
        protected override void OnPaint(PaintEventArgs pe)
        {
            //base.OnPaint(pe);
            Graphics g = pe.Graphics;
			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
			SolidBrush sb = new SolidBrush(Color.Transparent);
            Pen p = new Pen(m_FrameColor);
            try
            {
                sb.Color = Color.Transparent;
				Rectangle r = new Rectangle(0, 0, Width, Height);
				Fill(g, sb);
                
				if(m_IsCheckMode && m_Checked)
				{
					sb.Color = EnabledColor(m_Color_Down,Enabled);
					Fill(g, sb, DrawArea);

				}
				else if (m_MouseDown)
                {
					sb.Color = EnabledColor(m_Color_Down, Enabled);
					Fill(g, sb, r);
				}
				else if (m_MouseEnter || this.Focused)
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
				Rectangle rct = DrawArea;
				DrawStr(g, Text, sb, rct);

				if (m_IsDrawFrame)
				{
					p.Color = EnabledColor(m_FrameColor, Enabled);
					DrawFrame(g, p,rct,1);
				}
				sb.Color = EnabledColor(m_FrameColor, Enabled);
				DrawPadding(g, sb);
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
		private void ChkRadioButton()
		{
			if (this.Parent == null) return;
			if(m_Checked==false) return;
			if(this.Parent.Controls.Count>1)
			{
				foreach(Control c in this.Parent.Controls)
				{
					if (c is TR_Button)
					{
						TR_Button b = (TR_Button)c;
						if ((b == this)||(b.m_IsCheckMode==false)) continue;
						if(b.m_Group == m_Group)
						{
							if(b.m_Checked )
							{
								b.m_Checked = false;
								b.Invalidate();
							}

						}
					}
				}
			}
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (m_IsCheckMode==true)
			{
				if(m_Checked==false)
				{
					m_Checked = true;
					ChkRadioButton();
				}
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
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			this.Invalidate();
		}
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			this.Invalidate();
		}
	}
}
