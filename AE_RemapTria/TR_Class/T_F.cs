using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE_RemapTria
{
    public enum FileType
    {
        None,
        Ardj,
        Ard,
        STS
    }

    public class T_F
    {
        private static int Find(byte[] buf, byte[] t, int s = 0)
        {
            int ret = -1;
            if (buf.Length <= 0 || t.Length <= 0) return ret;

            for (int i = 0; i < buf.Length - t.Length; i++)
            {
                if (buf[i] == t[0])
                {
                    bool b = true;
                    for (int j = 1; j < t.Length; j++)
                    {
                        if (buf[i + j] != t[j])
                        {
                            b = false;
                            break;
                        }
                    }
                    if (b)
                    {
                        ret = i;
                        break;
                    }
                }
            }
            return ret;
        }
        public static FileType ChkFileType(string p)
        {
            byte[] BOM = new byte[] { 0xEF, 0xBB, 0xBF };
            byte[] STS = new byte[] { 0x11, 0x53, 0x68, 0x69, 0x72, 0x61, 0x68, 0x65, 0x69, 0x54, 0x69, 0x6D, 0x65, 0x53, 0x68, 0x65, 0x65, 0x74 };
            byte[] ARD = new byte[] {0x23, 0x54, 0x69, 0x6D, 0x65, 0x53, 0x68, 0x65, 0x65, 0x74, 0x47,
                0x72, 0x69, 0x64, 0x20, 0x53, 0x68, 0x65, 0x65, 0x74, 0x44, 0x61, 0x74, 0x61 };
            byte[] ARDJ = new byte[] { 0x7B };
            FileType ret = FileType.None;
            if (File.Exists(p) == false) return ret;
            FileStream fs = new FileStream(p, FileMode.Open, FileAccess.Read);
            byte[] bs = new byte[0x20];
            try
            {
                int sz = fs.Read(bs, 0, bs.Length);
                if (sz == bs.Length)
                {
                    // BOMが付いていたら消す
                    if (bs[0] == BOM[0] && bs[1] == BOM[1] && bs[0] == BOM[0])
                    {
                        for (int i = 0; i < bs.Length - 3; i++)
                        {
                            bs[i] = bs[i + 3];
                        }
                    }
                }
            }
            catch
            {
                return ret;
            }
            finally
            {
                fs.Close();
            }
            if (bs[0] == ARDJ[0])
            {
                ret = FileType.Ardj;

            }
            else if (Find(bs, ARD) == 0)
            {
                ret = FileType.Ard;
            }
            else if (Find(bs, STS) == 0)
            {
                ret = FileType.STS;
            }
            return ret;
        }
    }
}
