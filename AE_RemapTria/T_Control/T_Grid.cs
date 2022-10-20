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
	public partial class T_Grid : T_BaseControl
	{
		public bool IsMultExecute = false;
		private bool m_IsJapanOS = true;
		public bool IsJapanOS { get { return m_IsJapanOS; } }

		public T_CellData CellData = new T_CellData();
		public T_Size Sizes = new T_Size();
		public T_Colors Colors = new T_Colors();
		public T_Funcs Funcs = new T_Funcs();

		public int FrameCount
		{
			get { return CellData.FrameCount; }
			set
			{
				if(CellData.FrameCount!=value)
				{
					CellData.FrameCount = value;
					ChkMinMax();
				}
			}
		}

		private T_HScrol? m_HScrol = null;
		private T_VScrol? m_VScrol = null;

		private T_Input? m_Input = null;
		public void SetT_Input(T_Input ti)
		{
			m_Input = ti;
		}
		private T_Caption? m_Caption = null;
		public void SetT_Caption(T_Caption ti)
		{
			m_Caption = ti;
		}
		private T_Frame? m_Frame = null;
		public void SetT_Frame(T_Frame ti)
		{
			m_Frame = ti;
		}
		private T_Menu? m_Menu = null;
		public void SetT_Menu(T_Menu tm)
		{
			m_Menu = tm;
		}
		private T_Form? m_Form = null;
		public void SetForm(T_Form fm)
		{
			m_Form = fm;
		}
		public string FileName = "";
		// ************************************************************************************
		public T_Grid()
		{
			m_IsJapanOS = (System.Globalization.CultureInfo.CurrentUICulture.Name== "ja-JP");
			InitializeComponent();

			Init();
			ChkMinMax();

			MyFontSize = 9;
			Alignment = StringAlignment.Center;

			MakeFuncs();

			CellData.ValueChanged += CellData_ValueChangedEvent;
			CellData.SelChanged += CellData_SelChangedEvent;
			CellData.CountChanged += CellData_CountChanged;
			Sizes.ChangeGridSize += Sizes_ChangeGridSize;
			Sizes.ChangeDisp += Sizes_ChangeDisp;
			Sizes.ChangeDispMax += Sizes_ChangeDisp;
			this.AllowDrop = true;
		}

		private void CellData_CountChanged(object? sender, EventArgs e)
		{
			ChkMinMax();
			ChkHScrl();
			ChkVScrl();
		}

		protected override void InitLayout()
		{
			base.InitLayout();
			ChkMinMax();
			ChkHScrl();
			ChkVScrl();
			//ChkGrid();
			MyFontSize = 9;
			Alignment = StringAlignment.Center;
		}
		// ************************************************************************************
		private void Sizes_ChangeDisp(object? sender, EventArgs e)
		{
			if (m_Frame != null) m_Frame.Invalidate();
			this.Invalidate();
		}

		private void Sizes_ChangeGridSize(object? sender, EventArgs e)
		{
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
			this.MinimumSize = new Size(
				Sizes.CellWidth * 6, 
				Sizes.CellHeight * 6);
			this.MaximumSize = new Size(
				Sizes.CellWidth * CellData.CellCount, 
				Sizes.CellHeight * CellData.FrameCountTrue);
			SizeSetting();

		}
		// ************************************************************************************
		public void SizeSetting()
		{
			Sizes.SizeSetting(this.Size, CellData);
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
				Point p = new Point(
					this.Left,
					this.Top + this.Height + Sizes.InterHeight);
				if (m_HScrol.Location != p) m_HScrol.Location = p;
				if (m_HScrol.Width != this.Width) m_HScrol.Width = this.Width; 
				
				m_HScrol.ValueChangedEvent += M_VScrol_ValueChangedEvent;
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
				Point p = new Point(
					this.Left + this.Width + Sizes.InterWidth,
					this.Top);
				if (m_VScrol.Location != p) m_VScrol.Location = p;
				if (m_VScrol.Height != this.Height) m_VScrol.Height = this.Height; 
				m_VScrol.ValueChangedEvent += M_VScrol_ValueChangedEvent;
				m_VScrol.Maximum = Sizes.DispMax.Y;
				m_VScrol.Value = Sizes.Disp.Y;
			}

		}

		// ************************************************************************************
		private void M_VScrol_ValueChangedEvent(object? sender, EventArgs e)
		{
			if (m_VScrol != null) {
				Sizes.DispY = m_VScrol.Value;
				this.Invalidate();
			}
			if (m_HScrol != null)
			{
				Sizes.DispX = m_HScrol.Value;
				this.Invalidate();
			}
		}

		// ************************************************************************************
		protected override void OnResize(EventArgs e)
		{
			SizeSetting();
			if (m_HScrol != null)
			{
				Point p = new Point(
					this.Left,
					this.Top + this.Height + Sizes.InterHeight);
				if (m_HScrol.Location != p) m_HScrol.Location = p;
				if(m_HScrol.Width != this.Width) m_HScrol.Width = this.Width;

				m_HScrol.Maximum = Sizes.DispMax.X;
				m_HScrol.Value = Sizes.Disp.X;
			}
			if (m_VScrol != null)
			{
				Point p = new Point(
					this.Left+this.Width+ Sizes.InterWidth,
					this.Top);
				if (m_VScrol.Location != p) m_VScrol.Location = p;
				if (m_VScrol.Height != this.Height) m_VScrol.Height = this.Height;

				m_VScrol.Maximum = Sizes.DispMax.Y;
				m_VScrol.Value = Sizes.Disp.Y;
			}
			if (m_Caption != null) m_Caption.SetLoc();
			if (m_Frame != null) m_Frame.SetLocSize();
			base.OnResize(e);
			this.Invalidate();
		}
		//-------------------------------------------------
		public Point PosCell(int x,int y)
		{
			return  Sizes.PosCell(x,y);
		}
		//-------------------------------------------------
		private int m_mdFrame = -1;
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				Point cp = Sizes.PosCell(e.X, e.Y);
				if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
				{
					int y0 = CellData.Selection.Start;
					int y1 = CellData.Selection.LastFrame;
					if(cp.Y <= y0)
					{
						y0 = cp.Y;
					}
					else
					{ 
						y1 = cp.Y;
					}
					CellData.PushUndo(BackupSratus.SelectionChange);
					CellData.Selection.Set2Frame(y0,y1);
				}else if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
				{
					CellData.PushUndo(BackupSratus.SelectionChange);
					CellData.Selection.SelToEnd();
				}
				else
				{
					m_mdFrame = cp.Y;

					CellData.PushUndo(BackupSratus.SelectionChange);
					CellData.Selection.SetTargetStartLength(cp.X, cp.Y, 1);
				}
				this.Invalidate();
				Sizes.CallOnChangeDisp();
			}
			base.OnMouseDown(e);
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				if(m_mdFrame>=0)
				{
					if(e.Y>=this.Height)
					{
						Sizes.DispY += Sizes.CellHeight;
					}else if(e.Y<0)
					{
						Sizes.DispY -= Sizes.CellHeight;
					}
					Point cp = Sizes.PosCell(e.X, e.Y);
					CellData.Selection.Set2Frame(m_mdFrame, cp.Y);
					this.Invalidate();
					Sizes.CallOnChangeDisp();
				}
			}
			base.OnMouseMove(e);
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if(m_mdFrame>=0)
			{
				m_mdFrame = -1;
				this.Invalidate();
			}
			base.OnMouseUp(e);
		}
		//-------------------------------------------------
		private void DrawCell(Graphics g, SolidBrush sb, Pen p, int l, int f)
		{
			int x = l * Sizes.CellWidth - Sizes.Disp.X;
			int y = f * Sizes.CellHeight - Sizes.Disp.Y;

			Rectangle r = new Rectangle(x, y, Sizes.CellWidth, Sizes.CellHeight);
			//塗りつぶし
			bool IsSel = (CellData.IsSelected(l, f));
			bool IsUnEnabled = !CellData.EnableFrame(f);
			if (IsSel == true)
			{
				sb.Color = Colors.Selection;
			}
			else if (IsUnEnabled)
			{
				sb.Color = Colors.Gray;
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

			CellSatus cs = CellData.GetCellStatus(l, f);
			switch (cs.Status)
			{
				case CellType.Normal:
					if (IsUnEnabled) sb.Color = Colors.GrayMoji;
					else sb.Color = Colors.Moji;
					DrawStr(g, CellData.GetCellData(l, f).ToString(), sb, r);
					break;
				case CellType.SameAsBefore:
					if (IsUnEnabled) p.Color = Colors.GrayMoji;
					else p.Color = Colors.Moji;
					DrawVerLine(g,p, r.Left + r.Width / 2, r.Top, r.Bottom);
					break;
				case CellType.EmptyStart:
					if (IsUnEnabled) p.Color = Colors.GrayMoji;
					else p.Color = Colors.Moji;
					DrawBatsu(g, p, r);
					break;
				case CellType.None:
					break;
			}

		}
		protected override void OnPaint(PaintEventArgs pe)
		{
			Pen p = new Pen(Color.Black);
			SolidBrush sb = new SolidBrush(Color.White);
			Graphics g = pe.Graphics;
			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
			try
			{
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
				// *************************************
				p.Width = 1;
				int Sec = 24;
				int HSec = 12;
				int HHSec = 6;
				switch (CellData.FrameRate)
				{
					case T_Fps.FPS24:
						Sec = 24;
						HSec = 12;
						HHSec = 6;
						break;
					case T_Fps.FPS30:
						Sec = 30;
						HSec = 15;
						HHSec = 5;
						break;
				}
				for (int j = r.Top; j <= r.Bottom; j++)
				{
					int y = j * Sizes.CellHeight - Sizes.DispY;
					if (j % Sec == 0)
					{
						p.Color = Colors.Line;
						DrawHorLine(g, p, 0, this.Width, y);
						DrawHorLine(g, p, 0, this.Width, y - 1);
					}
					else if (j % HSec == 0)
					{
						p.Color = Colors.Line;
						DrawHorLine(g, p, 0, this.Width, y);

					}
					else
					{
						if (j % HHSec == 0)
						{
							p.Color = Colors.LineA;
							DrawHorLine(g, p, 0, this.Width, y);
						}
						else
						{
							p.Color = Colors.LineB;
							DrawHorLine(g, p, 0, this.Width, y);
						}
					}
				}
				//******************************************
				for (int i = r.Left; i <= r.Right; i++)
				{
					int x = i * Sizes.CellWidth- Sizes.DispX;
					DrawVerLine(g, p, x, 0, this.Height);
				}
				//****************************************

				p.Color = Colors.Line;
				DrawFrame(g, p);
			}
			finally
			{
				p.Dispose();
				sb.Dispose();
			}


		}
		protected override void OnDragEnter(DragEventArgs drgevent)
		{
			if (drgevent != null)
			{
#pragma warning disable CS8602 // null 参照の可能性があるものの逆参照です。
				if (drgevent.Data.GetDataPresent(DataFormats.FileDrop))
				{
					drgevent.Effect = DragDropEffects.All;
				}
				else
				{
					drgevent.Effect = DragDropEffects.None;
				}
#pragma warning restore CS8602 // null 参照の可能性があるものの逆参照です。
			}
			//base.OnDragEnter(drgevent);
		}
		protected override void OnDragDrop(DragEventArgs drgevent)
		{
			if (drgevent != null)
			{
				string[] files = (string[])drgevent.Data.GetData(DataFormats.FileDrop, false);
				foreach(string file in files)
				{
					if(Open(file)==true)
					{
						break;
					}
				}
			}
			//base.OnDragDrop(drgevent);
		}
	}
#pragma warning restore CS8603 // Null 参照戻り値である可能性があります。
}
