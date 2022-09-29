using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE_RemapTria
{
	public delegate bool FuncType();
	public class FuncItem
	{
		public string Caption { get; set; }
		public FuncType? Func { get; set; }
		private Keys[] m_Keys = new Keys[2];

		public Keys[] Keys
		{
			get { return m_Keys; }
			set { m_Keys = value; }
		}
		public FuncItem(FuncType fnc,string caption, Keys[] keys)
		{
			Func = fnc;
			Func = fnc;
			Caption = caption;
			m_Keys = keys;
		}
		public FuncItem(FuncType fnc, string caption, Keys key)
		{
			Func = fnc;
			Caption = caption;
			m_Keys = new Keys[] { key };
		}
		public FuncItem(FuncType fnc, string caption, Keys key0, Keys key1)
		{
			Func = fnc;
			Caption = caption;
			m_Keys = new Keys[] { key0,key1 };
		}
		public bool IsKey(Keys k)
		{
			bool ret = false;
			if(m_Keys.Length>0)
			{
				foreach(Keys k2 in m_Keys)
				{
					if(k2==k)
					{
						ret = true;
						break;
					}
				}
			}
			return ret;
		}
	}

	public class T_Funcs
	{
		private  FuncItem[] m_FuncItems = new FuncItem[0];
		public FuncItem? FindKeys(Keys k)
		{
			FuncItem? ret =null;
			if(m_FuncItems.Length>0)
			{
				foreach(FuncItem item in m_FuncItems)
				{
					if (item.IsKey(k) == true)
					{
						ret = item;
						break;
					}
				}
			}
			return ret;
		}
		public T_Funcs()
		{

		}
		private void Init()
		{
			m_FuncItems = new FuncItem[0]; ;
		}
		public void SetFuncs(FuncItem[] fs)
		{
			m_FuncItems = fs;
		}
	}
}
