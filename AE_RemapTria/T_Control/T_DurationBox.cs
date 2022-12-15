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
		public void Add(int v)
		{
			if((v>=0) && (v<=9))
			{
				m_ValueStr += $"{v}";
				this.Invalidate();
			}
		}
		public void AddSec()
		{
			if(m_ValueStr=="")
			{
				m_ValueStr = "0+";
			}else if (m_ValueStr.IndexOf("+")>=0)
			{
				return;
			}
			else
			{
				m_ValueStr += "+";
			}
			this.Invalidate();
		}
		public void BS()
		{
			if(m_ValueStr!="")
			{
				m_ValueStr = m_ValueStr.Substring(0, m_ValueStr.Length - 1);
				this.Invalidate();
			}
		}
		public int FrameValue
		{
			get
			{
				if (m_ValueStr.Trim() == "")
				{
					return -1;
				}
				int frm = 0;
				string[] sa = m_ValueStr.Split('+');
				if (sa.Length == 1)
				{
					int v = 0;
					if (int.TryParse(sa[0], out v) == false) return -1;
					frm = v;
				}
				else
				{
					int v0 = 0;
					int v1 = 0;
					if (int.TryParse(sa[0], out v0) == false) return -1;
					if (int.TryParse(sa[1], out v1) == false) return -1;
					frm = (int)m_Fps * v0 + v1;
				}
				return frm;
			}
			set
			{
				if (value < 0) value = 0;
				int sec = value / (int)m_Fps;
				int koma = value % (int)m_Fps;
				m_ValueStr = $"{sec}+{koma}";
			}
		}
		public double Duration
		{
			get{return (double)FrameValue /(double)m_Fps;}
			set
			{
				FrameValue = (int)(value * (double)m_Fps + 0.5);
			}
		}


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
			Pen p = new Pen(Color.FromArgb(50, 50, 75));
			try
			{
				Rectangle r = new Rectangle(0,0,this.Width,this.Height);
				Fill(g,sb);

				sb.Color = this.ForeColor;
				if(m_ValueStr!="")DrawStr(g, m_ValueStr, sb, r);
				if (this.Focused)
				{
					DrawFrame(g, p, 1);
				}
			}
			finally
			{
				sb.Dispose();
				p.Dispose();
			}
		}
		// *************************************************************
		private bool m_IsEdit = false;
		public void SetEdit()
		{
			if (m_IsEdit) return;
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
			m_IsEdit = true;
			m_TextBox.Focus();
		}

		private void M_TextBox_KeyPress(object? sender, KeyPressEventArgs e)
		{
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
			}else if (e.KeyChar==(char)Keys.Enter)
			{
				if (ChkEdit())
				{
					EndEdit();
				}
				e.Handled = true;
			}
			else if (e.KeyChar==(char) Keys.Escape)
			{
				ChkEdit();
				EndEdit();
				e.Handled = true;
			}
			else
			{
				e.Handled = true;
			}
		}



		private void M_TextBox_KeyDown(object? sender, KeyEventArgs e)
		{
			Keys k = e.KeyData;
			if (k == Keys.Delete)
			{
				if(m_TextBox!=null)
				m_TextBox.Text="";
			}
		}

		public void EndEdit()
		{
			if (m_IsEdit == false) return;
			m_IsEdit = false;
			this.Controls.Remove(m_TextBox);
		}
		public bool ChkEdit()
		{
			bool ret = false;
			if (m_IsEdit == false) return false;
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
			if (m_IsEdit)
			{
				ChkEdit();
				EndEdit();
			}
			this.Invalidate();
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
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if ((this.Focused) && (m_IsEdit == false))
			{
				SetEdit();
			}
		}
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			this.Invalidate();
		}
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			this.Invalidate();
		}

	}
}
