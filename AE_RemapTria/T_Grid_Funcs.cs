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
	partial class T_Grid
	{
		// ************************************************************************************
		private void MakeFuncs()
		{
			List<FuncItem> lst = new List<FuncItem>();

			lst.Add(new FuncItem(Input0, "Input0", Keys.D0 , Keys.NumPad0));
			lst.Add(new FuncItem(Input1, "Input1", Keys.D1, Keys.NumPad1));
			lst.Add(new FuncItem(Input2, "Input2", Keys.D2, Keys.NumPad2));
			lst.Add(new FuncItem(Input3, "Input3", Keys.D3, Keys.NumPad3));
			lst.Add(new FuncItem(Input4, "Input4", Keys.D4, Keys.NumPad4));
			lst.Add(new FuncItem(Input5, "Input5", Keys.D5, Keys.NumPad5));
			lst.Add(new FuncItem(Input6, "Input6", Keys.D6, Keys.NumPad6));
			lst.Add(new FuncItem(Input7, "Input7", Keys.D7, Keys.NumPad7));
			lst.Add(new FuncItem(Input8, "Input8", Keys.D8, Keys.NumPad8));
			lst.Add(new FuncItem(Input9, "Input9", Keys.D9, Keys.NumPad9));
			lst.Add(new FuncItem(InputClear, "InputClear", Keys.Delete));
			lst.Add(new FuncItem(InputBS, "InputBS", Keys.Back));
			lst.Add(new FuncItem(InputEnter, "InputEnter", Keys.Enter,Keys.Return));
			lst.Add(new FuncItem(InputEmpty, "Inputempty", Keys.Decimal, Keys.OemPeriod));
			lst.Add(new FuncItem(InputInc, "InputInc", Keys.Add));
			lst.Add(new FuncItem(InputDec, "InputDec", Keys.Subtract));
			lst.Add(new FuncItem(SelMoveUp, "SelMoveUp", Keys.Up));
			lst.Add(new FuncItem(SelMoveDown, "SelMoveDown", Keys.Down));
			lst.Add(new FuncItem(SelMoveLeft, "SelMoveLeft", Keys.Left));
			lst.Add(new FuncItem(SelMoveRight, "SelMoveRight", Keys.Right));
			lst.Add(new FuncItem(SelAdd, "SelAdd", Keys.Shift| Keys.Down,Keys.Multiply));
			lst.Add(new FuncItem(SelDec, "SelDec", Keys.Shift | Keys.Up,Keys.Divide));
			lst.Add(new FuncItem(Undo, "Undo", Keys.Z|Keys.Control));
			lst.Add(new FuncItem(Open, "Open", Keys.Control | Keys.O));
			lst.Add(new FuncItem(ToggleFrameEnabled, "ToggleFrameEnabled", Keys.Control | Keys.Oemtilde));
			lst.Add(new FuncItem(HeightMax, "HeightMax", Keys.Control | Keys.Oem5));

			Funcs.SetFuncs(lst.ToArray());
		}
		// ************************************************************************************
		public void MakeMenu()
		{
			if (m_Menu == null) return;
			m_Menu.AddMenu("AE_RemapTria", 93);
			m_Menu.AddMenu("Edit", 40);
			m_Menu.AddSubMenu(0, "Open ctrl+O", Open);
			m_Menu.AddSubMenu(0, "Quit ctrl+Q", Quit);
			m_Menu.AddSubMenu(1, "Undo ctrl+Z", Undo);
			m_Menu.AddSubMenu(1, "ToggleFrameEnabled ctrl+@", ToggleFrameEnabled);
			m_Menu.AddSubMenu(1, "HeightMax ctrl+\\", HeightMax);
		}
		public void T_Menu_MenuExec(object sender, MenuEventArgs e)
		{
			if(m_Menu == null) return;
			if(m_Menu.Funcs[e.Id]()==true)
			{
				this.Invalidate();
			}
		}

		// ************************************************************************************
		public bool Open()
		{
			MessageBox.Show("Open");
			return true;
		}
		// ************************************************************************************
		public bool Quit()
		{
			Application.Exit();
			return true;
		}
		public bool Undo()
		{
			CellData.PopUndo();
			return true;
		}
		// ************************************************************************************
		public bool ToggleFrameEnabled()
		{
			return CellData.ToggleFrameEnabled();
		}
		// ************************************************************************************
		public bool Inputnum(int v)
		{
			bool ret = false;
			if(m_Input!=null) ret = m_Input.InputAddKey(v);
			return ret;
		}
		public bool Input0() { return Inputnum(0); }
		public bool Input1() { return Inputnum(1); }
		public bool Input2() { return Inputnum(2); }
		public bool Input3() { return Inputnum(3); }
		public bool Input4() { return Inputnum(4); }
		public bool Input5() { return Inputnum(5); }
		public bool Input6() { return Inputnum(6); }
		public bool Input7() { return Inputnum(7); }
		public bool Input8() { return Inputnum(8); }
		public bool Input9() { return Inputnum(9); }
		public bool InputClear()
		{
			bool ret = false;
			if (m_Input != null)
			{
				if (m_Input.Value >= 0)
				{
					ret = m_Input.InputClear();
				}
				else
				{
					CellData.SetCellNumEmpty(false);
					ret = true;
				}
			}
			return ret;
		}
		public bool InputBS()
		{
			bool ret = false;
			if (m_Input != null)
			{
				if (m_Input.Value >= 0)
				{
					ret = m_Input.InputBS();
				}
				else
				{
					CellData.SetCellNumBS();
					ret = true;
				}
			}
			return ret;
		}
		public bool InputEmpty()
		{
			bool ret = false;
			if (m_Input != null)
			{
				CellData.SetCellNumEmpty(true);
				ret = true;
			}
			return ret;
		}
		public void ChkSelectionV()
		{
			int y0 = CellData.Selection.Start * Sizes.CellHeight;
			int y1 = (CellData.Selection.Start+ CellData.Selection.Length) * Sizes.CellHeight;

			int t0 = y0 - Sizes.DispY;
			int t1 = y1 - Sizes.DispY;
			if (t0>=this.Height)
			{
				int v = (CellData.Selection.Start + CellData.Selection.Length +1) * Sizes.CellHeight - this.Height;
				if (v > Sizes.DispMax.Y - Sizes.CellHeight * 3) v = Sizes.DispMax.Y;
				Sizes.DispY = v;
			}
			else if(t1<=0)
			{
				int v = (CellData.Selection.Start) * Sizes.CellHeight;
				if (v < Sizes.CellHeight * 3) v = 0;
				Sizes.DispY = v;
			}

		}
		public void ChkSelectionH()
		{
			int x0 = CellData.Selection.Target * Sizes.CellWidth;
			int x1 = x0 + Sizes.CellWidth;

			int t0 = x0 - Sizes.DispX;
			int t1 = x1 - Sizes.DispX;
			if (t1 >= this.Width)
			{
				int v = Sizes.CellWidth * (CellData.Selection.Target + 1) - this.Width;
				if (v >= Sizes.DispMax.X - Sizes.CellWidth/2 ) v = Sizes.DispMax.X;
				Sizes.DispX = v;
			}
			else if (t0 <= 0)
			{
				int v = CellData.Selection.Target * Sizes.CellWidth;
				if (v < Sizes.CellHeight/2 ) v = 0;
				Sizes.DispX = v;
			}

		}
		public bool InputEnter()
		{
			bool ret = false;
			if (m_Input != null)
			{
				if(m_Input.Value>=0)
				{
					CellData.SetCellNum(m_Input.Value);
					m_Input.InputClear();
					ret = true;
					ChkSelectionV();
				}
				else
				{
					ret = InputSame();
				}
			}
			return ret;

		}
		public bool InputSame()
		{
			CellData.SetCellNumSame();
			ChkSelectionV();
			return true;
		}
		public bool InputInc()
		{
			CellData.SetCellNumInc();
			ChkSelectionV();
			return true;
		}
		public bool InputDec()
		{
			CellData.SetCellNumDec();
			ChkSelectionV();
			return true;
		}

		// ************************************************************************************
		public bool SelMoveDown()
		{
			CellData.PushUndo(BackupSratus.SelectionChange);
			bool ret = CellData.Selection.MoveDown();
			ChkSelectionV();
			return ret;
		}
		// ************************************************************************************
		public bool SelMoveUp()
		{
			CellData.PushUndo(BackupSratus.SelectionChange);
			bool ret = CellData.Selection.MoveUp();
			ChkSelectionV();
			return ret;
		}
		// ************************************************************************************
		public bool SelMoveRight()
		{
			CellData.PushUndo(BackupSratus.SelectionChange);
			bool b = CellData.Selection.MoveRight();
			ChkSelectionH();
			return b;
		}
		public bool SelMoveLeft()
		{
			CellData.PushUndo(BackupSratus.SelectionChange);
			bool b = CellData.Selection.MoveLeft();
			ChkSelectionH();
			return b;
		}
		public bool SelAdd()
		{
			return CellData.SelectionAdd(1);
		}
		public bool SelDec()
		{
			bool ret = false;
			if (CellData.Selection.Length > 1)
			{
				ret = CellData.SelectionAdd(-1);
			}
			return ret;
		}
		public bool SelectionAll()
		{
			bool ret = false;
			if (CellData.Selection.Length < CellData.FrameCount)
			{
				CellData.PushUndo(BackupSratus.SelectionChange);
				ret = CellData.SelectionAll();
			}
			return ret;
		}
		// ************************************************************************************
		public bool HeightMax()
		{
			bool ret = false;
			if (m_Form != null)
			{
				ret = m_Form.HeightMax();
			}
			return ret;
		}
		// ************************************************************************************
	}
}
