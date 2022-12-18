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
	public partial class TR_NameDialog : TR_BaseDialog
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
		public TR_NameDialog()
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
		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (e.KeyData == Keys.Escape)
			{
				this.DialogResult = DialogResult.Cancel;
			}
			base.OnKeyDown(e);
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			tbCaption.StopEdit();
		}
	}
}
