using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AE_RemapTria
{
	public partial class TR_FrameRateDIalog : TR_BaseDialog
	{
		public T_Fps Fps
		{
			get { return tR_Fps1.Fps; }
			set { tR_Fps1.Fps = value; }
		}
		public TR_FrameRateDIalog()
		{
			InitializeComponent();
			SetEventHandler(tR_Label1);
			SetEventHandler(tR_Zebra1);
			btnCancel.Click += BtnCancel_Click;
			btnOK.Click += BtnOK_Click;
		}

		private void BtnOK_Click(object? sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}

		private void BtnCancel_Click(object? sender, EventArgs e)
		{
			this.DialogResult= DialogResult.Cancel;
		}
		protected override void OnKeyDown(KeyEventArgs e)
		{
			Debug.WriteLine(e.KeyData.ToString());	
			if (e.KeyData == Keys.Escape)
			{
				this.DialogResult = DialogResult.Cancel;
			}
			base.OnKeyDown(e);

		}
	}
}
