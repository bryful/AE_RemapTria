using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace AE_RemapTria
{
	public class TR_STS
	{
		byte[] header = new byte[]
		{
			0x11,0x53,0x68,0x69,0x72,0x61,0x68,0x65,0x69,0x54,0x69,0x6D,0x65,0x53,0x68,0x65,0x65,0x74
		};
		private TR_CellData? m_CellData= null;

		public string SheetName = "";	
		public int CellCount = 0;
		public int FrameCount = 0;
		public int[][] m_data = new int[0][];
		public string[] Caption = new string[0];
		public TR_STS(TR_CellData cd)
		{
			m_CellData = cd;
		}
		// ***************************************************************************
		private bool ToCellData()
		{
			bool ret = false;
			if (m_CellData == null) return ret;
			m_CellData.PushUndo(BackupSratus.All);
			bool be = m_CellData._eventFlag;
			bool bu = m_CellData._undoPushFlag;
			m_CellData._eventFlag = false;
			m_CellData._undoPushFlag = false;
			m_CellData.ClearAll();
			m_CellData.SetCellFrame(CellCount, FrameCount);
			m_CellData.SheetName = SheetName;
			m_CellData.Caption = Caption;
			m_CellData.SetAllValues(m_data);
			m_CellData._eventFlag = be;
			m_CellData._undoPushFlag = bu;
			return ret;
		}
		// ***************************************************************************
		private int IndexOf(byte[] buf, byte[] v)
		{
			int ret = -1;
			if((buf.Length<=0)||(v.Length<=0)) return ret;
			int cnt = buf.Length -v.Length;
			for(int i=0; i<cnt;i++)
			{
				if(buf[i] == v[0])
				{
					bool ok = true;
					for(int j=1; j< v.Length;i++)
					{
						if( buf[i+j] != v[j])
						{
							ok = false;
							break;
						}
					}
					if(ok)
					{
						ret = i;
						break;
					}
				}
			}
			return ret;
		}
		// ***************************************************************************
		private bool StsDecord(byte[] buf)
		{
			bool ret = false;
			if ((buf == null) || (buf.Length < 0x20)) return ret;
			if (IndexOf(buf, header) != 0) return ret;
			int c = buf[0x12];
			int f = (int)((uint)buf[0x13] | (uint)buf[0x14]<<8);
			if ((c <= 0) || (f <= 0)) return ret;
			CellCount = c;
			FrameCount = f;
			m_data = new int[CellCount][];
			for (int i = 0; i < c; i++) m_data[i] = new int[f];
			int idx = 0x17;
			for (int j = 0; j < c; j++)
			{
				for (int i = 0; i < f; i++)
				{
					int v = (int)buf[idx];
					idx++;
					v |= (int)buf[idx] <<8;
					idx++;
					if (v < 0) v = 0;
					m_data[j][i] = v;
				}
				if(idx >= buf.Length) return ret;
			}
			int cc = 0;
			Caption = new string[c];
			while (idx < buf.Length)
			{
				int cnt = buf[idx];
				idx++;

				byte[] ba = new byte[cnt];
				for (int i = 0; i < cnt; i++)
				{
					ba[i] = buf[idx];
					idx++;
				}
				Caption[cc] = Encoding.Unicode.GetString(ba);
				cc++;
				if (idx >= buf.Length)
				{
					if(cc==c) ret = true;
					break;
				}
			}
			return ret;
		}
		// ***************************************************************************
		public bool Load(string p)
		{
			bool ret = false;
			if (File.Exists(p) == false) return ret;
			FileStream fs = new FileStream(
				p,
				FileMode.Open,
				FileAccess.Read);
			try
			{
				byte[] bs = new byte[fs.Length];
				int sz = fs.Read(bs, 0, bs.Length);
				ret = (sz == bs.Length);
				if(ret)
				{
					SheetName = T_Def.GetNameNoExt(p);
					ret = StsDecord(bs);
					if(ret)
					{
						ret = ToCellData();
					}
				}

			}
			catch
			{
				ret = false;
			}
			finally
			{
				fs.Close();
			}

			return ret;
		}
	}
}
