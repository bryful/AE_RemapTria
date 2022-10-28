﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AE_RemapTria
{
	public class DriveChangedArg : EventArgs
	{
		public string Dir;
		public DriveChangedArg(string v)
		{
			Dir = v;
		}
	}
	public partial class T_DriveList : T_BaseControl
	{
		#region Event
		public delegate void DriveChangedHandler(object sender, DriveChangedArg e);
		public event DriveChangedHandler DriveChanged;
		protected virtual void OnDriveChanged(DriveChangedArg e)
		{
			if (DriveChanged != null)
			{
				DriveChanged(this, e);
			}
		}
		#endregion
		// **********************************************************************
		private FInfo[] m_drives = new FInfo[0];
		// **********************************************************************
		#region Prop
		private bool m_IsHor = true;
		public bool IsHor
		{
			get { return m_IsHor; }
			set { m_IsHor = value; this.Invalidate(); }
		}
		private int m_SelectedIndex = -1;
		public int SelectedIndex
		{
			get { return m_SelectedIndex; }
			set
			{
				int v = value;
				if (v >= m_drives.Length) v = m_drives.Length - 1;
				SetSelectedIndex(v);
			}
		}
		private void SetSelectedIndex(int idx)
		{
			if (m_SelectedIndex != idx)
			{
				m_SelectedIndex = idx;
				if ((m_FList != null) && (m_SelectedIndex >= 0))
				{
					m_FList.FullName = FullName;
					this.Invalidate();
				}
			}
		}
		#endregion

		#region Layout
		private int m_IconWidth = 32;
		private int m_IconHeight = 32;
		public int IconWidth
		{
			get { return m_IconWidth; }
			set { m_IconWidth = value; this.Invalidate(); }
		}
		public int IconHeight
		{
			get { return m_IconHeight; }
			set { m_IconHeight = value; this.Invalidate(); }
		}
		private Color m_IconBaseColor = Color.FromArgb(30, 50, 70);
		public Color IconBaseColor
		{
			get { return m_IconBaseColor; }
			set { m_IconBaseColor = value; this.Invalidate(); }
		}
		private Color m_IconFrameColor = Color.FromArgb(100, 100, 150);
		public Color IconFrameColor
		{
			get { return m_IconFrameColor; }
			set { m_IconFrameColor = value; this.Invalidate(); }
		}
		private Color m_IconFrameColorLo = Color.FromArgb(50, 50, 75);
		public Color IconFrameColorLo
		{
			get { return m_IconFrameColorLo; }
			set { m_IconFrameColorLo = value; this.Invalidate(); }
		}
		#endregion
		public string FullName
		{
			get 
			{
				if ((m_SelectedIndex >= 0) && (m_SelectedIndex < m_drives.Length))
				{
					return m_drives[m_SelectedIndex].FullName;
				}
				else
				{
					return "";
				}
			}
			set
			{

				int idx = -1;
				if ((value == null)|| (value == "")) return;
				DirectoryInfo di = new DirectoryInfo(value);
				if (di.Exists ==true)
				{
					char s = di.FullName.Substring(0, 1).ToUpper()[0];
					for (int i = 0; i < m_drives.Length; i++)
					{
						if (m_drives[i].DriveLetter == s)
						{
							idx = i;
							break;
						}
					}
					if(di.FullName != m_drives[idx].FullName)
					{
						m_drives[idx].SetDir(value);
						SetSelectedIndex(idx);
					}

				}
				//this.Invalidate();
			}
		}
		// ***********************************************************************
		public T_DriveList()
		{
			//this.Size = new Size(200, 40);
			this.ForeColor = Color.FromArgb(200, 200, 250);
			InitializeComponent();
			GetDrives();
		}
		// ***********************************************************************
		public void ReLoad()
		{
			GetDrives();
		}
		private void GetDrives()
		{
			ManagementObject mo = new ManagementObject();

			string[] drives = Environment.GetLogicalDrives();
			List<FInfo> list = new List<FInfo>();
			int idx = 0;
			foreach (string drive in drives)
			{
				DriveInfo di = new DriveInfo(drive);
				list.Add(new FInfo(di,idx));
				idx++;
			}
			m_drives = list.ToArray();
			if ((m_SelectedIndex<0)&&(m_drives.Length > 0)) m_SelectedIndex = 0;
			this.Invalidate();
		}
		// *****************************************************************
		#region Mouse Event
		protected override void OnMouseDown(MouseEventArgs e)
		{
			//base.OnMouseDown(e);
			int idx;
			if(m_IsHor)
			{
				idx = e.X / m_IconWidth;
			}
			else
			{
				idx = e.Y / m_IconHeight;
			}
			if ((idx >= 0) && (idx < m_drives.Length))
			{
				SetSelectedIndex(idx);
			}

		}
		#endregion
		// *****************************************************************
		protected override void OnPaint(PaintEventArgs pe)
		{
			//base.OnPaint(pe);
			Graphics g = pe.Graphics;
			SolidBrush sb = new SolidBrush(Color.Transparent);
			Pen p = new Pen(m_IconFrameColor,2);
			StringFormat sf = new StringFormat();
			sf.Alignment = StringAlignment.Center;
			sf.LineAlignment = StringAlignment.Center;
			try
			{
				Rectangle r1,r2;
				Fill(g, sb);
				int cnt = this.Width / m_IconWidth;
				for (int i = 0; i < cnt; i++)
				{
					if (m_IsHor)
					{
						r1 = new Rectangle(m_IconWidth * i, 0, m_IconWidth, m_IconHeight);
						r2 = new Rectangle(m_IconWidth * i + 1, 1, m_IconWidth - 3, m_IconHeight - 3);
					}
					else
					{
						r1 = new Rectangle(0, m_IconHeight*i, m_IconWidth, m_IconHeight);
						r2 = new Rectangle(1, m_IconHeight * i+1, m_IconWidth - 3, m_IconHeight - 3);
					}
					if (i < m_drives.Length)
					{
						sb.Color = m_IconBaseColor;
						g.FillRectangle(sb, r1);
						if (i == m_SelectedIndex)
						{
							p.Color = this.ForeColor;
						}
						else
						{
							p.Color = m_IconFrameColor;
						}
						g.DrawRectangle(p, r2);


						sb.Color = this.ForeColor;
						g.DrawString(m_drives[i].Caption, this.Font, sb, r2, sf);
					}
					else
					{
						p.Color = m_IconFrameColorLo;
						g.DrawRectangle(p, r2);
					}
				}
				
			}
			finally
			{
				sb.Dispose();
				p.Dispose();
			}
			
		}

		private T_FList? m_FList = null;
		public T_FList? FList
		{
			get { return m_FList; }
			set
			{
				m_FList = value;
				if(m_FList != null)
				{
					m_FList.DirChanged += M_FList_DirChanged;
				}
			}
		}

		private void M_FList_DirChanged(object sender, DirChangedArg e)
		{
			FullName = e.Dir;
		}
	}
}
