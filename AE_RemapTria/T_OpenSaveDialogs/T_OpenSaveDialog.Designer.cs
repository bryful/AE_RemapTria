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
			this.components = new System.ComponentModel.Container();
			this.t_MyFonts1 = new AE_RemapTria.T_MyFonts(this.components);
			this.lbCaption = new AE_RemapTria.T_Label();
			this.t_DriveList1 = new AE_RemapTria.T_DriveList();
			this.FList = new AE_RemapTria.T_FList();
			this.lbDirectory = new AE_RemapTria.T_Label();
			this.t_FileExtFilter1 = new AE_RemapTria.T_FileExtFilter();
			this.tbFileName = new AE_RemapTria.T_TextBox();
			this.btnToParent = new AE_RemapTria.T_Button();
			this.btnToRoot = new AE_RemapTria.T_Button();
			this.t_vScrBar1 = new AE_RemapTria.T_VScrBar();
			this.t_Label2 = new AE_RemapTria.T_Label();
			this.zebra1 = new AE_RemapTria.T_Zebra();
			this.btnOK = new AE_RemapTria.T_Button();
			this.btnCancel = new AE_RemapTria.T_Button();
			this.t_ColorPlate1 = new AE_RemapTria.T_ColorPlate();
			this.t_bListBox1 = new AE_RemapTria.T_BListBox();
			this.SuspendLayout();
			// 
			// lbCaption
			// 
			this.lbCaption.Alignment = System.Drawing.StringAlignment.Near;
			this.lbCaption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lbCaption.BackColor = System.Drawing.Color.Transparent;
			this.lbCaption.BottomBar = new System.Drawing.Size(0, 0);
			this.lbCaption.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lbCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.lbCaption.LeftBar = new System.Drawing.Size(14, 14);
			this.lbCaption.LineAlignment = System.Drawing.StringAlignment.Center;
			this.lbCaption.Location = new System.Drawing.Point(27, 12);
			this.lbCaption.MyFontIndex = 5;
			this.lbCaption.MyFonts = this.t_MyFonts1;
			this.lbCaption.MyFontSize = 10F;
			this.lbCaption.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.lbCaption.Name = "lbCaption";
			this.lbCaption.RightBar = new System.Drawing.Size(4, 14);
			this.lbCaption.Size = new System.Drawing.Size(614, 30);
			this.lbCaption.TabIndex = 0;
			this.lbCaption.Text = "Import Export Dialog";
			this.lbCaption.TopBar = new System.Drawing.Size(1500, 2);
			// 
			// t_DriveList1
			// 
			this.t_DriveList1.Alignment = System.Drawing.StringAlignment.Far;
			this.t_DriveList1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.t_DriveList1.BackColor = System.Drawing.Color.Black;
			this.t_DriveList1.Drivers = new string[] {
        "C:\\",
        "D:\\",
        "E:\\",
        "F:\\",
        "Y:\\",
        "Z:\\"};
			this.t_DriveList1.FList = this.FList;
			this.t_DriveList1.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.t_DriveList1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(250)))));
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
			this.t_DriveList1.MyFonts = this.t_MyFonts1;
			this.t_DriveList1.MyFontSize = 10F;
			this.t_DriveList1.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.t_DriveList1.Name = "t_DriveList1";
			this.t_DriveList1.SelectedIndex = 0;
			this.t_DriveList1.Size = new System.Drawing.Size(610, 32);
			this.t_DriveList1.TabIndex = 1;
			this.t_DriveList1.Text = "t_DriveList1";
			// 
			// FList
			// 
			this.FList.Alignment = System.Drawing.StringAlignment.Far;
			this.FList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.FList.BackColor = System.Drawing.Color.Black;
			this.FList.DirectoryLabel = this.lbDirectory;
			this.FList.DispY = 0;
			this.FList.FileExtFilter = this.t_FileExtFilter1;
			this.FList.FileTextBox = this.tbFileName;
			this.FList.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.FList.ForcusColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
			this.FList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(250)))));
			this.FList.FrameColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(220)))));
			this.FList.FullName = "C:\\";
			this.FList.LineAlignment = System.Drawing.StringAlignment.Center;
			this.FList.Location = new System.Drawing.Point(199, 174);
			this.FList.MyFontIndex = 5;
			this.FList.MyFonts = this.t_MyFonts1;
			this.FList.MyFontSize = 9F;
			this.FList.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.FList.Name = "FList";
			this.FList.RowHeight = 20;
			this.FList.ScrolBarWidth = 20;
			this.FList.Size = new System.Drawing.Size(442, 266);
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
			this.lbDirectory.BackColor = System.Drawing.Color.Transparent;
			this.lbDirectory.BottomBar = new System.Drawing.Size(1500, 2);
			this.lbDirectory.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lbDirectory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.lbDirectory.LeftBar = new System.Drawing.Size(14, 14);
			this.lbDirectory.LineAlignment = System.Drawing.StringAlignment.Center;
			this.lbDirectory.Location = new System.Drawing.Point(27, 38);
			this.lbDirectory.MyFontIndex = 5;
			this.lbDirectory.MyFonts = this.t_MyFonts1;
			this.lbDirectory.MyFontSize = 10F;
			this.lbDirectory.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.lbDirectory.Name = "lbDirectory";
			this.lbDirectory.RightBar = new System.Drawing.Size(4, 40);
			this.lbDirectory.Size = new System.Drawing.Size(614, 57);
			this.lbDirectory.TabIndex = 2;
			this.lbDirectory.Text = "C:\\";
			this.lbDirectory.TopBar = new System.Drawing.Size(1500, 2);
			// 
			// t_FileExtFilter1
			// 
			this.t_FileExtFilter1.Alignment = System.Drawing.StringAlignment.Far;
			this.t_FileExtFilter1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.t_FileExtFilter1.BackColor = System.Drawing.Color.Black;
			this.t_FileExtFilter1.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.t_FileExtFilter1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(250)))));
			this.t_FileExtFilter1.LineAlignment = System.Drawing.StringAlignment.Center;
			this.t_FileExtFilter1.Location = new System.Drawing.Point(129, 506);
			this.t_FileExtFilter1.MyFontIndex = 5;
			this.t_FileExtFilter1.MyFonts = this.t_MyFonts1;
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
			this.tbFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbFileName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(11)))), ((int)(((byte)(25)))));
			this.tbFileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbFileName.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.tbFileName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(220)))), ((int)(((byte)(250)))));
			this.tbFileName.Location = new System.Drawing.Point(199, 459);
			this.tbFileName.MyFontIndex = 5;
			this.tbFileName.MyFonts = this.t_MyFonts1;
			this.tbFileName.MyFontSize = 9F;
			this.tbFileName.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.tbFileName.Name = "tbFileName";
			this.tbFileName.Size = new System.Drawing.Size(442, 25);
			this.tbFileName.TabIndex = 6;
			this.tbFileName.TextChanged += new System.EventHandler(this.tbFileName_TextChanged);
			// 
			// btnToParent
			// 
			this.btnToParent.Alignment = System.Drawing.StringAlignment.Center;
			this.btnToParent.BackColor = System.Drawing.Color.Black;
			this.btnToParent.BottomBar = 0;
			this.btnToParent.Checked = false;
			this.btnToParent.Color_Down = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
			this.btnToParent.Color_Enter = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.btnToParent.Color_line = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.btnToParent.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnToParent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(250)))));
			this.btnToParent.Id = 0;
			this.btnToParent.IsCheckMode = false;
			this.btnToParent.IsDrawFrame = true;
			this.btnToParent.IsMouseDown = false;
			this.btnToParent.LeftBar = 0;
			this.btnToParent.LineAlignment = System.Drawing.StringAlignment.Center;
			this.btnToParent.Location = new System.Drawing.Point(280, 145);
			this.btnToParent.MyFontIndex = 5;
			this.btnToParent.MyFonts = null;
			this.btnToParent.MyFontSize = 9F;
			this.btnToParent.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.btnToParent.Name = "btnToParent";
			this.btnToParent.RightBar = 0;
			this.btnToParent.Size = new System.Drawing.Size(75, 25);
			this.btnToParent.TabIndex = 14;
			this.btnToParent.Text = "Parent";
			this.btnToParent.TopBar = 3;
			// 
			// btnToRoot
			// 
			this.btnToRoot.Alignment = System.Drawing.StringAlignment.Center;
			this.btnToRoot.BackColor = System.Drawing.Color.Black;
			this.btnToRoot.BottomBar = 0;
			this.btnToRoot.Checked = false;
			this.btnToRoot.Color_Down = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
			this.btnToRoot.Color_Enter = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.btnToRoot.Color_line = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.btnToRoot.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnToRoot.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(250)))));
			this.btnToRoot.Id = 0;
			this.btnToRoot.IsCheckMode = false;
			this.btnToRoot.IsDrawFrame = true;
			this.btnToRoot.IsMouseDown = false;
			this.btnToRoot.LeftBar = 0;
			this.btnToRoot.LineAlignment = System.Drawing.StringAlignment.Center;
			this.btnToRoot.Location = new System.Drawing.Point(199, 144);
			this.btnToRoot.MyFontIndex = 5;
			this.btnToRoot.MyFonts = null;
			this.btnToRoot.MyFontSize = 9F;
			this.btnToRoot.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.btnToRoot.Name = "btnToRoot";
			this.btnToRoot.RightBar = 0;
			this.btnToRoot.Size = new System.Drawing.Size(75, 25);
			this.btnToRoot.TabIndex = 13;
			this.btnToRoot.Text = "Root";
			this.btnToRoot.TopBar = 3;
			// 
			// t_vScrBar1
			// 
			this.t_vScrBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.t_vScrBar1.BackColor = System.Drawing.Color.Transparent;
			this.t_vScrBar1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(180)))));
			this.t_vScrBar1.Location = new System.Drawing.Point(647, 173);
			this.t_vScrBar1.MaxValue = 154;
			this.t_vScrBar1.Name = "t_vScrBar1";
			this.t_vScrBar1.Size = new System.Drawing.Size(20, 267);
			this.t_vScrBar1.TabIndex = 12;
			this.t_vScrBar1.Text = "t_vScrBar1";
			this.t_vScrBar1.Value = 0;
			// 
			// t_Label2
			// 
			this.t_Label2.Alignment = System.Drawing.StringAlignment.Near;
			this.t_Label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.t_Label2.BackColor = System.Drawing.Color.Transparent;
			this.t_Label2.BottomBar = new System.Drawing.Size(0, 0);
			this.t_Label2.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.t_Label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.t_Label2.LeftBar = new System.Drawing.Size(14, 14);
			this.t_Label2.LineAlignment = System.Drawing.StringAlignment.Center;
			this.t_Label2.Location = new System.Drawing.Point(35, 502);
			this.t_Label2.MyFontIndex = 5;
			this.t_Label2.MyFonts = this.t_MyFonts1;
			this.t_Label2.MyFontSize = 8.5F;
			this.t_Label2.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.t_Label2.Name = "t_Label2";
			this.t_Label2.RightBar = new System.Drawing.Size(0, 0);
			this.t_Label2.Size = new System.Drawing.Size(93, 24);
			this.t_Label2.TabIndex = 8;
			this.t_Label2.Text = "Extension";
			this.t_Label2.TopBar = new System.Drawing.Size(0, 0);
			// 
			// zebra1
			// 
			this.zebra1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.zebra1.BackColor = System.Drawing.Color.Transparent;
			this.zebra1.Location = new System.Drawing.Point(0, 451);
			this.zebra1.Name = "zebra1";
			this.zebra1.Size = new System.Drawing.Size(680, 40);
			this.zebra1.TabIndex = 5;
			this.zebra1.Text = "t_Zebra1";
			this.zebra1.ZebraIndex = AE_RemapTria.ZEBRA_TYPE.DARKBLUE;
			// 
			// btnOK
			// 
			this.btnOK.Alignment = System.Drawing.StringAlignment.Center;
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.BackColor = System.Drawing.Color.Black;
			this.btnOK.BottomBar = 3;
			this.btnOK.Checked = false;
			this.btnOK.Color_Down = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
			this.btnOK.Color_Enter = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.btnOK.Color_line = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.btnOK.Enabled = false;
			this.btnOK.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(250)))));
			this.btnOK.Id = 0;
			this.btnOK.IsCheckMode = false;
			this.btnOK.IsDrawFrame = true;
			this.btnOK.IsMouseDown = false;
			this.btnOK.LeftBar = 0;
			this.btnOK.LineAlignment = System.Drawing.StringAlignment.Center;
			this.btnOK.Location = new System.Drawing.Point(535, 498);
			this.btnOK.MyFontIndex = 5;
			this.btnOK.MyFonts = null;
			this.btnOK.MyFontSize = 9F;
			this.btnOK.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.btnOK.Name = "btnOK";
			this.btnOK.RightBar = 10;
			this.btnOK.Size = new System.Drawing.Size(106, 41);
			this.btnOK.TabIndex = 11;
			this.btnOK.Text = "OK";
			this.btnOK.TopBar = 0;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Alignment = System.Drawing.StringAlignment.Center;
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.BackColor = System.Drawing.Color.Black;
			this.btnCancel.BottomBar = 3;
			this.btnCancel.Checked = false;
			this.btnCancel.Color_Down = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
			this.btnCancel.Color_Enter = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.btnCancel.Color_line = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.btnCancel.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(250)))));
			this.btnCancel.Id = 0;
			this.btnCancel.IsCheckMode = false;
			this.btnCancel.IsDrawFrame = true;
			this.btnCancel.IsMouseDown = false;
			this.btnCancel.LeftBar = 0;
			this.btnCancel.LineAlignment = System.Drawing.StringAlignment.Center;
			this.btnCancel.Location = new System.Drawing.Point(441, 498);
			this.btnCancel.MyFontIndex = 5;
			this.btnCancel.MyFonts = null;
			this.btnCancel.MyFontSize = 9F;
			this.btnCancel.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.RightBar = 0;
			this.btnCancel.Size = new System.Drawing.Size(88, 40);
			this.btnCancel.TabIndex = 10;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.TopBar = 0;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// t_ColorPlate1
			// 
			this.t_ColorPlate1.Alignment = System.Drawing.StringAlignment.Far;
			this.t_ColorPlate1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.t_ColorPlate1.BackColor = System.Drawing.Color.Black;
			this.t_ColorPlate1.ColorPlate = AE_RemapTria.ColorPlate.KagiBL;
			this.t_ColorPlate1.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.t_ColorPlate1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
			this.t_ColorPlate1.KagiHWeight = 3;
			this.t_ColorPlate1.KagiVWeight = 3;
			this.t_ColorPlate1.LineAlignment = System.Drawing.StringAlignment.Center;
			this.t_ColorPlate1.Location = new System.Drawing.Point(26, 500);
			this.t_ColorPlate1.MyFontIndex = 5;
			this.t_ColorPlate1.MyFonts = null;
			this.t_ColorPlate1.MyFontSize = 9F;
			this.t_ColorPlate1.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.t_ColorPlate1.Name = "t_ColorPlate1";
			this.t_ColorPlate1.Opacity = 128;
			this.t_ColorPlate1.Size = new System.Drawing.Size(409, 40);
			this.t_ColorPlate1.TabIndex = 7;
			this.t_ColorPlate1.Text = "t_ColorPlate1";
			// 
			// t_bListBox1
			// 
			this.t_bListBox1.BackColor = System.Drawing.Color.Transparent;
			this.t_bListBox1.Flist = this.FList;
			this.t_bListBox1.Location = new System.Drawing.Point(26, 145);
			this.t_bListBox1.MinimumSize = new System.Drawing.Size(166, 164);
			this.t_bListBox1.MyFontIndex = 5;
			this.t_bListBox1.MyFonts = null;
			this.t_bListBox1.MyFontSize = 9F;
			this.t_bListBox1.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.t_bListBox1.Name = "t_bListBox1";
			this.t_bListBox1.Size = new System.Drawing.Size(166, 295);
			this.t_bListBox1.TabIndex = 15;
			// 
			// T_OpenSaveDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CanReSize = true;
			this.ClientSize = new System.Drawing.Size(680, 553);
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
			this.Controls.Add(this.t_vScrBar1);
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
			this.MyFonts = this.t_MyFonts1;
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
			this.PerformLayout();

		}

		#endregion

		private T_MyFonts t_MyFonts1;
		private T_Label lbCaption;
		private T_DriveList t_DriveList1;
		private T_VScrBar t_vScrBar1;
		private T_Label t_Label2;
		private T_FileExtFilter t_FileExtFilter1;
		private T_Zebra zebra1;
		private T_TextBox tbFileName;
		private T_Label lbDirectory;
		private T_Button btnOK;
		private T_Button btnCancel;
		private T_FList FList;
		private T_ColorPlate t_ColorPlate1;
		private T_Button btnToRoot;
		private T_Button btnToParent;
		private T_BListBox t_bListBox1;
	}
}