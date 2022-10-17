namespace AE_RemapTria
{
	partial class T_OffsetFrameDialog
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
			this.lbOffset = new AE_RemapTria.T_Label();
			this.nbOffset = new AE_RemapTria.T_NumberBox();
			this.btnOK = new AE_RemapTria.T_Button();
			this.btnCancel = new AE_RemapTria.T_Button();
			this.lbMain = new AE_RemapTria.T_Label();
			this.t_Zebra1 = new AE_RemapTria.T_Zebra();
			((System.ComponentModel.ISupportInitialize)(this.nbOffset)).BeginInit();
			this.SuspendLayout();
			// 
			// lbOffset
			// 
			this.lbOffset.Alignment = System.Drawing.StringAlignment.Near;
			this.lbOffset.BackColor = System.Drawing.Color.Transparent;
			this.lbOffset.BottomBar = new System.Drawing.Size(0, 0);
			this.lbOffset.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lbOffset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.lbOffset.LeftBar = new System.Drawing.Size(14, 14);
			this.lbOffset.LineAlignment = System.Drawing.StringAlignment.Center;
			this.lbOffset.Location = new System.Drawing.Point(79, 42);
			this.lbOffset.MyFontIndex = 5;
			this.lbOffset.MyFonts = this.t_MyFonts1;
			this.lbOffset.MyFontSize = 12F;
			this.lbOffset.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.lbOffset.Name = "lbOffset";
			this.lbOffset.RightBar = new System.Drawing.Size(0, 0);
			this.lbOffset.Size = new System.Drawing.Size(145, 31);
			this.lbOffset.TabIndex = 0;
			this.lbOffset.Text = "OffsetFrame";
			this.lbOffset.TopBar = new System.Drawing.Size(0, 0);
			// 
			// nbOffset
			// 
			this.nbOffset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(11)))), ((int)(((byte)(25)))));
			this.nbOffset.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.nbOffset.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.nbOffset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(220)))), ((int)(((byte)(250)))));
			this.nbOffset.Location = new System.Drawing.Point(230, 42);
			this.nbOffset.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
			this.nbOffset.Minimum = new decimal(new int[] {
            180,
            0,
            0,
            -2147483648});
			this.nbOffset.MyFontIndex = 5;
			this.nbOffset.MyFonts = this.t_MyFonts1;
			this.nbOffset.MyFontSize = 12F;
			this.nbOffset.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.nbOffset.Name = "nbOffset";
			this.nbOffset.Size = new System.Drawing.Size(128, 31);
			this.nbOffset.TabIndex = 1;
			// 
			// btnOK
			// 
			this.btnOK.Alignment = System.Drawing.StringAlignment.Center;
			this.btnOK.BackColor = System.Drawing.Color.Transparent;
			this.btnOK.BottomBar = 3;
			this.btnOK.Checked = false;
			this.btnOK.Color_Down = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
			this.btnOK.Color_Enter = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.btnOK.Color_line = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.btnOK.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(250)))));
			this.btnOK.Id = 0;
			this.btnOK.IsCheckMode = false;
			this.btnOK.IsDrawFrame = true;
			this.btnOK.IsMouseDown = false;
			this.btnOK.LeftBar = 0;
			this.btnOK.LineAlignment = System.Drawing.StringAlignment.Center;
			this.btnOK.Location = new System.Drawing.Point(235, 84);
			this.btnOK.MyFontIndex = 5;
			this.btnOK.MyFonts = this.t_MyFonts1;
			this.btnOK.MyFontSize = 12F;
			this.btnOK.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.btnOK.Name = "btnOK";
			this.btnOK.RightBar = 12;
			this.btnOK.Size = new System.Drawing.Size(123, 30);
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "OK";
			this.btnOK.TopBar = 0;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Alignment = System.Drawing.StringAlignment.Center;
			this.btnCancel.BackColor = System.Drawing.Color.Transparent;
			this.btnCancel.BottomBar = 3;
			this.btnCancel.Checked = false;
			this.btnCancel.Color_Down = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
			this.btnCancel.Color_Enter = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.btnCancel.Color_line = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.btnCancel.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(250)))));
			this.btnCancel.Id = 0;
			this.btnCancel.IsCheckMode = false;
			this.btnCancel.IsDrawFrame = true;
			this.btnCancel.IsMouseDown = false;
			this.btnCancel.LeftBar = 0;
			this.btnCancel.LineAlignment = System.Drawing.StringAlignment.Center;
			this.btnCancel.Location = new System.Drawing.Point(96, 84);
			this.btnCancel.MyFontIndex = 5;
			this.btnCancel.MyFonts = this.t_MyFonts1;
			this.btnCancel.MyFontSize = 12F;
			this.btnCancel.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.RightBar = 0;
			this.btnCancel.Size = new System.Drawing.Size(133, 30);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cencel";
			this.btnCancel.TopBar = 0;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// lbMain
			// 
			this.lbMain.Alignment = System.Drawing.StringAlignment.Near;
			this.lbMain.BackColor = System.Drawing.Color.Transparent;
			this.lbMain.BottomBar = new System.Drawing.Size(400, 2);
			this.lbMain.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lbMain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.lbMain.LeftBar = new System.Drawing.Size(0, 0);
			this.lbMain.LineAlignment = System.Drawing.StringAlignment.Center;
			this.lbMain.Location = new System.Drawing.Point(76, 12);
			this.lbMain.MyFontIndex = 5;
			this.lbMain.MyFonts = this.t_MyFonts1;
			this.lbMain.MyFontSize = 12F;
			this.lbMain.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.lbMain.Name = "lbMain";
			this.lbMain.RightBar = new System.Drawing.Size(0, 0);
			this.lbMain.Size = new System.Drawing.Size(279, 24);
			this.lbMain.TabIndex = 4;
			this.lbMain.Text = "Sheet Top";
			this.lbMain.TopBar = new System.Drawing.Size(0, 0);
			// 
			// t_Zebra1
			// 
			this.t_Zebra1.BackColor = System.Drawing.Color.Transparent;
			this.t_Zebra1.Location = new System.Drawing.Point(31, 19);
			this.t_Zebra1.Name = "t_Zebra1";
			this.t_Zebra1.Size = new System.Drawing.Size(39, 95);
			this.t_Zebra1.TabIndex = 5;
			this.t_Zebra1.Text = "t_Zebra1";
			this.t_Zebra1.ZebraIndex = 0;
			// 
			// T_OffsetFrameDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(389, 135);
			this.Controls.Add(this.t_Zebra1);
			this.Controls.Add(this.lbMain);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.nbOffset);
			this.Controls.Add(this.lbOffset);
			this.Edge = new System.Drawing.Rectangle(10, 20, 10, 10);
			this.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.KagiHeight = 20;
			this.KagiWeight = 2;
			this.MyFonts = this.t_MyFonts1;
			this.Name = "T_OffsetFrameDialog";
			this.Text = "T_OffsetFrameDialog";
			((System.ComponentModel.ISupportInitialize)(this.nbOffset)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private T_MyFonts t_MyFonts1;
		private T_Label lbOffset;
		private T_NumberBox nbOffset;
		private T_Button btnOK;
		private T_Button btnCancel;
		private T_Label lbMain;
		private T_Zebra t_Zebra1;
	}
}