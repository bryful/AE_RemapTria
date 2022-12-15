using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Unicode;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace AE_RemapTria
{
    public partial class T_OpenSaveDialog : T_BaseDialog
	{
		[Category("_AE_Remap")]
		public string Caption
		{
			get { return lbCaption.Text; }
			set { lbCaption.Text = value; }
		}
		/// <summary>
		/// カレントのフォルダ
		/// </summary>
		[Category("_AE_Remap")]
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
		[Category("_AE_Remap")]
		public string FileName
		{
			get { return tbFileName.Text; }
			set { tbFileName.Text = value; }
		}
		[Category("_AE_Remap")]
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
		// ****************************************************
		public string PrefPath()
		{
			string p = PrefFile.GetFileSystemPath(Environment.SpecialFolder.ApplicationData);

			return Path.Combine(p, "AE_remap_OpenSaveDilaog.json");
		}
		// ****************************************************************
		public bool ExportPref()
		{
			bool ret = false;

			JsonObject jo = new JsonObject();

			jo.Add("width", this.Width);
			jo.Add("height", this.Height);
			jo.Add("drives", t_DriveList1.ToJsonObject());
			jo.Add("boolmark", t_bListBox1.ToJsonObject());
			jo.Add("filters", t_FileExtFilter1.ToJsonObject());
			var options = new JsonSerializerOptions
			{
				Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
				WriteIndented = true
			};
			string js= jo.ToJsonString(options);
			try
			{
				File.WriteAllText(PrefPath(), js);
				ret = true;
			}
			catch
			{
				ret = false;
			}
			return ret;
		}
		public bool ImpoetPref()
		{
			bool ret = false;
			string p = PrefPath();
			string str = "";
			try
			{
				if (File.Exists(p) == true)
				{
					str = File.ReadAllText(p, Encoding.GetEncoding("utf-8"));
				}
			}
			catch
			{
				ret = false;
				return ret;
			}
			var doc = JsonNode.Parse(str);
			if (doc == null) return ret;
			JsonObject jo = (JsonObject)doc;
			string key = "width";
			int w = 0;
			int h = 0;
			if (jo.ContainsKey(key)) w = jo[key].GetValue<int>();
			key = "height";
			if (jo.ContainsKey(key)) h = jo[key].GetValue<int>();
			if((w>0)&&(h>0))
			{
				this.Size = new Size(w, h);
			}
			key = "drives";
			if (jo.ContainsKey(key)) t_DriveList1.FromJsonObject(jo[key].AsObject());
			key = "boolmark";
			if (jo.ContainsKey(key)) t_bListBox1.FromJsonObject(jo[key].AsObject());
			key = "filters";
			if (jo.ContainsKey(key)) t_FileExtFilter1.FromJsonObject(jo[key].AsObject());

			return ret;
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
			TR_TextBox rd = (TR_TextBox)sender;
			btnOK.Enabled = (rd.Text != "");
		}
		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
			ExportPref();
		}
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			ImpoetPref();
		}

		private int m_kd = 0;
		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				if(btnOK.Enabled)
				{
					m_kd = 1;
					btnOK.IsMouseDown = true;
					btnOK.Invalidate();
				}
			}else if (e.KeyCode == Keys.Escape)
			{
				m_kd = 2;
				btnCancel.IsMouseDown = true;
				btnCancel.Invalidate();
			}
			else
			{
				base.OnKeyDown(e);

			}
		}
		protected override void OnKeyUp(KeyEventArgs e)
		{
			if (m_kd == 1)
			{
				btnOK.IsMouseDown = false;
				btnOK.Invalidate();
				this.DialogResult = DialogResult.OK;
			}else if (m_kd==2)
			{
				btnCancel.IsMouseDown = false;
				btnCancel.Invalidate();
				this.DialogResult = DialogResult.Cancel;
			}
			else
			{
				base.OnKeyUp(e);

			}
		}
	}
}
