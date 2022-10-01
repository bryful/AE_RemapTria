using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE_RemapTria
{
	public class T_SheetSettingDialog : T_DialogBase
	{
		public T_SheetSettingDialog()
		{

		}

		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// T_SheetSettingDialog
			// 
			this.Alignment = System.Drawing.StringAlignment.Near;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
			this.ClientSize = new System.Drawing.Size(359, 221);
			this.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.Name = "T_SheetSettingDialog";
			this.Text = "T_DialogAA";
			this.ResumeLayout(false);

		}
	}
}
