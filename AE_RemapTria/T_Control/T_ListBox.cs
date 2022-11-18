using System;
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
	public partial class T_ListBox : ListBox
	{
		private Color m_SelectedColor = Color.FromArgb(50, 50, 100);
		[Category("_AE_Remap")]
		public Color SelectedColor
		{
			get { return m_SelectedColor; }
			set { m_SelectedColor = value; this.Invalidate(); }
		}
		private T_Funcs t_Funcs = new T_Funcs();
		private T_MyFonts? m_MyFonts = null;
		[Category("_AE_Remap")]
		public T_MyFonts? MyFonts
		{
			get { return m_MyFonts; }
			set
			{
				m_MyFonts = value;
				if (m_MyFonts != null)
				{
					this.Font = m_MyFonts.MyFont(m_MyFontIndex, this.Font.Size, this.Font.Style);
					this.Invalidate();
				}
			}
		}
		private int m_MyFontIndex = 5;
		[Category("_AE_Remap")]
		public int MyFontIndex
		{
			get { return m_MyFontIndex; }
			set
			{
				m_MyFontIndex = value;
				if (m_MyFontIndex < 0) m_MyFontIndex = 0;
				if (m_MyFonts != null)
				{
					this.Font = m_MyFonts.MyFont(m_MyFontIndex, this.Font.Size, this.Font.Style);
					this.Invalidate();
				}
			}
		}
		[Category("_AE_Remap")]
		public float MyFontSize
		{
			get { return this.Font.Size; }
			set
			{
				SetFontSizeStyle(value, this.Font.Style);
			}
		}
		[Category("_AE_Remap")]
		public FontStyle MyFontStyle
		{
			get { return this.Font.Style; }
			set
			{
				SetFontSizeStyle(this.Font.Size, value);
			}
		}
		public void SetFontSizeStyle(float sz, FontStyle fs)
		{
			if (m_MyFonts != null)
			{
				this.Font = m_MyFonts.MyFont(m_MyFontIndex, sz, fs);
			}
			else
			{
				this.Font = new Font(this.Font.FontFamily, sz, fs);
			}
			this.Invalidate();
		}
		public T_ListBox()
		{
			//this.BorderStyle = BorderStyle.FixedSingle;
			InitializeComponent();
			Init();
			this.DrawMode = DrawMode.OwnerDrawFixed;
			this.DrawItem += new DrawItemEventHandler(DrawItems);
			this.ForeColor = Color.FromArgb(255, 120, 220, 250);
			this.BackColor = Color.Transparent;

		}
		public void Init()
		{

			
			this.SetStyle(
				ControlStyles.DoubleBuffer |
				//ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.SupportsTransparentBackColor,
				true);
			this.UpdateStyles();
			

		}
		private void DrawItems(object sender, System.Windows.Forms.DrawItemEventArgs e)
		{
			SolidBrush sb = new SolidBrush(this.BackColor);
			T_ListBox lb = (T_ListBox)sender;

			try
			{
				e.DrawBackground();
				//sb.Color = Color.Transparent;
				//e.Graphics.FillRectangle(sb, e.Bounds);
				
				if ((e.Index > -1)&&(lb.Items.Count>0))
				{

					if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
					{
						sb.Color = m_SelectedColor;
						e.Graphics.FillRectangle(sb, e.Bounds);
					}

					//文字列の取得
					string txt = lb.Items[e.Index].ToString();
					//文字列の描画
					sb.Color = this.ForeColor;
					e.Graphics.DrawString(txt, e.Font, sb, e.Bounds);

				}

			}
			finally
			{
				sb.Dispose();
			}
			//背景を描画する
		}
		/*
		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
		}
		*/
		[Category("_AE_Remap")]
		public string[] Names
		{
			get
			{
				string[] ret = new string[this.Items.Count];
				if (this.Items.Count>0)
				{
					for(int i = 0; i < this.Items.Count; i++)
					{
						string s = Items[i].ToString();
						if (s != null) s = "";
						ret[i] = s;
					}
				}
				return ret;
			}
			set
			{
				this.Items.Clear();
				this.Items.AddRange(value);
			}
		}
	}
}
