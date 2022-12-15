using BRY;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AE_RemapTria
{
	public partial class T_KeyBindDialog : T_BaseDialog
	{
		public T_Funcs Funcs = new T_Funcs();
		public T_KeyBindDialog()
		{
			InitializeComponent();
			SetEventHandler(t_Zebra1);
			SetEventHandler(t_Zebra2);
			SetEventHandler(t_Label1);
		}
		public bool ChkKey(Keys k)
		{
			FuncItem? f = Funcs.FindKeys(k);
			return (f != null);
		}

		public string[] Names
		{
			get { return t_ListBox1.Names; }
			set { t_ListBox1.Names = value; }
		}
		public void SetFuncs(T_Funcs fs)
		{
			Funcs.CopyFrom(fs);
			t_ListBox1.Names = fs.Names;
			//t_FuncList1.SetFuncs(fs);
		}
		private void btnOK_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}

		private void t_KeysPanel1_MouseDown(object sender, MouseEventArgs e)
		{

		}
		private void ShoEGetKey(int idx)
		{

		}

		private void btnGetKey1_Click(object sender, EventArgs e)
		{
			int sidx = t_ListBox1.SelectedIndex;
			if (sidx < 0) return;
			FuncItem? f = Funcs.Items(sidx);
			if (f == null) return;
			TR_Button btn = (TR_Button)sender;
			int btnID = btn.Id;
			if (btnID < 0) btnID = 0;
			else if (btnID > 1) btnID = 1;

			T_GetKey dlg = new T_GetKey();
			dlg.SetDialog(this);
			dlg.Size = btn.Size;
			dlg.Location = new Point(this.Left+btn.Left, this.Top+btn.Top);
			lbCaution.Visible = true;
			dlg.SetKey(f.KeyArray[btnID]);

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				btn.Text = T_G.KeyInfo(dlg.Key);
				Funcs.FuncItems[sidx].SetKey(btnID, dlg.Key);
			}
			else
			{
				btn.Text = Funcs.Items(sidx).KeyArray[btnID].ToString();
			}
			dlg.Dispose();
			lbCaution.Visible = false;
		}

		private void t_ListBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (t_ListBox1.SelectedIndex > -1)
			{
				FuncItem f = Funcs.Items(t_ListBox1.SelectedIndex);
				tbJap.Text = f.JapName;
				if (f.KeyArray.Length >= 1)
				{
					btnGetKey1.Text = T_G.KeyInfo(f.KeyArray[0]);
				}
				else
				{
					btnGetKey1.Text = "None";
				}
				if (f.KeyArray.Length >= 2)
				{
					btnGetKey2.Text = T_G.KeyInfo(f.KeyArray[1]);
				}
				else
				{
					btnGetKey2.Text = "None";
				}
			}
		}

		private void tbJap_TextChanged(object sender, EventArgs e)
		{
			int idx = t_ListBox1.SelectedIndex;
			if (( idx>= 0)&&(Funcs.Count>0))
			{
				Funcs.SetJapName(idx, tbJap.Text);
			}
		}
	}
}
