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
		public T_Selection sel = new();
		public int[] ints = Array.Empty<int>();
		public int[][] aints = new int[0][];
		public string[] caps = new string[0];
		public bool[] enableds = new bool[0];
		public BackupSratus stat = BackupSratus.None;
		public BackupCellData(T_CellData cd, BackupSratus bs)
		{
			stat = bs;
			switch (bs)
			{
				case BackupSratus.All:
					this.sel = new T_Selection(sel);
					aints = cd.BackupCellData();
					caps = cd.BackupCaption();
					enableds = cd.GetFrameEnabled();
					break;
				case BackupSratus.NumberInput:
					this.sel = new T_Selection(cd.Selection);
					this.ints = cd.GetCellNum();
					break;
				case BackupSratus.SelectionChange:
					this.sel = new T_Selection(cd.Selection);
					break;
				case BackupSratus.FrameEnabled:
					this.sel = new T_Selection(cd.Selection);
					this.enableds = cd.GetFrameEnabled();
					break;
			}

		}
		public void Restore(T_CellData cd)
		{
			switch (stat)
			{
				case BackupSratus.All:
					cd.RestoreCellData(aints);
					cd.RestoreCaption(caps);
					cd.Selection.Copy(sel);
					cd.SetFrameEnabled(enableds);
					break;
				case BackupSratus.NumberInput:
					cd.Selection.Copy(sel);
					cd.SetCellNum(ints);
					break;
				case BackupSratus.SelectionChange:
					cd.Selection.Copy(sel);
					break;
				case BackupSratus.FrameEnabled:
					cd.Selection.Copy(sel);
					cd.SetFrameEnabled(enableds);
					break;
			}
		}
	}
	partial class T_CellData
	{
		public bool _undePushFlag = true;
		private List<BackupCellData> m_BackupCells = new List<BackupCellData>();
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
