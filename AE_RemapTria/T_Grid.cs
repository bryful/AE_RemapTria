using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AE_RemapTria
{
	public partial class T_Grid : T_ContolBase
	{
		public T_CellData CellData = new T_CellData();
		public T_Size Sizes = new T_Size();
		public T_Colors Colors = new T_Colors();


		// ************************************************************************************
		public T_Grid()
		{
			InitializeComponent();

			MFontInit();
			SetMyFont( 2, 14.0F);
			ChkMinMax();
			CellData.ValueChangedEvent += CellData_ValueChangedEvent;
			CellData.SelChangedEvent += CellData_SelChangedEvent;
			Sizes.ChangeGridSize += Sizes_ChangeGridSize;
			Sizes.ChangeDisp += Sizes_ChangeDisp;
			Sizes.ChangeDispMax += Sizes_ChangeDisp;
		}

		private void Sizes_ChangeDisp(object? sender, EventArgs e)
		{
			this.Invalidate();
		}

		private void Sizes_ChangeGridSize(object? sender, EventArgs e)
		{
			ChkMinMax();
			this.Invalidate();
		}

		// ************************************************************************************
		private void CellData_SelChangedEvent(object? sender, EventArgs e)
		{
			this.Invalidate();
		}
		// ************************************************************************************
		private void CellData_ValueChangedEvent(object? sender, EventArgs e)
		{
			this.Invalidate();
		}

		// ************************************************************************************
		private void ChkMinMax()
		{
			this.MinimumSize = new Size(Sizes.CellWidth * 6, Sizes.CellHeight * 6);
			this.MaximumSize = new Size(Sizes.CellWidth * CellData.CellCount, Sizes.CellHeight * CellData.FrameCount);
			Sizes.SetSize(this.Size, CellData);
		}
		// ************************************************************************************
		public void SetSize()
		{
			Sizes.SetSize(this.Size, CellData);
		}
		// ************************************************************************************
		protected override void OnResize(EventArgs e)
		{
			SetSize();
			/*
			if (m_HScrollBar != null)
			{
				m_HScrollBar.Maximum = m_GridSize.DispMax.X;
				m_HScrollBar.Value = m_GridSize.Disp.X;
			}
			if (m_VScrollBar != null)
			{
				m_VScrollBar.Maximum = m_GridSize.DispMax.Y;
				m_VScrollBar.Value = m_GridSize.Disp.Y;
			}
			*/
			base.OnResize(e);
			this.Invalidate();
		}
		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
			int ps = 1;
			Pen p = new Pen(Color.Black, ps);
			SolidBrush sb = new SolidBrush(Color.Transparent);
			try
			{
				Graphics g = pe.Graphics;
				g.SmoothingMode = SmoothingMode.AntiAlias;
				g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
				Fill(g, sb);

				PushFontStatus();
				Alignment = StringAlignment.Center;
				LineAlignment = StringAlignment.Center;
				Rectangle rct = this.Rect();
				sb.Color = Colors.Moji;
				DrawStr(g, "0123456789", sb, rct);
				p.Color = Colors.InputLine;
				DrawFrame(g, p, rct, 2);
				PopFontStatus();

			}
			finally
			{
				p.Dispose();
				sb.Dispose();
			}

		}
	}
}
