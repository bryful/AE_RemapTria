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
			SetEventHandler(tR_ColorPlate1);
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
		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (e.KeyData == Keys.Escape)
			{
				this.DialogResult = DialogResult.Cancel;
			}
			base.OnKeyDown(e);
		}

		public override void SetTRForm(TR_Form fm)
		{
			base.SetTRForm(fm);
			if((Form!=null) &&(CellData!=null))
			{
				SetCelLData(CellData);	
			}
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			
			foreach(Control c in this.Controls)
			{
				if(c is TR_CaptionTextBox)
				{
					((TR_CaptionTextBox)c).StopEdit();
				}
			}

		}
	}
}
