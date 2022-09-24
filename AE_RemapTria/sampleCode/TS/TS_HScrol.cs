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
	public class TS_HScrol : Control
	{
		enum MD
		{
			NONE = 0,
			LEFT_BTN,
			PLEFT_BTN,
			PLEFT,
			CURSOR,
			PRIGHT,

			PRIGHT_BTN,
			RIGHT_BTN,
		}
		public event EventHandler ChangeValueEvent;
		public event EventHandler ChangeMinimumSizeEvent;

		private int m_Maximum = 200;
		private int m_Value = 0;
		private int m_ValueArea = 0;
		private int m_ValuePos = 0;

		private Bitmap LA = AE_Remap_Drei.Properties.Resources._01_ScrolIconA_HL;
		private Bitmap LAD = AE_Remap_Drei.Properties.Resources._01_ScrolIconA_HL_dn;
		private Bitmap LB = AE_Remap_Drei.Properties.Resources._01_ScrolIconB_HL;
		private Bitmap LBD = AE_Remap_Drei.Properties.Resources._01_ScrolIconB_HL_dn;
		private Bitmap RA = AE_Remap_Drei.Properties.Resources._01_ScrolIconA_HR;
		private Bitmap RAD = AE_Remap_Drei.Properties.Resources._01_ScrolIconA_HR_dn;
		private Bitmap RB = AE_Remap_Drei.Properties.Resources._01_ScrolIconB_HR;
		private Bitmap BRBD = AE_Remap_Drei.Properties.Resources._01_ScrolIconB_HR_dn;
		private Bitmap ARW = AE_Remap_Drei.Properties.Resources._01_ScrolArrow_H;
		private Bitmap ARWD = AE_Remap_Drei.Properties.Resources._01_ScrolArrow_H_dn;

		private Bitmap C1 = AE_Remap_Drei.Properties.Resources._01_ScrolIconC_H1;
		private Bitmap C2 = AE_Remap_Drei.Properties.Resources._01_ScrolIconC_H2;


		private int m_DownValue = 0;
		private int m_DownvaluePos = 0;

		private MD m_mouseDownMode = MD.NONE;

		/// <summary>
		/// 最小ピクセル
		/// </summary>
		private int m_MinWidth;
		private int m_ValueLeft;
		private int m_AreaLeft;
		private int m_AreaRight;
		private int m_ValueRight;
		private int m_BarWidth;


		private Color m_LineDark = Color.FromArgb(128, 50, 140, 200);
		private Color m_Line = Color.FromArgb(128, 50, 190, 220);

		public TS_HScrol()
		{

			m_MinWidth = LA.Width + LB.Width + ARW.Width + RB.Width + RA.Width;
			m_ValueLeft = LA.Width + LB.Width;
			m_AreaLeft = LA.Width + LB.Width + ARW.Width / 2;

			this.MinimumSize = new Size(m_MinWidth,22 );
			
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
			m_ValueArea = this.Width - m_MinWidth;
			if (m_Maximum <= 0)
			{
				m_ValuePos = (this.Width - ARW.Width)/2;
			}
			else
			{
				m_ValuePos = m_ValueLeft + m_ValueArea * m_Value / m_Maximum;
			}
			m_AreaRight = this.Width - m_AreaLeft;
			m_ValueRight = this.Width - m_ValueLeft;
			m_BarWidth = m_ValueArea/2 - ARW.Width;

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
				g.FillRectangle(sb, new Rectangle(this.Width / 2 - 1,0, 3, 20));

				g.FillRectangle(sb, new Rectangle(this.Width / 2 - m_ValueArea / 4, 12, 2, 8));
				g.FillRectangle(sb, new Rectangle(this.Width / 2 + m_ValueArea / 4, 12, 2, 8));

				//前後のバー

				g.FillRectangle(sb, new Rectangle(m_AreaLeft, 0, 1, 20));
				g.FillRectangle(sb, new Rectangle(m_AreaRight, 0, 1, 20));

				sb.Color = m_LineDark;
				g.FillRectangle(sb, new Rectangle(m_ValueLeft, 0 , m_ValuePos - m_ValueLeft, 10));
				g.FillRectangle(sb, new Rectangle(m_ValuePos + ARW.Width, 0,  m_ValueRight -m_ValuePos - ARW.Width,10   ));




				//アイコンの描画
				Rectangle r = new Rectangle(0, 0, 22, 20);
				if (m_Maximum <= 0)
				{
					g.DrawImage(C1, r);
				}
				else if (m_mouseDownMode == MD.LEFT_BTN)
				{
					g.DrawImage(LBD, r);
				}
				else
				{
					g.DrawImage(LB, r);
				}
				r.X += LB.Width;
				if (m_Maximum <= 0)
				{
					g.DrawImage(C2, r);
				}
				else if (m_mouseDownMode == MD.PLEFT_BTN)
				{
					g.DrawImage(LAD, r);
				}
				else
				{
					g.DrawImage(LA, r);
				}
				r.X = this.Width - RB.Width;
				if (m_Maximum <= 0)
				{
					g.DrawImage(C2, r);
				}
				else if (m_mouseDownMode == MD.RIGHT_BTN)
				{
					g.DrawImage(BRBD, r);
				}
				else
				{
					g.DrawImage(RB, r);
				}
				r.X -= RA.Height;
				if (m_Maximum <= 0)
				{
					g.DrawImage(C1, r);
				}
				else if (m_mouseDownMode == MD.PRIGHT_BTN)
				{
					g.DrawImage(RAD, r);
				}
				else
				{
					g.DrawImage(RA, r);

				}
				//
				r = new Rectangle(m_ValuePos, 2, ARW.Width, ARW.Height);
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
			int x = e.X;
			if ((x < 0) || (x >= this.Width)) return;


			int v = 0;
			bool b = false;
			if (x < LB.Width)
			{
				m_mouseDownMode = MD.LEFT_BTN;
			}
			else if (x < LB.Width + LA.Width)
			{
				m_mouseDownMode = MD.PLEFT_BTN;

			}else if (x<m_ValuePos)
			{
				v  = (x - ARW.Height/2 - m_ValueLeft)* m_Maximum/ m_ValueArea;
				if (v < 0) v = 0;
				b = (m_Value != v);
				m_Value = v;

			}
			else if (x< m_ValuePos + ARW.Width)
			{
				m_mouseDownMode = MD.CURSOR;
				m_DownValue = m_Value;
				m_DownvaluePos = x;

			}
			else if (x < m_AreaRight + ARW.Width / 2)
			{
				v = (x - ARW.Height / 2 - m_ValueLeft) * m_Maximum / m_ValueArea;
				if (v > m_Maximum) v = m_Maximum;
				b = (m_Value != v);
				m_Value = v;

			}
			else if (x< m_AreaRight + ARW.Width / 2 + RA.Width)
			{
				m_mouseDownMode = MD.PRIGHT_BTN;

			}
			else
			{
				m_mouseDownMode = MD.RIGHT_BTN;
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
				int dx = e.X - m_DownvaluePos;
				m_Value = m_DownValue + (dx * m_Maximum / m_ValueArea);
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
				case MD.LEFT_BTN:
					v = 0;
					break;
				case MD.RIGHT_BTN:
					v = m_Maximum;
					break;
				case MD.PLEFT_BTN:
					v = m_Value - 100;
					if (v < 0) v = 0;
					break;
				case MD.PRIGHT_BTN:
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
