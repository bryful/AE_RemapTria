using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BRY;

namespace AE_RemapTria
{
	public partial class TR_Arrow : TR_DialogControl
	{

		private DrawArrowPrm m_ap = new DrawArrowPrm();
		[Category("_AE_Remap")]
		public bool IsCut
		{
			get { return m_ap.IsCut; }
			set { m_ap.IsCut = value; this.Invalidate(); }
		}
		[Category("_AE_Remap")]
		public ArrowDir ArrowDir
		{
			get { return m_ap.Dir; }
			set { m_ap.Dir = value; this.Invalidate(); }
		}
		[Category("_AE_Remap")]
		public Color Fill
		{
			get { return ForeColor; }
			set { ForeColor = value; this.Invalidate(); }
		}
		public TR_Arrow()
		{
			this.ForeColor = Color.FromArgb(200, 200, 200, 250);
			InitializeComponent();
			this.SetStyle(
				ControlStyles.DoubleBuffer |
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint|
				ControlStyles.SupportsTransparentBackColor,
				true);
			this.BackColor = Color.Transparent;
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			Graphics g = pe.Graphics;
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			SolidBrush sb = new SolidBrush(BackColor);
			try
			{
				Fill(g, sb);
				sb.Color = this.ForeColor;
				switch(m_ap.Dir)
				{
					case ArrowDir.Top:
						m_ap.Loc = new Point(this.Width / 2, this.Height - 1);
						m_ap.Width = this.Width - 1;
						m_ap.Height = this.Height - 1;
						break;
					case ArrowDir.Left:
						m_ap.Loc = new Point(this.Width - 1, this.Height/2);
						m_ap.Width = this.Height-1;
						m_ap.Height = this.Width-1;
						break;
					case ArrowDir.Bottom:
						m_ap.Loc = new Point(this.Width / 2, 0);
						m_ap.Width = this.Width - 1;
						m_ap.Height = this.Height - 1;
						break;
					case ArrowDir.Right:
						m_ap.Loc = new Point(0, this.Height / 2);
						m_ap.Width = this.Height - 1;
						m_ap.Height = this.Width - 1;
						break;

				}
				T_G.DrawArrow(g, sb,m_ap);
			}
			finally
			{
				sb.Dispose();
			}
		}
	}
}
