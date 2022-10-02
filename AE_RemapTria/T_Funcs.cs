﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace AE_RemapTria
{
	public delegate bool FuncType();
	public class FuncItem
	{
		public string? Caption { get; set; }
		public string? Info { get; set; }
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
		public string ToJson()
		{
			JsonObject jo = new JsonObject();
			JsonArray ja = new JsonArray();

			foreach(FuncItem item in m_FuncItems)
			{
				JsonObject jo2 = new JsonObject();
				jo2.Add("funcName", item.Caption);
				JsonArray ja2 = new JsonArray();
				if(item.Keys.Length>0)
				{
					foreach(Keys k in item.Keys)
					{
						ja2.Add(k);
					}
				}
				jo2.Add("keys", ja2);
				ja.Add(jo2);
			}
			jo.Add("KeyBind", ja);
			//JsonSerializerOptions options = new() { WriteIndented = true };
			return jo.ToJsonString();

		}
	}
}
