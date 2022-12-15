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

	public partial class TR_Zebra : TR_DialogControl
	{
		private Color m_ZebraColor = Color.FromArgb(200,200,0);
		[Category("_AE_Remap")]
		public Color ZebraColor
		{
			get { return m_ZebraColor; }
			set
			{
				m_ZebraColor = value;
				this.Invalidate();

			}
		}
		private float m_ZebraWidth = 25;
		[Category("_AE_Remap")]
		public float ZebraWidth
		{
			get { return m_ZebraWidth; }
			set
			{
				m_ZebraWidth = value;
				this.Invalidate();

			}
		}
		private float m_ZebraRot = 45;
		[Category("_AE_Remap")]
		public float ZebraRot
		{
			get { return m_ZebraRot; }
			set
			{
				m_ZebraRot = value;
				if (m_ZebraRot < -45) m_ZebraRot = -45;
				else if (m_ZebraRot > 45) m_ZebraRot = 45;
				this.Invalidate();

			}
		}

		public TR_Zebra()
		{
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
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			SolidBrush sb = new SolidBrush(m_ZebraColor);
			try
			{
				sb.Color = BackColor;
				g.FillRectangle(sb, this.ClientRectangle);
				sb.Color = m_ZebraColor;
				TRc.DrawZebra(g, sb, this.ClientRectangle, m_ZebraWidth, m_ZebraRot);
			}
			finally
			{
				sb.Dispose();
			}

		}
	}
}
