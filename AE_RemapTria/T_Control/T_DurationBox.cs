using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE_RemapTria
{

	public class T_DurationBox : T_BaseControl
	{
		public event EventHandler? DurationChanged = null;
		protected virtual void OnDurationChanged(EventArgs e)
		{
			if (DurationChanged != null)
			{
				DurationChanged(this, e); 
			}
		}
		private double m_Duration = 0;
		public string m_Sec = "";
		public string m_Frame = "";
		private T_Fps m_Fps = T_Fps.FPS24;
		[Category("_AE_Remap")]
		public int Frame
		{
			get { return (int)((double)m_Fps * (double)m_Duration); }
			set
			{
				double f = (double)value / (double)m_Fps;
				if(m_Duration!=f)
				{
					m_Duration = f;
					int ff = (int)((double)m_Fps * (double)m_Duration);
					string sec = String.Format("{0}", ff / (int)m_Fps);
					if (sec == "0") sec = "";
					m_Sec = sec;
					string fm = String.Format("{0}", ff % (int)m_Fps);
					if (fm == "0") fm = "";
					m_Frame = fm;
					if ((m_Sec != "") && (m_Frame == "")) m_Frame = "0";
					OnDurationChanged(new EventArgs());
					this.Invalidate();
				}
			}
		}
		[Category("_AE_Remap")]
		public T_Fps Fps
		{
			get { return m_Fps; }
			set
			{
				if(m_Fps!=value)
				{
					m_Fps = value;
					int ff = (int)((double)m_Fps * (double)m_Duration);
					string sec = String.Format("{0}", ff / (int)m_Fps);
					if (sec == "0") sec = "";
					m_Sec = sec;
					string fm = String.Format("{0}", ff % (int)m_Fps);
					if (fm == "0") fm = "";
					if ((m_Sec != "") && (m_Frame == "")) m_Frame = "0";
					m_Frame = fm;
					OnDurationChanged(new EventArgs());
					this.Invalidate();
				}
			}
		}
		[Category("_AE_Remap")]
		public string Info
		{
			get
			{
				int f = (int)(m_Duration * (int)m_Fps);
				int sec = f / (int)m_Fps;
				int koma = f % (int)m_Fps;
				return string.Format("{0}+{1} ({2}F)",
					sec,koma,f					);
			}
		}
		public T_DurationBox()
		{
			this.Size = new Size(150, 30);
			Alignment = StringAlignment.Far;
			LineAlignment = StringAlignment.Center;
			this.SetStyle(
				ControlStyles.DoubleBuffer |
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint|
				ControlStyles.SupportsTransparentBackColor,
				true);
			this.BackColor = Color.Transparent;

		}
		public void SetFrame(T_Fps fps,int f)
		{
			m_Fps = fps;
			m_Duration = (double)f/(double)fps;
			int ff = (int)((double)m_Fps * (double)m_Duration);
			string sec = String.Format("{0}", ff / (int)fps);
			m_Sec = sec;
			string fm = String.Format("{0}", ff % (int)fps);
			if (fm == "0") fm = "00";
			m_Frame = fm;
			OnDurationChanged(new EventArgs());
		}
		// *************************************************************
		public void InputBS()
		{
			bool b = false;
			if (m_Frame != "")
			{
				m_Frame = m_Frame.Substring(0, m_Frame.Length - 1);
				b = true;
			}else if (m_Sec != "")
			{
				m_Frame = m_Sec;
				m_Sec = "";
				b = true;
			}
			if (b)
			{
				double d = ToDuration();
				if (m_Duration != d)
				{
					m_Duration = d;
					OnDurationChanged(new EventArgs());
				}
				this.Refresh();
			}
		}
		// *************************************************************
		public void InputAdd(int v)
		{
			bool b = false;
			if((v>=0)&& (v<=9))
			{
				string s = String.Format("{0}",v);
				if (m_Sec == "")
				{
					m_Frame += s;
					b = true;
				}
				else
				{
					string ss = m_Frame + s;
					int vv = int.Parse(ss);
					if(vv < (int)m_Fps)
					{
						m_Frame = ss;
						b = true;
					}
				}

			}
			double d = ToDuration();
			if (m_Duration != d)
			{
				m_Duration = d;
				OnDurationChanged(new EventArgs());
			}
			if (b) this.Refresh();

		}
		// *******************
		public void InputClear()
		{
			m_Sec = "";
			m_Frame = "";
			m_Duration = 0;
			OnDurationChanged(new EventArgs());
			this.Refresh();
		}
		// *******************
		public void InputSec()
		{
			if(m_Sec != "") return;
			m_Sec = m_Frame;
			m_Frame = "";
			double d = ToDuration();
			if (m_Duration != d)
			{
				m_Duration = d;
				OnDurationChanged(new EventArgs());
			}
			this.Refresh();
		}
		// *******************
		public void InputKey(int v)
		{
			if((v>=0)&&(v<=9))
			{
				InputAdd(v);
			}else if (v==10)
			{
				InputSec();
			}else if(v==11)
			{
				InputBS();
			}
			else if (v == 12)
			{
				InputClear();
			}
		}

		public int KeyToNum(Keys k)
		{
			int ret = -1;
			if((k>=Keys.D0) && (k<=Keys.D9))
			{
				ret = ((int)k - (int)Keys.D0);
			}else if ((k >= Keys.NumPad0) && (k <= Keys.NumPad9))
			{
				ret = ((int)k - (int)Keys.NumPad0);
			}else if((k==Keys.Add)|| (k == Keys.Oemplus))
			{
				ret =10;
			}else if (k == Keys.Back)
			{
				ret = 11;
			}
			else if(k== Keys.Delete)
			{
				ret = 12;
			}
			return ret;
		}
		// ******************************************
		private double ToDuration()
		{
			double d = 0;
			if(m_Sec!="")
			{
				d += double.Parse(m_Sec);
			}
			if(m_Frame!="")
			{
				d += double.Parse(m_Frame)/(double)m_Fps;

			}
			return d;
		}
		// *************************************************************
		protected override void OnPaint(PaintEventArgs pe)
		{
			//base.OnPaint(pe);
			Graphics g = pe.Graphics;
			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
			SolidBrush sb = new SolidBrush(this.BackColor);
			try
			{
				Rectangle r = new Rectangle(0,0,this.Width,this.Height);
				Fill(g,sb);
				sb.Color = this.ForeColor;
				string s = m_Frame;
				if (m_Sec != "")
				{
					s = m_Sec + "+" + s;
				}
				if(s!="")DrawStr(g, s, sb, r);

			}
			finally
			{
				sb.Dispose();
			}
		}
		// *************************************************************
	}
}
