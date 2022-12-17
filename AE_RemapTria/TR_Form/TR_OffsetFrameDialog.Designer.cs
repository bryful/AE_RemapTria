

namespace AE_RemapTria
{
    partial class TR_OffsetFrameDialog
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
			this.lbOffset = new AE_RemapTria.TR_Label();
			this.nbOffset = new AE_RemapTria.TR_NumberBox();
			this.btnOK = new AE_RemapTria.TR_Button();
			this.btnCancel = new AE_RemapTria.TR_Button();
			this.lbMain = new AE_RemapTria.TR_Label();
			this.t_Zebra1 = new AE_RemapTria.TR_Zebra();
			this.SuspendLayout();
			// 
			// lbOffset
			// 
			this.lbOffset.Alignment = System.Drawing.StringAlignment.Near;
			this.lbOffset.Back = System.Drawing.Color.Transparent;
			this.lbOffset.BackColor = System.Drawing.Color.Transparent;
			this.lbOffset.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lbOffset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.lbOffset.Frame = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
			this.lbOffset.FrameMargin = new System.Windows.Forms.Padding(0);
			this.lbOffset.FrameWeight = new System.Windows.Forms.Padding(8, 0, 0, 0);
			this.lbOffset.LineAlignment = System.Drawing.StringAlignment.Center;
			this.lbOffset.Location = new System.Drawing.Point(93, 60);
			this.lbOffset.MyFontIndex = 5;
			this.lbOffset.MyFontSize = 9F;
			this.lbOffset.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.lbOffset.Name = "lbOffset";
			this.lbOffset.Size = new System.Drawing.Size(117, 31);
			this.lbOffset.TabIndex = 0;
			this.lbOffset.TabStop = false;
			this.lbOffset.Text = "OffsetFrame";
			// 
			// nbOffset
			// 
			this.nbOffset.Alignment = System.Drawing.StringAlignment.Far;
			this.nbOffset.Back = System.Drawing.Color.Transparent;
			this.nbOffset.BackColor = System.Drawing.Color.Transparent;
			this.nbOffset.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.nbOffset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(220)))), ((int)(((byte)(250)))));
			this.nbOffset.Frame = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(150)))));
			this.nbOffset.FrameMargin = new System.Windows.Forms.Padding(0);
			this.nbOffset.FrameWeight = new System.Windows.Forms.Padding(0, 0, 0, 1);
			this.nbOffset.LineAlignment = System.Drawing.StringAlignment.Center;
			this.nbOffset.Location = new System.Drawing.Point(216, 60);
			this.nbOffset.MyFontIndex = 5;
			this.nbOffset.MyFontSize = 12F;
			this.nbOffset.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.nbOffset.Name = "nbOffset";
			this.nbOffset.Size = new System.Drawing.Size(128, 25);
			this.nbOffset.TabIndex = 1;
			this.nbOffset.Value = 0;
			this.nbOffset.ValueMax = 32000;
			this.nbOffset.ValueMin = 0;
			// 
			// btnOK
			// 
			this.btnOK.Alignment = System.Drawing.StringAlignment.Center;
			this.btnOK.Back = System.Drawing.Color.Transparent;
			this.btnOK.BackColor = System.Drawing.Color.Transparent;
			this.btnOK.Checked = false;
			this.btnOK.Color_Down = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
			this.btnOK.Color_Enter = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.btnOK.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(250)))));
			this.btnOK.Frame = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
			this.btnOK.FrameMargin = new System.Windows.Forms.Padding(0, 0, 3, 3);
			this.btnOK.FrameWeight = new System.Windows.Forms.Padding(0, 0, 10, 1);
			this.btnOK.Group = 0;
			this.btnOK.Id = 0;
			this.btnOK.IsCheckMode = false;
			this.btnOK.IsDrawFrame = true;
			this.btnOK.IsMouseDown = false;
			this.btnOK.LineAlignment = System.Drawing.StringAlignment.Center;
			this.btnOK.Location = new System.Drawing.Point(244, 106);
			this.btnOK.MyFontIndex = 5;
			this.btnOK.MyFontSize = 12F;
			this.btnOK.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(100, 30);
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Alignment = System.Drawing.StringAlignment.Center;
			this.btnCancel.Back = System.Drawing.Color.Transparent;
			this.btnCancel.BackColor = System.Drawing.Color.Transparent;
			this.btnCancel.Checked = false;
			this.btnCancel.Color_Down = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
			this.btnCancel.Color_Enter = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.btnCancel.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(250)))));
			this.btnCancel.Frame = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
			this.btnCancel.FrameMargin = new System.Windows.Forms.Padding(3, 0, 0, 3);
			this.btnCancel.FrameWeight = new System.Windows.Forms.Padding(10, 0, 0, 1);
			this.btnCancel.Group = 0;
			this.btnCancel.Id = 0;
			this.btnCancel.IsCheckMode = false;
			this.btnCancel.IsDrawFrame = true;
			this.btnCancel.IsMouseDown = false;
			this.btnCancel.LineAlignment = System.Drawing.StringAlignment.Center;
			this.btnCancel.Location = new System.Drawing.Point(126, 106);
			this.btnCancel.MyFontIndex = 5;
			this.btnCancel.MyFontSize = 12F;
			this.btnCancel.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(100, 30);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cencel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// lbMain
			// 
			this.lbMain.Alignment = System.Drawing.StringAlignment.Near;
			this.lbMain.Back = System.Drawing.Color.Transparent;
			this.lbMain.BackColor = System.Drawing.Color.Transparent;
			this.lbMain.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lbMain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.lbMain.Frame = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
			this.lbMain.FrameMargin = new System.Windows.Forms.Padding(0);
			this.lbMain.FrameWeight = new System.Windows.Forms.Padding(12, 0, 0, 1);
			this.lbMain.LineAlignment = System.Drawing.StringAlignment.Center;
			this.lbMain.Location = new System.Drawing.Point(76, 19);
			this.lbMain.MyFontIndex = 5;
			this.lbMain.MyFontSize = 12F;
			this.lbMain.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.lbMain.Name = "lbMain";
			this.lbMain.Size = new System.Drawing.Size(279, 24);
			this.lbMain.TabIndex = 4;
			this.lbMain.TabStop = false;
			this.lbMain.Text = "Sheet Top";
			// 
			// t_Zebra1
			// 
			this.t_Zebra1.Alignment = System.Drawing.StringAlignment.Far;
			this.t_Zebra1.Back = System.Drawing.Color.Transparent;
			this.t_Zebra1.BackColor = System.Drawing.Color.Transparent;
			this.t_Zebra1.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.t_Zebra1.Frame = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
			this.t_Zebra1.FrameMargin = new System.Windows.Forms.Padding(0);
			this.t_Zebra1.FrameWeight = new System.Windows.Forms.Padding(0);
			this.t_Zebra1.LineAlignment = System.Drawing.StringAlignment.Center;
			this.t_Zebra1.Location = new System.Drawing.Point(31, 19);
			this.t_Zebra1.MyFontIndex = 5;
			this.t_Zebra1.MyFontSize = 5F;
			this.t_Zebra1.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.t_Zebra1.Name = "t_Zebra1";
			this.t_Zebra1.Size = new System.Drawing.Size(39, 129);
			this.t_Zebra1.TabIndex = 5;
			this.t_Zebra1.TabStop = false;
			this.t_Zebra1.Text = "t_Zebra1";
			this.t_Zebra1.ZebraColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(110)))));
			this.t_Zebra1.ZebraRot = 45F;
			this.t_Zebra1.ZebraWidth = 15F;
			// 
			// T_OffsetFrameDialog
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(389, 160);
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
			this.MyFontSize = 9F;
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
			this.ResumeLayout(false);

		}

		#endregion

		private TR_MyFonts t_MyFonts1;
		private TR_Label lbOffset;
		private TR_NumberBox nbOffset;
		private TR_Button btnOK;
		private TR_Button btnCancel;
		private TR_Label lbMain;
		private TR_Zebra t_Zebra1;
	}
}