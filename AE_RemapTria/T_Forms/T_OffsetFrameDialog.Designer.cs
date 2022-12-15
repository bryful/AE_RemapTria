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
			this.lbOffset = new AE_RemapTria.TR_Label();
			this.nbOffset = new AE_RemapTria.T_NumberBox();
			this.btnOK = new AE_RemapTria.TR_Button();
			this.btnCancel = new AE_RemapTria.TR_Button();
			this.lbMain = new AE_RemapTria.TR_Label();
			this.t_Zebra1 = new AE_RemapTria.TR_Zebra();
			this.t_Arrow1 = new AE_RemapTria.TR_Arrow();
			((System.ComponentModel.ISupportInitialize)(this.nbOffset)).BeginInit();
			this.SuspendLayout();
			// 
			// lbOffset
			// 
			this.lbOffset.Alignment = System.Drawing.StringAlignment.Near;
			this.lbOffset.BackColor = System.Drawing.Color.Transparent;
			this.lbOffset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.lbOffset.LineAlignment = System.Drawing.StringAlignment.Center;
			this.lbOffset.Location = new System.Drawing.Point(79, 42);
			this.lbOffset.MyFontIndex = 5;
			this.lbOffset.MyFontSize = 12F;
			this.lbOffset.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.lbOffset.Name = "lbOffset";
			this.lbOffset.Size = new System.Drawing.Size(145, 31);
			this.lbOffset.TabIndex = 0;
			this.lbOffset.Text = "OffsetFrame";
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
			this.btnOK.Checked = false;
			this.btnOK.Color_Down = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
			this.btnOK.Color_Enter = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.btnOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(250)))));
			this.btnOK.Id = 0;
			this.btnOK.IsCheckMode = false;
			this.btnOK.IsDrawFrame = true;
			this.btnOK.IsMouseDown = false;
			this.btnOK.LineAlignment = System.Drawing.StringAlignment.Center;
			this.btnOK.Location = new System.Drawing.Point(235, 84);
			this.btnOK.MyFontIndex = 5;
			this.btnOK.MyFontSize = 12F;
			this.btnOK.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(123, 30);
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Alignment = System.Drawing.StringAlignment.Center;
			this.btnCancel.BackColor = System.Drawing.Color.Transparent;
			this.btnCancel.Checked = false;
			this.btnCancel.Color_Down = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
			this.btnCancel.Color_Enter = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(250)))));
			this.btnCancel.Id = 0;
			this.btnCancel.IsCheckMode = false;
			this.btnCancel.IsDrawFrame = true;
			this.btnCancel.IsMouseDown = false;
			this.btnCancel.LineAlignment = System.Drawing.StringAlignment.Center;
			this.btnCancel.Location = new System.Drawing.Point(96, 84);
			this.btnCancel.MyFontIndex = 5;
			this.btnCancel.MyFontSize = 12F;
			this.btnCancel.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(133, 30);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cencel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// lbMain
			// 
			this.lbMain.Alignment = System.Drawing.StringAlignment.Near;
			this.lbMain.BackColor = System.Drawing.Color.Transparent;
			this.lbMain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.lbMain.LineAlignment = System.Drawing.StringAlignment.Center;
			this.lbMain.Location = new System.Drawing.Point(76, 12);
			this.lbMain.MyFontIndex = 5;
			this.lbMain.MyFontSize = 12F;
			this.lbMain.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.lbMain.Name = "lbMain";
			this.lbMain.Size = new System.Drawing.Size(279, 24);
			this.lbMain.TabIndex = 4;
			this.lbMain.Text = "Sheet Top";
			// 
			// t_Zebra1
			// 
			this.t_Zebra1.BackColor = System.Drawing.Color.Transparent;
			this.t_Zebra1.Location = new System.Drawing.Point(31, 19);
			this.t_Zebra1.Name = "t_Zebra1";
			this.t_Zebra1.Size = new System.Drawing.Size(39, 95);
			this.t_Zebra1.TabIndex = 5;
			this.t_Zebra1.Text = "t_Zebra1";
			// 
			// t_Arrow1
			// 
			this.t_Arrow1.Alignment = System.Drawing.StringAlignment.Far;
			this.t_Arrow1.ArrowDir = BRY.ArrowDir.Right;
			this.t_Arrow1.BackColor = System.Drawing.Color.Transparent;
			this.t_Arrow1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(250)))));
			this.t_Arrow1.IsCut = false;
			this.t_Arrow1.LineAlignment = System.Drawing.StringAlignment.Center;
			this.t_Arrow1.Location = new System.Drawing.Point(362, 47);
			this.t_Arrow1.MyFontIndex = 5;
			this.t_Arrow1.MyFontSize = 9F;
			this.t_Arrow1.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.t_Arrow1.Name = "t_Arrow1";
			this.t_Arrow1.Size = new System.Drawing.Size(15, 20);
			this.t_Arrow1.TabIndex = 6;
			this.t_Arrow1.Text = "t_Arrow1";
			// 
			// T_OffsetFrameDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(389, 135);
			this.Controls.Add(this.t_Arrow1);
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
			this.ScaleColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
			this.SideCenterLength = 12;
			this.SideCenterWeight = 0;
			this.SideCount = new int[] {
        0,
        0,
        0};
			this.SideInter = new int[] {
        10,
        20,
        120};
			this.SideLength = new int[] {
        5,
        12,
        8};
			this.SideWeight = new int[] {
        1,
        2,
        4};
			this.TBCenterLength = 8;
			this.TBCount = new int[] {
        2,
        2,
        0};
			this.TBInter = new int[] {
        35,
        70,
        120};
			this.TBLength = new int[] {
        3,
        6,
        8};
			this.TBWeight = new int[] {
        1,
        2,
        4};
			this.Text = "T_OffsetFrameDialog";
			((System.ComponentModel.ISupportInitialize)(this.nbOffset)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private T_MyFonts t_MyFonts1;
		private TR_Label lbOffset;
		private T_NumberBox nbOffset;
		private TR_Button btnOK;
		private TR_Button btnCancel;
		private TR_Label lbMain;
		private TR_Zebra t_Zebra1;
		private TR_Arrow t_Arrow1;
	}
}