using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
namespace BRY
{
	public class F_AEJson
	{
		static private bool StringBlock(string s,int idx,out int result)
		{
			bool ret = false;
			result = -1;
			if ((idx < 0) || (idx >= s.Length)) return ret;
			if (s[idx] == '"')
			{
				for (int i = idx + 1; i < s.Length; i++)
				{
					if ((s[i] == '"') && (s[i - 1] != '\\'))
					{
						result = i;
						ret = true;
						break;
					}
				}
			}
			return ret;
		}
		static private bool IsNameTag(string s, int idx)
		{
			bool ret = false;

			for(int i= idx; i < s.Length; i++)
			{
				if ((s[i] == ' ') || (s[i] == '\t') || (s[i] == '\r') || (s[i] == 'n'))
					continue;
				if(s[i] == ':')
				{
					ret = true;
				}
				break;
			}
			return ret;
		}
		static public string ToAEJson(string s)
		{
			string ret = "";

			int idx = 0;
			int len = s.Length;
			string block = "";
			while(idx<len)
			{
				//string ss = s.Substring(idx, 1);
				int j = 0;
				if (StringBlock(s, idx, out j) == true)
				{
					ret += block; block = "";
					string nm = s.Substring(idx, j - idx + 1);
					idx = j + 1;
					if (IsNameTag(s, idx) == true)
					{
						ret += nm.Substring(1, nm.Length - 2);
					}
					else
					{
						ret += nm;
					}
					continue;
				}
				if (s[idx]=='{')
				{
					ret += block; block = "";
					ret += "({";
					idx ++;
					continue;
				}
				if (s[idx] == '}')
				{
					ret += block; block = "";
					ret += "})";
					idx++;
					continue;
				}
				if ((s[idx] ==' ')|| (s[idx] == '\t')||(s[idx] == '\r')||(s[idx] == '\n'))
				{
					idx++;
					continue;
				}
				block += s.Substring(idx, 1);
				idx++;
			}
			if (block != "") ret += block;
			return ret;
		}
		static public string FromAEJson(string s)
		{
			string ret = "";

			int idx = 0;
			int len = s.Length;
			string block = "";
			while (idx < len)
			{
				int j = 0;
				if (StringBlock(s, idx, out j) == true)
				{
					ret += block; block = "";
					ret += s.Substring(idx, j - idx + 1);
					idx = j + 1;
					continue;
				}
				if ((idx < len - 1) && (s[idx] == '(') && (s[idx + 1] == '{'))
				{
					ret += block; block = "";
					ret += "{";
					idx += 2;
					continue;
				}
				if ((idx < len - 1) && (s[idx] == '}') && (s[idx + 1] == ')'))
				{
					ret += block; block = "";
					ret += "}";
					idx += 2;
					continue;
				}
				if (s[idx] == ':')
				{
					ret += "\"" + block + "\"" + s.Substring(idx, 1);
					block = "";
					idx++;
					continue;
				}
				if ((s[idx] == ' ') || (s[idx] == '\t') || (s[idx] == '\r') || (s[idx] == '\n'))
				{
					idx++;
					continue;
				}
				if ((s[idx] == ',') || (s[idx] == '[') || (s[idx] == ']'))
				{
					ret += block + s.Substring(idx, 1);
					block = "";
					idx++;
					continue;
				}
				block += s.Substring(idx, 1);
				idx++;
			}
			if (block != "") ret += block;
			return ret;
		}
	}
}
