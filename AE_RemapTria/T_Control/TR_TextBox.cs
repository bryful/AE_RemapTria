using BRY;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AE_RemapTria
{
	public partial class TR_TextBox : TR_DialogControl
	{
		private StringFormat m_sf = new StringFormat();
		private TextBox? m_TextBox = null;
		public TR_TextBox()
		{
			m_sf.Alignment = StringAlignment.Near;
			m_sf.LineAlignment = StringAlignment.Center;
			this.ForeColor = Color.FromArgb(255, 120, 220, 250);
			this.BackColor = Color.FromArgb( 6, 11, 25);
			InitializeComponent();
		}
		protected override void OnPaint(PaintEventArgs pe)
		{
			Graphics g = pe.Graphics;
			SolidBrush sb = new SolidBrush(this.BackColor);

			try
			{
				sb.Color = BackColor;
				g.FillRectangle(sb, this.ClientRectangle);
				Rectangle rct = DrawArea;
				sb.Color = this.ForeColor;
				g.DrawString(this.Text, this.Font, sb, rct, m_sf);
				sb.Color = m_FrameColor;
				DrawPadding(g, sb);
			}
			finally
			{
				sb.Dispose();
			}
		}
		public void SetEdit()
		{
			if (m_TextBox != null) return;
			m_TextBox = new TextBox();
			m_TextBox.Name = "a";
			m_TextBox.AutoSize = false;
			m_TextBox.Location = new Point(0, 0);
			m_TextBox.BorderStyle = BorderStyle.None;
			m_TextBox.Size = new Size(Width,Height );
			m_TextBox.Text = this.Text;
			m_TextBox.LostFocus += M_TextBox_LostFocus;
			m_TextBox.PreviewKeyDown += M_TextBox_PreviewKeyDown; ;
			m_TextBox.Font = this.Font;
			m_TextBox.ForeColor = this.ForeColor;
			m_TextBox.BackColor = Color.FromArgb(10, 10, 25);
			this.Controls.Add(m_TextBox);
			F_W.SetFocus(m_TextBox.Handle);
		}

		private void M_TextBox_PreviewKeyDown(object? sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				if (ChkEdit())
				{
					EndEdit();
				}
				e.IsInputKey = false;
			}
		}

		private void M_TextBox_KeyDown(object? sender, KeyEventArgs e)
		{
			
		}

		public void EndEdit()
		{
			if (m_TextBox == null) return;
			this.Controls.Remove(m_TextBox);
			m_TextBox = null;
		}
		public bool ChkEdit()
		{
			bool ret = false;
			if (m_TextBox == null) return ret;
			string s = m_TextBox.Text.Trim();
			if (this.Text != s)
			{
				this.Text = s;
				ret = true;
			}
			return ret;
		}

		private void M_TextBox_LostFocus(object? sender, EventArgs e)
		{
			ChkEdit();
			EndEdit();
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if(m_TextBox == null)
			{
				SetEdit();
				return;
			}
			base.OnMouseDown(e);
		}
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			SetEdit();
		}
	}
}
