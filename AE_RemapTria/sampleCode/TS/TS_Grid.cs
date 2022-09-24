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
	public class TS_Grid : TS_GridBase
	{
		private TS_CellData m_CellData = new TS_CellData();

		private TS_GridSize m_GridSize = new TS_GridSize();
		private TS_GridColor m_GridColor = new TS_GridColor();
		private TS_Selection m_Selection = new TS_Selection();

		private HScrollBar m_HScrollBar = null;
		private VScrollBar m_VScrollBar = null;
		//-------------------------------------------------
		public TS_Grid()
		{
			Init();

			ChkMinMax();
			m_Selection.ChangeSelectionEvent += M_Selection_ChangeSelectionEvent;
			m_GridSize.ChangeGridSize += M_GridSize_ChangeGridSize;
			m_GridSize.ChangeDisp += M_GridSize_ChangeGridSize;
		}

		//-------------------------------------------------
		public TS_CellData CellData
		{
			get { return m_CellData; }
		}
		public TS_GridSize GridSize
		{
			get { return m_GridSize; }
		}
		public TS_GridColor GridColor
		{
			get { return m_GridColor; }
			
		}
		public TS_Selection Selection
		{
			get { return m_Selection; }
		}
		public int LayerIndex
		{
			get { return m_Selection.LayerIndex; }
			set
			{
				m_Selection.LayerIndex = value;
			}
		}
		public int StartIndex
		{
			get { return m_Selection.StartIndex; }
			set { m_Selection.StartIndex = value; }
		}
		public int SelLength
		{
			get { return m_Selection.Length; }
			set { m_Selection.Length = value; }
		}
		//-------------------------------------------------
		private void M_GridSize_ChangeGridSize(object sender, EventArgs e)
		{
			this.Invalidate();
		}
		//-------------------------------------------------
		private void M_Selection_ChangeSelectionEvent(object sender, EventArgs e)
		{
			this.Invalidate();
		}

		//-------------------------------------------------
		private void ChkMinMax()
		{
			this.MinimumSize = new Size(m_GridSize.CellWidth * 6, m_GridSize.CellHeight * 6);
			this.MaximumSize = new Size(m_GridSize.CellWidth * m_CellData.LayerCount, m_GridSize.CellHeight * m_CellData.FrameCount);
			m_GridSize.SetSize(this.Size, m_CellData);
		}
		//-------------------------------------------------
		public HScrollBar HScrollBar
		{
			get { return m_HScrollBar; }
			set
			{
				m_HScrollBar = value;
				if (m_HScrollBar!=null)
				{
					m_HScrollBar.ValueChanged += M_HScrollBar_ValueChanged;
					HScrollBar.Minimum = 0;
					HScrollBar.Maximum = m_GridSize.DispMax.X;
					HScrollBar.Value = m_GridSize.Disp.X;
				}
			}
		}
		//-------------------------------------------------------------
		private void M_HScrollBar_ValueChanged(object sender, EventArgs e)
		{
			m_GridSize.DispX = m_HScrollBar.Value;
		}
		//-------------------------------------------------------------
		public VScrollBar VScrollBar
		{
			get { return m_VScrollBar; }
			set
			{
				m_VScrollBar = value;
				if (m_VScrollBar != null)
				{
					m_VScrollBar.ValueChanged += M_VScrollBar_ValueChanged; ;
					m_VScrollBar.Minimum = 0;
					m_VScrollBar.Maximum = m_GridSize.DispMax.Y;
					m_VScrollBar.Value = m_GridSize.Disp.Y;
				}
			}
		}
		//-------------------------------------------------------------
		private void M_VScrollBar_ValueChanged(object sender, EventArgs e)
		{
			m_GridSize.DispY = m_VScrollBar.Value;
		}


		//-------------------------------------------------
		protected override void OnResize(EventArgs e)
		{

			if((m_GridSize!=null)&&(m_CellData!=null))
			{
				m_GridSize.SetSize(this.Size, m_CellData);
				if (m_HScrollBar!=null)
				{
					m_HScrollBar.Maximum = m_GridSize.DispMax.X;
					m_HScrollBar.Value = m_GridSize.Disp.X;
				}
				if (m_VScrollBar != null)
				{
					m_VScrollBar.Maximum = m_GridSize.DispMax.Y;
					m_VScrollBar.Value = m_GridSize.Disp.Y;
				}
			}

			base.OnResize(e);

			this.Invalidate();
		}
		//-------------------------------------------------
		private void DrawCell(Graphics g, SolidBrush sb,Pen p, int l,int f)
		{
			int x = l * m_GridSize.CellWidth - m_GridSize.Disp.X;
			int y = f * m_GridSize.CellHeight - m_GridSize.Disp.Y;

			Rectangle r = new Rectangle(x, y, m_GridSize.CellWidth, m_GridSize.CellHeight);
            //塗りつぶし
            bool IsSel = (m_Selection.IsTarget(l, f));
            if (IsSel==true)
			{
				sb.Color = m_GridColor.Selection;
			}
			else
			{
				if (l % 2 == 0)
				{
					sb.Color = m_GridColor.CellA;

				}
				else
				{
					sb.Color = m_GridColor.CellA_sdw;
				}
			}
			g.FillRectangle(sb, r);

            //
			p.Color = m_GridColor.LineB;
			p.Width = 1;
			int x2 = x + m_GridSize.CellWidth -1;
			int y2 = y + m_GridSize.CellHeight-1;
            //横線
            DrawHorLine(g, p, x, x2, y);
            //縦線
            DrawVerLine(g, p, x, y, y2);
            if (IsSel == true)
            {
                p.Color = m_GridColor.LineA;
                //DrawVerLine(g, p, x, y, y2);
                //DrawVerLine(g, p, x2, y, y2);
            }

            switch (m_CellData.FrameRate)
            {
                case FRAMERATE.FPS24:
                    if (f % (int)FRAMERATE.FPS24 == 0)
                    {
                        p.Color = m_GridColor.Line;
                        DrawHorLine(g, p, x, x2, y);
                        DrawHorLine(g, p, x, x2, y - 1);
                    }
                    else if (f % 12 == 0)
                    {
                        p.Color = m_GridColor.Line;
                        DrawHorLine(g, p, x, x2, y);

                    }
                    else
                    {
                        if (f % 6 == 0)
                        {
                            p.Color = m_GridColor.LineA;
                            DrawHorLine(g, p, x, x2, y);
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
			SolidBrush sb = new SolidBrush(Color.White);
			try
			{
				Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                //とりあえず塗りつぶし
                if ((m_CellData==null)|| (m_GridSize == null)||(m_GridColor==null)||(m_Selection==null))
				{
					Fill(g, sb);
					return;
				}
				sb.Color = m_GridColor.Base;
				Fill(g, sb);
				Rectangle r = m_GridSize.DispCell;
				for ( int j = r.Top; j <= r.Bottom; j++)
				{
					for (int i = r.Left; i <= r.Right; i++)
					{
						DrawCell(g, sb, p, i, j);
					}

				}

				p.Color = m_GridColor.Line;
				DrawFrame(g, p);
			}
			finally
			{
				p.Dispose();
				sb.Dispose();
			}

		}

	}
}
