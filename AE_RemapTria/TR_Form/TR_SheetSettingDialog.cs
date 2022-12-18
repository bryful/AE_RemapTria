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
	public partial class TR_SheetSettingDialog : TR_BaseDialog
	{
		public string SheetName
		{
			get { return tbSheetName.Text; }
			set { tbSheetName.Text = value; }
		}
		public string Caption
		{
			get { return lbStatus.Text; }
			set { lbStatus.Text = value; }
		}
		public int FrameCount
		{
			get { return DurationBox1.FrameCount; }
			set
			{
				DurationBox1.FrameCount = value;
			}
		}
		public TR_SheetSettingDialog()
		{
			InitializeComponent();
			SetEventHandler(lbMain);
			SetEventHandler(lbName);
			SetEventHandler(lbStatus);
			SetEventHandler(lbDuration);
			SetEventHandler(t_Label2);
			SetEventHandler(t_Zebra1);

			tbSheetName.EditFinished += TbSheetName_EditFinished;

		}

		private void TbSheetName_EditFinished(object? sender, EventArgs e)
		{
			if(tbSheetName.Text!="")
			{
				btnOK.Focus();
			}
		}
		private void BtnOK_Click(object sender, EventArgs e)
		{
			EndDialog();
		}
		private void EndDialog()
		{
			if (FrameCount < 12)
			{
				FrameCount = 12;
				return;
			}
			if (tbSheetName.Text == "")
			{
				tbSheetName.Focus();
				tbSheetName.SetEdit();
				return;
			}
			this.DialogResult = DialogResult.OK;
		}
		protected override void OnKeyDown(KeyEventArgs e)
		{
			KP v = KP.None;
			
			if((tbSheetName.IsEdit)||(DurationBox1.IsEdit))
			{
				base.OnKeyDown(e);
				return;
			}else 
			if((btnOK.Focused)&&(e.KeyData==Keys.Enter))
			{
				EndDialog();
				return;
			}else if ((btnCancel.Focused) && (e.KeyData == Keys.Enter))
			{
				this.DialogResult = DialogResult.Cancel;
			}else if (e.KeyData == Keys.Escape)
			{
				this.DialogResult = DialogResult.Cancel;
			}
			switch (e.KeyData)
			{
				case Keys.D0:
				case Keys.NumPad0:
					v = KP.key0;
					break;
				case Keys.D1:
				case Keys.NumPad1:
					v = KP.key1;
					break;
				case Keys.D2:
				case Keys.NumPad2:
					v = KP.key2;
					break;
				case Keys.D3:
				case Keys.NumPad3:
					v = KP.key3;
					break;
				case Keys.D4:
				case Keys.NumPad4:
					v = KP.key4;
					break;
				case Keys.D5:
				case Keys.NumPad5:
					v = KP.key5;
					break;
				case Keys.D6:
				case Keys.NumPad6:
					v = KP.key6;
					break;
				case Keys.D7:
				case Keys.NumPad7:
					v = KP.key7;
					break;
				case Keys.D8:
				case Keys.NumPad8:
					v = KP.key8;
					break;
				case Keys.D9:
				case Keys.NumPad9:
					v = KP.key9;
					break;
				case Keys.Back:
					v = KP.keyBS;
					break;
				case Keys.Delete:
					v = KP.keyCLS;
					break;
				case Keys.Add:
				case Keys.Oemplus:
				case Keys.Decimal:
					v = KP.keySEC;
					break;
			}
			if(v!= KP.None)
			{
				tR_KeyPad1.SetMDPos(v);
				tR_KeyPad1.Refresh();
				tR_KeyPad1.SetMDPos(KP.None);
				tR_KeyPad1.Refresh();
				tR_KeyPad1.SetDBox(v);
			}
			else
			{
				base.OnKeyDown(e);
			}

		}

		private void BtnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			tbSheetName.StopEdit();
			DurationBox1.StopEdit();
		}
	}
}
