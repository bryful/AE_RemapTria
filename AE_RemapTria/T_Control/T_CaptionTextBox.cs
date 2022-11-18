using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AE_RemapTria
{
	public partial class T_CaptionTextBox : T_BaseControl
	{
		private T_Label m_Label = new T_Label();
		private T_TextBox m_TextBox = new T_TextBox();

		[Category("_AE_Remap")]
		public string Caption
		{
			get { return m_Label.Text; }
			set { m_Label.Text = value; }
		}
		[Category("_AE_Remap")]
		public int CaptionWidth
		{
			get { return m_Label.Width; }
			set { m_Label.Width = value; ChkSize(); }
		}
		[Category("_AE_Remap")]
		public string ValueText
		{
			get { return m_TextBox.Text; }
			set { m_TextBox.Text = value; }
		}
		[Category("_AE_Remap")]
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
		[Category("_AE_Remap")]
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
			m_Label.BackColor = Color.Transparent;
			m_Label.GotFocus += M_Label_GotFocus;
			this.SetStyle(
				ControlStyles.DoubleBuffer |
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint|
				ControlStyles.SupportsTransparentBackColor,
				true);
			//this.BackColor = Color.Black;
			this.UpdateStyles();
			this.BackColor = Color.Transparent;
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
			Fill(pe.Graphics, this.BackColor);
		}
		protected override void OnGotFocus(EventArgs e)
		{
			m_TextBox.Focus();
			//base.OnGotFocus(e);
		}

	}
}
