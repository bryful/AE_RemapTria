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
	public partial class TR_SheetInfoDialog : TR_BaseDialog
	{
		public TR_SheetInfoDialog()
		{
			InitializeComponent();
			SetEventHandler(t_Zebra1);
			//SetEventHandler(t_ColorPlate1);
			SetEventHandler(t_ColorPlate2);

		}

		private void btnCANCEL_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
		}

		public void SetCelLData(TR_CellData cd)
		{
			ctTittle.ValueText = cd.TITLE;
			ctSubTitle.ValueText = cd.SUB_TITLE;
			ctOpus.ValueText = cd.OPUS;
			ctScecne.ValueText = cd.SCECNE;
			ctCut.ValueText = cd.CUT;
			ctCampany.ValueText = cd.CAMPANY_NAME;
			ctCU.ValueText = cd.CREATE_USER;
			ctUU.ValueText = cd.UPDATE_USER;
			ctCT.ValueText = cd.CREATE_TIME.ToString();
			ctUT.ValueText = cd.UPDATE_TIME.ToString();
		}
		public void GetCellData(ref TR_CellData cd)
		{
			cd.TITLE = ctTittle.ValueText;
			cd.SUB_TITLE = ctSubTitle.ValueText;
			cd.OPUS = ctOpus.ValueText;
			cd.SCECNE = ctScecne.ValueText;
			cd.CUT = ctCut.ValueText;
			cd.CAMPANY_NAME = ctCampany.ValueText;
			cd.CREATE_USER = ctCU.ValueText;
			cd.UPDATE_USER = ctUU.ValueText;

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
					btnCANCEL.IsMouseDown = true;
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
					btnCANCEL.IsMouseDown = false;
					this.DialogResult = DialogResult.Cancel;
				}
				m_md = 0;
			}
			base.OnKeyUp(e);
		}
	}
}
