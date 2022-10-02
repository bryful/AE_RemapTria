using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE_RemapTria
{
	public enum BackupSratus
	{
		None,
		NumberInput,
		SelectionChange,
		FrameEnabled,
		All
	}
	public class BackupCellData
	{
		public T_Selection sel = new T_Selection();
		public int[][] data = new int[0][];
		public string[] caps = new string[0];
		public int[] enables = new int[0];
		public BackupSratus stat = BackupSratus.None;
		public BackupCellData(T_CellData cd, BackupSratus bs)
		{
			stat = bs;
			switch (bs)
			{
				case BackupSratus.All:
					this.sel = new T_Selection(sel);
					data = cd.BackupCellData();
					caps = cd.BackupCaption();
					enables = cd.EnableFrames();
					break;
				case BackupSratus.NumberInput:
					this.sel = new T_Selection(cd.Selection);
					data = new int[1][];
					data[0] = cd.GetCellNum();
					break;
				case BackupSratus.SelectionChange:
					this.sel = new T_Selection(cd.Selection);
					break;
				case BackupSratus.FrameEnabled:
					this.sel = new T_Selection(cd.Selection);
					this.enables = cd.EnableFrames();
					break;
			}

		}
		public void Restore(T_CellData cd)
		{
			switch (stat)
			{
				case BackupSratus.All:
					cd.RestoreCellData(data);
					cd.RestoreCaption(caps);
					cd.Selection.Copy(sel);
					cd.SetFrameEnabled(enables);
					break;
				case BackupSratus.NumberInput:
					cd.Selection.Copy(sel);
					cd.SetCellNum(data[0]);
					break;
				case BackupSratus.SelectionChange:
					cd.Selection.Copy(sel);
					break;
				case BackupSratus.FrameEnabled:
					cd.Selection.Copy(sel);
					cd.SetFrameEnabled(enables);
					break;
			}
		}
	}
	partial class T_CellData
	{
		public bool _undePushFlag = true;
		private List<BackupCellData> m_BackupCells = new List<BackupCellData>();
		// *************************************
		public int[][] BackupCellData()
		{
			int[][] ret = new int[CellCount][];
			for (int i = 0; i < CellCount; i++)
			{
				ret[i] = m_cells[i].Arrays();
			}
			return ret;
		}
		public void RestoreCellData(int[][] d)
		{
			if (d.Length <= 0) return;
			if (d[0].Length <= 0) return;
			bool b = _undePushFlag;
			_undePushFlag = false;
			SetCellCount(d.Length);
			SetFrameCount(d[0].Length);
			for (int i = 0; i < d.Length; i++)
			{
				m_cells[i].SetArrays(d[i]);
			}

			_undePushFlag = b;
		}
		public string[] BackupCaption()
		{
			string[] ret = new string[CellCount];
			for (int i = 0; i < CellCount; i++)
			{
				ret[i] = m_cells[i].Caption;
			}
			return ret;
		}
		public void RestoreCaption(string[] c)
		{
			int cl = c.Length;
			if (cl <= 0) return;
			if (CellCount != cl)
			{
				bool b = _undePushFlag;
				_undePushFlag = false;
				SetCellCount(cl);
				_undePushFlag = b;
			}
			for (int i = 0; i < cl; i++)
			{
				m_cells[i].Caption = c[i];
			}
		}
		// ******************************************************
		public void PushUndo(BackupSratus bs)
		{
			if (_undePushFlag == false) return;
			m_BackupCells.Add(new BackupCellData(this, bs));
			if (m_BackupCells.Count > 2000)
			{
				m_BackupCells.RemoveAt(0);
			}
		}
		// ******************************************************
		public void PopUndo()
		{
			int idx = m_BackupCells.Count - 1;
			if (idx < 0) return;
			bool b = _undePushFlag;
			bool b2 = _eventFlag;
			_undePushFlag = false;
			_eventFlag = false;
			m_BackupCells[idx].Restore(this);
			m_BackupCells.RemoveAt(idx);
			_undePushFlag = b;
			_eventFlag = b2;
			OnValueChanged(new EventArgs());
		}
		// ******************************************************
	}
}
