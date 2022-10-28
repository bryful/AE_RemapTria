using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AE_RemapTria
{
	public partial class T_BList : T_BaseControl
	{
		#region Event
		public delegate void DirChangedHandler(object sender, DirChangedArg e);
		public event DirChangedHandler DireChanged;
		protected virtual void OnDirChanged(DirChangedArg e)
		{
			if (DireChanged != null)
			{
				DireChanged(this, e);
			}
		}
		// ********************
		public delegate void SelectedIndexChangedHandler(object sender, EventArgs e);
		public event SelectedIndexChangedHandler SelectedIndexChanged;
		protected virtual void OnSelectedIndexChanged(EventArgs e)
		{
			if (SelectedIndexChanged != null)
			{
				SelectedIndexChanged(this, e);
			}
		}
		#endregion
		// *********************************************************************
		private List<FInfo> m_Items = new List<FInfo> ();
		public int Count
		{
			get { return m_Items.Count; }
		}
		// *********************************************************************
		#region Layout
		private int m_RowHeight = 20;
		public int RowHeight
		{
			get { return m_RowHeight; }
			set { m_RowHeight = value; this.Invalidate(); }
		}
		private int m_DispMaxY = 0;
		public int DispMaxY
		{
			get { return m_DispMaxY; }
		}
		private int m_RowTop = 0;
		private int m_RowBottom = 0;

		private int m_DispY = 0;
		public int DispY
		{
			get { return m_DispY; }
			set 
			{
				int v = value;
				if (v < 0) v = 0;
				else if (v > m_DispMaxY) v = m_DispMaxY;
				if(m_DispY != v)
				{
					m_DispY = v;
					if(m_VScrBar!=null)
					{
						m_VScrBar.Value = m_DispY;
						m_VScrBar.MaxValue = m_DispMaxY;

					}
					this.Invalidate();

				}
			}
		}
		private Color m_FrameColor = Color.FromArgb(180, 180, 220);
		public Color FrameColor
		{
			get { return m_FrameColor; }
			set { m_FrameColor = value; this.Invalidate(); }
		}
		private Color m_SelectedColor = Color.FromArgb(75, 75, 120);
		public Color SelectedColor
		{
			get { return m_SelectedColor; }
			set { m_SelectedColor = value; this.Invalidate(); }
		}
		public int m_SelectedIndex = -1;
		public int SelectedIndex
		{
			get { return m_SelectedIndex; }
			set 
			{
				int v = value;
				if (v < 0) v = -1;
				else if(v >= m_Items.Count) v = m_Items.Count-1;

				if(m_SelectedIndex != v)
				{
					m_SelectedIndex = value;
					OnSelectedIndexChanged(new EventArgs());
				}
				this.Invalidate(); 
			}
		}
		#endregion
		#region Control
		private T_VScrBar? m_VScrBar = null;
		public T_VScrBar? VScrBar
		{
			get { return m_VScrBar; }
			set
			{
				m_VScrBar = value;
				if (m_VScrBar != null)
				{
					m_VScrBar.ValueChanged += T_VScrolBar_ValueChanged;

					m_VScrBar.MaxValue = m_DispMaxY;
					m_VScrBar.Value = m_DispY;
				}
			}
		}
		#endregion
		private void T_VScrolBar_ValueChanged(object sender, ValueChangedArg e)
		{
			m_DispY = ((T_VScrBar)sender).Value;
			this.Invalidate();
		}
		// *****************************************************************
		public T_BList()
		{
			this.ForeColor = Color.FromArgb(200, 200, 250);
			this.Size = new Size(200, 100);
			InitializeComponent();

			AddDir(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),"Desktop");
			AddDir(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Doc");

		}
		// *****************************************************************
		public void SwapItem(int c0, int c1)
		{
			if((c0 == c1)||(c0<0)||(c0>=m_Items.Count) || (c1 < 0) || (c1 >= m_Items.Count))return;

			FInfo fi = m_Items[c0];
			m_Items[c0] = m_Items[c1];
			m_Items[c1] = fi;
		}
		// *****************************************************************
		public void  ItemUp()
		{
			if((m_SelectedIndex>=1)&&(m_SelectedIndex< m_Items.Count))
			{
				SwapItem(m_SelectedIndex, m_SelectedIndex - 1);
				m_SelectedIndex--;
				this.Invalidate();
			}
		}
		public void ItemDown()
		{
			if ((m_SelectedIndex >= 0) && (m_SelectedIndex < m_Items.Count-1))
			{
				SwapItem(m_SelectedIndex, m_SelectedIndex + 1);
				m_SelectedIndex++;
				this.Invalidate();
			}
		}
		// *****************************************************************
		public int FindItem(string p)
		{
			int ret = -1;
			if(m_Items.Count>0)
			{
				for(int i=0; i< m_Items.Count;i++)
				{
					if (m_Items[i].FullName == p)
					{
						ret = i;
						break;
					}
				}
			}
			return ret;
		}
		// *****************************************************************
		public bool AddDir(string p,string cap)
		{
			int idx = FindItem(p);
			if (idx >= 0) return false;
			DirectoryInfo di = new DirectoryInfo(p);
			FInfo fi = new FInfo(di, m_Items.Count);
			fi.Caption = cap;
			m_Items.Add(fi);
			CalcDisp();
			this.Invalidate();
			return true;
		}
		// *****************************************************************
		private void CalcDisp()
		{
			int mz = m_Items.Count * m_RowHeight;
			m_DispMaxY = mz - this.Height;
			if (m_DispMaxY < 0) m_DispMaxY = 0;
			if (m_DispY > m_DispMaxY) m_DispY = m_DispMaxY;

			int ls = mz - (m_DispY + this.Height);
			if (ls < 0) ls = 0;

			int tcnt = m_DispY / m_RowHeight;
			int lcnt = ls / m_RowHeight;
			m_RowTop = tcnt;
			m_RowBottom = m_Items.Count - lcnt;

			if (m_VScrBar != null)
			{
				m_VScrBar.MaxValue = m_DispMaxY;
				m_VScrBar.Value = m_DispY;
			}
		}
		// *****************************************************************
		protected override void OnPaint(PaintEventArgs pe)
		{
			//base.OnPaint(pe);
			Graphics g = pe.Graphics;
			SolidBrush sb = new SolidBrush(Color.Transparent);
			Pen p = new Pen(this.ForeColor);
			try
			{
				Rectangle r;
				Fill(g, sb);
				if (m_Items.Count > 0)
				{
					CalcDisp();
					StringFormat sf = new StringFormat();
					sf.Alignment = StringAlignment.Near;
					sf.LineAlignment = StringAlignment.Near;

					for (int i = m_RowTop; i < m_RowBottom; i++)
					{
						r = new Rectangle(5, m_RowHeight * i - m_DispY, this.Width - 5, m_RowHeight);
						if(i==m_SelectedIndex)
						{
							sb.Color = m_SelectedColor;
							g.FillRectangle(sb, r);
						}
						sb.Color = this.ForeColor;
						g.DrawString(m_Items[i].Caption, this.Font, sb, r, sf);

					}
				}
				r = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
				p.Color = m_FrameColor;
				g.DrawRectangle(p, r);
			}
			finally
			{
				sb.Dispose();
			}
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			CalcDisp();
			this.Invalidate();
		}

		#region Mouse Event
		protected override void OnMouseDown(MouseEventArgs e)
		{
			int idx = (e.Y + m_DispY) / m_RowHeight;
			if ((idx >= 0) && (idx < m_Items.Count))
			{
				if (idx != m_SelectedIndex)
				{
					m_SelectedIndex = idx;
					OnSelectedIndexChanged(new EventArgs());
					this.Invalidate();
				}
			}else
			{
				base.OnMouseDown(e);
			}
		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			int v = e.Delta * SystemInformation.MouseWheelScrollLines / 15;
			DispY -= v;
			base.OnMouseWheel(e);
		}
		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			int idx = (e.Y + m_DispY) / m_RowHeight;

			if ((idx >= 0) && (idx < m_Items.Count))
			{
				m_SelectedIndex = idx;
				this.Invalidate();
				if (m_Items[idx].IsDir)
				{
				}
			}
			base.OnMouseDoubleClick(e);
		}
		#endregion
	}
}
