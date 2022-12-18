using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AE_RemapTria
{
	public partial class TR_EditColor : TR_DialogControl
	{
		private COLS m_TargetCOLS = COLS.Line;
		public COLS TargetCOLS
		{
			get { return m_TargetCOLS; }
			set
			{ 
				m_TargetCOLS = value;
				Text = TR_Colors.COLSName(m_TargetCOLS);
				this.Invalidate();
			}	
		}
		public TR_EditColor()
		{
			MyFontIndex= 5;
			MyFontSize= 10;
			Colors = new TR_Colors();
			this.Size = new Size(250, 25);
			InitializeComponent();
		}
		private int m_CaptionWidth = 160;
		public int CaptionWidth
		{
			get { return m_CaptionWidth; }
			set { m_CaptionWidth = value; this.Invalidate(); }
		}
		// ****************************************************
		private string ColorTo(Color c)
		{
			if( c.A<255)
			{
				return $"{c.A},{c.R},{c.G},{c.B}";
			}
			else
			{
				return $"{c.R},{c.G},{c.B}";
			}
		}
		// ****************************************************
		protected override void OnPaint(PaintEventArgs pe)
		{
			if(Colors==null) return;
			SolidBrush sb = new SolidBrush(BackColor);
			Pen p= new Pen(ForeColor);
			Graphics g = pe.Graphics;
			try
			{
				sb.Color = Color.Transparent;
				g.FillRectangle(sb, this.ClientRectangle);

				sb.Color = Color.FromArgb(200, 200, 250);
				Rectangle rct = new Rectangle(0, 0, m_CaptionWidth, this.Height);
				m_format.Alignment = StringAlignment.Far;
				g.DrawString(Text,Font, sb, rct, m_format);

				Color t = Colors.Get(m_TargetCOLS);
				rct = new Rectangle(m_CaptionWidth,0,this.Height,this.Height);
				sb.Color = t;
				g.FillRectangle(sb, rct);
				p.Color = Color.Gray;
				p.Width = 1;
				rct = new Rectangle(m_CaptionWidth, 0, this.Height-1, this.Height-1);
				g.DrawRectangle(p,rct);

				int y = m_CaptionWidth + this.Height;
				sb.Color = Color.FromArgb(200, 200, 250);
				rct = new Rectangle(y, 0, this.Width-y, this.Height);
				m_format.Alignment = StringAlignment.Near;
				g.DrawString(ColorTo(t), Font, sb, rct, m_format);
				p.Color = Color.Gray;
				p.Width = 1;
				rct = new Rectangle(y, 0, this.Width - y-1, this.Height-1);
				g.DrawRectangle(p, rct);

			}
			finally
			{
				sb.Dispose();
				p.Dispose();
			}
		}
	}
}
