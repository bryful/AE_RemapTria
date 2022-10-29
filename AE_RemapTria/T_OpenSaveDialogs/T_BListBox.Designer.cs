namespace AE_RemapTria
{
	partial class T_BListBox
	{
		/// <summary> 
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region コンポーネント デザイナーで生成されたコード

		/// <summary> 
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.BList = new AE_RemapTria.T_BList();
			this.t_vScrBar1 = new AE_RemapTria.T_VScrBar();
			this.btnAdd = new AE_RemapTria.T_Button();
			this.btnDell = new AE_RemapTria.T_Button();
			this.btnUp = new AE_RemapTria.T_Button();
			this.btnDown = new AE_RemapTria.T_Button();
			this.btnEdit = new AE_RemapTria.T_Button();
			this.SuspendLayout();
			// 
			// BList
			// 
			this.BList.Alignment = System.Drawing.StringAlignment.Far;
			this.BList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.BList.BackColor = System.Drawing.Color.Transparent;
			this.BList.DispY = 0;
			this.BList.Flist = null;
			this.BList.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.BList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(250)))));
			this.BList.FrameColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(220)))));
			this.BList.LineAlignment = System.Drawing.StringAlignment.Center;
			this.BList.Location = new System.Drawing.Point(0, 31);
			this.BList.MyFontIndex = 5;
			this.BList.MyFonts = null;
			this.BList.MyFontSize = 8.999999F;
			this.BList.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.BList.Name = "BList";
			this.BList.RowHeight = 20;
			this.BList.SelectedCaption = "";
			this.BList.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(120)))));
			this.BList.SelectedIndex = -1;
			this.BList.Size = new System.Drawing.Size(133, 274);
			this.BList.TabIndex = 0;
			this.BList.Text = "t_bList1";
			this.BList.VScrBar = this.t_vScrBar1;
			// 
			// t_vScrBar1
			// 
			this.t_vScrBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.t_vScrBar1.BackColor = System.Drawing.Color.Transparent;
			this.t_vScrBar1.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.t_vScrBar1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(180)))));
			this.t_vScrBar1.Location = new System.Drawing.Point(138, 31);
			this.t_vScrBar1.MaxValue = 0;
			this.t_vScrBar1.Name = "t_vScrBar1";
			this.t_vScrBar1.Size = new System.Drawing.Size(16, 274);
			this.t_vScrBar1.TabIndex = 1;
			this.t_vScrBar1.Text = "t_vScrBar1";
			this.t_vScrBar1.Value = 0;
			// 
			// btnAdd
			// 
			this.btnAdd.Alignment = System.Drawing.StringAlignment.Center;
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAdd.BackColor = System.Drawing.Color.Transparent;
			this.btnAdd.BottomBar = 0;
			this.btnAdd.Checked = false;
			this.btnAdd.Color_Down = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
			this.btnAdd.Color_Enter = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.btnAdd.Color_line = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.btnAdd.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnAdd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(250)))));
			this.btnAdd.Id = 0;
			this.btnAdd.IsCheckMode = false;
			this.btnAdd.IsDrawFrame = true;
			this.btnAdd.IsMouseDown = false;
			this.btnAdd.LeftBar = 0;
			this.btnAdd.LineAlignment = System.Drawing.StringAlignment.Center;
			this.btnAdd.Location = new System.Drawing.Point(0, 0);
			this.btnAdd.MyFontIndex = 5;
			this.btnAdd.MyFonts = null;
			this.btnAdd.MyFontSize = 8.999999F;
			this.btnAdd.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.RightBar = 0;
			this.btnAdd.Size = new System.Drawing.Size(70, 25);
			this.btnAdd.TabIndex = 2;
			this.btnAdd.Text = "Add";
			this.btnAdd.TopBar = 0;
			// 
			// btnDell
			// 
			this.btnDell.Alignment = System.Drawing.StringAlignment.Center;
			this.btnDell.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnDell.BackColor = System.Drawing.Color.Transparent;
			this.btnDell.BottomBar = 0;
			this.btnDell.Checked = false;
			this.btnDell.Color_Down = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
			this.btnDell.Color_Enter = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.btnDell.Color_line = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.btnDell.Enabled = false;
			this.btnDell.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnDell.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(250)))));
			this.btnDell.Id = 0;
			this.btnDell.IsCheckMode = false;
			this.btnDell.IsDrawFrame = true;
			this.btnDell.IsMouseDown = false;
			this.btnDell.LeftBar = 0;
			this.btnDell.LineAlignment = System.Drawing.StringAlignment.Center;
			this.btnDell.Location = new System.Drawing.Point(0, 311);
			this.btnDell.MyFontIndex = 5;
			this.btnDell.MyFonts = null;
			this.btnDell.MyFontSize = 8.999999F;
			this.btnDell.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.btnDell.Name = "btnDell";
			this.btnDell.RightBar = 0;
			this.btnDell.Size = new System.Drawing.Size(34, 25);
			this.btnDell.TabIndex = 3;
			this.btnDell.Text = "Del";
			this.btnDell.TopBar = 0;
			// 
			// btnUp
			// 
			this.btnUp.Alignment = System.Drawing.StringAlignment.Center;
			this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnUp.BackColor = System.Drawing.Color.Transparent;
			this.btnUp.BottomBar = 0;
			this.btnUp.Checked = false;
			this.btnUp.Color_Down = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
			this.btnUp.Color_Enter = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.btnUp.Color_line = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.btnUp.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnUp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(250)))));
			this.btnUp.Id = 0;
			this.btnUp.IsCheckMode = false;
			this.btnUp.IsDrawFrame = true;
			this.btnUp.IsMouseDown = false;
			this.btnUp.LeftBar = 0;
			this.btnUp.LineAlignment = System.Drawing.StringAlignment.Center;
			this.btnUp.Location = new System.Drawing.Point(50, 311);
			this.btnUp.MyFontIndex = 5;
			this.btnUp.MyFonts = null;
			this.btnUp.MyFontSize = 8.999999F;
			this.btnUp.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.btnUp.Name = "btnUp";
			this.btnUp.RightBar = 0;
			this.btnUp.Size = new System.Drawing.Size(40, 25);
			this.btnUp.TabIndex = 4;
			this.btnUp.Text = "Up";
			this.btnUp.TopBar = 0;
			// 
			// btnDown
			// 
			this.btnDown.Alignment = System.Drawing.StringAlignment.Center;
			this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDown.BackColor = System.Drawing.Color.Transparent;
			this.btnDown.BottomBar = 0;
			this.btnDown.Checked = false;
			this.btnDown.Color_Down = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
			this.btnDown.Color_Enter = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.btnDown.Color_line = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.btnDown.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnDown.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(250)))));
			this.btnDown.Id = 0;
			this.btnDown.IsCheckMode = false;
			this.btnDown.IsDrawFrame = true;
			this.btnDown.IsMouseDown = false;
			this.btnDown.LeftBar = 0;
			this.btnDown.LineAlignment = System.Drawing.StringAlignment.Center;
			this.btnDown.Location = new System.Drawing.Point(93, 311);
			this.btnDown.MyFontIndex = 5;
			this.btnDown.MyFonts = null;
			this.btnDown.MyFontSize = 8.999999F;
			this.btnDown.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.btnDown.Name = "btnDown";
			this.btnDown.RightBar = 0;
			this.btnDown.Size = new System.Drawing.Size(40, 25);
			this.btnDown.TabIndex = 5;
			this.btnDown.Text = "Down";
			this.btnDown.TopBar = 0;
			// 
			// btnEdit
			// 
			this.btnEdit.Alignment = System.Drawing.StringAlignment.Center;
			this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnEdit.BackColor = System.Drawing.Color.Transparent;
			this.btnEdit.BottomBar = 0;
			this.btnEdit.Checked = false;
			this.btnEdit.Color_Down = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
			this.btnEdit.Color_Enter = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.btnEdit.Color_line = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.btnEdit.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnEdit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(250)))));
			this.btnEdit.Id = 0;
			this.btnEdit.IsCheckMode = false;
			this.btnEdit.IsDrawFrame = true;
			this.btnEdit.IsMouseDown = false;
			this.btnEdit.LeftBar = 0;
			this.btnEdit.LineAlignment = System.Drawing.StringAlignment.Center;
			this.btnEdit.Location = new System.Drawing.Point(76, 0);
			this.btnEdit.MyFontIndex = 5;
			this.btnEdit.MyFonts = null;
			this.btnEdit.MyFontSize = 8.999999F;
			this.btnEdit.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.RightBar = 0;
			this.btnEdit.Size = new System.Drawing.Size(57, 25);
			this.btnEdit.TabIndex = 6;
			this.btnEdit.Text = "Edit";
			this.btnEdit.TopBar = 0;
			// 
			// T_BListBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.btnEdit);
			this.Controls.Add(this.btnDown);
			this.Controls.Add(this.btnUp);
			this.Controls.Add(this.btnDell);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.t_vScrBar1);
			this.Controls.Add(this.BList);
			this.MinimumSize = new System.Drawing.Size(145, 145);
			this.Name = "T_BListBox";
			this.Size = new System.Drawing.Size(157, 340);
			this.ResumeLayout(false);

		}

		#endregion

		private T_BList BList;
		private T_VScrBar t_vScrBar1;
		private T_Button btnAdd;
		private T_Button btnDell;
		private T_Button btnUp;
		private T_Button btnDown;
		private T_Button btnEdit;
	}
}
