using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AE_RemapTria
{
	public partial class T_BListBox : UserControl
	{
		// ******************************************************
		[Category("_AE_Remap")]
		public T_FList? Flist
		{
			get { return BList.Flist; }
			set 
			{ 
				BList.Flist = value;
			}
		}

		// ******************************************************
		#region Font
		private T_MyFonts? m_MyFonts = null;
		/// <summary>
		/// リソースフォント管理のコンポーネント
		/// </summary>
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
					btnAdd.MyFonts = m_MyFonts;
					btnDell.MyFonts = m_MyFonts;
					btnUp.MyFonts = m_MyFonts;
					btnDown.MyFonts = m_MyFonts;
					BList.MyFonts = m_MyFonts;

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
					btnAdd.MyFontIndex = m_MyFontIndex;
					btnDell.MyFontIndex = m_MyFontIndex;
					btnUp.MyFontIndex = m_MyFontIndex;
					btnDown.MyFontIndex = m_MyFontIndex;
					BList.MyFontIndex = m_MyFontIndex;
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
				btnAdd.MyFontSize = value;
				btnDell.MyFontSize = value;
				btnUp.MyFontSize = value;
				btnDown.MyFontSize = value;
				BList.MyFontSize = value;
			}
		}
		[Category("_AE_Remap")]
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
			btnEdit.Click += BtnEdit_Click;
			btnDell.Click += BtnDell_Click;
		}

		private void BtnDell_Click(object? sender, EventArgs e)
		{
			ItemDelete();
		}

		private void BtnEdit_Click(object? sender, EventArgs e)
		{
			EditCaption();
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
				btnEdit.Enabled = false;
				btnDell.Enabled = false;
				btnUp.Enabled = false;
				btnDown.Enabled = false;
			}
			else
			{
				btnEdit.Enabled = BList.SelectedIndex >= 0;
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
			if(Flist==null) return;

			T_NameDialog dlg = new T_NameDialog();
			dlg.CanSameName = true;
			dlg.Caption = "Add Directory";
			dlg.ToCenter();
			dlg.ValueText = Path.GetFileName(Flist.FullName);
			if(dlg.ShowDialog() == DialogResult.OK)
			{
				if (dlg.ValueText != "")
				{
					AddDir(Flist.FullName, dlg.ValueText);
					BList.Invalidate();
				}
			}
		}
		// ***********************************************************************
		public void EditCaption()
		{
			if (BList.SelectedIndex < 0) return;
			T_NameDialog dlg = new T_NameDialog();
			dlg.CanSameName = false;
			dlg.Caption = "Edit Caption";
			dlg.ToCenter();
			dlg.ValueText = BList.SelectedCaption;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				if (dlg.ValueText != "")
				{
					BList.SelectedCaption = dlg.ValueText;
					BList.Invalidate();
				}
			}

		}
		public void ItemDelete()
		{
			BList.ItemDelete();

		}
		public bool Import()
		{
			return BList.Load();
		}
		public bool Export()
		{
			return BList.Save();
		}
		public JsonObject ToJsonObject()
		{
			return BList.ToJsonObject();
		}
		public void FromJsonObject(JsonObject jo)
		{
			BList.FromJsonObject(jo);
		}
		// ***********************************************************************

	}
}
