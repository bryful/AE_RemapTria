using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE_RemapTria
{
	/*
	public enum COLS
	{
		Base = 0,
		Line,
		LineDark,
		LineB,
		Frame,
		Caption,
		Caption2,
		Input,
		InputLine,
		InputLineA,
		CellEven,
		CellA_sdw,
		CellB,
		CellB_sdw,
		Selection,
		SelectionCaption,
		Moji,
		Kagi,
		GrayMoji,
		Gray,
		TopBar,
		
		MenuBack,
		MenuBackNoActive,
		MenuMoji,
		MenuMojiNoActive,
		Count
	};
	*/
	public class TR_Colors
	{
		public bool _eventFlag=true;
		public event EventHandler? ColorChangedEvent;

		private Color[] cols = new Color[(int)COLS.Count];
		// *****************************************************************************
		public TR_Colors()
		{
			cols[(int)COLS.Base] = Color.Transparent;
			cols[(int)COLS.Line] = Color.FromArgb( 100, 200, 255);
			cols[(int)COLS.LineDark] = Color.FromArgb( 100, 150, 200);
			cols[(int)COLS.LineB] = Color.FromArgb( 75, 120, 180);
			cols[(int)COLS.Frame] = Color.FromArgb( 0, 30, 50);
			cols[(int)COLS.Caption] = Color.FromArgb( 25, 45, 50);
			cols[(int)COLS.Caption2] = Color.FromArgb( 10, 10, 20);

			cols[(int)COLS.Input] = Color.FromArgb( 0, 0, 0);
			cols[(int)COLS.InputLine] = Color.FromArgb( 55, 100, 125);
			cols[(int)COLS.InputLineA] = Color.FromArgb( 110, 200, 250);
			cols[(int)COLS.Kagi] = Color.FromArgb( 30, 150, 250);

			cols[(int)COLS.CellEven] = Color.FromArgb( 30, 30, 60);
			cols[(int)COLS.CellA_sdw] = Color.Transparent;
			cols[(int)COLS.CellB] = Color.FromArgb(255, 245, 245);
			cols[(int)COLS.CellB_sdw] = Color.FromArgb(255, 240, 240);
			cols[(int)COLS.Selection] = Color.FromArgb(50, 100, 180);
			cols[(int)COLS.SelectionCaption] = Color.FromArgb( 0, 75, 128);
			cols[(int)COLS.Moji] = Color.FromArgb( 120, 220, 250);
			cols[(int)COLS.GrayMoji] = Color.FromArgb( 80, 80, 150);
			cols[(int)COLS.Gray] = Color.FromArgb( 20, 20, 50);
			cols[(int)COLS.TopBar] = Color.FromArgb(16, 32, 75);

			cols[(int)COLS.MenuBack] = Color.FromArgb(75, 88, 156);
			cols[(int)COLS.MenuBackNoActive] = Color.FromArgb(75,81, 109);
			cols[(int)COLS.MenuMoji] = Color.FromArgb(194, 202, 243);
			cols[(int)COLS.MenuMojiNoActive] = Color.FromArgb(52, 56, 78);

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
		public Color LineDark
		{
			get { return cols[(int)COLS.LineDark]; }
			set { cols[(int)COLS.LineDark] = value; OnColorChangedEvent(new EventArgs()); }
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
		public Color InputLineA
		{
			get { return cols[(int)COLS.InputLineA]; }
			set { cols[(int)COLS.InputLineA] = value; OnColorChangedEvent(new EventArgs()); }
		}
		//---------------------------------------
		public Color Kagi
		{
			get { return cols[(int)COLS.Kagi]; }
			set { cols[(int)COLS.Kagi] = value; OnColorChangedEvent(new EventArgs()); }
		}
		//---------------------------------------
		public Color CellEven
		{
			get { return cols[(int)COLS.CellEven]; }
			set { cols[(int)COLS.CellEven] = value; OnColorChangedEvent(new EventArgs()); }
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
		//---------------------------------------
		public Color GrayMoji
		{
			get { return cols[(int)COLS.GrayMoji]; }
			set { cols[(int)COLS.GrayMoji] = value; OnColorChangedEvent(new EventArgs()); }
		}
		//---------------------------------------
		public Color Gray
		{
			get { return cols[(int)COLS.Gray]; }
			set { cols[(int)COLS.Gray] = value; OnColorChangedEvent(new EventArgs()); }
		}
		//---------------------------------------
		public Color TopBar
		{
			get { return cols[(int)COLS.TopBar]; }
			set { cols[(int)COLS.TopBar] = value; OnColorChangedEvent(new EventArgs()); }
		}
		//---------------------------------------
		public Color MenuBack
		{
			get { return cols[(int)COLS.MenuBack]; }
			set { cols[(int)COLS.MenuBack] = value; OnColorChangedEvent(new EventArgs()); }
		}
		public Color MenuBackNoActive
		{
			get { return cols[(int)COLS.MenuBackNoActive]; }
			set { cols[(int)COLS.MenuBackNoActive] = value; OnColorChangedEvent(new EventArgs()); }
		}
		public Color MenuMoji
		{
			get { return cols[(int)COLS.MenuMoji]; }
			set { cols[(int)COLS.MenuMoji] = value; OnColorChangedEvent(new EventArgs()); }
		}
		public Color MenuMojiNoActive
		{
			get { return cols[(int)COLS.MenuMojiNoActive]; }
			set { cols[(int)COLS.MenuMojiNoActive] = value; OnColorChangedEvent(new EventArgs()); }
		}
	}
}
