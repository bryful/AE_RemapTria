using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AE_Remap_Drei;

namespace TS
{
	public class TS_VScrol : Control
	{
		enum MD
		{
			NONE = 0,
			TOP_BTN,
			PUP_BTN,
			PUP,
			CURSOR,
			PDOWN,

			PDOWN_BTN,
			END_BTN,
		}
		public event EventHandler ChangeValueEvent;
		public event EventHandler ChangeMinimumSizeEvent;

		private int m_Maximum = 200;
		private int m_Value = 0;
		private int m_ValueArea = 0;
		private int m_ValuePos = 0;

		private Bitmap TA = AE_Remap_Drei.Properties.Resources._02_ScrolIconA_VT;
		private Bitmap TAD = AE_Remap_Drei.Properties.Resources._02_ScrolIconA_VT_dn;
		private Bitmap TB = AE_Remap_Drei.Properties.Resources._02_ScrolIconB_VT;
		private Bitmap TBD = AE_Remap_Drei.Properties.Resources._02_ScrolIconB_VT_dn;

		private Bitmap C1 = AE_Remap_Drei.Properties.Resources._02_ScrolIconC_V1;
		private Bitmap C2 = AE_Remap_Drei.Properties.Resources._02_ScrolIconC_V2;

		private Bitmap BA = AE_Remap_Drei.Properties.Resources._02_ScrolIconA_VB;
		private Bitmap BAD = AE_Remap_Drei.Properties.Resources._02_ScrolIconA_VB_dn;
		private Bitmap BB = AE_Remap_Drei.Properties.Resources._02_ScrolIconB_VB;
		private Bitmap BBD = AE_Remap_Drei.Properties.Resources._02_ScrolIconB_VB_dn;
		private Bitmap ARW = AE_Remap_Drei.Properties.Resources._02_ScrolArrow_V;
		private Bitmap ARWD = AE_Remap_Drei.Properties.Resources._02_ScrolArrow_V_dn;


		private int m_DownValue = 0;
		private int m_DownvaluePos = 0;

		private MD m_mouseDownMode = MD.NONE;

		/// <summary>
		/// 最小ピクセル
		/// </summary>
		private int m_MinHeight;
		private int m_ValueTop;
		private int m_AreaTop;
		private int m_AreaBottom;
		private int m_ValueBottom;
		private int m_BarHeight;


		private Color m_LineDark = Color.FromArgb(128, 50, 140, 200);
		private Color m_Line = Color.FromArgb(128, 50, 190, 220);

		public TS_VScrol()
		{

			m_MinHeight = TA.Height + TB.Height + ARW.Height + BB.Height + BA.Height;
			m_ValueTop = TA.Height + TB.Height;
			m_AreaTop = TA.Height + TB.Height + ARW.Height / 2;

			this.MinimumSize = new Size(22, m_MinHeight);
			
			this.SetStyle(
		   ControlStyles.DoubleBuffer |
		   ControlStyles.UserPaint |
		   ControlStyles.AllPaintingInWmPaint|
		   ControlStyles.SupportsTransparentBackColor,
		   true);
			this.BackColor = Color.Transparent;

		}
		//---------------------------------------
		protected virtual void OnChangeValueEvent(EventArgs e)
		{
			if (ChangeValueEvent != null)
			{
				ChangeValueEvent(this, e);
			}
		}
		//---------------------------------------
		protected virtual void OnChangeMinimumSizeEvent(EventArgs e)
		{
			if (ChangeMinimumSizeEvent != null)
			{
				ChangeMinimumSizeEvent(this, e);
			}
		}
		//----------------------------------------------------------------
		protected override void OnResize(EventArgs e)
		{
			ChkSize();
			base.OnResize(e);
			this.Invalidate();

		}
		//----------------------------------------------------------------
		private void ChkSize()
		{
			m_ValueArea = this.Height - m_MinHeight;
			if (m_Maximum <= 0)
			{
				m_ValuePos = (this.Height - ARW.Height) / 2 ;
			}
			else
			{
				m_ValuePos = m_ValueTop + m_ValueArea * m_Value / m_Maximum;
			}
			m_AreaBottom = this.Height - m_AreaTop;
			m_ValueBottom = this.Height - m_ValueTop;
			m_BarHeight = m_ValueArea/2 - ARW.Height;

		}
		//----------------------------------------------------------------
		protected override void OnPaint(PaintEventArgs pe)
		{
			//base.OnPaint(pe);
			ChkSize();

			Graphics g = pe.Graphics;
			g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;

			SolidBrush sb = new SolidBrush(Color.Transparent);
			try
			{
				sb.Color = Color.Transparent;
				g.FillRectangle(sb, new Rectangle(0, 0, this.Width, this.Height));
				//
				sb.Color = m_Line;
				//センター
				g.FillRectangle(sb, new Rectangle(0, this.Height/2-1, 20, 3));

				g.FillRectangle(sb, new Rectangle(12, this.Height / 2 - m_ValueArea/4, 8, 2));
				g.FillRectangle(sb, new Rectangle(12, this.Height / 2 + m_ValueArea / 4, 8, 2));

				//前後のバー

				g.FillRectangle(sb, new Rectangle(0, m_AreaTop, 20, 1));
				g.FillRectangle(sb, new Rectangle(0, m_AreaBottom, 20, 1));

				sb.Color = m_LineDark;
				g.FillRectangle(sb, new Rectangle(0, m_ValueTop, 10, m_ValuePos - m_ValueTop));
				g.FillRectangle(sb, new Rectangle(0, m_ValuePos + ARW.Height, 10, m_ValueBottom -m_ValuePos - ARW.Height  ));




				//アイコンの描画
				Rectangle r = new Rectangle(0, 0, 20, 22);
				if (m_Maximum <= 0) {
					g.DrawImage(C1, r);
				}
				else if (m_mouseDownMode == MD.TOP_BTN)
				{
					g.DrawImage(TBD, r);
				}
				else
				{
					g.DrawImage(TB, r);
				}
				r.Y += TB.Height;
				if (m_Maximum <= 0)
				{
					g.DrawImage(C2, r);
				}
				else if (m_mouseDownMode == MD.PUP_BTN)
				{
					g.DrawImage(TAD, r);
				}
				else
				{
					g.DrawImage(TA, r);
				}
				r.Y = this.Height - BB.Height;
				if (m_Maximum <= 0)
				{
					g.DrawImage(C2, r);
				}
				else if (m_mouseDownMode == MD.END_BTN)
				{
					g.DrawImage(BBD, r);
				}
				else
				{
					g.DrawImage(BB, r);
				}
				r.Y -= BA.Height;
				if (m_Maximum <= 0)
				{
					g.DrawImage(C1, r);
				}
				else if (m_mouseDownMode == MD.PDOWN_BTN)
				{
					g.DrawImage(BAD, r);
				}
				else
				{
					g.DrawImage(BA, r);

				}
				//
				r = new Rectangle(2, m_ValuePos, ARW.Width, ARW.Height);
				if (m_Maximum <= 0)
				{
				}
				else if (m_mouseDownMode==MD.CURSOR)
				{
					g.DrawImage(ARWD, r);
				}
				else
				{
					g.DrawImage(ARW, r);
				}

			}
			finally
			{
				sb.Dispose();
			}




		}
		//------------------------------------------------
		public int Maximum
		{
			get { return m_Maximum; }
			set
			{
				int v = value;
				if (v < 0) v = 0;
				if (m_Maximum != v)
				{
					m_Maximum = v;
					if (m_Value > m_Maximum)
					{
						m_Value = m_Maximum;
						OnChangeValueEvent(new EventArgs());
					}
					OnChangeMinimumSizeEvent(new EventArgs());
					this.Invalidate();
				}
			}
		}
		//------------------------------------------------
		public int Value
		{
			get { return m_Value; }
			set
			{
				int v = value;
				if (v < 0) v = 0;
				else if (v > m_Maximum) v = m_Maximum;
				bool b = (m_Value != v);
				m_Value = v;
				this.Invalidate();
				if (b) OnChangeValueEvent(new EventArgs());
				
			}
		}
		//--------------------------------------------------------
		protected override void OnMouseDown(MouseEventArgs e)
		{
			int y = e.Y;
			if ((y < 0) || (y >= this.Height)||(m_Maximum<=0)) return;


			int v = 0;
			bool b = false;
			if (y < TB.Height)
			{
				m_mouseDownMode = MD.TOP_BTN;
			}
			else if (y < TB.Height+TA.Height)
			{
				m_mouseDownMode = MD.PUP_BTN;

			}else if (y<m_ValuePos)
			{
				v  = (y - ARW.Height/2 - m_ValueTop)* m_Maximum/ m_ValueArea;
				if (v < 0) v = 0;
				b = (m_Value != v);
				m_Value = v;

			}
			else if (y< m_ValuePos + ARW.Height)
			{
				m_mouseDownMode = MD.CURSOR;
				m_DownValue = m_Value;
				m_DownvaluePos = y;

			}
			else if (y < m_AreaBottom + ARW.Height / 2)
			{
				v = (y - ARW.Height / 2 - m_ValueTop) * m_Maximum / m_ValueArea;
				if (v > m_Maximum) v = m_Maximum;
				b = (m_Value != v);
				m_Value = v;

			}
			else if (y< m_AreaBottom + ARW.Height / 2 + BA.Height)
			{
				m_mouseDownMode = MD.PDOWN_BTN;

			}
			else
			{
				m_mouseDownMode = MD.END_BTN;
			}
			//base.OnMouseDown(e);
			this.Invalidate();
			if (b) OnChangeValueEvent(new EventArgs());
		}
		//--------------------------------------------------------
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (m_mouseDownMode == MD.CURSOR)
			{
				int dy = e.Y - m_DownvaluePos;
				m_Value = m_DownValue + (dy * m_Maximum / m_ValueArea);
				if (m_Value < 0) m_Value = 0;
				else if (m_Value > m_Maximum) m_Value = m_Maximum;
				this.Invalidate();
				OnChangeValueEvent(new EventArgs());
			}
			//base.OnMouseMove(e);
		}
		//--------------------------------------------------------
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (m_mouseDownMode == MD.NONE) return;
			bool b = false;
			int v = 0;
			switch (m_mouseDownMode)
			{
				case MD.TOP_BTN:
					v = 0;
					break;
				case MD.END_BTN:
					v = m_Maximum;
					break;
				case MD.PUP_BTN:
					v = m_Value - 100;
					if (v < 0) v = 0;
					break;
				case MD.PDOWN_BTN:
					v = m_Value + 100;
					if (v > m_Maximum) v = m_Maximum;
					break;
				case MD.CURSOR:
					v = m_Value;
					break;
			}
			m_mouseDownMode = MD.NONE;
			m_DownValue = 0;
			m_DownvaluePos = 0;
			b = (m_Value != v);
			m_Value = v;
			this.Invalidate();
			if (b) OnChangeValueEvent(new EventArgs());
			//base.OnMouseUp(e);
		}
	}
}
