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
	public partial class TR_AutoInputDialog : TR_BaseDialog
	{
		public int Start
		{
			get { return nbStart.Value; }
			set { nbStart.Value = value; }
		}
		public int Last
		{
			get { return (int)nbLast.Value; }
			set { nbLast.Value = value; }
		}
		public int Koma
		{
			get { return (int)nbKoma.Value; }
			set { nbKoma.Value = value; }
		}
		public TR_AutoInputDialog()
		{
			InitializeComponent();
			SetEventHandler(t_Zebra1);
			SetEventHandler(t_Zebra2);
		}

		private bool CheckValue()
		{
			return  ((Start != Last) && (Koma > 0));
		}
		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (CheckValue())
			{
				this.DialogResult = DialogResult.OK;
			}
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
			nbKoma.StopEdit();
			nbStart.StopEdit();
			nbLast.StopEdit();
		}
	}
}
