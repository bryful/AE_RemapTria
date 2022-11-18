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
    public enum ColorPlate
	{
		KagiTL = Kagi_Style.TL,
		KagiTR = Kagi_Style.TR,
		KagiBL = Kagi_Style.BL,
		KagiBR = Kagi_Style.BR,
		Fill,
		FillDot,
	}
	public partial class T_ColorPlate : T_BaseControl
	{
		private ColorPlate m_ColorPlate = ColorPlate.Fill;
		public ColorPlate ColorPlate
		{
			get { return m_ColorPlate; }
			set { m_ColorPlate = value; this.Invalidate(); }
		}
		private int m_Opacity = 128;
		[Category("_AE_Remap")]
		public int Opacity
		{
			get { return m_Opacity; }
			set {
				int v = value;
				if (v < 0) v = 0;
				else if(v>255) v= 255;
				m_Opacity = v;

				this.ForeColor = Color.FromArgb(m_Opacity, this.ForeColor.R, this.ForeColor.G, this.ForeColor.B);
				this.Invalidate(); 
			}
		}
		public int m_KagiHWeight = 10;
		public int m_KagiVWeight = 10;
		public int KagiHWeight { get { return m_KagiHWeight; } set { m_KagiHWeight = value;this.Invalidate(); } }
		public int KagiVWeight { get { return m_KagiVWeight; } set { m_KagiVWeight = value; this.Invalidate(); } }
		public T_ColorPlate()
		{
			this.Size = new Size(40, 40);
			this.ForeColor = Color.FromArgb(m_Opacity, 200, 200, 255);
			InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			Graphics g = pe.Graphics;
			g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
			g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
			SolidBrush sb = new SolidBrush(Color.Transparent);
			try
			{
				int w = this.Width;
				int h = this.Height;
				Rectangle r = new Rectangle(0, 0, w, h);
				DrawKagiPrm kp = new DrawKagiPrm();
				kp.HLength = this.Width;
				kp.HWeight = m_KagiHWeight;
				kp.VLength = this.Height;
				kp.VWeight = m_KagiVWeight;
				g.FillRectangle(sb,r);
				if( m_Opacity>0)
				{
					sb.Color = this.ForeColor;

					switch (m_ColorPlate)
					{
						case ColorPlate.Fill:
							g.FillRectangle(sb, r);
							break;
						case ColorPlate.KagiTL:
							kp.Loc = new Point(0, 0);
							kp.Style = Kagi_Style.TL;
							T_G.DrawKagi(g, sb, kp);

							break;
						case ColorPlate.KagiTR:
							kp.Loc = new Point(this.Width, 0);
							kp.Style = Kagi_Style.TR;
							T_G.DrawKagi(g, sb, kp);
							break;
						case ColorPlate.KagiBL:
							kp.Loc = new Point(0, this.Height);
							kp.Style = Kagi_Style.BL;
							T_G.DrawKagi(g, sb, kp);
							break;
						case ColorPlate.KagiBR:
							kp.Loc = new Point(this.Width, this.Height);
							kp.Style = Kagi_Style.BR;
							T_G.DrawKagi(g, sb, kp); 
							break;
						case ColorPlate.FillDot:
							sb.Color = this.ForeColor;
							g.FillRectangle(sb, r);
							w = this.Width / 10;
							h = this.Height / 10;
							r= new Rectangle(this.Width-w*2, this.Height-h*2, w, h);
							sb.Color = Color.Black;
							g.FillRectangle(sb, r);
							break;

					}
				}

			}
			finally
			{
				sb.Dispose();
			}



			//base.OnPaint(pe);
		}
	}
}
