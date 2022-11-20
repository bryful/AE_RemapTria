using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharpCore.Drawing;
using PdfSharpCore.Fonts;
using PdfSharpCore.Pdf;
using PdfSharpCore;
using System.Reflection;
using static System.Windows.Forms.LinkLabel;
using System.Windows.Forms;

namespace AE_RemapTria
{

    public class T_PDF
    {
        // **********************************************************************************************
        static double pt2mm(XUnit pt) { return (double)pt * 25.4 / 72; }
        static XUnit mm2pt(double mm) { return (XUnit)(mm * 72 / 25.4); }
        // **********************************************************************************************
        /// <summary>
        /// 用紙の横幅
        /// </summary>
        public const double A4Width = 595;
        /// <summary>
        /// 用紙の縦幅
        /// </summary>
        public const double A4Height = 842;
        /// <summary>
        /// 左端の隙間
        /// </summary>
        public const double LeftInt = 15;
        /// <summary>
        /// 右端の位置
        /// </summary>
        public const double LeftPos = LeftInt;
        /// <summary>
        /// 右端の隙間
        /// </summary>
        public const double RightInt = 15;
        /// <summary>
        /// 右端の位置
        /// </summary>
        public const double RightPos = A4Width - RightInt;
        /// <summary>
        /// 上の隙間
        /// </summary>
        public const double TopInt = 19;
        /// <summary>
        /// 上の位置
        /// </summary>
        public const double TopPos = TopInt;
        /// <summary>
        /// 下の隙間
        /// </summary>
        public const double BottomInt = 24;
		public const double BottomPos = A4Height - BottomInt;
		/// <summary>
		/// 基本的な横幅
		/// </summary>
		public const double BaseWidth = A4Width - LeftInt - RightInt;
        public const double CapHeight = 36;
		public const double CapHorHeight = 12;
		public const double CapTop = TopInt;
        public const double CapHorPos = TopPos + CapHorHeight;
        public const double CapBottom = TopPos + CapHeight;
		public const double CapHeight2= CapHeight - CapHorHeight;

		public const double CapTitlePos = LeftInt;
        public const double CapTitleWidth = 115;
        public const double CapOpsPos = CapTitlePos + CapTitleWidth;

        public const double CapOpsWidth = 55;
        public const double CapScenePos = CapOpsPos + CapOpsWidth;
        
        public const double CapSceneWidth = 55;
        public const double CapCutPos = CapScenePos + CapSceneWidth;
        
        public const double CapCutWidth = 75;
        public const double CapTimePos = CapCutPos + CapCutWidth;
        
        public const double CapTimeWidth = 95;
        public const double CapUserPos = CapTimePos + CapTimeWidth;
        
        public const double CapUserWidth = 75;
        public const double CapRightPos = CapUserPos + CapUserWidth;

		public const double CapPageHeight = CapHeight - CapHorHeight;

		public const double CapPageWidth = 75;
		public const double CapPageWidthA = 40;
		public const double CapPageWidthB = 28;
		public const double CapPageLeftPos = A4Width - RightInt - CapPageWidth;
		public const double CapPageSlashH1 = CapHorPos + 5;
		public const double CapPageSlashH2 = CapBottom - 5;
		public const double CapPageSlashW1 = CapPageLeftPos + CapPageWidthA;
		public const double CapPageSlashW2 = CapPageRightPos - CapPageWidthB;

		public const double CapPageRightPos = RightPos;



		// *************************************************
		public const double CellWidth = 13;
		public const double CellHeight24 = 9;
		public const double CellHeight30 = 648/90;

		public const double CellGridTop = BottomPos - CellGridHeight;
		public const double CellGridHeight = 648 + CellHor1Height + CellHor2Height;
		public const double CellGridWidrh= 277;


        // *************************************************
        public const double CellVur1Pos =  25;
		public const double CellVur2Pos = CellVur1Pos + 30;
		public const double CellVur3Pos = CellVur2Pos + CellWidth * CellColCount;
        public const double CellFrameWidth = CellVur2Pos - CellVur1Pos;
		public const double CellTG = 210;

		public const double CellHor1Height = 12;
		public const double CellHor2Height = 12;

		public const double CellHor1pos = CellGridTop + CellHor1Height;
		public const double CellHor2pos = CellHor1pos + CellHor2Height;

		public const double CellColCount = 15;
		public const double CellGridHor1 = CellHor1Height + CellGridTop;


		public const double CellRightPos = RightPos - CellGridWidrh;



		public const double lw = 1;
		public const double lw1 = 0.8;
		public const double lw2 = 0.7;
		public const double lws = 0.5;




		// **********************************************************************************************
		static private void DrawCap(T_Grid m_grid, XGraphics g, XPen p, int page,int pageMax)
        {
            var lines = new List<XPoint>()
                    {
                        // pt単位
                        new XPoint(CapTitlePos, CapTop),
                        new XPoint(CapRightPos, CapTop),
                        new XPoint(CapRightPos, CapBottom),
                        new XPoint(CapTitlePos, CapBottom),
                    };
			p.Width = lw;
			g.DrawPolygon(p, lines.ToArray());
			lines = new List<XPoint>()
					{
                        // pt単位
                        new XPoint(CapPageLeftPos, CapTop),
						new XPoint(CapPageRightPos, CapTop),
						new XPoint(CapPageRightPos, CapBottom),
						new XPoint(CapPageLeftPos, CapBottom),
					};
			g.DrawPolygon(p, lines.ToArray());
            p.Width = lw2;
            g.DrawLine(p,CapTitlePos,CapHorPos,CapRightPos,CapHorPos);
			g.DrawLine(p, CapPageLeftPos, CapHorPos, CapPageRightPos, CapHorPos);
			p.Width = lw2;
			g.DrawLine(p, CapOpsPos, CapTop, CapOpsPos, CapBottom);
			g.DrawLine(p, CapScenePos, CapTop, CapScenePos, CapBottom);
			g.DrawLine(p, CapCutPos, CapTop, CapCutPos, CapBottom);
			g.DrawLine(p, CapTitlePos, CapTop, CapTitlePos, CapBottom);
			g.DrawLine(p, CapTimePos, CapTop, CapTimePos, CapBottom);
			g.DrawLine(p, CapUserPos, CapTop, CapUserPos, CapBottom);

			var f = new XFont("Gen Shin Gothic", 10, XFontStyle.Italic);

            g.DrawString("Title", f, XBrushes.Black,
                new XRect(CapTitlePos, CapTop, CapTitleWidth, CapHorHeight),
                XStringFormats.Center);
			g.DrawString("Opus", f, XBrushes.Black,
				new XRect(CapOpsPos, CapTop, CapOpsWidth, CapHorHeight),
				XStringFormats.Center);
			g.DrawString("Scene", f, XBrushes.Black,
				new XRect(CapScenePos, CapTop, CapSceneWidth, CapHorHeight),
				XStringFormats.Center);
			g.DrawString("Cut", f, XBrushes.Black,
				new XRect(CapCutPos, CapTop, CapCutWidth, CapHorHeight),
				XStringFormats.Center);
			g.DrawString("Time", f, XBrushes.Black,
				new XRect(CapTimePos, CapTop, CapTimeWidth, CapHorHeight),
				XStringFormats.Center);
			g.DrawString("Operater", f, XBrushes.Black,
				new XRect(CapUserPos, CapTop, CapUserWidth, CapHorHeight),
				XStringFormats.Center);
			g.DrawString("Sheet No.", f, XBrushes.Black,
				new XRect(CapPageLeftPos, CapTop, CapPageWidth, CapHorHeight),
				XStringFormats.Center);

			f = new XFont("Gen Shin Gothic", 16, XFontStyle.Regular);
			g.DrawString($"{page + 1} / {pageMax}", f, XBrushes.Black,
				new XRect(CapPageLeftPos, CapHorPos, CapPageWidth, CapPageHeight),
				XStringFormats.Center);
			f = new XFont("Gen Shin Gothic", 4.5, XFontStyle.Italic);
			g.DrawString("枚目中", f, XBrushes.Black,
				new XRect(CapPageLeftPos, CapHorPos, CapPageWidth-1, CapPageHeight-0.5),
				XStringFormats.BottomRight);

			f = new XFont("Gen Shin Gothic", 15, XFontStyle.Regular);
			g.DrawString($"{m_grid.CellData.TITLE}", f, XBrushes.Black,
				new XRect(CapTitlePos, CapHorPos, CapTitleWidth, CapHeight2),
				XStringFormats.Center);
			g.DrawString($"{m_grid.CellData.OPUS}", f, XBrushes.Black,
				new XRect(CapOpsPos, CapHorPos, CapOpsWidth, CapHeight2),
				XStringFormats.Center);
			g.DrawString($"{m_grid.CellData.SCECNE}", f, XBrushes.Black,
				new XRect(CapScenePos, CapHorPos, CapSceneWidth, CapHeight2),
				XStringFormats.Center);
			g.DrawString($"{m_grid.CellData.CUT}", f, XBrushes.Black,
				new XRect(CapCutPos, CapHorPos, CapCutWidth, CapHeight2),
				XStringFormats.Center);
            int fc = m_grid.CellData.FrameCount;
            int s = m_grid.CellData.FrameCount / (int)m_grid.CellData.FrameRate;
			int k = m_grid.CellData.FrameCount % (int)m_grid.CellData.FrameRate;
			g.DrawString($"{s}+{k:00}", f, XBrushes.Black,
				new XRect(CapTimePos, CapHorPos, CapTimeWidth, CapHeight2),
				XStringFormats.Center);

            string u = m_grid.CellData.UPDATE_USER;
            if(u=="") u = m_grid.CellData.CREATE_USER;
			g.DrawString($"{u}", f, XBrushes.Black,
				new XRect(CapUserPos, CapHorPos, CapUserWidth, CapHeight2),
				XStringFormats.Center);




		}
		// **********************************************************************************************
		static private void DrawSheetGrid(T_Grid m_grid, XGraphics g, XPen p, int page,int left)
        {
            double x = 0;
            if(left==0)
            {
                x = LeftPos;
            }
            else
            {
				x = CellRightPos;
			}
            #region セル名
            int cc = m_grid.CellData.CellCount;
            if (cc > (int)CellColCount) cc = (int)CellColCount;
			var f1 = new XFont("Gen Shin Gothic", 9, XFontStyle.Bold);
			var f2 = new XFont("Gen Shin Gothic", 9, XFontStyle.Italic);
			for (int i=0; i<cc; i++)
            {
                string cn = m_grid.CellData.Caption[i];
				double x2 = x + CellVur2Pos + CellWidth * i;
				g.DrawString($"{cn}", f1, XBrushes.Black,
					new XRect(x2, CellHor1pos, CellWidth, CellHor2Height),
					XStringFormats.Center);
				g.DrawString($"{cn}", f2, XBrushes.Black,
					new XRect(x2, BottomPos, CellWidth, CellHor2Height),
					XStringFormats.TopCenter);
			}
			#endregion
			int fc = 72;
			double fh = CellHeight24;
			int si = 24;
			int hi = 12;
			int hhi = 6;
			if ((int)m_grid.CellData.FrameRate == 30)
			{
				fc = 90;
				fh = CellHeight30;
				si = 30;
				hi = 15;
				hhi = 5;
			}
			#region フレーム数
			f1 = new XFont("Gen Shin Gothic", 7, XFontStyle.Italic);
			for (int i=0; i<fc; i++)
            {
                int frm = page * fc * 2 + i;
                if (left == 1) frm += fc;
				string frms = "";
				frms = $"{(frm%(fc*2) + 1)}";
				double yy = CellHor2pos + fh * i;
				if (frm >= m_grid.CellData.FrameCount)
				{
					g.DrawRectangle(XBrushes.LightGray,
						new XRect(x + CellVur1Pos, yy, CellFrameWidth, fh)
						);
				}

				g.DrawString($"{frms}", f1, XBrushes.Black,
					new XRect(x+CellVur1Pos,yy , CellFrameWidth-2, fh),
					XStringFormats.CenterRight);
                if( (i+1)%si == 0 )
                {
					int sec = frm / si;
					g.DrawString($"{sec+1}", f1, XBrushes.Black,
						new XRect(x + CellVur1Pos+2, yy, CellFrameWidth, fh),
						XStringFormats.CenterLeft);

				}
			}

			#endregion
			#region コマ数

			p.Width = lws;
			for(int c=0; c<cc;c++)
			{
				double xp = x + CellVur2Pos + CellWidth * c;
				for (int f = 0; f < fc; f++)
				{
					int frm = page * fc * 2 + f;
					if (left == 1) frm += fc;
					double yp = CellHor2pos + fh * f;
					CellSatus cs = m_grid.CellData.GetCellStatus(c, frm);
					double cx = xp + CellWidth / 2;
					double cy = yp + fh / 2;
					double l = fh / 2 - 1;
					if (frm < m_grid.CellData.FrameCount)
					{
						switch (cs.Status)
						{
							case CellType.SameAsBefore:
								g.DrawLine(p, cx, cy - l, cx, cy + l);
								break;
							case CellType.EmptyStart:
								g.DrawLine(p, cx - l, cy - l, cx + l, cy + l);
								g.DrawLine(p, cx - l, cy + l, cx + l, cy - l);
								break;
							case CellType.Normal:
								g.DrawString($"{cs.Value}", f1, XBrushes.Black,
									new XRect(xp, yp, CellWidth, fh),
									XStringFormats.Center);
								break;
							case CellType.None:
							default:
								break;
						}
					}
					else
					{
						g.DrawRectangle(XBrushes.LightGray, new XRect(xp, yp, CellWidth, fh));
					}
				}
			}


			#endregion

			#region 縦線
			//縦線
			p.Width = lw2;
            g.DrawLine(p, x + CellVur1Pos, CellGridTop, x + CellVur1Pos, BottomPos);
			g.DrawLine(p, x + CellVur2Pos, CellGridTop, x + CellVur2Pos, BottomPos);
			g.DrawLine(p, x + CellVur3Pos, CellGridTop, x + CellVur3Pos, BottomPos);
			p.Width = lws;
			for (int i=1; i< CellColCount;i++)
            {
                double x2 = x + CellVur2Pos + CellWidth * i;
                g.DrawLine(p, x2, CellHor1pos, x2, BottomPos);
			}
			#endregion
			#region 横線
			//横線
			p.Width = lw2;
			g.DrawLine(p, x + CellVur2Pos, CellHor1pos, x + CellVur3Pos, CellHor1pos);
			p.Width = lw;
			g.DrawLine(p, x , CellHor2pos, x + CellGridWidrh, CellHor2pos);
 

			p.Width = lws;
			for (int i = 1; i < fc; i++)
			{
                if(i % si ==0)
                {
                    p.Width = lw;
                }else if( i % hi ==0)
                {
					p.Width = lw1;
				}
				else if (i % hhi == 0)
				{
					p.Width = lw2;
                }
                else
                {
					p.Width = lws;
				}
				double y = CellHor2pos + fh * i;
               g.DrawLine(p, x + CellVur1Pos, y, x + CellGridWidrh, y);
			}
			#endregion
			p.Width = lw;
			//周囲の枠
			var lines = new XPoint[4]
				{
                    new XPoint(x,CellGridTop ),
					new XPoint(x+CellGridWidrh, CellGridTop),
					new XPoint(x+CellGridWidrh, BottomPos),
					new XPoint(x, BottomPos),
				};
			p.Width = 1;
			g.DrawPolygon(p, lines.ToArray());
		}
		// **********************************************************************************************
		static private void DrawGrid(T_Grid m_grid,XGraphics g, XPen p, int page)
        {
            // メモ
            var lines = new List<XPoint>()
                    {
                        // pt単位
                        new XPoint(LeftPos,CapBottom+5 ),
                        new XPoint(RightPos, CapBottom+5),
                        new XPoint(RightPos, CellGridTop-5),
                        new XPoint(LeftPos, CellGridTop-5),
                    };
			p.Width = lws;
			g.DrawPolygon(p, lines.ToArray());

            DrawSheetGrid(m_grid, g, p, page, 0);
			DrawSheetGrid(m_grid, g, p, page, 1);
		}

		static private bool _loadFontResolver = false;
		// **********************************************************************************************
		static public bool ExportPDF(string filePath,T_Grid gd)
        {
			if (_loadFontResolver == false)
			{
				GlobalFontSettings.FontResolver = new JapaneseFontResolver();
				_loadFontResolver = true;
			}
			T_Grid m_grid = gd;
			bool ret = false;
            try
            {
                //MessageBox.Show($"{CapPageWidth}");
                using (var document = new PdfDocument())
                {
					// 新規ドキュメントを作成
					int pageF = (int)m_grid.CellData.FrameRate * 6;

					int pc = m_grid.CellData.FrameCount / pageF;
                    if (m_grid.CellData.FrameCount % pageF>0) pc++;
					XPen p = new XPen(XColor.FromArgb(0, 0, 0), 1);
					for (int i = 0; i < pc; i++)
                    {
                        var page = document.AddPage();
                        page.Size = PageSize.A4;
                        page.Orientation = PageOrientation.Portrait;
                        //MessageBox.Show($"{page.Width.Millimeter}x{page.Height.Millimeter}");
                        var g = XGraphics.FromPdfPage(page);
                        DrawCap(gd,g, p, i, pc);
                        DrawGrid(gd, g, p, i);
					}
					document.Save(filePath);
                    ret = true;

                }
            }
            catch (Exception)
            {
                ret = false;
            }
            return ret;
        }
        // **********************************************************************************************


    }
    /// <summary>
    /// 日本語フォントのためのフォントリゾルバー
    /// </summary>
    public class JapaneseFontResolver : IFontResolver
    {
        // 源真ゴシック（ http://jikasei.me/font/genshin/）
        private static readonly string GEN_SHIN_GOTHIC_BOLD_TTF = "AE_RemapTria.T_PDF.GenShinGothic-Monospace-Bold.ttf";
        private static readonly string GEN_SHIN_GOTHIC_LIGHT_TTF = "AE_RemapTria.T_PDF.GenShinGothic-Monospace-Light.ttf";
        private static readonly string GEN_SHIN_GOTHIC_MEDIUM_TTF = "AE_RemapTria.T_PDF.GenShinGothic-Monospace-Medium.ttf";


        public string DefaultFontName => throw new NotImplementedException();

        public byte[]? GetFont(string faceName)
        {
            switch (faceName)
            {
                case "GenShinGothic#Bold":
                    return LoadFontData(GEN_SHIN_GOTHIC_BOLD_TTF);
                case "GenShinGothic#Light":
                    return LoadFontData(GEN_SHIN_GOTHIC_LIGHT_TTF);
                case "GenShinGothic#Medium":
                    return LoadFontData(GEN_SHIN_GOTHIC_MEDIUM_TTF);
            }
            return null;
        }

        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            var fontName = familyName.ToLower();

            switch (fontName)
            {
                case "gen shin gothic":
                    if (isBold)
                    {
                        return new FontResolverInfo("GenShinGothic#Bold");
                    }
                    else if (isItalic)
                    {
                        return new FontResolverInfo("GenShinGothic#Light");
                    }
                    else
                    {
                        return new FontResolverInfo("GenShinGothic#Medium");
                    }
            }

            // デフォルトのフォント
            return PlatformFontResolver.ResolveTypeface("Arial", isBold, isItalic);
        }

        // 埋め込みリソースからフォントファイルを読み込む
        private byte[] LoadFontData(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new ArgumentException("No resource with name " + resourceName);
                }

                var count = (int)stream.Length;
                var data = new byte[count];
                stream.Read(data, 0, count);
                return data;
            }
        }
    }
}
