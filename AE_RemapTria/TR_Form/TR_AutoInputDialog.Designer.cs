
namespace AE_RemapTria
{
    partial class TR_AutoInputDialog
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
			this.nbStart = new AE_RemapTria.TR_NumberBox();
			this.lbStart = new AE_RemapTria.TR_Label();
			this.lbEnd = new AE_RemapTria.TR_Label();
			this.nbLast = new AE_RemapTria.TR_NumberBox();
			this.tbKoma = new AE_RemapTria.TR_Label();
			this.nbKoma = new AE_RemapTria.TR_NumberBox();
			this.btnCancel = new AE_RemapTria.TR_Button();
			this.btnOK = new AE_RemapTria.TR_Button();
			this.t_Zebra1 = new AE_RemapTria.TR_Zebra();
			this.t_Zebra2 = new AE_RemapTria.TR_Zebra();
			this.lbCaption = new AE_RemapTria.TR_Label();
			this.t_ColorPlate1 = new AE_RemapTria.TR_ColorPlate();
			this.t_ColorPlate2 = new AE_RemapTria.TR_ColorPlate();
			this.SuspendLayout();
			// 
			// nbStart
			// 
			this.nbStart.Alignment = System.Drawing.StringAlignment.Far;
			this.nbStart.Back = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(11)))), ((int)(((byte)(25)))));
			this.nbStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(11)))), ((int)(((byte)(25)))));
			this.nbStart.CanReturnEdit = true;
			this.nbStart.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.nbStart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(220)))), ((int)(((byte)(250)))));
			this.nbStart.Frame = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
			this.nbStart.FrameMargin = new System.Windows.Forms.Padding(0);
			this.nbStart.FrameWeight = new System.Windows.Forms.Padding(1);
			this.nbStart.LineAlignment = System.Drawing.StringAlignment.Center;
			this.nbStart.Location = new System.Drawing.Point(178, 77);
			this.nbStart.MyFontIndex = 5;
			this.nbStart.MyFontSize = 12F;
			this.nbStart.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.nbStart.Name = "nbStart";
			this.nbStart.Size = new System.Drawing.Size(90, 31);
			this.nbStart.TabIndex = 6;
			this.nbStart.Value = 0;
			this.nbStart.ValueMax = 32000;
			this.nbStart.ValueMin = 0;
			// 
			// lbStart
			// 
			this.lbStart.Alignment = System.Drawing.StringAlignment.Near;
			this.lbStart.Back = System.Drawing.Color.Transparent;
			this.lbStart.BackColor = System.Drawing.Color.Transparent;
			this.lbStart.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lbStart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.lbStart.Frame = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
			this.lbStart.FrameMargin = new System.Windows.Forms.Padding(0);
			this.lbStart.FrameWeight = new System.Windows.Forms.Padding(8, 0, 0, 0);
			this.lbStart.LineAlignment = System.Drawing.StringAlignment.Center;
			this.lbStart.Location = new System.Drawing.Point(34, 76);
			this.lbStart.MyFontIndex = 5;
			this.lbStart.MyFontSize = 10F;
			this.lbStart.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.lbStart.Name = "lbStart";
			this.lbStart.Size = new System.Drawing.Size(122, 32);
			this.lbStart.TabIndex = 3;
			this.lbStart.TabStop = false;
			this.lbStart.Text = "StartNumber";
			// 
			// lbEnd
			// 
			this.lbEnd.Alignment = System.Drawing.StringAlignment.Near;
			this.lbEnd.Back = System.Drawing.Color.Transparent;
			this.lbEnd.BackColor = System.Drawing.Color.Transparent;
			this.lbEnd.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lbEnd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.lbEnd.Frame = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
			this.lbEnd.FrameMargin = new System.Windows.Forms.Padding(0);
			this.lbEnd.FrameWeight = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.lbEnd.LineAlignment = System.Drawing.StringAlignment.Center;
			this.lbEnd.Location = new System.Drawing.Point(34, 113);
			this.lbEnd.MyFontIndex = 5;
			this.lbEnd.MyFontSize = 10F;
			this.lbEnd.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.lbEnd.Name = "lbEnd";
			this.lbEnd.Size = new System.Drawing.Size(110, 32);
			this.lbEnd.TabIndex = 4;
			this.lbEnd.TabStop = false;
			this.lbEnd.Text = "LastNumber";
			// 
			// nbLast
			// 
			this.nbLast.Alignment = System.Drawing.StringAlignment.Far;
			this.nbLast.Back = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(11)))), ((int)(((byte)(25)))));
			this.nbLast.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(11)))), ((int)(((byte)(25)))));
			this.nbLast.CanReturnEdit = true;
			this.nbLast.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.nbLast.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(220)))), ((int)(((byte)(250)))));
			this.nbLast.Frame = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
			this.nbLast.FrameMargin = new System.Windows.Forms.Padding(0);
			this.nbLast.FrameWeight = new System.Windows.Forms.Padding(0);
			this.nbLast.LineAlignment = System.Drawing.StringAlignment.Center;
			this.nbLast.Location = new System.Drawing.Point(178, 113);
			this.nbLast.MyFontIndex = 5;
			this.nbLast.MyFontSize = 12F;
			this.nbLast.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.nbLast.Name = "nbLast";
			this.nbLast.Size = new System.Drawing.Size(90, 31);
			this.nbLast.TabIndex = 7;
			this.nbLast.Value = 0;
			this.nbLast.ValueMax = 32000;
			this.nbLast.ValueMin = 0;
			// 
			// tbKoma
			// 
			this.tbKoma.Alignment = System.Drawing.StringAlignment.Near;
			this.tbKoma.Back = System.Drawing.Color.Transparent;
			this.tbKoma.BackColor = System.Drawing.Color.Transparent;
			this.tbKoma.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.tbKoma.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.tbKoma.Frame = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
			this.tbKoma.FrameMargin = new System.Windows.Forms.Padding(0);
			this.tbKoma.FrameWeight = new System.Windows.Forms.Padding(8, 0, 0, 0);
			this.tbKoma.LineAlignment = System.Drawing.StringAlignment.Center;
			this.tbKoma.Location = new System.Drawing.Point(34, 150);
			this.tbKoma.MyFontIndex = 5;
			this.tbKoma.MyFontSize = 10F;
			this.tbKoma.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.tbKoma.Name = "tbKoma";
			this.tbKoma.Size = new System.Drawing.Size(110, 32);
			this.tbKoma.TabIndex = 5;
			this.tbKoma.TabStop = false;
			this.tbKoma.Text = "Koma";
			// 
			// nbKoma
			// 
			this.nbKoma.Alignment = System.Drawing.StringAlignment.Far;
			this.nbKoma.Back = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(11)))), ((int)(((byte)(25)))));
			this.nbKoma.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(11)))), ((int)(((byte)(25)))));
			this.nbKoma.CanReturnEdit = true;
			this.nbKoma.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.nbKoma.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(220)))), ((int)(((byte)(250)))));
			this.nbKoma.Frame = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
			this.nbKoma.FrameMargin = new System.Windows.Forms.Padding(0);
			this.nbKoma.FrameWeight = new System.Windows.Forms.Padding(0);
			this.nbKoma.LineAlignment = System.Drawing.StringAlignment.Center;
			this.nbKoma.Location = new System.Drawing.Point(178, 150);
			this.nbKoma.MyFontIndex = 5;
			this.nbKoma.MyFontSize = 12F;
			this.nbKoma.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.nbKoma.Name = "nbKoma";
			this.nbKoma.Size = new System.Drawing.Size(90, 31);
			this.nbKoma.TabIndex = 8;
			this.nbKoma.Value = 0;
			this.nbKoma.ValueMax = 32000;
			this.nbKoma.ValueMin = 0;
			// 
			// btnCancel
			// 
			this.btnCancel.Alignment = System.Drawing.StringAlignment.Center;
			this.btnCancel.Back = System.Drawing.Color.Transparent;
			this.btnCancel.BackColor = System.Drawing.Color.Transparent;
			this.btnCancel.Checked = false;
			this.btnCancel.Color_Down = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
			this.btnCancel.Color_Enter = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.btnCancel.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(250)))));
			this.btnCancel.Frame = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
			this.btnCancel.FrameMargin = new System.Windows.Forms.Padding(3, 0, 0, 3);
			this.btnCancel.FrameWeight = new System.Windows.Forms.Padding(10, 0, 0, 2);
			this.btnCancel.Group = 0;
			this.btnCancel.Id = 0;
			this.btnCancel.IsCheckMode = false;
			this.btnCancel.IsDrawFrame = true;
			this.btnCancel.IsMouseDown = false;
			this.btnCancel.LineAlignment = System.Drawing.StringAlignment.Center;
			this.btnCancel.Location = new System.Drawing.Point(34, 213);
			this.btnCancel.MyFontIndex = 5;
			this.btnCancel.MyFontSize = 9F;
			this.btnCancel.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(110, 31);
			this.btnCancel.TabIndex = 10;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOK
			// 
			this.btnOK.Alignment = System.Drawing.StringAlignment.Center;
			this.btnOK.Back = System.Drawing.Color.Transparent;
			this.btnOK.BackColor = System.Drawing.Color.Transparent;
			this.btnOK.Checked = false;
			this.btnOK.Color_Down = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
			this.btnOK.Color_Enter = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.btnOK.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(250)))));
			this.btnOK.Frame = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
			this.btnOK.FrameMargin = new System.Windows.Forms.Padding(0, 0, 3, 3);
			this.btnOK.FrameWeight = new System.Windows.Forms.Padding(0, 0, 10, 2);
			this.btnOK.Group = 0;
			this.btnOK.Id = 0;
			this.btnOK.IsCheckMode = false;
			this.btnOK.IsDrawFrame = true;
			this.btnOK.IsMouseDown = false;
			this.btnOK.LineAlignment = System.Drawing.StringAlignment.Center;
			this.btnOK.Location = new System.Drawing.Point(158, 213);
			this.btnOK.MyFontIndex = 5;
			this.btnOK.MyFontSize = 9F;
			this.btnOK.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(110, 31);
			this.btnOK.TabIndex = 11;
			this.btnOK.Text = "OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
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
			this.t_Zebra1.Location = new System.Drawing.Point(34, 187);
			this.t_Zebra1.MyFontIndex = 5;
			this.t_Zebra1.MyFontSize = 5F;
			this.t_Zebra1.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.t_Zebra1.Name = "t_Zebra1";
			this.t_Zebra1.Size = new System.Drawing.Size(234, 20);
			this.t_Zebra1.TabIndex = 9;
			this.t_Zebra1.TabStop = false;
			this.t_Zebra1.Text = "t_Zebra1";
			this.t_Zebra1.ZebraColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(100)))));
			this.t_Zebra1.ZebraRot = 45F;
			this.t_Zebra1.ZebraWidth = 15F;
			// 
			// t_Zebra2
			// 
			this.t_Zebra2.Alignment = System.Drawing.StringAlignment.Far;
			this.t_Zebra2.Back = System.Drawing.Color.Transparent;
			this.t_Zebra2.BackColor = System.Drawing.Color.Transparent;
			this.t_Zebra2.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.t_Zebra2.Frame = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
			this.t_Zebra2.FrameMargin = new System.Windows.Forms.Padding(0);
			this.t_Zebra2.FrameWeight = new System.Windows.Forms.Padding(0);
			this.t_Zebra2.LineAlignment = System.Drawing.StringAlignment.Center;
			this.t_Zebra2.Location = new System.Drawing.Point(34, 43);
			this.t_Zebra2.MyFontIndex = 5;
			this.t_Zebra2.MyFontSize = 5F;
			this.t_Zebra2.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.t_Zebra2.Name = "t_Zebra2";
			this.t_Zebra2.Size = new System.Drawing.Size(234, 28);
			this.t_Zebra2.TabIndex = 2;
			this.t_Zebra2.TabStop = false;
			this.t_Zebra2.Text = "t_Zebra2";
			this.t_Zebra2.ZebraColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(100)))));
			this.t_Zebra2.ZebraRot = 45F;
			this.t_Zebra2.ZebraWidth = 15F;
			// 
			// lbCaption
			// 
			this.lbCaption.Alignment = System.Drawing.StringAlignment.Near;
			this.lbCaption.Back = System.Drawing.Color.Transparent;
			this.lbCaption.BackColor = System.Drawing.Color.Transparent;
			this.lbCaption.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lbCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.lbCaption.Frame = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(150)))));
			this.lbCaption.FrameMargin = new System.Windows.Forms.Padding(0);
			this.lbCaption.FrameWeight = new System.Windows.Forms.Padding(12, 0, 0, 2);
			this.lbCaption.LineAlignment = System.Drawing.StringAlignment.Center;
			this.lbCaption.Location = new System.Drawing.Point(35, 15);
			this.lbCaption.MyFontIndex = 5;
			this.lbCaption.MyFontSize = 12F;
			this.lbCaption.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.lbCaption.Name = "lbCaption";
			this.lbCaption.Size = new System.Drawing.Size(233, 24);
			this.lbCaption.TabIndex = 1;
			this.lbCaption.TabStop = false;
			this.lbCaption.Text = "Auto Input";
			// 
			// t_ColorPlate1
			// 
			this.t_ColorPlate1.Alignment = System.Drawing.StringAlignment.Far;
			this.t_ColorPlate1.Back = System.Drawing.Color.Transparent;
			this.t_ColorPlate1.BackColor = System.Drawing.Color.Transparent;
			this.t_ColorPlate1.DotColor = System.Drawing.Color.Transparent;
			this.t_ColorPlate1.DotLoc = new System.Drawing.Point(10, 10);
			this.t_ColorPlate1.DotSize = new System.Drawing.Size(10, 10);
			this.t_ColorPlate1.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.t_ColorPlate1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
			this.t_ColorPlate1.Frame = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
			this.t_ColorPlate1.FrameMargin = new System.Windows.Forms.Padding(0);
			this.t_ColorPlate1.FrameWeight = new System.Windows.Forms.Padding(0);
			this.t_ColorPlate1.LineAlignment = System.Drawing.StringAlignment.Center;
			this.t_ColorPlate1.Location = new System.Drawing.Point(34, 182);
			this.t_ColorPlate1.MyFontIndex = 5;
			this.t_ColorPlate1.MyFontSize = 12F;
			this.t_ColorPlate1.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.t_ColorPlate1.Name = "t_ColorPlate1";
			this.t_ColorPlate1.Size = new System.Drawing.Size(246, 68);
			this.t_ColorPlate1.TabIndex = 12;
			this.t_ColorPlate1.TabStop = false;
			this.t_ColorPlate1.Text = "t_ColorPlate1";
			// 
			// t_ColorPlate2
			// 
			this.t_ColorPlate2.Alignment = System.Drawing.StringAlignment.Far;
			this.t_ColorPlate2.Back = System.Drawing.Color.Transparent;
			this.t_ColorPlate2.BackColor = System.Drawing.Color.Transparent;
			this.t_ColorPlate2.DotColor = System.Drawing.Color.Transparent;
			this.t_ColorPlate2.DotLoc = new System.Drawing.Point(10, 10);
			this.t_ColorPlate2.DotSize = new System.Drawing.Size(10, 10);
			this.t_ColorPlate2.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.t_ColorPlate2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
			this.t_ColorPlate2.Frame = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
			this.t_ColorPlate2.FrameMargin = new System.Windows.Forms.Padding(0);
			this.t_ColorPlate2.FrameWeight = new System.Windows.Forms.Padding(0);
			this.t_ColorPlate2.LineAlignment = System.Drawing.StringAlignment.Center;
			this.t_ColorPlate2.Location = new System.Drawing.Point(27, 12);
			this.t_ColorPlate2.MyFontIndex = 5;
			this.t_ColorPlate2.MyFontSize = 12F;
			this.t_ColorPlate2.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.t_ColorPlate2.Name = "t_ColorPlate2";
			this.t_ColorPlate2.Size = new System.Drawing.Size(246, 68);
			this.t_ColorPlate2.TabIndex = 0;
			this.t_ColorPlate2.Text = "t_ColorPlate2";
			// 
			// TR_AutoInputDialog
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(304, 263);
			this.Controls.Add(this.lbCaption);
			this.Controls.Add(this.t_Zebra2);
			this.Controls.Add(this.t_Zebra1);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.tbKoma);
			this.Controls.Add(this.nbKoma);
			this.Controls.Add(this.lbEnd);
			this.Controls.Add(this.nbLast);
			this.Controls.Add(this.lbStart);
			this.Controls.Add(this.nbStart);
			this.Controls.Add(this.t_ColorPlate1);
			this.Controls.Add(this.t_ColorPlate2);
			this.Edge = new System.Drawing.Rectangle(10, 10, 10, 10);
			this.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.KagiHeight = 20;
			this.KagiWeight = 3;
			this.KeyPreview = true;
			this.MyFontSize = 12F;
			this.Name = "TR_AutoInputDialog";
			this.SideCount = new int[] {
        4,
        4,
        0};
			this.SideInter = new int[] {
        10,
        20,
        120};
			this.SideLength = new int[] {
        4,
        8,
        0};
			this.SideWeight = new int[] {
        1,
        2,
        4};
			this.TBCenterWeight = 0;
			this.TBCount = new int[] {
        4,
        4,
        1};
			this.TBInter = new int[] {
        10,
        20,
        120};
			this.TBLength = new int[] {
        5,
        12,
        8};
			this.TBWeight = new int[] {
        0,
        0,
        0};
			this.Text = "T_AutoInputcs";
			this.Load += new System.EventHandler(this.T_AutoInputcs_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private TR_MyFonts t_MyFonts1;
		private TR_NumberBox nbStart;
		private TR_Label lbStart;
		private TR_Label lbEnd;
		private TR_NumberBox nbLast;
		private TR_Label tbKoma;
		private TR_NumberBox nbKoma;
		private TR_Button btnCancel;
		private TR_Button btnOK;
		private TR_Zebra t_Zebra1;
		private TR_Zebra t_Zebra2;
		private TR_Label lbCaption;
		private TR_ColorPlate t_ColorPlate1;
		private TR_ColorPlate t_ColorPlate2;
	}
}