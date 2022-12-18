namespace AE_RemapTria
{
	partial class Form2
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
			this.tR_EditColorList1 = new AE_RemapTria.TR_EditColorList();
			this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
			this.SuspendLayout();
			// 
			// tR_EditColorList1
			// 
			this.tR_EditColorList1.Alignment = System.Drawing.StringAlignment.Far;
			this.tR_EditColorList1.Back = System.Drawing.Color.Transparent;
			this.tR_EditColorList1.BackColor = System.Drawing.Color.Transparent;
			this.tR_EditColorList1.CaptionWidth = 155;
			this.tR_EditColorList1.DispValue = 200;
			this.tR_EditColorList1.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.tR_EditColorList1.Frame = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
			this.tR_EditColorList1.FrameMargin = new System.Windows.Forms.Padding(0);
			this.tR_EditColorList1.FrameWeight = new System.Windows.Forms.Padding(0);
			this.tR_EditColorList1.LineAlignment = System.Drawing.StringAlignment.Center;
			this.tR_EditColorList1.Location = new System.Drawing.Point(79, 56);
			this.tR_EditColorList1.MyFontIndex = 5;
			this.tR_EditColorList1.MyFontSize = 5F;
			this.tR_EditColorList1.MyFontStyle = System.Drawing.FontStyle.Regular;
			this.tR_EditColorList1.Name = "tR_EditColorList1";
			this.tR_EditColorList1.Size = new System.Drawing.Size(328, 443);
			this.tR_EditColorList1.TabIndex = 0;
			this.tR_EditColorList1.Text = "tR_EditColorList1";
			// 
			// vScrollBar1
			// 
			this.vScrollBar1.Location = new System.Drawing.Point(424, 325);
			this.vScrollBar1.Name = "vScrollBar1";
			this.vScrollBar1.Size = new System.Drawing.Size(17, 80);
			this.vScrollBar1.TabIndex = 1;
			// 
			// Form2
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(480, 560);
			this.Controls.Add(this.vScrollBar1);
			this.Controls.Add(this.tR_EditColorList1);
			this.Name = "Form2";
			this.SideCount = new int[] {
        4,
        4,
        1};
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
        1,
        2,
        4};
			this.Text = "Form2";
			this.ResumeLayout(false);

		}

		#endregion

		private TR_EditColorList tR_EditColorList1;
		private VScrollBar vScrollBar1;
	}
}