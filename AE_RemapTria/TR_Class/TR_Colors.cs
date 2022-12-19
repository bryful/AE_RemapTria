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
		GLine,
		GLineInside,
		GLineSame,
		GLineHor,
		Selection,
		Moji,
		Kagi,
		KagiBR,
		KagiBRHi,
		UnEnabledMoji,
		UnEnabled,
		
		FrameDot,
		SelectionFrame,

		MenuBack,
		MenuBackSelected,
		MenuBackNoActive,
		MenuMoji,
		MenuMojiNoActive,
		MenuWaku,
		MenuWaku1,
		MenuWaku2,
		MenuSdw,

		BGGrad0,
		BGGrad1,
		BGGrad2,
		Transparent
	};

	public class TR_Colors
	{
		public bool _eventFlag=true;
		public event EventHandler? ColorChangedEvent;

		static public string? COLSName(COLS c)
		{
			return Enum.GetName(typeof(COLS),c);
		}
		static public string[] COLSNames()
		{
			return Enum.GetNames(typeof(COLS));
		}
		private Color[] cols = new Color[(int)COLS.Transparent];
		private Color[] colsBackup = new Color[(int)COLS.Transparent];
		static private Color[] STColors = new Color[0];
		// *****************************************************************************
		public TR_Colors()
		{
			cols= InitColors();
		}
		// *****************************************************************************
		public string ToCSharp()
		{
			string ret = "";
			for (int i = 0; i < (int)COLS.Transparent; i++)
			{
				if (cols[i].A==255)
				{
					ret += $"ret[(int)COLS.{COLSName((COLS)i)}] = Color.FromArgb({cols[i].R},{cols[i].G},{cols[i].B});\r\n";

				}
				else
				{
					ret += $"ret[(int)COLS.{COLSName((COLS)i)}] = Color.FromArgb({cols[i].A},{cols[i].R},{cols[i].G},{cols[i].B});\r\n";
				}
			}
			return ret;
		}
		public void CopyToCSharp()
		{
			Clipboard.SetText(ToCSharp());
		}
		// *****************************************************************************
			public void Push()
		{
			for(int i=0;i<(int)COLS.Transparent;i++)
			{
				colsBackup[i] = Color.FromArgb(
					cols[i].A,
					cols[i].R,
					cols[i].G,
					cols[i].B
					);
				;
			}
		}
		public void Pop()
		{
			for (int i = 0; i < (int)COLS.Transparent; i++)
			{
				cols[i] = Color.FromArgb(
					colsBackup[i].A,
					colsBackup[i].R,
					colsBackup[i].G,
					colsBackup[i].B
					);
				;
			}
		}
		static public Color[] InitColors()
		{
			Color[] ret = new Color[(int)COLS.Transparent];
			ret[(int)COLS.Base] = Color.Transparent;
			ret[(int)COLS.GLine] = Color.FromArgb(100, 200, 255);
			ret[(int)COLS.GLineInside] = Color.FromArgb(100, 150, 200);
			ret[(int)COLS.GLineSame] = Color.FromArgb(60, 110, 160);
			ret[(int)COLS.GLineHor] = Color.FromArgb(70, 120, 170);
			ret[(int)COLS.Kagi] = Color.FromArgb(133, 148, 197);
			ret[(int)COLS.KagiBR] = Color.FromArgb(188, 191, 57);
			ret[(int)COLS.KagiBRHi] = Color.FromArgb(242, 243, 216);

			ret[(int)COLS.Selection] = Color.FromArgb(70, 70, 140);
			ret[(int)COLS.Moji] = Color.FromArgb(120, 220, 250);
			ret[(int)COLS.UnEnabledMoji] = Color.FromArgb(8, 8, 20);
			ret[(int)COLS.UnEnabled] = Color.FromArgb(80, 80, 100);

			ret[(int)COLS.FrameDot] = Color.FromArgb(40, 160, 220);
			ret[(int)COLS.SelectionFrame] = Color.FromArgb(40, 40, 98);

			ret[(int)COLS.MenuBack] = Color.FromArgb(52, 55, 106);
			ret[(int)COLS.MenuBackSelected] = Color.FromArgb(82, 85, 136);
			ret[(int)COLS.MenuBackNoActive] = Color.FromArgb(26, 27, 53);
			ret[(int)COLS.MenuMoji] = Color.FromArgb(159, 173, 244);
			ret[(int)COLS.MenuWaku] = Color.FromArgb(0x43, 0x62, 0xb2);
			ret[(int)COLS.MenuWaku1] = Color.FromArgb(0x0e, 0x12, 0x42);
			ret[(int)COLS.MenuWaku2] = Color.FromArgb(0x2a, 0x2d, 0x60);
			ret[(int)COLS.MenuSdw] = Color.FromArgb(0x18, 0x1a, 0x2f);
			ret[(int)COLS.MenuMojiNoActive] = Color.FromArgb(52, 56, 78);

			ret[(int)COLS.BGGrad0] = Color.FromArgb(20, 40, 90);
			ret[(int)COLS.BGGrad1] = Color.FromArgb(5, 5, 20);
			ret[(int)COLS.BGGrad2] = Color.FromArgb(20, 40, 70);

			return ret;
		}
		//---------------------------------------
		static public Color GetColor(COLS c)
		{
			Color ret = Color.White;
			if(STColors.Length<= (int)COLS.Transparent)
			{
				STColors = InitColors();
			}
			if(((int)c >=0)&&((int)c < (int)COLS.Transparent))
			{
				ret = STColors[(int)c];
			}
			return ret;
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
		public Color Get(COLS c)
		{
			Color ret = Color.White;
			if (((int)c >= 0) && ((int)c < (int)COLS.Transparent))
			{
				ret = cols[(int)c];
			}
			return ret;
		}
		public void Set(COLS c,Color col)
		{
			if (((int)c >= 0) && ((int)c < (int)COLS.Transparent))
			{
				cols[(int)c] = col;
			}
		}
		//---------------------------------------
		public Color Base
		{
			get { return cols[(int)COLS.Base]; }
			set { cols[(int)COLS.Base] = value; OnColorChangedEvent(new EventArgs()); }
		}
		//---------------------------------------
		public Color GLine
		{
			get { return cols[(int)COLS.GLine]; }
			set { cols[(int)COLS.GLine] = value; OnColorChangedEvent(new EventArgs()); }
		}
		//---------------------------------------
		public Color GLineInSide
		{
			get { return cols[(int)COLS.GLineInside]; }
			set { cols[(int)COLS.GLineInside] = value; OnColorChangedEvent(new EventArgs()); }
		}
		//---------------------------------------
		public Color GLineSame
		{
			get { return cols[(int)COLS.GLineSame]; }
			set { cols[(int)COLS.GLineSame] = value; OnColorChangedEvent(new EventArgs()); }
		}
		//---------------------------------------
		public Color GLineHor
		{
			get { return cols[(int)COLS.GLineHor]; }
			set { cols[(int)COLS.GLineHor] = value; OnColorChangedEvent(new EventArgs()); }
		}
		//---------------------------------------
		public Color FrameDot
		{
			get { return cols[(int)COLS.FrameDot]; }
			set { cols[(int)COLS.FrameDot] = value; OnColorChangedEvent(new EventArgs()); }
		}
		//---------------------------------------
		public Color Kagi
		{
			get { return cols[(int)COLS.Kagi]; }
			set { cols[(int)COLS.Kagi] = value; OnColorChangedEvent(new EventArgs()); }
		}
		//---------------------------------------
		public Color KagiBR
		{
			get { return cols[(int)COLS.KagiBR]; }
			set { cols[(int)COLS.KagiBR] = value; OnColorChangedEvent(new EventArgs()); }
		}
		//---------------------------------------
		public Color KagiBRHi
		{
			get { return cols[(int)COLS.KagiBRHi]; }
			set { cols[(int)COLS.KagiBRHi] = value; OnColorChangedEvent(new EventArgs()); }
		}
		//---------------------------------------
		public Color Selection
		{
			get { return cols[(int)COLS.Selection]; }
			set { cols[(int)COLS.Selection] = value; OnColorChangedEvent(new EventArgs()); }
		}
		//---------------------------------------
		public Color SelectionFrame
		{
			get { return cols[(int)COLS.SelectionFrame]; }
			set { cols[(int)COLS.SelectionFrame] = value; OnColorChangedEvent(new EventArgs()); }
		}
		//---------------------------------------
		public Color Moji
		{
			get { return cols[(int)COLS.Moji]; }
			set { cols[(int)COLS.Moji] = value; OnColorChangedEvent(new EventArgs()); }
		}
		//---------------------------------------
		public Color UnEnabledMoji
		{
			get { return cols[(int)COLS.UnEnabledMoji]; }
			set { cols[(int)COLS.UnEnabledMoji] = value; OnColorChangedEvent(new EventArgs()); }
		}
		//---------------------------------------
		public Color UnEnabled
		{
			get { return cols[(int)COLS.UnEnabled]; }
			set { cols[(int)COLS.UnEnabled] = value; OnColorChangedEvent(new EventArgs()); }
		}

		//---------------------------------------
		public Color MenuBack
		{
			get { return cols[(int)COLS.MenuBack]; }
			set { cols[(int)COLS.MenuBack] = value; OnColorChangedEvent(new EventArgs()); }
		}
		public Color MenuBackSelected
		{
			get { return cols[(int)COLS.MenuBackSelected]; }
			set { cols[(int)COLS.MenuBackSelected] = value; OnColorChangedEvent(new EventArgs()); }
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
		public Color MenuWaku
		{
			get { return cols[(int)COLS.MenuWaku]; }
			set { cols[(int)COLS.MenuWaku] = value; OnColorChangedEvent(new EventArgs()); }
		}
		public Color MenuMojiNoActive
		{
			get { return cols[(int)COLS.MenuMojiNoActive]; }
			set { cols[(int)COLS.MenuMojiNoActive] = value; OnColorChangedEvent(new EventArgs()); }
		}
		public Color MenuWaku1
		{
			get { return cols[(int)COLS.MenuWaku1]; }
			set { cols[(int)COLS.MenuWaku1] = value; OnColorChangedEvent(new EventArgs()); }
		}
		public Color MenuWaku2
		{
			get { return cols[(int)COLS.MenuWaku2]; }
			set { cols[(int)COLS.MenuWaku2] = value; OnColorChangedEvent(new EventArgs()); }
		}
		public Color MenuSdw
		{
			get { return cols[(int)COLS.MenuSdw]; }
			set { cols[(int)COLS.MenuSdw] = value; OnColorChangedEvent(new EventArgs()); }
		}
		public Color BGGrad0
		{
			get { return cols[(int)COLS.BGGrad0]; }
			set { cols[(int)COLS.BGGrad0] = value; OnColorChangedEvent(new EventArgs()); }
		}
		public Color BGGrad1
		{
			get { return cols[(int)COLS.BGGrad1]; }
			set { cols[(int)COLS.BGGrad1] = value; OnColorChangedEvent(new EventArgs()); }
		}
		public Color BGGrad2
		{
			get { return cols[(int)COLS.BGGrad2]; }
			set { cols[(int)COLS.BGGrad2] = value; OnColorChangedEvent(new EventArgs()); }
		}
	}
}
