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
	public partial class TR_AboutDialog : TR_BaseDialog 
	{
		public string Info
		{
			get { return lbInfo.Text; }
			set { lbInfo.Text = value; }
		}
		public TR_AboutDialog()
		{
			InitializeComponent();
			SetEventHandler(t_Arrow1);
			SetEventHandler(t_Arrow2);
			SetEventHandler(t_ColorPlate1);
			SetEventHandler(t_ColorPlate2);
			SetEventHandler(t_Zebra1);
			SetEventHandler(t_Label1);
			SetEventHandler(t_Label2);
			SetEventHandler(t_Label3);
			SetEventHandler(t_Label4);
			SetEventHandler(t_Label5);
			foreach(Control c in this.Controls)
			{
				c.MouseClick += C_MouseClick;
				c.MouseDown += C_MouseDown;
			}
		}

		private void C_MouseDown(object? sender, MouseEventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			Close();
		}

		private void C_MouseClick(object? sender, MouseEventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			Close();
		}

		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			Close();
		}
		protected override void OnDoubleClick(EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			//base.OnDoubleClick(e);
			Close();
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			Close();
		}
	}
}
