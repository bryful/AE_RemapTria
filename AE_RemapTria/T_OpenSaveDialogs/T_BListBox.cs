using PdfSharpCore.Drawing;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

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
		private TR_MyFonts? m_MyFonts = null;
		private Font? m_font = null;
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
		private float m_MyFontSize = 9;
		[Category("_AE_Remap")]
		public float MyFontSize
		{
			get { return m_MyFontSize; }
			set
			{
				m_MyFontSize = value;
				if (m_MyFonts != null)
				{
					this.Font = m_MyFonts.MyFont(m_MyFontIndex, m_MyFontSize, this.Font.Style);
					btnAdd.MyFontSize = m_MyFontSize;
					btnDell.MyFontSize = m_MyFontSize;
					btnUp.MyFontSize = m_MyFontSize;
					btnDown.MyFontSize = m_MyFontSize;
					BList.MyFontSize = m_MyFontSize;
				}
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

			TR_NameDialog dlg = new TR_NameDialog();
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
			TR_NameDialog dlg = new TR_NameDialog();
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
		protected TR_Form? m_form = null;
		protected TR_BaseDialog? m_dialog = null;
		protected TR_Colors? Colors = null;
		protected TR_Size? Sizes = null;
		protected TR_Grid? Grid = null;
		protected TR_CellData? CellData = null;
		public void SetTRDialog(TR_BaseDialog? bd)
		{
			m_dialog = bd;
			if (m_dialog != null)
			{
				m_form = m_dialog.Form;
				m_MyFonts = m_dialog.MyFonts;
				if (m_form != null)
				{
					Grid = m_form.Grid;
					Colors = m_form.Colors;
					Sizes = m_form.Sizes;
					CellData = m_form.CellData;
					m_MyFonts = m_form.MyFonts;

				}
				if (m_MyFonts != null)
				{
					m_font = 
					this.Font = m_MyFonts.MyFont(m_MyFontIndex, m_MyFontSize, this.Font.Style);
				}
				btnEdit.SetTRDialog(bd);
				btnDell.SetTRDialog(bd);
				btnUp.SetTRDialog(bd);
				btnDown.SetTRDialog(bd);
			}
		}
	}
}
