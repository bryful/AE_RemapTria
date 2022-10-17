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
	public partial class T_Zebra : Control
	{
		private int m_ZebraIndex = 3;
		public int ZebraIndex
		{
			get { return m_ZebraIndex; }
			set
			{
				int v = value;
				if (v < 0) v = 0;
				else if (v >= zebra.Length) v = zebra.Length - 1;
				m_ZebraIndex = v;
				this.Invalidate();

			}
		}
		private Bitmap [] zebra = new Bitmap[5];
		public T_Zebra()
		{
			zebra[0] = Properties.Resources.Zebra_red;
			zebra[1] = Properties.Resources.Zebra_green;
			zebra[2] = Properties.Resources.Zebra_blue;
			zebra[3] = Properties.Resources.Zebra_yellow;
			zebra[4] = Properties.Resources.Zebra_skyblue;

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
			TextureBrush tBrush = new TextureBrush(zebra[m_ZebraIndex]);
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
