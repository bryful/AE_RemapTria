using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRY
{


	public class F_ArgItem
	{
		private string m_Name = "";
		public string Name { get { return m_Name; } }
		private int m_Index = -1;
		public int Index { get { return m_Index; } }
		public void SetIndex(int idx) { m_Index = idx; }
		private bool m_IsOption = false;
		public bool IsOption { get { return m_IsOption; } }
		private string m_Option = "";
		public string Option { get { return m_Option; } }
		public F_ArgItem(string n, int idx)
		{
			m_Name = n;
			m_Index = idx;
			m_IsOption = ((m_Name[0] == '/') || (m_Name[0] == '-'));
			if(m_IsOption)
			{
				m_Option = m_Name.Substring(1);
			}
			else
			{
				m_Option = "";
			}
		}
		public bool EqualOption(string op)
		{
			return string.Compare(m_Option,op,true)==0;
		}
	}
	public class F_Args
	{
		private F_ArgItem[] m_args = new F_ArgItem[0];
		public int Count { get { return m_args.Length; } }
		public F_ArgItem this[int idx]
		{
			get 
			{
				return m_args[idx];
			}
		}
		public int OptionCount { get { return m_OptionTable.Length; } }
		private int[] m_OptionTable = new int[0];
		public F_ArgItem? Option(int idx)
		{
			F_ArgItem ret = null;
			if((idx>=0)&&(idx<m_OptionTable.Length))
			{
				int idx2 = m_OptionTable[idx];
				if((idx2>=0)&&(idx2< m_args.Length))
				{
					ret = m_args[idx2];
				}
			}
			return ret;
		}
		// **************************************************************
		public F_Args(string[] args)
		{
			SetArgs(args);
		}
		// **************************************************************
		public void SetArgs(string[] args)
		{
			m_args = new F_ArgItem[args.Length];
			if (args.Length>0)
			{
				int idx = 0;
				List<int> ints = new List<int>();
				foreach(string a in args)
				{
					m_args[idx] = new F_ArgItem(a, idx);
					idx++;
				}
			}
			ChkOption();
		}
		// **************************************************************
		public void RemoveAt(int idx)
		{
			if((idx<0)||(idx>= m_args.Length)) return;
			if(idx< m_args.Length-1)
			{
				for (int i = idx; i < m_args.Length-1; i++)
				{
					m_args[i] = m_args[i+1];
				}
				for (int i = 0; i < m_args.Length-1; i++)
					m_args[i].SetIndex(i);
			}
			Array.Resize(ref m_args, m_args.Length - 1);
			ChkOption();

		}
		// **************************************************************
		public void RemoveOption(string op)
		{
			if (m_args.Length == 0) return;
			int cnt  = m_args.Length - 1;
			for(int i = cnt; i >=0 ; i--)
			{
				if (m_args[i].EqualOption(op))
				{
					RemoveAt(i);
				}
			}
		}
		// **************************************************************
		public void ChkOption()
		{
			m_OptionTable = new int[0];
			if (m_args.Length > 0)
			{
				List<int> ints = new List<int>();
				int idx = 0;
				foreach (F_ArgItem a in m_args)
				{
					if (a.IsOption)
					{
						ints.Add(a.Index);
					}
					idx++;
				}
				m_OptionTable = ints.ToArray();
			}
		}
		// **************************************************************
		public int FindOption(string op)
		{
			int ret = -1;
			int idx = 0;
			foreach(F_ArgItem item in m_args)
			{
				if(item.EqualOption(op))
				{
					ret = item.Index;
					break;
				}
				idx++;
			}
			return ret;
		}
		// ***********************************************
		public bool IsOptions(string[] op)
		{
			bool ret = false;
			foreach(string s in op)
			{
				if(FindOption(s)>=0)
				{
					ret = true;
					break;
				}
			}
			return ret;
		}
		// **************************************************************
		private bool InSpace(string s)
		{
			bool ret = false;
			foreach(char c in s)
			{
				if((c==' ')|| (c == '\t')|| (c == '\r')|| (c == '\n'))
				{
					ret = true;
					break;
				}
			}
			return ret;
		}
		// **************************************************************
		public string ArgsString()
		{
			string ret = "";
			foreach (F_ArgItem item in m_args)
			{
				if (ret != "") ret += " ";
				if (InSpace(item.Name))
				{
					ret += "\"" + item.Name + "\"";
				}
				else
				{
					ret += item.Name;
				}
			}
			return ret;
		}
		public string ArgsString(string noOpt)
		{
			string ret = "";
			foreach (F_ArgItem item in m_args)
			{
				if (noOpt != "")
					if (item.EqualOption(noOpt))
						continue;
				if (ret != "") ret += " ";
				if (InSpace(item.Name))
				{
					ret += "\"" + item.Name + "\"";
				}
				else
				{
					ret += item.Name;
				}
			}
			return ret;
		}
		public string ArgsString(string [] noOpts)
		{
			string ret = "";
			foreach (F_ArgItem item in m_args)
			{
				if (noOpts.Length>0)
				{
					foreach(string s in noOpts)
					{
						if (item.EqualOption(s)) continue;
					}
				}
				if (ret != "") ret += " ";
				if (InSpace(item.Name))
				{
					ret += "\"" + item.Name + "\"";
				}
				else
				{
					ret += item.Name;
				}
			}
			return ret;
		}
		public string[] ToStringArray()
		{
			string [] ret = new string[0];
			if (m_args.Length > 0)
			{
				List<string> list = new List<string>();
				foreach (F_ArgItem item in m_args)
				{
					list.Add(item.Name);
				}
				ret = list.ToArray();
			}
			return ret;
		}
		public string[] ToStringArray(string op)
		{
			string[] ret = new string[0];
			if (m_args.Length > 0)
			{
				List<string> list = new List<string>();
				foreach (F_ArgItem item in m_args)
				{
					if(op!="")
						if (item.EqualOption(op)) 
							continue;
					list.Add(item.Name);
				}
				ret = list.ToArray();
			}
			return ret;
		}
		public string[] ToStringArray(string[] ops)
		{
			string[] ret = new string[0];
			if (m_args.Length > 0)
			{
				List<string> list = new List<string>();
				foreach (F_ArgItem item in m_args)
				{
					if (ops.Length>0)
					{
						foreach(string s in ops)
						{
							if (item.EqualOption(s))
								continue;
						}
					}
					list.Add(item.Name);
				}
				ret = list.ToArray();
			}
			return ret;
		}
	}
}
