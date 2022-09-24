namespace AE_RemapTria
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.t_Grid1 = new AE_RemapTria.T_Grid();
			this.t_Caption1 = new AE_RemapTria.T_Caption();
			this.t_Input1 = new AE_RemapTria.T_Input();
			this.SuspendLayout();
			// 
			// t_Grid1
			// 
			this.t_Grid1.Alignment = System.Drawing.StringAlignment.Far;
			this.t_Grid1.BackColor = System.Drawing.Color.Transparent;
			this.t_Grid1.Font = new System.Drawing.Font("源ノ角ゴシック Code JP L", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.t_Grid1.Font_Size = 14F;
			this.t_Grid1.Font_Style = System.Drawing.FontStyle.Regular;
			this.t_Grid1.FontBold = false;
			this.t_Grid1.LineAlignment = System.Drawing.StringAlignment.Center;
			this.t_Grid1.Location = new System.Drawing.Point(120, 75);
			this.t_Grid1.MaximumSize = new System.Drawing.Size(300, 384);
			this.t_Grid1.MFontIndex = 2;
			this.t_Grid1.MFontSize = 14F;
			this.t_Grid1.MinimumSize = new System.Drawing.Size(180, 96);
			this.t_Grid1.Name = "t_Grid1";
			this.t_Grid1.Size = new System.Drawing.Size(300, 348);
			this.t_Grid1.TabIndex = 1;
			this.t_Grid1.Text = "t_Grid1";
			// 
			// t_Caption1
			// 
			this.t_Caption1.Alignment = System.Drawing.StringAlignment.Center;
			this.t_Caption1.BackColor = System.Drawing.Color.Transparent;
			this.t_Caption1.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.t_Caption1.Font_Size = 14F;
			this.t_Caption1.Font_Style = System.Drawing.FontStyle.Regular;
			this.t_Caption1.FontBold = false;
			this.t_Caption1.Grid = this.t_Grid1;
			this.t_Caption1.LineAlignment = System.Drawing.StringAlignment.Center;
			this.t_Caption1.Location = new System.Drawing.Point(120, 24);
			this.t_Caption1.MaximumSize = new System.Drawing.Size(300, 45);
			this.t_Caption1.MFontIndex = 5;
			this.t_Caption1.MFontSize = 14F;
			this.t_Caption1.MinimumSize = new System.Drawing.Size(180, 45);
			this.t_Caption1.Name = "t_Caption1";
			this.t_Caption1.Size = new System.Drawing.Size(300, 45);
			this.t_Caption1.TabIndex = 2;
			this.t_Caption1.Text = "t_Caption1";
			// 
			// t_Input1
			// 
			this.t_Input1.Alignment = System.Drawing.StringAlignment.Far;
			this.t_Input1.BackColor = System.Drawing.Color.Transparent;
			this.t_Input1.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.t_Input1.Font_Size = 14F;
			this.t_Input1.Font_Style = System.Drawing.FontStyle.Regular;
			this.t_Input1.FontBold = false;
			this.t_Input1.Grid = this.t_Grid1;
			this.t_Input1.LineAlignment = System.Drawing.StringAlignment.Center;
			this.t_Input1.Location = new System.Drawing.Point(24, 44);
			this.t_Input1.MaximumSize = new System.Drawing.Size(90, 25);
			this.t_Input1.MFontIndex = 5;
			this.t_Input1.MFontSize = 14F;
			this.t_Input1.MinimumSize = new System.Drawing.Size(90, 25);
			this.t_Input1.Name = "t_Input1";
			this.t_Input1.Size = new System.Drawing.Size(90, 25);
			this.t_Input1.TabIndex = 3;
			this.t_Input1.Text = "t_Input1";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(457, 435);
			this.Controls.Add(this.t_Input1);
			this.Controls.Add(this.t_Caption1);
			this.Controls.Add(this.t_Grid1);
			this.Name = "Form1";
			this.Text = "DesignToolsServer";
			this.Controls.SetChildIndex(this.t_Grid1, 0);
			this.Controls.SetChildIndex(this.t_Caption1, 0);
			this.Controls.SetChildIndex(this.t_Input1, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private T_Grid t_Grid1;
		private T_Caption t_Caption1;
		private T_Input t_Input1;
	}
}