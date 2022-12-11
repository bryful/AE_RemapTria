using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BRY;
using PdfSharpCore.Drawing;

namespace AE_RemapTria
{
	public partial class TR_Input : TR_Control
	{
		public TR_Input()
		{
			m_FontSize = 8;
			ChkOffScr();
		}
		//-------------------------------------------
		public override void SetTRForm(TR_Form fm)
		{
			m_form = fm;
			if (m_form != null)
			{
				m_font = m_form.MyFont(m_FontIndex, m_FontSize, m_form.FontStyle);
				SetLocSize();
				ChkOffScr();
			}
		}
		//------------------------------------------
		public override void SetLocSize()
		{
			if (m_form != null)
			{
				Point p = new Point(
				m_form.Sizes.FrameWidth2,
				m_form.Sizes.MenuHeight + m_form.Sizes.InterHeight
				+m_form.Sizes.CaptionHeight2
				);
				if (m_Location != p) this.Location = p;
				
				Size z = new Size(
					m_form.Sizes.FrameWidth - m_form.Sizes.FrameWidth2,
					m_form.Sizes.CaptionHeight);
				if (m_Size != z)
				{
					m_Size = z;
					ChkOffScr();
				}
			}
		}
		//------------------------------------------
	

		// ************************************************************************
		/*

		*/
		// ************************************************************************
		//------------------------------------------
		public override void Draw(Graphics g)
		{
			if (m_form == null) return;
			SolidBrush sb = new SolidBrush(m_form.Colors.Moji);
			Pen p = new Pen(m_form.Colors.InputLineA);
			try
			{
				Rectangle rct = new Rectangle(0, 0, Width, Height);

				if (m_form.Value >= 0)
				{
					sb.Color = m_form.Colors.Moji;
					m_form.Alignment = StringAlignment.Far;
					DrawStr(g, $"{m_form.Value}", sb, rct);
					p.Color = m_form.Colors.InputLineA;
				}
				else
				{
					p.Color = m_form.Colors.InputLine;
				}
				DrawFrame(g, p, rct, 1);
			}
			finally
			{
				sb.Dispose();
				p.Dispose();
			}
				
		}
	}
}
