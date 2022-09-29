using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BRY;
namespace AE_RemapTria
{
	public partial class T_DialogBase : Form
	{
		private T_Grid? m_grid = null;
		private bool m_IsClose = false;
		private Font m_font = new Font("Arial", 14);
		private int m_MFontIndex = 5;
		public int MFontIndex { get { return m_MFontIndex; } set { SetMFontIndex(value); } }
		private float m_MFontSize = 14;
		public float MFontSize { get { return m_MFontSize; } set { SetMFontSize(value); } }
		public Font MFont { get { return m_font; } }
		private StringFormat m_format = new StringFormat();
		private StringAlignment m_Alignment_Bak;
		private StringAlignment m_LineAlignment_Bak;
		private float m_FontSize_Bak;
		private FontStyle m_FontStyle_Bak;
		[System.Runtime.InteropServices.DllImport("gdi32.dll")]
		public static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);
		public PrivateFontCollection? MyFonts = null;// new PrivateFontCollection();

		public T_DialogBase()
		{
			InitializeComponent();
			Init();
		}
		// ************************************************************************
		public void Init()
		{

			this.SetStyle(
				ControlStyles.DoubleBuffer |
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.SupportsTransparentBackColor,
				true);
			this.BackColor = Color.Transparent;
			this.UpdateStyles();
			m_Alignment_Bak =
			Alignment = StringAlignment.Far;
			m_LineAlignment_Bak =
			LineAlignment = StringAlignment.Center;
			m_FontSize_Bak = this.Font.Size;
			m_FontStyle_Bak = this.Font.Style;
			this.FormBorderStyle = FormBorderStyle.None;
			this.TopMost = true;


		}
		// *****************************************************************
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			ChkGrid();
		}
		// *****************************************************************
		public DialogResult ShowDialogM(Form fm, T_Grid g, Point p,bool isClose = false)
		{
			m_grid = g;
			ChkGrid();
			this.Location = p;
			m_IsClose = isClose;
			return ShowDialog(fm);
		}
		// *****************************************************************
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			if(m_IsClose==true)
			{
				this.DialogResult = DialogResult.Cancel;
				Close();
			}
		}
		protected override void OnMouseClick(MouseEventArgs e)
		{
			base.OnMouseClick(e);
			this.Close();
		}
		// *****************************************************************
		public T_Grid Grid
		{
			get { return m_grid; }
			set
			{
				m_grid = value;
				ChkGrid();
			}
		}
		// *****************************************************************
		private void ChkGrid()
		{
			if (m_grid != null)
			{

			}
		}
		// *****************************************************************
		public int MyFontsCount
		{
			get
			{
				int ret = 0;
				if (MyFonts != null)
				{
					ret = MyFonts.Families.Length;
				}
				return ret;
			}
		}
		// ************************************************************************
		public void SetMFontIndex(int idx)
		{
			try
			{
				Font? f = MFontMake(idx, m_MFontSize);
				if (f != null)
				{
					m_font = f;
					this.Invalidate();
				}
			}
			catch
			{

			}
		}
		// ************************************************************************
		public void SetMFontSize(float sz)
		{
			try
			{
				Font? f = MFontMake(m_MFontIndex, sz);
				if (f != null)
				{
					m_font = f;
					this.Invalidate();
				}
			}
			catch
			{

			}
		}
		// ************************************************************************
		public void MFontInit()
		{
			//SourceHanCodeJP
			//MyricaM
			//Myrica
			byte[] fontData = Properties.Resources.SourceHanCodeJP;
			IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
			System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
			uint dummy = 0;
			MyFonts = new PrivateFontCollection();
			MyFonts.AddMemoryFont(fontPtr, Properties.Resources.SourceHanCodeJP.Length);
			AddFontMemResourceEx(fontPtr, (uint)Properties.Resources.SourceHanCodeJP.Length, IntPtr.Zero, ref dummy);
			System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);
			SetMyFont(m_MFontIndex, m_MFontSize);
		}
		// ************************************************************************
		public void SetMyFont(int idx, float sz)
		{
			m_font = MFontMake(idx, sz);
			this.Font = m_font;
			this.Invalidate();
		}
		// ************************************************************************
		public Font MFontMake(int idx, float sz)
		{
			Font ret = new Font("Arial", m_MFontSize);
			if (MyFonts != null)
			{
				if ((idx >= 0) && (idx < MyFonts.Families.Length))
				{
					try
					{
						ret = new Font(MyFonts.Families[idx], sz);
						m_MFontIndex = idx;
						m_MFontSize = sz;
					}
					catch
					{
						m_MFontIndex = 0;
						ret = new Font(MyFonts.Families[0], m_MFontSize);
					}
				}
			}
			return ret;
		}
		// ************************************************************************
		public StringAlignment Alignment
		{
			get { return m_format.Alignment; }
			set { m_format.Alignment = value; }
		}
		// ************************************************************************
		public StringAlignment LineAlignment
		{
			get { return m_format.LineAlignment; }
			set { m_format.LineAlignment = value; }
		}
	}
}
