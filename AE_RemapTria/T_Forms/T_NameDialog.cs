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
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if ((ValueText == "") || (m_Original_ValueText == ValueText)) return;
			this.DialogResult = DialogResult.OK;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}
	}
}
