namespace AE_RemapTria
{
	partial class T_NameDialog
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
			this.components = new System.ComponentModel.Container();
			this.t_MyFonts1 = new AE_RemapTria.T_MyFonts(this.components);
			this.lbCaption = new AE_RemapTria.T_Label();
			this.tbCaption = new AE_RemapTria.T_TextBox();
			this.btnOK = new AE_RemapTria.T_Button();
			this.btnCancel = new AE_RemapTria.T_Button();
			this.t_Zebra1 = new AE_RemapTria.T_Zebra();
			this.SuspendLayout();
			// 
			// lbCaption
			// 
			this.lbCaption.Alignment = System.Drawing.StringAlignment.Near;
			this.lbCaption.BackColor = System.Drawing.Color.Transparent;
			this.lbCaption.BottomBar = new System.Drawing.Size(270, 1);
			this.lbCaption.Font = new System.Drawing.Font("源ノ角ゴシック Code JP EL", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lbCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.lbCaption.LeftBar = new System.Drawing.Size(15, 15);
			this.lbCaption.LineAlignment = System.Drawing.StringAlignment.Center;
			this.lbCaption.Location = new System.Drawing.Point(37, 35);
			this.lbCaption.MyFontIndex = 0;
			this.lbCaption.MyFonts = this.t_MyFonts1;
			this.lbCaption.MyFontSize = 9F;
			this.lbCaption.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.lbCaption.Name = "lbCaption";
			this.lbCaption.RightBar = new System.Drawing.Size(0, 0);
			this.lbCaption.Size = new System.Drawing.Size(218, 26);
			this.lbCaption.TabIndex = 0;
			this.lbCaption.Text = "シート名を入力してください。";
			this.lbCaption.TopBar = new System.Drawing.Size(0, 0);
			// 
			// tbCaption
			// 
			this.tbCaption.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(11)))), ((int)(((byte)(25)))));
			this.tbCaption.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbCaption.Font = new System.Drawing.Font("源ノ角ゴシック Code JP EL", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.tbCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(220)))), ((int)(((byte)(250)))));
			this.tbCaption.Location = new System.Drawing.Point(53, 67);
			this.tbCaption.MyFontIndex = 0;
			this.tbCaption.MyFonts = this.t_MyFonts1;
			this.tbCaption.MyFontSize = 12F;
			this.tbCaption.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.tbCaption.Name = "tbCaption";
			this.tbCaption.Size = new System.Drawing.Size(202, 31);
			this.tbCaption.TabIndex = 1;
			this.tbCaption.Text = "AAAA";
			// 
			// btnOK
			// 
			this.btnOK.Alignment = System.Drawing.StringAlignment.Center;
			this.btnOK.BackColor = System.Drawing.Color.Transparent;
			this.btnOK.BottomBar = 0;
			this.btnOK.Checked = false;
			this.btnOK.Color_Down = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
			this.btnOK.Color_Enter = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.btnOK.Color_line = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.btnOK.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(250)))));
			this.btnOK.Id = 0;
			this.btnOK.IsCheckMode = false;
			this.btnOK.IsDrawFrame = true;
			this.btnOK.IsMouseDown = false;
			this.btnOK.LeftBar = 0;
			this.btnOK.LineAlignment = System.Drawing.StringAlignment.Center;
			this.btnOK.Location = new System.Drawing.Point(164, 118);
			this.btnOK.MyFontIndex = 5;
			this.btnOK.MyFonts = this.t_MyFonts1;
			this.btnOK.MyFontSize = 9F;
			this.btnOK.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.btnOK.Name = "btnOK";
			this.btnOK.RightBar = 6;
			this.btnOK.Size = new System.Drawing.Size(100, 30);
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "OK";
			this.btnOK.TopBar = 0;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Alignment = System.Drawing.StringAlignment.Center;
			this.btnCancel.BackColor = System.Drawing.Color.Transparent;
			this.btnCancel.BottomBar = 0;
			this.btnCancel.Checked = false;
			this.btnCancel.Color_Down = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
			this.btnCancel.Color_Enter = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.btnCancel.Color_line = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.btnCancel.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(250)))));
			this.btnCancel.Id = 0;
			this.btnCancel.IsCheckMode = false;
			this.btnCancel.IsDrawFrame = true;
			this.btnCancel.IsMouseDown = false;
			this.btnCancel.LeftBar = 8;
			this.btnCancel.LineAlignment = System.Drawing.StringAlignment.Center;
			this.btnCancel.Location = new System.Drawing.Point(48, 118);
			this.btnCancel.MyFontIndex = 5;
			this.btnCancel.MyFonts = this.t_MyFonts1;
			this.btnCancel.MyFontSize = 9F;
			this.btnCancel.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.RightBar = 0;
			this.btnCancel.Size = new System.Drawing.Size(110, 30);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.TopBar = 0;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// t_Zebra1
			// 
			this.t_Zebra1.BackColor = System.Drawing.Color.Transparent;
			this.t_Zebra1.Location = new System.Drawing.Point(37, 16);
			this.t_Zebra1.Name = "t_Zebra1";
			this.t_Zebra1.Size = new System.Drawing.Size(227, 13);
			this.t_Zebra1.TabIndex = 4;
			this.t_Zebra1.Text = "t_Zebra1";
			this.t_Zebra1.ZebraIndex = 4;
			// 
			// T_NameDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(298, 160);
			this.Controls.Add(this.t_Zebra1);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.tbCaption);
			this.Controls.Add(this.lbCaption);
			this.Edge = new System.Drawing.Rectangle(10, 10, 10, 10);
			this.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.KagiHeight = 30;
			this.KagiWeight = 2;
			this.KagiWidth = 40;
			this.KeyPreview = true;
			this.MyFonts = this.t_MyFonts1;
			this.Name = "T_NameDialog";
			this.Text = "T_NameDialog";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private T_MyFonts t_MyFonts1;
		private T_Label lbCaption;
		private T_TextBox tbCaption;
		private T_Button btnOK;
		private T_Button btnCancel;
		private T_Zebra t_Zebra1;
	}
}