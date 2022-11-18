using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;

using BRY;

namespace AE_RemapTria
{
	public class FilterChangedArg : EventArgs
	{
		public string[] Filter;
		public FilterChangedArg(string[] v)
		{
			Filter = v;
		}
	}
	public class FilterItem
	{
		public string Ext="";
		public bool IsUse=false;
		public FilterItem(string e,bool b=true)
		{
			Ext = e;
			IsUse = b;
		}
		public FilterItem(JsonObject jo)
		{
			FromJsonObject(jo);
		}
		public JsonObject ToJsonObject()
		{
			JsonObject obj = new JsonObject();
			obj.Add("ext", Ext);
			obj.Add("isUse", IsUse);
			return obj;
		}
		public bool FromJsonObject(JsonObject jo)
		{
			if(jo ==null) return false;
			if(jo.ContainsKey("ext")==false) return false;
			Ext = jo["ext"].GetValue<string>();
			if(jo.ContainsKey("isUse"))
			{
				IsUse = jo["isUse"].GetValue<bool>();
			}
			else
			{
				IsUse = false;
			}
			return true;
		}
	}

	public partial class T_FileExtFilter : T_BaseControl
	{
		#region Event
		public delegate void FilterChangedHandler(object sender, FilterChangedArg e);
		public event FilterChangedHandler? FilterChanged;
		protected virtual void OnFilterChanged(FilterChangedArg e)
		{
			if (FilterChanged != null)
			{
				FilterChanged(this, e);
			}
		}
		#endregion
		private Color m_SelectedColor = Color.FromArgb(90, 90, 120);
		[Category("_AE_Remap")]
		public Color SelectedColor
		{
			get { return m_SelectedColor; }
			set { m_SelectedColor = value; this.Invalidate(); }
		}
		[Category("_AE_Remap")]
		public string[] Filter
		{
			get
			{
				List<string> lst = new List<string>();
				for(int i=0;i< m_filters.Count;i++)
				{
					if (m_filters[i].IsUse==true)
					{
						lst.Add(m_filters[i].Ext);
					}
				}
				return lst.ToArray();
			}
		}
		// ***************************************************
		public JsonObject ToJsonObject()
		{
			JsonObject jo = new JsonObject();

			JsonArray ja = new JsonArray();
			//if()
			if(m_filters.Count>0)
			{
				foreach(FilterItem item in m_filters)
				{
					ja.Add(item.ToJsonObject());
				}
			}
			jo.Add("filters", ja);

			return jo;
		}
		public void FromJsonObject(JsonObject jo)
		{
			if(jo==null) return;
			if (jo.ContainsKey("filters"))
			{
				JsonArray ja = jo["filters"].AsArray();
				if (ja.Count > 0)
				{
					m_filters.Clear();
					foreach (JsonObject joo in ja)
					{
						FilterItem fi = new FilterItem(joo);
						AddFilter(fi.Ext, fi.IsUse);
					}
				}
			}
			OnFilterChanged(new FilterChangedArg(Filter));
		}

		// ***************************************************
		//private string[] m_exts = new string[] { ".ardj.json", ".ardj", ".ard", ".sts", ".*" };
		//private bool[] m_extsBool = new bool[] { false, false, false, false, false };
		private List<FilterItem> m_filters = new List<FilterItem>();

		private int[] m_extsYPos = new int[] { 10,10,10,10,10 };
		private int m_FilterIndex = 0;
		// ***************************************************
		public T_FileExtFilter()
		{
			this.ForeColor = Color.FromArgb(200, 200, 250);
			InitializeComponent();
			AddFilter(".ardj.json", true);
			AddFilter(".ardj", true);
			AddFilter(".ard", true);
			AddFilter(".sts", false);
			AddFilter(".*", false);

		}
		// ***************************************************
		public int FindFilter(string e)
		{
			int ret = -1;
			if(m_filters.Count>0)
			{
				string e2 = e.ToLower();
				int cnt = 0;
				foreach(FilterItem item in m_filters)
				{
					if(item.Ext.ToLower()==e)
					{
						ret = cnt;
						break;
					}
					cnt++;
				}
			}
			return ret;
		}
		public bool AddFilter(string e,bool isUse=true)
		{
			if ((e==null)||(e==""))return false;
			if (e[0] != '.') e = "." + e;
			int idx = FindFilter(e);
			if (idx >= 0) return false;
			m_filters.Add(new FilterItem(e, isUse));
			return true;
		}
		// *****************************************************************************************
		protected override void OnMouseDown(MouseEventArgs e)
		{
			for(int i=0; i<m_extsYPos.Length; i++)
			{
				if (e.X < m_extsYPos[i])
				{
					m_FilterIndex = i;
					if(m_filters[i].Ext==".*")
					{
						for(int j=0; j< m_filters.Count;j++) m_filters[j].IsUse = false;
					}
					else
					{
						m_filters[i].IsUse = !m_filters[i].IsUse;
					}
					OnFilterChanged(new FilterChangedArg(Filter));
					this.Invalidate();
					break;
				}
			}
			base.OnMouseDown(e);
		}
		/*
		protected override void OnPaint(PaintEventArgs pe)
		{
			//base.OnPaint(pe);
			Graphics g = pe.Graphics;
			SolidBrush sb = new SolidBrush(Color.Transparent);
			Pen p = new Pen(this.ForeColor);
			StringFormat sf = new StringFormat();
			sf.Alignment = StringAlignment.Center;
			sf.LineAlignment = StringAlignment.Center;

			try
			{
				Rectangle r;
				Fill(g, sb);
				if(m_exts.Length>0)
				{
					int left = 0;
					for(int i=0; i< m_exts.Length; i++)
					{
						Size strSize = TextRenderer.MeasureText(g, m_exts[i], this.Font);
						r = new Rectangle(left, 0, strSize.Width + 10 -1, this.Height-1);
						m_extsYPos[i] = r.Right;
						if (m_extsBool[i])
						{
							sb.Color = m_SelectedColor;
							g.FillRectangle(sb, r);
						}
						sb.Color = T_G.EnabledCol(this.ForeColor, m_extsBool[i]);
						g.DrawString(m_exts[i], this.Font, sb, r, sf);
						g.DrawRectangle(p, r);
						left += strSize.Width + 14;
					}
				}

			}
			finally
			{
				sb.Dispose();
				p.Dispose();
			}
		}
		*/
		protected override void OnPaint(PaintEventArgs pe)
		{
			Graphics g = pe.Graphics;
			SolidBrush sb = new SolidBrush(Color.Transparent);
			Pen p = new Pen(this.ForeColor);
			StringFormat sf = new StringFormat();
			sf.Alignment = StringAlignment.Center;
			sf.LineAlignment = StringAlignment.Center;

			try
			{
				Rectangle r;
				Fill(g, sb);
				if (m_filters.Count > 0)
				{
					int left = 0;
					for (int i = 0; i < m_filters.Count; i++)
					{
						Size strSize = TextRenderer.MeasureText(g, m_filters[i].Ext, this.Font);
						r = new Rectangle(left, 0, strSize.Width + 10 - 1, this.Height - 1);
						m_extsYPos[i] = r.Right;
						if (m_filters[i].IsUse)
						{
							sb.Color = m_SelectedColor;
							g.FillRectangle(sb, r);
						}
						sb.Color = T_G.EnabledCol(this.ForeColor, m_filters[i].IsUse);
						g.DrawString(m_filters[i].Ext, this.Font, sb, r, sf);
						g.DrawRectangle(p, r);
						left += strSize.Width + 14;
					}
				}

			}
			finally
			{
				sb.Dispose();
				p.Dispose();
			}
		}
	}
}
