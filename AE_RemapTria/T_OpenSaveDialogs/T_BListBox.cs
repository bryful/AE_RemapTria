using System;
using System.Collections;
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
	public partial class T_BListBox : UserControl
	{
		// ******************************************************
		private T_FList? m_Flist = null;
		public T_FList? Flist
		{
			get { return m_Flist; }
			set 
			{ 
				m_Flist = value; 
			}
		}

		// ******************************************************
		#region Font
		private T_MyFonts? m_MyFonts = null;
		/// <summary>
		/// リソースフォント管理のコンポーネント
		/// </summary>
		public T_MyFonts? MyFonts
		{
			get { return m_MyFonts; }
			set
			{
				m_MyFonts = value;
				if (m_MyFonts != null)
				{
					this.Font = m_MyFonts.MyFont(m_MyFontIndex, this.Font.Size, this.Font.Style);
					btnAdd.MyFonts = m_MyFonts;
					btnDell.MyFonts = m_MyFonts;
					btnUp.MyFonts = m_MyFonts;
					btnDown.MyFonts = m_MyFonts;
					BList.MyFonts = m_MyFonts;

				}
			}
		}
		private int m_MyFontIndex = 5;
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
					btnAdd.MyFontIndex = m_MyFontIndex;
					btnDell.MyFontIndex = m_MyFontIndex;
					btnUp.MyFontIndex = m_MyFontIndex;
					btnDown.MyFontIndex = m_MyFontIndex;
					BList.MyFontIndex = m_MyFontIndex;
				}
			}
		}
		public float MyFontSize
		{
			get { return this.Font.Size; }
			set
			{
				SetFontSizeStyle(value, this.Font.Style);
				btnAdd.MyFontSize = value;
				btnDell.MyFontSize = value;
				btnUp.MyFontSize = value;
				btnDown.MyFontSize = value;
				BList.MyFontSize = value;
			}
		}
		public FontStyle MyFontStyle
		{
			get { return this.Font.Style; }
			set
			{
				SetFontSizeStyle(this.Font.Size, value);
				btnAdd.MyFontStyle = value;
				btnDell.MyFontStyle = value;
				btnUp.MyFontStyle = value;
				btnDown.MyFontStyle = value;
				BList.MyFontStyle = value;
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
		}
		#endregion
		// ***********************************************************************
		public T_BListBox()
		{
			InitializeComponent();
			if (m_MyFonts != null)
			{
				this.Font = m_MyFonts.MyFont(m_MyFontIndex, this.Font.Size, this.Font.Style);
			}
			ChkEnabled();
			BList.SelectedIndexChanged += BList_SelectedIndexChanged;
			btnAdd.Click += BtnAdd_Click;
			btnUp.Click += BtnUp_Click;
			btnDown.Click += BtnDown_Click;
		}

		private void BtnDown_Click(object? sender, EventArgs e)
		{
			BList.ItemDown();
			ChkEnabled();
		}

		private void BtnUp_Click(object? sender, EventArgs e)
		{
			BList.ItemUp();
			ChkEnabled();
		}

		private void ChkEnabled()
		{
			if(BList.SelectedIndex<0)
			{
				btnDell.Enabled = false;
				btnUp.Enabled = false;
				btnDown.Enabled = false;
			}
			else
			{
				btnDell.Enabled = BList.SelectedIndex >= 0;
				btnUp.Enabled = BList.SelectedIndex >= 1;
				btnDown.Enabled = BList.SelectedIndex < BList.Count-1;
			}

		}
		private void BList_SelectedIndexChanged(object sender, EventArgs e)
		{
			ChkEnabled();
		}

		// ***********************************************************************
		private void BtnAdd_Click(object? sender, EventArgs e)
		{
			AddDir();
		}
		// ***********************************************************************
		public bool AddDir(string p,string cap)
		{
			return BList.AddDir(p, cap);
		}
		// ***********************************************************************
		public void AddDir()
		{
			if(m_Flist==null) return;
			AddDir(m_Flist.FullName, Path.GetFileName(m_Flist.FullName));
		}
		// ***********************************************************************

	}
}
