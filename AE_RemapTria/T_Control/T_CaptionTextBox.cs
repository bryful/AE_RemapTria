﻿using System;
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
	public partial class T_CaptionTextBox : T_BaseControl
	{
		private T_Label m_Label = new T_Label();
		private T_TextBox m_TextBox = new T_TextBox();

		public string Caption
		{
			get { return m_Label.Text; }
			set { m_Label.Text = value; }
		}
		public int CaptionWidth
		{
			get { return m_Label.Width; }
			set { m_Label.Width = value; ChkSize(); }
		}
		public string ValueText
		{
			get { return m_TextBox.Text; }
			set { m_TextBox.Text = value; }
		}
		public new T_MyFonts? MyFonts
		{
			get { return base.MyFonts; }
			set
			{
				m_Label.MyFonts = value;
				m_TextBox.MyFonts = value;
				base.MyFonts = value;
			}
		}
		public new float MyFontSize
		{
			get { return base.MyFontSize; }
			set
			{
				base.MyFontSize = value;
				m_Label.MyFontSize = value;
				m_TextBox.MyFontSize = value;
			}
		}

		public T_CaptionTextBox()
		{
			m_Label.Bounds = new Rectangle(0, 0, 100, 25);
			m_Label.RightBar = new Size(0, 0);
			m_Label.TopBar = new Size(0, 0);
			m_Label.BottomBar = new Size(0, 0);
			m_TextBox.Bounds = new Rectangle(100, 0, 100, 25);
			this.Size = new Size(200, 25);
			this.Controls.Add(m_Label);
			this.Controls.Add(m_TextBox);
			InitializeComponent();

			m_Label.GotFocus += M_Label_GotFocus;
		}

		private void M_Label_GotFocus(object? sender, EventArgs e)
		{
			m_TextBox.Focus();
		}

		private void ChkSize()
		{
			if (m_TextBox.Height!=this.Height)
			{
				this.Height = m_TextBox.Height;
			}
			m_Label.Size = new Size(m_Label.Width, m_TextBox.Height);
			m_TextBox.Bounds = new Rectangle(m_Label.Width, 0, this.Width - m_Label.Width, m_TextBox.Height);

		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			ChkSize();

		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
		}
		protected override void OnGotFocus(EventArgs e)
		{
			m_TextBox.Focus();
			//base.OnGotFocus(e);
		}

	}
}