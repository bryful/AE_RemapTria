namespace AE_RemapTria
{
	partial class T_KeyBindDialog
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
			this.t_ListBox1 = new AE_RemapTria.T_ListBox();
			this.tbJap = new AE_RemapTria.T_TextBox();
			this.btnOK = new AE_RemapTria.T_Button();
			this.btnCancel = new AE_RemapTria.T_Button();
			this.t_Label2 = new AE_RemapTria.T_Label();
			this.btnGetKey1 = new AE_RemapTria.T_Button();
			this.btnGetKey2 = new AE_RemapTria.T_Button();
			this.lbCaution = new AE_RemapTria.T_Label();
			this.t_Label3 = new AE_RemapTria.T_Label();
			this.t_Zebra1 = new AE_RemapTria.T_Zebra();
			this.t_Zebra2 = new AE_RemapTria.T_Zebra();
			this.t_ColorPlate1 = new AE_RemapTria.T_ColorPlate();
			this.SuspendLayout();
			// 
			// t_ListBox1
			// 
			this.t_ListBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(30)))));
			this.t_ListBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.t_ListBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.t_ListBox1.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.t_ListBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(220)))), ((int)(((byte)(250)))));
			this.t_ListBox1.FormattingEnabled = true;
			this.t_ListBox1.ItemHeight = 19;
			this.t_ListBox1.Items.AddRange(new object[] {
            "",
            "",
            ""});
			this.t_ListBox1.Location = new System.Drawing.Point(66, 41);
			this.t_ListBox1.MyFontIndex = 5;
			this.t_ListBox1.MyFonts = this.t_MyFonts1;
			this.t_ListBox1.MyFontSize = 10F;
			this.t_ListBox1.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.t_ListBox1.Name = "t_ListBox1";
			this.t_ListBox1.Names = new string[] {
        "",
        "",
        ""};
			this.t_ListBox1.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(100)))));
			this.t_ListBox1.Size = new System.Drawing.Size(160, 344);
			this.t_ListBox1.TabIndex = 0;
			this.t_ListBox1.SelectedIndexChanged += new System.EventHandler(this.t_ListBox1_SelectedIndexChanged);
			// 
			// tbJap
			// 
			this.tbJap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(11)))), ((int)(((byte)(25)))));
			this.tbJap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbJap.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.tbJap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(220)))), ((int)(((byte)(250)))));
			this.tbJap.Location = new System.Drawing.Point(233, 66);
			this.tbJap.MyFontIndex = 5;
			this.tbJap.MyFonts = this.t_MyFonts1;
			this.tbJap.MyFontSize = 10F;
			this.tbJap.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.tbJap.Name = "tbJap";
			this.tbJap.Size = new System.Drawing.Size(158, 27);
			this.tbJap.TabIndex = 5;
			this.tbJap.Text = "456";
			// 
			// btnOK
			// 
			this.btnOK.Alignment = System.Drawing.StringAlignment.Center;
			this.btnOK.BackColor = System.Drawing.Color.Transparent;
			this.btnOK.BottomBar = 2;
			this.btnOK.Checked = false;
			this.btnOK.Color_Down = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
			this.btnOK.Color_Enter = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.btnOK.Color_line = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.btnOK.Enabled = false;
			this.btnOK.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(250)))));
			this.btnOK.Id = 0;
			this.btnOK.IsCheckMode = false;
			this.btnOK.IsDrawFrame = true;
			this.btnOK.IsMouseDown = false;
			this.btnOK.LeftBar = 0;
			this.btnOK.LineAlignment = System.Drawing.StringAlignment.Center;
			this.btnOK.Location = new System.Drawing.Point(234, 351);
			this.btnOK.MyFontIndex = 5;
			this.btnOK.MyFonts = this.t_MyFonts1;
			this.btnOK.MyFontSize = 12F;
			this.btnOK.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.btnOK.Name = "btnOK";
			this.btnOK.RightBar = 8;
			this.btnOK.Size = new System.Drawing.Size(158, 34);
			this.btnOK.TabIndex = 6;
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
			this.btnCancel.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(250)))));
			this.btnCancel.Id = 0;
			this.btnCancel.IsCheckMode = false;
			this.btnCancel.IsDrawFrame = true;
			this.btnCancel.IsMouseDown = false;
			this.btnCancel.LeftBar = 0;
			this.btnCancel.LineAlignment = System.Drawing.StringAlignment.Center;
			this.btnCancel.Location = new System.Drawing.Point(233, 311);
			this.btnCancel.MyFontIndex = 5;
			this.btnCancel.MyFonts = this.t_MyFonts1;
			this.btnCancel.MyFontSize = 12F;
			this.btnCancel.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.RightBar = 8;
			this.btnCancel.Size = new System.Drawing.Size(158, 34);
			this.btnCancel.TabIndex = 7;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.TopBar = 2;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// t_Label2
			// 
			this.t_Label2.Alignment = System.Drawing.StringAlignment.Near;
			this.t_Label2.BackColor = System.Drawing.Color.Transparent;
			this.t_Label2.BottomBar = new System.Drawing.Size(0, 0);
			this.t_Label2.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.t_Label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.t_Label2.LeftBar = new System.Drawing.Size(14, 14);
			this.t_Label2.LineAlignment = System.Drawing.StringAlignment.Center;
			this.t_Label2.Location = new System.Drawing.Point(233, 41);
			this.t_Label2.MyFontIndex = 5;
			this.t_Label2.MyFonts = this.t_MyFonts1;
			this.t_Label2.MyFontSize = 10F;
			this.t_Label2.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.t_Label2.Name = "t_Label2";
			this.t_Label2.RightBar = new System.Drawing.Size(0, 0);
			this.t_Label2.Size = new System.Drawing.Size(173, 24);
			this.t_Label2.TabIndex = 8;
			this.t_Label2.Text = "Jananese";
			this.t_Label2.TopBar = new System.Drawing.Size(0, 0);
			// 
			// btnGetKey1
			// 
			this.btnGetKey1.Alignment = System.Drawing.StringAlignment.Center;
			this.btnGetKey1.BackColor = System.Drawing.Color.Transparent;
			this.btnGetKey1.BottomBar = 0;
			this.btnGetKey1.Checked = false;
			this.btnGetKey1.Color_Down = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
			this.btnGetKey1.Color_Enter = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.btnGetKey1.Color_line = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.btnGetKey1.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnGetKey1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(250)))));
			this.btnGetKey1.Id = 0;
			this.btnGetKey1.IsCheckMode = false;
			this.btnGetKey1.IsDrawFrame = true;
			this.btnGetKey1.IsMouseDown = false;
			this.btnGetKey1.LeftBar = 12;
			this.btnGetKey1.LineAlignment = System.Drawing.StringAlignment.Center;
			this.btnGetKey1.Location = new System.Drawing.Point(233, 146);
			this.btnGetKey1.MyFontIndex = 5;
			this.btnGetKey1.MyFonts = this.t_MyFonts1;
			this.btnGetKey1.MyFontSize = 9F;
			this.btnGetKey1.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.btnGetKey1.Name = "btnGetKey1";
			this.btnGetKey1.RightBar = 0;
			this.btnGetKey1.Size = new System.Drawing.Size(159, 36);
			this.btnGetKey1.TabIndex = 9;
			this.btnGetKey1.Text = "None";
			this.btnGetKey1.TopBar = 0;
			this.btnGetKey1.Click += new System.EventHandler(this.btnGetKey1_Click);
			// 
			// btnGetKey2
			// 
			this.btnGetKey2.Alignment = System.Drawing.StringAlignment.Center;
			this.btnGetKey2.BackColor = System.Drawing.Color.Transparent;
			this.btnGetKey2.BottomBar = 0;
			this.btnGetKey2.Checked = false;
			this.btnGetKey2.Color_Down = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
			this.btnGetKey2.Color_Enter = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.btnGetKey2.Color_line = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.btnGetKey2.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnGetKey2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(250)))));
			this.btnGetKey2.Id = 1;
			this.btnGetKey2.IsCheckMode = false;
			this.btnGetKey2.IsDrawFrame = true;
			this.btnGetKey2.IsMouseDown = false;
			this.btnGetKey2.LeftBar = 12;
			this.btnGetKey2.LineAlignment = System.Drawing.StringAlignment.Center;
			this.btnGetKey2.Location = new System.Drawing.Point(232, 188);
			this.btnGetKey2.MyFontIndex = 5;
			this.btnGetKey2.MyFonts = this.t_MyFonts1;
			this.btnGetKey2.MyFontSize = 9F;
			this.btnGetKey2.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.btnGetKey2.Name = "btnGetKey2";
			this.btnGetKey2.RightBar = 0;
			this.btnGetKey2.Size = new System.Drawing.Size(160, 36);
			this.btnGetKey2.TabIndex = 10;
			this.btnGetKey2.Text = "None";
			this.btnGetKey2.TopBar = 0;
			this.btnGetKey2.Click += new System.EventHandler(this.btnGetKey1_Click);
			// 
			// lbCaution
			// 
			this.lbCaution.Alignment = System.Drawing.StringAlignment.Near;
			this.lbCaution.BackColor = System.Drawing.Color.Transparent;
			this.lbCaution.BottomBar = new System.Drawing.Size(0, 0);
			this.lbCaution.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lbCaution.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.lbCaution.LeftBar = new System.Drawing.Size(14, 14);
			this.lbCaution.LineAlignment = System.Drawing.StringAlignment.Center;
			this.lbCaution.Location = new System.Drawing.Point(232, 230);
			this.lbCaution.MyFontIndex = 5;
			this.lbCaution.MyFonts = this.t_MyFonts1;
			this.lbCaution.MyFontSize = 7F;
			this.lbCaution.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.lbCaution.Name = "lbCaution";
			this.lbCaution.RightBar = new System.Drawing.Size(0, 0);
			this.lbCaution.Size = new System.Drawing.Size(159, 24);
			this.lbCaution.TabIndex = 11;
			this.lbCaution.Text = "Please,Push Key. MouseClick Exit";
			this.lbCaution.TopBar = new System.Drawing.Size(0, 0);
			this.lbCaution.Visible = false;
			// 
			// t_Label3
			// 
			this.t_Label3.Alignment = System.Drawing.StringAlignment.Near;
			this.t_Label3.BackColor = System.Drawing.Color.Transparent;
			this.t_Label3.BottomBar = new System.Drawing.Size(0, 0);
			this.t_Label3.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.t_Label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.t_Label3.LeftBar = new System.Drawing.Size(14, 14);
			this.t_Label3.LineAlignment = System.Drawing.StringAlignment.Center;
			this.t_Label3.Location = new System.Drawing.Point(232, 118);
			this.t_Label3.MyFontIndex = 5;
			this.t_Label3.MyFonts = this.t_MyFonts1;
			this.t_Label3.MyFontSize = 10F;
			this.t_Label3.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.t_Label3.Name = "t_Label3";
			this.t_Label3.RightBar = new System.Drawing.Size(0, 0);
			this.t_Label3.Size = new System.Drawing.Size(159, 24);
			this.t_Label3.TabIndex = 12;
			this.t_Label3.Text = "Short cut key";
			this.t_Label3.TopBar = new System.Drawing.Size(0, 0);
			// 
			// t_Zebra1
			// 
			this.t_Zebra1.BackColor = System.Drawing.Color.Transparent;
			this.t_Zebra1.Location = new System.Drawing.Point(232, 260);
			this.t_Zebra1.Name = "t_Zebra1";
			this.t_Zebra1.Size = new System.Drawing.Size(160, 45);
			this.t_Zebra1.TabIndex = 13;
			this.t_Zebra1.Text = "t_Zebra1";
			this.t_Zebra1.ZebraIndex = AE_RemapTria.ZEBRA_TYPE.DARKBLUE;
			// 
			// t_Zebra2
			// 
			this.t_Zebra2.BackColor = System.Drawing.Color.Transparent;
			this.t_Zebra2.Location = new System.Drawing.Point(33, 41);
			this.t_Zebra2.Name = "t_Zebra2";
			this.t_Zebra2.Size = new System.Drawing.Size(27, 344);
			this.t_Zebra2.TabIndex = 14;
			this.t_Zebra2.Text = "t_Zebra2";
			this.t_Zebra2.ZebraIndex = AE_RemapTria.ZEBRA_TYPE.DARKYELLOW;
			// 
			// t_ColorPlate1
			// 
			this.t_ColorPlate1.Alignment = System.Drawing.StringAlignment.Far;
			this.t_ColorPlate1.BackColor = System.Drawing.Color.Transparent;
			this.t_ColorPlate1.ColorPlate = AE_RemapTria.ColorPlate.Fill;
			this.t_ColorPlate1.Font = new System.Drawing.Font("Yu Gothic UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.t_ColorPlate1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(155)))));
			this.t_ColorPlate1.KagiHWeight = 10;
			this.t_ColorPlate1.KagiVWeight = 10;
			this.t_ColorPlate1.LineAlignment = System.Drawing.StringAlignment.Center;
			this.t_ColorPlate1.Location = new System.Drawing.Point(33, 12);
			this.t_ColorPlate1.MyFontIndex = 5;
			this.t_ColorPlate1.MyFonts = null;
			this.t_ColorPlate1.MyFontSize = 10F;
			this.t_ColorPlate1.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.t_ColorPlate1.Name = "t_ColorPlate1";
			this.t_ColorPlate1.Opacity = 128;
			this.t_ColorPlate1.Size = new System.Drawing.Size(373, 23);
			this.t_ColorPlate1.TabIndex = 15;
			this.t_ColorPlate1.Text = "t_ColorPlate1";
			// 
			// T_KeyBindDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(433, 406);
			this.Controls.Add(this.t_ColorPlate1);
			this.Controls.Add(this.t_Zebra2);
			this.Controls.Add(this.t_Zebra1);
			this.Controls.Add(this.t_Label3);
			this.Controls.Add(this.lbCaution);
			this.Controls.Add(this.btnGetKey2);
			this.Controls.Add(this.btnGetKey1);
			this.Controls.Add(this.t_Label2);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.tbJap);
			this.Controls.Add(this.t_ListBox1);
			this.Edge = new System.Drawing.Rectangle(10, 10, 10, 10);
			this.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.KagiHeight = 20;
			this.KagiWeight = 2;
			this.KeyPreview = true;
			this.MyFonts = this.t_MyFonts1;
			this.MyFontSize = 10F;
			this.Name = "T_KeyBindDialog";
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
			this.TBCenterLength = 0;
			this.TBCenterWeight = 0;
			this.TBCount = new int[] {
        0,
        0,
        0};
			this.TBInter = new int[] {
        10,
        20,
        120};
			this.TBLength = new int[] {
        5,
        12,
        8};
			this.TBWeight = new int[] {
        1,
        2,
        4};
			this.Text = "T_KeyBindDialog";
			this.TransparencyKey = System.Drawing.Color.DarkGray;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private T_MyFonts t_MyFonts1;
		private T_ListBox t_ListBox1;
		private T_TextBox tbJap;
		private T_Button btnOK;
		private T_Button btnCancel;
		private T_Label t_Label2;
		private T_Button btnGetKey1;
		private T_Button btnGetKey2;
		private T_Label lbCaution;
		private T_Label t_Label3;
		private T_Zebra t_Zebra1;
		private T_Zebra t_Zebra2;
		private T_ColorPlate t_ColorPlate1;
		private TextBox textBox1;
	}
}