namespace AE_RemapTria
{
    partial class T_OpenSaveDialog
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
			this.lbCaption = new AE_RemapTria.TR_Label();
			this.t_DriveList1 = new AE_RemapTria.T_DriveList();
			this.FList = new AE_RemapTria.T_FList();
			this.lbDirectory = new AE_RemapTria.TR_Label();
			this.t_FileExtFilter1 = new AE_RemapTria.T_FileExtFilter();
			this.tbFileName = new AE_RemapTria.TR_TextBox();
			this.btnToParent = new AE_RemapTria.TR_Button();
			this.btnToRoot = new AE_RemapTria.TR_Button();
			this.t_vScrBar1 = new AE_RemapTria.T_VScrBar();
			this.t_Label2 = new AE_RemapTria.TR_Label();
			this.zebra1 = new AE_RemapTria.TR_Zebra();
			this.btnOK = new AE_RemapTria.TR_Button();
			this.btnCancel = new AE_RemapTria.TR_Button();
			this.t_ColorPlate1 = new AE_RemapTria.TR_ColorPlate();
			this.t_bListBox1 = new AE_RemapTria.T_BListBox();
			this.SuspendLayout();
			// 
			// lbCaption
			// 
			this.lbCaption.Alignment = System.Drawing.StringAlignment.Near;
			this.lbCaption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lbCaption.Back = System.Drawing.Color.Transparent;
			this.lbCaption.BackColor = System.Drawing.Color.Transparent;
			this.lbCaption.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lbCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.lbCaption.Frame = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
			this.lbCaption.FrameMargin = new System.Windows.Forms.Padding(0);
			this.lbCaption.FrameWeight = new System.Windows.Forms.Padding(20, 1, 0, 1);
			this.lbCaption.LineAlignment = System.Drawing.StringAlignment.Center;
			this.lbCaption.Location = new System.Drawing.Point(27, 12);
			this.lbCaption.MyFontIndex = 5;
			this.lbCaption.MyFontSize = 10F;
			this.lbCaption.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.lbCaption.Name = "lbCaption";
			this.lbCaption.Size = new System.Drawing.Size(649, 32);
			this.lbCaption.TabIndex = 0;
			this.lbCaption.TabStop = false;
			this.lbCaption.Text = "Import Export Dialog";
			// 
			// t_DriveList1
			// 
			this.t_DriveList1.Alignment = System.Drawing.StringAlignment.Far;
			this.t_DriveList1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.t_DriveList1.Back = System.Drawing.Color.Transparent;
			this.t_DriveList1.BackColor = System.Drawing.Color.Transparent;
			this.t_DriveList1.Drivers = new string[] {
        "C:\\",
        "D:\\",
        "E:\\",
        "F:\\"};
			this.t_DriveList1.FList = this.FList;
			this.t_DriveList1.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.t_DriveList1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(250)))));
			this.t_DriveList1.Frame = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
			this.t_DriveList1.FrameMargin = new System.Windows.Forms.Padding(0);
			this.t_DriveList1.FrameWeight = new System.Windows.Forms.Padding(0);
			this.t_DriveList1.FullName = "C:\\";
			this.t_DriveList1.IconBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
			this.t_DriveList1.IconFrameColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(180)))));
			this.t_DriveList1.IconFrameColorLo = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(100)))));
			this.t_DriveList1.IconHeight = 32;
			this.t_DriveList1.IconWidth = 32;
			this.t_DriveList1.IsHor = true;
			this.t_DriveList1.LineAlignment = System.Drawing.StringAlignment.Center;
			this.t_DriveList1.Location = new System.Drawing.Point(26, 101);
			this.t_DriveList1.MyFontIndex = 5;
			this.t_DriveList1.MyFontSize = 10F;
			this.t_DriveList1.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.t_DriveList1.Name = "t_DriveList1";
			this.t_DriveList1.SelectedIndex = 0;
			this.t_DriveList1.Size = new System.Drawing.Size(645, 32);
			this.t_DriveList1.TabIndex = 1;
			this.t_DriveList1.Text = "t_DriveList1";
			// 
			// FList
			// 
			this.FList.Alignment = System.Drawing.StringAlignment.Far;
			this.FList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.FList.Back = System.Drawing.Color.Transparent;
			this.FList.BackColor = System.Drawing.Color.Transparent;
			this.FList.DirectoryLabel = this.lbDirectory;
			this.FList.DispY = 0;
			this.FList.FileExtFilter = this.t_FileExtFilter1;
			this.FList.FileTextBox = this.tbFileName;
			this.FList.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.FList.ForcusColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
			this.FList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(250)))));
			this.FList.Frame = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
			this.FList.FrameColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(220)))));
			this.FList.FrameMargin = new System.Windows.Forms.Padding(0);
			this.FList.FrameWeight = new System.Windows.Forms.Padding(0);
			this.FList.FullName = "C:\\";
			this.FList.LineAlignment = System.Drawing.StringAlignment.Center;
			this.FList.Location = new System.Drawing.Point(199, 174);
			this.FList.MyFontIndex = 5;
			this.FList.MyFontSize = 9F;
			this.FList.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.FList.Name = "FList";
			this.FList.RowHeight = 20;
			this.FList.ScrolBarWidth = 20;
			this.FList.Size = new System.Drawing.Size(477, 263);
			this.FList.TabIndex = 4;
			this.FList.Text = "t_fList1";
			this.FList.ToParentBtn = this.btnToParent;
			this.FList.ToRootBtn = this.btnToRoot;
			this.FList.TragetExt = new string[0];
			this.FList.VScrBar = this.t_vScrBar1;
			// 
			// lbDirectory
			// 
			this.lbDirectory.Alignment = System.Drawing.StringAlignment.Near;
			this.lbDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lbDirectory.Back = System.Drawing.Color.Transparent;
			this.lbDirectory.BackColor = System.Drawing.Color.Transparent;
			this.lbDirectory.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lbDirectory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.lbDirectory.Frame = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
			this.lbDirectory.FrameMargin = new System.Windows.Forms.Padding(0);
			this.lbDirectory.FrameWeight = new System.Windows.Forms.Padding(0);
			this.lbDirectory.LineAlignment = System.Drawing.StringAlignment.Center;
			this.lbDirectory.Location = new System.Drawing.Point(27, 50);
			this.lbDirectory.MyFontIndex = 5;
			this.lbDirectory.MyFontSize = 10F;
			this.lbDirectory.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.lbDirectory.Name = "lbDirectory";
			this.lbDirectory.Size = new System.Drawing.Size(649, 45);
			this.lbDirectory.TabIndex = 2;
			this.lbDirectory.TabStop = false;
			this.lbDirectory.Text = "C:\\";
			// 
			// t_FileExtFilter1
			// 
			this.t_FileExtFilter1.Alignment = System.Drawing.StringAlignment.Far;
			this.t_FileExtFilter1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.t_FileExtFilter1.Back = System.Drawing.Color.Black;
			this.t_FileExtFilter1.BackColor = System.Drawing.Color.Black;
			this.t_FileExtFilter1.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.t_FileExtFilter1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(250)))));
			this.t_FileExtFilter1.Frame = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
			this.t_FileExtFilter1.FrameMargin = new System.Windows.Forms.Padding(0);
			this.t_FileExtFilter1.FrameWeight = new System.Windows.Forms.Padding(0);
			this.t_FileExtFilter1.LineAlignment = System.Drawing.StringAlignment.Center;
			this.t_FileExtFilter1.Location = new System.Drawing.Point(129, 503);
			this.t_FileExtFilter1.MyFontIndex = 5;
			this.t_FileExtFilter1.MyFontSize = 9F;
			this.t_FileExtFilter1.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.t_FileExtFilter1.Name = "t_FileExtFilter1";
			this.t_FileExtFilter1.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(120)))));
			this.t_FileExtFilter1.Size = new System.Drawing.Size(307, 20);
			this.t_FileExtFilter1.TabIndex = 9;
			this.t_FileExtFilter1.Text = "t_FileExtFilter1";
			// 
			// tbFileName
			// 
			this.tbFileName.Alignment = System.Drawing.StringAlignment.Far;
			this.tbFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbFileName.Back = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(11)))), ((int)(((byte)(25)))));
			this.tbFileName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(11)))), ((int)(((byte)(25)))));
			this.tbFileName.CanReturnEdit = true;
			this.tbFileName.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.tbFileName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(220)))), ((int)(((byte)(250)))));
			this.tbFileName.Frame = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
			this.tbFileName.FrameMargin = new System.Windows.Forms.Padding(0);
			this.tbFileName.FrameWeight = new System.Windows.Forms.Padding(0);
			this.tbFileName.LineAlignment = System.Drawing.StringAlignment.Center;
			this.tbFileName.Location = new System.Drawing.Point(199, 456);
			this.tbFileName.MyFontIndex = 5;
			this.tbFileName.MyFontSize = 9F;
			this.tbFileName.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.tbFileName.Name = "tbFileName";
			this.tbFileName.Size = new System.Drawing.Size(477, 25);
			this.tbFileName.TabIndex = 6;
			this.tbFileName.TextChanged += new System.EventHandler(this.tbFileName_TextChanged);
			// 
			// btnToParent
			// 
			this.btnToParent.Alignment = System.Drawing.StringAlignment.Center;
			this.btnToParent.Back = System.Drawing.Color.Black;
			this.btnToParent.BackColor = System.Drawing.Color.Black;
			this.btnToParent.Checked = false;
			this.btnToParent.Color_Down = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
			this.btnToParent.Color_Enter = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.btnToParent.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnToParent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(250)))));
			this.btnToParent.Frame = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
			this.btnToParent.FrameMargin = new System.Windows.Forms.Padding(0);
			this.btnToParent.FrameWeight = new System.Windows.Forms.Padding(0);
			this.btnToParent.Group = 0;
			this.btnToParent.Id = 0;
			this.btnToParent.IsCheckMode = false;
			this.btnToParent.IsDrawFrame = true;
			this.btnToParent.IsMouseDown = false;
			this.btnToParent.LineAlignment = System.Drawing.StringAlignment.Center;
			this.btnToParent.Location = new System.Drawing.Point(280, 145);
			this.btnToParent.MyFontIndex = 5;
			this.btnToParent.MyFontSize = 9F;
			this.btnToParent.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.btnToParent.Name = "btnToParent";
			this.btnToParent.Size = new System.Drawing.Size(75, 25);
			this.btnToParent.TabIndex = 14;
			this.btnToParent.Text = "Parent";
			// 
			// btnToRoot
			// 
			this.btnToRoot.Alignment = System.Drawing.StringAlignment.Center;
			this.btnToRoot.Back = System.Drawing.Color.Black;
			this.btnToRoot.BackColor = System.Drawing.Color.Black;
			this.btnToRoot.Checked = false;
			this.btnToRoot.Color_Down = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
			this.btnToRoot.Color_Enter = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.btnToRoot.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnToRoot.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(250)))));
			this.btnToRoot.Frame = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
			this.btnToRoot.FrameMargin = new System.Windows.Forms.Padding(0);
			this.btnToRoot.FrameWeight = new System.Windows.Forms.Padding(0);
			this.btnToRoot.Group = 0;
			this.btnToRoot.Id = 0;
			this.btnToRoot.IsCheckMode = false;
			this.btnToRoot.IsDrawFrame = true;
			this.btnToRoot.IsMouseDown = false;
			this.btnToRoot.LineAlignment = System.Drawing.StringAlignment.Center;
			this.btnToRoot.Location = new System.Drawing.Point(199, 144);
			this.btnToRoot.MyFontIndex = 5;
			this.btnToRoot.MyFontSize = 9F;
			this.btnToRoot.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.btnToRoot.Name = "btnToRoot";
			this.btnToRoot.Size = new System.Drawing.Size(75, 25);
			this.btnToRoot.TabIndex = 13;
			this.btnToRoot.Text = "Root";
			// 
			// t_vScrBar1
			// 
			this.t_vScrBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.t_vScrBar1.BackColor = System.Drawing.Color.Transparent;
			this.t_vScrBar1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(180)))));
			this.t_vScrBar1.Location = new System.Drawing.Point(683, 174);
			this.t_vScrBar1.MaxValue = 97;
			this.t_vScrBar1.Name = "t_vScrBar1";
			this.t_vScrBar1.Size = new System.Drawing.Size(20, 263);
			this.t_vScrBar1.TabIndex = 16;
			this.t_vScrBar1.Text = "t_vScrBar2";
			this.t_vScrBar1.Value = 0;
			// 
			// t_Label2
			// 
			this.t_Label2.Alignment = System.Drawing.StringAlignment.Near;
			this.t_Label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.t_Label2.Back = System.Drawing.Color.Transparent;
			this.t_Label2.BackColor = System.Drawing.Color.Transparent;
			this.t_Label2.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.t_Label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.t_Label2.Frame = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
			this.t_Label2.FrameMargin = new System.Windows.Forms.Padding(0);
			this.t_Label2.FrameWeight = new System.Windows.Forms.Padding(0);
			this.t_Label2.LineAlignment = System.Drawing.StringAlignment.Center;
			this.t_Label2.Location = new System.Drawing.Point(35, 499);
			this.t_Label2.MyFontIndex = 5;
			this.t_Label2.MyFontSize = 8.5F;
			this.t_Label2.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.t_Label2.Name = "t_Label2";
			this.t_Label2.Size = new System.Drawing.Size(93, 24);
			this.t_Label2.TabIndex = 8;
			this.t_Label2.TabStop = false;
			this.t_Label2.Text = "Extension";
			// 
			// zebra1
			// 
			this.zebra1.Alignment = System.Drawing.StringAlignment.Far;
			this.zebra1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.zebra1.Back = System.Drawing.Color.Transparent;
			this.zebra1.BackColor = System.Drawing.Color.Transparent;
			this.zebra1.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.zebra1.ForeColor = System.Drawing.Color.Transparent;
			this.zebra1.Frame = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
			this.zebra1.FrameMargin = new System.Windows.Forms.Padding(0);
			this.zebra1.FrameWeight = new System.Windows.Forms.Padding(0);
			this.zebra1.LineAlignment = System.Drawing.StringAlignment.Center;
			this.zebra1.Location = new System.Drawing.Point(-2, 448);
			this.zebra1.MyFontIndex = 5;
			this.zebra1.MyFontSize = 5F;
			this.zebra1.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.zebra1.Name = "zebra1";
			this.zebra1.Size = new System.Drawing.Size(710, 40);
			this.zebra1.TabIndex = 5;
			this.zebra1.TabStop = false;
			this.zebra1.Text = "t_Zebra1";
			this.zebra1.ZebraColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
			this.zebra1.ZebraRot = 45F;
			this.zebra1.ZebraWidth = 25F;
			// 
			// btnOK
			// 
			this.btnOK.Alignment = System.Drawing.StringAlignment.Center;
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.Back = System.Drawing.Color.Transparent;
			this.btnOK.BackColor = System.Drawing.Color.Transparent;
			this.btnOK.Checked = false;
			this.btnOK.Color_Down = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
			this.btnOK.Color_Enter = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.btnOK.Enabled = false;
			this.btnOK.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(250)))));
			this.btnOK.Frame = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
			this.btnOK.FrameMargin = new System.Windows.Forms.Padding(0);
			this.btnOK.FrameWeight = new System.Windows.Forms.Padding(0);
			this.btnOK.Group = 0;
			this.btnOK.Id = 0;
			this.btnOK.IsCheckMode = false;
			this.btnOK.IsDrawFrame = true;
			this.btnOK.IsMouseDown = false;
			this.btnOK.LineAlignment = System.Drawing.StringAlignment.Center;
			this.btnOK.Location = new System.Drawing.Point(570, 495);
			this.btnOK.MyFontIndex = 5;
			this.btnOK.MyFontSize = 9F;
			this.btnOK.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(106, 41);
			this.btnOK.TabIndex = 11;
			this.btnOK.Text = "OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Alignment = System.Drawing.StringAlignment.Center;
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Back = System.Drawing.Color.Transparent;
			this.btnCancel.BackColor = System.Drawing.Color.Transparent;
			this.btnCancel.Checked = false;
			this.btnCancel.Color_Down = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
			this.btnCancel.Color_Enter = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.btnCancel.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(250)))));
			this.btnCancel.Frame = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
			this.btnCancel.FrameMargin = new System.Windows.Forms.Padding(0);
			this.btnCancel.FrameWeight = new System.Windows.Forms.Padding(0);
			this.btnCancel.Group = 0;
			this.btnCancel.Id = 0;
			this.btnCancel.IsCheckMode = false;
			this.btnCancel.IsDrawFrame = true;
			this.btnCancel.IsMouseDown = false;
			this.btnCancel.LineAlignment = System.Drawing.StringAlignment.Center;
			this.btnCancel.Location = new System.Drawing.Point(476, 495);
			this.btnCancel.MyFontIndex = 5;
			this.btnCancel.MyFontSize = 9F;
			this.btnCancel.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(88, 40);
			this.btnCancel.TabIndex = 10;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// t_ColorPlate1
			// 
			this.t_ColorPlate1.Alignment = System.Drawing.StringAlignment.Far;
			this.t_ColorPlate1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.t_ColorPlate1.Back = System.Drawing.Color.Transparent;
			this.t_ColorPlate1.BackColor = System.Drawing.Color.Transparent;
			this.t_ColorPlate1.DotColor = System.Drawing.Color.Transparent;
			this.t_ColorPlate1.DotLoc = new System.Drawing.Point(10, 10);
			this.t_ColorPlate1.DotSize = new System.Drawing.Size(10, 10);
			this.t_ColorPlate1.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.t_ColorPlate1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
			this.t_ColorPlate1.Frame = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
			this.t_ColorPlate1.FrameMargin = new System.Windows.Forms.Padding(0);
			this.t_ColorPlate1.FrameWeight = new System.Windows.Forms.Padding(0);
			this.t_ColorPlate1.LineAlignment = System.Drawing.StringAlignment.Center;
			this.t_ColorPlate1.Location = new System.Drawing.Point(26, 497);
			this.t_ColorPlate1.MyFontIndex = 5;
			this.t_ColorPlate1.MyFontSize = 9F;
			this.t_ColorPlate1.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.t_ColorPlate1.Name = "t_ColorPlate1";
			this.t_ColorPlate1.Size = new System.Drawing.Size(444, 40);
			this.t_ColorPlate1.TabIndex = 7;
			this.t_ColorPlate1.Text = "t_ColorPlate1";
			// 
			// t_bListBox1
			// 
			this.t_bListBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.t_bListBox1.AutoScroll = true;
			this.t_bListBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.t_bListBox1.BackColor = System.Drawing.Color.Transparent;
			this.t_bListBox1.Flist = this.FList;
			this.t_bListBox1.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.t_bListBox1.Location = new System.Drawing.Point(30, 145);
			this.t_bListBox1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
			this.t_bListBox1.MinimumSize = new System.Drawing.Size(120, 186);
			this.t_bListBox1.MyFontIndex = 5;
			this.t_bListBox1.MyFontSize = 12F;
			this.t_bListBox1.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.t_bListBox1.Name = "t_bListBox1";
			this.t_bListBox1.Size = new System.Drawing.Size(163, 292);
			this.t_bListBox1.TabIndex = 15;
			// 
			// T_OpenSaveDialog
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.CanReSize = true;
			this.ClientSize = new System.Drawing.Size(715, 550);
			this.Controls.Add(this.t_vScrBar1);
			this.Controls.Add(this.t_bListBox1);
			this.Controls.Add(this.btnToParent);
			this.Controls.Add(this.btnToRoot);
			this.Controls.Add(this.FList);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.lbDirectory);
			this.Controls.Add(this.tbFileName);
			this.Controls.Add(this.zebra1);
			this.Controls.Add(this.t_FileExtFilter1);
			this.Controls.Add(this.t_Label2);
			this.Controls.Add(this.t_DriveList1);
			this.Controls.Add(this.lbCaption);
			this.Controls.Add(this.t_ColorPlate1);
			this.DoubleBuffered = true;
			this.Edge = new System.Drawing.Rectangle(10, 5, 10, 5);
			this.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.KagiHeight = 40;
			this.KagiWeight = 1;
			this.KeyPreview = true;
			this.MinimumSize = new System.Drawing.Size(680, 370);
			this.MyFontSize = 9F;
			this.Name = "T_OpenSaveDialog";
			this.SideCenterLength = 0;
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
			this.Text = "T_OpenSaveDialog";
			this.ResumeLayout(false);

		}

		#endregion
		private TR_Label lbCaption;
		private T_DriveList t_DriveList1;
		private TR_Label t_Label2;
		private T_FileExtFilter t_FileExtFilter1;
		private TR_Zebra zebra1;
		private TR_TextBox tbFileName;
		private TR_Label lbDirectory;
		private TR_Button btnOK;
		private TR_Button btnCancel;
		private T_FList FList;
		private TR_ColorPlate t_ColorPlate1;
		private TR_Button btnToRoot;
		private TR_Button btnToParent;
		private T_VScrBar t_vScrBar1;
		private T_BListBox t_bListBox1;
	}
}