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
	public partial class T_OpenSaveDialog : T_BaseDialog
	{
		public string DirectoryName
		{
			get { return FList.FullName; }
			set
			{
				if(Directory.Exists(value))
				{
					FList.FullName = value;
				}
			}
		}


		public T_OpenSaveDialog()
		{
			CanReSize = true;
			InitializeComponent();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}

	}
}
