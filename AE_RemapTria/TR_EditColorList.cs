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
	public partial class TR_EditColorList : TR_DialogControl
	{
		private List<TR_EditColor> m_Items = new List<TR_EditColor>(); 
		private VScrollBar scrl = new VScrollBar();
		public int CaptionWidth
		{
			get 
			{
				if (m_Items.Count > 0)
				{
					return m_Items[0].CaptionWidth;
				}
				else
				{
					return 160;
				}
			}
			set 
			{
				if (m_Items.Count > 0)
				{
					foreach(TR_EditColor item in m_Items)
					{ 
						item.CaptionWidth = value;
					}
				}
			}
		}
		public int DispValue
		{
			get { return scrl.Value; }
			set
			{
				if (value < scrl.Minimum) value = scrl.Minimum;
				if (value > scrl.Maximum) value = scrl.Maximum;
				if (scrl.Value != value) 
				{
					scrl.Value = value;
				}
			}
		}
		public TR_EditColorList()
		{

			this.Size = new Size(250, 200);
			InitializeComponent();

			for(int i=(int)COLS.GLine;i< (int)COLS.Transparent;i++)
			{
				TR_EditColor m = new TR_EditColor();
				m.Location = new Point(0, (i-1) * 25);
				m.Size = new Size(this.Width - scrl.Width, 25); ;
				m.TargetCOLS = (COLS)i;
				m.TabStop = false;
				m.SetTRDialog(m_dialog);
				m.ValueChanged += M_ValueChanged;
				m_Items.Add(m);
				this.Controls.Add(m);
			}
			scrl.Location= new Point(this.Width-scrl.Width, 0);
			scrl.Size = new Size(scrl.Width, this.Height);
			this.Controls.Add(scrl);
			ChkSize();
			scrl.ValueChanged += Scrl_ValueChanged;
		}

		private void M_ValueChanged(object? sender, EventArgs e)
		{
		}

		private void Scrl_ValueChanged(object? sender, EventArgs e)
		{
			ChkScrol();
		}

		public override void SetTRDialog(TR_BaseDialog? bd)
		{
			base.SetTRDialog(bd);
			for (int i = 0; i < m_Items.Count; i++)
			{
				m_Items[i].SetTRDialog(m_dialog);
			}
		}
		protected override void OnPaint(PaintEventArgs pe)
		{
			Graphics g = pe.Graphics;
			//base.OnPaint(pe);
			SolidBrush sb = new SolidBrush(Color.Transparent);
			Pen p = new Pen(Color.Gray); 
			g.FillRectangle(sb,this.ClientRectangle);
			DrawFrame(g, p, this.ClientRectangle);
			sb.Dispose();
			p.Dispose();
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			ChkSize();
			ChkScrol();
		}
		public void ChkSize()
		{
			int sz = m_Items.Count * 25 - this.Height;
			if (sz < 0) { sz = 0; }
			scrl.Location = new Point(this.Width - scrl.Width, 0);
			scrl.Size = new Size(scrl.Width, this.Height);
			if (scrl.Maximum != sz) scrl.Maximum = sz;
		}
		public void ChkScrol()
		{
			this.SuspendLayout();
			for (int i = 0; i < m_Items.Count; i++)
			{
				m_Items[i].Location = new Point(0, i * 25 - scrl.Value);
				m_Items[i].Size = new Size(this.Width - 20, 25);
			}
			this.ResumeLayout();
		}
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			int v = e.Delta * SystemInformation.MouseWheelScrollLines / 15;
			int vv = scrl.Value - v;
			if (vv < scrl.Minimum) vv = scrl.Minimum;
			else if (vv > scrl.Maximum) vv = scrl.Maximum;
			if(scrl.Value != vv) scrl.Value = vv;
			base.OnMouseWheel(e);
		}
	}
}
