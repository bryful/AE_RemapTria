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
	public partial class TR_EditColor : TR_DialogControl
	{
		public event EventHandler? ValueChanged = null;
		protected virtual void OnValueChanged(EventArgs e)
		{
			if (ValueChanged != null)
			{
				ValueChanged(this, e);
			}
		}
		private TextBox? m_TextBox = null;

		private COLS m_TargetCOLS = COLS.GLine;
		public COLS TargetCOLS
		{
			get { return m_TargetCOLS; }
			set
			{ 
				m_TargetCOLS = value;
				Text = TR_Colors.COLSName(m_TargetCOLS);
				this.Invalidate();
			}	
		}
		public TR_EditColor()
		{
			MyFontIndex= 5;
			MyFontSize= 10;
			Colors = new TR_Colors();
			this.Size = new Size(250, 25);
			InitializeComponent();
		}
		private int m_CaptionWidth = 160;
		public int CaptionWidth
		{
			get { return m_CaptionWidth; }
			set { m_CaptionWidth = value; this.Invalidate(); }
		}
		// ****************************************************
		private string ColorTo(Color c)
		{
			if( c.A<255)
			{
				return $"{c.A},{c.R},{c.G},{c.B}";
			}
			else
			{
				return $"{c.R},{c.G},{c.B}";
			}
		}
		// ****************************************************
		protected override void OnPaint(PaintEventArgs pe)
		{
			if(Colors==null) return;
			SolidBrush sb = new SolidBrush(BackColor);
			Pen p= new Pen(ForeColor);
			Graphics g = pe.Graphics;
			try
			{
				sb.Color = Color.Transparent;
				g.FillRectangle(sb, this.ClientRectangle);

				sb.Color = Color.FromArgb(200, 200, 250);
				Rectangle rct = new Rectangle(0, 0, m_CaptionWidth, this.Height);
				m_format.Alignment = StringAlignment.Far;
				g.DrawString(Text,Font, sb, rct, m_format);

				Color t = Colors.Get(m_TargetCOLS);
				rct = new Rectangle(m_CaptionWidth,0,this.Height,this.Height);
				sb.Color = t;
				g.FillRectangle(sb, rct);
				p.Color = Color.Gray;
				p.Width = 1;
				rct = new Rectangle(m_CaptionWidth, 0, this.Height-1, this.Height-1);
				g.DrawRectangle(p,rct);

				int y = m_CaptionWidth + this.Height;
				sb.Color = Color.FromArgb(200, 200, 250);
				rct = new Rectangle(y, 0, this.Width-y, this.Height);
				m_format.Alignment = StringAlignment.Near;
				g.DrawString(ColorTo(t), Font, sb, rct, m_format);
				p.Color = Color.Gray;
				p.Width = 1;
				rct = new Rectangle(y, 0, this.Width - y-1, this.Height-1);
				g.DrawRectangle(p, rct);

			}
			finally
			{
				sb.Dispose();
				p.Dispose();
			}
		}
		private bool m_IsEdit = false;
		public bool IsEdit { get { return m_IsEdit; } }
		public void SetEdit()
		{
			if (m_IsEdit) return;
			m_TextBox = new TextBox();
			m_TextBox.Name = "a";
			m_TextBox.AutoSize = false;
			m_TextBox.Location = new Point(m_CaptionWidth+this.Height, 0);
			m_TextBox.BorderStyle = BorderStyle.None;
			m_TextBox.Size = new Size(Width -(m_CaptionWidth + this.Height), Height);
			m_TextBox.Text = ColorTo(Colors.Get(m_TargetCOLS));
			m_TextBox.LostFocus += M_TextBox_LostFocus;
			m_TextBox.KeyDown += M_TextBox_KeyDown;
			m_TextBox.KeyPress += M_TextBox_KeyPress;

			m_TextBox.Font = this.Font;
			m_TextBox.ForeColor = Color.Black;
			m_TextBox.BackColor = Color.FromArgb(220, 220, 220);
			m_TextBox.SelectionStart = m_TextBox.Text.Length;
			if(m_format.Alignment == StringAlignment.Near)
			{
				m_TextBox.TextAlign = HorizontalAlignment.Left;
			}
			else
			{
				m_TextBox.TextAlign = HorizontalAlignment.Right;
			}
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
		}
		public bool ChkEdit()
		{
			bool ret = false;
			if (m_IsEdit == false) return false;
			if (m_TextBox != null)
			{
				string s = m_TextBox.Text.Trim();
				if (s.Length > 0)
				{
					Color t = Colors.Get(m_TargetCOLS);
					int a;
					int r;
					int g;
					int b;
					string[] sa = s.Split(',');
					if(sa.Length >= 3)
					{
						int idx = 0;
						a = 255;
						if (sa.Length >= 4)
						{
							if (int.TryParse(sa[idx], out a) == false)
							{

								a = t.A;
							}
							else
							{
								if (a < 0) a = 0; else if (a > 255) a = 255;
							}
							idx++;
						}
						if (int.TryParse(sa[idx], out r) == false)
						{

							r = t.R;
						}
						else
						{
							if (r < 0) r = 0; else if (r > 255) r = 255;
						}
						idx++;
						if (int.TryParse(sa[idx], out g) == false)
						{

							g = t.G;
						}
						else
						{
							if (g < 0) g = 0; else if (g > 255) g = 255;
						}
						idx++;
						if (int.TryParse(sa[idx], out b) == false)
						{

							b = t.B;
						}
						else
						{
							if (b < 0) b = 0; else if (b > 255) b = 255;
						}
						idx++;
						
						Color n = Color.FromArgb(a,r,g,b);
						if(t != n)
						{
							Colors.Set(m_TargetCOLS, n);
							m_form.DrawAll();
							OnValueChanged(new EventArgs());
							this.Invalidate();
						}
					}
					ret = true;

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
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (m_IsEdit)
			{
				ChkEdit();
				EndEdit();
			}
			if ((e.X>=m_CaptionWidth+this.Height)&&(m_IsEdit==false))
			{
				SetEdit();
			}else if ((e.X>=m_CaptionWidth)&&(e.X <= m_CaptionWidth + this.Height))
			{
				ColorDialog dlg = new ColorDialog();
				dlg.AllowFullOpen = true;
				dlg.FullOpen = true;
				Color t = Colors.Get(m_TargetCOLS);
				dlg.Color = t;
				if(dlg.ShowDialog() == DialogResult.OK)
				{

				}
				dlg.Dispose();
			}
			base.OnMouseDown(e);

		}
	}
}
