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
	public partial class T_AutoInputDialog : TR_BaseDialog
	{
		public int Start
		{
			get { return (int)nbStart.Value; }
			set { nbStart.Value = (decimal)value; }
		}
		public int Last
		{
			get { return (int)nbLast.Value; }
			set { nbLast.Value = (decimal)value; }
		}
		public int Koma
		{
			get { return (int)nbKoma.Value; }
			set { nbKoma.Value = (decimal)value; }
		}
		public T_AutoInputDialog()
		{
			InitializeComponent();
			SetEventHandler(t_Zebra1);
			SetEventHandler(t_Zebra2);
		}

		private void T_AutoInputcs_Load(object sender, EventArgs e)
		{

		}
		private bool CheckValue()
		{
			return  ((Start != Last) && (Koma > 0));
		}
		private int m_md = 0;
		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (m_md == 0)
			{
				if (e.KeyCode == Keys.Enter)
				{
					if (CheckValue())
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

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if(CheckValue())
			{
				this.DialogResult = DialogResult.OK;
			}
		}
	}
}
