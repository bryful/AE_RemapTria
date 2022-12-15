using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AE_RemapTria
{

	public class Ardj
	{
		public string? header { get; set; }
		public string? sheetName { get; set; }
		public int? frameRate { get; set; }
		public int? cellCount { get; set; }
		public int? frameCount { get; set; }
		public int? frameCountTrue { get; set; }
		public int? offsetFrame { get; set; }

		public string[]? caption { get; set; }

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

		public int[][][]? cellWithEnabled { get; set; }

		public int[][][]? cell { get; set; }


		public string[]? rawCaption { get; set; }

		public JsonObject? rawData { get; set; }

		public Ardj()
		{
			//Init();
		}

		public void Init()
		{
			header = "";
			cellCount = 12;
			frameCount = 72;
			frameCountTrue = 72;
			frameRate = 24;
			offsetFrame = 0;

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
			cellWithEnabled =new int[0][][];

			rawCaption = new string[0];
			rawData = new JsonObject();
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
			if(offsetFrame==null)
			{
				offsetFrame = 0;
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
	public class TR_Ardj
	{
		private TR_CellData? m_CellData = null;
		// ****************************************
		public TR_Ardj(TR_CellData? cellData)
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
				return ToJson(FromCellDataToArdj(m_CellData));
			}
		}
		// ****************************************
		public bool Save(string p)
		{
			bool ret = false;
			if(m_CellData == null) return ret;
			if (File.Exists(p) == true) File.Delete(p);
			Encoding enc = new UTF8Encoding(false);
			string json = ToJson(FromCellDataToArdj(m_CellData));
			File.WriteAllText(p, json);
			ret = File.Exists(p);
			return ret;
		}
		// *******************************************************************
		public string ToArdj()
		{
			string ret = "";
			if (m_CellData == null) return ret;
			try
			{
				ret = ToJson(FromCellDataToArdj(m_CellData));
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
		static public bool ArdjToCellData(Ardj? aj,ref TR_CellData cd)
		{
			bool ret = false;
			if (aj == null) return ret;
			if (aj.Check() == false) return ret;
			bool b = cd._eventFlag;
			cd._eventFlag = false;

			if((aj.frameCountTrue!=null)&&(aj.cellCount!=null))
				cd.SetCellFrame((int)aj.cellCount, (int)aj.frameCountTrue);
			if(aj.frameRate != null) cd.FrameRate = (T_Fps)aj.frameRate;
			if (aj.sheetName != null) cd.SheetName = aj.sheetName;
			if (aj.CREATE_USER != null) cd.CREATE_USER = aj.CREATE_USER;
			if (aj.UPDATE_USER != null) cd.UPDATE_USER = aj.UPDATE_USER;
			if (aj.CREATE_TIME != null) cd.CREATE_TIME = DateTime.Parse(aj.CREATE_TIME);
			if (aj.UPDATE_TIME != null) cd.UPDATE_TIME = DateTime.Parse(aj.UPDATE_TIME);
			if (aj.TITLE != null) cd.TITLE = aj.TITLE;
			if (aj.SUB_TITLE != null) cd.SUB_TITLE = aj.SUB_TITLE;
			if (aj.OPUS != null) cd.OPUS = aj.OPUS;
			if (aj.SCECNE != null) cd.SCECNE = aj.SCECNE;
			if (aj.CUT != null) cd.CUT = aj.CUT;
			if (aj.CAMPANY_NAME != null) cd.CAMPANY_NAME = aj.CAMPANY_NAME;

			if (aj.offsetFrame != null) cd.OffSetFrame = (int)aj.offsetFrame;

			if (aj.caption != null) cd.Caption = aj.caption;
			if (aj.frameCount == aj.frameCountTrue)
			{
				if(aj.cell!=null)
				cd.SetCell( aj.cell);
			}
			else
			{
				if(aj.cellWithEnabled !=null)
				cd.CellWithEnabled = aj.cellWithEnabled;
			}
			cd._eventFlag = b;
			return true;
		}       
		// ****************************************
		static public Ardj FromCellDataToArdj(TR_CellData cd)
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

			ardj.offsetFrame = cd.OffSetFrame;

			ardj.caption = cd.Caption;
			ardj.cell = cd.Cell;
			ardj.cellWithEnabled = cd.CellWithEnabled;

			ardj.rawData = cd.RawData;
			ardj.rawCaption = cd.RawCaption;
			return ardj;
		}
		// ****************************************
		public bool ToCellData(Ardj ardj, ref TR_CellData cd)
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

			if (ardj.offsetFrame != null) cd.OffSetFrame = (int)ardj.offsetFrame;

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
				//ret = JsonSerializer.Deserialize<Ardj>(json, GetOption());
				var doc = JsonNode.Parse(json);
				if (doc == null) return null;
				JsonObject jo = (JsonObject)doc;
				if (jo == null)
				{
					ret = null;
					return ret;
				}
				ret = new Ardj();
				string key = "header";
				if (jo.ContainsKey(key))
				{
					ret.header = jo[key].GetValue<string>();
				}
				key = "cellCount";
				if (jo.ContainsKey(key))
				{
					ret.cellCount = jo[key].GetValue<int>();
				}
				key = "frameCount";
				if (jo.ContainsKey(key))
				{
					ret.frameCount = jo[key].GetValue<int>();
				}
				key = "frameCountTrue";
				ret.frameCountTrue = 0;
				if (jo.ContainsKey(key))
				{
					ret.frameCountTrue = jo[key].GetValue<int>();
				}
				if (ret.frameCountTrue < 6) ret.frameCountTrue = ret.frameCount;
				key = "frameRate";
				if (jo.ContainsKey(key))
				{
					ret.frameRate = jo[key].GetValue<int>();
				}
				key = "offsetFrame";
				if (jo.ContainsKey(key))
				{
					ret.offsetFrame = jo[key].GetValue<int>();
				}
				key = "sheetName";
				if (jo.ContainsKey(key))
				{
					ret.sheetName = jo[key].GetValue<string>();
				}
				key = "CREATE_USER";
				if (jo.ContainsKey(key))
				{
					ret.CREATE_USER = jo[key].GetValue<string>();
				}
				key = "UPDATE_USER";
				if (jo.ContainsKey(key))
				{
					ret.UPDATE_USER = jo[key].GetValue<string>();
				}
				key = "CREATE_TIME";
				if (jo.ContainsKey(key))
				{
					ret.CREATE_TIME = jo[key].GetValue<string>();
				}
				key = "UPDATE_TIME";
				if (jo.ContainsKey(key))
				{
					ret.UPDATE_TIME = jo[key].GetValue<string>();
				}
				key = "TITLE";
				if (jo.ContainsKey(key))
				{
					ret.TITLE = jo[key].GetValue<string>();
				}
				key = "SUB_TITLE";
				if (jo.ContainsKey(key))
				{
					ret.SUB_TITLE = jo[key].GetValue<string>();
				}
				key = "OPUS";
				if (jo.ContainsKey(key))
				{
					ret.OPUS = jo[key].GetValue<string>();
				}
				key = "SCECNE";
				if (jo.ContainsKey(key))
				{
					ret.SCECNE = jo[key].GetValue<string>();
				}
				key = "CUT";
				if (jo.ContainsKey(key))
				{
					ret.CUT = jo[key].GetValue<string>();
				}
				key = "CAMPANY_NAME";
				if (jo.ContainsKey(key))
				{
					ret.CAMPANY_NAME = jo[key].GetValue<string>();
				}
				key = "caption";
				if (jo.ContainsKey(key))
				{
					var ja = jo[key].AsArray();
					if(ja.Count>0)
					{
						ret.caption = new string[ja.Count];
						int i = 0;
						foreach (var s in ja)
						{
							ret.caption[i] = s.GetValue<string>();
							i++;

						}
					}
				}
				key = "cell";
				if (jo.ContainsKey(key))
				{
					var ja = jo[key].AsArray();
					if (ja.Count > 0)
					{
						ret.cell = new int[ja.Count][][];
						int i = 0;
						foreach (var s in ja)
						{
							var ja2 = s.AsArray();
							if(ja2.Count >0)
							{
								int[][] vv = new int[ja2.Count][];
								int j = 0;
								foreach (var s2 in ja2)
								{
									var ja3 = s2.AsArray();
									if(ja3.Count>=2)
									{
										int[] vvv = new int[2];
										vvv[0] = ja3[0].GetValue<int>();
										vvv[1] = ja3[1].GetValue<int>();
										vv[j] = vvv;
									}
									j++;
								}
								ret.cell[i] = vv;
							}
							i++;
						}
					}
				}
				key = "cellWithEnabled";
				if (jo.ContainsKey(key))
				{
					var ja = jo[key].AsArray();
					if (ja.Count > 0)
					{
						ret.cellWithEnabled = new int[ja.Count][][];
						int i = 0;
						foreach (var s in ja)
						{
							var ja2 = s.AsArray();
							if (ja2.Count > 0)
							{
								int[][] vv = new int[ja2.Count][];
								int j = 0;
								foreach (var s2 in ja2)
								{
									var ja3 = s2.AsArray();
									if (ja3.Count >= 2)
									{
										int[] vvv = new int[2];
										vvv[0] = ja3[0].GetValue<int>();
										vvv[1] = ja3[1].GetValue<int>();
										vv[j] = vvv;
									}
									j++;
								}
								ret.cellWithEnabled[i] = vv;
							}
							i++;
						}
					}
				}
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
