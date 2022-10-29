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
	public partial class T_NameDialog : T_BaseDialog
	{
		public bool CanSameName = false;

		public string Caption
		{
			get { return lbCaption.Text; }
			set { lbCaption.Text = value; }
		}
		private string m_Original_ValueText = "";
		public string ValueText
		{
			get { return tbCaption.Text; }
			set 
			{
				tbCaption.Text = value;
				m_Original_ValueText = value;
			}
		}
		public T_NameDialog()
		{
			InitializeComponent();
			SetEventHandler(lbCaption);
			SetEventHandler(t_Zebra1);
			SetEventHandler(t_Zebra2);

		}
		private bool ChkSameName()
		{
			if (CanSameName == true) return false;
			else
				return (m_Original_ValueText == ValueText);
		}
		private void btnOK_Click(object sender, EventArgs e)
		{
			if ((ValueText == "") || (ChkSameName())) return;
			this.DialogResult = DialogResult.OK;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}
		private int m_md = 0;
		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (m_md == 0)
			{
				if (e.KeyCode == Keys.Enter)
				{
					if ((ValueText != "") && (ChkSameName()==false))
					{
						m_md = 1;
						btnOK.IsMouseDown = true;
					}
				}
				else if (e.KeyCode == Keys.Escape)
				{
					m_md = 2;
					btnCancel.IsMouseDown = true;
				}
			}
			base.OnKeyDown(e);
		}
		protected override void OnKeyUp(KeyEventArgs e)
		{
			if (m_md != 0)
			{
				if (m_md == 1)
				{
					btnOK.IsMouseDown = false;
					this.DialogResult = DialogResult.OK;
				}
				else if (m_md == 2)
				{
					btnCancel.IsMouseDown = false;
					this.DialogResult = DialogResult.Cancel;
				}
				m_md = 0;
			}
			base.OnKeyUp(e);
		}
	}
}
