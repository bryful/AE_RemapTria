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
	public partial class TR_ColorSettingsDialog : TR_BaseDialog
	{

		public TR_ColorSettingsDialog()
		{
			InitializeComponent();
			btnOK.Click += BtnOK_Click;
			btnCancel.Click += BtnCancel_Click;
		}

		private void BtnCancel_Click(object? sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}

		private void BtnOK_Click(object? sender, EventArgs e)
		{
			this.DialogResult= DialogResult.OK;
		}
		protected override void OnKeyDown(KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Escape)
			{
				this.DialogResult = DialogResult.Cancel;
			}
			base.OnKeyDown(e);
		}
		public override void SetTRForm(TR_Form fm)
		{
			base.SetTRForm(fm);
			if(fm != null)
			{
				tR_EditColorList1.SetTRDialog(this);
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			Colors.CopyToCSharp();
		}
	}
}
