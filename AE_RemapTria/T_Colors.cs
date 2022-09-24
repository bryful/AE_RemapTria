using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE_RemapTria
{
	public enum COLS
	{
		Base = 0,
		Line,
		LineA,
		LineB,
		Frame,
		Caption,
		Caption2,
		Input,
		InputLine,
		CellA,
		CellA_sdw,
		CellB,
		CellB_sdw,
		Selection,
		SelectionCaption,
		Moji,
		Kagi,
		Count
	};
	public class T_Colors
	{
		public bool _eventFlag=true;
		public event EventHandler? ColorChangedEvent;

		private Color[] cols = new Color[(int)COLS.Count];
		// *****************************************************************************
		public T_Colors()
		{
			cols[(int)COLS.Base] = Color.Transparent;
			cols[(int)COLS.Line] = Color.FromArgb(255, 100, 200, 255);
			cols[(int)COLS.LineA] = Color.FromArgb(164, 100, 200, 255);
			cols[(int)COLS.LineB] = Color.FromArgb(128, 100, 200, 255);
			cols[(int)COLS.Frame] = Color.FromArgb(64, 0, 120, 150);
			cols[(int)COLS.Caption] = Color.FromArgb(64, 100, 180, 200);
			cols[(int)COLS.Caption2] = Color.FromArgb(64, 0, 0, 0);

			cols[(int)COLS.Input] = Color.FromArgb(64, 0, 0, 0);
			cols[(int)COLS.InputLine] = Color.FromArgb(128, 110, 200, 250);
			cols[(int)COLS.Kagi] = Color.FromArgb(192, 30, 150, 250);

			cols[(int)COLS.CellA] = Color.FromArgb(64, 0, 80, 140);
			cols[(int)COLS.CellA_sdw] = Color.Transparent;
			cols[(int)COLS.CellB] = Color.FromArgb(245, 245, 245);
			cols[(int)COLS.CellB_sdw] = Color.FromArgb(240, 240, 240);
			cols[(int)COLS.Selection] = Color.FromArgb(128, 0, 150, 255);
			cols[(int)COLS.SelectionCaption] = Color.FromArgb(128, 0, 150, 255);
			cols[(int)COLS.Moji] = Color.FromArgb(255, 120, 220, 250);

		}
		//---------------------------------------
		protected virtual void OnColorChangedEvent(EventArgs e)
		{
			if (_eventFlag == false) return;
			if (ColorChangedEvent != null)
			{
				ColorChangedEvent(this, e);
			}
		}
		//---------------------------------------
		public Color Base
		{
			get { return cols[(int)COLS.Base]; }
			set { cols[(int)COLS.Base] = value; OnColorChangedEvent(new EventArgs()); }
		}
		//---------------------------------------
		public Color Line
		{
			get { return cols[(int)COLS.Line]; }
			set { cols[(int)COLS.Line] = value; OnColorChangedEvent(new EventArgs()); }
		}
		//---------------------------------------
		public Color LineA
		{
			get { return cols[(int)COLS.LineA]; }
			set { cols[(int)COLS.LineA] = value; OnColorChangedEvent(new EventArgs()); }
		}
		//---------------------------------------
		public Color LineB
		{
			get { return cols[(int)COLS.LineB]; }
			set { cols[(int)COLS.LineB] = value; OnColorChangedEvent(new EventArgs()); }
		}
		//---------------------------------------
		public Color Frame
		{
			get { return cols[(int)COLS.Frame]; }
			set { cols[(int)COLS.Frame] = value; OnColorChangedEvent(new EventArgs()); }
		}

		//---------------------------------------
		public Color Caption
		{
			get { return cols[(int)COLS.Caption]; }
			set { cols[(int)COLS.Caption] = value; OnColorChangedEvent(new EventArgs()); }
		}
		//---------------------------------------
		public Color Caption2
		{
			get { return cols[(int)COLS.Caption2]; }
			set { cols[(int)COLS.Caption2] = value; OnColorChangedEvent(new EventArgs()); }
		}
		//---------------------------------------
		public Color Input
		{
			get { return cols[(int)COLS.Input]; }
			set { cols[(int)COLS.Input] = value; OnColorChangedEvent(new EventArgs()); }
		}
		//---------------------------------------
		public Color InputLine
		{
			get { return cols[(int)COLS.InputLine]; }
			set { cols[(int)COLS.InputLine] = value; OnColorChangedEvent(new EventArgs()); }
		}
		//---------------------------------------
		public Color Kagi
		{
			get { return cols[(int)COLS.Kagi]; }
			set { cols[(int)COLS.Kagi] = value; OnColorChangedEvent(new EventArgs()); }
		}
		//---------------------------------------
		public Color CellA
		{
			get { return cols[(int)COLS.CellA]; }
			set { cols[(int)COLS.CellA] = value; OnColorChangedEvent(new EventArgs()); }
		}
		//---------------------------------------
		public Color CellA_sdw
		{
			get { return cols[(int)COLS.CellA_sdw]; }
			set { cols[(int)COLS.CellA_sdw] = value; OnColorChangedEvent(new EventArgs()); }
		}
		//---------------------------------------
		public Color CellB
		{
			get { return cols[(int)COLS.CellB]; }
			set { cols[(int)COLS.CellB] = value; OnColorChangedEvent(new EventArgs()); }
		}
		//---------------------------------------
		public Color CellB_sdw
		{
			get { return cols[(int)COLS.CellB_sdw]; }
			set { cols[(int)COLS.CellB_sdw] = value; OnColorChangedEvent(new EventArgs()); }
		}
		//---------------------------------------
		public Color Selection
		{
			get { return cols[(int)COLS.Selection]; }
			set { cols[(int)COLS.Selection] = value; OnColorChangedEvent(new EventArgs()); }
		}
		//---------------------------------------
		public Color SelectionCaption
		{
			get { return cols[(int)COLS.SelectionCaption]; }
			set { cols[(int)COLS.SelectionCaption] = value; OnColorChangedEvent(new EventArgs()); }
		}
		//---------------------------------------
		public Color Moji
		{
			get { return cols[(int)COLS.Moji]; }
			set { cols[(int)COLS.Moji] = value; OnColorChangedEvent(new EventArgs()); }
		}
	}
}
