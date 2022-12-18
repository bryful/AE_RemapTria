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
	public partial class TR_NumberBox : TR_DialogControl
	{
		private TextBox m_TextBox = new TextBox();
		private int m_value = 0;
		[Category("_AE_Remap")]
		public int Value
		{
			get { return m_value; }	
			set
			{
				if (m_value != value)
				{
					m_value = value;
					if (m_value < m_ValueMin) { m_value = m_ValueMin; }
					else if (m_value > m_ValueMax) m_value = m_ValueMax;
				}
			}
		}
		private int m_ValueMax = 32000;
		[Category("_AE_Remap")]
		public int ValueMax
		{
			get { return m_ValueMax; }
			set
			{
				if (m_ValueMax != value)
				{
					m_ValueMax = value;
					if (m_value > m_ValueMax) { m_value = m_ValueMax; }
				}
			}
		}
		private int m_ValueMin = 0;
		[Category("_AE_Remap")]
		public int ValueMin
		{
			get { return m_ValueMin; }
			set
			{
				if (m_ValueMin != value)
				{
					m_ValueMin = value;
					if (m_value < m_ValueMin) { m_value = m_ValueMin; }
				}
			}
		}
		private bool m_CanReturnEdit = true;
		[Category("_AE_Remap")]
		public bool CanReturnEdit
		{
			get { return m_CanReturnEdit; }
			set { m_CanReturnEdit = value; }
		}
		private Color m_BtnColor = Color.FromArgb(75, 75, 100);
		private Color m_BtnColorHi = Color.FromArgb(110,110, 170);
		private Color m_FoucusColor = Color.FromArgb(75, 75, 100);
		private int m_BtnWidth = 20;
		public TR_NumberBox()
		{
			InitializeComponent();
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
		protected override void OnPaint(PaintEventArgs pe)
		{
			//base.OnPaint(pe);
			Graphics g = pe.Graphics;
			SolidBrush sb = new SolidBrush(Color.Transparent);
			Pen p = new Pen(ForeColor);

			try
			{

				Rectangle r = this.ClientRectangle;
				sb.Color = BackColor;
				g.FillRectangle(sb, r);

				Rectangle r2 = new Rectangle(0,0,this.Width-m_BtnWidth,this.Height);
				sb.Color = ForeColor;
				DrawStr(g, $"{m_value}", sb, r2);

				float x0 = this.Width - m_BtnWidth + 2;
				float x1 = x0 + ((float)m_BtnWidth - 4)/2;
				float x2 = x0 + ((float)m_BtnWidth - 4);
				float cy = ((float)this.Height/2);

				PointF[] ps = new PointF[]
				{
					new PointF(x0,cy-2),
					new PointF(x1,cy-10),
					new PointF(x2,cy-2),

				};
				if (m_MDPos == 0)
				{
					sb.Color = m_BtnColorHi;
				}
				else
				{
					sb.Color = m_BtnColor;
				}
				g.FillPolygon(sb, ps);
				ps = new PointF[]
				{
					new PointF(x0,cy+2),
					new PointF(x1,cy+10),
					new PointF(x2,cy+2),

				};
				if (m_MDPos == 1)
				{
					sb.Color = m_BtnColorHi;
				}
				else
				{
					sb.Color = m_BtnColor;
				}
				g.FillPolygon(sb, ps);

				sb.Color = EnabledColor(m_FrameColor, this.Focused);
				TRc.DrawFrame(g,sb,r, m_FrameWeight);
				if(this.Focused)
				{
					p.Width = 1;
					p.Color = m_FoucusColor;
					p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
					g.DrawRectangle(p, DrawAreaIn);

				}
			}
			finally
			{
				sb.Dispose();
				p.Dispose();
			}

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
		private int m_MDPos = -1;
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Left)
			{
				if(e.X>this.Width -m_BtnWidth)
				{
					if(e.Y< this.Height/2)
					{
						m_MDPos = 0;
						m_value += 1;
						if (m_value > m_ValueMax) m_value = m_ValueMax;
					}
					else
					{
						m_MDPos = 1;
						m_value -= 1;
						if (m_value < m_ValueMin) m_value = m_ValueMin;
					}
					this.Invalidate();
				}
				else
				{
					if (m_IsEdit == false)
					{
						SetEdit();
					}
				}

			}
			else
			{
				base.OnMouseDown(e);
				m_MDPos = -1;

			}
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if(m_MDPos > -1)
			{
				m_MDPos= -1;
				this.Invalidate();
			}
			base.OnMouseUp(e);
		}
		private bool m_IsEdit = false;
		public bool IsEdit { get { return m_IsEdit; } }
		public void SetEdit()
		{
			if (m_IsEdit) return;
			m_TextBox = new TextBox();
			m_TextBox.Name = "a";
			m_TextBox.AutoSize = false;
			m_TextBox.Location = new Point(0, 0);
			m_TextBox.BorderStyle = BorderStyle.None;
			m_TextBox.Size = new Size(Width-m_BtnWidth, Height);
			m_TextBox.Text = $"{m_value}";
			m_TextBox.LostFocus += M_TextBox_LostFocus;
			m_TextBox.KeyDown += M_TextBox_KeyDown;
			m_TextBox.KeyPress += M_TextBox_KeyPress;
			if(Alignment==StringAlignment.Near)
			{
				m_TextBox.TextAlign = HorizontalAlignment.Left;
			}
			else
			{
				m_TextBox.TextAlign = HorizontalAlignment.Right;
			}
			m_TextBox.Font = this.Font;
			m_TextBox.ForeColor = Color.Black;
			m_TextBox.BackColor = Color.FromArgb(220, 220, 220);
			m_TextBox.SelectionStart = m_TextBox.Text.Length;
			this.Controls.Add(m_TextBox);
			m_IsEdit = true;
			m_TextBox.Focus();
		}

		private void M_TextBox_KeyPress(object? sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Enter)
			{
				if (ChkEdit())
				{
					EndEdit();
				}
				e.Handled = true;
			}
			else if (e.KeyChar == (char)Keys.Escape)
			{
				ChkEdit();
				EndEdit();
				e.Handled = true;
			}
		}

		private void M_TextBox_KeyDown(object? sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				if (m_TextBox != null) m_TextBox.Text = "";
			}
		}



		public void EndEdit()
		{
			if (m_IsEdit == false) return;
			m_IsEdit = false;
			this.Controls.Remove(m_TextBox);
			//OnEditFinished(new EventArgs());
		}
		public bool ChkEdit()
		{
			bool ret = false;
			if (m_IsEdit == false) return false;
			if (m_TextBox != null)
			{
				string s = m_TextBox.Text.Trim();
				int v = -1;
				if (int.TryParse(s, out v))
				{
					if (m_value != v)
					{
						m_value = v;
						if (v < m_ValueMin) { v = m_ValueMin; }
						else if (v > m_ValueMax) { v = m_ValueMax; }
						this.Invalidate();
						ret = true;
					}
				}
			}
			return ret;
		}

		private void M_TextBox_LostFocus(object? sender, EventArgs e)
		{
			if (m_IsEdit)
			{
				ChkEdit();
				EndEdit();
			}
			this.Invalidate();
		}
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if ((this.Focused) && (m_IsEdit == false) && (e.KeyData == Keys.Enter) && (m_CanReturnEdit))
			{
				SetEdit();
			}
		}
		public void StopEdit()
		{
			if (m_IsEdit == true)
			{
				ChkEdit();
				EndEdit();
			}
		}
	}
}
