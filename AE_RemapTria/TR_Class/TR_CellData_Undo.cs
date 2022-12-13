using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE_RemapTria
{
	/*
	public enum BackupSratus
	{
		None,
		NumberInput,
		SelectionChange,
		FrameEnabled,
		All
	}
	*/
	public class BackupCellData2
	{
		public TR_Selection sel = new TR_Selection();
		public int[][] data = new int[0][];
		public string[] caps = new string[0];
		public int[] enables = new int[0];
		public Point Disp = new Point(0, 0);
		public BackupSratus stat = BackupSratus.None;
		public BackupCellData2(TR_CellData cd, BackupSratus bs)
		{
			stat = bs;
			switch (bs)
			{
				case BackupSratus.All:
					this.sel = new TR_Selection(sel);
					data = cd.BackupCellData();
					caps = cd.BackupCaption();
					enables = cd.EnableFrames();
					break;
				case BackupSratus.NumberInput:
					this.sel = new TR_Selection(sel);
					data = new int[1][];
					data[0] = cd.GetCellNum();
					break;
				case BackupSratus.SelectionChange:
					this.sel = new TR_Selection(sel);
					break;
				case BackupSratus.FrameEnabled:
					this.sel = new TR_Selection(sel);
					this.enables = cd.EnableFrames();
					break;
			}

		}
		public BackupSratus Restore(TR_CellData cd)
		{
			BackupSratus ret = stat;
			switch (stat)
			{
				case BackupSratus.All:
					cd.RestoreCellData(data);
					cd.RestoreCaption(caps);
					cd.sel.Copy(sel);
					cd.SetFrameEnabled(enables);
					break;
				case BackupSratus.NumberInput:
					cd.sel.Copy(sel);
					cd.SetCellNum(data[0]);
					break;
				case BackupSratus.SelectionChange:
					cd.sel.Copy(sel);
					break;
				case BackupSratus.FrameEnabled:
					cd.sel.Copy(sel);
					cd.SetFrameEnabled(enables);
					break;
			}
			return ret;
		}
	}
	partial class TR_CellData
	{
		public bool _undoPushFlag = true;
		private List<BackupCellData2> m_BackupCells = new List<BackupCellData2>();
		// *************************************
		public int[][] BackupCellData()
		{
			int[][] ret = new int[CellCount][];
			for (int i = 0; i < CellCount; i++)
			{
				ret[i] = m_cells[i].RawData();
			}
			return ret;
		}
		public void RestoreCellData(int[][] d)
		{
			if (d.Length <= 0) return;
			if (d[0].Length <= 0) return;
			bool b = _undoPushFlag;
			_undoPushFlag = false;
			SetCellCount(d.Length);
			SetFrameCount(d[0].Length);
			for (int i = 0; i < d.Length; i++)
			{
				m_cells[i].SetRawData(d[i]);
			}

			_undoPushFlag = b;
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
				bool b = _undoPushFlag;
				_undoPushFlag = false;
				SetCellCount(cl);
				_undoPushFlag = b;
			}
			for (int i = 0; i < cl; i++)
			{
				m_cells[i].Caption = c[i];
			}
		}
		// ******************************************************
		public void PushUndo(BackupSratus bs)
		{
			if (_undoPushFlag == false) return;
			m_BackupCells.Add(new BackupCellData2(this, bs));
			if (m_BackupCells.Count > 4000)
			{
				m_BackupCells.RemoveAt(0);
			}
		}
		public void InitUndo()
		{
			m_BackupCells.Clear();
		}
		// ******************************************************
		public BackupSratus PopUndo()
		{
			BackupSratus ret = BackupSratus.None;
			int idx = m_BackupCells.Count - 1;
			if (idx < 0) return ret;
			bool b = _undoPushFlag;
			bool b2 = _eventFlag;
			_undoPushFlag = false;
			_eventFlag = false;
			ret = m_BackupCells[idx].Restore(this);
			m_BackupCells.RemoveAt(idx);
			_undoPushFlag = b;
			_eventFlag = b2;
			return ret;
		}
		// ******************************************************
		public TR_Selection GetUndoSel()
		{
			TR_Selection ret = new TR_Selection();
			ret.Start = 0;
			ret.Length = 1;
			ret.Target = 0;
			if (m_BackupCells.Count > 0)
			{
				ret.Copy( m_BackupCells[m_BackupCells.Count - 1].sel);
			}
			return ret;
		}
	}
}
