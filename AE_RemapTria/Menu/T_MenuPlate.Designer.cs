﻿namespace AE_RemapTria
{
	partial class T_MenuPlate
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
			this.SuspendLayout();
			// 
			// T_MenuPlate
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(199, 262);
			this.DoubleBuffered = true;
			this.Edge = new System.Drawing.Rectangle(10, 5, 0, 0);
			this.Font = new System.Drawing.Font("源ノ角ゴシック Code JP R", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.ForeColor = System.Drawing.Color.White;
			this.KagiWeight = 0;
			this.MyFonts = this.t_MyFonts1;
			this.Name = "T_MenuPlate";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
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
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
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
			this.Text = "T_MenuPlate";
			this.TransparencyKey = System.Drawing.Color.Empty;
			this.ResumeLayout(false);

		}

		#endregion

		private T_MyFonts t_MyFonts1;
	}
}