using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE_RemapTria
{
    public class T_ARD
    {
        private T_CellData? m_CellData = null;

        public int LayerCount { get; set; }
        public int FrameCount { get; set; }
        public int CmpFps { get; set; }
        public string? CREATE_USER { get; set; }
        public string? UPDATE_USER { get; set; }
        public DateTime CREATE_TIME { get; set; }
        public DateTime UPDATE_TIME { get; set; }
        public string? TITLE { get; set; }
        public string? SUB_TITLE { get; set; }
        public string? OPUS { get; set; }
        public string? SCECNE { get; set; }
        public string? CUT { get; set; }
        public string? CAMPANY_NAME { get; set; }

        public string[] CellCaption = new string[0];
        public int[][][] Cell = new int[0][][];

        public T_ARD(T_CellData? cellData)
        {
            Init();
            m_CellData = cellData;
        }
        // **********************************************
        private void Init()
        {
            LayerCount = 10;
            FrameCount = 72;
            CmpFps = 24;
            CREATE_USER = "";
            UPDATE_USER = "";
            CREATE_TIME = new DateTime(1963, 9, 9);
            UPDATE_TIME = new DateTime(1963, 9, 9);
            TITLE = "";
            SUB_TITLE = "";
            OPUS = "";
            SCECNE = "";
            CUT = "";
            CAMPANY_NAME = "";

            CellCaption = new string[0];
            Cell = new int[0][][];
        }
        // **********************************************
        private bool ToCellData()
        {
            if (m_CellData == null) return false;
            bool b = m_CellData._eventFlag;
            m_CellData._eventFlag = false;
            m_CellData.PushUndo(BackupSratus.All);
            m_CellData.SetCellFrame(LayerCount, FrameCount);
            m_CellData.FrameRate = (T_Fps)CmpFps;
            if (CREATE_USER != null) m_CellData.CREATE_USER = CREATE_USER;
            if (UPDATE_USER != null) m_CellData.UPDATE_USER = UPDATE_USER;

            m_CellData.CREATE_TIME = CREATE_TIME;
            m_CellData.UPDATE_TIME = UPDATE_TIME;

            if (TITLE != null) m_CellData.TITLE = TITLE;
            if (SUB_TITLE != null) m_CellData.SUB_TITLE = SUB_TITLE;
            if (OPUS != null) m_CellData.OPUS = OPUS;
            if (SCECNE != null) m_CellData.SCECNE = SCECNE;
            if (CUT != null) m_CellData.CUT = CUT;
            if (CAMPANY_NAME != null) m_CellData.CAMPANY_NAME = CAMPANY_NAME;
            m_CellData._eventFlag = b;

            return true;
        }
        // **********************************************
        private int IndexOfTag(string[] lines, string tag, int start = 0)
        {
            int ret = -1;
            if (start >= lines.Length || lines.Length <= 0 || tag.Length <= 0) return ret;
            string tag2 = tag.ToLower();
            for (int i = start; i < lines.Length; i++)
            {
                if (lines[i].IndexOf(tag2) == 0)
                {
                    ret = i;
                    break;
                }
            }
            return ret;
        }
        // **********************************************
        private string ParamValue(string[] lines, string tag)
        {
            int idx = IndexOfTag(lines, tag);
            if (idx < 0) return "";
            return lines[idx].Substring(tag.Length).Trim(); ;
        }
        // **********************************************
        private string[] GetCellCaption(string[] lines)
        {
            string[] ret = new string[0];
            int sidx = IndexOfTag(lines, "*CellName");
            if (sidx < 0) return ret;
            int nidx = IndexOfTag(lines, "*", sidx + 1);
            if (nidx < 0) return ret;

            List<string> ary = new List<string>();
            for (int i = sidx + 1; i < nidx; i++)
            {
                string line = lines[i];
                if (line == "") continue;
                string[] sa = line.Split("\t");
                if (sa.Length >= 2)
                {
                    ary.Add(sa[1]);
                }
            }
            return ary.ToArray();
        }
        // **********************************************
        private int[][][] GetCell(string[] lines)
        {
            int[][][] ret = new int[0][][];
            int sidx = IndexOfTag(lines, "*CellDataStart");
            if (sidx < 0) return ret;
            int nidx = IndexOfTag(lines, "*End");
            if (nidx < 0) return ret;

            int idx = sidx + 1;
            List<List<int[]>> ary = new List<List<int[]>>();
            while (idx < nidx)
            {
                int p1 = IndexOfTag(lines, "*Cell", idx);
                if (p1 < 0) return ret;
                int p2 = IndexOfTag(lines, "*CellEnd", p1 + 1);
                if (p2 < 0) return ret;
                List<int[]> ary2 = new List<int[]>();
                for (int i = p1 + 1; i < p2; i++)
                {
                    string[] sa = lines[i].Split("\t");
                    if (sa.Length >= 2)
                    {
                        int[] vv = new int[2];
                        int v = 0;
                        if (int.TryParse(sa[0], out v))
                        {
                            vv[0] = v;
                        }
                        if (int.TryParse(sa[1], out v))
                        {
                            vv[1] = v;
                        }
                        ary2.Add(vv);
                    }
                }
                ary.Add(ary2);
                idx = p2;
            }
            ret = new int[ary.Count][][];
            for (int i = 0; i < ary.Count; i++)
            {
                ret[i] = new int[ary[i].Count][];
                for (int j = 0; j < ary[i].Count; j++)
                {
                    ret[i][j][0] = ary[i][j][0];
                    ret[i][j][1] = ary[i][j][1];
                }
            }
            return ret;
        }
        // **********************************************
        private bool DecordARD(string[] lines)
        {
            Init();
            bool ret = false;
            //Param
            string s = "";
            int v = 0;
            s = ParamValue(lines, "LayerCount");
            if (s != "") if (int.TryParse(s, out v)) LayerCount = v;
            s = ParamValue(lines, "FrameCount");
            if (s != "") if (int.TryParse(s, out v)) FrameCount = v;
            s = ParamValue(lines, "CmpFps");
            if (s != "") if (int.TryParse(s, out v)) CmpFps = v;
            CREATE_USER = ParamValue(lines, "CREATE_USER");
            UPDATE_USER = ParamValue(lines, "UPDATE_USER");
            s = ParamValue(lines, "CREATE_TIME");
            CREATE_TIME = DateTime.Parse(s);
            s = ParamValue(lines, "UPDATE_TIME");
            UPDATE_TIME = DateTime.Parse(s);
            TITLE = ParamValue(lines, "TITLE");
            SUB_TITLE = ParamValue(lines, "SUB_TITLE");
            OPUS = ParamValue(lines, "OPUS");
            SCECNE = ParamValue(lines, "SCECNE");
            CUT = ParamValue(lines, "CUT");
            CAMPANY_NAME = ParamValue(lines, "CAMPANY_NAME");

            // cellCaption
            CellCaption = GetCellCaption(lines);
            Cell = GetCell(lines);
            ToCellData();
            ret = true;
            return ret;
        }
        // **********************************************
        private string[] DelEmpty(string[] lines)
        {
            List<string> ls = new List<string>();
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                if (line != "")
                {
                    ls.Add(line);
                }
            }
            return ls.ToArray();
        }
        // **********************************************
        public bool Load(string p)
        {
            bool ret = false;
            //ファイルがなければエラー
            if (File.Exists(p) == false) return ret;
            try
            {
                string[] sa = File.ReadAllLines(p);
                if (sa.Length <= 0) return ret;
                sa = DelEmpty(sa);
                if (sa[0].IndexOf("#TimeSheetGrid SheetData") != 0) return ret;
                ret = DecordARD(sa);
            }
            finally
            {
            }
            return ret;
        }
        // **********************************************
    }
}
