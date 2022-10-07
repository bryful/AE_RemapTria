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

namespace AE_RemapTria.T_Class
{
	public class Ardj
	{
		public string? header { get; set; }
		public int? cellCount { get; set; }
		public int? frameCount { get; set; }
		public int? frameRate { get; set; }
		public string? sheetName { get; set; }
		public string? CREATE_USER { get; set; }
		public string? UPDATE_USER { get; set; }
		public DateTime? CREATE_TIME { get; set; }
		public DateTime? UPDATE_TIME { get; set; }

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
			Init();
		}

		public void Init()
		{
			header = T_Def.ARDJ_Header;
			cellCount = 12;
			frameCount = 72;
			frameRate = 24;
			sheetName = "";
			CREATE_USER = "";
			UPDATE_USER = "";
			UPDATE_USER = "";
			CREATE_TIME = new DateTime(1963, 9, 9); ;
			UPDATE_TIME = new DateTime(1963, 9, 9); ;
			TITLE = "";
			SUB_TITLE = "";
			OPUS = "";
			SCECNE = "";
			CUT = "";
			CAMPANY_NAME = "";
			caption = new string[0];
			cell = new int[0][][];
			frameCountTrue = 72;
			cellWithEnabled =[0][][];
		}
	}

	public class T_Ardj
	{
		// ****************************************
		public T_Ardj()
		{
		}
	// ****************************************
	public void CopyToCellData(Ardj ardj,ref T_CellData cd)
		{
			/*

			cd.SetCellFrame((int)ardj.cellCount, (int)ardj.frameCount)
			cd.frameRate = ardj.frameRate;
			cd.frameCountTrue = ardj.frameCountTrue;
			cd.sheetName = ardj.sheetName;
			if (cd.sheetName == null) sheetName = "";
			cd.CREATE_USER = ardj.CREATE_USER;
			if (cd.CREATE_USER == null) CREATE_USER = "";
			cd.UPDATE_USER = ardj.UPDATE_USER;
			if (cd.UPDATE_USER == null) UPDATE_USER = "";
			cd.CREATE_TIME = ardj.CREATE_TIME;
			cd.UPDATE_TIME = ardj.UPDATE_TIME;
			cd.TITLE = ardj.TITLE;
			cd.SUB_TITLE = ardj.SUB_TITLE;
			cd.OPUS = ardj.OPUS;
			cd.SCECNE = ardj.SCECNE;
			cd.CUT = ardj.CUT;
			cd.cell = ardj.cell;
			cd.cellWithEnabled = ardj.cellWithEnabled;
			*/
		}
		// ****************************************
		public Ardj FromCellDataToJrdj(T_CellData cd)
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

			ardj.CREATE_TIME = cd.CREATE_TIME;
			ardj.UPDATE_TIME = cd.UPDATE_TIME;
			DateTime DefT = new DateTime(1963, 9, 9);
			DateTime now = DateTime.Now;
			if(cd.CREATE_TIME == DefT)
			{
				ardj.CREATE_TIME = now;
			}
			if((cd.UPDATE_TIME == DefT)||(now> cd.UPDATE_TIME))
			{
				ardj.UPDATE_TIME = now;
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

			ardj.caption = cd.Captions;
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
			if (ardj.CREATE_USER != null) cd.UPDATE_USER = ardj.UPDATE_USER;
			if (ardj.CREATE_TIME != null) cd.CREATE_TIME = ardj.CREATE_TIME;
			if (ardj.UPDATE_TIME != null) cd.UPDATE_TIME = ardj.UPDATE_TIME;
			if (ardj.TITLE != null) cd.TITLE = ardj.TITLE;
			if (ardj.SUB_TITLE != null) cd.SUB_TITLE = ardj.SUB_TITLE;
			if (ardj.OPUS != null) cd.OPUS = ardj.OPUS;
			if (ardj.SCECNE != null) cd.SCECNE = ardj.SCECNE;
			if (ardj.CUT != null) cd.CUT = ardj.CUT;
			if (ardj.CAMPANY_NAME != null) cd.CAMPANY_NAME = ardj.CAMPANY_NAME;

			int? fc = ardj.frameCount;
			if (fc == null) return ret;
			int? fct = fc;
			if ((ardj.frameCountTrue!=null)&&(ardj.frameCountTrue>fc))
			{
				fct = (int)ardj.frameCountTrue;
			}
			else
			{
				fct = fc;
			}
			int f = (int)fc;
			if (f < fct) f = (int)fct;
			if (f < 6) return ret;
			int? cc = cd.CellCount;
			if ((cc == null)||(cc<6)) return ret;

			cd.SetCellFrame((int)cc, f);
			cd.FrameRate = (T_Fps)cd.FrameRate;
			cd.Captions = cd.Captions;

			cell = cd.Cell;
			cellWithEnabled = cd.CellWithEnabled;
		}
		// ****************************************
		private JsonSerializerOptions GetOption()
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
		public string ToJson()
		{
			try
			{
				var json = JsonSerializer.Serialize(this, GetOption());
				return json;
			}
			catch (JsonException e)
			{
				Console.WriteLine(e.Message);
				return "";
			}
		}
		// ****************************************
		public bool FromJson(string json)
		{
			if (String.IsNullOrEmpty(json))
			{
				return false;
			}
			try
			{
				//T_Ardj sub = JsonSerializer.Deserialize<T_Ardj>(json, GetOption());
				//Copy(sub);
				var doc = JsonNode.Parse(json);
				JsonObject? jo = (JsonObject)doc;
				if (jo == null) return false;
				string key = "";
				key = "header";
				if(jo.ContainsKey(key)==false) return false;
				if (jo[key].GetValue<string>() != T_Def.ARDJ_Header) return false;
				key = "cellCount";
				cellCount = 12;
				if (jo.ContainsKey(key)) cellCount = jo[key].GetValue<int>();
				frameRate = 24;
				key = "frameRate";
				if (jo.ContainsKey(key)) frameRate = jo[key].GetValue<int>();
				frameCountTrue = 72;
				frameCount = 72;
				key = "frameCount";
				if (jo.ContainsKey(key))
				{
					frameCount = jo[key].GetValue<int>();
				}
				key = "frameCountTrue";
				if (jo.ContainsKey(key))
				{
					frameCountTrue = jo[key].GetValue<int>();
				}
				else
				{
					frameCountTrue = frameCount;
				}
				key = "cell";
				cell = new int[0][][];
				if (jo.ContainsKey(key))
				{
					cell = jo[key].GetValue<int[][][]>();
				}
				key = "cellWithEnabled";
				cellWithEnabled = new int[0][][];
				if (jo.ContainsKey(key))
				{
					cellWithEnabled = jo[key].GetValue<int[][][]>();
				}
				key = "caption";
				caption = new string[0];
				if (jo.ContainsKey(key))
				{
					caption = jo[key].GetValue<string[]>();
				}

				key = "CREATE_TIME";
				CREATE_TIME = new DateTime(1963, 9, 9); ;
				if (jo.ContainsKey(key)) CREATE_TIME = DateTime.Parse(jo[key].GetValue<string>());
				key = "UPDATE_TIME";
				UPDATE_TIME = new DateTime(1963, 9, 9); ;
				if (jo.ContainsKey(key)) UPDATE_TIME = DateTime.Parse(jo[key].GetValue<string>());

				key = "sheetName";
				sheetName = "";
				if (jo.ContainsKey(key)) sheetName = jo[key].GetValue<string>();
				key = "CREATE_USER";
				CREATE_USER = "";
				if (jo.ContainsKey(key)) CREATE_USER = jo[key].GetValue<string>();
				key = "UPDATE_USER";
				UPDATE_USER = "";
				if (jo.ContainsKey(key)) UPDATE_USER = jo[key].GetValue<string>();
				key = "TITLE";
				TITLE = "";
				if (jo.ContainsKey(key)) TITLE = jo[key].GetValue<string>();
				key = "SUB_TITLE";
				SUB_TITLE = "";
				if (jo.ContainsKey(key)) SUB_TITLE = jo[key].GetValue<string>();
				key = "OPUS";
				OPUS = "";
				if (jo.ContainsKey(key)) OPUS = jo[key].GetValue<string>();
				key = "SCECNE";
				SCECNE = "";
				if (jo.ContainsKey(key)) SCECNE = jo[key].GetValue<string>();
				key = "CUT";
				CUT = "";
				if (jo.ContainsKey(key)) CUT = jo[key].GetValue<string>();
				key = "CAMPANY_NAME";
				CAMPANY_NAME = "";
				if (jo.ContainsKey(key)) CAMPANY_NAME = jo[key].GetValue<string>();
				return true;
			}
			catch (JsonException e)
			{
				return false;
			}
		}
	}
	// *************************************************************************************
	public class T_Ardj_layer
	{
		public string header { get; set; }
		public int frameCount { get; set; }
		public int frameRate { get; set; }
		public int[][] cell { get; set; }
	}

}
