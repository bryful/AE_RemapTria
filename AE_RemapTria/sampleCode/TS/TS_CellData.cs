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
    public enum FRAMERATE
    {
        FPS24 = 24,
        FPS30 = 30

    }
    public class TS_CellData
	{
		public event EventHandler ChangeFrameEvent;
		public event EventHandler ChangeLayerEvent;

		private int m_LayerCount;
		private int m_FrameCount;

		private int m_StartFrame = 1;
        private FRAMERATE m_FrameRate = FRAMERATE.FPS24;

		TS_CellLayer[] m_data = new TS_CellLayer[12];
		//-------------------------------------------------
		public TS_CellData()
		{
			Init(12, 72);
		}
		//---------------------------------------
		protected virtual void OnChangeFrameEvent(EventArgs e)
		{
			ChangeFrameEvent?.Invoke(this, e);
		}
		//---------------------------------------
		protected virtual void OnChangeLayerEvent(EventArgs e)
		{
			ChangeLayerEvent?.Invoke(this, e);
		}
		//-------------------------------------------------
		public void Init(int lc = 12, int fc = 72)
		{
			if (lc < 6) lc = 6;
			Array.Resize(ref m_data, lc);
			for (int i = 0; i < lc; i++)
			{
				char s = ((char)((int)'A' + i));
				m_data[i] = new TS_CellLayer(fc, "" + s);
			}
			m_LayerCount = m_data.Length;
			m_FrameCount = m_data[0].FrameCount;
		}
		//-------------------------------------------------
		public int LayerCount
		{
			get { return m_LayerCount; }
			set { SetLayerCount(value); }
		}
		//-------------------------------------------------
		public int FrameCount
		{
			get { return m_FrameCount; }
			set { SetFrameCount(value); }
		}
		//-------------------------------------------------
		public string Caption(int idx)
		{
			if ((idx >= 0) && (idx < m_LayerCount))
			{
				return m_data[idx].Caption;
			}
			else
			{
				return "";
			}
		}
		//-------------------------------------------------
		public void SetCaption(int idx,string s)
		{
			if ((idx >= 0) && (idx < m_LayerCount))
			{
				m_data[idx].Caption = s.Trim();
			}
		}
		//-------------------------------------------------
		public string [] CaptionArray
		{
			get
			{
				string[] ret = new string[m_LayerCount];
				for (int i=0; i< m_LayerCount;i++)
				{
					ret[i] = m_data[i].Caption;
				}
				return ret;
			}

		}
		//-------------------------------------------------
		public void SetFrameCount(int fc)
		{
			if (fc < 6) fc = 6;
			if (m_FrameCount != fc)
			{
				for (int i = 0; i < LayerCount; i++)
				{
					m_data[i].SetFrameCount(fc);
				}
				m_FrameCount = fc;
				OnChangeFrameEvent(new EventArgs());
			}
		}
		//----------------------------------------------------------
		public void SetLayerCount(int lc)
		{
			if (lc < 6) lc = 6;
			if (m_LayerCount != lc)
			{
				int fc = m_FrameCount;
				if (lc < 6) lc = 6;
				int l2 = m_LayerCount;
				int l3 = lc - l2;
				Array.Resize(ref m_data, lc);
				if (l3 > 0)
				{
					for (int i = l2; i < lc; i++)
					{
						char c = 'A';
						if (i > 0)
						{
							string s = m_data[i - 1].Caption;
							if (s != "")
							{
								c = (char)((int)s[0] + 1);
							}

						}
						m_data[i] = new TS_CellLayer(fc, c + "");
					}
				}
				m_LayerCount = lc;
				OnChangeLayerEvent(new EventArgs());
			}
		}
		//-----------------------------------------------------
		public int StartFrame
		{
			get { return m_StartFrame; }
			set { m_StartFrame = value; }
		}
		//-----------------------------------------------------
        public FRAMERATE FrameRate
        {
            get { return m_FrameRate; }
            set { m_FrameRate = value; }
        }
	}
}
