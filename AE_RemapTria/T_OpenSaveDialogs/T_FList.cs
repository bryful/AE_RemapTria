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
		#region Event
		public delegate void DirChangedHandler(object sender, DirChangedArg e);
		public event DirChangedHandler DirChanged;
		protected virtual void OnDirChanged(DirChangedArg e)
		{
			if (DirChanged != null)
			{
				DirChanged(this, e);
			}
		}
		// ************************
		public delegate void SelectedFileHandler(object sender, EventArgs e);
		public event SelectedFileHandler SelectedFileChanged;
		protected virtual void OnSelectedFileChanged(EventArgs e)
		{
			if (SelectedFileChanged != null)
			{
				SelectedFileChanged(this, e);
			}
		}
		#endregion

		// *************************************************************************
		private FInfo[] m_Items = new FInfo[0];
		// *************************************************************************
		#region IO
		private DirectoryInfo m_dir = new DirectoryInfo("C:\\");
		public DirectoryInfo Dir
		{
			get { return m_dir; }
			set
			{
				if(m_dir.FullName!=value.FullName)
				{
					m_dir = value;
					Listup();
				}
			}
		}
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
					}
				}
			}
		}
		#endregion
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
		private Color m_ForcusColor = Color.FromArgb(75, 75, 120);
		public Color ForcusColor
		{
			get { return m_ForcusColor; }
			set { m_ForcusColor = value; this.Invalidate(); }
		}
		public int m_ForcusIndex = -1;
		#endregion

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
		#region Contrl
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
		private T_Label? m_DirectoryLabel = null;
		public T_Label? DirectoryLabel
		{
			get { return m_DirectoryLabel; }
			set
			{
				m_DirectoryLabel = value;
			}
		}
		private T_TextBox? m_FileTextBox = null;
		public T_TextBox? FileTextBox
		{
			get { return m_FileTextBox; }
			set
			{
				m_FileTextBox = value;
			}
		}

		private void T_VScrolBar_ValueChanged(object sender, ValueChangedArg e)
		{
			m_DispY = ((T_VScrBar)sender).Value;
			this.Invalidate();
		}
		#endregion
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
			try
			{
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
						if (fi.IsExt(m_TragetExt) == true)
						{
							fi.Caption = " " + fi.Caption;
							lst.Add(fi);
							cnt++;
						}
					}
				}
			}
			catch { }
			m_Items = lst.ToArray();
			CalcDisp();
			OnDirChanged(new DirChangedArg(m_dir.FullName));
			this.Invalidate();
			if(m_DirectoryLabel != null)
			{
				m_DirectoryLabel.Text = FullName;
			}
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
						if(i==m_ForcusIndex)
						{
							sb.Color = m_ForcusColor;
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
		protected override void OnMouseMove(MouseEventArgs e)
		{
			int idx = (e.Y + m_DispY) / m_RowHeight;
			if((idx>=0)&&(idx<m_Items.Length))
			{
				if(idx!=m_ForcusIndex)
				{
					m_ForcusIndex = idx;
					this.Invalidate();
				}
			}
			base.OnMouseMove(e);
		}
		protected override void OnMouseLeave(EventArgs e)
		{
			if(m_ForcusIndex>=0)
			{
				m_ForcusIndex = -1;
				this.Invalidate();
			}
			base.OnMouseLeave(e);
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

			if ((idx >= 0) && (idx < m_Items.Length))
			{
				this.Invalidate();
				if (m_Items[idx].IsDir)
				{
					if(m_Items[idx].Directory!=null)
					{
						m_dir = m_Items[idx].Directory;
						m_DispY = 0;
						Listup();
					}
				}
			}
			base.OnMouseDoubleClick(e);
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			int idx = (e.Y + m_DispY) / m_RowHeight;

			if ((idx >= 0) && (idx < m_Items.Length))
			{
				this.Invalidate();
				if (m_Items[idx].IsFile)
				{
					if (m_Items[idx].Directory != null)
					{
						OnSelectedFileChanged(new EventArgs());
						if(m_FileTextBox!=null)
						{
							m_FileTextBox.Text = m_Items[idx].Name;
						}
					}
				}
			}
			base.OnMouseDown(e);
		}
		#endregion
	}
}
