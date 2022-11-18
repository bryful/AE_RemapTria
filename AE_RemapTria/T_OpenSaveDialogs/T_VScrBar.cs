using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace AE_RemapTria
{
	public class ValueChangedArg : EventArgs
	{
		public int Value;
		public ValueChangedArg(int v)
		{
			Value = v;
		}
	}
	public partial class T_VScrBar : Control
	{
		public delegate void ValueChangedHandler(object sender, ValueChangedArg e);
		public event ValueChangedHandler ValueChanged;
		protected virtual void OnValueChanged(ValueChangedArg e)
		{
			if (ValueChanged != null)
			{
				ValueChanged(this, e);
			}
		}
		private Bitmap ATop = Properties.Resources.HVS_Arrow_Top;
		private Bitmap ATopD = Properties.Resources.HVS_Arrow_Top_down;
		private Bitmap ABottom = Properties.Resources.HVS_Arrow_Bottom;
		private Bitmap ABottomD = Properties.Resources.HVS_Arrow_Bottom_down;
		private Bitmap CC = Properties.Resources.HVS_Cursol;
		private Bitmap CCD = Properties.Resources.HVS_Cursol_down;

		private int m_Value=0;
		[Category("_AE_Remap")]
		public int Value
		{
			get { return m_Value; }
			set
			{ 
				if(m_MaxValue <= 0)
				{
					m_Value = 0;
				}
				else
				{
					int v = value;
					if (v < 0) v = 0;
					if (v > m_MaxValue) v = m_MaxValue;
					int vt = v * m_VTrueMax / m_MaxValue;
					if (m_Value != v)
					{
						m_Value = v;
						m_VTrue = vt;
						CalcY();
						OnValueChanged(new ValueChangedArg(m_Value));
						this.Invalidate();
					}
				}
			}
		}
		private int m_MaxValue = 100;
		[Category("_AE_Remap")]
		public int MaxValue
		{
			get { return m_MaxValue; }
			set
			{
				m_MaxValue = value;
				if (m_MaxValue < 0)
				{
					m_MaxValue = 0;
					m_Value = 0;
					m_VTrue = 0;
				}

				CalcY();
				this.Invalidate();
			}
		}
		private int m_VTrue = 0;
		private int m_VTrueMax = 300;
		public T_VScrBar()
		{
			this.Size = new Size(20, 100);
			InitializeComponent();
			this.SetStyle(
	ControlStyles.DoubleBuffer |
	ControlStyles.UserPaint |
	ControlStyles.AllPaintingInWmPaint |
	ControlStyles.SupportsTransparentBackColor,
	true);
			this.ForeColor = Color.FromArgb(100, 100, 180);
			this.BackColor = Color.Transparent;
			this.UpdateStyles();
			CalcY();
		}
		private int m_md = 0;
		private int m_mdp = 0;
		private int m_mdv = 0;

		protected override void OnMouseDown(MouseEventArgs e)
		{
			int y = e.Y;
			if(y<ATop.Height)
			{
				m_md = 1;
				this.Invalidate();
			}else if (y > this.Height -ABottom.Height)
			{
				m_md = 2;
				this.Invalidate();
			}else if((m_MaxValue>0)&&(m_ValueRect.Height>0)&&(y>=m_ValueRect.Top)&&(y<m_ValueRect.Bottom))
			{
				m_md = 3;
				m_mdp = e.Y;
				m_mdv = m_VTrue;
				this.Invalidate();
			}
			else
			{
				base.OnMouseDown(e);
			}
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (m_md == 3)
			{

				int ay = (e.Y - m_mdp) ;
		
				m_VTrue = m_mdv + ay;
				if (m_VTrue < 0) m_VTrue = 0;
				else if (m_VTrue > m_VTrueMax) m_VTrue = m_VTrueMax;
				m_ValueRect = new Rectangle(0, m_VTrue + ATop.Height, CC.Width, CC.Height);
				int v = m_VTrue * m_MaxValue / m_VTrueMax;
				if(m_Value !=v)
				{
					m_Value = v;
					OnValueChanged(new ValueChangedArg(m_Value));
				}
				this.Invalidate();
			}
			else
			{
				base.OnMouseMove(e);
			}
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if((m_md == 1)|| (m_md == 2))
			{
				if(m_md == 1)
				{
					m_VTrue -= 60;
				}
				else
				{
					m_VTrue += 60;
				}
				if (m_VTrue < 0) m_VTrue = 0;
				else if (m_VTrue > m_VTrueMax) m_VTrue = m_VTrueMax;
				m_ValueRect = new Rectangle(0, m_VTrue + ATop.Height, CC.Width, CC.Height);
				int v = m_VTrue * m_MaxValue / m_VTrueMax;
				if (m_Value != v)
				{
					m_Value = v;
					OnValueChanged(new ValueChangedArg(m_Value));
				}
				m_md = 0;
				this.Invalidate();
			}
			else if (m_md > 0)
			{
				m_md = 0;
				this.Invalidate();
			}
			else
			{
				base.OnMouseUp(e);
			}
		}
		private Rectangle m_ValueRect = new Rectangle(0, 0, 0, 0);
		private void CalcY()
		{
			if (m_MaxValue > 0)
			{
				m_VTrueMax = this.Height - ATop.Height - ABottom.Height - CC.Height;
				if (m_VTrueMax < 0) m_VTrueMax = 0;
				if (m_VTrue > m_VTrueMax)
				{
					m_VTrue = m_VTrueMax;
					m_Value = m_MaxValue;
				}
				m_ValueRect = new Rectangle(0, m_VTrue + ATop.Height, CC.Width, CC.Height);
				this.Invalidate();
			}
			else
			{
				m_ValueRect = new Rectangle(0, 0,0,0);
				m_VTrue = 0;
				m_VTrueMax = 0;
			}

		}
		protected override void OnResize(EventArgs e)
		{
			CalcY();
			base.OnResize(e);
		}
		protected override void OnPaint(PaintEventArgs pe)
		{
			//base.OnPaint(pe);
			Graphics g = pe.Graphics;
			SolidBrush sb = new SolidBrush(Color.Transparent);
			try
			{
				g.FillRectangle(sb, this.ClientRectangle);

				int b = (this.Height - (ATop.Height + ABottom.Height+10))/10;
				Rectangle r = new Rectangle(0, ATop.Height + 5, 6, 1);
				sb.Color = this.ForeColor;
				for(int i=0;i<11;i++)
				{
					int l = 8;
					if (i == 5) l = 12;
					else if (i % 2 == 1) l = 4;
					r = new Rectangle(0, b * i + ATop.Height + 5, l, 1);
					g.FillRectangle(sb, r);
				}



				if (m_md == 1)
				{
					g.DrawImage(ATopD, new Point(0, 0));
				}
				else
				{
					g.DrawImage(ATop, new Point(0, 0));
				}
				if (m_MaxValue > 0)
				{
					if (m_md == 3)
					{
						g.DrawImage(CCD, new Point(0, m_ValueRect.Top));
					}
					else
					{
						g.DrawImage(CC, new Point(0, m_ValueRect.Top));
					}
				}


				if (m_md == 2)
				{
					g.DrawImage(ABottomD, new Point(0, this.Height - ABottom.Height));
				}
				else
				{
					g.DrawImage(ABottom, new Point(0, this.Height - ABottom.Height));
				}

			}finally
			{
				sb.Dispose();
			}
		}
	}
}
