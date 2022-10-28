using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;


namespace BRY
{
    // ********************************************************
    public enum Kagi_Style
    {
        TL,
        TR,
        BL,
        BR
    }
	// ********************************************************
	public class DrawKagiPrm
    {
        public Point Loc = new Point(0, 0);
        public int HLength = 40;
        public int HWeight = 10;
        public int VLength = 40;
        public int VWeight = 10;
        public Kagi_Style Style= Kagi_Style.TL;
        public DrawKagiPrm()
        {

        }
        public DrawKagiPrm(Point p, int HL, int HW, int VL, int VW)
        {
            Loc = p;
            HLength = HL;
            HWeight = HW;
            VLength = VL;
            VWeight = VW;
        }

    }
	// ********************************************************
	public enum Scale_Style
	{
		None = 0,
		Top = 1,
		Left = 2,
		Bottom = 4,
		Right = 8
	}
	public class DrawScaleSubPrm
    {
		public int Length = 6;
		public int Weight = 6;
		public int Count = 2;
		public int Inter = 10;
		public DrawScaleSubPrm()
        {

        }

		public DrawScaleSubPrm(int l,int w,int c,int it)
        {
            Length = l;
            Weight = w;
            Count = c;
            Inter = it;
        }
	}
	public class DrawSCalePrm
    {
        public Point Loc = new Point(0,0);
        public int CenterLength = 16;
		public int CenterWeight = 4;
        public DrawScaleSubPrm[] s = new DrawScaleSubPrm[3];
        public Scale_Style Style = Scale_Style.Left;
        public DrawSCalePrm()
        {
            s[0] = new DrawScaleSubPrm(05, 01, 04, 10);
            s[1] = new DrawScaleSubPrm(12, 02, 04, 20);
            s[2] = new DrawScaleSubPrm(08, 04, 01, 120);
		}
		public int[] Inter
		{
			get { return new int[] { s[0].Inter, s[1].Inter, s[2].Inter }; }
			set
			{
				if (value.Length > 0) s[0].Inter = value[0];
				if (value.Length > 1) s[1].Inter = value[1];
				if (value.Length > 2) s[2].Inter = value[2];
			}

		}
		public int[] Weight
		{
			get { return new int[] { s[0].Weight, s[1].Weight, s[2].Weight }; }
			set
			{
				if (value.Length > 0) s[0].Weight = value[0];
				if (value.Length > 1) s[1].Weight = value[1];
				if (value.Length > 2) s[2].Weight = value[2];
			}
		}
		public int[] Length
		{
			get { return new int[] { s[0].Length, s[1].Length, s[2].Length }; }
			set
			{
				if (value.Length > 0) s[0].Length = value[0];
				if (value.Length > 1) s[1].Length = value[1];
				if (value.Length > 2) s[2].Length = value[2];
			}
		}
		public int[] Count
		{
			get { return new int[] { s[0].Count, s[1].Count, s[2].Count }; }
			set
			{
                if (value.Length > 0) s[0].Count = value[0];
				if (value.Length > 1) s[1].Count = value[1];
				if (value.Length > 2) s[2].Count = value[2];
			}
		}
	}

	public enum ArrowDir
	{
		Top,
		Right,
		Bottom,
		Left,
	}
	public class DrawArrowPrm
	{
		public Point Loc = new Point(0, 0);
		public int Width = 100;
		public int Height = 100;
		public ArrowDir Dir = ArrowDir.Top;
        public bool IsCut = false;
		public DrawArrowPrm()
		{ }
		public DrawArrowPrm(int w, int h)
		{
			Width = w;
			Height = h;
		}

	}


	// ********************************************************
	public class T_G
    {
        static public string KeyInfo(Keys k)
        {
            string ret = "";

            Keys up = (Keys)((uint)k & 0xFFFF0000);
			Keys lo = (Keys)((uint)k & 0x0000FFFF);

            if( (up & Keys.Shift)== Keys.Shift)
			{
                ret += "Shift";
            }
			if ((up & Keys.Control) == Keys.Control)
            {
                if (ret != "") ret += "+";
				ret += "Control";
			}
			if ((up & Keys.Alt) == Keys.Alt)
			{
				if (ret != "") ret += "+";
				ret += "Alt";
			}
			if (ret != "") ret += "+";
            ret += lo.ToString();

            return ret;
		}
		// ********************************************************
		static public void DrawKagi(
            Graphics g,
            SolidBrush sb,
            Point loc,
            int HLength,
            int HWeight,
            int VLength,
            int VWeight,
            Kagi_Style ks
            )
        {
            DrawKagiPrm kp = new DrawKagiPrm();
            kp.Loc = loc;
            kp.HLength = HLength;
            kp.HWeight = HWeight;
            kp.VLength = VLength;
            kp.VWeight = VWeight;
            kp.Style = ks;
            DrawKagi(g, sb, kp);

        }
		// ********************************************************
		static public void DrawKagi(Graphics g, SolidBrush sb, DrawKagiPrm prm,Kagi_Style ks)
        {
            prm.Style = ks;
            DrawKagi(g, sb, prm);

		}
		// ********************************************************
		static public void DrawKagi(Graphics g, SolidBrush sb, DrawKagiPrm prm)
        {
            int x = 0;
            int y = 0;
            int w = 0;
            int h = 0;
            Rectangle r = new Rectangle(x, y, w, h);
            switch (prm.Style)
            {
                case Kagi_Style.TL:
                    x = prm.Loc.X;
                    y = prm.Loc.Y;
                    w = prm.HLength;
                    h = prm.HWeight;
                    if (w > 0 && h > 0)
                    {
                        r = new Rectangle(x, y, w, h);
                        g.FillRectangle(sb, r);
                    }
                    x = prm.Loc.X;
                    y = prm.Loc.Y + prm.HWeight;
                    w = prm.VWeight;
                    h = prm.VLength - prm.HWeight;
                    if (w > 0 && h > 0)
                    {
                        r = new Rectangle(x, y, w, h);
                        g.FillRectangle(sb, r);
                    }
                    break;

                case Kagi_Style.TR:
                    x = prm.Loc.X - prm.HLength - 1;
                    y = prm.Loc.Y;
                    w = prm.HLength;
                    h = prm.HWeight;
                    if (w > 0 && h > 0)
                    {
                        r = new Rectangle(x, y, w, h);
                        g.FillRectangle(sb, r);
                    }
                    x = prm.Loc.X - prm.VWeight - 1;
                    y = prm.Loc.Y + prm.HWeight;
                    w = prm.VWeight;
                    h = prm.VLength - prm.HWeight;
                    if (w > 0 && h > 0)
                    {
                        r = new Rectangle(x, y, w, h);
                        g.FillRectangle(sb, r);
                    }
                    break;
                case Kagi_Style.BL:
                    x = prm.Loc.X;
                    y = prm.Loc.Y - prm.HWeight - 1;
                    w = prm.HLength;
                    h = prm.HWeight;
                    if (w > 0 && h > 0)
                    {
                        r = new Rectangle(x, y, w, h);
                        g.FillRectangle(sb, r);
                    }
                    x = prm.Loc.X;
                    y = prm.Loc.Y - prm.VLength - 1;
                    w = prm.VWeight;
                    h = prm.VLength - prm.HWeight;
                    if (w > 0 && h > 0)
                    {
                        r = new Rectangle(x, y, w, h);
                        g.FillRectangle(sb, r);
                    }
                    break;
                case Kagi_Style.BR:
                    x = prm.Loc.X - prm.HLength - 1;
                    y = prm.Loc.Y - prm.HWeight - 1;
                    w = prm.HLength;
                    h = prm.HWeight;
                    if (w > 0 && h > 0)
                    {
                        r = new Rectangle(x, y, w, h);
                        g.FillRectangle(sb, r);
                    }
                    x = prm.Loc.X - prm.VWeight - 1;
                    y = prm.Loc.Y - prm.VLength - 1;
                    w = prm.VWeight;
                    h = prm.VLength - prm.HWeight;
                    if (w > 0 && h > 0)
                    {
                        r = new Rectangle(x, y, w, h);
                        g.FillRectangle(sb, r);
                    }
                    break;
            }
        }
		// ********************************************************
		static public void DrawScale(Graphics g, Pen p, DrawSCalePrm sp,Scale_Style ss)
        {
            sp.Style = ss;
            DrawScale(g, p, sp);

		}
		// ********************************************************
		static public void DrawScale(Graphics g,Pen p, DrawSCalePrm sp)
        {
            float pw = p.Width;
            Color pc = p.Color;
            Point p0 = new Point(0, 0);
			Point p1 = new Point(0, 0);
            int h0 = 0;
            int h1 = 0;
            int v0 = 0;
			int v1 = 0;
            CompositingMode cm = g.CompositingMode;
            g.CompositingMode = CompositingMode.SourceCopy;
			switch (sp.Style)
            {
                case Scale_Style.Left:
                    // center
                    if ((sp.CenterWeight > 0) && (sp.CenterLength>0))
                    {
                        p.Width = sp.CenterWeight;
                        h0 = sp.Loc.X;
                        h1 = sp.Loc.X + sp.CenterLength;
                        v0 = sp.Loc.Y;
                        g.DrawLine(p, h0, v0, h1, v0);
                    }
                    //ScaleA
                    for (int cc = 0; cc < sp.s.Length; cc++)
                    {
                        if (sp.s[cc].Count > 0)
                        {
                            p.Width = sp.s[cc].Weight;
                            if (sp.s[cc].Weight > 0)
                            {
                                for (int i = 1; i <= sp.s[cc].Count; i++)
                                {
                                    if (sp.s[cc].Length > 0)
                                    {
                                        h0 = sp.Loc.X;
                                        h1 = sp.Loc.X + sp.s[cc].Length;
                                        v0 = sp.Loc.Y - sp.s[cc].Inter * i;
                                        v1 = sp.Loc.Y + sp.s[cc].Inter * i;
                                        g.DrawLine(p, h0, v0, h1, v0);
                                        g.DrawLine(p, h0, v1, h1, v1);
                                    }

                                }
                            }
						}
                    }
                    break;
				case Scale_Style.Right:
                    // center
                    if ((sp.CenterWeight > 0) && (sp.CenterLength > 0))
                    {
                        p.Width = sp.CenterWeight;
                        h0 = sp.Loc.X - sp.CenterLength;
                        h1 = sp.Loc.X;
                        v0 = sp.Loc.Y;
                        g.DrawLine(p, h0, v0, h1, v0);
                    }

                       //ScaleA
					for (int cc = 0; cc < sp.s.Length; cc++)
					{
						if (sp.s[cc].Count > 0)
						{
							p.Width = sp.s[cc].Weight;
                            if (sp.s[cc].Weight > 0)
                            {
                                for (int i = 1; i <= sp.s[cc].Count; i++)
                                {
                                    if (sp.s[cc].Length > 0)
                                    {
                                        h0 = sp.Loc.X - sp.s[cc].Length;
                                        h1 = sp.Loc.X;
                                        v0 = sp.Loc.Y - sp.s[cc].Inter * i;
                                        v1 = sp.Loc.Y + sp.s[cc].Inter * i;
                                        g.DrawLine(p, h0, v0, h1, v0);
                                        g.DrawLine(p, h0, v1, h1, v1);
                                    }
                                }
                            }
						}
					}
					break;
				case Scale_Style.Top:
                    // center
                    if ((sp.CenterWeight > 0) && (sp.CenterLength > 0))
                    {
                        p.Width = sp.CenterWeight;
                        h0 = sp.Loc.X;
                        v0 = sp.Loc.Y;
                        v1 = sp.Loc.Y + sp.CenterLength;
                        g.DrawLine(p, h0, v0, h0, v1);
                    }
					//ScaleA
					for (int cc = 0; cc < sp.s.Length; cc++)
					{
						if (sp.s[cc].Count > 0)
						{
							p.Width = sp.s[cc].Weight;
                            if (sp.s[cc].Weight>0)
                            {
                                for (int i = 1; i <= sp.s[cc].Count; i++)
                                {
                                    if (sp.s[cc].Length > 0)
                                    {
                                        h0 = sp.Loc.X - sp.s[cc].Inter * i;
                                        h1 = sp.Loc.X + sp.s[cc].Inter * i;

                                        v0 = sp.Loc.Y;
                                        v1 = sp.Loc.Y + sp.s[cc].Length;
                                        g.DrawLine(p, h0, v0, h0, v1);
                                        g.DrawLine(p, h1, v0, h1, v1);
                                    }

                                }
                            }
						}
					}
					break;
				case Scale_Style.Bottom:
                    // center
                    if ((sp.CenterWeight > 0) && (sp.CenterLength > 0))
                    {
                        p.Width = sp.CenterWeight;
                        h0 = sp.Loc.X;
                        v0 = sp.Loc.Y - sp.CenterLength;
                        v1 = sp.Loc.Y;
                        g.DrawLine(p, h0, v0, h0, v1);
                    }

					//ScaleA
					for (int cc = 0; cc < sp.s.Length; cc++)
					{
						if (sp.s[cc].Count > 0)
						{
							p.Width = sp.s[cc].Weight;
                            if (sp.s[cc].Weight > 0)
                            {
                                for (int i = 1; i <= sp.s[cc].Count; i++)
                                {
                                    if (sp.s[cc].Length>0)
                                    {
                                        h0 = sp.Loc.X - sp.s[cc].Inter * i;
                                        h1 = sp.Loc.X + sp.s[cc].Inter * i;

                                        v0 = sp.Loc.Y - sp.s[cc].Length;
                                        v1 = sp.Loc.Y;
                                        g.DrawLine(p, h0, v0, h0, v1);
                                        g.DrawLine(p, h1, v0, h1, v1);
                                    }

                                }
                            }
						}
					}
					break;
			}
            p.Width = pw;
            p.Color = pc;
            g.CompositingMode = cm;

		}
		// ********************************************************
  
		static public void DrawArrow(Graphics g,SolidBrush sb,DrawArrowPrm ap)
        {
			Point[] pnts = new Point[0];
			if (ap.IsCut == false)
            {
                pnts = new Point[4];

				switch (ap.Dir)
                {
                    case ArrowDir.Top:
						pnts[0] = new Point(ap.Loc.X, ap.Loc.Y);
						pnts[1] = new Point(ap.Loc.X + ap.Width / 2, ap.Loc.Y - ap.Height);
						pnts[2] = new Point(ap.Loc.X - ap.Width / 2, ap.Loc.Y - ap.Height);
						pnts[3] = new Point(ap.Loc.X, ap.Loc.Y);
						break;
                    case ArrowDir.Right:
						pnts[0] = new Point(ap.Loc.X, ap.Loc.Y);
						pnts[1] = new Point(ap.Loc.X + ap.Height, ap.Loc.Y + ap.Width / 2);
						pnts[2] = new Point(ap.Loc.X + ap.Height, ap.Loc.Y - ap.Width / 2);
						pnts[3] = new Point(ap.Loc.X, ap.Loc.Y);
						break;
                    case ArrowDir.Bottom:
						pnts[0] = new Point(ap.Loc.X, ap.Loc.Y);
						pnts[1] = new Point(ap.Loc.X + ap.Width / 2, ap.Loc.Y + ap.Height);
						pnts[2] = new Point(ap.Loc.X - ap.Width / 2, ap.Loc.Y + ap.Height);
						pnts[3] = new Point(ap.Loc.X, ap.Loc.Y);
                        break;
                    case ArrowDir.Left:
						pnts[0] = new Point(ap.Loc.X, ap.Loc.Y);
						pnts[1] = new Point(ap.Loc.X - ap.Height, ap.Loc.Y + ap.Width / 2);
						pnts[2] = new Point(ap.Loc.X - ap.Height, ap.Loc.Y - ap.Width / 2);
						pnts[3] = new Point(ap.Loc.X, ap.Loc.Y);

                        break;
                }
            }
            else
            {
				pnts = new Point[7];
				switch (ap.Dir)
				{
					case ArrowDir.Bottom:
						pnts[0] = new Point(ap.Loc.X, ap.Loc.Y);
                        pnts[1] = new Point(ap.Loc.X + ap.Width / 2, ap.Loc.Y + ap.Height);
						pnts[2] = new Point(ap.Loc.X + ap.Width / 4, ap.Loc.Y + ap.Height);
						pnts[3] = new Point(ap.Loc.X, ap.Loc.Y + ap.Height/2);
						pnts[4] = new Point(ap.Loc.X - ap.Width / 4, ap.Loc.Y + ap.Height);
						pnts[5] = new Point(ap.Loc.X - ap.Width / 2, ap.Loc.Y + ap.Height);
						pnts[6] = pnts[0];
						break;
					case ArrowDir.Right:
						pnts[0] = new Point(ap.Loc.X, ap.Loc.Y);
                        pnts[1] = new Point(ap.Loc.X + ap.Height, ap.Loc.Y - ap.Width / 2);
						pnts[2] = new Point(ap.Loc.X + ap.Height, ap.Loc.Y - ap.Width / 4);
						pnts[3] = new Point(ap.Loc.X + ap.Height/2, ap.Loc.Y);
						pnts[4] = new Point(ap.Loc.X + ap.Height, ap.Loc.Y + ap.Width / 4);
						pnts[5] = new Point(ap.Loc.X + ap.Height, ap.Loc.Y + ap.Width / 2);
                        pnts[6] = pnts[0];
						break;
					case ArrowDir.Top:
						pnts[0] = new Point(ap.Loc.X, ap.Loc.Y);
						pnts[1] = new Point(ap.Loc.X + ap.Width / 2, ap.Loc.Y - ap.Height);
						pnts[2] = new Point(ap.Loc.X + ap.Width / 4, ap.Loc.Y - ap.Height);
						pnts[3] = new Point(ap.Loc.X, ap.Loc.Y - ap.Height / 2);
						pnts[4] = new Point(ap.Loc.X - ap.Width / 4, ap.Loc.Y - ap.Height);
						pnts[5] = new Point(ap.Loc.X - ap.Width / 2, ap.Loc.Y - ap.Height);
						pnts[6] = pnts[0];
						break;
					case ArrowDir.Left:
						pnts[0] = new Point(ap.Loc.X, ap.Loc.Y);
						pnts[1] = new Point(ap.Loc.X - ap.Height, ap.Loc.Y - ap.Width / 2);
						pnts[2] = new Point(ap.Loc.X - ap.Height, ap.Loc.Y - ap.Width / 4);
						pnts[3] = new Point(ap.Loc.X - ap.Height / 2, ap.Loc.Y);
						pnts[4] = new Point(ap.Loc.X - ap.Height, ap.Loc.Y + ap.Width / 4);
						pnts[5] = new Point(ap.Loc.X - ap.Height, ap.Loc.Y + ap.Width / 2);
						pnts[6] = pnts[0];
						break;
				}
			}
			g.FillPolygon(sb,pnts);

		}
        static public Color EnabledCol(Color col,bool b)
        {
            if(b)
            {
                return col;
            }
            else
            {
                return Color.FromArgb(col.A,col.R/2, col.G/2, col.B/2);
            }
        }
	}
}
