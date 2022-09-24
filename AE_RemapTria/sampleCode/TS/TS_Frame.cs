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
	public class TS_Frame : TS_GridBase
	{
		private TS_Grid m_grid = null;
		private TS_CellData m_CellData = null;
		private TS_GridSize m_GridSize = null;
		private TS_GridColor m_GridColor = null;
		private TS_Selection m_Selection = null;

        private bool UseParm = false;


        private Rectangle Dot1 = new Rectangle(5, 5, 12, 10);
        private Rectangle Dot2 = new Rectangle(5, 8, 8, 4);
        private Rectangle Dot3 = new Rectangle(5, 9, 4, 2);

        //---------------------------------------------
        public TS_Frame()
		{
			Init();
            Font_Size = 10;
            Alignment = StringAlignment.Far;
            LineAlignment = StringAlignment.Center;
			FontBold = true;
            ChkCellData();
            ChkGridSize();
            ChkGridColor();
            ChkSelection();
            ChkDot();
        }
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
            ChkMinMax();
            m_GridSize.ChangeGridSize += M_GridSize_ChangeGridSize;
            m_GridSize.ChangeDisp += ChangeStatus;
        }
        //------------------------------------------
        private void M_GridSize_ChangeGridSize(object sender, EventArgs e)
        {
            ChkDot();
        }
        private void ChkDot()
        {
            int w = m_GridSize.FrameWidth2 *2/ 3;
            int h = m_GridSize.CellHeight / 2;
            int l = (m_GridSize.FrameWidth2-w)/2;
            int t = -h/2;
            int l2 = l + w;
            Dot1 = new Rectangle(l, t, w, h);
            w = w*2/3;
            //h = h/2;
            t = -h/2;
            l = l2 - w; 
            Dot2 = new Rectangle(l, t, w, h);
            w = w /2;
            h = h /2;
            t = -h / 2;
            l = l2 - w;
            Dot3 = new Rectangle(l, t, w, h);

        }
        //------------------------------------------
        private void ChkGridColor()
        {
            if (m_GridColor == null) m_GridColor = new TS_GridColor();
            m_GridColor.ChangeColorEvent += ChangeStatus;
        }
        //------------------------------------------
        private void ChkCellData()
        {
            if (m_CellData == null) m_CellData = new TS_CellData();
        }
        //------------------------------------------
        private void ChkSelection()
        {
            if (m_Selection != null)
            {
                m_Selection.ChangeSelectionEvent += ChangeStatus;
            }
        }
        //--------------------------------------------
        private void ChkGrid()
		{

			if (m_grid == null)
			{
				m_CellData = null;
				m_GridSize = null;
				m_GridColor = null;
				m_Selection = null;
                UseParm = false;
            }
            else
            {
                m_CellData = m_grid.CellData;
                m_GridSize = m_grid.GridSize;
                m_GridColor = m_grid.GridColor;
                m_Selection = m_grid.Selection;
                UseParm = true;
            }
            ChkCellData();
            ChkGridSize();
            ChkGridColor();
            ChkSelection();

		}

		//--------------------------------------------
		private void ChangeStatus(object sender, EventArgs e)
		{
			this.Invalidate();
		}       

		//-------------------------------------------------
		private void ChkMinMax()
		{
			this.MinimumSize = new Size(m_GridSize.FrameWidth, m_GridSize.CellHeight*6);
            this.MaximumSize = new Size(m_GridSize.FrameWidth, m_GridSize.CellHeight * m_CellData.FrameCount);
            ChkDot();
		}
		//-------------------------------------------------
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
            if (UseParm == false) m_GridSize.SetSize(this.Size, m_CellData);

            this.Invalidate();
		}
		//-------------------------------------------------
		private void DrawFrame(Graphics g, SolidBrush sb, Pen p, int f,Rectangle rct)
		{
			int y = f * m_GridSize.CellHeight - m_GridSize.Disp.Y;
            rct.Y = y;

			if (m_Selection != null)
			{
				if (m_Selection.IsTargetFrame(f) == true)
				{
					sb.Color = m_GridColor.Selection;
                    Fill(g, sb, rct);
				}
			}

			sb.Color = m_GridColor.Moji;
			DrawStr(g, (f+m_CellData.StartFrame).ToString(), sb, rct);

			p.Color = m_GridColor.LineB;
			p.Width = 1;
            DrawHorLine(g, p, m_GridSize.FrameWidth2, rct.Right - 1, y);

            sb.Color = m_GridColor.Kagi;
            switch (m_CellData.FrameRate)
            {
                case FRAMERATE.FPS24:
                    if (f % (int)FRAMERATE.FPS24 == 0)
                    {
                        Rectangle r = new Rectangle(Dot1.Location,Dot1.Size);
                        r.Y += y;
                        Fill(g, sb, r);
                        p.Color = m_GridColor.Line;
                        int y2 = y - 1;
                        DrawHorLine(g, p,rct.Left, rct.Right, y2);
                        y2--;
                        DrawHorLine(g, p, rct.Left, rct.Right, y2);
                    }
                    else if (f % 12 == 0)
                    {
                        Rectangle r = new Rectangle(Dot2.Location, Dot2.Size);
                        r.Y += y;
                        Fill(g, sb, r);
                        p.Color = m_GridColor.Line;
                        int y2 = y - 1;
                        DrawHorLine(g, p, rct.Left, rct.Right, y2);

                    }
                    else 
                    {
                        Rectangle r = new Rectangle(Dot3.Location, Dot3.Size);
                        r.Y += y;
                        Fill(g, sb, r);
                        if (f % 6 == 0) {
                            p.Color = m_GridColor.LineA;
                            int y2 = y - 1;
                            DrawHorLine(g, p, rct.Left, rct.Right, y2);
                        }
                    }
                    break;
                case FRAMERATE.FPS30:
                    break;
            }
		}
		//-------------------------------------------------
		protected override void OnPaint(PaintEventArgs e)
		{
			Pen p = new Pen(Color.Black);
			SolidBrush sb = new SolidBrush(Color.Transparent);
			try
			{
				Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                //とりあえず塗りつぶし
                sb.Color = Color.Transparent;
                Fill(g, sb);

                Rectangle rct = new Rectangle(
                    m_GridSize.FrameWidth2,
                    0,
                    m_GridSize.FrameWidth - m_GridSize.FrameWidth2,
                    m_GridSize.CellHeight);

                Rectangle r = m_GridSize.DispCell;
				for (int i = r.Top; i <= r.Bottom; i++)
				{
					DrawFrame(g, sb, p, i,rct);
				}
                rct.Height = this.Height;
                p.Color = m_GridColor.Line;
				DrawFrame(g, p,rct);
			}
			finally
			{
				p.Dispose();
				sb.Dispose();
			}

		}
	}
}
