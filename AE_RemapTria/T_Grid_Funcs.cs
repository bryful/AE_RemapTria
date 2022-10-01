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

			lst.Add(new FuncItem(Undo, "Undo", Keys.Z | Keys.Control));
			lst.Add(new FuncItem(Input0, "Input0", Keys.D0, Keys.NumPad0));
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
			lst.Add(new FuncItem(InputEnter, "InputEnter", Keys.Enter, Keys.Return));
			lst.Add(new FuncItem(InputEmpty, "Inputempty", Keys.Decimal, Keys.OemPeriod));
			lst.Add(new FuncItem(InputInc, "InputInc", Keys.Add));
			lst.Add(new FuncItem(InputDec, "InputDec", Keys.Subtract));
			lst.Add(new FuncItem(SelMoveUp, "SelMoveUp", Keys.Up));
			lst.Add(new FuncItem(SelMoveDown, "SelMoveDown", Keys.Down));
			lst.Add(new FuncItem(SelMoveLeft, "SelMoveLeft", Keys.Left));
			lst.Add(new FuncItem(SelMoveRight, "SelMoveRight", Keys.Right));
			lst.Add(new FuncItem(SelAdd, "SelAdd", Keys.Shift | Keys.Down, Keys.Multiply));
			lst.Add(new FuncItem(SelDec, "SelDec", Keys.Shift | Keys.Up, Keys.Divide));
			lst.Add(new FuncItem(PageUp, "PageUp", Keys.PageUp));
			lst.Add(new FuncItem(PageDown, "PageUp", Keys.PageDown));
			lst.Add(new FuncItem(DispUp, "DispUp", Keys.Up | Keys.Control));
			lst.Add(new FuncItem(DispDown, "DispDown", Keys.Down | Keys.Control));
			lst.Add(new FuncItem(DispLeft, "DispLeft", Keys.Left | Keys.Control));
			lst.Add(new FuncItem(DispRight, "DispRight", Keys.Right | Keys.Control));
			lst.Add(new FuncItem(Home, "Home", Keys.Home));
			lst.Add(new FuncItem(End, "End", Keys.End));

			lst.Add(new FuncItem(Open, "Open", Keys.Control | Keys.O));
			lst.Add(new FuncItem(ToggleFrameEnabled, "ToggleFrameEnabled", Keys.Control | Keys.Oemtilde));
			lst.Add(new FuncItem(HeightMax, "HeightMax", Keys.Control | Keys.Oem5));
			lst.Add(new FuncItem(SheetSettings, "SheetSettings", Keys.Control | Keys.K));
			lst.Add(new FuncItem(SetSelection1, "SetSelection1",  Keys.F1));
			lst.Add(new FuncItem(SetSelection2, "SetSelection2", Keys.F2));
			lst.Add(new FuncItem(SetSelection3, "SetSelection3", Keys.F3));
			lst.Add(new FuncItem(SetSelection4, "SetSelection4", Keys.F4));
			lst.Add(new FuncItem(SetSelection5, "SetSelection5", Keys.F5));
			lst.Add(new FuncItem(SetSelection6, "SetSelection6", Keys.F6));
			lst.Add(new FuncItem(SetSelection7, "SetSelection7", Keys.F7));
			lst.Add(new FuncItem(SetSelection8, "SetSelection8", Keys.F8));
			lst.Add(new FuncItem(SetSelection9, "SetSelection9", Keys.F9));
			lst.Add(new FuncItem(SetSelection10, "SetSelection10", Keys.F10));
			lst.Add(new FuncItem(SetSelection11, "SetSelection11", Keys.F11));
			lst.Add(new FuncItem(SetSelection12, "SetSelection12", Keys.F12));

			Funcs.SetFuncs(lst.ToArray());
		}
		// ************************************************************************************
		public void MakeMenu()
		{
			if (m_Menu == null) return;
			m_Menu.AddMenu("AE_RemapTria", 93);
			m_Menu.AddMenu("Edit", 40);
			m_Menu.AddSubMenu(0, "Seet Setting ctrl+k", SheetSettings);
			m_Menu.AddSubMenu(0, "Open ctrl+O", Open);
			m_Menu.AddSubMenu(0, "Quit ctrl+Q", Quit);
			m_Menu.AddSubMenu(1, "Undo ctrl+Z", Undo);
			m_Menu.AddSubMenu(1, "ToggleFrameEnabled ctrl+@", ToggleFrameEnabled);
			m_Menu.AddSubMenu(1, "HeightMax ctrl+\\", HeightMax);
		}
		public void T_Menu_MenuExec(object sender, MenuEventArgs e)
		{
			if (m_Menu == null) return;
			if (m_Menu.Funcs[e.Id]() == true)
			{
				this.Invalidate();
			}
		}

		// ************************************************************************************
		public bool SheetSettings()
		{
			if (m_Form == null) return false;
			T_SheetSettingDialog dlg = new T_SheetSettingDialog();
			dlg.SetForm(m_Form);
			dlg.Frame = CellData.FrameCount;
			dlg.Fps = CellData.FrameRate;
			dlg.Caption = string.Format("Orig: {0}+{1}({2}F) fps:{3}",
				CellData.FrameCount / (int)CellData.FrameRate,
				CellData.FrameCount % (int)CellData.FrameRate,
				CellData.FrameCount,
				(int)CellData.FrameRate
				);
			dlg.Location = new Point(
				m_Form.Left + 20,
				m_Form.Top + 25 + Sizes.CaptionHeight + Sizes.CaptionHeight2
				);
			if (dlg.ShowDialog(m_Form) == DialogResult.OK)
			{
				CellData.FrameRate = dlg.Fps;
				CellData.FrameCount = dlg.Frame;
				Sizes.CallOnChangeGridSize();
			}
			dlg.Dispose();
			return true;
		}
		// ************************************************************************************
		public bool Open()
		{
			if (m_Form == null) return false;
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
			if (m_Input != null) ret = m_Input.InputAddKey(v);
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
			int y1 = (CellData.Selection.Start + CellData.Selection.Length) * Sizes.CellHeight;

			int t0 = y0 - Sizes.DispY;
			int t1 = y1 - Sizes.DispY;
			if (t0 >= this.Height)
			{
				int v = (CellData.Selection.Start + CellData.Selection.Length + 1) * Sizes.CellHeight - this.Height;
				if (v > Sizes.DispMax.Y - Sizes.CellHeight * 3) v = Sizes.DispMax.Y;
				Sizes.DispY = v;
			}
			else if (t1 <= 0)
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
				if (v >= Sizes.DispMax.X - Sizes.CellWidth / 2) v = Sizes.DispMax.X;
				Sizes.DispX = v;
			}
			else if (t0 <= 0)
			{
				int v = CellData.Selection.Target * Sizes.CellWidth;
				if (v < Sizes.CellHeight / 2) v = 0;
				Sizes.DispX = v;
			}

		}
		public bool InputEnter()
		{
			bool ret = false;
			if (m_Input != null)
			{
				if (m_Input.Value >= 0)
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
		public bool Home()
		{
			Sizes.DispY = 0;
			return true;

		}
		public bool End()
		{
			Sizes.DispY = Sizes.DispMax.Y;
			return true;

		}
		public bool PageDown()
		{
			Sizes.DispY += this.Height / 2;
			return true;
		}
		public bool PageUp()
		{
			Sizes.DispY -= this.Height / 2;
			return true;
		}
		public bool DispUp()
		{
			Sizes.DispY -= Sizes.CellHeight;
			return true;
		}
		public bool DispDown()
		{
			Sizes.DispY += Sizes.CellHeight;
			return true;
		}
		public bool DispLeft()
		{
			Sizes.DispX -= Sizes.CellWidth;
			return true;
		}
		public bool DispRight()
		{
			Sizes.DispX += Sizes.CellWidth;
			return true;
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
		public bool SetSelectionLength(int v)
		{
			bool b = false;
			if (CellData.Selection.Length != v)
			{
				CellData.PushUndo(BackupSratus.SelectionChange);
				CellData.Selection.Length = v;
				b = true;
			}
			return b;
		}
		public bool SetSelection1()
		{
			return SetSelectionLength(1);
		}
		public bool SetSelection2()
		{
			return SetSelectionLength(2);
		}
		public bool SetSelection3()
		{
			return SetSelectionLength(3);
		}
		public bool SetSelection4()
		{
			return SetSelectionLength(4);
		}
		public bool SetSelection5()
		{
			return SetSelectionLength(5);
		}
		public bool SetSelection6()
		{
			return SetSelectionLength(6);
		}
		public bool SetSelection7()
		{
			return SetSelectionLength(7);
		}
		public bool SetSelection8()
		{
			return SetSelectionLength(8);
		}
		public bool SetSelection9()
		{
			return SetSelectionLength(9);
		}
		public bool SetSelection10()
		{
			return SetSelectionLength(10);
		}
		public bool SetSelection11()
		{
			return SetSelectionLength(11);
		}
		public bool SetSelection12()
		{
			return SetSelectionLength(12);
		}
	}
}
