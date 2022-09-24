using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TS
{
	public class TS_Position :Component
	{
		private TS_Grid m_Grid = null;
		private TS_Input m_Input = null;
		private TS_Caption m_Caption = null;
		private TS_Frame m_Frame = null;
		private HScrollBar m_HScrollBar = null;
		private VScrollBar m_VScrollBar = null;
		private Form m_Form = null;

		private MenuStrip m_MenuStrip = null;
		private StatusStrip m_StatusStrip = null;

		private TS_GridSize m_GridSize = null;
		private TS_CellData m_CellData = null;
		//-------------------------------------------------
		public TS_Position()
		{

		}
		//-------------------------------------------------
		public TS_Grid Grid
		{
			get { return m_Grid; }
			set
			{
				m_Grid = value;
				if (m_Grid!=null)
				{
					m_GridSize = m_Grid.GridSize;
					if (m_GridSize!=null)
					{
						SizeChk();
						m_GridSize.ChangeGridSize += M_GridSize_ChangeGridSize;
					}
					m_CellData = m_Grid.CellData;
				}
				else
				{
					m_GridSize = null;
				}
			}
		}


		//-------------------------------------------------
		public TS_Input Input
		{
			get { return m_Input; }
			set { m_Input = value; }
		}
		//-------------------------------------------------
		public TS_Caption Caption
		{
			get { return m_Caption; }
			set { m_Caption = value; SizeChk(); }
		}
		//-------------------------------------------------
		public TS_Frame Frame
		{
			get { return m_Frame; }
			set { m_Frame = value; SizeChk(); }
		}
		//-------------------------------------------------
		public HScrollBar HScrollBar
		{
			get { return m_HScrollBar; }
			set { m_HScrollBar = value; SizeChk(); }
		}
		//-------------------------------------------------
		public VScrollBar VScrollBar
		{
			get { return m_VScrollBar; }
			set { m_VScrollBar = value; SizeChk(); }
		}
		//-------------------------------------------------
		public MenuStrip MenuStrip
		{
			get { return m_MenuStrip; }
			set { m_MenuStrip = value; SizeChk(); }
		}
		//-------------------------------------------------
		public StatusStrip StatusStrip
		{
			get { return m_StatusStrip; }
			set { m_StatusStrip = value; SizeChk(); }
		}
		//-------------------------------------------------
		public Form Form
		{
			get { return m_Form; }
			set
			{
				m_Form = value;
				if(m_Form!=null)
				{
					SizeChk();
					m_Form.Resize += M_Form_Resize;
				}
			}
		}
		//-------------------------------------------------
		private void M_Form_Resize(object sender, EventArgs e)
		{
			SizeChk();
		}
		//-------------------------------------------------
		private void M_GridSize_ChangeGridSize(object sender, EventArgs e)
		{
			SizeChk();
		}
		//-------------------------------------------------
		public void SizeChk()
		{
			if ((m_Grid == null)||(m_GridSize==null)||(m_Form==null)) return;

			m_Form.SuspendLayout();

			Size sz = m_Form.ClientSize;



			int BaseTop = 0;
			int BaseBottom = 0;
			if (m_MenuStrip!=null)
			{
				BaseTop = m_MenuStrip.Height;
			}
			if (m_StatusStrip != null)
			{
				BaseBottom = m_StatusStrip.Height;
			}



			int hh = 0;
			int vw = 0;
			if (m_HScrollBar != null)
			{
				hh = m_HScrollBar.Height;
			}
			if (m_VScrollBar != null)
			{
				vw = m_VScrollBar.Width;
			}
			int w = sz.Width - (m_GridSize.FrameWidth + m_GridSize.InterWidth + vw);
			int h = sz.Height - (m_GridSize.CaptionHeight + m_GridSize.InterHeight + hh + BaseTop + BaseBottom);
			int l = m_GridSize.FrameWidth + m_GridSize.InterWidth;
			int t = m_GridSize.CaptionHeight + m_GridSize.InterHeight + BaseTop;

			if (m_Input!=null)
			{
				m_Input.Location = new Point(0, BaseTop);
			}
			if (m_Caption!=null)
			{
				m_Caption.Location = new Point(l, BaseTop);
				m_Caption.Size = new Size(w, m_GridSize.CaptionHeight);
			}
			if (m_Frame!=null)
			{
				m_Frame.Location = new Point(0, t);
				m_Frame.Size = new Size(m_GridSize.FrameWidth, h);
			}
			if (m_Grid != null)
			{
				m_Grid.Location = new Point(l, t);
				m_Grid.Size = new Size(w, h);
			}
			if(m_HScrollBar!=null)
			{
				m_HScrollBar.Location = new Point(l, sz.Height - (BaseBottom + m_HScrollBar.Height));
				m_HScrollBar.Size = new Size(w, m_HScrollBar.Height);
			}
			if (m_VScrollBar != null)
			{
				m_VScrollBar.Location = new Point(sz.Width - m_VScrollBar.Width, t);
				m_VScrollBar.Size = new Size(m_VScrollBar.Width, h);
			}

			Size dd = m_Form.Size - m_Form.ClientSize;

			m_Form.MinimumSize = new Size(
				m_GridSize.FrameWidth + m_GridSize.InterWidth + vw + m_GridSize.CellWidth * 6 + dd.Width,
				m_GridSize.CaptionHeight + m_GridSize.InterHeight+ hh + m_GridSize.CellHeight*6 + dd.Height+BaseBottom+BaseTop);
			if (m_CellData != null)
			{
				m_Form.MaximumSize = new Size(
					m_GridSize.FrameWidth + m_GridSize.InterWidth + vw + m_GridSize.CellWidth * m_CellData.LayerCount + dd.Width,
					m_GridSize.CaptionHeight + m_GridSize.InterHeight + hh + m_GridSize.CellHeight * m_CellData.FrameCount + dd.Height);
			}



			m_Form.ResumeLayout();
		}
		//-------------------------------------------------
		public void PositonChk()
		{
			if (m_Form == null) return;
			Size sz = m_Form.ClientSize;


		}
	}
}
