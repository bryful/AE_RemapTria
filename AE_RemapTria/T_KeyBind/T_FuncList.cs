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
    public partial class T_FuncList : TR_ListBox
	{
		public T_Funcs Funcs = new T_Funcs();
		public void SetFuncs(T_Funcs f)
		{
			Funcs.CopyFrom(f);
			this.Items.Clear();
			this.Items.AddRange(f.Names);
		}
		private T_Funcs t_Funcs = new T_Funcs();
		public T_FuncList()
		{
			//this.BorderStyle = BorderStyle.FixedSingle;
			this.Size = new Size(100, 100);
			this.ForeColor = Color.FromArgb(255, 120, 220, 250);
			this.BackColor = Color.Transparent;
			InitializeComponent();

		}

		public string[] Names
		{
			get
			{
				string[] ret = new string[this.Items.Count];
				if (this.Items.Count>0)
				{
					for(int i = 0; i < this.Items.Count; i++)
					{
						string s = this.Items[i].ToString();
						if (s != null) s = "";
						ret[i] = s;
					}
				}
				return ret;
			}
			set
			{
				this.Items.Clear();
				this.Items.AddRange(value);
			}
		}
	}
}
