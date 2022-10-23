using BRY;
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
	public partial class T_GetKey : T_BaseDialog
	{
		private T_KeyBindDialog? _BindDialog=null;

		public void SetDialog(T_KeyBindDialog dlg)
		{
			_BindDialog = dlg;
		}
		private string m_errStr = "";
		private Keys m_Orgkey = Keys.None;
		private Keys m_key = Keys.None;
		public Keys Key
		{
			get { return m_key; }
		}
		public void SetKey(Keys k)
		{
			m_Orgkey = k;
			m_key = k;
			this.Invalidate();
		}
		public T_GetKey()
		{
			InitializeComponent();
		}
		private bool ChkKey(Keys k)
		{
			bool ret = false;
			if (k == Keys.None) return false;
			if (_BindDialog != null)
			{
				ret =_BindDialog.ChkKey(k);
			}
			return ret;
		}
		protected override void OnKeyDown(KeyEventArgs e)
		{
			m_errStr = "";
			base.OnKeyDown(e);
			m_key = (Keys)e.KeyData;
			if(m_key == Keys.Escape)
			{
				this.DialogResult = DialogResult.Cancel;
			}
			this.Invalidate();
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			m_errStr = "";
			if(e.Button == MouseButtons.Left)
			{
				if(e.X < 24)
				{
					m_key = Keys.None;
					this.Invalidate();
					return;
				}
				else if (e.X > this.Width - 24)
				{
					this.DialogResult = DialogResult.Cancel;
					return;
				}
				else if(m_Orgkey==m_key)
				{
					this.DialogResult = DialogResult.Cancel;
					return;
				}
				if (ChkKey(m_key)==false)
				{
					this.DialogResult = DialogResult.OK;
					return;
				}
				else
				{
					m_errStr = "exists key";
					this.Invalidate();
					return;
				}
			}
			else
			{
				this.DialogResult = DialogResult.Cancel;
				return;
			}
			//base.OnMouseDown(e);
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Graphics g = e.Graphics;
			SolidBrush sb = new SolidBrush(this.ForeColor);
			Pen p = new Pen(this.ForeColor,2);
			try
			{
				Rectangle r = new Rectangle(22,0,this.Width-44,this.Height);
				StringFormat sf = new StringFormat();
				sf.Alignment = StringAlignment.Center;
				sf.LineAlignment = StringAlignment.Center;
				string str =  "";
				if (m_errStr != "")
				{
					str = m_errStr;
				}
				else
				{
					str = T_G.KeyInfo(m_key);
				}
				g.DrawString(str, this.Font, sb, r, sf);
				g.DrawRectangle(p, r);

				p.Width = 1;
				r = new Rectangle(this.Width - 20, (this.Height - 20) / 2, 20, 20);
				g.DrawRectangle(p, r);
				g.DrawLine(p, r.Left, r.Top, r.Right, r.Bottom);
				g.DrawLine(p, r.Left, r.Bottom, r.Right, r.Top);
				r = new Rectangle(0, (this.Height - 20) / 2, 20, 20);
				g.DrawRectangle(p, r);
				g.DrawString("cl", this.Font, sb, r, sf);

			}
			finally
			{
				sb.Dispose();
				p.Dispose();
			}
		}
	}
}
