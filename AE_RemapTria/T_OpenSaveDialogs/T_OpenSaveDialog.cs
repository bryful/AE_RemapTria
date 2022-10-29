using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;

using BRY;
namespace AE_RemapTria
{
	public partial class T_OpenSaveDialog : T_BaseDialog
	{
		public string Caption
		{
			get { return lbCaption.Text; }
			set { lbCaption.Text = value; }
		}
		/// <summary>
		/// カレントのフォルダ
		/// </summary>
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
		public string FileName
		{
			get { return tbFileName.Text; }
			set { tbFileName.Text = value; }
		}
		public string FullName
		{
			get { return Path.Combine(DirectoryName, FileName); }
			set
			{
				DirectoryName = Path.GetDirectoryName(value);
				FileName = Path.GetFileName(value);
			}
		}
		// ****************************************************************
		public T_OpenSaveDialog()
		{
			CanReSize = true;
			InitializeComponent();
			SetEventHandler(lbCaption);
			SetEventHandler(lbDirectory);
			SetEventHandler(zebra1);
			SetEventHandler(t_ColorPlate1);

		}

		// ****************************************************************
		public bool ExportPref()
		{
			JsonObject jo = new JsonObject();

			jo.Add("width", this.Width);
			jo.Add("height", this.Height);
			jo.Add("drives", t_DriveList1.ToJsonObject());
			jo.Add("boolmark", t_bListBox1.ToJsonObject());
			jo.Add("filters", t_FileExtFilter1.ToJsonObject());
			
			ここから

			return true;
		}
		// ****************************************************************
		public bool ImportDrive()
		{
			t_bListBox1.Import();
			return t_DriveList1.Load();
		}
		public bool ExportDrive()
		{
			t_bListBox1.Export();
			return t_DriveList1.Save();
		}
		#region Event
		private void btnOK_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}
		#endregion

		private void tbFileName_TextChanged(object sender, EventArgs e)
		{
			T_TextBox rd = (T_TextBox)sender;
			btnOK.Enabled = (rd.Text != "");
		}
	}
}
