

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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.t_Grid1 = new AE_RemapTria.T_Grid();
			this.t_hScrol1 = new AE_RemapTria.T_HScrol();
			this.t_MyFonts1 = new AE_RemapTria.T_MyFonts(this.components);
			this.t_vScrol1 = new AE_RemapTria.T_VScrol();
			this.t_Input1 = new AE_RemapTria.T_Input();
			this.t_Menu1 = new AE_RemapTria.T_Menu();
			this.t_Caption1 = new AE_RemapTria.T_Caption();
			this.t_Frame1 = new AE_RemapTria.T_Frame();
			this.t_LabelInfo1 = new AE_RemapTria.T_LabelInfo();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// t_Grid1
			// 
			this.t_Grid1.Alignment = System.Drawing.StringAlignment.Center;
			this.t_Grid1.AllowDrop = true;
			this.t_Grid1.BackColor = System.Drawing.Color.Black;
			this.t_Grid1.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.t_Grid1.FrameCount = 72;
			this.t_Grid1.HScrol = this.t_hScrol1;
			this.t_Grid1.LineAlignment = System.Drawing.StringAlignment.Center;
			this.t_Grid1.Location = new System.Drawing.Point(94, 60);
			this.t_Grid1.MaximumSize = new System.Drawing.Size(360, 1152);
			this.t_Grid1.MinimumSize = new System.Drawing.Size(180, 96);
			this.t_Grid1.MyFontIndex = 5;
			this.t_Grid1.MyFonts = this.t_MyFonts1;
			this.t_Grid1.MyFontSize = 9F;
			this.t_Grid1.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.t_Grid1.Name = "t_Grid1";
			this.t_Grid1.Size = new System.Drawing.Size(334, 459);
			this.t_Grid1.TabIndex = 0;
			this.t_Grid1.Text = "t_Grid1";
			this.t_Grid1.VScrol = this.t_vScrol1;
			// 
			// t_hScrol1
			// 
			this.t_hScrol1.BackColor = System.Drawing.Color.Transparent;
			this.t_hScrol1.Grid = this.t_Grid1;
			this.t_hScrol1.Location = new System.Drawing.Point(94, 523);
			this.t_hScrol1.Maximum = 26;
			this.t_hScrol1.MaximumSize = new System.Drawing.Size(65536, 20);
			this.t_hScrol1.MinimumSize = new System.Drawing.Size(123, 20);
			this.t_hScrol1.Name = "t_hScrol1";
			this.t_hScrol1.Size = new System.Drawing.Size(334, 20);
			this.t_hScrol1.TabIndex = 4;
			this.t_hScrol1.Text = "t_hScrol1";
			this.t_hScrol1.Value = 0;
			// 
			// t_vScrol1
			// 
			this.t_vScrol1.BackColor = System.Drawing.Color.Transparent;
			this.t_vScrol1.Grid = this.t_Grid1;
			this.t_vScrol1.Location = new System.Drawing.Point(432, 60);
			this.t_vScrol1.Maximum = 693;
			this.t_vScrol1.MaximumSize = new System.Drawing.Size(20, 65536);
			this.t_vScrol1.MinimumSize = new System.Drawing.Size(20, 123);
			this.t_vScrol1.Name = "t_vScrol1";
			this.t_vScrol1.Size = new System.Drawing.Size(20, 459);
			this.t_vScrol1.TabIndex = 2;
			this.t_vScrol1.Text = "t_vScrol1";
			this.t_vScrol1.Value = 0;
			// 
			// t_Input1
			// 
			this.t_Input1.Alignment = System.Drawing.StringAlignment.Far;
			this.t_Input1.BackColor = System.Drawing.Color.Black;
			this.t_Input1.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.t_Input1.Grid = this.t_Grid1;
			this.t_Input1.LineAlignment = System.Drawing.StringAlignment.Center;
			this.t_Input1.Location = new System.Drawing.Point(20, 40);
			this.t_Input1.MyFontIndex = 5;
			this.t_Input1.MyFonts = this.t_MyFonts1;
			this.t_Input1.MyFontSize = 9F;
			this.t_Input1.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.t_Input1.Name = "t_Input1";
			this.t_Input1.Size = new System.Drawing.Size(70, 16);
			this.t_Input1.TabIndex = 1;
			this.t_Input1.Text = "t_Input1";
			this.t_Input1.Value = -1;
			// 
			// t_Menu1
			// 
			this.t_Menu1.Alignment = System.Drawing.StringAlignment.Center;
			this.t_Menu1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.t_Menu1.BackColor = System.Drawing.Color.Black;
			this.t_Menu1.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.t_Menu1.Form = this;
			this.t_Menu1.Grid = this.t_Grid1;
			this.t_Menu1.IsActive = false;
			this.t_Menu1.LineAlignment = System.Drawing.StringAlignment.Center;
			this.t_Menu1.Location = new System.Drawing.Point(0, 4);
			this.t_Menu1.MyFontIndex = 5;
			this.t_Menu1.MyFonts = this.t_MyFonts1;
			this.t_Menu1.MyFontSize = 8F;
			this.t_Menu1.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.t_Menu1.Name = "t_Menu1";
			this.t_Menu1.Size = new System.Drawing.Size(452, 20);
			this.t_Menu1.TabIndex = 3;
			// 
			// t_Caption1
			// 
			this.t_Caption1.Alignment = System.Drawing.StringAlignment.Center;
			this.t_Caption1.BackColor = System.Drawing.Color.Black;
			this.t_Caption1.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.t_Caption1.Grid = this.t_Grid1;
			this.t_Caption1.LineAlignment = System.Drawing.StringAlignment.Center;
			this.t_Caption1.Location = new System.Drawing.Point(94, 24);
			this.t_Caption1.MyFontIndex = 5;
			this.t_Caption1.MyFonts = this.t_MyFonts1;
			this.t_Caption1.MyFontSize = 9F;
			this.t_Caption1.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.t_Caption1.Name = "t_Caption1";
			this.t_Caption1.Size = new System.Drawing.Size(334, 32);
			this.t_Caption1.TabIndex = 5;
			this.t_Caption1.Text = "t_Caption1";
			// 
			// t_Frame1
			// 
			this.t_Frame1.Alignment = System.Drawing.StringAlignment.Far;
			this.t_Frame1.BackColor = System.Drawing.Color.Black;
			this.t_Frame1.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.t_Frame1.Grid = this.t_Grid1;
			this.t_Frame1.LineAlignment = System.Drawing.StringAlignment.Center;
			this.t_Frame1.Location = new System.Drawing.Point(0, 60);
			this.t_Frame1.MyFontIndex = 5;
			this.t_Frame1.MyFonts = this.t_MyFonts1;
			this.t_Frame1.MyFontSize = 9F;
			this.t_Frame1.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.t_Frame1.Name = "t_Frame1";
			this.t_Frame1.Size = new System.Drawing.Size(90, 459);
			this.t_Frame1.TabIndex = 6;
			this.t_Frame1.Text = "t_Frame1";
			// 
			// t_LabelInfo1
			// 
			this.t_LabelInfo1.Alignment = System.Drawing.StringAlignment.Far;
			this.t_LabelInfo1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.t_LabelInfo1.BackColor = System.Drawing.Color.Transparent;
			this.t_LabelInfo1.BottomBar = new System.Drawing.Size(0, 0);
			this.t_LabelInfo1.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.t_LabelInfo1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
			this.t_LabelInfo1.Grid = this.t_Grid1;
			this.t_LabelInfo1.LeftBar = new System.Drawing.Size(0, 0);
			this.t_LabelInfo1.LineAlignment = System.Drawing.StringAlignment.Center;
			this.t_LabelInfo1.Location = new System.Drawing.Point(24, 523);
			this.t_LabelInfo1.MyFontIndex = 5;
			this.t_LabelInfo1.MyFonts = this.t_MyFonts1;
			this.t_LabelInfo1.MyFontSize = 8F;
			this.t_LabelInfo1.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.t_LabelInfo1.Name = "t_LabelInfo1";
			this.t_LabelInfo1.RightBar = new System.Drawing.Size(0, 0);
			this.t_LabelInfo1.Size = new System.Drawing.Size(66, 12);
			this.t_LabelInfo1.TabIndex = 7;
			this.t_LabelInfo1.Text = "3+0:24";
			this.t_LabelInfo1.TopBar = new System.Drawing.Size(0, 0);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(61, 10);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(57, 6);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(456, 547);
			this.Controls.Add(this.t_LabelInfo1);
			this.Controls.Add(this.t_Frame1);
			this.Controls.Add(this.t_Caption1);
			this.Controls.Add(this.t_hScrol1);
			this.Controls.Add(this.t_Menu1);
			this.Controls.Add(this.t_vScrol1);
			this.Controls.Add(this.t_Input1);
			this.Controls.Add(this.t_Grid1);
			this.Grid = this.t_Grid1;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Input = this.t_Input1;
			this.MaximumSize = new System.Drawing.Size(486, 1241);
			this.MinimumSize = new System.Drawing.Size(310, 193);
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "AE_Remap τρία";
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private T_Grid t_Grid1;
		private T_Input t_Input1;
		private T_VScrol t_vScrol1;
		private T_MyFonts t_MyFonts1;
		private T_Menu t_Menu1;
		private T_HScrol t_hScrol1;
		private T_Caption t_Caption1;
		private T_Frame t_Frame1;
		private T_LabelInfo t_LabelInfo1;
		private ContextMenuStrip contextMenuStrip1;
		private ToolStripSeparator toolStripMenuItem1;
	}
}