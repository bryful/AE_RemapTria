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
	public partial class T_AboutDialog : T_BaseDialog
	{
		public string Info
		{
			get { return lbInfo.Text; }
			set { lbInfo.Text = value; }
		}
		public T_AboutDialog()
		{
			InitializeComponent();
			SetEventHandler(t_Arrow1);
			SetEventHandler(t_Arrow2);
			SetEventHandler(t_ColorPlate1);
			SetEventHandler(t_ColorPlate2);
			SetEventHandler(t_Zebra1);
		}
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			this.DialogResult = DialogResult.OK;
		}
		protected override void OnDoubleClick(EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			//base.OnDoubleClick(e);
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
		}

		private void t_ColorPlate1_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
		}
	}
}
