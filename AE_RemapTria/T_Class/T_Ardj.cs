using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace AE_RemapTria
{
	public class T_Ardj_layer
	{
		public string? header { get; set; }
		public int? frameCount { get; set; }
		public int? frameRate { get; set; }
		public int[][]? cell { get; set; }
		public T_Ardj_layer()
		{
			Init();
		}
		public void Init()
		{
			header = "";
			frameCount = 12;
			frameRate = 24;
			cell = new int[0][];
		}
	}

	public class Ardj
	{
		public string? header { get; set; }
		public int? cellCount { get; set; }
		public int? frameCount { get; set; }
		public int? frameRate { get; set; }
		public string? sheetName { get; set; }
		public string? CREATE_USER { get; set; }
		public string? UPDATE_USER { get; set; }
		public string? CREATE_TIME { get; set; }
		public string? UPDATE_TIME { get; set; }

		public string? TITLE { get; set; }
		public string? SUB_TITLE { get; set; }
		public string? OPUS { get; set; }
		public string? SCECNE { get; set; }
		public string? CUT { get; set; }
		public string? CAMPANY_NAME { get; set; }

		public string[]? caption { get; set; }
		public int[][][]? cell { get; set; }

		public int? frameCountTrue { get; set; }
		public int[][][]? cellWithEnabled { get; set; }
		public Ardj()
		{
			//Init();
		}

		public void Init()
		{
			header = "";
			cellCount = 12;
			frameCount = 72;
			frameRate = 24;
			sheetName = "";
			CREATE_USER = "";
			UPDATE_USER = "";
			UPDATE_USER = "";
			CREATE_TIME = "";
			UPDATE_TIME = "";
			TITLE = "";
			SUB_TITLE = "";
			OPUS = "";
			SCECNE = "";
			CUT = "";
			CAMPANY_NAME = "";
			caption = new string[0];
			cell = new int[0][][];
			frameCountTrue = 72;
			cellWithEnabled =new int[0][][];
		}
		public bool Check()
		{
			bool ret = false;
			if ((header == null)||(header != T_Def.ARDJ_Header)) return ret;
			if ((cellCount==null)||(cellCount < 6)) return ret;
			if ((caption == null) || (caption.Length < cellCount)) return ret;
			if ((frameCount == null) || (frameCount < 6)) return ret;
			if (frameRate == null) frameRate = 24;
			if((frameRate!=24)&& (frameRate != 30)) frameRate = 24;
			if ((frameCountTrue == null) || (frameCountTrue < 6))
			{
				frameCountTrue = frameCount;
			}
			if((cell==null)||(cell.Length<cellCount)) return ret;
			if ((cellWithEnabled == null) || (cellWithEnabled.Length < cellCount+1))
			{
				cellWithEnabled = new int[(int)cellCount + 1][][];
				cellWithEnabled[0] = new int[1][] { new int[] {0,1 } };

				for (int i=0; i<cellCount;i++)
				{
					int l = cell[i].Length;
					int[][] lyr = new int[l][];
					for(int j=0; j<l; j++)
					{
						lyr[j] = new int[2];
						lyr[j][0] = cell[i][j][0];
						lyr[j][1] = cell[i][j][1];
					}
					cellWithEnabled[i + 1] = lyr;
				}
			}
			if (sheetName == null) sheetName = "";
			if (CREATE_USER == null) CREATE_USER = "";
			if (UPDATE_USER == null) UPDATE_USER = "";
			if (UPDATE_USER == null) UPDATE_USER = "";
			if ((CREATE_TIME == null)||(CREATE_TIME=="")) CREATE_TIME = new DateTime(1963, 9, 9).ToString();
			if ((UPDATE_TIME == null)|| (UPDATE_TIME == "")) UPDATE_TIME = new DateTime(1963, 9, 9).ToString();
			if (TITLE == null) TITLE = "";
			if (SUB_TITLE == null) SUB_TITLE = "";
			if (OPUS == null) OPUS = "";
			if (SCECNE == null) SCECNE = "";
			if (CUT == null) CUT = "";
			if (CAMPANY_NAME == null) CAMPANY_NAME = "";
			ret = true;
			return ret;
		}

	}

	public class T_Ardj
	{
		private T_CellData? m_CellData = null;
		// ****************************************
		public T_Ardj(T_CellData? cellData)
		{
			m_CellData = cellData;
		}
		// ****************************************
		public string ToJson()
		{
			if (m_CellData == null)
			{
				return "";
			}
			else
			{
				return ToJson(FromCellDataToJrdj(m_CellData));
			}
		}
		// ****************************************
		public bool Save(string p)
		{
			bool ret = false;
			if(m_CellData == null) return ret;
			try
			{
				var utf8_encoding = new System.Text.UTF8Encoding(false);
				File.WriteAllText(p, ToJson(FromCellDataToJrdj(m_CellData)), utf8_encoding);
				ret = true;
			}
			catch
			{
				ret = false;
			}
			return ret;
		}
		// *******************************************************************
		public string ToArdj()
		{
			string ret = "";
			if (m_CellData == null) return ret;
			try
			{
				ret = ToJson(FromCellDataToJrdj(m_CellData));
			}
			catch
			{
				ret = "";
			}
			return ret;
		}
		// *******************************************************************
		public bool Load(string p)
		{
			bool ret = false;
			if (m_CellData == null) return ret;
			try
			{
				if (File.Exists(p) == true)
				{
					string str = File.ReadAllText(p, Encoding.GetEncoding("utf-8"));
					if (str != "")
					{
						Ardj? a =  FromJson(str);
						if(a != null)
						{
							ret = ArdjToCellData(a,ref m_CellData);
						}
					}
				}
			}
			catch
			{
				ret = false;
			}
			return ret;
		}
		static public bool ArdjToCellData(Ardj? aj,ref T_CellData cd)
		{
			bool ret = false;
			if (aj == null) return ret;
			if (aj.Check() == false) return ret;
			bool b = cd._eventFlag;
			cd._eventFlag = false;

			cd.SetCellFrame((int)aj.cellCount, (int)aj.frameCountTrue);
			cd.FrameRate = (T_Fps)aj.frameRate;
			cd.SheetName = aj.sheetName;
			cd.CREATE_USER = aj.CREATE_USER;
			cd.UPDATE_USER = aj.UPDATE_USER;
			cd.CREATE_TIME = DateTime.Parse(aj.CREATE_TIME);
			cd.UPDATE_TIME = DateTime.Parse(aj.UPDATE_TIME);
			cd.TITLE = aj.TITLE;
			cd.SUB_TITLE = aj.SUB_TITLE;
			cd.OPUS = aj.OPUS;
			cd.SCECNE = aj.SCECNE;
			cd.CUT = aj.CUT;

			cd.Caption = aj.caption;
			if (aj.frameCount == aj.frameCountTrue)
			{
				cd.SetCell( aj.cell);
			}
			else
			{
				cd.CellWithEnabled = aj.cellWithEnabled;
			}
			cd._eventFlag = b;
			cd.CallCountCahnged();
			return true;
		}       
		// ****************************************
		static public Ardj FromCellDataToJrdj(T_CellData cd)
		{
			Ardj ardj = new Ardj();
			ardj.Init();
			ardj.header = T_Def.ARDJ_Header;

			if(cd.CREATE_USER == "")
			{
				ardj.CREATE_USER = Environment.UserName+"("+ Environment.MachineName+")";
			}
			else
			{
				ardj.CREATE_USER = cd.CREATE_USER;
			}
			if ((cd.CREATE_USER != "") && (cd.UPDATE_USER == ""))
			{
				ardj.UPDATE_USER = Environment.UserName+ "(" + Environment.MachineName + ")";
			}
			else
			{
				ardj.UPDATE_USER = cd.UPDATE_USER;
			}

			ardj.CREATE_TIME = cd.CREATE_TIME.ToString();
			ardj.UPDATE_TIME = cd.UPDATE_TIME.ToString();
			DateTime DefT = new DateTime(1963, 9, 9);
			DateTime now = DateTime.Now;
			if(cd.CREATE_TIME == DefT)
			{
				ardj.CREATE_TIME = now.ToString();
			}
			if((cd.UPDATE_TIME == DefT)||(now> cd.UPDATE_TIME))
			{
				ardj.UPDATE_TIME = now.ToString();
			}

			ardj.TITLE = cd.TITLE;
			ardj.SUB_TITLE = cd.SUB_TITLE;
			ardj.OPUS = cd.OPUS;
			ardj.SCECNE = cd.SCECNE;
			ardj.CUT = cd.CUT;
			ardj.CAMPANY_NAME = cd.CAMPANY_NAME;


			ardj.cellCount = cd.CellCount;
			ardj.frameCount = cd.FrameCount;
			ardj.frameCountTrue = cd.FrameCountTrue;
			ardj.frameRate = (int)cd.FrameRate;
			ardj.sheetName = cd.SheetName;

			ardj.caption = cd.Caption;
			ardj.cell = cd.Cell;
			ardj.cellWithEnabled = cd.CellWithEnabled;
			return ardj;
		}
		// ****************************************
		public bool ToCellData(Ardj ardj, ref T_CellData cd)
		{
			bool ret = false;
			if (ardj.header != T_Def.ARDJ_Header) return ret;

			if(ardj.sheetName!=null) cd.SheetName = ardj.sheetName;
			if (ardj.CREATE_USER != null) cd.CREATE_USER = ardj.CREATE_USER;
			if (ardj.UPDATE_USER != null) cd.UPDATE_USER = ardj.UPDATE_USER;
			if (ardj.CREATE_TIME != null) cd.CREATE_TIME = DateTime.Parse(ardj.CREATE_TIME);
			if (ardj.UPDATE_TIME != null) cd.UPDATE_TIME = DateTime.Parse(ardj.UPDATE_TIME);
			if (ardj.TITLE != null) cd.TITLE = ardj.TITLE;
			if (ardj.SUB_TITLE != null) cd.SUB_TITLE = ardj.SUB_TITLE;
			if (ardj.OPUS != null) cd.OPUS = ardj.OPUS;
			if (ardj.SCECNE != null) cd.SCECNE = ardj.SCECNE;
			if (ardj.CUT != null) cd.CUT = ardj.CUT;
			if (ardj.CAMPANY_NAME != null) cd.CAMPANY_NAME = ardj.CAMPANY_NAME;

			int? fc = ardj.frameCount;
			if (fc == null) return ret;
			int fct = (int)fc;
			if ((ardj.frameCountTrue!=null)&&(ardj.frameCountTrue>fc))
			{
				fct = (int)ardj.frameCountTrue;
			}
			else
			{
				fct = (int)fc;
			}
			int f = (int)fc;
			if (f < fct) f = (int)fct;
			if (f < 6) return ret;
			int? cc = cd.CellCount;
			if ((cc == null)||(cc<6)) return ret;

			cd.SetCellFrame((int)cc, f);
			if (ardj.frameRate != null)
				cd.FrameRate = (T_Fps)ardj.frameRate;
			if (ardj.caption != null)
				cd.Caption = ardj.caption;

			if (ardj.cell != null)
				cd.Cell = ardj.cell;
			if (ardj.cellWithEnabled != null)
				cd.CellWithEnabled = ardj.cellWithEnabled;
			ret =true;
			return ret;
		}
		// ****************************************
		static private JsonSerializerOptions GetOption()
		{
			// ユニコードのレンジ指定で日本語も正しく表示、インデントされるように指定
			var options = new JsonSerializerOptions
			{
				Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
				//WriteIndented = true,
			};
			return options;
		}
		// ****************************************
		static public string ToJson(Ardj ardj)
		{
			try
			{

				var json = JsonSerializer.Serialize(ardj, GetOption());
				return json;
			}
			catch
			{
				return "";
			}
		}
		// ****************************************
		static public Ardj? FromJson(string json)
		{
			Ardj? ret = null;

			if (String.IsNullOrEmpty(json))
			{
				return ret;
			}
			try
			{
				ret = JsonSerializer.Deserialize<Ardj>(json, GetOption());
			}
			catch
			{
				ret = null;
			}
			return ret;
		}
	}
	// *************************************************************************************


}
