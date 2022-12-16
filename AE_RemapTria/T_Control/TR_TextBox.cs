using BRY;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AE_RemapTria
{
	public partial class TR_TextBox : TR_DialogControl
	{
		public event EventHandler? EditFinished = null;
		protected virtual void OnEditFinished(EventArgs e)
		{
			if (EditFinished != null)
			{
				EditFinished(this, e);
			}
		}
		private bool m_CanReturnEdit = true;
		[Category("_AE_Remap")]
		public bool CanReturnEdit
		{
			get { return m_CanReturnEdit; }	
			set { m_CanReturnEdit = value; }
		}

		private StringFormat m_sf = new StringFormat();
		private TextBox? m_TextBox = null;
		public TR_TextBox()
		{
			m_sf.Alignment = StringAlignment.Near;
			m_sf.LineAlignment = StringAlignment.Center;
			this.ForeColor = Color.FromArgb(255, 120, 220, 250);
			this.BackColor = Color.FromArgb( 6, 11, 25);
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
			Graphics g = pe.Graphics;
			SolidBrush sb = new SolidBrush(this.BackColor);
			Pen p = new Pen(Color.FromArgb(50,50,75));

			try
			{
				sb.Color = BackColor;
				g.FillRectangle(sb, this.ClientRectangle);
				Rectangle rct = DrawArea;
				sb.Color = this.ForeColor;
				g.DrawString(this.Text, this.Font, sb, rct, m_sf);
				sb.Color = m_FrameColor;
				DrawPadding(g, sb);

				if (this.Focused)
				{
					p.DashStyle = DashStyle.Dot;
					p.Color = Color.Gray;
					DrawFrame(g, p, 1);
					p.DashStyle = DashStyle.Solid;
				}


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
			m_TextBox.Location = new Point(0, 0);
			m_TextBox.BorderStyle = BorderStyle.None;
			m_TextBox.Size = new Size(Width,Height );
			m_TextBox.Text = this.Text;
			m_TextBox.LostFocus += M_TextBox_LostFocus;
			m_TextBox.KeyDown += M_TextBox_KeyDown;
			m_TextBox.KeyPress += M_TextBox_KeyPress;

			m_TextBox.Font = this.Font;
			m_TextBox.ForeColor = Color.Black;
			m_TextBox.BackColor = Color.FromArgb(220, 220,220);
			m_TextBox.SelectionStart = m_TextBox.Text.Length;
			this.Controls.Add(m_TextBox);
			m_IsEdit=true;
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
			}else if (e.KeyChar == (char)Keys.Escape)
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
				if(m_TextBox!=null) m_TextBox.Text = "";
			}
		}



		public void EndEdit()
		{
			if (m_IsEdit == false) return;
			m_IsEdit = false;
			this.Controls.Remove(m_TextBox);
			OnEditFinished(new EventArgs());
		}
		public bool ChkEdit()
		{
			bool ret = false;
			if (m_IsEdit == false) return false;
			if (m_TextBox != null)
			{
				string s = m_TextBox.Text.Trim();
				if (this.Text != s)
				{
					this.Text = s;
				}
			}
			ret = true;
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
			if (m_IsEdit == false)
			{
				SetEdit();
				return;
			}
			base.OnMouseDown(e);
		}
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if((this.Focused)&&(m_IsEdit == false)&&(e.KeyData==Keys.Enter)&&(m_CanReturnEdit))
			{
				SetEdit();
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
		public void StopEdit()
		{
			if(m_IsEdit==true)
			{
				ChkEdit();
				EndEdit();
			}
		}
	}
}
