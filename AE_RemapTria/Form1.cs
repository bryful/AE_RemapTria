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
using System.Xml.Linq;

namespace AE_RemapTria
{
	public partial class Form1 : T_Form
	{
		public Form1()
		{
			InitializeComponent();

		}

		private void button1_Click(object sender, EventArgs e)
		{
			JsonObject jo = new JsonObject();
			int[][] data = t_Grid1.CellData.ToArrayFromTarget();
			JsonArray array = new JsonArray();
			foreach (int[] a in data)
			{
				JsonArray array2 = new JsonArray();
				if (a.Length >= 2)
				{
					array2.Add(a[0]);
					array2.Add(a[1]);
					array.Add(array2);
				}
			}
			jo.Add("data", array);
			string s = jo.ToJsonString();
			MessageBox.Show(s);
		}


		private void button1_Click_1(object sender, EventArgs e)
		{
			//button1.Text= System.Globalization.CultureInfo.CurrentUICulture.Name;
			string s = t_Grid1.Funcs.ToJson();
			MessageBox.Show(s);
		}
	}
}
