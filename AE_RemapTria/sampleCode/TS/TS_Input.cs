using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace TS
{
	public   class TS_Input : TS_GridBase
	{
        private TS_Grid m_grid = null;
		private TS_CellData m_CellData = null;

		private TS_GridSize m_GridSize = new TS_GridSize();
		private TS_GridColor m_GridColor = new TS_GridColor();
		private int m_value = 0;
		//-------------------------------------------
		public TS_Input()
		{
            Init();

            ChkGridSize();
			ChkGridColor();

            Alignment = StringAlignment.Far;
			FontBold= true;

			SizeFix();
		}
		//-------------------------------------------
		public TS_Grid Grid
		{
			get { return m_grid; }

			set
			{
				m_grid = value;
				ChkGrid();
			}
		}
		//------------------------------------------
		private void ChkGridSize()
		{
			if (m_GridSize == null) m_GridSize = new TS_GridSize();
			SizeFix();
			m_GridSize.ChangeGridSize += M_GridSize_ChangeGridSize;
		}
		//------------------------------------------
		private void ChkGridColor()
		{
			if (m_GridColor == null) m_GridColor = new TS_GridColor();
			m_GridColor.ChangeColorEvent += M_GridColor_ChangeColorEvent;
		}
		//------------------------------------------
		private void ChkGrid()
		{
			if (m_grid == null)
			{
				m_CellData = null;
				m_GridSize = null;
				m_GridColor = null;
			}
			else
			{
				m_CellData = m_grid.CellData;
				m_GridSize = m_grid.GridSize;
				m_GridColor = m_grid.GridColor;
			}
			ChkGridSize();
			ChkGridColor();
		}
		//------------------------------------------
		private void M_GridColor_ChangeColorEvent(object sender, EventArgs e)
		{
			this.Invalidate();
		}
        //------------------------------------------
        private void M_GridSize_ChangeGridSize(object sender, EventArgs e)
        {
            SizeFix();
            this.Invalidate();
        }
        //------------------------------------------
        public void SizeFix()
		{
			this.Size = new Size(m_GridSize.FrameWidth, m_GridSize.CaptionHeight+m_GridSize.CaptionHeight2);
			this.MinimumSize = this.Size;
			this.MaximumSize = this.Size;
			this.Invalidate();
		}


		//-------------------------------------------
		protected override void OnPaint(PaintEventArgs e)
		{
			int ps = 1;
			Pen p = new Pen(Color.Black, ps);
			SolidBrush sb = new SolidBrush(Color.Transparent);


			try
			{
				Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                Fill(g, sb);



				Rectangle ar = new Rectangle(0, 0, m_GridSize.FrameWidth, m_GridSize.CaptionHeight2);
				sb.Color = m_GridColor.Moji;
                PushFontStatus();
                Font_Size = 7;
                Font_Style = FontStyle.Italic;
                LineAlignment = StringAlignment.Far;
				DrawStr(g, "AE_Remap Drei", sb, ar);
                PopFontStatus();
                Rectangle rct = new Rectangle(
					m_GridSize.FrameWidth2,
					m_GridSize.CaptionHeight2,
					m_GridSize.FrameWidth - m_GridSize.FrameWidth2,
					m_GridSize.CaptionHeight
					);
				sb.Color = m_GridColor.Input;
				Fill(g, sb, rct);
				sb.Color = m_GridColor.Moji;
				DrawStr(g, m_value.ToString(), sb, rct);
				p.Color = m_GridColor.InputLine;
				DrawFrame(g, p,rct,2);
                
				p.Width = 5;
				int pp = (int)p.Width / 2;
				int pl = 22;
				Point[] ln = new Point[3];
				ln[0] = new Point(pp, pl);
				ln[1] = new Point(pp, pp);
				ln[2] = new Point(pl, pp);
				p.Color = m_GridColor.Kagi;
				g.DrawLines(p, ln);
                

			}
			finally
			{
				p.Dispose();
				sb.Dispose();
			}

		}
 
    }
}
