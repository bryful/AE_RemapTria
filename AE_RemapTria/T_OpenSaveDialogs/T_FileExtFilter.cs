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
	public partial class T_FileExtFilter : T_BaseControl
	{
		private Color m_SelectedColor = Color.FromArgb(90, 90, 120);
		public Color SelectedColor
		{
			get { return m_SelectedColor; }
			set { m_SelectedColor = value; this.Invalidate(); }
		}
		// ***************************************************
		private string[] m_exts = new string[] { ".ardj.json", ".ardj", ".ard", ".sts", ".*" };
		private int[] m_extsYPos = new int[] { 10,10,10,10,10 };
		private int m_FilterIndex = 0;
		// ***************************************************
		public T_FileExtFilter()
		{
			this.ForeColor = Color.FromArgb(200, 200, 250);
			InitializeComponent();
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			for(int i=0; i<m_extsYPos.Length; i++)
			{
				if (e.X < m_extsYPos[i])
				{
					m_FilterIndex = i;
					this.Invalidate();
					break;
				}
			}
			base.OnMouseDown(e);
		}
		protected override void OnPaint(PaintEventArgs pe)
		{
			//base.OnPaint(pe);
			Graphics g = pe.Graphics;
			SolidBrush sb = new SolidBrush(Color.Transparent);
			Pen p = new Pen(this.ForeColor);
			StringFormat sf = new StringFormat();
			sf.Alignment = StringAlignment.Center;
			sf.LineAlignment = StringAlignment.Center;

			try
			{
				Rectangle r;
				Fill(g, sb);
				if(m_exts.Length>0)
				{
					int left = 0;
					for(int i=0; i< m_exts.Length; i++)
					{
						Size strSize = TextRenderer.MeasureText(g, m_exts[i], this.Font);
						r = new Rectangle(left, 0, strSize.Width + 10 -1, this.Height-1);
						m_extsYPos[i] = r.Right;
						if (m_FilterIndex==i)
						{
							sb.Color = m_SelectedColor;
							g.FillRectangle(sb, r);
						}
						g.DrawRectangle(p, r);
						sb.Color = this.ForeColor;
						g.DrawString(m_exts[i], this.Font, sb, r, sf);
						left += strSize.Width + 14;
					}
				}

			}
			finally
			{
				sb.Dispose();
				p.Dispose();
			}
		}
	}
}
