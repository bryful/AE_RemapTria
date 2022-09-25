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
#pragma warning disable CS8603 // Null 参照戻り値である可能性があります。
	public partial class T_Grid : T_ControlBase
	{
		public T_CellData CellData = new T_CellData();
		public T_Size Sizes = new T_Size();
		public T_Colors Colors = new T_Colors();

		private T_HScrol? m_HScrol = null;
		private T_VScrol? m_VScrol = null;

		// ************************************************************************************
		public T_Grid()
		{
			InitializeComponent();

			Init();
			MFontInit();
			SetMyFont( 2, 14.0F);
			ChkMinMax();
			CellData.ValueChangedEvent += CellData_ValueChangedEvent;
			CellData.SelChangedEvent += CellData_SelChangedEvent;
			Sizes.ChangeGridSize += Sizes_ChangeGridSize;
			Sizes.ChangeDisp += Sizes_ChangeDisp;
			Sizes.ChangeDispMax += Sizes_ChangeDisp;
		}
		protected override void InitLayout()
		{
			base.InitLayout();
			SetSize();
			ChkMinMax();
			ChkHScrl();
			ChkVScrl();
			//ChkGrid();
		}
		// ************************************************************************************
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
		//-------------------------------------------------
		public T_HScrol HScrol
		{
			get { return m_HScrol; }
			set
			{
				m_HScrol = value;
				ChkHScrl();
			}
		}
		private void ChkHScrl()
		{
			if (m_HScrol != null)
			{
				m_HScrol.ValueChangedEvent += M_HScrol_ValueChangedEvent;
				m_HScrol.Maximum = Sizes.DispMax.X;
				m_HScrol.Value = Sizes.Disp.X;
			}

		}


		//-------------------------------------------------------------
		public T_VScrol VScrol
		{
			get { return m_VScrol; }
			set
			{
				m_VScrol = value;
				ChkVScrl();
			}
		}
		private void ChkVScrl()
		{
			if (m_VScrol != null)
			{
				m_VScrol.ValueChangedEvent += M_HScrol_ValueChangedEvent;
				m_VScrol.Maximum = Sizes.DispMax.Y;
				m_VScrol.Value = Sizes.Disp.Y;
			}

		}
		// ************************************************************************************

		private void M_HScrol_ValueChangedEvent(object? sender, EventArgs e)
		{
			this.Invalidate();
		}

		protected override void OnResize(EventArgs e)
		{
			SetSize();
			if (m_HScrol != null)
			{
				m_HScrol.Maximum = Sizes.DispMax.X;
				m_HScrol.Value = Sizes.Disp.X;
			}
			if (m_VScrol != null)
			{
				m_VScrol.Maximum = Sizes.DispMax.Y;
				m_VScrol.Value = Sizes.Disp.Y;
			}
			base.OnResize(e);
			this.Invalidate();
		}
		//-------------------------------------------------
		private void DrawCell(Graphics g, SolidBrush sb, Pen p, int l, int f)
		{
			int x = l * Sizes.CellWidth - Sizes.Disp.X;
			int y = f * Sizes.CellHeight - Sizes.Disp.Y;

			Rectangle r = new Rectangle(x, y, Sizes.CellWidth, Sizes.CellHeight);
			//塗りつぶし
			bool IsSel = (CellData.IsSelected(l, f));
			if (IsSel == true)
			{
				sb.Color = Colors.Selection;
			}
			else
			{
				if (l % 2 == 0)
				{
					sb.Color = Colors.CellA;

				}
				else
				{
					sb.Color = Colors.CellA_sdw;
				}
			}
			g.FillRectangle(sb, r);

			//
			p.Color = Colors.LineB;
			p.Width = 1;
			int x2 = x + Sizes.CellWidth - 1;
			int y2 = y + Sizes.CellHeight - 1;
			//横線
			DrawHorLine(g, p, x, x2, y);
			//縦線
			DrawVerLine(g, p, x, y, y2);
			if (IsSel == true)
			{
				p.Color = Colors.LineA;
				//DrawVerLine(g, p, x, y, y2);
				//DrawVerLine(g, p, x2, y, y2);
			}

			switch (CellData.FrameRate)
			{
				case T_Fps.FPS24:
					if (f % 24 == 0)
					{
						p.Color = Colors.Line;
						DrawHorLine(g, p, x, x2, y);
						DrawHorLine(g, p, x, x2, y - 1);
					}
					else if (f % 12 == 0)
					{
						p.Color = Colors.Line;
						DrawHorLine(g, p, x, x2, y);

					}
					else
					{
						if (f % 6 == 0)
						{
							p.Color = Colors.LineA;
							DrawHorLine(g, p, x, x2, y);
						}
					}
					break;
				case T_Fps.FPS30:
					break;
			}
		}
		protected override void OnPaint(PaintEventArgs pe)
		{
			Pen p = new Pen(Color.Black);
			SolidBrush sb = new SolidBrush(Color.White);
			try
			{
				Graphics g = pe.Graphics;
				g.SmoothingMode = SmoothingMode.AntiAlias;
				g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
				sb.Color = Colors.Base;
				Fill(g, sb);
				Rectangle r = Sizes.DispCell;
				for (int j = r.Top; j <= r.Bottom; j++)
				{
					for (int i = r.Left; i <= r.Right; i++)
					{
						DrawCell(g, sb, p, i, j);
					}

				}

				p.Color = Colors.Line;
				DrawFrame(g, p);
			}
			finally
			{
				p.Dispose();
				sb.Dispose();
			}


		}
	}
#pragma warning restore CS8603 // Null 参照戻り値である可能性があります。
}
