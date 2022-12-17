using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AE_RemapTria
{
	public enum TriangleStyle
	{
		Top,
		Right,
		Bottom,
		Left,
		Center
	}
	public class TRc
    {
        static public void DrawFrame(Graphics g, Pen p, int weight, Rectangle r)
        {
            float pw = p.Width;
            p.Width = 1;
            Rectangle r2 = new Rectangle(r.X, r.Y, r.Width - 1, r.Height - 1);
            for (int i = 0; i < weight; i++)
            {
                g.DrawRectangle(p, r2);
                r2 = new Rectangle(r2.X + 1, r2.Y + 1, r2.Width - 2, r2.Height - 2);
            }
        }
		static public void Fill(Graphics g, SolidBrush sb, Rectangle r)
		{
			Rectangle r2 = new Rectangle(r.X, r.Y, r.Width - 1, r.Height -1);
			g.FillRectangle(sb, r2);
		}
		static public void DrawFrame(Graphics g, SolidBrush sb, Rectangle r,Padding p)
		{
            if((p.Left<=0)&& (p.Right<=0)&&(p.Top<=0)&&(p.Bottom<=0))
            {
                return;
            }
			if ((p.Top + p.Bottom>=r.Height)|| (p.Right + p.Left >= r.Width))
			{
                g.FillRectangle(sb, r);
                return;
            }
            Rectangle r2;
            if(p.Top>0)
            {
                r2 = new Rectangle(r.Left, r.Top, r.Width, p.Top);
                g.FillRectangle(sb, r2);
            }
			if (p.Bottom > 0)
			{
				r2 = new Rectangle(r.Left, r.Bottom- p.Bottom-1, r.Width, p.Bottom);
				g.FillRectangle(sb, r2);
			}
            int h = r.Height - p.Top - p.Bottom;
            if (h > 0)
            {
                if (p.Left > 0)
                {
                    r2 = new Rectangle(
                        r.Left, 
                        r.Top + p.Top, 
                        p.Left,
                        h);
					g.FillRectangle(sb, r2);
				}
				if (p.Right > 0)
                {
                    r2 = new Rectangle(
                        r.Right - p.Right,
                        r.Top + p.Top,
                        p.Right,
                        h);
					g.FillRectangle(sb, r2);
				}
			}
		}
		static public PointF[] TrianglePolygon(PointF pos, float length, float rot)
		{
			PointF[] pnts = new PointF[3];
			float r = rot;
			for (int i = 0; i < 3; i++)
			{
				double x = (double)length * Math.Sin(r * Math.PI / 180);
				double y = (double)length * Math.Cos(r * Math.PI / 180);
				pnts[i] = new PointF((float)x + pos.X, -(float)y + pos.Y);
				r += 360 / 3;
			}
			return pnts;
		}
		static public void Triangle(Graphics g, Pen p, SolidBrush? sb, PointF pos, float length, float rot)
		{
			if ((sb != null) && (sb.Color.A > 0))
			{
				PointF[] pnts = TrianglePolygon(pos, length, rot);
				g.FillPolygon(sb, pnts);
			}
			if ((p.Width > 0) && (p.Color.A > 0))
			{
				PointF[] pnts = TrianglePolygon(pos, length- p.Width/2, rot);
				g.DrawPolygon(p, pnts);
			}
		}
		static public PointF[] TrianglePolygon(RectangleF rct, TriangleStyle ts = TriangleStyle.Top)
		{
			PointF[] pnts = new PointF[3];
			switch (ts)
			{
				case TriangleStyle.Top:
					pnts[0] = new PointF((float)rct.Left + (float)rct.Width / 2, (float)rct.Top);
					pnts[1] = new PointF((float)rct.Left + (float)rct.Width - 1, (float)rct.Bottom - 1);
					pnts[2] = new PointF((float)rct.Left, (float)rct.Bottom - 1);
					break;
				case TriangleStyle.Right:
					pnts[0] = new PointF((float)rct.Left, (float)rct.Top);
					pnts[1] = new PointF((float)rct.Left + (float)rct.Width - 1, (float)rct.Top + (float)rct.Height / 2 - 1);
					pnts[2] = new PointF((float)rct.Left, (float)rct.Bottom - 1);
					break;
				case TriangleStyle.Bottom:
					pnts[0] = new PointF((float)rct.Left, (float)rct.Top);
					pnts[1] = new PointF((float)rct.Left + (float)rct.Width - 1, (float)rct.Top);
					pnts[2] = new PointF((float)rct.Left + (float)rct.Width / 2, (float)rct.Bottom - 1);
					break;
				case TriangleStyle.Left:
				default:
					pnts[0] = new PointF((float)rct.Left + (float)rct.Width - 1, (float)rct.Top);
					pnts[1] = new PointF((float)rct.Left, (float)rct.Top + (float)rct.Height / 2 - 1);
					pnts[2] = new PointF((float)rct.Left + (float)rct.Width - 1, (float)rct.Bottom - 1);
					break;
			}
			return pnts;
		}
		static public void Triangle(Graphics g, Pen p, SolidBrush? sb, Rectangle rct, float pw, TriangleStyle ts = TriangleStyle.Top)
		{
			PointF[] pnts = TrianglePolygon(rct, ts);

			if (sb != null)
			{
				g.FillPolygon(sb, pnts);
			}
			g.DrawPolygon(p, pnts);
		}
		static public PointF[] PolygonPolygon(int cnt, PointF pos, float length, float rot)
		{
			if (cnt < 3) cnt = 3; else if (cnt > 12) cnt = 12;
			PointF[] pnts = new PointF[cnt];


			float r = rot;
			for (int i = 0; i < cnt; i++)
			{
				double x = (double)length * Math.Sin(r * Math.PI / 180);
				double y = (double)length * Math.Cos(r * Math.PI / 180);
				pnts[i] = new PointF((float)x + pos.X, -(float)y + pos.Y);
				r += 360 / cnt;
			}
			return pnts;

		}
		static public void Polygon(Graphics g, Pen p, SolidBrush? sb, int cnt, PointF pos, float length, float rot)
		{
			PointF[] pnts = PolygonPolygon(cnt, pos, length, rot);
			if ((sb != null) && (sb.Color.A > 0)) ;
			{
				g.FillPolygon(sb, pnts);
			}
			g.DrawPolygon(p, pnts);
		}
		static public PointF[] Parallelogram(float x, float y, float w, float h, float rot)
		{
			PointF[] points = new PointF[4];
			float ax = Tan(h, rot);
			points[0] = new PointF(x, y);
			points[1] = new PointF(x + w, y);
			points[2] = new PointF(x + w + ax, y + h);
			points[3] = new PointF(x + ax, y + h);
			return points;
		}
		static public PointF[] AddPoints(PointF[] ps, float x, float y)
		{
			PointF[] ret = new PointF[ps.Length];
			if (ps.Length > 0)
			{
				for (int i = 0; i < ps.Length; i++)
				{
					ret[i].X = ps[i].X + x;
					ret[i].Y = ps[i].Y + y;
				}
			}
			return ret;
		}

		static public void DrawZebra(Graphics g, SolidBrush sb, Rectangle r, float w, float rot)
		{
			bool mflag = (rot < 0);

			if (mflag == false)
			{
				PointF[] points = Parallelogram(r.Left, r.Top, w, r.Height, rot);
				while (points[3].X < r.Right)
				{
					g.FillPolygon(sb, points);
					points = AddPoints(points, w * 2, 0);
				}
			}
			else
			{
				float ax = Tan(r.Height, rot);
				PointF[] points = Parallelogram(r.Left - ax, r.Top, w, r.Height, rot);
				while (points[0].X < r.Right)
				{
					g.FillPolygon(sb, points);
					points = AddPoints(points, w * 2, 0);
				}

			}

		}
		static private float Tan(float h, float rot)
		{
			float r = Math.Abs(rot);
			if (r > 45) r = 45;
			float v = -1;
			if (rot < 0) v = 1;
			return (float)Math.Tan((double)r * Math.PI / 180) * h * v;
		}

	}
}
