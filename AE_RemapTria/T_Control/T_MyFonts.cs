using AE_RemapTria.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AE_RemapTria
{
	/// <summary>
	/// リソースにあるフォントを管理するクラス
	/// </summary>
	public partial class T_MyFonts :Component
	{
		[System.Runtime.InteropServices.DllImport("gdi32.dll")]
		public static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);
		public PrivateFontCollection? MyFonts = null;// new PrivateFontCollection();
		/// <summary>
		/// フォントの数
		/// </summary>
		[Category("_AE_Remap")]
		public int FontCount
		{
			get
			{
				int ret = 0;
				if (MyFonts == null) MyFontsInit(true);
				if (MyFonts == null) return ret;
				ret = MyFonts.Families.Length;
				return ret;
			}
		}
		// *****************************************************
		public T_MyFonts()
		{
			InitializeComponent(); 
			MyFontsInit(true);
		}
		public T_MyFonts(IContainer container)
		{
			container.Add(this);

			InitializeComponent();
		}       
		// *****************************************************
		/// <summary>
		///　リソースフォントを読み込む
		/// </summary>
		/// <param name="isInit">trueで強制的に読み込む</param>
		private void MyFontsInit(byte[] fontData, bool isInit = false)
		{
			//SourceHanCodeJP
			//MyricaM
			//Myrica
			if ((MyFonts == null) || (isInit == true))
			{
				//byte[] fontData = Properties.Resources.SourceHanCodeJP;
				IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
				System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
				uint dummy = 0;
				MyFonts = new PrivateFontCollection();
				MyFonts.AddMemoryFont(fontPtr, fontData.Length);
				AddFontMemResourceEx(fontPtr, (uint)fontData.Length, IntPtr.Zero, ref dummy);
				System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);
			}
		}
		private void MyFontsInit( bool isInit = false)
		{
			//SourceHanCodeJP
			//MyricaM
			//Myrica
			MyFontsInit(Properties.Resources.SourceHanCodeJP, isInit);
		}
		public Font MyFont(int idx, float sz, FontStyle fs)
		{
			Font ret = new Font("System",sz,fs);
			if (MyFonts == null) MyFontsInit(true);
			if (MyFonts == null) return ret;
			if (idx < 0) idx = 0;
			else if (idx >= MyFonts.Families.Length) idx = MyFonts.Families.Length - 1;
			try
			{
				ret = new Font(MyFonts.Families[idx], sz, fs);
			}
			catch
			{
				ret = new Font("System", sz, fs);
			}
			return ret;
		}
	}
}
