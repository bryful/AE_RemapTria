using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace AE_RemapTria
{
	public enum ZEBRA_TYPE
	{
		RED=0,
		GREEN,
		BLUE,
		YELLOW,
		SKYBULE,
		DARKBLUE,
		DRAKGRAY,
		DRAKRED,
		DARKYELLOW
	}

	public partial class T_Zebra : Control
	{
		private ZEBRA_TYPE m_ZebraIndex = ZEBRA_TYPE.YELLOW;
		[Category("_AE_Remap")]
		public ZEBRA_TYPE ZebraIndex
		{
			get { return m_ZebraIndex; }
			set
			{
				int v = (int)value;
				if (v < 0) v = 0;
				else if (v > (int)ZEBRA_TYPE.DARKYELLOW) v = (int)ZEBRA_TYPE.DARKYELLOW;
				m_ZebraIndex = (ZEBRA_TYPE)v;
				this.Invalidate();

			}
		}
		private Bitmap [] zebra = new Bitmap[(int)ZEBRA_TYPE.DARKYELLOW+1];
		public T_Zebra()
		{
			zebra[(int)ZEBRA_TYPE.RED] = Properties.Resources.Zebra_red;
			zebra[(int)ZEBRA_TYPE.GREEN] = Properties.Resources.Zebra_green;
			zebra[(int)ZEBRA_TYPE.BLUE] = Properties.Resources.Zebra_blue;
			zebra[(int)ZEBRA_TYPE.YELLOW] = Properties.Resources.Zebra_yellow;
			zebra[(int)ZEBRA_TYPE.SKYBULE] = Properties.Resources.Zebra_skyblue;
			zebra[(int)ZEBRA_TYPE.DARKBLUE] = Properties.Resources.Zebra_darkblue;
			zebra[(int)ZEBRA_TYPE.DRAKGRAY] = Properties.Resources.Zebra_darkgray;
			zebra[(int)ZEBRA_TYPE.DRAKRED] = Properties.Resources.Zebra_darkred;
			zebra[(int)ZEBRA_TYPE.DARKYELLOW] = Properties.Resources.Zebra_darkyellow;

			InitializeComponent();
			this.Size = new Size(100, 50);
			this.SetStyle(
	ControlStyles.DoubleBuffer |
	ControlStyles.UserPaint |
	ControlStyles.AllPaintingInWmPaint |
	ControlStyles.SupportsTransparentBackColor,
	true);
			this.BackColor = Color.Transparent;
			this.UpdateStyles();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			Graphics g = pe.Graphics;
			SolidBrush sb = new SolidBrush(this.BackColor);
			TextureBrush tBrush = new TextureBrush(zebra[(int)m_ZebraIndex]);
			try
			{
				g.FillRectangle(sb,this.ClientRectangle);
				g.FillRectangle(tBrush, this.ClientRectangle);
			}
			finally
			{
				sb.Dispose();
				tBrush.Dispose();
			}

			base.OnPaint(pe);
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
		}
	}
}
