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

		
		private void button1_Click_1(object sender, EventArgs e)
		{
			string s = @"C:\AAA\bbb.ardj.jsx";

			//string p = T_Def.GetDir(s);
			//string n = T_Def.GetName(s);
			//string ne = T_Def.GetNameNoExt(s);
			//string ee = T_Def.GetExt(s);
			string js = T_Def.ToJSPath(s);
			string ws = T_Def.ToWindwsPath(js);
			//string s2 = $"{p}\r\n{n}\r\n{ne}\r\n{ee}";
			MessageBox.Show(js+"\r\n"+ws);

		}
	}
}
