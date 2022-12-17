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
	public partial class TR_OffsetFrameDialog : TR_BaseDialog
	{
		public int Value
		{
			get
			{
				return (int)nbOffset.Value;
			}
			set
			{
				nbOffset.Value = value;
			}
		}
		public TR_OffsetFrameDialog()
		{
			InitializeComponent();
			SetEventHandler(lbMain);
			SetEventHandler(lbOffset);
			SetEventHandler(t_Zebra1);
			nbOffset.ValueMax = 32000;
			nbOffset.ValueMin = -32000;

		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
		}
		private int m_md = 0;
		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (m_md == 0)
			{
				if (e.KeyCode == Keys.Enter)
				{
					m_md = 1;
					btnOK.IsMouseDown = true;
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
			if(m_md != 0)
			{
				if (m_md==1)
				{
					btnOK.IsMouseDown = false;
					this.DialogResult = DialogResult.OK;
				}else if(m_md==2)
				{
					btnCancel.IsMouseDown = false;
					this.DialogResult = DialogResult.Cancel;
				}
				m_md = 0;
			}
			base.OnKeyUp(e);
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			nbOffset.StopEdit();
		}
	}
}
