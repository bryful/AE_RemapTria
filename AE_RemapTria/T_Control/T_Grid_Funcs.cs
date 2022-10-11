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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AE_RemapTria
{
	partial class T_Grid
	{
		// ************************************************************************************
		private void MakeFuncs()
		{
			List<FuncItem> lst = new List<FuncItem>();

			lst.Add(new FuncItem(Input0, Keys.D0, Keys.NumPad0, "入力エリアに0"));
			lst.Add(new FuncItem(Input1, Keys.D1, Keys.NumPad1, "入力エリアに1"));
			lst.Add(new FuncItem(Input2, Keys.D2, Keys.NumPad2, "入力エリアに2"));
			lst.Add(new FuncItem(Input3, Keys.D3, Keys.NumPad3, "入力エリアに3"));
			lst.Add(new FuncItem(Input4, Keys.D4, Keys.NumPad4, "入力エリアに4"));
			lst.Add(new FuncItem(Input5, Keys.D5, Keys.NumPad5, "入力エリアに5"));
			lst.Add(new FuncItem(Input6, Keys.D6, Keys.NumPad6, "入力エリアに6"));
			lst.Add(new FuncItem(Input7, Keys.D7, Keys.NumPad7, "入力エリアに7"));
			lst.Add(new FuncItem(Input8, Keys.D8, Keys.NumPad8, "入力エリアに8"));
			lst.Add(new FuncItem(Input9, Keys.D9, Keys.NumPad9, "入力エリアに9"));
			lst.Add(new FuncItem(InputClear, Keys.Delete, "クリア"));
			lst.Add(new FuncItem(InputBS, Keys.Back, "BS"));
			lst.Add(new FuncItem(InputEnter, Keys.Enter, Keys.Return, "入力"));
			lst.Add(new FuncItem(InputEmpty, Keys.Decimal, Keys.OemPeriod, "空セル"));
			lst.Add(new FuncItem(InputInc, Keys.Add, "前の数値に1を足して入力"));
			lst.Add(new FuncItem(InputDec, Keys.Subtract, "前の数値に1を引いて入力"));
			lst.Add(new FuncItem(SelMoveUp, Keys.Up, "選択範囲を上に"));
			lst.Add(new FuncItem(SelMoveDown, Keys.Down, "選択範囲を下に"));
			lst.Add(new FuncItem(SelMoveLeft, Keys.Left, "選択範囲を左に"));
			lst.Add(new FuncItem(SelMoveRight, Keys.Right, "選択範囲を右に"));
			lst.Add(new FuncItem(SelAdd, Keys.Shift | Keys.Down, Keys.Multiply, "SelAdd"));
			lst.Add(new FuncItem(SelDec, Keys.Shift | Keys.Up, Keys.Divide, "SelDec"));
			lst.Add(new FuncItem(PageUp, Keys.PageUp));
			lst.Add(new FuncItem(PageDown, Keys.PageDown));
			lst.Add(new FuncItem(DispUp, Keys.Up | Keys.Control));
			lst.Add(new FuncItem(DispDown, Keys.Down | Keys.Control));
			lst.Add(new FuncItem(DispLeft, Keys.Left | Keys.Control));
			lst.Add(new FuncItem(DispRight, Keys.Right | Keys.Control));
			lst.Add(new FuncItem(Home, Keys.Home, "先頭へ"));
			lst.Add(new FuncItem(End, Keys.End, "最後へ"));

			lst.Add(new FuncItem(ToggleFrameEnabled, Keys.Control | Keys.Oemtilde));
			lst.Add(new FuncItem(FrameEnabledOFF, Keys.Control | Keys.Oem5));
			lst.Add(new FuncItem(FrameEnabledON, Keys.Control | Keys.Oem7));
			lst.Add(new FuncItem(HeightMax, Keys.Control | Keys.Oem5, "上下に広げる"));
			lst.Add(new FuncItem(SheetSettings, Keys.Control | Keys.K, "シート設定"));
			lst.Add(new FuncItem(SetSelection1, Keys.F1));
			lst.Add(new FuncItem(SetSelection2, Keys.F2));
			lst.Add(new FuncItem(SetSelection3, Keys.F3));
			lst.Add(new FuncItem(SetSelection4, Keys.F4));
			lst.Add(new FuncItem(SetSelection5, Keys.F5));
			lst.Add(new FuncItem(SetSelection6, Keys.F6));
			lst.Add(new FuncItem(SetSelection7, Keys.F7));
			lst.Add(new FuncItem(SetSelection8, Keys.F8));
			lst.Add(new FuncItem(SetSelection9, Keys.F9));
			lst.Add(new FuncItem(SetSelection10, Keys.F10));
			lst.Add(new FuncItem(SetSelection11, Keys.F11));
			lst.Add(new FuncItem(SetSelection12, Keys.F12));
			lst.Add(new FuncItem(SelectionAll, Keys.Control | Keys.A));
			lst.Add(new FuncItem(Undo, Keys.Control|Keys.Z));
			lst.Add(new FuncItem(Quit, Keys.Control | Keys.Q ,"終了"));
			lst.Add(new FuncItem(Save, Keys.Control | Keys.S, "保存"));
			lst.Add(new FuncItem(SaveAs, Keys.Shift | Keys.Control | Keys.S, "別名保存"));
			lst.Add(new FuncItem(Open, Keys.Control | Keys.O, "読み込み"));
			lst.Add(new FuncItem(SheetNameDialog, Keys.Control | Keys.E, "シート名の編集"));
			lst.Add(new FuncItem(Copy, Keys.Control | Keys.C));
			lst.Add(new FuncItem(Cut, Keys.Control | Keys.X));
			lst.Add(new FuncItem(Paste, Keys.Control | Keys.V));
			lst.Add(new FuncItem(ClearAll, Keys.Control | Keys.Delete));

			Funcs.SetFuncItems(lst.ToArray());
		}
		// ************************************************************************************
		public void MakeMenu()
		{
			if (m_Menu == null) return;
			m_Menu.AddMenu("AE_RemapTria", 93);
			m_Menu.AddMenu("Edit", 40);
			m_Menu.AddMenu("Windw", 50);

			m_Menu.AddSubMenu(0, "SheetSettings");
			m_Menu.AddSubMenu(0, "SheetNameDialog");
			m_Menu.AddSubMenuSepa(0);
			m_Menu.AddSubMenu(0, "Open");
			m_Menu.AddSubMenu(0, "Save");
			m_Menu.AddSubMenu(0, "SaveAs");
			m_Menu.AddSubMenu(0, "Quit");

			m_Menu.AddSubMenu(1, "Undo");
			m_Menu.AddSubMenuSepa(1);
			m_Menu.AddSubMenu(1, "Copy");
			m_Menu.AddSubMenu(1, "Cut");
			m_Menu.AddSubMenu(1, "Paste");
			m_Menu.AddSubMenuSepa(1);
			m_Menu.AddSubMenu(1, "ToggleFrameEnabled");

			m_Menu.AddSubMenu(2, "HeightMax");
		}
		// ************************************************************************************
		public bool SheetSettings()
		{
			if (m_Form == null) return false;
			T_SheetSettingDialog dlg = new T_SheetSettingDialog();
			dlg.SetForm(m_Form);
			int ExFrame = CellData.UnEnabledFrameCount;
			dlg.Frame = CellData.FrameCount;
			dlg.Fps = CellData.FrameRate;
			dlg.SheetName = CellData.SheetName;
			string caution = "";
			if(ExFrame > 0)
			{
				caution=String.Format("Warning! 抜きコマ{0}個あり。", ExFrame);
			}
			else
			{
				caution = string.Format("Orig: {0}+{1}({2}F) fps:{3}",
				CellData.FrameCount / (int)CellData.FrameRate,
				CellData.FrameCount % (int)CellData.FrameRate,
				CellData.FrameCount,
				(int)CellData.FrameRate
				);
			}
			dlg.Caption = caution;
			dlg.Location = new Point(
				m_Form.Left + 20,
				m_Form.Top + T_Size.MenuHeightDef + Sizes.CaptionHeight + Sizes.CaptionHeight2
				);
			if (dlg.ShowDialog(m_Form) == DialogResult.OK)
			{
				CellData.PushUndo(BackupSratus.All);
				CellData.FrameRate = dlg.Fps;
				CellData.FrameCount = dlg.Frame;
				CellData.SheetName = dlg.SheetName;
				if (m_Form != null)
				{
					m_Form.FileName = T_Def.ChangeName(m_Form.FileName, dlg.SheetName);
				}
				Sizes.CallOnChangeGridSize();
			}
			dlg.Dispose();
			return true;
		}

		// ************************************************************************************
		public bool Quit()
		{
			Application.Exit();
			return true;
		}
		// ************************************************************************************
		public bool Undo()
		{
			BackupSratus bs = CellData.PopUndo();
			if(bs== BackupSratus.All)
			{
				ChkMinMax();
				ChkHScrl();
				ChkVScrl();
				if (m_Form != null)
				{
					m_Form.ChkSize();
				}
			}
			else
			{
				SizeSetting();
			}

			return true;
		}
		// ************************************************************************************
		public bool ClearAll()
		{
			CellData.ClearAll();
			return true;
		}
		// ************************************************************************************
		public bool ToggleFrameEnabled()
		{
			return CellData.ToggleFrameEnabled();
		}
		// ************************************************************************************
		public bool FrameEnabledON()
		{
			CellData.SetFrameEnabled(true);
			return	true;
		}
		// ************************************************************************************
		public bool FrameEnabledOFF()
		{
			CellData.SetFrameEnabled(false);
			return true;
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
				m_Form.ForegroundWindow();
				ret = m_Form.HeightMax();
			}
			return ret;
		}
		public bool SetSelectionLength(int v)
		{
			bool b = false;
			if (CellData.Selection.Length != v)
			{
				CellData.PushUndo(BackupSratus.SelectionChange);
				CellData.Selection.Length = v;
				b = true;
				this.Invalidate();
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
		public bool SaveAs()
		{
			if (IsMultExecute) return false;
			return SaveDialog(FileName);
		}
		public bool SaveDialog(string p)
		{
			bool ret = false;
			if (IsMultExecute) return ret;
			SaveFileDialog dlg = new SaveFileDialog();
			if (p != "")
			{
				dlg.InitialDirectory = Path.GetDirectoryName(p);
				dlg.FileName = Path.GetFileName(p);
			}
			dlg.Filter = "*.ardj.json|*.ardj.json|*.jsx|*.jsx|*.*|*.*";
			dlg.FilterIndex = 1;
			bool b = false;
			if (m_Form != null)
			{
				b = m_Form.TopMost;
				m_Form.TopMost = false;
				m_Form.ForegroundWindow();
			}
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				if (Save(dlg.FileName))
				{
					FileName = dlg.FileName;
					ret = true;
				}
				else
				{
					MessageBox.Show("err");
				}
			}
			if (m_Form != null) m_Form.TopMost = b;
			return ret;
		}
		public bool Save(string p)
		{
			if (IsMultExecute) return false;
			CellData.SheetName = Path.GetFileNameWithoutExtension(p);
			bool ret = CellData.Save(p);
			if (ret)
			{
				FileName=p;
				CellData.SheetName = Path.GetFileNameWithoutExtension(FileName);
			}
			return ret;
		}
		public bool Save()
		{
			if (IsMultExecute) return false;
			if(FileName!="")
			{
				return Save(FileName);
			}
			else
			{
				return SaveAs();
			}
		}
		public bool Open()
		{
			bool ret = false;
			OpenFileDialog dlg = new OpenFileDialog();
			if (FileName != "")
			{
				dlg.InitialDirectory = Path.GetDirectoryName(FileName);
				dlg.FileName = Path.GetFileName(FileName);
			}
			dlg.Filter = "*.ardj.json|*.ardj.json|*.jsx|*.jsx|*.*|*.*";
			dlg.FilterIndex = 1;
			bool b = false;
			if(m_Form!=null)
			{
				b = m_Form.TopMost;
				m_Form.TopMost =false;
				m_Form.ForegroundWindow();
			}
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				ret = (Open(dlg.FileName));
			}
			if (m_Form != null)
			{
				m_Form.TopMost = b;
			}
			return ret;
		}
		public bool Open(string p)
		{
			bool ret = false;
			if (File.Exists(p) == false) return ret;
			CellData.PushUndo(BackupSratus.All);
			ret = CellData.Load(p);
			if(ret)
			{
				FileName = p;
				CellData.SheetName = Path.GetFileNameWithoutExtension(p);
				ChkMinMax();
				ChkHScrl();
				ChkVScrl();
				if (m_Form != null)
				{
					m_Form.ChkSize();
				}
				this.Invalidate();
			}
			return ret;

		}
		public bool SheetNameDialog()
		{
			bool ret = false;
			if (m_Form == null) return false;
			m_Form.ForegroundWindow();
			T_NameDialog dlg = new T_NameDialog();
			dlg.SetForm(m_Form);
			dlg.Caption = "Input: Sheet Name";
			dlg.ValueText = CellData.SheetName;
			dlg.Location = new Point(
				m_Form.Left + 20,
				m_Form.Top + T_Size.MenuHeightDef + Sizes.CaptionHeight + Sizes.CaptionHeight2
				);
			bool b = false;
			if (m_Form != null)
			{
				b = m_Form.TopMost;
				m_Form.TopMost = false;
			}
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				CellData.SheetName = dlg.ValueText;
				if(m_Form!=null)
				{
					m_Form.FileName = T_Def.ChangeName(m_Form.FileName, dlg.ValueText);
				}

				ret = true;
			}
			if (m_Form != null) m_Form.TopMost = b;
			return ret;
		}
		public string ToArdj()
		{
			return CellData.ToArdj();
		}
		public bool Copy()
		{
			return CellData.Copy();
		}
		public bool Cut()
		{
			bool ret = CellData.Cut();
			if (ret)
			{
				this.Invalidate();
			}
			return ret;
		}
		public bool Paste()
		{
			bool ret = CellData.Paste();
			if (ret)
			{
				this.Invalidate();
			}
			return ret;
		}
		public bool CellLeftShift()
		{
			bool ret = CellData.CellLeftShift();
			if (ret)
			{
				this.Invalidate();
			}
			return ret;
		}
		public bool CellRightShift()
		{
			bool ret = CellData.CellRightShift();
			if (ret)
			{
				this.Invalidate();
			}
			return ret;
		}
	}
}
