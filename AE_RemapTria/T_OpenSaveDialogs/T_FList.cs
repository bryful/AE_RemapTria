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
	public enum FListType
	{
		Dir,
		File
	}
	public class DirChangedArg : EventArgs
	{
		public string Dir;
		public DirChangedArg(string v)
		{
			Dir = v;
		}
	}
	public partial class T_FList : T_BaseControl
	{
		public delegate void DirChangedHandler(object sender, DirChangedArg e);
		public event DirChangedHandler DireChanged;
		protected virtual void OnDirChanged(DirChangedArg e)
		{
			if (DireChanged != null)
			{
				DireChanged(this, e);
			}
		}
		private DirectoryInfo m_dir = new DirectoryInfo("C:\\");
		public string FullName
		{
			get { return m_dir.FullName; }
			set
			{
				DirectoryInfo dir = new DirectoryInfo(value);
				if (dir.Exists)
				{
					if(m_dir.FullName!=dir.FullName)
					{
						m_dir = dir;
						Listup();
						OnDirChanged(new DirChangedArg(m_dir.FullName));
						if(m_VScrBar!=null)
						{
							m_VScrBar.MaxValue = m_DispMaxY;
							m_VScrBar.Value = m_DispY;
						}
					}
				}
			}
		}
		private FInfo[] m_Items = new FInfo[0];
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
		private int m_ScrolBarWidth = 20;
		public int ScrolBarWidth
		{
			get { return m_ScrolBarWidth; }
			set { m_ScrolBarWidth = value; this.Invalidate(); }
		}
		private Color m_FrameColor = Color.FromArgb(180, 180, 220);
		public Color FrameColor
		{
			get { return m_FrameColor; }
			set { m_FrameColor = value; this.Invalidate(); }
		}
		private Color m_SelectedColor = Color.FromArgb(90, 90, 120);
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
				if ((value >= 0) && (value < m_Items.Length))
				{
					if (m_SelectedIndex != value)
					{
						m_SelectedIndex = value;
						this.Invalidate();
					}
				}
			}
		}
		private string[] m_TragetExt = new string[] { ".ardj.json", ".ardj", ".ardj", ".sts"};
		public string[] TragetExt
		{
			get { return m_TragetExt; }
			set 
			{
				if(m_TragetExt != value)
				{
					m_TragetExt = value;
					Listup();
					this.Invalidate();
				}
			}
		}
		// *****************************************************************
		public T_FList()
		{
			this.ForeColor = Color.FromArgb(200, 200, 250);
			this.Size = new Size(200, 100);
			InitializeComponent();
			Listup();
		}
		// *****************************************************************
		private void Listup()
		{
			if (m_dir.Exists == false) return;
			m_Items = new FInfo[0];


			List<FInfo> lst = new List<FInfo>();

			int cnt = 0;
			if(m_dir.Parent!=null)
			{
				try
				{
					FInfo fi = new FInfo(m_dir.Parent, cnt);
					fi.Caption = "<Parent>";
					lst.Add(fi);
					cnt++;
				}
				catch
				{

				}
			}
			DirectoryInfo[] dis = m_dir.GetDirectories();
			if (dis.Length > 0)
			{
				for (int i = 0; i < dis.Length; i++)
				{
					FInfo fi = new FInfo(dis[i], cnt);
					fi.Caption = "<" + fi.Caption + ">";
					lst.Add(fi);
					cnt++;
				}
			}
			FileInfo[] fls = m_dir.GetFiles();
			if (fls.Length > 0)
			{
				for (int i = 0; i < fls.Length; i++)
				{
					FInfo fi = new FInfo(fls[i], cnt);
					if(fi.IsExt(m_TragetExt)==true)
					{
						fi.Caption = " " + fi.Caption;
						lst.Add(fi);
						cnt++;
					}
				}
			}
			m_Items = lst.ToArray();
			CalcDisp();
			this.Invalidate();
		}
		// *****************************************************************
		private void CalcDisp()
		{
			int mz = m_Items.Length * m_RowHeight;
			m_DispMaxY = mz - this.Height;
			if (m_DispMaxY < 0) m_DispMaxY = 0;
			if (m_DispY > m_DispMaxY) m_DispY = m_DispMaxY;

			int ls = mz - (m_DispY + this.Height);
			if (ls < 0) ls = 0;

			int tcnt = m_DispY / m_RowHeight;
			int lcnt = ls / m_RowHeight;
			m_RowTop = tcnt;
			m_RowBottom = m_Items.Length - lcnt;
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
				if (m_Items.Length > 0)
				{
					CalcDisp();
					StringFormat sf = new StringFormat();
					sf.Alignment = StringAlignment.Near;
					sf.LineAlignment = StringAlignment.Near;

					for (int i = m_RowTop; i < m_RowBottom; i++)
					{
						r = new Rectangle(5, m_RowHeight * i - m_DispY, this.Width - 5, m_RowHeight);
						if (i == m_SelectedIndex)
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

		private void T_VScrolBar_ValueChanged(object sender, ValueChangedArg e)
		{
			m_DispY = ((T_VScrBar)sender).Value;
			this.Invalidate();
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			CalcDisp();
			this.Invalidate();
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			int idx = (e.Y + m_DispY) / m_RowHeight;

			if ((idx >= 0) && (idx < m_Items.Length))
			{
				m_SelectedIndex = idx;
				this.Invalidate();
			}

			base.OnMouseDown(e);
		}
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			int v = e.Delta * SystemInformation.MouseWheelScrollLines / 15;
			DispY -= v;
			base.OnMouseWheel(e);
		}
	}
}
