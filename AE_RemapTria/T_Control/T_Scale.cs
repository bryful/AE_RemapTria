using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BRY;
namespace AE_RemapTria
{
	public partial class T_Scale : TR_DialogControl
	{
		private DrawSCalePrm m_sp = new DrawSCalePrm();
		[Category("_AE_Remap")]
		public int CenterLength
		{
			get { return m_sp.CenterLength; }
			set { m_sp.CenterLength = value; }
		}
		[Category("_AE_Remap")]
		public int CenterWeight
		{
			get { return m_sp.CenterWeight; }
			set { m_sp.CenterWeight = value; }
		}


		public Scale_Style Style
		{
			get {  return m_sp.Style; }
			set { m_sp.Style = value; this.Invalidate(); }
		}


		[Category("_AE_Remap")]
		public int[] Inter
		{
			get { return m_sp.Inter; }
			set
			{
				m_sp.Inter = value;
				this.Invalidate();
			}
		}
		[Category("_AE_Remap")]
		public int []Weight
		{
			get { return m_sp.Weight; }
			set
			{
				m_sp.Weight = value;
				this.Invalidate();
			}
		}
		[Category("_AE_Remap")]
		public int []Length
		{
			get { return m_sp.Length; }
			set
			{
				m_sp.Length = value;
				this.Invalidate();
			}
		}
		[Category("_AE_Remap")]
		public int []Count
		{
			get { return m_sp.Count; }
			set
			{
				m_sp.Count = value;
				this.Invalidate();
			}
		}
		public T_Scale()
		{
			this.Size = new Size(100, 200);
			this.ForeColor = Color.FromArgb(128, 200,200, 250);
			InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			Graphics g =pe.Graphics;
			SolidBrush sb = new SolidBrush(this.BackColor);
			Pen p = new Pen(this.ForeColor);
			try
			{
				Fill(g, sb);
				switch(m_sp.Style)
				{
					case Scale_Style.Left:
						m_sp.Loc = new Point(0, this.Height / 2);
						T_G.DrawScale(g, p, m_sp, Scale_Style.Left);
						break;
					case Scale_Style.Right:
						m_sp.Loc = new Point(this.Width, this.Height / 2);
						T_G.DrawScale(g, p, m_sp, Scale_Style.Right);
						break;
					case Scale_Style.Top:
						m_sp.Loc = new Point(this.Width/2, 0);
						T_G.DrawScale(g, p, m_sp, Scale_Style.Top);
						break;
					case Scale_Style.Bottom:
						m_sp.Loc = new Point(this.Width / 2, this.Height);
						T_G.DrawScale(g, p, m_sp, Scale_Style.Bottom);
						break;
				}
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
