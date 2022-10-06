using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE_RemapTria
{
	/// <summary>
	/// フレームレート
	/// </summary>
	public enum T_Fps
	{
		FPS24 = 24,
		FPS30 = 30
	}
	/// <summary>
	/// １ページのフレーム数 ６秒シートと３秒シート
	/// </summary>
	public enum T_PageSec
	{
		sec3 = 3,
		sec6 = 6
	}
	//フレームの表示設定
	public enum T_FrameDisp
	{
		frame = 0,
		pageFrame,
		pageSecFrame,
		SecFrame,
		Count
	}
	/// <summary>
	/// セルフレームの表示タイプ CellSatusで使用
	/// </summary>
	public enum CellType
	{
		/// <summary>
		/// 表示なし
		/// </summary>
		None,
		/// <summary>
		/// 数字表示
		/// </summary>
		Normal,
		/// <summary>
		/// 前のコマと同じ数値
		/// </summary>
		SameAsBefore,
		/// <summary>
		/// 空セルはじめ
		/// </summary>
		EmptyStart
	}
	/// <summary>
	/// セルフレームの表示データ T_Gridで描画しに使用
	/// </summary>
	public struct CellSatus
	{
		public int Cell = -1;
		public int Frame = -1;
		public int Value = 0;
		public CellType Status = CellType.None;

		public CellSatus()
		{
			Init();
		}
		public void Init()
		{
			Cell = -1;
			Frame = -1;
			Value = 0;
			Status = CellType.None;
		}
	}

	public class T_Def
	{
	}

}
