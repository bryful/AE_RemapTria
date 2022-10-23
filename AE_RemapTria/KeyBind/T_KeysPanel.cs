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
	public partial class T_KeysPanel : T_BaseControl
	{
		private Color m_BorderColor= Color.FromArgb(100, 100, 180);
		public Color BorderColor
		{
			get { return m_BorderColor; }
			set { m_BorderColor = value; this.Invalidate(); }
		}

		private Keys m_key = Keys.None;
		public Keys Key
		{
			get { return m_key; }
			set { m_key = value; this.Invalidate(); }
		}
		private bool m_IsTargetMode = false;
		public bool IsTargetMode
		{
			get { return m_IsTargetMode; }
			set { m_IsTargetMode = value; this.Invalidate(); }
		}
		public T_KeysPanel()
		{
			this.ForeColor = Color.FromArgb(200, 200, 250);
			InitializeComponent();
		}
		private string KeyToStr(Keys k)
		{
			return k.ToString();
		}


		protected override void OnPaint(PaintEventArgs pe)
		{
			
			Graphics g = pe.Graphics;
			SolidBrush sb = new SolidBrush(this.BackColor);
			Pen p = new Pen(this.ForeColor);
			try
			{

				Fill(g, sb);
				StringFormat sf = new StringFormat();
				sf.Alignment = StringAlignment.Center;
				sf.LineAlignment = StringAlignment.Center;
				sb.Color = this.ForeColor;
				g.DrawString(KeyToStr(m_key), this.Font, sb, this.ClientRectangle, sf);
				if(m_IsTargetMode)
				{
					DrawFrame(g, p,new Rectangle(3,3,this.Width-6,this.Height-6), 1);

				}
				p.Color = this.BorderColor;
				DrawFrame(g, p);

			}
			finally
			{
				sb.Dispose();
				p.Dispose();
			}

			//base.OnPaint(pe);
		}
	}
}
