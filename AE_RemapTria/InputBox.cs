using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AE_RemapTria
{
	public partial class InputBox : Control
	{
		public class ValueChangedEventArgs : EventArgs
		{
			public int Value;
			public ValueChangedEventArgs(int v)
			{
				Value = v;
			}
		}
		public delegate void ValueChangedHandler(object sender, ValueChangedEventArgs e);
		public event ValueChangedHandler? ValueChanged;
		protected virtual void OnValueChanged(ValueChangedEventArgs e)
		{
			if (ValueChanged != null)
			{
				ValueChanged(this, e);
			}
		}
		private string m_Value = "";
		// **************************************************************
		public TR_Form? m_form = null;
		public TR_Colors? Colors = null;
		public TR_Size? Sizes = null;
		// **************************************************************
		public InputBox()
		{
			InitializeComponent();
			this.SetStyle(
	ControlStyles.Selectable |
	ControlStyles.UserMouse |
	ControlStyles.DoubleBuffer |
	ControlStyles.UserPaint |
	ControlStyles.AllPaintingInWmPaint |
	ControlStyles.SupportsTransparentBackColor,
	true);
		}
		// **************************************************************
		public void SetTRForm(TR_Form mf)
		{
			m_form = mf;
			if (mf != null)
			{
				Colors = mf.Colors;
				Sizes = mf.Sizes;
				this.Size = new Size(Sizes.CellWidth - 2, Sizes.CellHeight - 2);

				this.Font = m_form.MyFont(m_form.Grid.FontIndex,
					m_form.Grid.FontSize,FontStyle.Regular);

				this.Invalidate();
			}
		}
		// **************************************************************
		protected override void OnPaint(PaintEventArgs pe)
		{
			if((m_form == null)|| (Colors == null)||(Sizes==null)) return;
			SolidBrush sb = new SolidBrush(Colors.Moji);
			Graphics g = pe.Graphics;
			try
			{
				StringFormat sf = new StringFormat();
				sf.Alignment = StringAlignment.Center;
				sf.LineAlignment= StringAlignment.Center;
				g.DrawString(m_Value, this.Font, sb, this.ClientRectangle, sf);
			}
			finally
			{
				sb.Dispose();
			}
		}
		// **************************************************************
		protected override void OnKeyDown(KeyEventArgs e)
		{
			Keys v = e.KeyData;

			if ((v >= Keys.NumPad0) && (v <= Keys.NumPad9))
			{
				if (m_Value.Length < 3)
				{
					m_Value += ((int)v - (int)Keys.NumPad0).ToString();
				}
			}
			else if ((v >= Keys.D0) && (v <= Keys.D9))
			{
				if (m_Value.Length < 3)
				{
					m_Value += ((int)v - (int)Keys.NumPad0).ToString();
				}
			}
			else if (v == Keys.Enter)
			{
				int vm = -1;
				if (int.TryParse(m_Value, out vm))
				{
					OnValueChanged(new ValueChangedEventArgs(vm));
					this.Visible = false;
				}
			}
			else
			{
				OnValueChanged(new ValueChangedEventArgs(-1));
				this.Visible = false;
			}
			Debug.WriteLine(m_Value);
			this.Invalidate();
		}

	
	/*
		protected override bool ProcessDialogKey(Keys keyData)
		{
			Keys v = keyData;

			if ((v >= Keys.NumPad0) && (v <= Keys.NumPad9))
			{
				if (m_Value.Length < 3)
				{
					m_Value += ((int)v - (int)Keys.NumPad0).ToString();
				}
			}
			else if ((v >= Keys.D0) && (v <= Keys.D9))
			{
				if (m_Value.Length < 3)
				{
					m_Value += ((int)v - (int)Keys.NumPad0).ToString();
				}
			}
			else if (v == Keys.Enter)
			{
				int vm = -1;
				if (int.TryParse(m_Value, out vm))
				{
					OnValueChanged(new ValueChangedEventArgs(vm));
					this.Visible = false;
				}
			}
			else
			{
				OnValueChanged(new ValueChangedEventArgs(-1));
				this.Visible = false;
			}
			Debug.WriteLine(m_Value);
			this.Invalidate();
			return base.ProcessDialogKey(keyData);
		}
	*/
	}
}
