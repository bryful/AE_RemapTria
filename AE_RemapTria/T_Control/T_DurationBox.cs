using BRY;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE_RemapTria
{

	public class T_DurationBox : TR_DialogControl
	{
		private StringFormat m_sf = new StringFormat();

		private string m_ValueStr = "12+3";

		private TextBox? m_TextBox = null;
		private T_Fps m_Fps = T_Fps.FPS24;
		[Category("_AE_Remap")]
		public T_Fps Fps
		{
			get { return m_Fps; }
			set
			{
				if(m_Fps!=value)
				{
					m_Fps = value;
					
					this.Invalidate();
				}
			}
		}
		[Category("_AE_Remap")]
		public string Info
		{
			get
			{
				
				return m_ValueStr;
			}
		}
		public T_DurationBox()
		{
			this.Size = new Size(150, 30);
			m_MyFontSize = 12;
			m_MyFontIndex = 5;
			Alignment = StringAlignment.Far;
			LineAlignment = StringAlignment.Center;
			m_ValueStr = "12+12";
		}
		public void SetFrame(T_Fps fps,int f)
		{
		}
		// *************************************************************
	
		// *************************************************************
		protected override void OnPaint(PaintEventArgs pe)
		{
			//base.OnPaint(pe);
			Graphics g = pe.Graphics;
			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
			SolidBrush sb = new SolidBrush(this.BackColor);
			try
			{
				Rectangle r = new Rectangle(0,0,this.Width,this.Height);
				Fill(g,sb);

				sb.Color = this.ForeColor;
				if(m_ValueStr!="")DrawStr(g, m_ValueStr, sb, r);

			}
			finally
			{
				sb.Dispose();
			}
		}
		// *************************************************************
		public void SetEdit()
		{
			if (m_TextBox != null) return;
			m_TextBox = new TextBox();
			m_TextBox.Name = "a";
			m_TextBox.AutoSize = false;
			m_TextBox.Location = new Point(0, 0);
			m_TextBox.BorderStyle = BorderStyle.None;
			m_TextBox.Size = new Size(Width, Height);
			m_TextBox.Text = m_ValueStr;
			m_TextBox.TextAlign = HorizontalAlignment.Right;
			m_TextBox.SelectionStart = m_TextBox.Text.Length;
			m_TextBox.LostFocus += M_TextBox_LostFocus;
			m_TextBox.KeyPress += M_TextBox_KeyPress;
			m_TextBox.KeyDown += M_TextBox_KeyDown;

			m_TextBox.Font = this.Font;
			m_TextBox.ForeColor = Color.Black;
			m_TextBox.BackColor = Color.FromArgb(240,240,240);
			m_TextBox.ImeMode = ImeMode.Disable;
			this.Controls.Add(m_TextBox);
			F_W.SetFocus(m_TextBox.Handle);
		}

		private void M_TextBox_KeyDown1(object? sender, KeyEventArgs e)
		{
			Debug.WriteLine("DOwn" + e.KeyData.ToString());
		}

		private void M_TextBox_KeyPress(object? sender, KeyPressEventArgs e)
		{
			Debug.WriteLine("Press"+((int)e.KeyChar).ToString());
			TextBox? tb = (TextBox?)sender;
			if(tb==null) return;
			if (e.KeyChar == (char)Keys.Back)
			{
				return;
			}
			else if ((e.KeyChar >= '0' && '9' >= e.KeyChar))
			{
				return;
			} else if ((e.KeyChar == '+'))
			{
				if (tb.Text.IndexOf("+") >= 0)
				{
					e.Handled = true;
				}
				else
				{
					return;
				}
			}
			else
			{
				e.Handled = true;
			}
		}



		private void M_TextBox_KeyDown(object? sender, KeyEventArgs e)
		{
			Debug.WriteLine("Down" + e.KeyData.ToString());
			Keys k = e.KeyData;
			if (k == Keys.Escape)
			{
				ChkEdit();
				EndEdit();
			}
			else if ((k == Keys.Return))
			{
				if (ChkEdit())
				{
					EndEdit();
				}
			}
			else if (k == Keys.Back)
			{
			}
			else if (k == Keys.Delete)
			{
				if(m_TextBox!=null)
				m_TextBox.Text="";
			}
		}

		public void EndEdit()
		{
			if (m_TextBox == null) return;
			this.Controls.Remove(m_TextBox);
			m_TextBox = null;
		}
		public bool ChkEdit()
		{
			bool ret = false;
			if (m_TextBox == null) return ret;
			string s = m_TextBox.Text.Trim();
			string [] sa = s.Split("+");
			s = "";
			int v = 0;
			if (sa.Length > 0)
			{
				if (int.TryParse(sa[0],out v))
				{
					s += v.ToString();
				}
				else
				{
					return ret;
				}
			}
			if (sa.Length > 1)
			{
				if (int.TryParse(sa[1], out v))
				{
					s += "+";
					s += v.ToString();
				}
			}

			if (m_ValueStr != s)
			{
				m_ValueStr = s;
			}
			ret = true;
			return ret;
		}

		private void M_TextBox_LostFocus(object? sender, EventArgs e)
		{
			ChkEdit();
			EndEdit();
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (m_TextBox == null)
			{
				SetEdit();
				return;
			}
			base.OnMouseDown(e);
		}
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			SetEdit();
		}
	}
}
