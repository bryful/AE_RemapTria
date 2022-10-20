﻿using System;
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
	public partial class T_Arrow : T_BaseControl
	{
		private DrawArrowPrm m_ap = new DrawArrowPrm();
		/*
		public int ArrowWidth
		{
			get { return m_ap.Width; }
			set { m_ap.Width = value; this.Invalidate(); }
		}
		public int ArrowHeight
		{
			get { return m_ap.Height; }
			set { m_ap.Height = value; this.Invalidate(); }
		}
		*/
		public bool IsCut
		{
			get { return m_ap.IsCut; }
			set { m_ap.IsCut = value; this.Invalidate(); }
		}
		public ArrowDir ArrowDir
		{
			get { return m_ap.Dir; }
			set { m_ap.Dir = value; this.Invalidate(); }
		}
		public T_Arrow()
		{
			this.ForeColor = Color.FromArgb(200, 200, 200, 250);
			InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			Graphics g = pe.Graphics;
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			SolidBrush sb = new SolidBrush(Color.Transparent);
			try
			{
				Fill(g, sb);
				sb.Color = this.ForeColor;
				switch(m_ap.Dir)
				{
					case ArrowDir.Top:
						m_ap.Loc = new Point(this.Width / 2, this.Height - 1);
						m_ap.Width = this.Width - 1;
						m_ap.Height = this.Height - 1;
						break;
					case ArrowDir.Left:
						m_ap.Loc = new Point(this.Width - 1, this.Height/2);
						m_ap.Width = this.Height-1;
						m_ap.Height = this.Width-1;
						break;
					case ArrowDir.Bottom:
						m_ap.Loc = new Point(this.Width / 2, 0);
						m_ap.Width = this.Width - 1;
						m_ap.Height = this.Height - 1;
						break;
					case ArrowDir.Right:
						m_ap.Loc = new Point(0, this.Height / 2);
						m_ap.Width = this.Height - 1;
						m_ap.Height = this.Width - 1;
						break;

				}
				T_G.DrawArrow(g, sb,m_ap);
			}
			finally
			{
				sb.Dispose();
			}
			//base.OnPaint(pe);
		}
	}
}